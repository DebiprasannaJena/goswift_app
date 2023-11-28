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

public partial class EditNonInvestorProfile : SessionCheck
{
    #region Declaration And Variables

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #endregion
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

            string strIndustryType = Convert.ToString(Session["IndustryType"]);

            if (strIndustryType == "2") ////Non-Industrial User
            {
                #region Non-Industrial User

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

                string strRetval = Convert.ToString(objService.InvestorData(objInvDet));
                if (strRetval == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data updated successfully !</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
                else if (strRetval == "3")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Internal server error,Please try after sometime !');", true);
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
        }
    }

    /////Button Back
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        string strIndustryType = Convert.ToString(Session["IndustryType"]);

        if (strIndustryType == "2") ////Non-Industrial User
        {
            Response.Redirect("InvesterDashboardNonIndustry.aspx");
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
                Txt_Unit_Name.Text = dt.Rows[0]["VCH_INV_NAME"].ToString();

                DrpDwn_Salutation.SelectedValue = dt.Rows[0]["INT_SALUTATION"].ToString();
                Txt_First_Name.Text = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                // Txt_Middle_Name.Text = dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString();
                //  Txt_Last_Name.Text = dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString();
                Txt_Mobile_No.Text = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                Txt_Email_Id.Text = dt.Rows[0]["VCH_EMAIL"].ToString();
                Txt_Address.Text = dt.Rows[0]["VCH_ADDRESS"].ToString();
                Txt_Site_Loc.Text = dt.Rows[0]["VCH_SITELOCATION"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

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

        DrpDwn_Salutation.SelectedIndex = 0;
    }

    #endregion
}