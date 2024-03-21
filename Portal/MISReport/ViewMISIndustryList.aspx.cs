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
using System.Text;
using System.IO;

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
                FillDetails(intInvetsorId);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewIndustryDetails");
        }
    }

    private void FillDetails(int intInvetsorId)
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
                Lbl_Mobile_No.Text = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE"]);
                Lbl_Email_Id.Text = Convert.ToString(dt.Rows[0]["VCH_EMAIL"]);
                Lbl_Address.Text = Convert.ToString(dt.Rows[0]["VCH_ADDRESS"]);               
                //empty
                if (Lbl_Address.Text == "")
                {
                    Lbl_Address.Text = "-NA-";
                    Lbl_Address.ForeColor = Color.Red;
                }
                Lbl_Regd_Address_2.Text = Convert.ToString(dt.Rows[0]["VCH_REG_ADDRESS_2"]);
                if (Lbl_Regd_Address_2.Text == "")
                {
                    Lbl_Regd_Address_2.Text = "-NA-";
                    Lbl_Regd_Address_2.ForeColor = Color.Red;
                }
                
                Lbl_Site_Location.Text = Convert.ToString(dt.Rows[0]["VCH_SITELOCATION"]);
                //empty
                if (Lbl_Site_Location.Text == "")
                {
                    Lbl_Site_Location.Text = "-NA-";
                    Lbl_Site_Location.ForeColor = Color.Red;
                }
                Lbl_Sl_Address_2.Text = Convert.ToString(dt.Rows[0]["VCH_SL_ADDRESS_2"]);
                if (Lbl_Sl_Address_2.Text == "")
                {
                    Lbl_Sl_Address_2.Text = "-NA-";
                    Lbl_Sl_Address_2.ForeColor = Color.Red;
                } 
                Lbl_Regd_Country.Text= Convert.ToString(dt.Rows[0]["VCH_REG_COUNTRY_NAME"]);

                if (Lbl_Regd_Country.Text == "")
                {
                    Lbl_Regd_Country.Text = "-NA-";
                    Lbl_Regd_Country.ForeColor = Color.Red;
                }
                Lbl_Sl_Country.Text = Convert.ToString(dt.Rows[0]["VCH_SL_COUNTRY_NAME"]);

                if (Lbl_Sl_Country.Text == "")
                {
                    Lbl_Sl_Country.Text = "-NA-";
                    Lbl_Sl_Country.ForeColor = Color.Red;
                }
                Lbl_Regd_State.Text = Convert.ToString(dt.Rows[0]["VCH_REG_STATE"]);

                if (Lbl_Regd_State.Text == "")
                {
                    Lbl_Regd_State.Text = "-NA-";
                    Lbl_Regd_State.ForeColor = Color.Red;
                }
                Lbl_Sl_State.Text = Convert.ToString(dt.Rows[0]["VCH_SL_STATE"]);

                if (Lbl_Sl_State.Text == "")
                {
                    Lbl_Sl_State.Text = "-NA-";
                    Lbl_Sl_State.ForeColor = Color.Red;
                }
                Lbl_Regd_City.Text = Convert.ToString(dt.Rows[0]["VCH_REG_CITY"]);

                if (Lbl_Regd_City.Text == "")
                {
                    Lbl_Regd_City.Text = "-NA-";
                    Lbl_Regd_City.ForeColor = Color.Red;
                }

                Lbl_Sl_City.Text = Convert.ToString(dt.Rows[0]["VCH_SL_CITY"]);

                if (Lbl_Sl_City.Text == "")
                {
                    Lbl_Sl_City.Text = "-NA-";
                    Lbl_Sl_City.ForeColor = Color.Red;
                }

                Lbl_Regd_Pincode.Text = Convert.ToString(dt.Rows[0]["VCH_REG_PIN"]);

                if (Lbl_Regd_Pincode.Text == "")
                {
                    Lbl_Regd_Pincode.Text = "-NA-";
                    Lbl_Regd_Pincode.ForeColor = Color.Red;
                }

                Lbl_Sl_Pincode.Text = Convert.ToString(dt.Rows[0]["VCH_SL_PIN"]);

                if (Lbl_Sl_Pincode.Text == "")
                {
                    Lbl_Sl_Pincode.Text = "-NA-";
                    Lbl_Sl_Pincode.ForeColor = Color.Red;
                }
                Lbl_Enity_Type.Text = Convert.ToString(dt.Rows[0]["vchEntityName"]);
               if (Lbl_Enity_Type.Text == "Incorporated Company")
                {
                    Div_Cin_Data.Visible = true;
                    Div_Cin.Visible = true;
                    Lbl_CIN_Number.Text= Convert.ToString(dt.Rows[0]["VCH_CIN_NUMBER"]);
                }
                else if(Lbl_Enity_Type.Text == "Limited Liability Partnership")
                {
                    Div_Llpin_Date.Visible = true;
                    Div_Llpin.Visible = true;
                    Lbl_LLPIN_Number.Text = Convert.ToString(dt.Rows[0]["VCH_LLPIN_NUMBER"]);
                }
                if (Lbl_Enity_Type.Text == "")
                {
                    Lbl_Enity_Type.Text = "-NA-";
                    Lbl_Enity_Type.ForeColor = Color.Red;
                }
                if(Lbl_CIN_Number.Text == "")
                {
                    Lbl_CIN_Number.Text = "-NA-";
                    Lbl_CIN_Number.ForeColor = Color.Red;
                }
                if (Lbl_LLPIN_Number.Text == "")
                {
                    Lbl_LLPIN_Number.Text = "-NA-";
                    Lbl_LLPIN_Number.ForeColor = Color.Red;
                }
                string encryptedCIN = Convert.ToString(dt.Rows[0]["VCH_CIN_DATA"]);
                if (!string.IsNullOrEmpty(encryptedCIN))
                {
                    string decryptedCIN = DecryptData(encryptedCIN);
                    Lbl_Cin_Data.Text = decryptedCIN;
                    Lbl_Llpin_Data.Text = decryptedCIN;
                    Lbl_Cin_Data.Visible = false;
                    DownloadLink.Visible = true;
                    Lbl_Llpin_Data.Visible = false;
                    LlpiData_Download.Visible = true;
                }
                else
                {
                    Lbl_Cin_Data.Text = "-NA-";
                    DownloadLink.Visible = false;
                    Lbl_Cin_Data.ForeColor = Color.Red;
                    Lbl_Llpin_Data.Text = "-NA-";
                    LlpiData_Download.Visible = false;
                    Lbl_Llpin_Data.ForeColor = Color.Red;
                }


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
                Lbl_Pan_Holder_Name.Text= Convert.ToString(dt.Rows[0]["VCH_PAN_HOLDER_NAME"]);
                if (Lbl_Pan_Holder_Name.Text == "")
                {
                    Lbl_Pan_Holder_Name.Text = "-NA-";
                    Lbl_Pan_Holder_Name.ForeColor = Color.Red;
                }
                Lbl_Dob.Text= Convert.ToString(dt.Rows[0]["DTM_DOB"]);
                if (Lbl_Dob.Text == "")
                {
                    Lbl_Dob.Text = "-NA-";
                    Lbl_Dob.ForeColor = Color.Red;
                }
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
                if (Lbl_Parent_Unit_Name.Text == "")
                {
                    Lbl_Parent_Unit_Name.Text = "-NA-";                  
                }
                Lbl_Rejection_Cause.Text = Convert.ToString(dt.Rows[0]["VCH_REJECTION_CAUSE"]);

                //empty
                if (Lbl_Rejection_Cause.Text == "" || Lbl_Rejection_Cause.Text == " ")
                {
                    Lbl_Rejection_Cause.Text = "-NA-";
                    Lbl_Rejection_Cause.ForeColor = Color.Red;
                }
                //empty

                Lbl_Approval_Remark.Text = Convert.ToString(dt.Rows[0]["VCH_APPROVALREMARK"]);
                if (Lbl_Approval_Remark.Text == "" || Lbl_Approval_Remark.Text == " ")
                {
                    Lbl_Approval_Remark.Text = "-NA-";
                    Lbl_Approval_Remark.ForeColor = Color.BlueViolet;
                }
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

    private string DecryptData(string encryptedData)
    {
        // Implement decryption logic here
        // Example decryption code (replace with your actual decryption logic)
        byte[] data = Convert.FromBase64String(encryptedData);
        string decryptedData = Encoding.UTF8.GetString(data);
        return decryptedData;
    }

    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndustryList.aspx");
    }  

    //Cin data download button clik
    protected void DownloadLink_Click(object sender, EventArgs e)
    {       
        // Retrieve decrypted CIN data
        string decryptedCIN = Lbl_Cin_Data.Text;

        // Set the content type and headers for the file download
        Response.ContentType = "text/plain";
        Response.AppendHeader("Content-Disposition", "attachment; filename=CIN_Data.txt");

        // Write the decrypted data directly to the response stream
        Response.Write(decryptedCIN);
        Response.End();

    }

    //Llpin data download button clik
    protected void LlpiData_Download_Click(object sender, EventArgs e)
    {
       
        // Retrieve decrypted CIN data
        string decryptedCIN = Lbl_Llpin_Data.Text;

        // Set the content type and headers for the file download
        Response.ContentType = "text/plain";
        Response.AppendHeader("Content-Disposition", "attachment; filename=LLPIN_Data.txt");

        // Write the decrypted data directly to the response stream
        Response.Write(decryptedCIN);
        Response.End();
    }
}