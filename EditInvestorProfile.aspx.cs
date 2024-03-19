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

public partial class EditInvestorProfile : SessionCheck
{
    #region Declaration And Variables

    /////// Get Project Name From Web.Config File   
    string StrProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    string action = "";

    InvestorRegistration objRegService = new InvestorRegistration();
    #endregion

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/LogOut.aspx", true);
        }

        if (!IsPostBack)
        {
            if (Convert.ToString(Session["InvestorId"]) != "")
            {
                CommonHelperCls ob = new CommonHelperCls();

                try
                {
                    ob.BindDropDown(DrpDwn_Salutation, "ID", "VCH_SALUTATION", "Select ID, VCH_SALUTATION from M_SALUTATION");
                    FillEntityType();
                    FillRegdCountry();
                    FillSLCountry();
                    FillInvestorInfo(Session["InvestorId"].ToString());                 
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "ProfileUpdate");
                }
            }
            
        }
    }

    #region ButtonClickEvenets

    ///// Button Update
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
            /*---------------------------------*/
            //For Server side validation.
            /*---------------------------------*/

            string StrMcaOnOffKey = ConfigurationManager.AppSettings["MCAValidation"];

            if(StrMcaOnOffKey== "ON")
            {
                if (Hid_cin_llpn.Value == "")
                {
                    Btn_CIN.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate CIN/LLPIN.</strong>');", true);
                    return;
                }

                if (Hid_cin_llpn.Value == "No Data")
                {
                    Btn_CIN.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate CIN/LLPIN.</strong>');", true);
                    return;
                }
                /*---------------------------------------------------------------*/
                ///Check whether the CIN or LLPIN, which was validated is the same during insertion
                /*---------------------------------------------------------------*/
                if (ViewState["CinNumber"] != null)
                {
                    if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)
                    {
                        if (Convert.ToString(ViewState["CinNumber"]) != Txt_CIN_Number.Text)
                        {
                            Btn_CIN.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate CIN/LLPIN.</strong>');", true);
                            return;
                        }
                    }
                    else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)
                    {
                        if (Convert.ToString(ViewState["CinNumber"]) != Txt_LLPIN_Number.Text)
                        {
                            Btn_CIN.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please click on button to validate CIN/LLPIN.</strong>');", true);
                            return;
                        }
                    }

                }
            }

            

            if (Txt_First_Name.Text.Trim() == "")
            {
                Txt_First_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter name of the applicant.</strong>');", true);
                Txt_First_Name.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_Mobile_No.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter mobile number.</strong>');", true);
                Txt_Mobile_No.Focus();
                return;
            }
            if (Txt_Mobile_No.Text.StartsWith("0"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Mobile Number should not be start with zero.</strong>');", true);
                Txt_Mobile_No.Focus();
                return;

            }
            if (Txt_Mobile_No.Text.Length < 10)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Mobile Number can not be less then 10 characters !</strong>');", true);
                Txt_Mobile_No.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_Address_1.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please registration enter address-1 !</strong>');", true);
                Txt_Address_1.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_RegAddress_2.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration address-2 !</strong>');", true);
                Txt_RegAddress_2.Focus();
                return;
            }
            if (DrpRegdCountry.SelectedIndex == 0)
            {
                DrpRegdCountry.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select registration country name !</strong>');", true);
                return;
            }
            string SelectedregCountryValue = DrpRegdCountry.SelectedValue;
            if (SelectedregCountryValue == "1")
            {
                string SelectedregStateText = DrpRegdState.SelectedItem.Text;

                if (SelectedregStateText == "--Select--")
                {
                    DrpRegdState.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select registration state name !</strong>');", true);
                    return;
                }
            }
            else
            {
                string stateName = TxtRegdState.Text.Trim();
                if (string.IsNullOrEmpty(stateName))
                {
                    TxtRegdState.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration state name !</strong>');", true);
                    return;
                }
            }
            if (string.IsNullOrEmpty(Txt_Regd_City.Text))
            {
                Txt_Regd_City.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration city name !</strong>');", true);
                return;
            }
            if (string.IsNullOrEmpty(Txt_Regd_Pincode.Text))
            {
                Txt_Regd_Pincode.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration pincode !</strong>');", true);
                return;
            }
            string Pincode = Txt_Regd_Pincode.Text.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(Pincode, @"^\d{6}$"))
            {
                Txt_Regd_Pincode.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>  Please enter a valid 6 - digit registration pincode. !</strong>');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_Site_Loc_1.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location address !</strong>');", true);
                Txt_Site_Loc_1.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_SitAddress_2.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location address-2 !</strong>');", true);
                Txt_SitAddress_2.Focus();
                return;
            }
            if (DrpSlCountry.SelectedIndex == 0)
            {
                DrpSlCountry.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select site location country name !</strong>');", true);
                return;
            }
            string SelectedslCountryValue = DrpSlCountry.SelectedValue;
            if (SelectedslCountryValue == "1")
            {
                string SelectedslStateText = DrpSlState.SelectedItem.Text;

                if (SelectedslStateText == "--Select--")
                {
                    DrpSlState.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select site location state name !</strong>');", true);
                    return;
                }
            }
            else
            {
                string StateName = Txt_SL_State.Text.Trim();
                if (string.IsNullOrEmpty(StateName))
                {
                    Txt_SL_State.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location state name !</strong>');", true);
                    return;
                }
            }
            if (string.IsNullOrEmpty(Txt_Sl_City.Text))
            {
                Txt_Sl_City.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location city name !</strong>');", true);
                return;
            }
            if (string.IsNullOrEmpty(Txt_Sl_Pincode.Text))
            {
                Txt_Sl_Pincode.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location pincode !</strong>');", true);
                return;
            }
            string Slpincode = Txt_Sl_Pincode.Text.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(Slpincode, @"^\d{6}$"))
            {
                Txt_Sl_Pincode.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>  Please enter a valid 6 - digit site location pincode. !</strong>');", true);
                return;
            }
            if (DrpDwn_Entity_Type.SelectedIndex == 0)
            {
                DrpDwn_Entity_Type.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select entity type !</strong>');", true);
                return;
            }
            int SelectedEntityType = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
            if (SelectedEntityType == 1)
            {
                string SelectedCINno = Txt_CIN_Number.Text.Trim();
                if (SelectedCINno == "")
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter CIN number.</strong>');", true);
                    return;
                }
                if (SelectedCINno.Length > 21)
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>CIN number should be 21 digits.</strong>');", true);
                    return;
                }
                // Regex pattern for CIN number validation
                string CinRegexPattern = @"^([L|U]{1})([0-9]{5})([A-Za-z]{2})([0-9]{4})([A-Za-z]{3})([0-9]{6})$";

                if (!System.Text.RegularExpressions.Regex.IsMatch(SelectedCINno, CinRegexPattern))
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid CIN number in the format L/U-12345AB1234XYZ123456.</strong>');", true);
                    return;
                }
            }
            else if (SelectedEntityType == 2)
            {
                string SelectedLLPINno = Txt_LLPIN_Number.Text.Trim();

                if (string.IsNullOrEmpty(SelectedLLPINno))
                {
                    Txt_LLPIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter LLPIN number.</strong>');", true);
                    return;
                }
                if (SelectedLLPINno.Length > 8)
                {
                    Txt_LLPIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>LLPIN number should be 8 digits.</strong>');", true);
                    return;
                }
                // Regex pattern for LLPIN number validation
                string llpinRegexPattern = @"^([a-zA-Z]{2,3})-([0-9]{4})$";

                if (!System.Text.RegularExpressions.Regex.IsMatch(SelectedLLPINno, llpinRegexPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid LLPIN number in the format XXX-1234.</strong>');", true);
                    return;
                }
            }
            
            string StrIndustryType = Convert.ToString(Session["IndustryType"]);

            if (StrIndustryType == "1") ////Industrial User
            {
                #region Industrial User

                /*-----------------------------------------------------------------*/
                /////// Service Initialization
                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                DWH_Model objEnt = new DWH_Model();

                /*-----------------------------------------------------------------*/
                /////// Assign value to property (For Service)

                objEnt.INTSALUTATION = Convert.ToInt32(DrpDwn_Salutation.SelectedValue);
                objEnt.VCHPROMOTERFNAME = Txt_First_Name.Text.Trim();
                objEnt.VCHADDRESS = Txt_Address_1.Text.Trim();
                objEnt.VCHCORADDRESS = Convert.ToString(Txt_Site_Loc_1.Text);
                objEnt.VCHMOBILENO = Txt_Mobile_No.Text.Trim();
                objEnt.VCHEMAILID = Txt_Email_Id.Text;
                objEnt.VCHUSERNAME = Session["UserId"].ToString();
                objEnt.VCH_REG_ADDRESS_2 = Txt_RegAddress_2.Text.Trim();//Add by Debi
                objEnt.INT_REG_COUNTRY= Convert.ToInt32(DrpRegdCountry.SelectedValue);//Add by Debi
                if (DrpRegdCountry.SelectedValue == "1")//Add by Debi
                {
                    objEnt.VCH_REG_STATE = DrpRegdState.SelectedItem.Text;//Add by Debi
                }
                else
                {
                    objEnt.VCH_REG_STATE = TxtRegdState.Text;//Add by Debi
                }
                objEnt.VCH_REG_CITY = Txt_Regd_City.Text.Trim();//Add by Debi
                objEnt.VCH_REG_PIN = Txt_Regd_Pincode.Text.Trim();//Add by Debi
                objEnt.VCH_SL_ADDRESS_2= Txt_SitAddress_2.Text.Trim();//Add by Debi
                objEnt.INT_SL_COUNTRY= Convert.ToInt32(DrpSlCountry.SelectedValue);//Add by Debi
                if (DrpSlCountry.SelectedValue == "1")//Add by Debi
                {
                    objEnt.VCH_SL_STATE = DrpSlState.SelectedItem.Text;//Add by Debi
                }
                else
                {
                    objEnt.VCH_SL_STATE = Txt_SL_State.Text.Trim();//Add by Debi
                }
                objEnt.VCH_SL_CITY = Txt_Sl_City.Text.Trim();//Add by Debi
                objEnt.VCH_SL_PIN = Txt_Sl_Pincode.Text.Trim();//Add by Debi
                if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue)==1)//Add by Debi
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);//Add by Debi
                    objEnt.VCHCINNUMBER = Convert.ToString(Txt_CIN_Number.Text.Trim());// add by anil
                }
                else if(Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)//Add by Debi
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);//Add by Debi
                    objEnt.VCH_LLPIN_NUMBER = Txt_LLPIN_Number.Text.Trim();//Add by Debi
                }
                else
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);// add by anil
                }
                /*----------------------------------------------------------------*/
                /////// Generate Encryption Key (Security key to access Data Warehouse servce methods)
                string StrEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                string StrSecurityKey = objSrvRef.KeyEncryption(StrEncryptionKey);

                /*-----------------------------------------------------------------*/
                /////// DML opertion through DWH service
                string StrReturnVal = objSrvRef.ProfileUpdate(objEnt, StrSecurityKey);
                if (StrReturnVal != "")
                {
                    if (StrReturnVal == "5") ////// Success
                    {
                        /*-----------------------------------------------------------------*/
                        ///// After succcessfully update in data warehouse,update in goswift database
                        /*-----------------------------------------------------------------*/
                        objInvDet.strAction = "U";
                        objInvDet.Salutation = Convert.ToInt32(DrpDwn_Salutation.SelectedValue);
                        objInvDet.strContactFirstName = Txt_First_Name.Text.Trim();
                        objInvDet.strAddress = Txt_Address_1.Text.Trim();
                        objInvDet.strSiteAddress = Convert.ToString(Txt_Site_Loc_1.Text);
                        objInvDet.MobNo = Txt_Mobile_No.Text.Trim();
                        objInvDet.strEmail = Txt_Email_Id.Text;
                        objInvDet.strUserID = Session["InvestorId"].ToString();
                        objInvDet.StrRegAddress_2 = Txt_RegAddress_2.Text.Trim();//Add by Debi
                        objInvDet.IntRegCountry = Convert.ToInt32(DrpRegdCountry.SelectedValue);//Add by Debi
                        if (DrpRegdCountry.SelectedValue == "1")//Add by Debi
                        {
                            objInvDet.StrRegState = DrpRegdState.SelectedItem.Text;//Add by Debi
                        }
                        else
                        {
                            objInvDet.StrRegState = TxtRegdState.Text;//Add by Debi
                        }
                        objInvDet.StrRegCity = Txt_Regd_City.Text.Trim();//Add by Debi
                        objInvDet.StrRegPincode = Txt_Regd_Pincode.Text.Trim();//Add by Debi
                        objInvDet.StrSlAddress_2 = Txt_SitAddress_2.Text.Trim();//Add by Debi
                        objInvDet.IntSlCountry = Convert.ToInt32(DrpSlCountry.SelectedValue);//Add by Debi
                        if (DrpSlCountry.SelectedValue == "1")//Add by Debi
                        {
                            objInvDet.StrSlState = DrpSlState.SelectedItem.Text;//Add by Debi
                        }
                        else
                        {
                            objInvDet.StrSlState = Txt_SL_State.Text.Trim();//Add by Debi
                        }
                        objInvDet.StrSlCity = Txt_Sl_City.Text.Trim();//Add by Debi
                        objInvDet.StrSlPincode = Txt_Sl_Pincode.Text.Trim();//Add by Debi
                        objInvDet.intEntitytype = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);//Add by Debi
                        if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)//Add by Debi
                        {                          
                            objInvDet.strCINnumber = Convert.ToString(Txt_CIN_Number.Text.Trim());//Add by Debi
                        }
                        else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)//Add by Debi
                        {
                            objInvDet.StrLLPINumber = Txt_LLPIN_Number.Text.Trim();//Add by Debi
                        }
                        string StrRetval = Convert.ToString(objService.InvestorData(objInvDet)); // data update in GOSWIFT DB
                        if (StrRetval == "2")
                        {
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

    /////Button Back
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        string strIndustryType = Convert.ToString(Session["IndustryType"]);
        if (strIndustryType == "1") ////Industrial User
        {
            Response.Redirect("InvesterDashboard.aspx");
        }
    }

    #endregion

    #region CommonMethods

    ///// Fill Investor Information
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
                Txt_Address_1.Text = dt.Rows[0]["VCH_ADDRESS"].ToString();
                Txt_Site_Loc_1.Text = dt.Rows[0]["VCH_SITELOCATION"].ToString();//Add by Debi
                DrpDwn_Entity_Type.SelectedValue= dt.Rows[0]["INT_ENTITY_TYPE"].ToString(); // add by anil
                Hid_Pan_Number.Value = StrPan_number;//Add by Debi
                if (DrpDwn_Entity_Type.SelectedValue == "1")//Add by Debi
                {
                    Div1.Visible = true;
                    Div2.Visible = false;
                    Div3.Visible = true;
                    Txt_CIN_Number.Text= dt.Rows[0]["VCH_CIN_NUMBER"].ToString(); // add by anil
                    Btn_CIN.Text = "Validate CIN Number";
                }
                else if (DrpDwn_Entity_Type.SelectedValue == "2")//Add by Debi
                {
                    Div1.Visible = false;
                    Div2.Visible = true;
                    Div3.Visible = true;
                    Txt_LLPIN_Number.Text = dt.Rows[0]["VCH_LLPIN_NUMBER"].ToString(); // add by anil
                    Btn_CIN.Text = "Validate LLPIN Number";
                }
                else
                {
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = false;
                }
                Txt_RegAddress_2.Text = dt.Rows[0]["VCH_REG_ADDRESS_2"].ToString();//Add by Debi
                DrpRegdCountry.SelectedValue = dt.Rows[0]["INT_REG_COUNTRY"].ToString(); //Add by Debi

                if (DrpRegdCountry.SelectedValue == "1")//Add by Debi
                {
                    DrpRegdCountry_SelectedIndexChanged(null, EventArgs.Empty);//Add by Debi
                    st3.Visible = true;
                    st4.Visible = false;
                    TxtRegdState.Text = "";
                    DrpRegdState.SelectedItem.Text = dt.Rows[0]["VCH_REG_STATE"].ToString();//Add by Debi
                }
                else
                {
                    st4.Visible = true;
                    st3.Visible = false;
                    TxtRegdState.Text = "";
                    TxtRegdState.Text = dt.Rows[0]["VCH_REG_STATE"].ToString();//Add by Debi
                }
                Txt_Regd_City.Text = dt.Rows[0]["VCH_REG_CITY"].ToString();//Add by Debi
                Txt_Regd_Pincode.Text = dt.Rows[0]["VCH_REG_PIN"].ToString();//Add by Debi
                Txt_SitAddress_2.Text = dt.Rows[0]["VCH_SL_ADDRESS_2"].ToString();//Add by Debi
                DrpSlCountry.SelectedValue = dt.Rows[0]["INT_SL_COUNTRY"].ToString();//Add by Debi
                if (DrpSlCountry.SelectedValue == "1")//Add by Debi
                {
                    DrpSlCountry_SelectedIndexChanged(null, EventArgs.Empty);//Add by Debi
                    st1.Visible = true;
                    st2.Visible = false;
                    Txt_SL_State.Text = "";
                    DrpSlState.SelectedItem.Text = dt.Rows[0]["VCH_SL_STATE"].ToString();//Add by Debi
                }
                else
                {
                    st2.Visible = true;
                    st1.Visible = false;                 
                    Txt_SL_State.Text = dt.Rows[0]["VCH_SL_STATE"].ToString();//Add by Debi
                }
                Txt_Sl_City.Text = dt.Rows[0]["VCH_SL_CITY"].ToString();//Add by Debi
                Txt_Sl_Pincode.Text = dt.Rows[0]["VCH_SL_PIN"].ToString();//Add by Debi
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    ///// Clear Fields
    public void Cleardata()
    {
        Txt_Unit_Name.Text = "";
        Txt_First_Name.Text = "";
        Txt_Mobile_No.Text = "";
        Txt_Address_1.Text = "";
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
            action = "FE";
            DataTable dtentitytype = objRegService.BindDistrict(action);
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
            action = "FC";
            DataTable dtregdcountry = objRegService.BindRegdCountry(action);
            DrpRegdCountry.DataSource = dtregdcountry;
            DrpRegdCountry.DataTextField = "VCH_COUNTRY_NAME";
            DrpRegdCountry.DataValueField = "INT_COUNTRYID";
            DrpRegdCountry.DataBind();
            DrpRegdCountry.Items.Insert(0, new ListItem("--Select--", "0"));
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
            action = "FC";
            DataTable dtslcountry = objRegService.BindRegdCountry(action);
            DrpSlCountry.DataSource = dtslcountry;
            DrpSlCountry.DataTextField = "VCH_COUNTRY_NAME";
            DrpSlCountry.DataValueField = "INT_COUNTRYID";
            DrpSlCountry.DataBind();
            DrpSlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
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
                if (DrpDwn_Entity_Type.SelectedValue == "1")
                {
                    Div1.Visible = true;
                    Div2.Visible = false;
                    Div3.Visible = true;
                Btn_CIN.Text = "Validate CIN Number";
                }
                else if (DrpDwn_Entity_Type.SelectedValue == "2")

                {
                    Div1.Visible = false;
                    Div2.Visible = true;    
                    Div3.Visible = true;
                Btn_CIN.Text = "Validate LLPIN Number";
                }               
                else
                {
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = false;
                }
        }
       catch(Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }
    }
    /// <summary>
    /// For registration country dropdwon change event 
    /// </summary>
    protected void DrpRegdCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            action = "FRS";
            DataTable dtregdstate = objRegService.BindRegdState(action, Convert.ToInt32(DrpRegdCountry.SelectedValue));
            DrpRegdState.DataSource = dtregdstate;
            DrpRegdState.DataTextField = "VCH_STATE";
            DrpRegdState.DataValueField = "INT_STATE_ID";
            DrpRegdState.DataBind();
            DrpRegdState.Items.Insert(0, new ListItem("--Select--", "0"));

            if(Convert.ToInt32( DrpRegdCountry.SelectedValue) == 1)
            {
                st3.Visible = true;
                st4.Visible = false;
                TxtRegdState.Text = "";
            }
            else
            {
                st4.Visible = true;
                st3.Visible = false;
                TxtRegdState.Text = "";
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
    protected void DrpSlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            action = "FRS";
            DataTable dtregdstate = objRegService.BindRegdState(action, Convert.ToInt32(DrpSlCountry.SelectedValue));
            DrpSlState.DataSource = dtregdstate;
            DrpSlState.DataTextField = "VCH_STATE";
            DrpSlState.DataValueField = "INT_STATE_ID";
            DrpSlState.DataBind();
            DrpSlState.Items.Insert(0, new ListItem("--Select--", "0"));

            if (Convert.ToInt32(DrpSlCountry.SelectedValue) == 1)
            {
                st1.Visible = true;
                st2.Visible = false;
                Txt_SL_State.Text = "";
            }
            else
            {
                st2.Visible = true;
                st1.Visible = false;
                Txt_SL_State.Text = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }
    }


    /// <summary>
    /// This function use fro Validate CIN and LLPIN numebr from MCA side
    /// </summary>
    
    protected void Btn_CIN_Click(object sender, EventArgs e)
    {
        try
        {
            string StrMcaOnOffKey = ConfigurationManager.AppSettings["MCAValidation"];

           if (StrMcaOnOffKey == "ON")
           { 

            InvestorDetails objInvDet = new InvestorDetails();
            InvestorBusinessLayer objService = new InvestorBusinessLayer();

            // CIN  api KEY
            string StrCINLLPINurlKey = ConfigurationManager.AppSettings["CinLlpinurl"];
            string StrCinTokenUserIdKey = ConfigurationManager.AppSettings["CinTokenUserId"];
            string StrCinTokenPasswordKey = ConfigurationManager.AppSettings["CinTokenPassword"];
            

            Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[REQUEST_For_GenerateToken_Url]:- " + StrCINLLPINurlKey + " - [REQUEST_For_GenerateToken_UserId]:- " + StrCinTokenUserIdKey+ "- [REQUEST_For_GenerateToken_Password]:- "+ StrCinTokenPasswordKey);


            // Token generation for CIN validate
            var client = new RestClient(StrCINLLPINurlKey);
            client.Timeout = -1;

            var Tokenrequest = new RestRequest("/token", Method.POST);
            Tokenrequest.AddHeader("Authorization", "Basic ME4wUDBtQm1NdGVGcTNZX1c5cjdZRkxQZWswYTpwQmVWd3hzTjdJWnVfcEdKUzk1MFZoUmxjQVlh");
            Tokenrequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Tokenrequest.AddParameter("grant_type", "password");
            Tokenrequest.AddParameter("username", StrCinTokenUserIdKey);
            Tokenrequest.AddParameter("password", StrCinTokenPasswordKey);
            IRestResponse TokenResponse = client.Execute(Tokenrequest);
            Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[Response_Form_TokenApi_StatusCode]:- " + TokenResponse.StatusCode);


            if (TokenResponse.StatusCode == HttpStatusCode.OK)
            {
                if (TokenResponse.Content.ToString() != "")
                {
                    Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[Response_Form_TokenApi_Content]:- " + TokenResponse.Content);

                    string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(TokenResponse.Content)["access_token"].ToString();

                    Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[Response_Form_TokenApi_Token]:- " + strAccessToke);

                    var Datarequest = (RestRequest)null;
                    if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)
                    {
                        Datarequest = new RestRequest("/cin/service/integration/1.0.0?CIN=" + Txt_CIN_Number.Text, Method.GET);
                        Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[Response_Form_TokenApi_Cin]:- " + Txt_CIN_Number.Text);
                    }
                    else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)
                    {
                        Datarequest = new RestRequest("/cin/service/integration/1.0.0?CIN=" + Txt_LLPIN_Number.Text, Method.GET);
                        Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[Response_Form_TokenApi_Cin]:- " + Txt_LLPIN_Number.Text);
                    }

                    Datarequest.AddHeader("Authorization", "Bearer " + strAccessToke);
                    IRestResponse Dataresponse = client.Execute(Datarequest); ////  CIN or LLPIN number validate api call


                    Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[Response_Form_CINdataApi_StatusCode]:- " + Dataresponse.StatusCode);

                    if (Dataresponse.StatusCode == HttpStatusCode.OK)
                    {
                        string message = JsonConvert.DeserializeObject<Dictionary<string, object>>(Dataresponse.Content)["message"].ToString();
                        Hid_cin_llpn.Value = message;
                        Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[Response_Form_CINdataApi_Content]:- " + Dataresponse.Content);


                        if (message == "No Data")
                        {
                            if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid CIN number !</strong>', '" + StrProjName + "'); </script>", false);
                                return;
                            }
                            else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid LLPIN number !</strong>', '" + StrProjName + "'); </script>", false);
                                return;
                            }


                        }
                        else if (message == "Data fetched Successfully")
                        {

                            if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>CIN number validate successfully !</strong>');", true);
                                ViewState["CinNumber"] = Txt_CIN_Number.Text;
                            }
                            else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>LLPIN number validate successfully !</strong>');", true);
                                ViewState["CinNumber"] = Txt_LLPIN_Number.Text;
                            }
                            else
                            {
                                ViewState["CinNumber"] = null;
                            }


                            objInvDet.strAction = "UCV";
                            objInvDet.strUserID = Session["InvestorId"].ToString();
                            objInvDet.StrCinLlpinData = Base64encription(Dataresponse.Content);
                            Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[CinAPIDataUpdateGOSWIFTDatabase]:- " + Base64encription(Dataresponse.Content));
                            string StrReturndata = Convert.ToString(objService.InvestorData(objInvDet)); // CIN or LLPIN validation data update in GOSWIFT DB

                            Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[CinAPIDataUpdateGOSWIFTDatabase]:- " + Base64encription(Dataresponse.Content)+ " -[DataUpdateStatus] :-"+ StrReturndata);
                        }
                    }
                    else if (Dataresponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong> Invalied Token ! <strong>');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Internal server error !<strong>');", true);
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>No Data find !<strong>');", true);
                }

            }
            else if (TokenResponse.StatusCode != HttpStatusCode.OK)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Internal server error !<strong>');", true);
            }

           }
            else
            {
                Util.LogRequestResponse("ProfileUpdate", "GetCINStatusFromMCA", "[WEBConfig key OFF]");
            }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ProfileUpdate");
        }

    }

    /// <summary>
    /// This function use  for base64 data encription
    /// </summary>

    public string Base64encription(string data) 
    {
        try
        {
            byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(data);

            // Perform Base64 encoding
            string encodedString = Convert.ToBase64String(bytesToEncode);
            return encodedString;
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        
    }
}