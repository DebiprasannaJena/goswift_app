using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Investor;
using System.Data;
using System.Drawing;
using System.Configuration;
using EntityLayer.GrievanceEntity;

public partial class Portal_Grievance_GrievanceApplicationDetails : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    string strGrivId = "";
    int intRequestId = 0;

    ////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["GrivId"] != null)
            {
                strGrivId = Convert.ToString(Request.QueryString["GrivId"]);
                intRequestId = Convert.ToInt32(Request.QueryString["RequestId"]);

                if (!IsPostBack)
                {
                    fillAppDetails(strGrivId);
                }


                if(intRequestId==5)
                {
                    Btn_Back.Enabled = false;
                }
                else
                {
                    Btn_Back.Enabled = true;
                }
            }

            //if (Request.QueryString["val"] != null)
            //{
            //    string[] strArr = new string[2];
            //    strArr = Request.QueryString["val"].Split('~');

            //    intInvetsorId = Convert.ToInt32(strArr[0]);
            //    intRequestId = Convert.ToInt32(strArr[1]);

            //    if (!IsPostBack)
            //    {
            //        fillDetails(intInvetsorId, intRequestId);
            //    }

            //    //// intRequestId=1 means Request coming from User_Enquire.aspx Page and M_INVESTOR_DETAILS data to be dispalyed.
            //    //// intRequestId=2 means Request coming from User_Approval.aspx Page and M_INVESTOR_DETAILS data to be dispalyed.
            //    //// intRequestId=3 means Request coming from User_Enquire.aspx Page and T_INVESTOR_DETAILS_LOG (Rejected Investor Details) data to be dispalyed.
            //    //// intRequestId=4 means Request coming from User_Approval_1st_Level.aspx Page and M_INVESTOR_DETAILS data to be dispalyed.
            //    //// intRequestId=5 means Request coming from View_Investor_Transaction_Details Page and T_GRIEVANCE_APPLICATION data to be dispalyed.
            //}
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }

    }

    #region FunctionUsed

    ////// Fill Investor Details

    private void fillAppDetails(string strGrivId)
    {
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objEntity = new GrievanceEntity();

        DataSet ds = new DataSet();

        try
        {
            objEntity.StrAction = "VAD";
            objEntity.strGrivId = strGrivId;

            /////// Select Data
            ds = objBAL.ViewGrivApplicationDetails(objEntity);

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
                Lbl_Industry_type.Text= Convert.ToString(ds.Tables[0].Rows[0]["intIndustryCategory"]); // For Industry and non industry type . by Anil sahoo

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
            ds = null;
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)//satya added
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
        if (intRequestId == 1)
        {
            Response.Redirect("GrievanceTakeAction.aspx");
        }
        else if (intRequestId == 2)
        {
            Response.Redirect("ViewGrievanceDetails.aspx");
        }
        else if (intRequestId == 3) // for grievance help desk  Track page add anil sahoo
        {
            Response.Redirect("TrackGrievanceAppStatus.aspx");
        }        
    }
}