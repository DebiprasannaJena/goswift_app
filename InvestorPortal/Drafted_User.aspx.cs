//******************************************************************************************************************
// File Name             : Drafted_User.aspx.cs
// Description           : To view  second level user (Person) which are in drafted stage and also repush the user details to Data Warehouse and update unique id.
// Created by            : Sushant Kumar Jena
// Created on            : 21-Sep-2018
// Modification History  :
//       <CR no.>         <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using DWHServiceReference;
using System.Configuration;

public partial class InvestorPortal_Drafted_User : System.Web.UI.Page
{
    #region GlobalVariables

    static int investorid;
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #endregion

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] != null)
        {
            investorid = Convert.ToInt32(Session["InvestorId"]);
        }

        if (!IsPostBack)
        {
            fillGrid();
        }
    }

    #region FunctionUsed

    /// <summary>
    /// Fill 2nd level drafted users
    /// </summary>
    private void fillGrid()
    {
        InvestorRegistration objBAL = new InvestorRegistration();
        InvestorInfo objInvEntity = new InvestorInfo();

        DataTable dt = new DataTable();
        try
        {
            objInvEntity.strAction = "VD2";
            objInvEntity.INT_INVESTOR_ID = investorid;

            /////// Select Data
            dt = objBAL.viewDraftedUsers(objInvEntity);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserDraftedApplication");
        }
        finally
        {
            dt = null;
            objBAL = null;
            objInvEntity = null;
        }
    }

    #endregion


    /////// Button Action for Approval and Assign Permission
    protected void Btn_Action_Click(object sender, EventArgs e)
    {
        InvestorRegistration objInvBAL = new InvestorRegistration();
        InvestorInfo objInvEntity = new InvestorInfo();

        DataTable dt = new DataTable();
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            /*--------------------------------------------------------------------------*/

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");

            objInvEntity.strAction = "V1";
            objInvEntity.INT_INVESTOR_ID = Convert.ToInt32(Hid_Investor_Id.Value);

            /////// Select Data
            dt = objInvBAL.viewUnitDetails(objInvEntity);

            if (dt.Rows.Count > 0)
            {
                string strUserId = Convert.ToString(dt.Rows[0]["VCH_INV_USERID"]);
                int intParentId = Convert.ToInt32(dt.Rows[0]["INT_PARENT_ID"]);

                /*-----------------------------------------------------------------*/
                /////// Get unique id against investor id
                objInvEntity.strAction = "UNQID";
                objInvEntity.INT_INVESTOR_ID = intParentId;
                string strUniqueId = objInvBAL.getUniqueId(objInvEntity);

                /*-----------------------------------------------------------------*/
                /////// Service Initialization (Push data to Data Warehouse)
                /*-----------------------------------------------------------------*/
                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                DWH_Model objSrvEntity = new DWH_Model();

                /*-----------------------------------------------------------------*/
                /////// Assign value to property
                objSrvEntity.VCHINDUSTRYNAME = Convert.ToString(dt.Rows[0]["VCH_INV_NAME"]);
                objSrvEntity.VCHEMAILID = Convert.ToString(dt.Rows[0]["VCH_EMAIL"]);
                objSrvEntity.INTSALUTATION = Convert.ToInt32(dt.Rows[0]["INT_SALUTATION"]);
                objSrvEntity.VCHPROMOTERFNAME = Convert.ToString(dt.Rows[0]["VCH_CONTACT_FIRSTNAME"]);
                objSrvEntity.VCHPROMOTERMNAME = Convert.ToString(dt.Rows[0]["VCH_CONTACT_MIDDLENAME"]);
                objSrvEntity.VCHPROMOTERLNAME = Convert.ToString(dt.Rows[0]["VCH_CONTACT_LASTNAME"]);
                objSrvEntity.VCHMOBILENO = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE"]);
                objSrvEntity.VCHUSERNAME = strUserId;
                objSrvEntity.VCHPASSWORD = Convert.ToString(dt.Rows[0]["VCH_INV_PASSWORD"]);
                objSrvEntity.INTPARENTID = intParentId;
                objSrvEntity.INTUSERLEVEL = 2; //// This specify second level user (It is a Person not an Unit)
                objSrvEntity.VCHDESIGNATION = Convert.ToString(dt.Rows[0]["VCH_USER_DESIGNATION"]);
                objSrvEntity.VCHUSERUNIQUEID = strUniqueId;
                objSrvEntity.VCHPANNO = Convert.ToString(dt.Rows[0]["VCH_PAN"]);

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
                    if (strArrRetVal[0] == "4" || strArrRetVal[0] == "1")
                    {
                        /*-----------------------------------------------------------------*/
                        ///// Update unique id in GOSWIFT database after successfully insertion in Data Warehouse
                        /*-----------------------------------------------------------------*/
                        objInvEntity.strAction = "U1";
                        objInvEntity.strUniqueId = strArrRetVal[1];
                        objInvEntity.VCH_INV_USERID = strUserId;

                        strReturnVal = objInvBAL.updateUniqueId(objInvEntity);
                        if (strReturnVal == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>User Created Successfully !</strong>');", true);
                            fillGrid();
                        }
                        else if (strReturnVal == "2")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Due to internal server problem, The data has not been saved, Please save again !</strong>');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try again !</strong>', '" + strProjName + "'); </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try again !</strong>', '" + strProjName + "'); </script>", false);
            }

            /*--------------------------------------------------------------------------*/
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserPermission");
        }
        finally
        {
            objInvBAL = null;
            objInvEntity = null;
        }
    }
}