using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.ServiceModel;
using System.Data;
using DWHServiceReference;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

public partial class EditInvestorProfile : SessionCheck
{
    #region Declaration And Variables

    /// Get Project Name from Web.Config File 
    readonly string StrProjName = ConfigurationManager.AppSettings["ProjectName"].ToString();

    /// Get keys for CIN from Web.Config File 
    readonly string StrValidateCinLlpin = ConfigurationManager.AppSettings["MCAValidation"];
    readonly string StrCinLlpinTokenUrl = ConfigurationManager.AppSettings["CinLlpinTokenUrl"];
    readonly string StrCinLlpinValidateUrl = ConfigurationManager.AppSettings["CinLlpinValidateUrl"];
    readonly string StrCinTokenUserId = ConfigurationManager.AppSettings["CinTokenUserId"];
    readonly string StrCinTokenPassword = ConfigurationManager.AppSettings["CinTokenPassword"];

    string StrAction = "";

    InvestorRegistration objRegService = new InvestorRegistration();
    #endregion

    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/LogOut.aspx", true);
        }

        /*------------------------------------------------------------------------*/
       

        if (!IsPostBack)
        {
            string StrInvestorId = Convert.ToString(Session["InvestorId"]);
            if (StrInvestorId != "")
            {
                CommonHelperCls ob = new CommonHelperCls();

                try
                {
                    Img_Success.Visible = false;
                    ob.BindDropDown(DrpDwn_Salutation, "ID", "VCH_SALUTATION", "Select ID, VCH_SALUTATION from M_SALUTATION");
                    FillEntityType();
                    FillRegdCountry();
                    FillSLCountry();
                    FillInvestorInfo(StrInvestorId);
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "ProfileUpdate");
                }
            }
        }
    }

    #region ButtonClickEvents

    /// <summary>
    /// Button Update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        InvestorDetails objInvDet = new InvestorDetails();
        InvestorBusinessLayer objService = new InvestorBusinessLayer();

        try
        {
            /*---------------------------------------------------------------------------------------------------------------*/
            /// Both Industrial and Non-Industrial user can update their own profile details.
            /// For Non-Industrial user (means the value Session["IndustryType"] is 2), the profile will be changed only at GOSWIFT end.
            /// For Industrial user (means the value Session["IndustryType"] is 1), the profile will be changed both at GOSWIFT and DWH ends. 
            /// For CIN or LLPIN number validation first check WEB config key ON 
            /*---------------------------------------------------------------------------------------------------------------*/

            /*-----------------------------------------------------------------------------------------------------*/
            /// Server side validation.
            /*-----------------------------------------------------------------------------------------------------*/

            #region ServerSideValidations

            int intEntityTypeValue = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);

            if (StrValidateCinLlpin == "ON")
            {
                if(intEntityTypeValue == 1 || intEntityTypeValue == 2)
                {
                    if (Hid_Cin_Llpin.Value == "")
                    {
                        Img_Success.Visible = false;
                        BtnValidateCinLlpin.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate CIN/LLPIN !</strong>');", true);
                        return;
                    }

                    if (Hid_Cin_Llpin.Value == "No Data")
                    {
                        Img_Success.Visible = false;
                        BtnValidateCinLlpin.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate CIN/LLPIN !</strong>');", true);
                        return;
                    }
                }
                

                /*---------------------------------------------------------------*/
                ///Check whether the CIN or LLPIN, which was validated is the same during insertion
                /*---------------------------------------------------------------*/

                string StrCinLlpinNumber = Convert.ToString(ViewState["CinNumber"]);

                if (StrCinLlpinNumber != null)
                {
                    if (intEntityTypeValue == 1 && StrCinLlpinNumber != Txt_CIN_Number.Text)
                    {
                        Img_Success.Visible = false;
                        BtnValidateCinLlpin.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate CIN !</strong>');", true);
                        return;
                    }
                    else if (intEntityTypeValue == 2 && StrCinLlpinNumber != Txt_LLPIN_Number.Text)
                    {
                        Img_Success.Visible = false;
                        BtnValidateCinLlpin.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate LLPIN !</strong>');", true);
                        return;
                    }
                }
            }

            /*-----------------------------------------------------------------------------------------------------*/

            if (Txt_First_Name.Text.Trim() == "")
            {
                Txt_First_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter the name of the applicant !</strong>');", true);
                return;
            }

            if (string.IsNullOrWhiteSpace(Txt_Mobile_No.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter the mobile number !</strong>');", true);
                Txt_Mobile_No.Focus();
                return;
            }

            if (Txt_Mobile_No.Text.StartsWith("0"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The mobile number should not be start with zero !</strong>');", true);
                Txt_Mobile_No.Focus();
                return;
            }

            if (Txt_Mobile_No.Text.Length != 10)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The mobile number should be 10 digits !</strong>');", true);
                Txt_Mobile_No.Focus();
                return;
            }

            /*--------------------------------------------------------------*/
            ///Validation for Registration Address Section
            /*--------------------------------------------------------------*/

            if (string.IsNullOrWhiteSpace(Txt_Reg_Address_1.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration address-1 !</strong>');", true);
                Txt_Reg_Address_1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Txt_Reg_Address_2.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration address-2 !</strong>');", true);
                Txt_Reg_Address_2.Focus();
                return;
            }

            if (DrpDwn_Reg_Country.SelectedIndex == 0)
            {
                DrpDwn_Reg_Country.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select registration country name !</strong>');", true);
                return;
            }

            if (DrpDwn_Reg_Country.SelectedValue == "1" && DrpDwn_Reg_State.SelectedIndex == 0) ///India
            {
                DrpDwn_Reg_State.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select registration state name !</strong>');", true);
                return;
            }

            if (DrpDwn_Reg_Country.SelectedValue != "1" && string.IsNullOrEmpty(Txt_Reg_State.Text.Trim()))
            {
                Txt_Reg_State.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration state name !</strong>');", true);
                return;
            }

            if (string.IsNullOrEmpty(Txt_Reg_City.Text))
            {
                Txt_Reg_City.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration city name !</strong>');", true);
                return;
            }

            if (string.IsNullOrEmpty(Txt_Reg_PIN_Code.Text))
            {
                Txt_Reg_PIN_Code.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration address pincode !</strong>');", true);
                return;
            }

            string StrRegPinCode = Txt_Reg_PIN_Code.Text.Trim();

            if (!Regex.IsMatch(StrRegPinCode, @"^\d{6}$"))
            {
                Txt_Reg_PIN_Code.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid 6-digit PIN code for the registration address !</strong>');", true);
                return;
            }

            /*--------------------------------------------------------------*/
            ///Validation for Site Location/Postal Address Section
            /*--------------------------------------------------------------*/
            if (string.IsNullOrWhiteSpace(Txt_SL_Address_1.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter the site location address-1 !</strong>');", true);
                Txt_SL_Address_1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Txt_SL_Address_2.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter the site location address-2 !</strong>');", true);
                Txt_SL_Address_2.Focus();
                return;
            }

            if (DrpDwn_SL_Country.SelectedIndex == 0)
            {
                DrpDwn_SL_Country.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select site location country name !</strong>');", true);
                return;
            }

            if (DrpDwn_SL_Country.SelectedValue == "1" && DrpDwn_SL_State.SelectedIndex == 0) ///India
            {
                DrpDwn_SL_State.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select site location state name !</strong>');", true);
                return;
            }

            if (DrpDwn_SL_Country.SelectedValue != "1" && string.IsNullOrEmpty(Txt_SL_State.Text.Trim()))
            {
                Txt_SL_State.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location state name !</strong>');", true);
                return;
            }

            if (string.IsNullOrEmpty(Txt_SL_City.Text))
            {
                Txt_SL_City.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location city name !</strong>');", true);
                return;
            }

            if (string.IsNullOrEmpty(Txt_SL_PIN_Code.Text))
            {
                Txt_SL_PIN_Code.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location pincode !</strong>');", true);
                return;
            }

            string StrSlPinCode = Txt_SL_PIN_Code.Text.Trim();

            if (!Regex.IsMatch(StrSlPinCode, @"^\d{6}$"))
            {
                Txt_SL_PIN_Code.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid 6-digit PIN code for the site location !</strong>');", true);
                return;
            }

            /*--------------------------------------------------------------*/
            /// CIN & LLPIN validation section
            /*--------------------------------------------------------------*/
            if (DrpDwn_Entity_Type.SelectedIndex == 0)
            {
                DrpDwn_Entity_Type.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select the entity type !</strong>');", true);
                return;
            }

            if (intEntityTypeValue == 1) ///Incorporated Company
            {
                string StrCinNo = Txt_CIN_Number.Text.Trim();
                if (StrCinNo == "")
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter the CIN number !</strong>');", true);
                    return;
                }
                if (StrCinNo.Length != 21)
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The CIN number should be 21 digits !</strong>');", true);
                    return;
                }

                // Regex pattern for CIN number validation
                string StrCinRegExPattern = @"^([L|U]{1})([0-9]{5})([A-Za-z]{2})([0-9]{4})([A-Za-z]{3})([0-9]{6})$";
                if (!Regex.IsMatch(StrCinNo, StrCinRegExPattern))
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid CIN number in the format L/U-12345AB1234XYZ123456.</strong>');", true);
                    return;
                }
            }
            else if (intEntityTypeValue == 2)///Limited Liabity
            {
                string StrLlpinNo = Txt_LLPIN_Number.Text.Trim();

                if (string.IsNullOrEmpty(StrLlpinNo))
                {
                    Txt_LLPIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter the LLPIN number.</strong>');", true);
                    return;
                }
                if (StrLlpinNo.Length != 8)
                {
                    Txt_LLPIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The LLPIN number should be 8 digits.</strong>');", true);
                    return;
                }
                // Regex pattern for LLPIN number validation
                string StrLlpinRegExPattern = @"^([a-zA-Z]{2,3})-([0-9]{4})$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(StrLlpinNo, StrLlpinRegExPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid LLPIN number in the format XXX-1234.</strong>');", true);
                    return;
                }
            }


            #endregion

            /*-----------------------------------------------------------------*/
            /// If the industry type is "Industrial User" then update the details in DWH server.
            /*-----------------------------------------------------------------*/
            string StrIndustryType = Convert.ToString(Session["IndustryType"]);
            if (StrIndustryType == "1") ////Industrial User
            {
                #region Industrial User

                /*-----------------------------------------------------------------*/
                /// Service Initialization
                /*-----------------------------------------------------------------*/
                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                DWH_Model objEnt = new DWH_Model
                {
                    /*-----------------------------------------------------------------*/
                    /// Assign value to property (For Service)
                    /*-----------------------------------------------------------------*/
                    INTSALUTATION = Convert.ToInt32(DrpDwn_Salutation.SelectedValue),
                    VCHPROMOTERFNAME = Txt_First_Name.Text.Trim(),
                    VCHADDRESS = Txt_Reg_Address_1.Text.Trim(),
                    VCHCORADDRESS = Convert.ToString(Txt_SL_Address_1.Text),
                    VCHMOBILENO = Txt_Mobile_No.Text.Trim(),
                    VCHEMAILID = Txt_Email_Id.Text,
                    VCHUSERNAME = Session["UserId"].ToString(),
                    VCH_REG_ADDRESS_2 = Txt_Reg_Address_2.Text.Trim(),//Add by Debi
                    INT_REG_COUNTRY = Convert.ToInt32(DrpDwn_Reg_Country.SelectedValue)//Add by Debi
                };

                if (DrpDwn_Reg_Country.SelectedValue == "1")//Add by Debi
                {
                    objEnt.VCH_REG_STATE = DrpDwn_Reg_State.SelectedItem.Text;//Add by Debi
                }
                else
                {
                    objEnt.VCH_REG_STATE = Txt_Reg_State.Text;//Add by Debi
                }

                objEnt.VCH_REG_CITY = Txt_Reg_City.Text.Trim();//Add by Debi
                objEnt.VCH_REG_PIN = Txt_Reg_PIN_Code.Text.Trim();//Add by Debi
                objEnt.VCH_SL_ADDRESS_2 = Txt_SL_Address_2.Text.Trim();//Add by Debi
                objEnt.INT_SL_COUNTRY = Convert.ToInt32(DrpDwn_SL_Country.SelectedValue);//Add by Debi

                if (DrpDwn_SL_Country.SelectedValue == "1")//Add by Debi
                {
                    objEnt.VCH_SL_STATE = DrpDwn_SL_State.SelectedItem.Text;//Add by Debi
                }
                else
                {
                    objEnt.VCH_SL_STATE = Txt_SL_State.Text.Trim();//Add by Debi
                }

                objEnt.VCH_SL_CITY = Txt_SL_City.Text.Trim();//Add by Debi
                objEnt.VCH_SL_PIN = Txt_SL_PIN_Code.Text.Trim();//Add by Debi

                if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)//Add by Debi
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);//Add by Debi
                    objEnt.VCHCINNUMBER = Convert.ToString(Txt_CIN_Number.Text.Trim());// add by anil
                }
                else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)//Add by Debi
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);//Add by Debi
                    objEnt.VCH_LLPIN_NUMBER = Txt_LLPIN_Number.Text.Trim();//Add by Debi
                }
                else
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);// add by anil
                }

                /*----------------------------------------------------------------*/
                /// Generate Encryption Key (Security key to access Data Warehouse servce methods)
                /*-----------------------------------------------------------------*/
                string StrEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                string StrSecurityKey = objSrvRef.KeyEncryption(StrEncryptionKey);

                /*-----------------------------------------------------------------*/
                /// DML opertion through DWH service
                /*-----------------------------------------------------------------*/
                string StrReturnVal = objSrvRef.ProfileUpdate(objEnt, StrSecurityKey);
                if (StrReturnVal != "")
                {
                    if (StrReturnVal == "5") ////// Success
                    {
                        /*-----------------------------------------------------------------*/
                        /// After succcessfully update in data warehouse,update in goswift database
                        /*-----------------------------------------------------------------*/
                        objInvDet.strAction = "U";
                        objInvDet.Salutation = Convert.ToInt32(DrpDwn_Salutation.SelectedValue);
                        objInvDet.strContactFirstName = Txt_First_Name.Text.Trim();
                        objInvDet.strAddress = Txt_Reg_Address_1.Text.Trim();
                        objInvDet.strSiteAddress = Convert.ToString(Txt_SL_Address_1.Text);
                        objInvDet.MobNo = Txt_Mobile_No.Text.Trim();
                        objInvDet.strEmail = Txt_Email_Id.Text;
                        objInvDet.strUserID = Session["InvestorId"].ToString();
                        objInvDet.StrRegAddress_2 = Txt_Reg_Address_2.Text.Trim();//Add by Debi
                        objInvDet.IntRegCountry = Convert.ToInt32(DrpDwn_Reg_Country.SelectedValue);//Add by Debi

                        if (DrpDwn_Reg_Country.SelectedValue == "1")//Add by Debi
                        {
                            objInvDet.StrRegState = DrpDwn_Reg_State.SelectedItem.Text;//Add by Debi
                        }
                        else
                        {
                            objInvDet.StrRegState = Txt_Reg_State.Text;//Add by Debi
                        }

                        objInvDet.StrRegCity = Txt_Reg_City.Text.Trim();//Add by Debi
                        objInvDet.StrRegPincode = Txt_Reg_PIN_Code.Text.Trim();//Add by Debi
                        objInvDet.StrSlAddress_2 = Txt_SL_Address_2.Text.Trim();//Add by Debi
                        objInvDet.IntSlCountry = Convert.ToInt32(DrpDwn_SL_Country.SelectedValue);//Add by Debi

                        if (DrpDwn_SL_Country.SelectedValue == "1")//Add by Debi
                        {
                            objInvDet.StrSlState = DrpDwn_SL_State.SelectedItem.Text;//Add by Debi
                        }
                        else
                        {
                            objInvDet.StrSlState = Txt_SL_State.Text.Trim();//Add by Debi
                        }

                        objInvDet.StrSlCity = Txt_SL_City.Text.Trim();//Add by Debi
                        objInvDet.StrSlPincode = Txt_SL_PIN_Code.Text.Trim();//Add by Debi
                        objInvDet.intEntitytype = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);//Add by Debi

                        if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)//Add by Debi
                        {
                            objInvDet.strCINnumber = Convert.ToString(Txt_CIN_Number.Text.Trim());//Add by Debi
                        }
                        else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)//Add by Debi
                        {
                            objInvDet.StrLLPINumber = Txt_LLPIN_Number.Text.Trim();//Add by Debi
                        }

                        if (ViewState["CinLlpinData"] != null)
                        {
                            objInvDet.StrCinLlpinData = ViewState["CinLlpinData"].ToString();
                        }

                        string StrRetval = Convert.ToString(objService.InvestorData(objInvDet)); // data update in GOSWIFT DB
                        if (StrRetval == "2")
                        {
                            ViewState["CinLlpinData"] = null;
                            Img_Success.Visible = false;
                            Lbl_CompName.Text = "";
                            if (Request.QueryString["app"] != null)
                            {
                                if (Convert.ToString(Request.QueryString["app"]) == "CICG")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data updated successfully !</strong>', '" + Messages.TitleOfProject + "', function () {location.href = '" + ConfigurationManager.AppSettings["CICGReturnUrl"] + "';}); </script>", false);
                                    return;
                                }
                                else if (Convert.ToString(Request.QueryString["app"]) == "GOiPAS")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data updated successfully !</strong>', '" + Messages.TitleOfProject + "', function () {location.href = '" + ConfigurationManager.AppSettings["GOIPASReturnUrl"] + "';}); </script>", false);
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data updated successfully !</strong>', '" + StrProjName + "'); </script>", false);
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data updated successfully !</strong>', '" + StrProjName + "'); </script>", false);
                                return;
                            }
                        }
                        else if (StrRetval == "3")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Internal server error,Please try after sometime !');", true);
                        }
                        else if (StrRetval == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('CIN numebr already exists !');", true);
                        }
                        else if (StrRetval == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('LLPI numebr already exists !');", true);
                        }
                    }
                    else if (StrReturnVal == "11")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid user id !</strong>', '" + StrProjName + "'); </script>", false);
                        return;
                    }
                    else if (StrReturnVal == "20")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Unit name already exists !</strong>', '" + StrProjName + "'); </script>", false);
                        return;
                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }
    }

    /// <summary>
    /// Button Back
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        string strIndustryType = Convert.ToString(Session["IndustryType"]);
        if (strIndustryType == "1") ////Industrial User
        {
            Response.Redirect("InvesterDashboard.aspx");
        }
    }

    /// <summary>
    /// This function use for Validate CIN and LLPIN numebr from MCA side
    /// </summary>
    protected void BtnValidateCinLlpin_Click(object sender, EventArgs e)
    {
        try
        {
            if (StrValidateCinLlpin == "ON")
            {
                string strEntityType = DrpDwn_Entity_Type.SelectedValue;

                #region InputValidation

                if (strEntityType == "0")
                {
                    DrpDwn_Entity_Type.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select entity type !</strong>');", true);
                    return;
                }

                /*---------------------------------------------------------------*/
                ///If the entity type is not in Incorporated Company or Limited Liability Partnership the CIN/LLPIN validation will not work.
                /*---------------------------------------------------------------*/
                if (strEntityType != "1" && strEntityType != "2")
                {
                    DrpDwn_Entity_Type.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid entity type !</strong>');", true);
                    return;
                }

                if (strEntityType == "1" && Txt_CIN_Number.Text.Trim() == "")
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter CIN number !</strong>');", true);
                    return;
                }

                if (strEntityType == "2" && Txt_LLPIN_Number.Text.Trim() == "")
                {
                    Txt_LLPIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter LLPIN number !</strong>');", true);
                    return;
                }


                #endregion

                /*---------------------------------------------------------------*/

                Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[RequestCredentials]" + "[RequestUrl]:- " + StrCinLlpinTokenUrl + " - [TokenUserId]:- " + StrCinTokenUserId + "- [TokenPassword]:- " + StrCinTokenPassword);

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                /*---------------------------------------------------------------*/
                // Generate token to validate CIN/LLPIN.
                /*---------------------------------------------------------------*/
                var client = new RestClient(StrCinLlpinTokenUrl)
                {
                    Timeout = -1
                };

                var TokenRequest = new RestRequest("/token", Method.POST);
                TokenRequest.AddHeader("Authorization", "Basic ME4wUDBtQm1NdGVGcTNZX1c5cjdZRkxQZWswYTpwQmVWd3hzTjdJWnVfcEdKUzk1MFZoUmxjQVlh");
                TokenRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                TokenRequest.AddParameter("grant_type", "password");
                TokenRequest.AddParameter("username", StrCinTokenUserId);
                TokenRequest.AddParameter("password", StrCinTokenPassword);
                IRestResponse TokenResponse = client.Execute(TokenRequest);

                if (TokenResponse.StatusCode == HttpStatusCode.OK)
                {
                    var strResponseContent = TokenResponse.Content.ToString();

                    Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[ResponseStatusCode]:- " + TokenResponse.StatusCode + " -[ResponseContent]:- " + strResponseContent);

                    if (strResponseContent != "")
                    {
                        ///Get the access token value.
                        string strAccessToken = JsonConvert.DeserializeObject<Dictionary<string, object>>(TokenResponse.Content)["access_token"].ToString();

                        ///Log the access token value in response log.
                        Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[ResponseToken]:- " + strAccessToken);

                        string strCinServiceUrl = "";
                        string strDinServiceUrl = "";
                        if (strEntityType == "1")
                        {
                            strCinServiceUrl = StrCinLlpinValidateUrl + "/cin/service/integration/1.0.0?CIN=" + Txt_CIN_Number.Text;
                            strDinServiceUrl= StrCinLlpinValidateUrl + "/din/service/integration/1.0.0?CIN=" + Txt_CIN_Number.Text;
                        }
                        else if (strEntityType == "2")
                        {
                            strCinServiceUrl = StrCinLlpinValidateUrl + "/cin/service/integration/1.0.0?CIN=" + Txt_LLPIN_Number.Text;
                            strDinServiceUrl = StrCinLlpinValidateUrl + "/din/service/integration/1.0.0?CIN=" + Txt_LLPIN_Number.Text;
                        }

                        Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[CinRequestData]:- " + strCinServiceUrl);
                        Util.LogRequestResponse("ProfileUpdate", "GetDinStatusFromMCA", "[DinRequestData]:- " + strDinServiceUrl);

                        /*---------------------------------------------------------------*/
                        /// Call CIN/LLPIN Service API.
                        /*---------------------------------------------------------------*/
                        var CinClientRequest = new RestClient(strCinServiceUrl)
                        {
                            Timeout = -1
                        };
                        var DinClientRequest = new RestClient(strDinServiceUrl)
                        {
                            Timeout = -1
                        };
                        var Cinrequest = new RestRequest(Method.GET);
                        Cinrequest.AddHeader("Authorization", "Bearer " + strAccessToken);
                        //request2.AddHeader("Content-Type", "application/json");

                        IRestResponse CinDataresponse = CinClientRequest.Execute(Cinrequest);

                        IRestResponse DinDataresponse = DinClientRequest.Execute(Cinrequest);


                        if (CinDataresponse.StatusCode == HttpStatusCode.OK   && DinDataresponse.StatusCode == HttpStatusCode.OK)
                        {
                            string CINmessage = JsonConvert.DeserializeObject<Dictionary<string, object>>(CinDataresponse.Content)["message"].ToString();

                            string DINmessage = JsonConvert.DeserializeObject<Dictionary<string, object>>(DinDataresponse.Content)["message"].ToString();

                            Hid_Cin_Llpin.Value = CINmessage;

                            Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[CINResponseStatusCode]:- " + CinDataresponse.StatusCode + " -[ResponseContent]:- " + CinDataresponse.Content);

                            Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[DINResponseStatusCode]:- " + DinDataresponse.StatusCode + " -[ResponseContent]:- " + DinDataresponse.Content);

                            if (CINmessage.ToUpper() == "NO DATA")
                            {
                                if (strEntityType == "1")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid CIN number !</strong>', '" + StrProjName + "'); </script>", false);
                                    return;
                                }
                                else if (strEntityType == "2")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid LLPIN number !</strong>', '" + StrProjName + "'); </script>", false);
                                    return;
                                }
                            }
                            else if (CINmessage.ToUpper() == "DATA FETCHED SUCCESSFULLY" && DINmessage.ToUpper() == "DATA FETCHED SUCCESSFULLY") 
                            {

                                // var CINResponse = JsonConvert.DeserializeObject<Dictionary<string, List<CompanyData>>>(CinDataresponse.Content);

                                CinRootData root = JsonConvert.DeserializeObject<CinRootData>(CinDataresponse.Content);

                                var CompanyData = root.data[0];

                                string StrCINnumber = CompanyData.CIN;   //CINResponse["data"][0].CIN;
                                string StrCompanyName = CompanyData.companyName;  //CINResponse["data"][0].companyName;
                                string StrCompanyStatus = CompanyData.companyStatus;  //CINResponse["data"][0].companyStatus;
                                string Stremail = CompanyData.emailAddress;   //CINResponse["data"][0].emailAddress;
                                string StrfinancialAuditStatus = string.IsNullOrEmpty(CompanyData.auditStatus) ? "" : CompanyData.auditStatus;
    

                                string StrprofitLoss = string.IsNullOrEmpty(CompanyData.profitLoss) ? "" : CompanyData.profitLoss; //"";
                                string StrturnOver = string.IsNullOrEmpty(CompanyData.turnover) ? "" : CompanyData.turnover;//"";
                                string Stryear = string.IsNullOrEmpty(CompanyData.financialYear) ? "" : CompanyData.financialYear; // "";
                                string Strincorpdate = CompanyData.incorporationDate;  //CINResponse["data"][0].incorporationDate ;
                                string StrregisteredAddress = CompanyData.addressLine1;  //CINResponse["data"][0].addressLine1;
                                string StrrocCode = CompanyData.ROCName;  //CINResponse["data"][0].ROCName;

                                Util.LogRequestResponse("ProfileUpdate", "GetCINdataFromMCA", "[StrrocCode]:- " + StrrocCode);

                                // Deserialize the JSON string into a list of DirectorData objects
                                var directorDataList = JsonConvert.DeserializeObject<DirectorDataList>(DinDataresponse.Content);

                                // Create an empty list to store director details
                                var directorDetailDtos = new List<JObject>();

                                foreach (var directorData in directorDataList.Data)
                                {
                                    var directorDetails = new JObject
                                                       {
                                                            { "contactNumber", "" },
                                                            { "din", directorData.DIN },
                                                            { "name", directorData.FirstName },
                                                       
                                                       
                                                       };

                                    var dinDetail = new JObject
                                                    {
                                                         { "dinName", "" },
                                                         { "dinStatus", directorData.DINStatus },
                                                         { "dob", directorData.DOB },
                                                         { "fatherName", directorData.FatherFirstName }
                                                    };


                                    // Create a new JObject to combine directorDetails and dinDetail
                                    var directorObject = new JObject
                                                             {
                                                                 { "directorDetails", directorDetails },
                                                                 { "dinDetail", dinDetail }
                                                             };

                                    // Add the directorObject to the directorDetailDtos list
                                    directorDetailDtos.Add(directorObject);


                                }


                                // Serialize the directorDetailDtos list to JSON format
                                string directorDetailDtosJson = JsonConvert.SerializeObject(directorDetailDtos, Formatting.Indented);
                                Util.LogRequestResponse("ProfileUpdate", "GetdinStatusFromMCA", "[din new format data]:- " + directorDetailDtosJson);
                                string ReplaceData = directorDetailDtosJson.Replace('[', ' ');
                                string NewDirectorDetailDtosJson = ReplaceData.Replace(']', ' ');

                                // CIN and DIN data  json 
                                var CinDinJson = @"{
                                      ""cinDto"": {
                                          ""cin"": """ + StrCINnumber + @""",
                                          ""companyName"": """ + StrCompanyName + @""",
                                          ""companyStatus"": """ + StrCompanyStatus + @""",
                                          ""email"": """ + Stremail + @""",
                                          ""financialAuditStatus"": """ + StrfinancialAuditStatus + @""",
                                          ""financialDetails"": [
                                              {
                                                  ""profitLoss"": """ + StrprofitLoss + @""",
                                                  ""turnOver"": """ + StrturnOver + @""",
                                                  ""year"": """ + Stryear + @"""
                                              }
                                          ],
                                          ""incorpdate"": """ + Strincorpdate + @""",
                                          ""registeredAddress"": """ + StrregisteredAddress + @""",
                                          ""rocCode"": """ + StrrocCode + @"""
                                      },
                                      ""directorDetailDtos"": [
                                          [" + NewDirectorDetailDtosJson + @"]
                                      ]
                                }";


                                Util.LogRequestResponse("ProfileUpdate", "MCANewJsonData", "[New Json format data]:- " + CinDinJson);

                                if (strEntityType == "1")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>CIN number validate successfully !</strong>');", true);
                                    ViewState["CinNumber"] = Txt_CIN_Number.Text;
                                    Img_Success.Visible = true;
                                    Lbl_CompName.Text = StrCompanyName;
                                }
                                else if (strEntityType == "2")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>LLPIN number validate successfully !</strong>');", true);
                                    ViewState["CinNumber"] = Txt_LLPIN_Number.Text;
                                    Img_Success.Visible = true;
                                    Lbl_CompName.Text = StrCompanyName;
                                }
                                else
                                {
                                    ViewState["CinNumber"] = null;
                                    Img_Success.Visible = false;
                                }

                                ///Don't update the CIN on the table here. Save when the user click on Update button.
                                ///Only store the base64 converted value here and push that value during data submission.

                               




                                ViewState["CinLlpinData"] = Base64Encryption(CinDinJson);
                                Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[CinAPIDataUpdateGOSWIFTDatabase]:- " + Base64Encryption(CinDinJson));

                            }
                        }
                        else if (CinDataresponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[ResponseStatusCode]:- " + CinDataresponse.StatusCode);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalied Token ! <strong>');", true);
                        }
                        else
                        {
                            Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[ResponseStatusCode]:- " + CinDataresponse.StatusCode);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Internal server error !<strong>');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something went wrong, Please try again !<strong>');", true);
                    }
                }
                else
                {
                    Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[ResponseStatusCode]:- " + TokenResponse.StatusCode);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Internal server error, Please try again !<strong>');", true);
                }
            }
            else
            {
                Util.LogRequestResponse("ProfileUpdate", "GetCinStatusFromMCA", "[CIN and LLPIN validation is OFF in the web config file]");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }
    }

    #endregion

    #region CommonMethods

    /// <summary>
    /// Fill Investor Information
    /// </summary>
    /// <param name="UserId"></param>
    public void FillInvestorInfo(string UserId)
    {
        try
        {
            DataTable dt = CommonHelperCls.ViewInvestorDetails(Session["InvestorId"].ToString(), "V");
            if (dt.Rows.Count > 0)
            {
                string StrPan_number = dt.Rows[0]["VCH_PAN"].ToString();//Add by Debi 
                Session["Pan"] = StrPan_number;//Add by Debi
                Txt_Unit_Name.Text = dt.Rows[0]["VCH_INV_NAME"].ToString();
                DrpDwn_Salutation.SelectedValue = dt.Rows[0]["INT_SALUTATION"].ToString();
                Txt_First_Name.Text = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                Txt_Mobile_No.Text = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                Txt_Email_Id.Text = dt.Rows[0]["VCH_EMAIL"].ToString();
                Txt_Reg_Address_1.Text = dt.Rows[0]["VCH_ADDRESS"].ToString();
                Txt_SL_Address_1.Text = dt.Rows[0]["VCH_SITELOCATION"].ToString();//Add by Debi
                DrpDwn_Entity_Type.SelectedValue = dt.Rows[0]["INT_ENTITY_TYPE"].ToString(); // add by anil
                Hid_Pan_Number.Value = StrPan_number;//Add by Debi

                int intEntityType = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                if (intEntityType == 1)//Add by Debi
                {
                    Div_CIN.Visible = true;
                    Div_LLPIN.Visible = false;
                    Div_CIN_LLPIN_Btn.Visible = true;
                    Txt_CIN_Number.Text = dt.Rows[0]["VCH_CIN_NUMBER"].ToString(); // add by anil
                    BtnValidateCinLlpin.Text = "Validate CIN Number";
                }
                else if (intEntityType == 2)//Add by Debi
                {
                    Div_CIN.Visible = false;
                    Div_LLPIN.Visible = true;
                    Div_CIN_LLPIN_Btn.Visible = true;
                    Txt_LLPIN_Number.Text = dt.Rows[0]["VCH_LLPIN_NUMBER"].ToString(); // add by anil
                    BtnValidateCinLlpin.Text = "Validate LLPIN Number";
                }
                else
                {
                    Div_CIN.Visible = false;
                    Div_LLPIN.Visible = false;
                    Div_CIN_LLPIN_Btn.Visible = false;
                }

                Txt_Reg_Address_2.Text = dt.Rows[0]["VCH_REG_ADDRESS_2"].ToString();//Add by Debi
                DrpDwn_Reg_Country.SelectedValue = dt.Rows[0]["INT_REG_COUNTRY"].ToString(); //Add by Debi

                if (DrpDwn_Reg_Country.SelectedValue == "1")//Add by Debi
                {
                    DrpDwn_Reg_Country_SelectedIndexChanged(null, EventArgs.Empty);//Add by Debi
                    Div_Reg_State_DrpDwn.Visible = true;
                    Div_Reg_State_Text.Visible = false;
                    Txt_Reg_State.Text = "";
                    DrpDwn_Reg_State.SelectedItem.Text = dt.Rows[0]["VCH_REG_STATE"].ToString();//Add by Debi
                }
                else
                {
                    Div_Reg_State_Text.Visible = true;
                    Div_Reg_State_DrpDwn.Visible = false;
                    Txt_Reg_State.Text = "";
                    Txt_Reg_State.Text = dt.Rows[0]["VCH_REG_STATE"].ToString();//Add by Debi
                }

                Txt_Reg_City.Text = dt.Rows[0]["VCH_REG_CITY"].ToString();//Add by Debi
                Txt_Reg_PIN_Code.Text = dt.Rows[0]["VCH_REG_PIN"].ToString();//Add by Debi
                Txt_SL_Address_2.Text = dt.Rows[0]["VCH_SL_ADDRESS_2"].ToString();//Add by Debi
                DrpDwn_SL_Country.SelectedValue = dt.Rows[0]["INT_SL_COUNTRY"].ToString();//Add by Debi

                if (DrpDwn_SL_Country.SelectedValue == "1")//Add by Debi
                {
                    DrpDwn_SL_Country_SelectedIndexChanged(null, EventArgs.Empty);//Add by Debi
                    Div_SL_State_DrpDwn.Visible = true;
                    Div_SL_State_Text.Visible = false;
                    Txt_SL_State.Text = "";
                    DrpDwn_SL_State.SelectedItem.Text = dt.Rows[0]["VCH_SL_STATE"].ToString();//Add by Debi
                }
                else
                {
                    Div_SL_State_Text.Visible = true;
                    Div_SL_State_DrpDwn.Visible = false;
                    Txt_SL_State.Text = dt.Rows[0]["VCH_SL_STATE"].ToString();//Add by Debi
                }

                Txt_SL_City.Text = dt.Rows[0]["VCH_SL_CITY"].ToString();//Add by Debi
                Txt_SL_PIN_Code.Text = dt.Rows[0]["VCH_SL_PIN"].ToString();//Add by Debi
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
        Txt_Unit_Name.Text = "";
        Txt_First_Name.Text = "";
        Txt_Mobile_No.Text = "";
        Txt_Reg_Address_1.Text = "";
        Txt_CIN_Number.Text = "";

        DrpDwn_Salutation.SelectedIndex = 0;
        DrpDwn_Entity_Type.SelectedIndex = 0;
    }

    /// <summary>
    /// For bind entity type data 
    /// </summary>
    public void FillEntityType()
    {
        try
        {
            StrAction = "FE";
            DataTable dtentitytype = objRegService.BindDistrict(StrAction);
            DrpDwn_Entity_Type.DataSource = dtentitytype;
            DrpDwn_Entity_Type.DataTextField = "vchEntityName";
            DrpDwn_Entity_Type.DataValueField = "intEntityCode";
            DrpDwn_Entity_Type.DataBind();
            DrpDwn_Entity_Type.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    /// <summary>
    /// For bind Registration Country  data 
    /// </summary>
    public void FillRegdCountry()
    {
        try
        {
            StrAction = "FC";
            DataTable dtregdcountry = objRegService.BindRegdCountry(StrAction);
            DrpDwn_Reg_Country.DataSource = dtregdcountry;
            DrpDwn_Reg_Country.DataTextField = "VCH_COUNTRY_NAME";
            DrpDwn_Reg_Country.DataValueField = "INT_COUNTRYID";
            DrpDwn_Reg_Country.DataBind();
            DrpDwn_Reg_Country.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    /// <summary>
    /// For bind Site Location Country  data 
    /// </summary>
    public void FillSLCountry()
    {
        try
        {
            StrAction = "FC";
            DataTable dtslcountry = objRegService.BindRegdCountry(StrAction);
            DrpDwn_SL_Country.DataSource = dtslcountry;
            DrpDwn_SL_Country.DataTextField = "VCH_COUNTRY_NAME";
            DrpDwn_SL_Country.DataValueField = "INT_COUNTRYID";
            DrpDwn_SL_Country.DataBind();
            DrpDwn_SL_Country.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    /// <summary>
    /// This function use  for base64 data encription
    /// </summary>
    public string Base64Encryption(string StrData)
    {
        try
        {
            byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(StrData);

            // Perform Base64 encoding
            string StrEncodedString = Convert.ToBase64String(bytesToEncode);
            return StrEncodedString;
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    #endregion

    /// <summary>
    /// For entity type dropdwon change event 
    /// </summary>
    protected void DrpDwn_Entity_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int SelectedEntityType = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);

            string StrPan_Number = Convert.ToString(Hid_Pan_Number.Value);

            char[] Char_Pan_Number = StrPan_Number.ToCharArray();
            if (Char_Pan_Number[3].ToString().ToUpper() == "C" && SelectedEntityType == 1)
            {
               
                
                    Div_CIN.Visible = true;
                    Div_LLPIN.Visible = false;
                    Div_CIN_LLPIN_Btn.Visible = true;
                    BtnValidateCinLlpin.Text = "Validate CIN Number";
                
            }
            else if (Char_Pan_Number[3].ToString().ToUpper() == "F" && SelectedEntityType == 2)
            {
                Div_CIN.Visible = false;
                Div_LLPIN.Visible = true;
                Div_CIN_LLPIN_Btn.Visible = true;
                BtnValidateCinLlpin.Text = "Validate LLPIN Number";
            }
            else
            {
                Div_CIN.Visible = false;
                Div_LLPIN.Visible = false;
                Div_CIN_LLPIN_Btn.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }
    }

    /// <summary>
    /// For registration country dropdwon change event 
    /// </summary>
    protected void DrpDwn_Reg_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StrAction = "FRS";
            DataTable dtRegState = objRegService.BindRegdState(StrAction, Convert.ToInt32(DrpDwn_Reg_Country.SelectedValue));
            DrpDwn_Reg_State.DataSource = dtRegState;
            DrpDwn_Reg_State.DataTextField = "VCH_STATE";
            DrpDwn_Reg_State.DataValueField = "INT_STATE_ID";
            DrpDwn_Reg_State.DataBind();
            DrpDwn_Reg_State.Items.Insert(0, new ListItem("--Select--", "0"));

            if (Convert.ToInt32(DrpDwn_Reg_Country.SelectedValue) == 1)
            {
                Div_Reg_State_DrpDwn.Visible = true;
                Div_Reg_State_Text.Visible = false;
                Txt_Reg_State.Text = "";
            }
            else
            {
                Div_Reg_State_Text.Visible = true;
                Div_Reg_State_DrpDwn.Visible = false;
                Txt_Reg_State.Text = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }
    }

    /// <summary>
    /// For site location country dropdwon change event 
    /// </summary>
    protected void DrpDwn_SL_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StrAction = "FRS";
            DataTable dtSlState = objRegService.BindRegdState(StrAction, Convert.ToInt32(DrpDwn_SL_Country.SelectedValue));
            DrpDwn_SL_State.DataSource = dtSlState;
            DrpDwn_SL_State.DataTextField = "VCH_STATE";
            DrpDwn_SL_State.DataValueField = "INT_STATE_ID";
            DrpDwn_SL_State.DataBind();
            DrpDwn_SL_State.Items.Insert(0, new ListItem("--Select--", "0"));

            if (Convert.ToInt32(DrpDwn_SL_Country.SelectedValue) == 1)
            {
                Div_SL_State_DrpDwn.Visible = true;
                Div_SL_State_Text.Visible = false;
                Txt_SL_State.Text = "";
            }
            else
            {
                Div_SL_State_Text.Visible = true;
                Div_SL_State_DrpDwn.Visible = false;
                Txt_SL_State.Text = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }
    }
}