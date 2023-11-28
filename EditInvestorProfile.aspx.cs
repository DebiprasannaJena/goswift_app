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

            if (Txt_CIN_No.Text.Trim() == "")
            {
                Txt_CIN_No.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter CIN number.</strong>');", true);
                return;
            }
            if (Txt_CIN_No.Text.Trim().Length !=21)
            {
                Txt_CIN_No.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>CIN number should be 21 digits.</strong>');", true);
                return;
            }
            if (DrpDwn_Entity_Type.SelectedIndex == 0)
            {
                DrpDwn_Entity_Type.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select entity type !</strong>');", true);
                return;
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
                objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);// add by anil
                objEnt.VCHCINNUMBER = Convert.ToString(Txt_CIN_No.Text.Trim());// add by anil

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
                        objInvDet.intEntitytype = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                        objInvDet.strCINnumber = Convert.ToString(Txt_CIN_No.Text.Trim());

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
                Txt_Unit_Name.Text = dt.Rows[0]["VCH_INV_NAME"].ToString();

                DrpDwn_Salutation.SelectedValue = dt.Rows[0]["INT_SALUTATION"].ToString();
                Txt_First_Name.Text = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
               // Txt_Middle_Name.Text = dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString();
              //  Txt_Last_Name.Text = dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString();
                Txt_Mobile_No.Text = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                Txt_Email_Id.Text = dt.Rows[0]["VCH_EMAIL"].ToString();
                Txt_Address.Text = dt.Rows[0]["VCH_ADDRESS"].ToString();
                Txt_Site_Loc.Text = dt.Rows[0]["VCH_SITELOCATION"].ToString();
                Txt_CIN_No.Text= dt.Rows[0]["VCH_CIN_NUMBER"].ToString(); // add by anil
                DrpDwn_Entity_Type.SelectedValue= dt.Rows[0]["INT_ENTITY_TYPE"].ToString(); // add by anil
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
        Txt_CIN_No.Text = "";
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

    #endregion
}