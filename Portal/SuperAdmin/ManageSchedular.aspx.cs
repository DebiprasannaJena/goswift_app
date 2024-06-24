using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_SuperAdmin_ManageSchedular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///// This page can only be accessed by goadmin.
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Convert.ToInt32(Session["UserId"]) != 1)
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    /// <summary>
    /// To run payment scheduler for SERVICE.
    /// This method is used to run payment scheduler for SERVICE instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnServicePaymentSched_Click(object sender, EventArgs e)
    {
        PaymentScheduler obj = new PaymentScheduler();
        obj.ServicePaymentSchedule();
        string strAlertMsg = "<script>alert('Service Payment Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    /// <summary>
    /// To run payment scheduler for PEAL.
    /// This method is used to run payment scheduler for PEAL instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnPealPaymentSched_Click(object sender, EventArgs e)
    {
        PaymentScheduler obj = new PaymentScheduler();
        obj.PealPaymentSchedule();
        string strAlertMsg = "<script>alert('PEAL Payment Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    /// <summary>
    /// To run scheduler for OSPCB.
    /// This method is used to run the OSPCB scheduler instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnOSPCBSched_Click(object sender, EventArgs e)
    {
        ServiceStatus cmnSvc = new ServiceStatus();
        cmnSvc.AutoPCBStatusUpdate();
        string strAlertMsg = "<script>alert('OSPCB Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    /// <summary>
    /// To run scheduler for CMS-ORTPSA.
    /// This method is used to run the CMS-ORTPSA scheduler instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCmsOrtpsaSched_Click(object sender, EventArgs e)
    {
        CmsOrtpsaPosting objCms = new CmsOrtpsaPosting();
        objCms.SendServiceInfoToCMS();
        string strAlertMsg = "<script>alert('CMS-ORTPSA Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    /// <summary>
    /// To run scheduler for Monthly Mail
    /// This method will redirect you to mail configuration page where you can configure and run monthly mail scheduler instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnMonthlyMail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/portal/DashBoard/ManageWeeklyMail.aspx");
    }

    #region NSWS-Section   

    /// <summary>
    /// To run scheduler for NSWS-Push Redirection URL.
    /// This method is used to run the NSWS-Push Redirection URL scheduler instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNswsPushRedirectUrl_Click(object sender, EventArgs e)
    {
        NSWSScheduler objNsws = new NSWSScheduler();
        objNsws.UpdateRedirectionURL();
        string strAlertMsg = "<script>alert('NSWS-Push Redirection Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    /// <summary>
    /// To run scheduler for NSWS-Push License/Application Status.
    /// This method is used to run the NSWS-Push License/Application Status scheduler instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNswsPushLicenseStatus_Click(object sender, EventArgs e)
    {
        NSWSScheduler objNsws = new NSWSScheduler();
        objNsws.PushLicenseStatus();
        string strAlertMsg = "<script>alert('NSWS-Push License Status Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    /// <summary>
    /// To run scheduler for NSWS-Push Query Status.
    /// This method is used to run the NSWS-Push Query Status scheduler instantly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNswsPushQuery_Click(object sender, EventArgs e)
    {
        NSWSScheduler objNsws = new NSWSScheduler();
        objNsws.PushQueryStatus();
        string strAlertMsg = "<script>alert('NSWS-Push Query Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    /// <summary>
    /// To run scheduler for NSWS-Push Approval Document.
    /// This method is used to run the NSWS-Push Approval Document scheduler instantly. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNswsPushDoc_Click(object sender, EventArgs e)
    {
        NSWSScheduler objNsws = new NSWSScheduler();
        objNsws.PushDocument();
        string strAlertMsg = "<script>alert('NSWS-Push Document Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    protected void BtnNswsPushDwh_Click(object sender, EventArgs e)
    {
        NSWSScheduler objNsws = new NSWSScheduler();
        objNsws.PushInvestorToDWH();
        string strAlertMsg = "<script>alert('DWH - Push Investor To DWH.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }

    #endregion

    /// <summary>
    /// To run payment status send scheduler for external SERVICE 2.0 .
    /// This method is used to run payment scheduler for external sevice 2.0 SERVICE instantly.
    /// add by anil
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void BtnExternalServicePaymentSched_Click(object sender, EventArgs e) 
    //{
    //    PaymentScheduler obj = new PaymentScheduler();
    //    obj.ExternalServicePaymentSchedule();
    //    string strAlertMsg = "<script>alert('External Service Payment Scheduler status send Completed.')</script>";
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    //}

    protected void Btn_Mosarkar_Scheduler_Click(object sender, EventArgs e)
    {
        PaymentScheduler obj = new PaymentScheduler();
        obj.MoSarkarServiceSchedule();
        string strAlertMsg = "<script>alert('MoSarkar Scheduler Completed.')</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", strAlertMsg, false);
    }
}