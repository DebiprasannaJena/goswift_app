#region PageInfo

//*************************************************************************************************************************************************************
// File Name             :   InvestorRegistrationUser.aspx.cs
// Description           :   Manage Investor Registration details
// Created by            :   AMit Sahoo
// Created On            :   16 July 2017
// Modification History  :
//                          <CR no.>      <Date>                <Modified by>          <Modification Summary>                                       <Instructed By>                                                        
//
// FUNCTION NAME         :   1           25-Aug-2018            Sushant Jena        PAN based registration with parent child unit creation.         Smruti Ranjan Nayak
//                           2           12-Sep-2018            Sushant Jena        Data Warehouse service integration for PAN based registration   Smruti Ranjan Nayak
//                           3           30-Aug-2019            Sushant Jena        Changes in Investment Level due to AIM Integration              Rama Rao Teki
//*************************************************************************************************************************************************************

#endregion

#region Namespace

using System;
using System.Web.UI;
using System.Data;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

public partial class InvestorRegistrationUser : System.Web.UI.Page
{
    #region Variables

    
    InvestorRegistration objRegService = new InvestorRegistration();

    string action = "";

    /// Get Project Name From Web.Config File   
    readonly string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #endregion

    #region Page_load

    ///// PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        Lbl_PAN.Text = "";
        Img_Success.Visible = false;

        if (!Page.IsPostBack)
        {
            Txt_Dob.Attributes.Add("readonly", "readonly");           

            Txt_PAN.Focus();
            CommonHelperCls ob = new CommonHelperCls();

            try
            {
                ///Fill Salutation
                ob.BindDropDown(DrpDwn_Salutation, "ID", "VCH_SALUTATION", "Select ID, VCH_SALUTATION from M_SALUTATION");

                ///Fill Security Questions
                ob.BindDropDown(DrpDwn_Question, "ID", "VCH_QUESTION", "Select ID, VCH_QUESTION from M_Security_Question");

                ///Fill Districts
                FillDistrict();

                ///Fill Sectors
                FillSector();

                
                //Dynamic Notification 
                if (ConfigurationManager.AppSettings["Notice"].ToString() == "On")
                {
                    DynamicScrolingText();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Registration");
            }
        }
    }

    #endregion

    #region FunctionUsed

    //// Fill District DropDownList
    public void FillDistrict()
    {
        try
        {
            action = "FD";
            DataTable dtdistrict = objRegService.BindDistrict(action);
            DrpDwn_District.DataSource = dtdistrict;
            DrpDwn_District.DataTextField = "vchDistrictName";
            DrpDwn_District.DataValueField = "intDistrictId";
            DrpDwn_District.DataBind();
            DrpDwn_District.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    //// Fill Sector DropDownList
    public void FillSector()
    {
        try
        {
            action = "FS";
            DataTable dtSector = objRegService.BindDistrict(action);
            DrpDwn_Sector.DataSource = dtSector;
            DrpDwn_Sector.DataTextField = "VCH_SECTOR";
            DrpDwn_Sector.DataValueField = "INT_SECTORID";
            DrpDwn_Sector.DataBind();
            DrpDwn_Sector.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    //// Method to Upload Document
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload only .pdf file !</strong>', '" + strProjName + "'); </script>", false);
                    bReturnValue = false;
                    return bReturnValue;
                }

                if (!IsFileValid(FileUpload_Doc))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) file name may contain dots !</strong>', '" + strProjName + "'); </script>", false);
                    bReturnValue = false;
                    return bReturnValue;
                }

                int fileSize = FileUpload_Doc.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB !</strong>', '" + strProjName + "'); </script>", false);
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
            Util.LogError(ex, "Registration");
        }

        return bReturnValue;
    }

    //// Method to Check File MimeType
    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf" };
        string[] allowedExtension = { ".pdf" };
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        imageTypes.AddRange(allowedImageTyps);
        imageExtension.AddRange(allowedExtension);
        string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
        int count = FileUpload1.FileName.Count(f => f == '.');

        System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
      

        if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //// To Check Existance of PAN    
    public DataTable CheckPan(string strPAN)
    {
        DataTable dt;
        try
        {
            dt = objRegService.PanCheckValidation(strPAN, "A");
            if (dt.Rows.Count > 0)
            {
                GrdUserList.DataSource = dt;
                GrdUserList.DataBind();

                ViewState["datatable"] = dt;
               
                Img_Success.Visible = false;

                dt.DefaultView.RowFilter = "INT_PARENT_ID = 0";
                DataTable dtOrg = (dt.DefaultView).ToTable();
                if (dtOrg.Rows.Count > 0)
                {
                    ViewState["parentid"] = dtOrg.Rows[0]["INT_INVESTOR_ID"].ToString();
                    int intApprovalStatus = Convert.ToInt32(dtOrg.Rows[0]["INT_APPROVAL_STATUS"]);
                    if (intApprovalStatus == 1)
                    {
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        Txt_PAN.Text = "";
                        Txt_User_Id.Text = "";
                        ModalPopupExtender1.Hide();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "validate", "<script>jAlert('<strong>Registration for unit under this PAN has been applied earlier and currently is in pending stage. Please wait till approval or contact to the administrator !</strong>');</script>");
                    }
                }

                Lbl_PAN.Text = Txt_PAN.Text;
                ViewState["PAN"] = Txt_PAN.Text;
            }
            else
            {
                ClearAutoFillControl();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "validate", "<script>jAlert('<strong>This PAN is available for registration and It will treated as your main unit.<br/>Please confirm this is your company PAN number !</strong>');</script>");

                string strRandNo = MakeRandom(3);

                Txt_User_Id.Text = Txt_PAN.Text.ToUpper() + "_" + strRandNo + "000";
                Txt_User_Id.ReadOnly = true;

                ViewState["parentid"] = "0";

                
                Img_Success.Visible = true;

                ModalPopupExtender1.Hide();
            }

            ViewState["checkedPan"] = Txt_PAN.Text;
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        return dt;
    }

    //// Clear auto-filled controls filled during PAN validation.
    private void ClearAutoFillControl()
    {
        DrpDwn_Salutation.SelectedIndex = 0;

        Txt_Address.Text = "";
        Txt_Mobile_No.Text = "";
        Txt_Email_Id.Text = "";
    }

    /// <summary>
    /// For Dynamic Scroling notifiaction 
    /// </summary>
    void DynamicScrolingText()
    {
        int? INDUSTRIAL_PAGE = null;
        string strOut = "";

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_AddNotice";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Action", "S");

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataReader sqlReader = cmd.ExecuteReader();

            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    strOut = sqlReader["VCH_NOTIFICATION"].ToString();
                    INDUSTRIAL_PAGE = Convert.ToInt32(sqlReader["INT_INDUSTRIAL_PAGE"].ToString());
                }

                if (INDUSTRIAL_PAGE == 0)
                {
                    string strHtmlText = "<marquee onmouseover='this.stop()' onmouseout='this.start()'> " + strOut + "</marquee>";

                    divScrollingText.Visible = true;
                    divScrollingText.InnerHtml = strHtmlText.ToString();
                }

            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    

    #endregion


    #region ButtonClick

    //// Registration
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        InvestorInfo objInvEntity = new InvestorInfo();
        try
        {
            bool bRetVal = true;

            /*---------------------------------------------------------------*/
            /// Validate Captcha Section
            /*---------------------------------------------------------------*/
            #region ValidateCaptcha

            #endregion

            /*---------------------------------------------------------------*/
            ///Server Side Validation
            /*---------------------------------------------------------------*/
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
            /*---------------------------------------------------------------*/
            ///Reqular expression validation for valid PAN format
            /*---------------------------------------------------------------*/
            Regex rePan = new Regex(@"^[A-Z]{5}\d{4}[A-Z]{1}$");
            if (!rePan.IsMatch(Txt_PAN.Text.Trim()))
            {
                Txt_PAN.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid PAN card number.</strong>');", true);
                return;
            }
            if (Txt_Pan_Holder_Name.Text.Trim() == "")
            {
                Txt_Pan_Holder_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter PAN holder name.</strong>');", true);
                return;
            }
            if(Txt_Dob.Text.Trim() == "")
            {
                Txt_Dob.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter PAN holder Date Of Birth.</strong>');", true);
                return;
            }


            /*---------------------------------------------------------------*/
            if (Txt_User_Id.Text.Trim() == "")
            {
                Btn_PAN_Validate.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on Check Availability button to validate PAN.</strong>');", true);
                return;
            }
            if (DrpDwn_Salutation.SelectedIndex == 0)
            {
                DrpDwn_Salutation.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select prefix for name of applicant.</strong>');", true);
                return;
            }
            if (Txt_First_Name.Text.Trim() == "")
            {
                Txt_First_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter first name .</strong>');", true);
                return;
            }
            if (Txt_Address.Text.Trim() == "")
            {
                Txt_Address.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter address.</strong>');", true);
                return;
            }
            if (Txt_Mobile_No.Text.Trim() == "")
            {
                Txt_Mobile_No.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter mobile number.</strong>');", true);
                return;
            }
            if (Txt_Mobile_No.Text.Trim().Substring(0, 1) == "0")
            {
                Txt_Mobile_No.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Mobile number should not be start with zero.</strong>');", true);
                return;
            }
            if (Txt_Mobile_No.Text.Trim().Length != 10)
            {
                Txt_Mobile_No.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Mobile number should be 10 digits.</strong>');", true);
                return;
            }
            /*---------------------------------------------------------------*/
            if (Txt_Email_Id.Text.Trim() == "")
            {
                Txt_Email_Id.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter email id.</strong>');", true);
                return;
            }
            /*---------------------------------------------------------------*/
            ///Reqular expression validation for email format
            /*---------------------------------------------------------------*/
            Regex regEmail = new Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");
            if (!regEmail.IsMatch(Txt_Email_Id.Text.Trim()))
            {
                Txt_Email_Id.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid email address.</strong>');", true);
                return;
            }
            /*---------------------------------------------------------------*/
            if (Txt_Unit_Name.Text.Trim() == "")
            {
                Txt_Unit_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter unit name.</strong>');", true);
                return;
            }
            if (DrpDwn_Invest_Level.SelectedIndex == 0)
            {
                DrpDwn_Invest_Level.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select investment level.</strong>');", true);
                return;
            }
            if (Txt_Site_Loc.Text.Trim() == "")
            {
                Txt_Site_Loc.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location.</strong>');", true);
                return;
            }

            /*---------------------------------------------------------------*/
            ///Validation for EIN/IEM depending on Investment Level (Large/MSME)
            /*---------------------------------------------------------------*/
            if (DrpDwn_License_Type.SelectedIndex == 0)
            {
                DrpDwn_License_Type.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select license type !</strong>');", true);
                return;
            }

            /*---------------------------------------------------------------*/
            ///File Upload Section
            /*---------------------------------------------------------------*/
            string strFileName = null;
            if (DrpDwn_Invest_Level.SelectedValue == "1") //// Large Industry Choosen
            {
                if (Txt_EIN_IEM.Text.Trim() == "")
                {
                    Txt_EIN_IEM.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter IEM/Production Certificate number !</strong>');", true);
                    return;
                }

                if (FileUpload_Licence_Doc.HasFile)
                {
                    string strFileExt = Path.GetExtension(FileUpload_Licence_Doc.FileName);
                    strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LICNDOC" + strFileExt;
                    string strFoldername = "RegdDoc";
                    bRetVal = UploadDocument(FileUpload_Licence_Doc, strFileName, strFoldername);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please upload document in support of IEM/Production Certificate !</strong>');", true);
                    bRetVal = false;
                    return;
                }
            }
            else if (DrpDwn_Invest_Level.SelectedValue == "2") //// MSME Industry Choosen
            {
                if (Txt_EIN_IEM.Text.Trim() == "")
                {
                    Txt_EIN_IEM.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter EIN/PC/Udyog Aadhaar/Udyam Registration number !</strong>');", true);
                    return;
                }

                if (FileUpload_Licence_Doc.HasFile)
                {
                    string strFileExt = Path.GetExtension(FileUpload_Licence_Doc.FileName);
                    strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LICNDOC" + strFileExt;
                    string strFoldername = "RegdDoc";
                    bRetVal = UploadDocument(FileUpload_Licence_Doc, strFileName, strFoldername);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please upload document in support of EIN/PC/Udyog Aadhaar/Udyam Registration !</strong>');", true);
                    bRetVal = false;
                    return;
                }
            }

            /*---------------------------------------------------------------*/

            if (DrpDwn_District.SelectedIndex == 0)
            {
                DrpDwn_District.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select district !</strong>');", true);
                return;
            }
            if (DrpDwn_Block.SelectedIndex == 0)
            {
                DrpDwn_Block.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select block !</strong>');", true);
                return;
            }
            if (DrpDwn_Sector.SelectedIndex == 0)
            {
                DrpDwn_Sector.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select sector !</strong>');", true);
                return;
            }
            if (DrpDwn_Sub_Sector.SelectedIndex == 0)
            {
                DrpDwn_Sub_Sector.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select sub sector !</strong>');", true);
                return;
            }
            if (Txt_GSTIN.Text.Trim() != "")
            {
                if (Txt_GSTIN.Text.Trim().Length < 15)
                {
                    Txt_GSTIN.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>GST identification no. can not be less then 15 characters.</strong>');", true);
                    return;
                }
            }

            

            if (Txt_Pwd.Text.Trim() == "")
            {
                Txt_Pwd.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter password.</strong>');", true);
                return;
            }
            /*---------------------------------------------------------------*/
            ///Reqular expression validation for password format.
            ///Password Format:- 8-14 Characters, Must contain atleast one uppercase letter,one lowercase letter,one number and one special character
            /*---------------------------------------------------------------*/
            Regex regPwd = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,14}$");
            if (!regPwd.IsMatch(Txt_Pwd.Text.Trim()))
            {
                Txt_Pwd.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Password must contain atleast one uppercase letter,one lowercase letter,one number and one special character and length must be between 8-14 characters.</strong>');", true);
                return;
            }
            /*---------------------------------------------------------------*/
            if (Txt_Conf_Pwd.Text.Trim() == "")
            {
                Txt_Conf_Pwd.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please confirm your password.</strong>');", true);
                return;
            }
            if (Txt_Pwd.Text.Trim() != Txt_Conf_Pwd.Text.Trim())
            {
                Txt_Conf_Pwd.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Confirm password does not match with password.</strong>');", true);
                return;
            }
            if (DrpDwn_Question.SelectedIndex == 0)
            {
                DrpDwn_Question.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select security question.</strong>');", true);
                return;
            }
            if (Txt_Answer.Text.Trim() == "")
            {
                Txt_Answer.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter answer.</strong>');", true);
                return;
            }

            #endregion

            /*---------------------------------------------------------------*/
            ///Check whether the PAN, which was validated is the same during insertion
            /*---------------------------------------------------------------*/
            if (ViewState["checkedPan"] != null)
            {
                if (Convert.ToString(ViewState["checkedPan"]) != Txt_PAN.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on Check Availability button to validate PAN !</strong>');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on Check Availability button to validate PAN !</strong>');", true);
                return;
            }


            /*---------------------------------------------------------------*/
            /// Get the Parent Id
            /// If the user registering first time then parent id=0
            /// Else parent id=Id of parent unit
            /*---------------------------------------------------------------*/
            int intParentId = 0;
            int intApprovalStatus = 0;
            int intStatus = 0;

            if (ViewState["parentid"] != null)
            {
                intParentId = Convert.ToInt32(ViewState["parentid"]);
            }

            /*---------------------------------------------------------------*/
            ///Data Insertion Process Begins
            /*---------------------------------------------------------------*/
            if (bRetVal)
            {
                objInvEntity.VCH_CONTACT_FIRSTNAME = Txt_First_Name.Text.Trim();
                objInvEntity.VCH_EMAIL = Txt_Email_Id.Text.Trim() == "" ? null : Txt_Email_Id.Text.Trim();
                objInvEntity.VCH_PANNO = Txt_PAN.Text.Trim();
                objInvEntity.INT_APPROVALSTATUS = intApprovalStatus;
                objInvEntity.VCH_EIN_IEM = Txt_EIN_IEM.Text.Trim() == "" ? null : Txt_EIN_IEM.Text.Trim();
                objInvEntity.INT_STATUS = intStatus;
                objInvEntity.VCH_INV_NAME = Txt_Unit_Name.Text.Trim();
                objInvEntity.VCH_INV_PASSWORD = CommonHelperCls.GenerateHash(Txt_Conf_Pwd.Text.Trim());
                objInvEntity.VCH_INV_USERID = Txt_User_Id.Text.Trim();
                objInvEntity.VCH_OFF_MOBILE = Txt_Mobile_No.Text.Trim();
                objInvEntity.VCH_SEC_ANSWER = Txt_Answer.Text.Trim();
                objInvEntity.INT_PARENTID = intParentId;
                objInvEntity.VCH_ADDRESS = Txt_Address.Text.Trim();
                objInvEntity.INT_SALUTATION = Convert.ToInt32(DrpDwn_Salutation.SelectedValue);
                objInvEntity.INT_SEC_QUES = Convert.ToInt32(DrpDwn_Question.SelectedValue);
                objInvEntity.INT_SMS_STATUS = Convert.ToInt32(chkBoxSMS.Checked);
                objInvEntity.INT_EMAIL_STATUS = Convert.ToInt32(chkBoxEmail.Checked);
                objInvEntity.INT_INDUSTRY_GROUP_ID = 0;
                objInvEntity.VCH_GSTIN = Txt_GSTIN.Text.ToString().Trim() == "" ? null : Txt_GSTIN.Text.ToString().Trim();
                objInvEntity.INT_DISTRICT = Convert.ToInt32(DrpDwn_District.SelectedValue);
                objInvEntity.INT_BLOCK = Convert.ToInt32(DrpDwn_Block.SelectedValue);
                objInvEntity.INT_CATEGORY = Convert.ToInt32(DrpDwn_Invest_Level.SelectedValue);
                objInvEntity.INT_SECTOR = Convert.ToInt32(DrpDwn_Sector.SelectedValue);
                objInvEntity.INT_SUBSECTOR = Convert.ToInt32(DrpDwn_Sub_Sector.SelectedValue);
                objInvEntity.DEC_INVESTAMOUNT = 0;
                objInvEntity.VCH_SITELOCATION = (Txt_Site_Loc.Text.Trim().ToString());
                objInvEntity.VCH_LICENCE_NO_TYPE = DrpDwn_License_Type.SelectedValue == "0" ? null : DrpDwn_License_Type.SelectedValue;
                objInvEntity.VCH_LICENCE_DOC = strFileName;
                objInvEntity.intUserLevel = 1; ///// This specify first level user                
                objInvEntity.StrVCH_PROP_NAME = Txt_Proprietorship_Name.Text;
                objInvEntity.INT_INDUSTRY_TYPE = 1;   // add by anil sahoo for Industry type
                objInvEntity.strPanHolderName = Txt_Pan_Holder_Name.Text;  // add by anil
                objInvEntity.strDOB = Txt_Dob.Text;  // add by anil


                /*---------------------------------------------------------------*/
                ///DML Operation
                /*---------------------------------------------------------------*/
                string strResult = objRegService.InvestorRegistrationBAL(objInvEntity, "I");
                if (strResult == "1") ///// Duplicate EIN/IEM Number Found
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>The EIN/IEM/Udyog Aadhaar/Production Certificate number provided by you is already exists !</strong>');", true);
                }
                else if (strResult == "2") //// Success
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Data Saved Successfully !</strong>');", true);

                    Session["InvRegUser"] = objInvEntity;
                    Session["MobileNo"] = Txt_Mobile_No.Text.Trim();
                    Session["EmailId"] = Txt_Email_Id.Text.Trim();
                    Session["UnitName"] = Txt_Unit_Name.Text.Trim();
                    string result = GenerateOTP(Txt_Unit_Name.Text, Txt_Mobile_No.Text, Txt_User_Id.Text.Trim(), Txt_Email_Id.Text.Trim());
                    Session["otp"] = result.Split('|')[1].ToString();
                    Session["UserIdDD"] = Txt_User_Id.Text.ToString().Trim();  
                    Session["UserName"] = Txt_First_Name.Text.Trim();

                    /*---------------------------------------------------------------*/
                    ///After successful registration, redirect to OTP validation page.
                    /*---------------------------------------------------------------*/
                    Response.Redirect("otpValidation.aspx", false);
                }
                else if (strResult == "3") //// Error
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Internal server error. Please try again after sometime !</strong>');", true);
                }
                else if (strResult == "4") //// Duplicate Unit Name Found
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The unit name provided by you is already exists, Please try with a different unit name !</strong>');", true);
                }
               
                else if (strResult == "6") //// Duplicate User Id Found
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid user id,Please Validate and Check Availibilty of PAN again !</strong>');", true);
                }
                else if (strResult == "7") ///// Duplicate CIN Number Found
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>The CIN number provided by you is already exists !</strong>');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Oops, an error occurred during file upload. Please try again.</strong>');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Registration");
        }
        
    }

    //// Validate PAN
    protected void Btn_PAN_Validate_Click(object sender, EventArgs e)
    {
        try
        {
            /*---------------------------------------------------------------*/
            ///Get NSDL validation enable status (ON/OFF) from web config.          
            /*---------------------------------------------------------------*/
            string strValidateNSDL = ConfigurationManager.AppSettings["ValidateNSDLPAN"].ToString();

            ViewState["PANTYPE"] = string.Empty;

            if (strValidateNSDL == "OFF") ////PAN will not go for NSDL validation
            {
                ///Validate PAN
                ViewState["PANTYPE"] = "INDUSTRY";
                CheckPan(Txt_PAN.Text.ToString().Trim());
            }
            else if (strValidateNSDL == "ON") ////PAN will go for NSDL validation
            {
                Txt_Unit_Name.Enabled = true;
                Txt_First_Name.Text = "";
                Txt_Unit_Name.Text = "";
                Txt_Proprietorship_Name.Text = "";

                /*---------------------------------------------------------------*/
                ///Common class used for PAN validation from NSDL portal.
                /*---------------------------------------------------------------*/
                PANValidationNSDL objPan = new PANValidationNSDL();
                // string strVal = objPan.GetPANStatusFromNSDL(Txt_PAN.Text);

                 string strPanResponse = objPan.GetPANStatusFromNSDL(Txt_PAN.Text, Txt_Pan_Holder_Name.Text, Txt_Dob.Text);




                var JsonObject = JObject.Parse(strPanResponse);
                string response_Code = (string)JsonObject["response_Code"];

                JArray outputData = (JArray)JsonObject["outputData"];
                string strDob = "";
                string strName = "";
                string strPan_Status = "";
                string strPan = "";
                string strSeeding_status = "";

                if (response_Code == "1")
                {
                    if (outputData.Count > 0)
                    {
                        foreach (JObject item in outputData)
                        {
                            strDob = (string)item["dob"];
                            strName = (string)item["name"];
                            strPan_Status = (string)item["pan_status"];
                            strPan = (string)item["pan"];
                            strSeeding_status = (string)item["seeding_status"];

                        }
                    }

                    if (strPan_Status == "D")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>PANs deleted !</strong>');", true);
                    }
                    else if (strPan_Status == "EC")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Acquisition in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "EA")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Amalgamation in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "ED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Death in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "EI")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Dissolution in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "EL")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Liquidated in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "EM")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Merger in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "EP")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Partition in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "ES")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Split in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "EU")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Existing and Valid but event marked as Under Liquidation in ITD database !</strong>');", true);
                    }
                    else if (strPan_Status == "X")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Marked as Deactivated !</strong>');", true);
                    }
                    else if (strPan_Status == "F")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Marked as Fake !</strong>');", true);
                    }
                    else if (strPan_Status == "N")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Not present in Income Tax Department (ITD) database/Invalid PAN !</strong>');", true);
                    }
                    else if (strPan_Status == "E")
                    {
                        if (strName == "N")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Name in your card dose not match please reenter correct name !</strong>');", true);
                        }
                        else if (strName == "Y")
                        {
                            Txt_Unit_Name.Text = Txt_Pan_Holder_Name.Text;
                            Txt_Unit_Name.Enabled = false;
                        }

                        /*---------------------------------------------------------------*/
                        ///Validate PAN from GOSWIFT.
                        /*---------------------------------------------------------------*/
                        CheckPan(Txt_PAN.Text.ToString().Trim());
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something is wrong !</strong>');", true);
                }

                /*---------------------------------------------------------------*/
                ///Write the response log, got from NSDL portal.
                /*---------------------------------------------------------------*/
                Util.LogRequestResponse("Registration", "GetPANStatusFromNSDL", "[REQUEST_PAN]:- " + Txt_PAN.Text + " - [RESPONSE_FROM_NSDL]:- " + strPanResponse);

                //Util.LogRequestResponse("Registration", "GetPANStatusFromNSDL", "[REQUEST_PAN]:- " + Txt_PAN.Text + " - [RESPONSE_FROM_NSDL]:- " + strVal);

                //if (strVal == "2")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>System Error !</strong>');", true);
                //}
                //else if (strVal == "3")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Authentication Failure !</strong>');", true);
                //    return;
                //}
                //else if (strVal == "4")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User not authorized !</strong>');", true);
                //}
                //else if (strVal == "5")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>No PANs Entered !</strong>');", true);
                //}
                //else if (strVal == "6")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User validity has expired !</strong>');", true);
                //}
                //else if (strVal == "7")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Number of PANs exceeds the limit (5) !</strong>');", true);
                //}
                //else if (strVal == "8")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Not enough balance !</strong>');", true);
                //}
                //else if (strVal == "9")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Not an HTTPs request !</strong>');", true);
                //}
                //else if (strVal == "10")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>POST method not used !</strong>');", true);
                //}
                //else if (strVal == "11")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>SLAB_CHANGE_RUNNING !</strong>');", true);
                //}
                //else if (strVal == "12")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid version number entered !</strong>');", true);
                //}
                //else if (strVal == "15")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User ID not sent in Input request and only PAN sent !</strong>');", true);
                //}
                //else if (strVal == "16")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Certificate Revocation List issued by the Certifying Authorities is expired !</strong>');", true);
                //}
                //else if (strVal == "17")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User id Deactivated !</strong>');", true);
                //}
                //else if (strVal == "18")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User ID not present in database or Wrong certificate used !</strong>');", true);
                //}
                //else if (strVal == "19")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Signature sent in input request is blank !</strong>');", true);
                //}
                //else if (strVal == "20")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User ID and PAN not sent in Input request !</strong>');", true);
                //}
                //else if (strVal == "21")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Only ^ sent in Input request !</strong>');", true);
                //}
                //else
                //{
                //    string[] strValueArr = strVal.Split('^');

                //    if (strValueArr[2] == "E")
                //    {
                //        if (strValueArr[4].Trim() == "")
                //        {
                //            ///For Industries
                //            Txt_Unit_Name.Text = strValueArr[3];
                //            Txt_Unit_Name.Enabled = false;
                //            proprietor.Visible = false;
                //            ViewState["PANTYPE"] = "INDUSTRY";
                //        }
                //        else
                //        {
                //            ///For Individual
                //            proprietor.Visible = true;
                //            ViewState["PANTYPE"] = "INDIVISUAL";
                //            if (strValueArr[5].Trim() != "")
                //            {
                //                Txt_Proprietorship_Name.Text = strValueArr[4] + " " + strValueArr[5] + " " + strValueArr[3];
                //            }
                //            else
                //            {
                //                Txt_Proprietorship_Name.Text = strValueArr[4] + " " + strValueArr[3];
                //            }
                //        }

                //        /*---------------------------------------------------------------*/
                //        ///Validate PAN from GOSWIFT.
                //        /*---------------------------------------------------------------*/
                //        CheckPan(Txt_PAN.Text.ToString().Trim());
                //    }
                //    else if (strValueArr[2] == "F")
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Fake PAN No !</strong>');", true);
                //    }
                //    else if (strValueArr[2] == "N")
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid PAN !</strong>');", true);
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>No Response !</strong>');", true);
                //    }
                //}


            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Registration");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Sorry, the PAN validation server is currently unavailable. Please try again after some time.</strong>');", true);
        }
    }

    //// Reset Fields
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Txt_Address.Text = "";
        Txt_Answer.Text = "";
        Txt_Captcha.Text = "";
        Txt_Conf_Pwd.Text = "";
        Txt_EIN_IEM.Text = "";
        Txt_Email_Id.Text = "";
        Txt_First_Name.Text = "";
        Txt_GSTIN.Text = "";
        Txt_Mobile_No.Text = "";
        Txt_PAN.Text = "";
        Txt_Pwd.Text = "";
        Txt_Site_Loc.Text = "";
        Txt_Unit_Name.Text = "";
        Txt_User_Id.Text = "";
        Txt_Proprietorship_Name.Text = "";
        Txt_Dob.Text = "";
        Txt_Pan_Holder_Name.Text = "";


        DrpDwn_Block.SelectedIndex = 0;
        DrpDwn_District.SelectedIndex = 0;
        DrpDwn_Invest_Level.SelectedIndex = 0;
        DrpDwn_License_Type.SelectedIndex = 0;
        DrpDwn_Question.SelectedIndex = 0;
        DrpDwn_Salutation.SelectedIndex = 0;
        DrpDwn_Sector.SelectedIndex = 0;
        DrpDwn_Sub_Sector.SelectedIndex = 0;
        DrpDwn_Unit_Prefix.SelectedIndex = 0;
        chkBoxEmail.Checked = false;
        chkBoxSMS.Checked = false;        
        Session["InvRegOTP"] = "";
       
        proprietor.Visible = false;
    }

    //// To confirm, Creation of child unit.(Yes)
    protected void Btn_Yes_Click(object sender, EventArgs e)
    {
        try
        {
            
            DataTable dt = (DataTable)ViewState["datatable"];

            int intPanCount = Convert.ToInt32(dt.Rows.Count);
            string strPrefixZero = "";

            /*----------------------------------------------------------------*/
            ///Check any unit have rejected or not
            /*----------------------------------------------------------------*/
            string strPAN = Convert.ToString(ViewState["PAN"]);
          
           DataTable dtLog = objRegService.PanCheckValidation(strPAN, "C");

            int intLogCount = Convert.ToInt32(dtLog.Rows[0]["LOG_COUNT"]);

            /*----------------------------------------------------------------*/
            /// Add the log count so that user id sequency will not break.
            /*----------------------------------------------------------------*/
            intPanCount = intPanCount + intLogCount;

            /*----------------------------------------------------------------*/
            if (intPanCount >= 1 && intPanCount < 10)
            {
                strPrefixZero = "00";
            }
            else if (intPanCount >= 10 && intPanCount < 100)
            {
                strPrefixZero = "0";
            }
            else if (intPanCount >= 100 && intPanCount < 1000)
            {
                strPrefixZero = "";
            }

            string strRandNo = MakeRandom(3);

            Txt_User_Id.Text = Txt_PAN.Text.ToUpper() + "_" + strRandNo + strPrefixZero + intPanCount;
            Txt_User_Id.ReadOnly = true;

            /*----------------------------------------------------------------*/
            ///Auto fill existing details
            /*----------------------------------------------------------------*/
            if (ViewState["PANTYPE"].ToString() == "INDIVISUAL")
            {
                Txt_Proprietorship_Name.Text = Convert.ToString(dt.Rows[0]["VCH_PROP_NAME"]);
                proprietor.Visible = true;
            }
            else
            {
                Txt_Proprietorship_Name.Text = "";
                proprietor.Visible = false;
            }

            DrpDwn_Salutation.SelectedValue = Convert.ToString(dt.Rows[0]["INT_SALUTATION"]);
            Txt_First_Name.Text = Convert.ToString(dt.Rows[0]["VCH_CONTACT_FIRSTNAME"]);
            
            Txt_Address.Text = Convert.ToString(dt.Rows[0]["VCH_ADDRESS"]);
            Txt_Mobile_No.Text = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE_ACTUAL"]);
            Txt_Email_Id.Text = Convert.ToString(dt.Rows[0]["VCH_EMAIL_ACTUAL"]);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Registration");
        }
    }

    //// To confirm, Creation of child unit.(No)
    protected void Btn_No_Click(object sender, EventArgs e)
    {
        try
        {
            Txt_PAN.Text = "";
            Txt_User_Id.Text = "";
            ModalPopupExtender1.Hide();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Registration");
        }
    }

    #endregion

    #region CommonMethods

    /// <summary>
    /// This method is used to generate 4 digits OTP and send the OTP to investor through the SMS and Email
    /// </summary>
    /// <param name="strUnitName"></param>
    /// <param name="strMobileNo"></param>
    /// <param name="strAction"></param>
    /// <param name="strUserId"></param>
    /// <param name="strEmailId"></param>
    /// <returns></returns>
    public static string GenerateOTP(string strUnitName, string strMobileNo, string strUserId, string strEmailId)
    {
        CommonHelperCls objComm = new CommonHelperCls();
        InvestorDetails objInvDet = new InvestorDetails();
        InvestorRegistration objInvService = new InvestorRegistration();

        try
        {
            string[] InvToEmail = new string[1];
            InvToEmail[0] = strEmailId;

            /*----------------------------------------------------------------*/
            ///Generate OTP and Send to the investor by Email and SMS
            /*----------------------------------------------------------------*/
            string result = objComm.AddOTP(strUnitName, strMobileNo, "I", strUserId);
            string[] arrResult = result.Split('|');
            if (arrResult[0] == "1" && arrResult[1] != "")
            {
                objInvDet.strAction = "M";
                DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
                if (dtcontent.Rows.Count > 0)
                {
                    /*----------------------------------------------------------------*/
                    ///Prepare SMS and Mail Content
                    /*----------------------------------------------------------------*/
                    string strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                    string strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[OTPCODE]", arrResult[1].ToString());
                    strSMSContent = strSMSContent.Replace("[UNITNAME]", strUnitName.ToString());
                    string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                    string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();

                    /*----------------------------------------------------------------*/
                    ///Send Mail
                    /*----------------------------------------------------------------*/
                    bool mailStatus = objComm.sendMail(strSubject, strSMSContent, InvToEmail, true);

                    /*----------------------------------------------------------------*/
                    ///Send SMS
                    /*----------------------------------------------------------------*/
                  
                    bool smsStatus = objComm.SendSmsWithTemplate(strMobileNo, strSMSContent, strTemplateId, strMsgType);

                    /*----------------------------------------------------------------*/
                    ///Update SMS and Email Status in Transaction Table
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
      
    }

    /// <summary>
    /// This method used to generate random number
    /// </summary>
    /// <param name="intLength"></param>
    /// <returns></returns>   
    public string MakeRandom(int intLength)
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

    #endregion

    #region SelectedIndexChanged

    /// <summary>
    /// District DropDownList SelectedIndexChanged (To Fill Blocks Under District)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>    
    protected void DrpDwn_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            action = "FB";
            DataTable dtblock = objRegService.FillBlock(action, Convert.ToInt32(DrpDwn_District.SelectedValue));
            DrpDwn_Block.DataSource = dtblock;
            DrpDwn_Block.DataTextField = "vchBlockName";
            DrpDwn_Block.DataValueField = "intBlockId";
            DrpDwn_Block.DataBind();
            DrpDwn_Block.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Registration");
        }
    }

    /// <summary>
    /// Sector DropDownList SelectedIndexChanged (To Fill Sub-Sector Under Sector)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>    
    protected void DrpDwn_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            action = "FSS";
            DataTable dtSubsector = objRegService.FillBlock(action, Convert.ToInt32(DrpDwn_Sector.SelectedValue));
            DrpDwn_Sub_Sector.DataSource = dtSubsector;
            DrpDwn_Sub_Sector.DataTextField = "vchSubSectorName";
            DrpDwn_Sub_Sector.DataValueField = "intSubSectorId";
            DrpDwn_Sub_Sector.DataBind();
            DrpDwn_Sub_Sector.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Registration");
        }
    }

    /// <summary>
    ///  Investment Level DropDownList SelectedIndexChanged
    ///  For Large(1) industry, IEM/PC
    ///  For MSME(2) industry, EIN/PC/Udyog Aadhaar/Udyam Registration
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DrpDwn_Invest_Level_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          

            Txt_EIN_IEM.Text = "";
            int intIndustryType = Convert.ToInt32(DrpDwn_Invest_Level.SelectedValue);

            ArrayList list = new ArrayList();
            list.Add(new ListItem("-Select-", "0"));

            if (intIndustryType == 1) //// Large Industry
            {
                list.Add(new ListItem("IEM"));
                list.Add(new ListItem("Production Certificate"));
            }
            else if (intIndustryType == 2) //// MSME Industry
            {
                list.Add(new ListItem("EIN"));
                list.Add(new ListItem("Production Certificate"));
                list.Add(new ListItem("Udyog Aadhaar"));
                list.Add(new ListItem("Udyam Registration"));

              
            }
            else
            {
                list.Add(new ListItem("EIN"));
                list.Add(new ListItem("IEM"));
                list.Add(new ListItem("Production Certificate"));
                list.Add(new ListItem("Udyog Aadhaar"));
                list.Add(new ListItem("Udyam Registration"));
            }

            DrpDwn_License_Type.DataSource = list;
            DrpDwn_License_Type.DataTextField = "Text";
            DrpDwn_License_Type.DataValueField = "Value";
            DrpDwn_License_Type.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Registration");
        }
        
    }

    #endregion    
}