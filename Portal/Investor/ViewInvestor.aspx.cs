#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   ViewInvestor.aspx.cs
// Description           :   View details of Investor
// Created by            :   AMit Sahoo
// Created On            :   16th August 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//                              1                       25-Sept-2017            Pranay Kumar                        Addition of Industry Code as per dicussion with Deepak Sir
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
#endregion

public partial class Investor_ViewInvestor :SessionCheck
{
    #region Variable_Declarations
    string result = "";
    string strmobile = "";
    string strname = "";
   
    InvestorBusinessLayer objService = new InvestorBusinessLayer();
    InvestorDetails objInvestor = new InvestorDetails();  
    InvestorRegistration objInvService = new InvestorRegistration();
    int intRetVal = 0;
    int count = 0; string strSubject = ""; string SMSContent = ""; bool smsStatus; bool mailStatus;
    string strprojname = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            FillInvestorInfo();
        }
    }
    #endregion

    #region Button_Click Events
 
    protected void lbtnStatusApprove_Click(object sender, EventArgs e)
    {
        
        string[] InvtoEmail = new string[1];
        LinkButton btn = (LinkButton)(sender);
        string InvID = btn.CommandArgument;
        int intinvid = Convert.ToInt32(InvID);
        TextBox TxtRemark = btn.FindControl("TxtRemark") as TextBox;
        
               objInvestor.strAction ="V";
               objInvestor.intInvId = Convert.ToInt32(InvID);

            List<InvestorDetails> objListinv = objService.ViewInvestorDetailsToInsertInSSO(objInvestor).ToList();
            string StrOut = "";
            if (objListinv.Count > 0)
            {
                try
                {
                 SSOService.ValidateService objSSO = new SSOService.ValidateService();                 
                
                 StrOut = objSSO.NewIndustryRegistration("A", objListinv[0].strEmail, objSSO.URLEncryption(objListinv[0].strPassword), objListinv[0].strIndName, objListinv[0].strSecAnswer, objListinv[0].strPanno, objListinv[0].strSiteAddress, objListinv[0].strAddress, objListinv[0].MobNo, objListinv[0].intSectorId, objListinv[0].intSubSectorId, objListinv[0].intCategoryId, objListinv[0].intDistrictId, objListinv[0].intBlockId, objListinv[0].decInvstAmount, "", objSSO.URLEncryption(System.Configuration.ConfigurationManager.AppSettings["SSOKey"].ToString()), "", 0);
                 
                    if (StrOut.Contains(","))
                    {
                     if (StrOut.Split(',')[0].ToString() == "1")
                     {
                         string StrRemark = TxtRemark.Text.ToString();
                         string strAction = "APV";
                         result = objService.InvApprovalDetails(objInvestor, strAction, intinvid, StrRemark);
                         string strInvestorUId = StrOut.Split(',')[1].ToString();
                         //Added By Pranay Kumar on 25-Sept-2017 for Addition of Industry Code as per dicussion with Deepak Sir
                         string strIndustryCode = StrOut.Split(',')[2].ToString();
                         //Ended By Pranay Kumar on 25-Sept-2017 for Addition of Industry Code as per dicussion with Deepak Sir
                         string struaction = "UC";
                         objService.strUpdateUniueInvId(struaction, InvID, strInvestorUId, strIndustryCode);
                         CommonHelperCls comm = new CommonHelperCls();
                         strmobile = result.Split(';')[1].ToString();
                         strname = result.Split(';')[2].ToString();
                         objInvestor.strAction = "A";
                         DataTable dtcontent = objInvService.GetSMSContent(objInvestor);
                         objInvestor.strAction="LD";
                         objInvestor.IntInvestorId = intinvid;
                         DataTable dtlogdetails = objService.GetInvestorLoginDetails(objInvestor);
                         if (dtcontent.Rows.Count > 0)
                         {
                             strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                             SMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[USERID]", dtlogdetails.Rows[0]["VCH_EMAIL"].ToString());                                                  
                             SMSContent = SMSContent.Replace("[UNITNAME]", dtlogdetails.Rows[0]["VCH_INV_NAME"].ToString());                             
                             InvtoEmail[0] = dtlogdetails.Rows[0]["VCH_EMAIL"].ToString();
                            
                             mailStatus = comm.sendMail(strSubject, SMSContent, InvtoEmail, true);
                         }
                        
                         smsStatus = comm.SendSmsNew("" + dtcontent.Rows[0]["vchSMSContent"].ToString() + " ", SMSContent);
                          comm.UpdateMailSMSStaus("InvestorApproved", strmobile, InvtoEmail[0], strSubject, "0", "0", 0, "0", SMSContent, SMSContent, smsStatus, mailStatus);
                       
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Investor Profile Approved Successfully !', '" + strprojname + "'); </script>", false);
                         FillInvestorInfo();
                     }
                     else if (StrOut.Split(',')[0].ToString() == "2")
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Service Error!', '" + strprojname + "'); </script>", false);
                         FillInvestorInfo();
                     }
                     else
                     {
                         ScriptManager.RegisterStartupScript(this, GetType(), "OnClick", "<script> jAlert('Service Error!', '" + strprojname + "'); </script>", false);
                         FillInvestorInfo();
                     }
                    }
                }
                catch(Exception ex)
                {
                throw ex.InnerException;
                }              
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Service Error!', '" + strprojname + "'); </script>", false);
                FillInvestorInfo();
            }
            
    }
    public static bool sendMail(string strSubject, string strBody, string[] toEmail, bool enbleSSl)
    {
        bool Res = false;
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smptp"].ToString());
            mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"].ToString());
            for (int i = 0; i < toEmail.Length; i++)
            {
                mail.To.Add(toEmail[i]);
            }
            mail.Subject = strSubject;
            strBody += "<br>From,<br>Single Window Portal<br>-------------------------------------------------------------------<br>This is a system generated Mail.Please don't Reply to this mail.<br>";
            mail.Body = strBody;

            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = enbleSSl;
            mail.IsBodyHtml = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            SmtpServer.Send(mail);
            Res = true;
        }
        catch
        {
            Res = false;
        }
        return Res;

    }
    protected void lbtnStatusReject_Click(object sender, EventArgs e)
    {
        
        string[] InvtoEmail = new string[1];
        LinkButton btn = (LinkButton)(sender);
        string InvID = btn.CommandArgument;
        int intinvid = Convert.ToInt32(InvID);
        TextBox TxtRemark = btn.FindControl("TxtRemark") as TextBox;
        string StrRejRemark = TxtRemark.Text.ToString();
        string strAction = "REJ";
        result = objService.InvApprovalDetails(objInvestor, strAction, intinvid, StrRejRemark);
        CommonHelperCls comm = new CommonHelperCls();
        strmobile = result.Split(';')[1].ToString();
        strname = result.Split(';')[2].ToString();
        objInvestor.strAction = "R";
        DataTable dtcontent = objInvService.GetSMSContent(objInvestor);
        DataTable dtlogdetails = objService.GetInvestorLoginDetails(objInvestor);
        if (dtcontent.Rows.Count > 0)
        {
            strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
            SMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[USERID]", dtlogdetails.Rows[0]["VCH_EMAIL"].ToString());
            SMSContent = SMSContent.Replace("[UNITNAME]", dtlogdetails.Rows[0]["VCH_INV_NAME"].ToString());         
            InvtoEmail[0] = dtlogdetails.Rows[0]["VCH_EMAIL"].ToString();
          
            mailStatus = comm.sendMail(strSubject, SMSContent, InvtoEmail, true);
        }
     
        smsStatus = comm.SendSmsNew("" + dtcontent.Rows[0]["vchSMSContent"].ToString() + " ", SMSContent);
        comm.UpdateMailSMSStaus("InvestorRejected", strmobile, InvtoEmail[0], strSubject, "0", "0", 0, "0", SMSContent, SMSContent, smsStatus, mailStatus);
   
        if (result.Split(';')[0].ToString() == "2")
        {
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Investor Profile Rejected Successfully !', '" + strprojname + "'); </script>", false);
            FillInvestorInfo();
        }
       
    }
    #endregion

    #region Fill_Grid
    public void FillInvestorInfo()
    {
        
            objInvestor.strAction = "VI";
            List<InvestorDetails> objList = objService.ViewInvestorDetailsPortal(objInvestor).ToList();
           
            if (objList.Count > 0)
            {
                gvInvestor.DataSource = objList;
                gvInvestor.DataBind();
                intRetVal = gvInvestor.Rows.Count;
                count = objList.Count;
                DisplayPaging();               
               
            }
      
    }

   
    #endregion
    
    protected void gvInvestor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvestor.PageIndex = e.NewPageIndex;
        FillInvestorInfo();
    }
    protected void gvInvestor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[0].Text = Convert.ToString((this.gvInvestor.PageIndex * this.gvInvestor.PageSize) + e.Row.RowIndex + 1);
        }

        for (int i = 0; i < gvInvestor.Rows.Count; i++)
        {

            Label LblAppRejStatus = (Label)gvInvestor.Rows[i].FindControl("LblAppRejStatus");
            Label LblVerfied = (Label)gvInvestor.Rows[i].FindControl("LblVerfied");
            
            if (LblAppRejStatus.Text == "2")
            {
                ((LinkButton)gvInvestor.Rows[i].FindControl("lbtnStatusReject")).Visible = false;
                ((LinkButton)gvInvestor.Rows[i].FindControl("lbtnStatusApprove")).Visible = false;
                ((Label)gvInvestor.Rows[i].FindControl("LblAppRejStatus")).Visible = true;
                ((Label)gvInvestor.Rows[i].FindControl("LblAppRejStatus")).Text = "Rejected";
                ((TextBox)gvInvestor.Rows[i].FindControl("TxtRemark")).Visible = false;
                ((Label)gvInvestor.Rows[i].FindControl("LblRemarks")).Visible = true;
            }
            else if (LblAppRejStatus.Text == "1")//Approved
            {
                ((LinkButton)gvInvestor.Rows[i].FindControl("lbtnStatusApprove")).Visible = false;
                ((LinkButton)gvInvestor.Rows[i].FindControl("lbtnStatusReject")).Visible = false;
                ((Label)gvInvestor.Rows[i].FindControl("LblAppRejStatus")).Visible = true;
                ((Label)gvInvestor.Rows[i].FindControl("LblAppRejStatus")).Text = "Approved";
                ((TextBox)gvInvestor.Rows[i].FindControl("TxtRemark")).Visible = false;
                ((Label)gvInvestor.Rows[i].FindControl("LblRemarks")).Visible = true;
            }
            if (LblVerfied.Text == "1")
            {
                ((Image)gvInvestor.Rows[i].FindControl("ImgApprStatus")).Visible = true;
                ((Image)gvInvestor.Rows[i].FindControl("ImgApprStatus")).ImageUrl = "~/images/verification-symbol.png";
            }
            else
            {
                ((Image)gvInvestor.Rows[i].FindControl("ImgApprStatus")).Visible = true;
                ((Image)gvInvestor.Rows[i].FindControl("ImgApprStatus")).ImageUrl = "~/images/cancel-square.png";
            }
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvInvestor.PageIndex = 0;
            gvInvestor.AllowPaging = false;
            FillInvestorInfo();
        }
        else
        {
            lbtnAll.Text = "All";
            gvInvestor.AllowPaging = true;
            FillInvestorInfo();
        }
    }
    #region "DISPLAYPAGEING"
    private void DisplayPaging()
    {
        if (gvInvestor.Rows.Count > 0)
        {
            this.LblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvInvestor.PageIndex + 1 == gvInvestor.PageCount)
            {
                this.LblPaging.Text = "Results <b>" + gvInvestor.Rows[0].Cells[0].Text + "</b> - <b>" + intRetVal + "</b> Of <b>" + count + "</b>";
            }
            else
            {
                this.LblPaging.Text = "Results <b>" + gvInvestor.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvInvestor.Rows[0].Cells[0].Text) + (gvInvestor.PageSize - 1)) + "</b> Of <b>" + count + "</b>";
            }
        }
        else
        {
            this.LblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }

    #endregion

}