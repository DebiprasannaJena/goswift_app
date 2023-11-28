//******************************************************************************************************************
// File Name             :   View_EC_Application_Status.aspx.cs
// Description           :   To View Empowered Committee Application Status and Details
// Created by            :   Sushant Kumar Jena
// Created on            :   13th Dec 2017
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
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data;

public partial class incentives_View_EC_Application_Status : SessionCheck
{
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();

    ///// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("incentiveoffered.aspx");
        }

        if (!IsPostBack)
        {
            Div_Details.Visible = false;
            fillGrid();
        }
    }

    ///// Function Used
    #region Function Used

    ///// Bind Gridview
    private void fillGrid()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strAction = "B";

            ds = objBAL.Inct_EC_Delay_Reason_VIEW(objEntity);

            ViewState["dataset"] = ds;

            Grd_Application.DataSource = ds.Tables[0];
            Grd_Application.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
        }
    }

    #endregion

    ///// Gridview Application RowDatBound
    protected void Grd_Application_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Lbl_Approval_Date_G = (Label)e.Row.FindControl("Lbl_Approval_Date_G");
                Label Lbl_Time_Allowed_G = (Label)e.Row.FindControl("Lbl_Time_Allowed_G");

                if (Lbl_Approval_Date_G.Text == "")
                {
                    Lbl_Approval_Date_G.Text = "-";
                    Lbl_Time_Allowed_G.Text = "-";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    ///// Gridview Linkbutton Click to View Application Details
    protected void LnkBtn_Details_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();

        try
        {
            Div_Details.Visible = true;

            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Sl_No = (HiddenField)row.FindControl("Hid_Sl_No");

            ds = (DataSet)ViewState["dataset"];

            ///// Filter Common Data
            ds.Tables[0].DefaultView.RowFilter = "intDelayId = '" + Hid_Sl_No.Value + "'";
            dt1 = (ds.Tables[0].DefaultView).ToTable();

            ///// Filter Supporting Document Data
            ds.Tables[1].DefaultView.RowFilter = "intDelayId = '" + Hid_Sl_No.Value + "'";
            dt2 = (ds.Tables[1].DefaultView).ToTable();

            if (dt1.Rows.Count > 0)
            {
                Lbl_Industry_Code.Text = dt1.Rows[0]["vchIndustryCode"].ToString();
                Lbl_Enterprise_Name.Text = dt1.Rows[0]["vchEnterpriseName"].ToString();
                Lbl_Unit_Cat.Text = dt1.Rows[0]["vchUnitCat"].ToString();
                Lbl_Unit_Type.Text = dt1.Rows[0]["vchUnitType"].ToString();
                Lbl_FFCI_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dt1.Rows[0]["dtmFFCI"]));
                Lbl_Prod_Comm.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dt1.Rows[0]["dtmProdComm"]));
                Lbl_Created_On.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dt1.Rows[0]["dtmCreatedOn"]));

                /*--------------------------------------------------------------------*/
                string strApproveDate = Convert.ToString(dt1.Rows[0]["dtmApprovalDate"]);
                if (strApproveDate != "")
                {
                    Lbl_Approval_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strApproveDate));
                }
                else
                {
                    Lbl_Approval_Date.Text = "-NA-";
                }
                /*--------------------------------------------------------------------*/
                Lbl_Status.Text = dt1.Rows[0]["vchStatus"].ToString();
                string strStatus = dt1.Rows[0]["intStatus"].ToString();

                if (strStatus == "1")
                {
                    Lbl_Status.ForeColor = System.Drawing.Color.Orange;
                }
                else if (strStatus == "2")
                {
                    Lbl_Status.ForeColor = System.Drawing.Color.Green;
                }
                else if (strStatus == "3")
                {
                    Lbl_Status.ForeColor = System.Drawing.Color.Red;
                }

                Lbl_Delay_Reason.Text = dt1.Rows[0]["vchReason"].ToString();

                /*--------------------------------------------------------------------*/
                string strRemark = dt1.Rows[0]["vchRemark"].ToString();
                if (strRemark != "")
                {
                    Lbl_Remark.Text = strRemark;
                }
                else
                {
                    Lbl_Remark.Text = "-NA-";
                }
                /*--------------------------------------------------------------------*/
                string strTimeAllowed = dt1.Rows[0]["intTimeAllowed"].ToString();
                if (strTimeAllowed != "")
                {
                    Lbl_Time_Allowed.Text = strTimeAllowed + " Month(s)";
                }
                else
                {
                    Lbl_Time_Allowed.Text = "-NA-";
                }
                /*--------------------------------------------------------------------*/
                string strECLetter = dt1.Rows[0]["vchECLetter"].ToString();
                if (strECLetter != "")
                {
                    Lbl_EC_Letter.Visible = false;
                    Hy_EC_Letter.Visible = true;
                    Hy_EC_Letter.NavigateUrl = "../Incentives/Files/InctEcDelayDoc/" + strECLetter;
                }
                else
                {
                    Lbl_EC_Letter.Visible = true;
                    Lbl_EC_Letter.Text = "-NA-";
                    Hy_EC_Letter.Visible = false;
                    Hy_EC_Letter.NavigateUrl = "#";
                }
            }

            /*--------------------------------------------------------------------*/
            ///// Bind Supporting Documents

            Grd_Document.DataSource = dt2;
            Grd_Document.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ds = null;
            dt1 = null;
            dt2 = null;
        }
    }
    ///// Gridview Documents RowDatBound for View Documents
    protected void Grd_Document_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_File_Name = (HiddenField)e.Row.FindControl("Hid_File_Name");
            HyperLink Hyp_View_Doc = (HyperLink)e.Row.FindControl("Hyp_View_Doc");
            Hyp_View_Doc.NavigateUrl = "../incentives/Files/InctEcDelayDoc/" + Hid_File_Name.Value;
        }
    }
}