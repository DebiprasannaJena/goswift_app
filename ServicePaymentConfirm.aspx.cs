using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
public partial class ServicePaymentConfirm : System.Web.UI.Page
{
    SWPNEWSERV.MiningVerificationService proxy = new SWPNEWSERV.MiningVerificationService();
    DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
    ProjectInfo objProject = new ProjectInfo();
    string strpaymentNo, strChlRef, strBTN, returnval, PaymentNo = "";
    int intAMT = 0;
    bool smsStatus;
    bool mailStatus;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        ProposalBAL objService = new ProposalBAL();
        strChlRef = txtChallanRefNo.Text.Trim();
        //strChlRef = "27CB006E76";
        strBTN = TxtTransactionID.Text.Trim();
        //strBTN = "CKD5028010";
        intAMT = Convert.ToInt32(txtChallanAmt.Text);
        //intAMT = 1985200
        PaymentNo = txtPaymentNo.Text;
        //PaymentNo = 91714;
        //List<ProjectInfo> objServicelist = new List<ProjectInfo>();
        //objProject.strAction = "S5";
        //objProject.vchOrderNo = txtPaymentNo.Text;
        //objServicelist = objService.GetProposalDtls(objProject).ToList();

        if (Request.QueryString["reqid"] != null)
        {
            returnval = proxy.getStatus(strChlRef, strBTN, PaymentNo, Convert.ToInt32(Request.QueryString["reqid"].ToString()), intAMT);
            if (returnval == "S")
            {
                objData.UpdatePaymentStatus(txtPaymentNo.Text, strBTN, strChlRef, 1,PaymentNo);

                //Response.Write("Success");
                ScriptManager.RegisterStartupScript(BtnSave, this.GetType(), "OnClick", "<script>jAlert('Payment Success!');</script>", false);
            }
            else
            {

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('PAYMENT CONFIRMATION FAILED !!\n\n This has occured for one of the following reasons :\n\n 1. You have entered incorrect Challan No. or Bank Transaction No. \n 2. You are trying to confirm with diffrent Request No. \n 3. Payment confirmation not yet received from bank. \n 4. The given information are not available with treasury.\n\n PLEASE TRY AFTER SOME TIME...\n You can check payment status @ www.swp.gov.in.','cc'); </script>", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('PAYMENT CONFIRMATION FAILED !!This has occured for one of the following reasons :1. You have entered incorrect Challan No. or Bank Transaction No. 2. You are trying to confirm with diffrent Payment No.3. Payment confirmation not yet received from bank. 4. The given information are not available with treasury. PLEASE TRY AFTER SOME TIME...You can check payment status', '" + Messages.TitleOfProject + "', function () {location.href = 'Proposals.aspx';}); </script>", false);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('PAYMENT CONFIRMATION FAILED !!\n\n This has occured for one of the following reasons :\n\n 1. You have entered incorrect Challan No. or Bank Transaction No. \n 2. You are trying to confirm with diffrent Request No. \n 3. Payment confirmation not yet received from bank. \n 4. The given information are not available with treasury.\n\n PLEASE TRY AFTER SOME TIME...\n You can check payment status @ www.odishatreasury.gov.in.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(BtnSave, this.GetType(), "OnClick", "<script>jAlert('Invalid Request ID!');</script>", false);
        }
       
    }
}