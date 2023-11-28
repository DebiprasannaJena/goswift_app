#region  PageInfo
//******************************************************************************************************************
// File Name             :   ViewMISIndustryList.aspx.cs
// Description           :   Get Industry Related Details Based On InvestorID
// Created by            :   Satyaprakash
// Created On            :   16-04-2019
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Investor;
using System.Data;
using System.Drawing;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;

public partial class Portal_MISReport_ViewMISIndustryList : System.Web.UI.Page
{
    int intInvetsorId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["val"] != null)
            {
                string[] strArr = new string[2];
                strArr = Request.QueryString["val"].Split('~');

                intInvetsorId = Convert.ToInt32(strArr[0]);
                fillDetails(intInvetsorId);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewIndustryDetails");
        }
    }

    private void fillDetails(int intInvetsorId)
    {
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProposal = new ProposalDet();

        DataTable dt = new DataTable();
        try
        {
            objProposal.strAction = "D2";

            objProposal.IntInvestorId = intInvetsorId;

            dt = objService.IndustryListDetails(objProposal);

            if (dt.Rows.Count > 0)
            {
                Lbl_Unit_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_NAME"]);
                string strSalutation = Convert.ToString(dt.Rows[0]["VCH_SALUTATION"]);
                string strFirstName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_FIRSTNAME"]);
                string strMiddleName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_MIDDLENAME"]);
                string strLastName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_LASTNAME"]);
                Lbl_Applicant_Name.Text = strSalutation + " " + strFirstName + " " + strMiddleName + " " + strLastName;
                Lbl_Address.Text = Convert.ToString(dt.Rows[0]["VCH_ADDRESS"]);
                //empty
                if (Lbl_Address.Text == "")
                {
                    Lbl_Address.Text = "-NA-";
                    Lbl_Address.ForeColor = Color.Red;
                }
                //empty
                Lbl_Mobile_No.Text = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE"]);
                Lbl_Email_Id.Text = Convert.ToString(dt.Rows[0]["VCH_EMAIL"]);

                Lbl_Site_Location.Text = Convert.ToString(dt.Rows[0]["VCH_SITELOCATION"]);
                //empty
                if (Lbl_Site_Location.Text == "")
                {
                    Lbl_Site_Location.Text = "-NA-";
                    Lbl_Site_Location.ForeColor = Color.Red;
                }
                //empty
                Lbl_District.Text = Convert.ToString(dt.Rows[0]["vchDistrictName"]);
                //empty
                if (Lbl_District.Text == "")
                {
                    Lbl_District.Text = "-NA-";
                    Lbl_District.ForeColor = Color.Red;
                }
                //empty
                Lbl_Block.Text = Convert.ToString(dt.Rows[0]["vchBlockName"]);
                //empty
                if (Lbl_Block.Text == "")
                {
                    Lbl_Block.Text = "-NA-";
                    Lbl_Block.ForeColor = Color.Red;
                }
                //empty
                Lbl_Sector.Text = Convert.ToString(dt.Rows[0]["VCH_SECTOR"]);
                //empty
                if (Lbl_Sector.Text == "")
                {
                    Lbl_Sector.Text = "-NA-";
                    Lbl_Sector.ForeColor = Color.Red;
                }
                //empty
                Lbl_Sub_Sector.Text = Convert.ToString(dt.Rows[0]["vchSubSectorName"]);

                //empty
                if (Lbl_Sub_Sector.Text == "")
                {
                    Lbl_Sub_Sector.Text = "-NA-";
                    Lbl_Sub_Sector.ForeColor = Color.Red;
                }
                //empty

                /*-------------------------------------------------------------------*/

                string strDocType = Convert.ToString(dt.Rows[0]["VCH_LICENCE_NO_TYPE"]);
                if (strDocType != "")
                {
                    Lbl_EIN_IEM_No.Text = Convert.ToString(dt.Rows[0]["VCH_EIN_IEM"]);
                    Lbl_Doc_Type.Text = strDocType + " Number";
                    HyperLink1.NavigateUrl = "~/Document/RegdDoc/" + Convert.ToString(dt.Rows[0]["VCH_LICENCE_DOC"]);
                    HyperLink1.Visible = true;
                    Lbl_Doc_Text.Text = "";
                    Lbl_Doc_Text.Visible = false;
                }
                else
                {
                    Lbl_EIN_IEM_No.Text = "-NA-";
                    Lbl_EIN_IEM_No.ForeColor = Color.Red;
                    Lbl_Doc_Type.Text = "EIN/IEM Number";
                    HyperLink1.NavigateUrl = "";
                    HyperLink1.Visible = false;
                    Lbl_Doc_Text.Text = "-NA-";
                    Lbl_Doc_Text.Visible = true;
                }

                /*-------------------------------------------------------------------*/

                Lbl_Unit_Type.Text = Convert.ToString(dt.Rows[0]["INT_PARENT_ID"]) == "0" ? "Main Unit" : "Subsidiary Unit";
                Lbl_GSTIN.Text = Convert.ToString(dt.Rows[0]["VCH_GSTIN"]);
                //empty
                if (Lbl_GSTIN.Text == "")
                {
                    Lbl_GSTIN.Text = "-NA-";
                    Lbl_GSTIN.ForeColor = Color.Red;
                }
                //empty
                Lbl_PAN.Text = Convert.ToString(dt.Rows[0]["VCH_PAN"]);
                Lbl_Investment_Level.Text = Convert.ToString(dt.Rows[0]["INT_CATEGORY"]) == "1" ? "Project Cost >= 50 Crore" : "Project Cost < 50 Crore";

                Lbl_User_Id.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID"]);
                Lbl_User_Level.Text = Convert.ToString(dt.Rows[0]["INT_USER_LEVEL"]);
                Lbl_Alias_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID_ALIAS"]);
                //empty
                if (Lbl_Alias_Name.Text == "")
                {
                    Lbl_Alias_Name.Text = "-NA-";
                    Lbl_Alias_Name.ForeColor = Color.Red;
                }
                //empty

                /*-------------------------------------------------------------------*/

                string strOTPStatus = Convert.ToString(dt.Rows[0]["INT_OTP_STATUS"]);
                if (strOTPStatus == "1")
                {
                    Lbl_OTP_Status.Text = "Verified";
                    Lbl_OTP_Status.ForeColor = Color.Green;
                }
                else
                {
                    Lbl_OTP_Status.Text = "Pending";
                    Lbl_OTP_Status.ForeColor = Color.Red;
                }

                /*-------------------------------------------------------------------*/

                Lbl_Approval_Status.Text = Convert.ToString(dt.Rows[0]["VCH_APPROVAL_STATUS"]);

                if (Lbl_Approval_Status.Text == "Approved")
                {
                    Lbl_Approval_Status.ForeColor = Color.Green;
                }
                else if ((Lbl_Approval_Status.Text == "Pending"))
                {
                    Lbl_Approval_Status.ForeColor = Color.Blue;
                }

                else if ((Lbl_Approval_Status.Text == "Rejected"))
                {
                    Lbl_Approval_Status.ForeColor = Color.Red;
                }

                /*-------------------------------------------------------------------*/

                Lbl_Parent_Unit_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_NAME_PARENT"]);
                Lbl_Rejection_Cause.Text = Convert.ToString(dt.Rows[0]["VCH_REJECTION_CAUSE"]);

                //empty
                if (Lbl_Rejection_Cause.Text == "")
                {
                    Lbl_Rejection_Cause.Text = "-NA-";
                    Lbl_Rejection_Cause.ForeColor = Color.Red;
                }
                //empty

            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            dt = null;
            objService = null;
            objProposal = null;
        }
    }

    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndustryList.aspx");
    }
}