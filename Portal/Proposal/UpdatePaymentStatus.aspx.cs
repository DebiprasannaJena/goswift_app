/*
 * Created By : Ritika Lath
 * Created On : 16th March 2018
 * Class Name : Portal_Proposal_UpdatePaymentStatus
 * File Name : UpdatePaymentStatus.aspx.cs
 * Description : To update the payment of pending and applied PEAL
 */

using System;
using System.Collections.Generic;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Portal_Proposal_UpdatePaymentStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //get all proposal that are pending
            BindProposalDropDown();
        }
    }

    private void BindProposalDropDown()
    {
        PealPaymentBAL objPealPaymentBAL = new PealPaymentBAL();
        List<PEAL_Update_Payment_Entity> lstEntity = new List<PEAL_Update_Payment_Entity>();
        lstEntity = objPealPaymentBAL.Peal_UpdatePaymentStatus_View("p", string.Empty);
        ddlProposalNo.DataSource = lstEntity;
        ddlProposalNo.DataTextField = "strProposalNo";
        ddlProposalNo.DataValueField = "strProposalNo";
        ddlProposalNo.DataBind();
        ddlProposalNo.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlProposalNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblInvestorName.Text = string.Empty;
        lblAppliedDate.Text = string.Empty;
        if (ddlProposalNo.SelectedIndex > 0)
        {
            PealPaymentBAL objPealPaymentBAL = new PealPaymentBAL();
            List<PEAL_Update_Payment_Entity> lstEntity = new List<PEAL_Update_Payment_Entity>();
            lstEntity = objPealPaymentBAL.Peal_UpdatePaymentStatus_View("pd", ddlProposalNo.SelectedValue);

            if (lstEntity.Count > 0)
            {
                lblInvestorName.Text = lstEntity[0].strInvestorName;
                lblAppliedDate.Text = lstEntity[0].strAppliedDate;
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        PEAL_Update_Payment_Entity objEntity = new PEAL_Update_Payment_Entity();
        objEntity.intCreatedBy = Convert.ToInt32(Session["userid"]);
        objEntity.strProposalNo = ddlProposalNo.SelectedValue;
        objEntity.strBankTransactionId = txtBankTransactionId.Text.Trim();
        objEntity.strChallanRefId = txtChallanRefId.Text.Trim();
        objEntity.decChallanAmt = string.IsNullOrEmpty(txtChallanAmount.Text.Trim()) ? 0.00M : Convert.ToDecimal(txtChallanAmount.Text.Trim());
        objEntity.strOrderNo = txtOrderNo.Text.Trim();
        objEntity.intReqId = string.IsNullOrEmpty(txtRequestId.Text.Trim()) ? 0 : Convert.ToInt32(txtRequestId.Text.Trim());
        objEntity.strPaymentDate = txtDate.Text.Trim();
        objEntity.strAction = "add";
        PealPaymentBAL objPealPaymentBAL = new PealPaymentBAL();
        int intRetValue = objPealPaymentBAL.Peal_UpdatePaymentStatus_AED(objEntity);
        if (intRetValue == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('Payment for proposal no - " + ddlProposalNo.SelectedValue + " updated successfully!');", true);
            BindProposalDropDown();
            txtOrderNo.Text = string.Empty;
            txtRequestId.Text = string.Empty;
            txtChallanRefId.Text = string.Empty;
            txtChallanAmount.Text = string.Empty;
            txtBankTransactionId.Text = string.Empty;
            txtDate.Text = string.Empty;
            lblAppliedDate.Text = string.Empty;
            lblInvestorName.Text = string.Empty;
        }
        else if (intRetValue == 5)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('Invalid Order no or Required Id!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('Error Occurred');", true);
        }
    }
}