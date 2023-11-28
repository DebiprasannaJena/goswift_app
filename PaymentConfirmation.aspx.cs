using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
public partial class PaymentConfirmation : System.Web.UI.Page
{
    SWPNEWSERV.MiningVerificationService proxy = new SWPNEWSERV.MiningVerificationService();
    ProjectInfo objProject = new ProjectInfo();
    string strChlRef, strBTN, returnval,PaymentNo = "";
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
       
        strBTN = TxtTransactionID.Text.Trim();
       
        intAMT = Convert.ToInt32(txtChallanAmt.Text);
       
        PaymentNo = txtPaymentNo.Text;
        
       
        objProject.strAction = "S5";
        objProject.vchOrderNo = txtPaymentNo.Text;
        List<ProjectInfo> objServicelist = objService.GetProposalDtls(objProject).ToList();
        ViewState["vchReqid"] = objServicelist[0].vchReqID;
        returnval = proxy.getStatus(strChlRef, strBTN, PaymentNo, Convert.ToInt16(ViewState["vchReqid"]), intAMT);
       
        if (returnval == "S")
        {
            objProject.strAction = "PU";
            objProject.vchOrderNo = PaymentNo.ToString();
            string Retval = objService.AddProposalDtls(objProject);

            CommonHelperCls comm = new CommonHelperCls();
           
            LandDet objProp = new LandDet();
            objProp.strAction = "P";
            objProp.intCreatedBy = Convert.ToInt32(Retval.ToString().Split('_')[1]);
            objProp.ApplicationNo = Retval.ToString().Split('_')[2];

            List<LandDet> objProjList = objService.GETMobileNoOfUser(objProp).ToList();
            string mobile = "";
         
            string[] toEmail = new string[1];
            if (objProjList.Count > 0)
            {
                mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
              
                toEmail[0] = Convert.ToString(objProjList[0].Email);
                string strSubject = "SWP: Application submitted successfully";
                string strBody = "The application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " has been submitted successfully.Which is pending at " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[2]) + ", This is for your information.Please Log into https://invest.odisha.gov.in for further details.";

                

                mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                smsStatus = comm.SendSmsNew(mobile, strBody);
                // FOR SMS and Mail Status Update

                string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["InvestorId"].ToString(), "1053", 1, Retval.ToString().Split('_')[2], strBody, strBody, smsStatus, mailStatus);
                // FOR SMS and Mail Status Update
            }
           
            ScriptManager.RegisterStartupScript(BtnSave, this.GetType(), "OnClick", "<script>jAlert('Success');</script>", false);
        }
        else
        {
           
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('PAYMENT CONFIRMATION FAILED !!This has occured for one of the following reasons :1. You have entered incorrect Challan No. or Bank Transaction No. 2. You are trying to confirm with diffrent Payment No.3. Payment confirmation not yet received from bank. 4. The given information are not available with treasury. PLEASE TRY AFTER SOME TIME...You can check payment status', '" + Messages.TitleOfProject + "', function () {location.href = 'Proposals.aspx';}); </script>", false);
           
        }
    }

  
}