//******************************************************************************************************************
// File Name             :   User_Creation.aspx.cs
// Description           :   Second Level User Creation
// Created by            :   Sushant Kumar Jena
// Created on            :   29-Aug-2018
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using DWHServiceReference;
using System.Configuration;

public partial class InvestorPortal_User_Creation : System.Web.UI.Page
{
    #region GlobalVariables

    static int investorid;
    ////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #endregion

    ////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] != null)
        {
            investorid = Convert.ToInt32(Session["InvestorId"]);
        }

        if (!IsPostBack)
        {
            generateUserId();
            fillInvestorChildUnit();
        }
    }

    #region FunctionUsed

    ////// To get child unit name and self unit name for a investor.  
    private void fillInvestorChildUnit()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "INVUNIT";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

            DataTable dt = new DataTable();
            dt = objserviceDashboard.getInvestorChildUnit(objSWP);

            DrpDwn_Unit_Name.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                DrpDwn_Unit_Name.DataTextField = "VCH_INV_NAME";
                DrpDwn_Unit_Name.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_Unit_Name.DataSource = dt;
                DrpDwn_Unit_Name.DataBind();
            }

            DrpDwn_Unit_Name.Items.Insert(0, new ListItem("-Select Unit-", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserCreation");
        }
    }

    ////// Clear fields
    private void clearField()
    {
        Txt_Conf_Pwd.Text = "";
        Txt_Designation.Text = "";
        Txt_Email_Id.Text = "";
        Txt_First_Name.Text = "";
        Txt_Last_Name.Text = "";
        Txt_Middle_Name.Text = "";
        Txt_Mobile_No.Text = "";
        Txt_Pwd.Text = "";
        Txt_User_Id.Text = "";

        DrpDwn_Salutation.SelectedIndex = 0;
        DrpDwn_Unit_Name.SelectedIndex = 0;
    }

    ////// Auto generated user id
    private void generateUserId()
    {
        InvestorInfo objInvEntity = new InvestorInfo();
        InvestorRegistration objBAL = new InvestorRegistration();

        objInvEntity.strAction = "B";
        objInvEntity.INT_INVESTOR_ID = investorid;

        DataTable dt = new DataTable();

        dt = objBAL.CheckSecondLevelUser(objInvEntity);

        string strPAN = Convert.ToString(dt.Rows[0]["VCH_PAN"]);
        int intCount = Convert.ToInt32(dt.Rows[0]["INT_COUNT"]);
        intCount = intCount + 1;

        string strRandNo = generateRandomNo(3); ///// Generate 3 digits random number
        string strUserId = strPAN + "_" + strRandNo + intCount.ToString(); //// Create user id

        Txt_User_Id.Text = strUserId;

        ViewState["PAN"] = strPAN;
    }

    ////// Generate Random Numner    
    public string generateRandomNo(int intLength)
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

    ////// Button Submit
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        InvestorRegistration objRegService = new InvestorRegistration();
        InvestorInfo objInvEntity = new InvestorInfo();

        try
        {
            /*---------------------------------------------------------------*/
            int intParentId = Convert.ToInt32(DrpDwn_Unit_Name.SelectedValue);
            int intApprovalStatus = 1; //// Auto Approved

            /*---------------------------------------------------------------*/

            objInvEntity.INT_SALUTATION = Convert.ToInt32(DrpDwn_Salutation.SelectedValue);
            objInvEntity.VCH_CONTACT_FIRSTNAME = Txt_First_Name.Text.Trim();
            objInvEntity.VCH_CONTACT_LASTNAME = Txt_Last_Name.Text.Trim();
            objInvEntity.VCH_CONTACT_MIDDLENAME = Txt_Middle_Name.Text.Trim();
            objInvEntity.VCH_EMAIL = Txt_Email_Id.Text.Trim() == "" ? null : Txt_Email_Id.Text.Trim();
            objInvEntity.INT_APPROVALSTATUS = intApprovalStatus;
            objInvEntity.INT_STATUS = 0;//// Active User
            objInvEntity.INT_OTP_STATUS = 1;//// OTP Auto Approved
            objInvEntity.VCH_INV_PASSWORD = CommonHelperCls.GenerateHash(Txt_Conf_Pwd.Text.Trim());
            objInvEntity.VCH_INV_USERID = Txt_User_Id.Text.Trim();
            objInvEntity.VCH_OFF_MOBILE = Txt_Mobile_No.Text.Trim();
            objInvEntity.INT_PARENTID = intParentId;
            objInvEntity.VCH_INV_NAME = DrpDwn_Unit_Name.SelectedItem.Text;
            objInvEntity.intUserLevel = 2; //// This specify second level user (It is a Person not an Unit)
            objInvEntity.INT_CREATED_BY = investorid;
            objInvEntity.strDesignation = Txt_Designation.Text.Trim();

            /////// DML Operation (To GOSWIFT Database)
            string strResult = objRegService.InvestorRegistrationBAL(objInvEntity, "I");
            if (strResult == "2") //// Success
            {
                /*-----------------------------------------------------------------*/
                /////// Get unique id against investor id 
                /////// Here get unique id of parent id as investor id and push to data warehouse
                /*-----------------------------------------------------------------*/
                objInvEntity.strAction = "UNQID";
                objInvEntity.INT_INVESTOR_ID = intParentId;
                string strUniqueId = objRegService.getUniqueId(objInvEntity);

                /*-----------------------------------------------------------------*/
                /////// Service Initialization (Push data to Data Warehouse)
                /*-----------------------------------------------------------------*/
                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                DWH_Model objSrvEntity = new DWH_Model();

                /*-----------------------------------------------------------------*/
                /////// Assign value to property
                objSrvEntity.VCHINDUSTRYNAME = objInvEntity.VCH_INV_NAME;
                objSrvEntity.INTSALUTATION = objInvEntity.INT_SALUTATION;
                objSrvEntity.VCHPROMOTERFNAME = objInvEntity.VCH_CONTACT_FIRSTNAME;
                objSrvEntity.VCHPROMOTERMNAME = objInvEntity.VCH_CONTACT_MIDDLENAME;
                objSrvEntity.VCHPROMOTERLNAME = objInvEntity.VCH_CONTACT_LASTNAME;
                objSrvEntity.VCHEMAILID = objInvEntity.VCH_EMAIL;
                objSrvEntity.VCHMOBILENO = objInvEntity.VCH_OFF_MOBILE;
                objSrvEntity.VCHUSERNAME = objInvEntity.VCH_INV_USERID;
                objSrvEntity.VCHPASSWORD = objInvEntity.VCH_INV_PASSWORD;
                objSrvEntity.INTPARENTID = objInvEntity.INT_PARENTID;
                objSrvEntity.INTUSERLEVEL = objInvEntity.intUserLevel;
                objSrvEntity.VCHDESIGNATION = objInvEntity.strDesignation;
                objSrvEntity.VCHUSERUNIQUEID = strUniqueId;
                objSrvEntity.VCHPANNO = ViewState["PAN"].ToString();

                /*-----------------------------------------------------------------*/
                /////// Generate Encryption Key (Security key to access Data Warehouse service methods)
                string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

                /*-----------------------------------------------------------------*/
                /////// DML Opertion (To Data Warehouse)
                string strReturnVal = objSrvRef.UserRegistration(objSrvEntity, strSecurityKey);
                if (strReturnVal != "")
                {
                    string[] strArrRetVal = strReturnVal.Split('_');
                    if (strArrRetVal[0] == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The user id provided by you is already exist !</strong>');", true);
                        return;
                    }
                    else if (strArrRetVal[0] == "4")
                    {
                        /*-----------------------------------------------------------------*/
                        ///// Update unique id in GOSWIFT database after successfully insertion in Data Warehouse
                        /*-----------------------------------------------------------------*/
                        objInvEntity.strAction = "U1";
                        objInvEntity.strUniqueId = strArrRetVal[1];
                        objInvEntity.VCH_INV_USERID = objInvEntity.VCH_INV_USERID;

                        strReturnVal = objRegService.updateUniqueId(objInvEntity);
                        if (strReturnVal == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>User Created Successfully !</strong>');", true);
                        }
                        else if (strReturnVal == "2")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Due to internal server problem,The data has not been saved,Please save again in Draft User section !</strong>');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try again !</strong>', '" + strProjName + "'); </script>", false);
                }

                clearField();
                generateUserId();
            }
            else if (strResult == "3") //// Error
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Internal Server error. Please try again after sometime !</strong>');", true);
            }
            //else if (strResult == "5") //// Duplicate Email Id Found
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The email id provided by you is already exist,Please try with a different email id !</strong>');", true);
            //}
            else if (strResult == "6") //// Duplicate User Id Found
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The user id provided by you is already exist !</strong>');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserCreation");
        }
        finally
        {
            objRegService = null;
            objInvEntity = null;
        }
    }
}