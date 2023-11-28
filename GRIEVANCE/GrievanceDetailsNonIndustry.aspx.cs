using EntityLayer.GrievanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class GRIEVANCE_GrievanceDetailsNonIndustry : SessionCheck
{
    ////// Page Load  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/LogOut.aspx", true);
        }

        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["StrGrievanceNo"] != null)
                {
                    string strGrivId = Convert.ToString(Request.QueryString["StrGrievanceNo"]);
                    fillAppDetails(strGrivId);
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "GrievanceDetails");
            }
        }
    }

    #region FunctionUsed

    ////// Fill Investor Details

    private void fillAppDetails(string strGrivId)
    {
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objEntity = new GrievanceEntity();

        try
        {
            objEntity.StrAction = "VAD";
            objEntity.strGrivId = strGrivId;

            /////// Select Data
            DataSet ds = objBAL.ViewGrivApplicationDetails(objEntity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Lbl_Griv_Id.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchGrivId"]);
                Lbl_Apply_Date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtmCreatedOn"].ToString()).ToString("dd-MMM-yyyy HH:mm tt");

                Lbl_Applicant_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchApplicantName"]);
                Lbl_Designation.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchDesignation"]);
                Lbl_Mobile_No.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchMobileNo"]);
                Lbl_Griv_Title.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchGrivTitle"]);
                Lbl_Griv_Details.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchGrivDetail"]);
                Lbl_District.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchDistrictName"]);
                Lbl_Griv_Type.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchGrivType"]);
                Lbl_Griv_Sub_Type.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchGrivSubType"]);
                Lbl_Investment_Level.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchInvestLevel"]);
                Lbl_Email.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchEmail"]);
                Lbl_Company_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCH_INV_NAME"]);
                Lbl_Industry_Type.Text = Convert.ToString(ds.Tables[0].Rows[0]["intIndustryCategory"]);// Add For display Industry Type. by Anil sahoo
                /*------------------------------------------------------------------------------------------------*/
                ////Attachment-1
                /*------------------------------------------------------------------------------------------------*/
                if (Convert.ToString(ds.Tables[0].Rows[0]["vchAttachment1"]) != "")
                {
                    Hyp_Attachment_1.NavigateUrl = "~/Grievance/Attachment/" + Convert.ToString(ds.Tables[0].Rows[0]["vchAttachment1"]);
                    Hyp_Attachment_1.Visible = true;
                    Lbl_Doc_Attachment_1.Text = "";
                    Lbl_Doc_Attachment_1.Visible = false;
                }
                else
                {
                    Hyp_Attachment_1.NavigateUrl = "";
                    Hyp_Attachment_1.Visible = false;
                    Lbl_Doc_Attachment_1.Text = "-NA-";
                    Lbl_Doc_Attachment_1.Visible = true;
                }

                /*------------------------------------------------------------------------------------------------*/
                ////Attachment-2
                /*------------------------------------------------------------------------------------------------*/
                if (Convert.ToString(ds.Tables[0].Rows[0]["vchAttachment2"]) != "")
                {
                    Hyp_Attachment_2.NavigateUrl = "~/Grievance/Attachment/" + Convert.ToString(ds.Tables[0].Rows[0]["vchAttachment2"]);
                    Hyp_Attachment_2.Visible = true;
                    Lbl_Doc_Attachment_2.Text = "";
                    Lbl_Doc_Attachment_2.Visible = false;
                }
                else
                {
                    Hyp_Attachment_2.NavigateUrl = "";
                    Hyp_Attachment_2.Visible = false;
                    Lbl_Doc_Attachment_2.Text = "-NA-";
                    Lbl_Doc_Attachment_2.Visible = true;
                }

                /*------------------------------------------------------------------------------------------------*/
                Lbl_Status.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchStatusName"]);
                int intStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["intStatus"]);

                if (intStatus == 4)////In Progress
                {
                    Lbl_Status.ForeColor = Color.Orange;
                }
                else if (intStatus == 3)////Rejected
                {
                    Lbl_Status.ForeColor = Color.Red;
                }
                else if (intStatus == 8 || intStatus == 13) ////Forwaded or Approved
                {
                    Lbl_Status.ForeColor = Color.Green;
                }

                /*------------------------------------------------------------------------------------------------*/

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtmActionDate"]) != "")
                {
                    Lbl_Action_Date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtmActionDate"].ToString()).ToString("dd-MMM-yyyy HH:mm tt");
                }

                /*------------------------------------------------------------------------------------------------*/
                ///// Display Action History
                /*------------------------------------------------------------------------------------------------*/
                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
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

    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Ref_Doc_Name = (HiddenField)e.Row.FindControl("Hid_Ref_Doc_Name");
            Label Lbl_Ref_Doc = (Label)e.Row.FindControl("Lbl_Ref_Doc");
            HyperLink Hyp_Ref_Doc = (HyperLink)e.Row.FindControl("Hyp_Ref_Doc");

            /////// For Reference File
            if (Hid_Ref_Doc_Name.Value == "")
            {
                Lbl_Ref_Doc.Text = "NA";
                Lbl_Ref_Doc.ForeColor = System.Drawing.Color.Red;
                Hyp_Ref_Doc.Visible = false;
            }
            else
            {
                Hyp_Ref_Doc.ToolTip = Hid_Ref_Doc_Name.Value;
                Hyp_Ref_Doc.Target = "_Blank";
                Hyp_Ref_Doc.NavigateUrl = "~/Portal/Grievance/ApprovalDocGriv/" + Hid_Ref_Doc_Name.Value;
            }
        }
    }

    ////// Button Back     
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("../GrievanceNonIndustry.aspx");
    }
}