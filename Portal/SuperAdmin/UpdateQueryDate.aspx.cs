using BusinessLogicLayer.Proposal;
using BusinessLogicLayer.Service;
using EntityLayer.Proposal;
using EntityLayer.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_SuperAdmin_UpdateQueryDate : System.Web.UI.Page
{
   
    string strRetval = "";
  
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/portal/SessionRedirect.aspx", false);
                return;
            }
           
            if (Convert.ToInt32(Session["UserId"]) != 1)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (IsPostBack)
            {
                
                Div_PealDetails.Visible = false;
                DivQueryUpdateDetails.Visible = false;
             
                txt_Pealnorecord.Text ="";
                Txt_ServicenoRecord.Text = "";
             

            }
            else
            {
                DivPeal.Visible = false;
                DivService.Visible = false;
            }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "UpdateQuery");
        }
    }

   
    // Function to get the details of peal query data
    private void ViewPealQueryUpdateDate(string strproposalno)
    {
        try
        {
            ProposalDet objProposal = new ProposalDet();
            ProposalBAL objService = new ProposalBAL();            
            objProposal.strProposalNo = strproposalno;
            DataTable dt = objService.QueryDateUpdate(objProposal);
            if (dt.Rows.Count > 0)
            {

                DivQueryUpdateDetails.Visible = true;
                Lbl_Proposal_No.Text = dt.Rows[0]["vchProposalNo"].ToString();
                Lbl_Query_Date.Text = dt.Rows[0]["DTM_RAISE_QUERY"].ToString();
                Lbl_timesofquery_Raise.Text = dt.Rows[0]["INT_CONFIG_QUERY_TIMES"].ToString();

            }
            else
            {
                DivQueryUpdateDetails.Visible = false;
                divNorecordPeal.Visible = true;
                txt_Pealnorecord.Text = "No Record Found";
                /// ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid Proposal Number.</strong>');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    // Function to get the details of service query data 
    private void ViewServiceQueryUpdateDate(string strapplicationno) 
    {
        try
        {
            ServiceDetails objServiceDet = new ServiceDetails();
            ServiceBusinessLayer Serviceobj = new ServiceBusinessLayer();           
            objServiceDet.strApplicationUnqKey = strapplicationno;
            DataTable dt = Serviceobj.ViewQueryDateUpdate(objServiceDet);

            if (dt.Rows.Count > 0)
            {
                Div_PealDetails.Visible = true;
                Lbl_Sevice_No.Text = dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString();
                Lbl_Service_Query_Raise_Date.Text = dt.Rows[0]["DTM_RAISE_QUERY"].ToString();
                Lbl_Service_Times_Query.Text = dt.Rows[0]["INT_CURRENT_QUERY_TIMES"].ToString();
            }
            else
            {
                Div_PealDetails.Visible = false;
                divNorecordService.Visible = true;
                Txt_ServicenoRecord.Text = "No Record Found";
               
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }



    protected void Rdbtn_Select_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           

            if (Rdbtn_Select_Type.SelectedItem.Value == "1")
            {
                DivPeal.Visible = true;
                DivService.Visible = false;
                Txt_Proposal_No.Text = "";
                Txt_Service_No.Text = "";


            }
            else if(Rdbtn_Select_Type.SelectedItem.Value == "2")
            {
                DivService.Visible = true;
                DivPeal.Visible = false;
                Txt_Service_No.Text = "";
                Txt_Proposal_No.Text = "";
            }
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
    }
    //Search By Proposal No
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {

            if (Txt_Proposal_No.Text.Trim() == "")
            {
                Txt_Proposal_No.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "jAlert('<strong>Enter proposal number.</strong>')", true);
                return;
            }
            else
            {
                ViewPealQueryUpdateDate(Txt_Proposal_No.Text);
            }
            
           
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "UpdateQuery");
        }
    }
    //  Update Peal QueryDate details
    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        try
        {
            ProposalDet objProposal = new ProposalDet();
            ProposalBAL objService = new ProposalBAL();
            objProposal.strAction = "UQ";
            objProposal.strProposalNo = Lbl_Proposal_No.Text;
            objProposal.VCH_RAISE_QUERY = Txt_Peal_Query_Date.Text;
            strRetval = objService.UpdateQueryDate(objProposal);
            if (strRetval == "2")
            {

                DivQueryUpdateDetails.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Proposal Query Date updated successfully.</strong>');", true);

            }
            else if (strRetval == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something went wrong,Please try again.</strong>');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateQuery");
        }
    }
    // Reset Peal Update Date
    protected void Btn_Reset_Click(object sender, EventArgs e) 
    {
        Txt_Proposal_No.Text = "";
    }
    //Cancel Peal Update Date Details
    protected void Btn_Cancel_Click(object sender, EventArgs e) 
    {
        Lbl_Proposal_No.Text= "";
        Lbl_Query_Date.Text = "";
        Lbl_timesofquery_Raise.Text = "";
        Txt_Peal_Query_Date.Text = "";
    }

    //Search By Service Application No.
    protected void Btn_Search_Service_Click(object sender, EventArgs e) 
    {
        try
        {
            if (Txt_Service_No.Text.Trim() == "")
            {
                Txt_Service_No.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "jAlert('<strong> Enter Service number.</strong>')", true);
                return;
            }
            else
            {
                ViewServiceQueryUpdateDate(Txt_Service_No.Text);
            }

        }
        catch(Exception ex)
        {
            Util.LogError(ex, "UpdateQuery");

        }
    }
    //Update Service QueryDate 
    protected void Btn_Service_Update_Click(object sender, EventArgs e) 
    {
        try
        {
            ServiceDetails objServiceDet = new ServiceDetails();
            ServiceBusinessLayer Serviceobj = new ServiceBusinessLayer();
            objServiceDet.strAction = "SQU";
            objServiceDet.strApplicationUnqKey = Lbl_Sevice_No.Text;
            objServiceDet.QueryRasied = Txt_Service_Query_Date.Text;
            strRetval = Serviceobj.ServiceUpdateQueryDate(objServiceDet);
            if (strRetval == "2")
            {

                Div_PealDetails.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Service Query Date updated successfully.</strong>');", true);

            }
            else if (strRetval == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something went wrong,Please try again.</strong>');", true);
                return;
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateQuery");
        }
    }

    //Cancel Service  Update Date Details
    protected void Btn_Service_Cancel_Click(object sender, EventArgs e) 
    {
        Lbl_Sevice_No.Text = "";
        Lbl_Service_Query_Raise_Date.Text = "";
        Lbl_Service_Times_Query.Text = "";
        Txt_Service_Query_Date.Text = "";

    }
    //Reset Service Update Date
    protected void Btn_Reset_Service_Click(object sender, EventArgs e) 
    {
        Txt_Service_No.Text = "";
    }
}