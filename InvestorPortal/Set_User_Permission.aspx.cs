//******************************************************************************************************************
// File Name             : Set_User_Permission.aspx.cs
// Description           : Grant application access permission to 2nd level user.
// Created by            : Sushant Kumar Jena
// Created on            : 26-Sep-2018
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

public partial class InvestorPortal_Set_User_Permission : SessionCheck
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
            fillUserList();
            fillPortalList();
        }
    }

    #region FunctionUsed

    ////// Fill 2nd Level User List
    private void fillUserList()
    {
        InvestorRegistration objBAL = new InvestorRegistration();
        InvestorInfo objInvEntity = new InvestorInfo();

        DataTable dt = new DataTable();
        try
        {
            objInvEntity.strAction = "V2";
            objInvEntity.INT_INVESTOR_ID = investorid;

            /////// Select Data
            dt = objBAL.fillUserList2ndLevel(objInvEntity);

            DrpDwn_User.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                DrpDwn_User.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_User.DataTextField = "VCH_FULL_NAME";
                DrpDwn_User.DataSource = dt;
                DrpDwn_User.DataBind();
            }
            DrpDwn_User.Items.Insert(0, new ListItem("-Select User-", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SetPermission");
        }
        finally
        {
            dt = null;
            objBAL = null;
            objInvEntity = null;
        }
    }

    ////// Get Portal(Application) List (From Service)
    private void fillPortalList()
    {
        try
        {
            /*-----------------------------------------------------------------*/
            /////// Service Initialization
            /*-----------------------------------------------------------------*/
            DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
            List<DWH_PortalList> objList = new List<DWH_PortalList>();

            /*-----------------------------------------------------------------*/
            /////// Generate Encryption Key (Security key to access Data Warehouse service methods)
            /*-----------------------------------------------------------------*/
            string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
            string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

            objList = objSrvRef.PortalList(strSecurityKey).ToList();

            DataList1.DataSource = objList;
            DataList1.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SetPermission");
        }
    }

    ////// Get Permission Details
    private void fillPermissionDetails()
    {
        InvestorRegistration objBAL = new InvestorRegistration();
        InvestorInfo objInvEntity = new InvestorInfo();

        DataTable dt = new DataTable();
        try
        {
            if (DrpDwn_User.SelectedIndex > 0)
            {
                objInvEntity.strAction = "V3";
                objInvEntity.INT_INVESTOR_ID = Convert.ToInt32(DrpDwn_User.SelectedValue);

                /////// Select Data
                dt = objBAL.ApplicationPermissionView(objInvEntity);
                if (dt.Rows.Count > 0)
                {
                    string strAppIds = dt.Rows[0]["VCH_APP_IDS"].ToString();
                    string[] strArrPer = strAppIds.Split(',');

                    for (int i = 0; i < DataList1.Items.Count; i++)
                    {
                        CheckBox chk = DataList1.Items[i].FindControl("CheckBox1") as CheckBox;
                        HiddenField Hid_App_Id = DataList1.Items[i].FindControl("Hid_App_Id") as HiddenField;

                        if (strArrPer.Contains(Hid_App_Id.Value))
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < DataList1.Items.Count; i++)
                    {
                        CheckBox chk = DataList1.Items[i].FindControl("CheckBox1") as CheckBox;
                        chk.Checked = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < DataList1.Items.Count; i++)
                {
                    CheckBox chk = DataList1.Items[i].FindControl("CheckBox1") as CheckBox;
                    chk.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SetPermission");
        }
        finally
        {
            dt = null;
            objBAL = null;
            objInvEntity = null;
        }
    }

    #endregion

    ////// Button for Data Insert and Update
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        InvestorRegistration objInvBAL = new InvestorRegistration();
        InvestorInfo objInvEntity = new InvestorInfo();

        try
        {
            if (DrpDwn_User.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Please select user to set permission !</strong>');", true);
                return;
            }

            /*-----------------------------------------------------------------*/
            string strAppId = "";
            for (int i = 0; i < DataList1.Items.Count; i++)
            {
                CheckBox chk = DataList1.Items[i].FindControl("CheckBox1") as CheckBox;

                if (chk.Checked == true)
                {
                    HiddenField Hid_App_Id = DataList1.Items[i].FindControl("Hid_App_Id") as HiddenField;
                    strAppId = strAppId + Hid_App_Id.Value + ",";
                }
            }

            if (strAppId == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Please select atleast one application to set permission !</strong>');", true);
                return;
            }
            /*-----------------------------------------------------------------*/

            strAppId = strAppId.Trim().TrimEnd(',');

            string[] strArr = DrpDwn_User.SelectedItem.Text.Split('-');
            string strUserId = strArr[0].ToString();

            /*-----------------------------------------------------------------*/
            /////// Service Initialization (Push data to Data Warehouse)
            /*-----------------------------------------------------------------*/
            DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
            DWH_Model objSrvEntity = new DWH_Model();

            /*-----------------------------------------------------------------*/
            /////// Assign value to property
            objSrvEntity.VCHAPPIDS = strAppId;
            objSrvEntity.VCHUSERNAME = strUserId;

            /*-----------------------------------------------------------------*/
            /////// Generate Encryption Key (Security key to access Data Warehouse service methods)
            string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
            string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

            /*-----------------------------------------------------------------*/
            /////// DML Opertion (To Data Warehouse)
            string strReturnVal = objSrvRef.PortalPermissionUpdate(objSrvEntity, strSecurityKey);
            if (strReturnVal == "5")
            {
                objInvEntity.strAction = "P1";
                objInvEntity.INT_INVESTOR_ID = Convert.ToInt32(DrpDwn_User.SelectedValue);
                objInvEntity.strAppIds = strAppId;
                objInvEntity.INT_CREATED_BY = investorid;

                string strReturnStatus = objInvBAL.ApplicationPermissionAED(objInvEntity);
                if (strReturnStatus == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Permission granted successfully !</strong>');", true);
                }
                else if (strReturnStatus == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Permission updated successfully !</strong>');", true);
                }
                else if (strReturnStatus == "3")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Something went wrong, Please try again !</strong>');", true);
                }
            }
            else if (strReturnVal == "11")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Invalid User Id !</strong>');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SetPermission");
        }
        finally
        {
            objInvBAL = null;
            objInvEntity = null;
        }
    }

    ////// DropDownList SelectedIndexChanged
    protected void DrpDwn_User_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPermissionDetails();
    }
}