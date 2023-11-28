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
using System.Security.Cryptography;

public partial class TreasuryResponseNew : System.Web.UI.Page
{
    bool smsStatus;
    bool mailStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            ProjectInfo objProject = new ProjectInfo();
            ProposalBAL objService = new ProposalBAL();
            string lreqId = string.Empty;
            string lOrdNo = string.Empty;
            System.Text.StringBuilder displayValues = new System.Text.StringBuilder();
            System.Collections.Specialized.NameValueCollection
                postedValues = Request.Form;
            String nextKey;
            for (int i = 0; i < postedValues.AllKeys.Length; i++)
            {
                nextKey = postedValues.AllKeys[i];
                if (nextKey == "msg")
                {
                    string s = postedValues[0];
                    string deresult = decrypt(s);
                    string[] result = deresult.Split('|');
                    string bchecksumdata = "";
                    for (int j = 0; j < result.Length - 1; j++)
                    {
                        bchecksumdata += result[j] + "|";
                        if (j == 1)
                        {
                            lOrdNo = result[j];
                            lreqId = result[j].ToString().Split('-')[0];
                        }
                        if (j == 20)
                        {
                            objProject.vchChallanAmt = result[j].ToString();
                        }
                        if (j == 39)
                        {
                            objProject.vchbankTransactionId = result[j].ToString();
                        }
                        if (j == 36)
                        {
                            objProject.vchbankchallanRefId = result[j].ToString();
                        }
                        if (j == 40)
                        {
                            objProject.vchbankTransactionStatus = result[j].ToString();
                        }
                        if (j == 40)
                        {
                            objProject.vchbankTransactionStatus = result[j].ToString();
                        }
                        if (j == 42)
                        {
                            objProject.vchbankTransTimeStamp = result[j].ToString();
                        }
                        if (j == 1)
                        {
                            objProject.vchReqID = lreqId;
                        }
                    }
                    bchecksumdata = bchecksumdata.Remove(bchecksumdata.Length - 1, 1);
                    string checksumvalue = result[result.Length - 1].ToString();
                    string convchecksumvalue = HmacSHA256(bchecksumdata, "");
                    if (checksumvalue == convchecksumvalue)
                    {
                        Session["ResponseMessage"] = bchecksumdata;
                        string[] Fstrmsg = bchecksumdata.ToString().Split('|');
                        string chalanAmt = Fstrmsg[20].ToString();
                        string BnkTransid = Fstrmsg[39].ToString();
                        string ChallanRefid = Fstrmsg[36].ToString();
                        string BnkTranStatus = Fstrmsg[40].ToString();
                        string BnkTranTimestamp = Fstrmsg[42].ToString();
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
                        string Retval = objService.AddProposalDtls(objProject);
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
                                objProp.intCreatedBy = Convert.ToInt32(Retval.ToString().Split('_')[1]);
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
                    }
                }
            }            
            if (Session["ReferId"] != null)
            {
                
            }            
            HttpContext _context = HttpContext.Current;          
        }
    }
    private byte[] GetKey()
    {
        byte[] byteKey = File.ReadAllBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        return byteKey;
    }

    private static byte[] GetFileBytes(String filename)
    {
        if (!File.Exists(filename))
            return null;
        Stream stream = new FileStream(filename, FileMode.Open);
        int datalen = (int)stream.Length;
        byte[] filebytes = new byte[datalen];
        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(filebytes, 0, datalen);
        stream.Close();
        return filebytes;
    }

    public String decrypt(String value)
    {
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateDecryptor();
        byte[] plain = Convert.FromBase64String(value);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        String encryptedText = Encoding.UTF8.GetString(cipher);
        return encryptedText;
    }

    private string HmacSHA256(string message, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key"); //encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }
}