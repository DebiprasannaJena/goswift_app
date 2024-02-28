using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using InvestorService;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.ServiceModel;
using System.Data;
using DWHServiceReference;
using System.Configuration;

public partial class EditInvestorProfile : SessionCheck
{
    #region Declaration And Variables

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

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
        CommonHelperCls ob = new CommonHelperCls();
        InvestorDetails objInvDet = new InvestorDetails();
        InvestorBusinessLayer objService = new InvestorBusinessLayer();

        try
        {
            /*---------------------------------------------------------------------------------------------------------------*/
            /// Both Industrial and Non-Industrial user can update their own profile details.
            /// For Non-Industrial user (means the value Session["IndustryType"] is 2), the profile will be changed only at GOSWIFT end.
            /// For Industrial user (means the value Session["IndustryType"] is 1), the profile will be changed both at GOSWIFT and DWH ends. 
            /*---------------------------------------------------------------------------------------------------------------*/

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
            if (string.IsNullOrWhiteSpace(Txt_Address.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please registration enter address !</strong>');", true);
                Txt_Address.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_Site_Loc.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location address !</strong>');", true);
                Txt_Site_Loc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_RegAddress_Invst.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration address-2 !</strong>');", true);
                Txt_RegAddress_Invst.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(Txt_SitAddress_Invst.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location address-2 !</strong>');", true);
                Txt_SitAddress_Invst.Focus();
                return;
            }
            if (DrpRegdCountry.SelectedIndex == 0)
            {
                DrpRegdCountry.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select registration country name !</strong>');", true);
                return;
            }
            string selectedregCountryValue = DrpRegdCountry.SelectedValue;
             if (selectedregCountryValue == "1")
             {
                string selectedregStateText = DrpRegdState.SelectedItem.Text;

                if (selectedregStateText == "--Select--")
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
            if (DrpSlCountry.SelectedIndex == 0)
            {
                DrpSlCountry.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select site location country name !</strong>');", true);
                return;
            }
            string selectedslCountryValue = DrpSlCountry.SelectedValue;
            if (selectedslCountryValue == "1")
            {
                string selectedslStateText = DrpSlState.SelectedItem.Text;

                if (selectedslStateText == "--Select--")
                {
                    DrpSlState.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select site location state name !</strong>');", true);
                    return;
                }
            }
            else
            {
                string stateName = Txt_SL_State.Text.Trim();
                if (string.IsNullOrEmpty(stateName))
                {
                    Txt_SL_State.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location state name !</strong>');", true);
                    return;
                }
            }
            if (string.IsNullOrEmpty(Txt_Regd_City.Text))
            {
                Txt_Regd_City.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration city name !</strong>');", true);
                return;
            }
            
            if (string.IsNullOrEmpty(Txt_Sl_City.Text))
            {
                Txt_Sl_City.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location city name !</strong>');", true);
                return;
            }
            if (string.IsNullOrEmpty(Txt_Regd_Pincode.Text))
            {
                Txt_Regd_Pincode.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter registration pincode !</strong>');", true);
                return;
            }
            string pincode = Txt_Regd_Pincode.Text.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(pincode, @"^\d{6}$"))
            {
              
                Txt_Regd_Pincode.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>  Please enter a valid 6 - digit registration pincode. !</strong>');", true);
                return;
            }
            if (string.IsNullOrEmpty(Txt_Sl_Pincode.Text))
            {
                Txt_Sl_Pincode.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter site location pincode !</strong>');", true);
                return;
            }
            string slpincode = Txt_Sl_Pincode.Text.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(slpincode, @"^\d{6}$"))
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
            int selectedEntityType = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
            if (selectedEntityType == 1)
            {
                // Assuming Txt_CIN_Number is the ID of your TextBox control for CIN number input
                string selectedCINno = Txt_CIN_Number.Text.Trim();

                if (selectedCINno == "")
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter CIN number.</strong>');", true);
                    return;
                }
                if (selectedCINno.Length != 21)
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>CIN number should be 21 digits.</strong>');", true);
                    return;
                }
                // Regex pattern for CIN number validation
                string cinRegexPattern = @"^([L|U]{1})([0-9]{5})([A-Za-z]{2})([0-9]{4})([A-Za-z]{3})([0-9]{6})$";

                if (!System.Text.RegularExpressions.Regex.IsMatch(selectedCINno, cinRegexPattern))
                {
                    Txt_CIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid CIN number in the format L/U-12345AB1234XYZ123456.</strong>');", true);
                    return;
                }
            }
            else if (selectedEntityType == 2)
            {
                string selectedLLPINno = Txt_LLPIN_Number.Text.Trim();

                if (string.IsNullOrEmpty(selectedLLPINno))
                {
                    Txt_LLPIN_Number.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter LLPIN number.</strong>');", true);
                    return;
                }
                //if (selectedLLPINno.Length != 16)
                //{
                //    Txt_LLPIN_Number.Focus();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>LLPIN number should be 16 digits.</strong>');", true);
                //    return;
                //}
                // Regex pattern for LLPIN number validation
                string llpinRegexPattern = @"^([a-zA-Z]{2,3})-([0-9]{4})$";

                if (!System.Text.RegularExpressions.Regex.IsMatch(selectedLLPINno, llpinRegexPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter a valid LLPIN number in the format XX-1234 or XXX-1234.</strong>');", true);
                    return;
                }
            }
            string strIndustryType = Convert.ToString(Session["IndustryType"]);

            if (strIndustryType == "1") ////Industrial User
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
                //objEnt.VCHPROMOTERMNAME = Txt_Middle_Name.Text.Trim();
               // objEnt.VCHPROMOTERLNAME = Txt_Last_Name.Text.Trim();
                objEnt.VCHADDRESS = Txt_Address.Text.Trim();
                objEnt.VCHCORADDRESS = Convert.ToString(Txt_Site_Loc.Text);
                objEnt.VCHMOBILENO = Txt_Mobile_No.Text.Trim();
                objEnt.VCHEMAILID = Txt_Email_Id.Text;
                objEnt.VCHUSERNAME = Session["UserId"].ToString();
               
                
                objEnt.VCH_REG_ADDRESS_2 = Txt_RegAddress_Invst.Text.Trim();
                objEnt.INT_REG_COUNTRY= Convert.ToInt32(DrpRegdCountry.SelectedValue);
                if(DrpRegdCountry.SelectedValue == "1")
                {
                    objEnt.VCH_REG_STATE = DrpRegdState.SelectedItem.Text;
                }
                else
                {
                    objEnt.VCH_REG_STATE = TxtRegdState.Text;
                }
                objEnt.VCH_REG_CITY = Txt_Regd_City.Text.Trim();
                objEnt.VCH_REG_PIN = Txt_Regd_Pincode.Text.Trim();
                objEnt.VCH_SL_ADDRESS_2= Txt_SitAddress_Invst.Text.Trim();
                objEnt.INT_SL_COUNTRY= Convert.ToInt32(DrpSlCountry.SelectedValue);
                if (DrpSlCountry.SelectedValue == "1")
                {
                    objEnt.VCH_SL_STATE = DrpSlState.SelectedItem.Text;
                }
                else
                {
                    objEnt.VCH_SL_STATE = Txt_SL_State.Text.Trim();
                }
                objEnt.VCH_SL_CITY = Txt_Sl_City.Text.Trim();
                objEnt.VCH_SL_PIN = Txt_Sl_Pincode.Text.Trim();
                if(Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue)==1)
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                    objEnt.VCHCINNUMBER = Convert.ToString(Txt_CIN_Number.Text.Trim());// add by anil
                }
                else if(Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                    objEnt.VCH_LLPIN_NUMBER = Txt_LLPIN_Number.Text.Trim();
                }
                else
                {
                    objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);// add by anil
                }
                /*----------------------------------------------------------------*/
                /////// Generate Encryption Key (Security key to access Data Warehouse servce methods)
                string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

                /*-----------------------------------------------------------------*/
                /////// DML opertion through service
                string strReturnVal = objSrvRef.ProfileUpdate(objEnt, strSecurityKey);
                if (strReturnVal != "")
                {
                    if (strReturnVal == "5") ////// Success
                    {
                        /*-----------------------------------------------------------------*/
                        ///// After succcessfully update in data warehouse,update in goswift database
                        /*-----------------------------------------------------------------*/
                        objInvDet.strAction = "U";
                        objInvDet.Salutation = Convert.ToInt32(DrpDwn_Salutation.SelectedValue);
                        objInvDet.strContactFirstName = Txt_First_Name.Text.Trim();
                       // objInvDet.strContactMiddleName = Txt_Middle_Name.Text.Trim();
                       // objInvDet.strContactLastName = Txt_Last_Name.Text.Trim();
                        objInvDet.strAddress = Txt_Address.Text.Trim();
                        objInvDet.strSiteAddress = Convert.ToString(Txt_Site_Loc.Text);
                        objInvDet.MobNo = Txt_Mobile_No.Text.Trim();
                        objInvDet.strEmail = Txt_Email_Id.Text;
                        objInvDet.strUserID = Session["InvestorId"].ToString();
                        
                        objInvDet.StrRegAddress_2 = Txt_RegAddress_Invst.Text.Trim();
                        objInvDet.IntRegCountry = Convert.ToInt32(DrpRegdCountry.SelectedValue);
                        if (DrpRegdCountry.SelectedValue == "1")
                        {
                            objInvDet.StrRegState = DrpRegdState.SelectedItem.Text;
                        }
                        else
                        {
                            objInvDet.StrRegState = TxtRegdState.Text;
                        }
                        objInvDet.StrRegCity = Txt_Regd_City.Text.Trim();
                        objInvDet.StrRegPincode = Txt_Regd_Pincode.Text.Trim();
                        objInvDet.StrSlAddress_2 = Txt_SitAddress_Invst.Text.Trim();
                        objInvDet.IntSlCountry = Convert.ToInt32(DrpSlCountry.SelectedValue);
                        if (DrpSlCountry.SelectedValue == "1")
                        {
                            objInvDet.StrSlState = DrpSlState.SelectedItem.Text;
                        }
                        else
                        {
                            objInvDet.StrSlState = Txt_SL_State.Text.Trim();
                        }
                        objInvDet.StrSlCity = Txt_Sl_City.Text.Trim();
                        objInvDet.StrSlPincode = Txt_Sl_Pincode.Text.Trim();
                        if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 1)
                        {
                            objInvDet.intEntitytype = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                            objInvDet.strCINnumber = Convert.ToString(Txt_CIN_Number.Text.Trim());
                        }
                        else if (Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue) == 2)
                        {
                            objInvDet.intEntitytype = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                            objInvDet.StrLLPINumber = Txt_LLPIN_Number.Text.Trim();
                        }
                        else
                        {
                            objInvDet.intEntitytype = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                        }
                        string strRetval = Convert.ToString(objService.InvestorData(objInvDet));
                        if (strRetval == "2")
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
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data updated successfully !</strong>', '" + strProjName + "'); </script>", false);
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data updated successfully !</strong>', '" + strProjName + "'); </script>", false);
                                return;

                            }
                        }
                        else if (strRetval == "3")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Internal server error,Please try after sometime !');", true);
                        }
                        else if (strRetval == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('CIN numebr already exists !');", true);
                        }
                        else if (strRetval == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('LLPI numebr already exists !');", true);
                        }
                    }
                    else if (strReturnVal == "11")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid user id !</strong>', '" + strProjName + "'); </script>", false);
                        return;
                    }
                    else if (strReturnVal == "20")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Unit name already exists !</strong>', '" + strProjName + "'); </script>", false);
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
        finally
        {
            ob = null;
            objInvDet = null;
            objService = null;
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
        DataTable dt = new DataTable();
        try
        {
            dt = CommonHelperCls.ViewInvestorDetails(Session["InvestorId"].ToString(), "V");
            if (dt.Rows.Count > 0)
            {
                
                string strPan = dt.Rows[0]["VCH_PAN"].ToString();
                Session["Pan"] = strPan;
                Txt_Unit_Name.Text = dt.Rows[0]["VCH_INV_NAME"].ToString();

                DrpDwn_Salutation.SelectedValue = dt.Rows[0]["INT_SALUTATION"].ToString();
                Txt_First_Name.Text = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
               // Txt_Middle_Name.Text = dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString();
              //  Txt_Last_Name.Text = dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString();
                Txt_Mobile_No.Text = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                Txt_Email_Id.Text = dt.Rows[0]["VCH_EMAIL"].ToString();
                Txt_Address.Text = dt.Rows[0]["VCH_ADDRESS"].ToString();
                Txt_Site_Loc.Text = dt.Rows[0]["VCH_SITELOCATION"].ToString();
                
                DrpDwn_Entity_Type.SelectedValue= dt.Rows[0]["INT_ENTITY_TYPE"].ToString(); // add by anil
                Hid_Pan_Number.Value = strPan;
                if (DrpDwn_Entity_Type.SelectedValue == "1")
                {

                    Div1.Visible = true;
                    Div2.Visible = false;
                    Txt_CIN_Number.Text= dt.Rows[0]["VCH_CIN_NUMBER"].ToString(); // add by anil
                }
                else if (DrpDwn_Entity_Type.SelectedValue == "2")
                {
                    Div1.Visible = false;
                    Div2.Visible = true;
                    Txt_LLPIN_Number.Text = dt.Rows[0]["VCH_LLPIN_NUMBER"].ToString(); // add by anil
                }
                else
                {
                   
                    Div1.Visible = false;
                    Div2.Visible = false;
                }
                Txt_RegAddress_Invst.Text = dt.Rows[0]["VCH_REG_ADDRESS_2"].ToString();
                DrpRegdCountry.SelectedValue = dt.Rows[0]["INT_REG_COUNTRY"].ToString(); 

                if (DrpRegdCountry.SelectedValue == "1")
                {
                    DrpRegdCountry_SelectedIndexChanged(null, EventArgs.Empty);
                    st3.Visible = true;
                    st4.Visible = false;
                    TxtRegdState.Text = "";
                    DrpRegdState.SelectedItem.Text = dt.Rows[0]["VCH_REG_STATE"].ToString();
                }
                else
                {
                    st4.Visible = true;
                    st3.Visible = false;
                    TxtRegdState.Text = "";
                    TxtRegdState.Text = dt.Rows[0]["VCH_REG_STATE"].ToString();
                }
                Txt_Regd_City.Text = dt.Rows[0]["VCH_REG_CITY"].ToString();
                Txt_Regd_Pincode.Text = dt.Rows[0]["VCH_REG_PIN"].ToString();
                Txt_SitAddress_Invst.Text = dt.Rows[0]["VCH_SL_ADDRESS_2"].ToString();
                DrpSlCountry.SelectedValue = dt.Rows[0]["INT_SL_COUNTRY"].ToString();
                if (DrpSlCountry.SelectedValue == "1")
                {
                    DrpSlCountry_SelectedIndexChanged(null, EventArgs.Empty);
                    st1.Visible = true;
                    st2.Visible = false;
                    Txt_SL_State.Text = "";
                    DrpSlState.SelectedItem.Text = dt.Rows[0]["VCH_SL_STATE"].ToString();
                }
                else
                {
                    st2.Visible = true;
                    st1.Visible = false;
                   // Txt_SL_State.Text = "";
                    Txt_SL_State.Text = dt.Rows[0]["VCH_SL_STATE"].ToString();
                }
                Txt_Sl_City.Text = dt.Rows[0]["VCH_SL_CITY"].ToString();
                Txt_Sl_Pincode.Text = dt.Rows[0]["VCH_SL_PIN"].ToString();


            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            dt = null;
        }
    }

    ///// Clear Fields
    public void Cleardata()
    {
        Txt_Unit_Name.Text = "";
        Txt_First_Name.Text = "";
       // Txt_Middle_Name.Text = "";
       // Txt_Last_Name.Text = "";
        Txt_Mobile_No.Text = "";
        Txt_Address.Text = "";
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
    /// For bind Regd Country  data 
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

    protected void DrpDwn_Entity_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
                if (DrpDwn_Entity_Type.SelectedValue == "1")
                {
                    Div1.Visible = true;
                    Div2.Visible = false;
                    Txt_CIN_Number.Text = "";
                    Txt_LLPIN_Number.Text = "";

                }
                else if (DrpDwn_Entity_Type.SelectedValue == "2")

                {
                    Div1.Visible = false;
                    Div2.Visible = true;
                    Txt_CIN_Number.Text = "";
                    Txt_LLPIN_Number.Text = "";
                }               
                else
                {
                    Div1.Visible = false;
                    Div2.Visible = false;
                }

            
        }
       catch(Exception ex)
        {
            throw ex;
        }
    }

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
            Util.LogError(ex, "Registration");
        }
    }

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
            Util.LogError(ex, "Registration");
        }
    }
}