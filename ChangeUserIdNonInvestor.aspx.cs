﻿#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using DWHServiceReference;
#endregion

public partial class ChangeUserIdNonInvestor : System.Web.UI.Page
{
    #region Variables

    string strprojname = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];

    #endregion

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.SessionCheck();

        if (!IsPostBack)
        {
            fillDetails();
        }
    }

    #endregion

    #region FunctionUsed

    private void fillDetails()
    {
        LoginBusinessLayer objBAL = new LoginBusinessLayer();
        LoginDetails objEntity = new LoginDetails();

        DataTable dt = new DataTable();
        try
        {
            objEntity.strAction = "V1";
            objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

            dt = objBAL.viewInvestorDetails(objEntity);
            if (dt.Rows.Count > 0)
            {
                Lbl_Unit_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_NAME"]);
                Lbl_Email_Id.Text = Convert.ToString(dt.Rows[0]["VCH_EMAIL"]);
                Lbl_Mobile_Number.Text = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE"]);
                Lbl_User_Id.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID"]);
                Txt_Alternate_User_Id.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID_ALIAS"]);

                int intAliasNameCount = Convert.ToInt32(dt.Rows[0]["INT_ALIAS_NAME_COUNT"]);
                if (intAliasNameCount == 1)
                {
                    Txt_Alternate_User_Id.Enabled = false;
                    divConfirmAlias.Visible = false;
                    divBtnSubmit.Visible = false;
                    Lbl_Warning_Msg.Text = "You have already created your alternative user name.";
                }
                else
                {
                    Txt_Alternate_User_Id.Enabled = true;
                    divConfirmAlias.Visible = true;
                    divBtnSubmit.Visible = true;
                    Lbl_Warning_Msg.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "AlternateUserId");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion

    #region ButtonClickEvenets

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        LoginBusinessLayer objBAL = new LoginBusinessLayer();
        LoginDetails objEntity = new LoginDetails();

        try
        {
            /*---------------------------------------------------------------------------------------------------------------*/
            /// Both Industrial and Non-Industrial user can create alternate user id (alias user name).
            /// For Non-Industrial user (means the value Session["IndustryType"] is 2), the alias name will be updated only at GOSWIFT end.
            /// For Industrial user (means the value Session["IndustryType"] is 1), the alias name will be updated both at GOSWIFT and DWH ends. 
            /*---------------------------------------------------------------------------------------------------------------*/

            string strIndustryType = Convert.ToString(Session["IndustryType"]);

            
            if (strIndustryType == "2") ////Non-Industrial User
            {
                #region Non-Industrial-User

                objEntity.strAction = "U1";
                objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
                objEntity.strUserIdAlias = Txt_Alternate_User_Id.Text.Trim();
                objEntity.intAliasNameCount = 1;

                /////// DML Opertion (To GOSWIFT)
                string strReturnStatus = objBAL.createAliasName(objEntity);
                if (strReturnStatus == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The alternate user name provided by you is already exists. Please try with a new user name !</strong>', '" + strprojname + "'); </script>", false);
                }
                else if (strReturnStatus == "2")
                {
                    fillDetails();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Alternative user name created successfully !</strong>', '" + strprojname + "'); </script>", false);
                }
                else if (strReturnStatus == "3")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>You have already created your alternative user name !</strong>', '" + strprojname + "'); </script>", false);
                }
                else if (strReturnStatus == "4")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal server error, Please try again !</strong>', '" + strprojname + "'); </script>", false);
                }

                #endregion
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "AlternateUserId");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion
}