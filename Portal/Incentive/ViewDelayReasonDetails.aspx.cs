/*--'*******************************************************************************************************************
' File Name         : ViewDelayReason.aspx
' Description       : View details of Delay Reason
' Created by        : Gouri Shankar Chhotray
' Created On        : 15 Dec 2017
' Modification History:
' Procedure used    : USP_INCT_EC_DELAY_VIEW
' Table Used        : T_INCT_EC_DELAY_REASON

'   <CR no.>                          <Date>                <Modified by>        <Modification Summary>                   <Instructed By>        

'   *********************************************************************************************************************-
*/

using System;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using System.Data;

public partial class Portal_Incentive_ViewDelayReasonDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillData();
    }

    private void fillData()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();
        DataSet ds = new DataSet();

        try
        {
            objEntity.strAction = "B";
            objEntity.INT_DELAY_ID = Request.QueryString["Did"] == null ? 0 : int.Parse(Request.QueryString["Did"].ToString());

            ds = objBAL.Inct_EC_Delay_Reason_VIEW(objEntity);
            /*--------------------------------------------------------------------*/
            Lbl_Industry_Code.Text = ds.Tables[0].Rows[0]["vchIndustryCode"].ToString();
            Lbl_Enterprise_Name.Text = ds.Tables[0].Rows[0]["vchEnterpriseName"].ToString();
            Lbl_Unit_Cat.Text = ds.Tables[0].Rows[0]["vchUnitCat"].ToString();
            Lbl_Unit_Type.Text = ds.Tables[0].Rows[0]["vchUnitType"].ToString();
            Lbl_FFCI_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[0].Rows[0]["dtmFFCI"]));
            Lbl_Prod_Comm.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[0].Rows[0]["dtmProdComm"]));
            Lbl_Created_On.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[0].Rows[0]["dtmCreatedOn"]));
            /*--------------------------------------------------------------------*/
            string strApproveDate = Convert.ToString(ds.Tables[0].Rows[0]["dtmApprovalDate"]);
            if (strApproveDate != "")
            {
                Lbl_Approval_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strApproveDate));
            }
            else
            {
                Lbl_Approval_Date.Text = "-NA-";
            }
            /*--------------------------------------------------------------------*/
            Lbl_Status.Text = ds.Tables[0].Rows[0]["vchStatus"].ToString();
            string strStatus = ds.Tables[0].Rows[0]["intStatus"].ToString();

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

            Lbl_Delay_Reason.Text = ds.Tables[0].Rows[0]["vchReason"].ToString();

            /*--------------------------------------------------------------------*/
            string strRemark = ds.Tables[0].Rows[0]["vchRemark"].ToString();
            if (strRemark != "")
            {
                Lbl_Remark.Text = strRemark;
            }
            else
            {
                Lbl_Remark.Text = "-NA-";
            }
            /*--------------------------------------------------------------------*/
            string strTimeAllowed = ds.Tables[0].Rows[0]["intTimeAllowed"].ToString();
            if (strTimeAllowed != "")
            {
                Lbl_Time_Allowed.Text = strTimeAllowed + " Month(s)";
            }
            else
            {
                Lbl_Time_Allowed.Text = "-NA-";
            }
            /*--------------------------------------------------------------------*/
            string strECLetter = ds.Tables[0].Rows[0]["vchECLetter"].ToString();
            if (strECLetter != "")
            {
                Lbl_EC_Letter.Visible = false;
                Lbl_EC_Letter.Text = "";
                Hy_EC_Letter.Visible = true;
                Hy_EC_Letter.NavigateUrl = "../../Incentives/Files/InctEcDelayDoc/" + strECLetter;
            }
            else
            {
                Lbl_EC_Letter.Visible = true;
                Lbl_EC_Letter.Text = "-NA-";
                Hy_EC_Letter.Visible = false;
                Hy_EC_Letter.NavigateUrl = "#";
            }

            ////// Supporting Document
            if (ds.Tables[1].Rows.Count > 0)
            {
                Grd_Application.DataSource = ds.Tables[1];
                Grd_Application.DataBind();
            }
            else
            {
                Grd_Application.DataSource = null;
                Grd_Application.DataBind();
            }
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
}