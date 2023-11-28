using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ServiceModel;

using System.Web.Services;
using System.IO;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Collections.Specialized;
using System.Text;

public partial class TreasuryResponseold : System.Web.UI.Page
{
    bool smsStatus;
    bool mailStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            ProjectInfo objProject = new ProjectInfo();
            ProposalBAL objService = new ProposalBAL();
            //CommonProperties gCommonProperties = new CommonProperties();
            string lreqId = string.Empty;
            string lOrdNo = string.Empty;
            System.Text.StringBuilder displayValues = new System.Text.StringBuilder();
            System.Collections.Specialized.NameValueCollection
                postedValues = Request.Form;
            String nextKey;
            String lStrMsg = string.Empty;
            for (int i = 0; i < postedValues.AllKeys.Length; i++)
            {
                nextKey = postedValues.AllKeys[i];
                if (nextKey.Substring(0, 2) != "__")
                {
                    if (i != 0)
                    {
                        displayValues.Append("&");
                    }
                    displayValues.Append(nextKey);
                    displayValues.Append("=");
                    displayValues.Append(postedValues[i]);
                    if (nextKey == "otherParameters")
                    {
                        lreqId = postedValues[i].ToString().Substring(postedValues[i].ToString().IndexOf('=') + 1, (postedValues[i].ToString().IndexOf('!') - postedValues[i].ToString().IndexOf('=') - 1));
                        lOrdNo = postedValues[i].Split(new[] { "!~!" }, StringSplitOptions.None)[1].Split('=')[1].ToString();
                    }
                    if (nextKey == "config")
                    {

                    }
                    if (nextKey == "challanAmount")
                    {
                        objProject.vchChallanAmt = postedValues[i].ToString();
                    }
                    if (nextKey == "bankTransactionId")
                    {
                        objProject.vchbankTransactionId = postedValues[i].ToString();
                    }
                    if (nextKey == "challanRefId")
                    {
                        objProject.vchbankchallanRefId = postedValues[i].ToString();
                    }
                    if (nextKey == "bankTransactionStatus")
                    {
                        objProject.vchbankTransactionStatus = postedValues[i].ToString();
                    }
                    if (nextKey == "bankTransactionStatus")
                    {
                        objProject.vchbankTransactionStatus = postedValues[i].ToString();
                    }
                    if (nextKey == "bankTransTimeStamp")
                    {
                        objProject.vchbankTransTimeStamp = postedValues[i].ToString();
                    }
                    if (nextKey == "ReqID")
                    {
                        objProject.vchReqID = lreqId;
                    }
                    lStrMsg = lStrMsg + "|" + nextKey + "|" + postedValues[i].ToString();
                }
            }
            Session["ResponseMessage"] = lStrMsg;
            string[] Fstrmsg = lStrMsg.ToString().Split('|');
            string chalanAmt = Fstrmsg[4].ToString();
            string BnkTransid = Fstrmsg[6].ToString();
            string ChallanRefid = Fstrmsg[8].ToString();
            string BnkTranStatus = Fstrmsg[10].ToString();
            string BnkTranTimestamp = Fstrmsg[12].ToString();
            string refid = string.Empty;
            objProject.ReqID = lreqId;
            objProject.OrderNo = lOrdNo;


            objProject.strAction = "U";
            objProject.vchOrderNo = objProject.ReqID.ToString();
            objProject.vchChallanAmt = chalanAmt;
            objProject.vchbankTransactionId = BnkTransid;
            objProject.vchbankchallanRefId = ChallanRefid;
            objProject.vchbankTransactionStatus = BnkTranStatus;
            objProject.vchbankTransTimeStamp = BnkTranTimestamp;
            objProject.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            string Retval=objService.AddProposalDtls(objProject);
            if (Retval.ToString().Split('_')[0] == "2")
            {
                if (BnkTranStatus == "S")
                {
                    Img1.Visible = true;
                    Img2.Visible = false;
                    lblPaymentStatus.Text = "Your Payment Successful";
                    lblchallanAmt.Text = chalanAmt;
                    lblchallanrefid.Text = ChallanRefid;
                    lblTrancId.Text = BnkTransid;
                    CommonHelperCls comm = new CommonHelperCls();
                    List<LandDet> objProjList = new List<LandDet>();
                    List<LandDet> objProjList1 = new List<LandDet>();
                    LandDet objProp = new LandDet();
                    objProp.strAction = "P";
                    objProp.intCreatedBy =Convert.ToInt32(Retval.ToString().Split('_')[1]);
                    objProp.ApplicationNo = Retval.ToString().Split('_')[2];
                    objProjList = objService.GETMobileNoOfUser(objProp).ToList();

                    objProp.strAction = "D";
                    objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"].ToString());
                    objProp.ApplicationNo = Retval.ToString().Split('_')[2];
                    objProjList1 = objService.GETMobileNoOfUser(objProp).ToList();

                    string mobile = "";
                    string mobilei = "";
                    string smsContent = "";
                    string[] toEmail = new string[1];
                    if (objProjList.Count > 0)
                    {
                        mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                        mobilei = Convert.ToString(objProjList1[0].MobileNo.ToString().Split('_')[0]);
                        smsContent = Convert.ToString(objProjList[0].SMSContent);
                        toEmail[0] = Convert.ToString(objProjList[0].Email);
                        //toEmail[1] = Convert.ToString(objProjList1[0].Email);
                        string strSubject = "SWP: Application submitted successfully";
                        string strBody = "The application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " has been submitted successfully.Which is pending at " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[2]) + ", This is for your information.Please Log into https://invest.odisha.gov.in for further details.";
                        //comm.sendMail(strSubject, strBody, toEmail, true);
                        //comm.SendSms(mobile, strBody);
                        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                        smsStatus = comm.SendSmsNew(mobile, strBody);
                        toEmail[0] = Convert.ToString(objProjList1[0].Email);
                        string strSubject1 = "SWP: Application submitted successfully";
                        string strBody1 = "The application for New Proposal of M/s " + Convert.ToString(objProjList1[0].MobileNo.ToString().Split('_')[1]) + " has been submitted successfully.Which is pending at " + Convert.ToString(objProjList1[0].MobileNo.ToString().Split('_')[2]) + ", This is for your information.Please Log into https://invest.odisha.gov.in for further details.";
                        mailStatus = comm.sendMail(strSubject1, strBody1, toEmail, true);
                        smsStatus = comm.SendSmsNew(mobilei, strBody1);
                        // FOR SMS and Mail Status Update

                        string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["InvestorId"].ToString(), "1053", 1, Retval.ToString().Split('_')[2], strBody, strBody, smsStatus, mailStatus);
                        // FOR SMS and Mail Status Update
                    }
                    
                }
                else
                {
                    lblPaymentStatus.Text = "Failure";
                    Img2.Visible = true;
                    Img1.Visible = false;
                }
            }
            else
            {
                Img1.Visible = false;
                lblPaymentStatus.Text = "Failure";
                Img2.Visible = true;
            }
            if (Session["ReferId"] != null)
            {
                //gCommonProperties.optionalParm = Session["ReferId"].ToString();
            }
            //string fpath = Session["redirect"].ToString();
            HttpContext _context = HttpContext.Current;
            //_context.Items.Add("DTIReturns", gCommonProperties);
            //Server.Execute("~/" + fpath);

        }
    }
}