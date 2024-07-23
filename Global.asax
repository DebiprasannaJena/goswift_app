<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.Security.Cryptography.X509Certificates" %>
<%@ Import Namespace="System.Net.Security" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="EntityLayer.Service" %>
<%@ Import Namespace="BusinessLogicLayer.Service" %>
<%@ Import Namespace="BusinessLogicLayer.HelpDesk" %>
<%@ Import Namespace="EntityLayer.Proposal" %>
<%@ Import Namespace="BusinessLogicLayer.Proposal" %>
<%@ Import Namespace="System.Data" %>
<script RunAt="server">
    ProposalBAL objService = new ProposalBAL();
    CommonHelperCls comm = new CommonHelperCls();
    bool smsStatus;
    bool mailStatus;

    bool smsStatus1;
    bool mailStatus1;
    string strSubjectMsg = "Ticket No#-[ISSUENO] | [CATEGORY] | GOSWIFT-HelpDesk | Escalated";
    string strSubjectMsgT="[CATEGORY]| Escalated";

    void Application_Start(object sender, EventArgs e)
    {
        //       try
        //       {
        //           System.Timers.Timer timScheduledTask = new System.Timers.Timer();
        //           // Timer interval is set in miliseconds,
        //           // In this case, we'll run a task every minute
        //           timScheduledTask.Interval = 60 * 1000 * 10;  //for local
        //           // timScheduledTask.Interval = 1000 * 60 * 60 * 12;  //for live
        //           timScheduledTask.Enabled = true;
        //           // Add handler for Elapsed event
        //           timScheduledTask.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledTask_Elapsed);
        //       }
        //       catch
        //       {
        //       }

        /*-------------------------------------------------------------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------*/

        //       try
        //       {

        //           System.Timers.Timer timScheduledMail = new System.Timers.Timer();
        //           // Timer interval is set in miliseconds,
        //           // In this case, we'll run a task every minute
        //           timScheduledMail.Interval = 60 * 1000 * 1;  //for local
        //           // timScheduledMail.Interval = 1000 * 60 * 60 * 24 * 7;  //for live
        //           timScheduledMail.Enabled = true;
        //           // Add handler for Elapsed event
        //           timScheduledMail.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledMail_Elapsed);
        //       }
        //       catch
        //       {
        //       }

        /*-------------------------------------------------------------------------------------------------------*/
        /*-------------------------------------------------------------------------------------------------------*/
        /*----------------------------Added by radhika------------------------------*/

        //       try
        //       {
        //           System.Timers.Timer timScheduledTaskEsc = new System.Timers.Timer();
        //           timScheduledTaskEsc.Interval = 60 * 1000 * 10;
        //           timScheduledTaskEsc.Enabled = true;
        //           timScheduledTaskEsc.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledTaskEsc_Elapsed);
        //       }
        //       catch
        //       {
        //       }

        /*--------------------------Ended by radhika--------------------------------*/

        /*----------------------------Added by radhika for PCB------------------------------*/

        //       System.Timers.Timer timScheduledTaskPCB = new System.Timers.Timer();
        //       timScheduledTaskPCB.Interval = 60 * 1000 * 60*2;
        //       timScheduledTaskPCB.Enabled = true;
        //       timScheduledTaskPCB.Elapsed +=
        //      new System.Timers.ElapsedEventHandler(timScheduledTaskPCB_Elapsed);
        ///*--------------------------Ended by radhika--------------------------------*/


        /*-------------------------------------------------------------------------------------------------------*/
        /////// Payment Scheduler for Service
        /*-------------------------------------------------------------------------------------------------------*/
        //       try
        //       {
        //           System.Timers.Timer timScheduledOrderPaymentService = new System.Timers.Timer();
        //           timScheduledOrderPaymentService.Interval = 60 * 1000 * 60;
        //           timScheduledOrderPaymentService.Enabled = true;
        //           timScheduledOrderPaymentService.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledOrderPaymentService_Elapsed);
        //       }
        //       catch
        //       {
        //       }

        /*-------------------------------------------------------------------------------------------------------*/
        /////// Payment Scheduler for External Service add by  anil
        /*-------------------------------------------------------------------------------------------------------*/
        //       try
        //       {
        //           System.Timers.Timer timScheduledOrderPaymentService = new System.Timers.Timer();
        //           timScheduledOrderPaymentService.Interval = 60 * 1000 * 60;
        //           timScheduledOrderPaymentService.Enabled = true;
        //           timScheduledOrderPaymentService.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledOrderPaymentExternalService_Elapsed);
        //       }
        //       catch
        //       {
        //       }


         /*-------------------------------------------------------------------------------------------------------*/
        /////// Peal Data Scheduler for Mosarkar Service add by  Debi
        /*-------------------------------------------------------------------------------------------------------*/
        //       try
        //       {
        //           System.Timers.Timer timScheduledPealDataPushToMosarkar = new System.Timers.Timer();
        //           timScheduledPealDataPushToMosarkar.Interval = 60 * 1000 * 60;
        //           timScheduledPealDataPushToMosarkar.Enabled = true;
        //           timScheduledPealDataPushToMosarkar.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledPealDataPushToMosarkar_Elapsed);
        //       }
        //       catch
        //       {
        //       }


        /*-------------------------------------------------------------------------------------------------------*/
        /////// Payment Scheduler for PEAL
        /*-------------------------------------------------------------------------------------------------------*/
        //       try
        //       {

        //           System.Timers.Timer timScheduledOrderPaymentPEAL = new System.Timers.Timer();
        //           timScheduledOrderPaymentPEAL.Interval = 60 * 1000 * 90;
        //           timScheduledOrderPaymentPEAL.Enabled = true;
        //           timScheduledOrderPaymentPEAL.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledOrderPaymentPEAL_Elapsed);
        //       }
        //       catch
        //       {
        //       }

        /*-------------------------------------------------------------------------------------------------------*/
        ///// Scheduler for Updation of EIN/PC Document from AIM portal
        /*-------------------------------------------------------------------------------------------------------*/
        //try
        //{
        //    System.Timers.Timer timScheduledDocumentAIM = new System.Timers.Timer();
        //    timScheduledDocumentAIM.Interval = 60 * 1000 * 1;
        //    timScheduledDocumentAIM.Enabled = true;
        //    timScheduledDocumentAIM.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledDocumentAIM_Elapsed);
        //}
        //catch
        //{
        //}

        /*-------------------------------------------------------------------------------------------------------*/
        ///// Scheduler for Pushing proposals to CMS (Central Monitoring System) portal for ORTPSA tracking.        
        /*-------------------------------------------------------------------------------------------------------*/
        //try
        //{
        //    System.Timers.Timer timCmsOrtpsa = new System.Timers.Timer();
        //    timCmsOrtpsa.Interval = 60 * 1000 * 1;
        //    timCmsOrtpsa.Enabled = true;
        //    timCmsOrtpsa.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledCmsOrtpsa_Elapsed);
        //}
        //catch
        //{
        //}


        /*-------------------------------------------------------------------------------------------------------*/
        ///// Scheduler for Pushing Redirection URL to the NSWS Portal.       
        /*-------------------------------------------------------------------------------------------------------*/
        //try
        //{
        //    System.Timers.Timer timNswsRedirectURL = new System.Timers.Timer();
        //    timNswsRedirectURL.Interval = 60 * 1000 * 1;
        //    timNswsRedirectURL.Enabled = true;
        //    timNswsRedirectURL.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledNSWSRedirectionUrl_Elapsed);
        //}
        //catch
        //{
        //}

        /*-------------------------------------------------------------------------------------------------------*/
        ///// Scheduler for Pushing Application Status to NSWS Portal.      
        /*-------------------------------------------------------------------------------------------------------*/
        //try
        //{
        //    System.Timers.Timer timNswsAppStatus = new System.Timers.Timer();
        //    timNswsAppStatus.Interval = 60 * 1000 * 1;
        //    timNswsAppStatus.Enabled = true;
        //    timNswsAppStatus.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledNSWSApplicationStatus_Elapsed);
        //}
        //catch
        //{
        //}

        /*-------------------------------------------------------------------------------------------------------*/
        ///// Scheduler for Pushing Query Status to NSWS Portal.      
        /*-------------------------------------------------------------------------------------------------------*/
        //try
        //{
        //    System.Timers.Timer timQueryStatus = new System.Timers.Timer();
        //    timQueryStatus.Interval = 60 * 1000 * 1;
        //    timQueryStatus.Enabled = true;
        //    timQueryStatus.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledNSWSQueryStatus_Elapsed);
        //}
        //catch
        //{
        //}

        /*-------------------------------------------------------------------------------------------------------*/
        ///// Scheduler for Pushing Approval Document to NSWS Portal.      
        /*-------------------------------------------------------------------------------------------------------*/
        //try
        //{
        //    System.Timers.Timer timAppDoc = new System.Timers.Timer();
        //    timAppDoc.Interval = 60 * 1000 * 1;
        //    timAppDoc.Enabled = true;
        //    timAppDoc.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledNSWSPushAppDoc_Elapsed);
        //}
        //catch
        //{
        //}


        /*-------------------------------------------------------------------------------------------------------*/
        ///// Scheduler for Pushing NSWS Investor Regestration data to DWH  .      
        /*-------------------------------------------------------------------------------------------------------*/
        //try
        //{
        //    System.Timers.Timer timAppDoc = new System.Timers.Timer();
        //    timAppDoc.Interval = 60 * 1000 * 1;
        //    timAppDoc.Enabled = true;
        //    timAppDoc.Elapsed += new System.Timers.ElapsedEventHandler(timScheduledNSWSPushToDWH_Elapsed);
        //}
        //catch
        //{
        //}
    }

    void timScheduledNSWSRedirectionUrl_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        //NSWSScheduler objNsws = new NSWSScheduler();
        //objNsws.UpdateRedirectionURL();         
    }
    void timScheduledNSWSApplicationStatus_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        //NSWSScheduler objNsws = new NSWSScheduler();
        //objNsws.PushLicenseStatus();       
    }
    void timScheduledNSWSQueryStatus_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        //NSWSScheduler objNsws = new NSWSScheduler();
        //objNsws.PushQueryStatus();
    }
    void timScheduledNSWSPushAppDoc_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        //NSWSScheduler objNsws = new NSWSScheduler();
        //objNsws.PushDocument(); 
    }
    void timScheduledNSWSPushToDWH_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        //NSWSScheduler objNsws = new NSWSScheduler();
        //objNsws.PushInvestorToDWH(); 
    }
    void timScheduledTaskPCB_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        ServiceStatus cmnSvc = new ServiceStatus();
        cmnSvc.AutoPCBStatusUpdate();
    }
    void timScheduledTaskEsc_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        bool status = true;
        bool smsStatus = true;
        bool smsStatus1 = true;
        bool MailStatus = true;
        bool MailStatus1 = true;
        List<IssueRegistration> RTNlST = new List<IssueRegistration>();
        IssueRegistration obj1 = new IssueRegistration();
        obj1.Action = "ES";
        HelpDeskBusinessLayer objServiceEntity = new HelpDeskBusinessLayer();
        RTNlST = objServiceEntity.AutoEscalationProcess(obj1);
        if (RTNlST.Count() > 0)
        {
            for (int i = 0; i < RTNlST.Count; i++)
            {
                //string[] UtoEmail = new string[1];
                //UtoEmail[0] = RTNlST[i].vchUserEmail.ToString();
                //status = objt.sendMail("Issue Raised", "A complaint escalated to you", UtoEmail, true);
                strSubjectMsg = strSubjectMsg.Replace("[ISSUENO]", RTNlST[i].vchIssueNo);
                strSubjectMsg = strSubjectMsg.Replace("[CATEGORY]", RTNlST[i].CategoryName);
                strSubjectMsg = strSubjectMsg.Replace("[Description]", RTNlST[i].vch_IssueDetails);
                strSubjectMsg = strSubjectMsg.Replace("[Investor]", RTNlST[i].vch_investName);
                strSubjectMsgT=strSubjectMsgT.Replace("[CATEGORY]",RTNlST[i].CategoryName);
                string[] toEmail = new string[1];
                toEmail[0] = RTNlST[i].Email.ToString();
                if (RTNlST[i].Email.Contains(','))
                {
                    string[] RAPhoneArry = RTNlST[i].VchMobile.Split(',');
                    string[] RAEmailArry = RTNlST[i].Email.Split(',');
                    for (int i1 = 0; RAEmailArry.Length > i1; i1++)
                    {
                        if (RAEmailArry[i1] != "" && RAEmailArry[i1] != null)
                        {
                            toEmail[0] = RAEmailArry[i1].ToString();

                            MailStatus1 = comm.sendMail(strSubjectMsg, RTNlST[i].vchEmailContent.Replace("[Investor]", RTNlST[i].vch_investName).Replace("[Description]", RTNlST[i].vch_IssueDetails).Replace("[ISSUENO]", RTNlST[i].vchIssueNo), toEmail, true);//Repoting authority
                            string str = comm.UpdateMailSMSStaus(strSubjectMsgT, RAPhoneArry[i1], RAEmailArry[i1], "Issue Escalated", "1", "1", 1, "1", RTNlST[i].vchEmailContent.Replace("[Investor]", RTNlST[i].vch_investName).Replace("[Description]", RTNlST[i].vch_IssueDetails).Replace("[ISSUENO]", RTNlST[i].vchIssueNo), RTNlST[i].vchMobileContent.ToString().Replace("[ISSUENO]", RTNlST[i].vchIssueNo), smsStatus1, MailStatus1);

                            //MailStatus1 = comm.sendMail(strSubjectMsg, RTNlST[i].vchEmailContent.Replace("[ISSUENO]", RTNlST[i].vchIssueNo), toEmail, true);//Repoting authority
                            //string str = comm.UpdateMailSMSStaus(strSubjectMsg, RAPhoneArry[i1], RAEmailArry[i1], "Issue Escalated", "1", "1", 1, "1", RTNlST[i].vchEmailContent.Replace("[ISSUENO]", RTNlST[i].vchIssueNo), RTNlST[i].vchMobileContent.ToString().Replace("[ISSUENO]", RTNlST[i].vchIssueNo), smsStatus1, MailStatus1);
                        }
                        if (RAPhoneArry[i1] != "" && RAPhoneArry[i1] != null)
                        {
                            smsStatus1 = comm.SendSmsNew(RAPhoneArry[i1].ToString(), RTNlST[i].vchMobileContent.Replace("[ISSUENO]", RTNlST[i].vchIssueNo));
                        }
                        i1 = i1++;
                    }
                }
                else
                {
                    toEmail[0] = RTNlST[i].Email.ToString();
                    MailStatus1 = comm.sendMail(strSubjectMsg, RTNlST[i].vchEmailContent.Replace("[Investor]", RTNlST[i].vch_investName).Replace("[Description]", RTNlST[i].vch_IssueDetails).Replace("[ISSUENO]", RTNlST[i].vchIssueNo), toEmail, true);//Repoting authority
                    smsStatus1 = comm.SendSmsNew(RTNlST[i].VchMobile.ToString(), RTNlST[i].vchMobileContent.ToString().Replace("[ISSUENO]", RTNlST[i].vchIssueNo));
                    string str = comm.UpdateMailSMSStaus("Issue", RTNlST[i].VchMobile.ToString(), RTNlST[i].Email.ToString(), "Issue Escalated", "1", "1", 1, "1", RTNlST[i].Email.ToString(), RTNlST[i].Email.ToString(), smsStatus1, MailStatus1);
                }

            }

        }
    }
    void timScheduledMail_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        MailMasterTracker objMailMaster = new MailMasterTracker();
        objMailMaster.mailSchedule();
    }

    void timScheduledOrderPaymentService_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        PaymentScheduler objPaySchedule = new PaymentScheduler();
        objPaySchedule.ServicePaymentSchedule();
    }
    void timScheduledOrderPaymentExternalService_Elapsed(object sender, System.Timers.ElapsedEventArgs e) // for external service add by anil
    {
        PaymentScheduler objPaySchedule = new PaymentScheduler();
        objPaySchedule.ExternalServicePaymentSchedule();
    }

     void timScheduledPealDataPushToMosarkar_Elapsed(object sender, System.Timers.ElapsedEventArgs e) // for mosarkar service add by Debi
    {
        PaymentScheduler objPaySchedule = new PaymentScheduler();
        objPaySchedule.MoSarkarServiceSchedule();
    }

    void timScheduledOrderPaymentPEAL_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        PaymentScheduler objPaySchedule = new PaymentScheduler();
        objPaySchedule.PealPaymentSchedule();
    }

    void timScheduledDocumentAIM_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        AIMDocumentScheduler objAIM = new AIMDocumentScheduler();
        objAIM.GetEinPcDocumentFromAIM();
    }

    void timScheduledCmsOrtpsa_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (DateTime.Now.Hour == 16 || DateTime.Now.Hour == 22) //// 4 PM and 10 PM
        {
            CmsOrtpsaPosting objCms = new CmsOrtpsaPosting();
            objCms.SendServiceInfoToCMS();
        }


        //string[] strScheduleHour = { "16", "24" };
        //string[] strScheduleMinute = { "00", "15", "30", "45" };

        //string strCurrentHour = DateTime.Now.Hour.ToString();
        //string strCurrentMinute = DateTime.Now.Minute.ToString();

        //if (strScheduleHour.Contains(strCurrentHour) && strScheduleMinute.Contains(strCurrentMinute)) //// 4 PM and 10 PM
        //{
        //    CmsOrtpsaPosting objCms = new CmsOrtpsaPosting();
        //    objCms.SendServiceInfoToCMS();
        //}

    }

    void timScheduledTask_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        // Scheduler

        string Connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString();
        string strMaildays = System.Configuration.ConfigurationManager.AppSettings["Mailbefore"].ToString();

        ORTPSTimeLine();

        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        ServiceDetails objServiceEntity = new ServiceDetails();
        objServiceEntity.STRACTION = "V";
        string strSMSFailureId = "";
        string strEMailFailureId = "";
        List<ServiceDetails> ServiceDetail = objService.ORTPS_SMSConfiguration(objServiceEntity).ToList();
        if (ServiceDetail.Count > 0)
        {
            for (int i = 0; i <= ServiceDetail.Count - 1; i++)
            {
                if (ServiceDetail[i].STRMOBILENO.ToString() != "NA")
                {
                    try
                    {
                        string strMobileNo = ServiceDetail[i].STRMOBILENO.ToString();
                        comm.SendSmsNew(strMobileNo, ServiceDetail[i].STRSMSCONTENT.ToString());
                    }
                    catch (Exception ex)
                    {
                        Util.LogError(ex, "GlobalORTPS");
                        //Response.Write(" Global ORTPS :" + ex.Message.ToString());
                        strSMSFailureId = strSMSFailureId + ServiceDetail[i].intConfigId.ToString() + ',';
                    }
                }
                else
                {
                    strSMSFailureId = strSMSFailureId + ServiceDetail[i].intConfigId.ToString() + ',';
                }
                if (ServiceDetail[i].STREMAILID.ToString() != "NA")
                {
                    try
                    {
                        string EmailId = ServiceDetail[i].STREMAILID.ToString();
                        string strATASubject = "Reminder to Take Action ";
                        string[] ATAtoEmail = new string[1];
                        ATAtoEmail[0] = EmailId;
                        //mailStatus = 
                        comm.sendMail(strATASubject, ServiceDetail[i].STRMAILCONTENT.ToString(), ATAtoEmail, true);
                        // mailStatus=sendMail(ServiceDetail[i].STRMAILCONTENT.ToString(), ServiceDetail[i].STREMAILID.ToString());
                    }
                    catch (Exception ex)
                    {
                        Util.LogError(ex, "GlobalORTPS");
                        strEMailFailureId = strEMailFailureId + ServiceDetail[i].intConfigId.ToString() + ',';
                    }
                }
                else
                {
                    strEMailFailureId = strEMailFailureId + ServiceDetail[i].intConfigId.ToString() + ',';
                }
                // string str = comm.UpdateMailSMSStaus("ORTPS", ServiceDetail[i].STRMOBILENO.ToString(), ServiceDetail[i].STREMAILID.ToString(), "Reminder to Take Action ", "1", ServiceDetail[i].INT_SERVICEID.ToString(), 1, ServiceDetail[i].VCH_APPLICATION_UNQ_KEY, ServiceDetail[i].STRSMSCONTENT.ToString(), ServiceDetail[i].STREMAILID.ToString(), smsStatus, mailStatus);

            }
        }
        //if (strSMSFailureId != "")
        //{
        objServiceEntity.STRACTION = "GS";
        objServiceEntity.STRSMSCONTENT = strSMSFailureId;
        //objServiceEntity.strFromdate = DateTime.Now.Date.ToString();
        List<ServiceDetails> ServiceDetail1 = objService.ORTPS_SMSConfiguration(objServiceEntity).ToList();
        //}
        //if (strEMailFailureId != "")
        //{
        objServiceEntity.STRACTION = "GE";
        objServiceEntity.STRMAILCONTENT = strEMailFailureId;
        // objServiceEntity.strFromdate = DateTime.Now.Date.ToString();
        List<ServiceDetails> ServiceDetail2 = objService.ORTPS_SMSConfiguration(objServiceEntity).ToList();
        //}
    }
    public void ORTPSTimeLine()
    {
        try
        {
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            ServiceDetails objServiceEntity = new ServiceDetails();
            objServiceEntity.STRACTION = "G";
            // objServiceEntity.strFromdate = DateTime.Now.Date.ToString();
            List<ServiceDetails> ServiceDetail = objService.ORTPS_SMSConfiguration(objServiceEntity).ToList();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GlobalORTPS");
        }
    }
    public void sendMail(string EmailContent, string EmailId)
    {
        try
        {
            EmailId = "radhikapatri089@gmail.com";
            string strATASubject = "Reminder to Take Action ";
            string[] ATAtoEmail = new string[1];
            ATAtoEmail[0] = EmailId;
            mailStatus = comm.sendMail(strATASubject, EmailContent, ATAtoEmail, true);

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GlobalORTPS");
        }
    }

    public void SendSms(string strMobNo, string Sms)
    {
        try
        {
            strMobNo = "9861957393";
            // string fb_url = "ASDhttp://193.105.74.159/api/v3/sendsms/plain?user=CSMTech&password=8hscKTZI&sender=BPCGRC&SMSText=" + Sms + "&type=longsms&GSM=91" + strMobNo;
            // AShttp://trans.websoftservices.com/api/sendmsg.php?user=websoft123&pass=websoft5302&sender=WEBSOF&phone=9776123242&text=Test SMS&priority=ndnd&stype=normal

            //string fb_url = "ADhttp://trans.websoftservices.com/api/sendmsg.php?user=websoft123&pass=websoft5302&sender=WEBSOF&priority=ndnd&stype=normal&phone=" + strMobNo + "&text=" + Sms;

            //SDFShttp://sms.websoftservices.com/rest/services/sendSMS/sendGroupSms?AUTH_KEY=6206ba97d1ba310284651649f66a546&message='dddd'&senderId='DEMOOS'&routeId=1&mobileNos='9861450389'&smsContentType=english
            string fb_url = "http://message.websoftservices.com/api/sendmultiplesms.php?username=ipicol&password=gQtCSZ31&sender=IPICOL&mobile=" + strMobNo + "&type=1&message=" + Sms + "";
            //  string fb_url = "http://sms.websoftservices.com/rest/services/sendSMS/sendGroupSms?AUTH_KEY=6206ba97d1ba310284651649f66a546&message=" + Sms + "&senderId=DEMOOS&routeId=1&mobileNos=" + strMobNo + "&smsContentType=english";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fb_url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();


            // string strmsg = "AUTH_KEY=6206ba97d1ba310284651649f66a546&message=" + Sms + "&senderId=DEMOOS&routeId=1&mobileNos=" + strMobNo + "&smsContentType=english";
            //// string strmsg =  Sms + "&senderId=" + UserId + "&routeId=1&mobileNos=" + strMobNo + "&smsContentType=english";

            // string curl="CChttp://sms.websoftservices.com/rest/services/sendSMS/sendGroupSms?'"+ strmsg +"";
            // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(curl);
            // request.Method = "GET";
            // request.ContentType = "application/x-www-form-urlencoded";
            // var response = (HttpWebResponse)request.GetResponse();
            // var reader = new StreamReader(response.GetResponseStream());
            // var objText = reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GlobalORTPS");
        }
    }
    void sendMail(string strSubject, string strBody, string[] toEmail, bool enbleSSl)
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
            strBody += "<br><br><br>From,<br>Single Window Portal<br>-------------------------------------------------------------------<br>This is a system generated Mail.Please don't Reply to this mail.<br>";
            mail.Body = strBody;
            //if (attchfile != null)
            //{
            //    mail.Attachments.Add(attchfile);
            //}
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = enbleSSl;
            mail.IsBodyHtml = true;

            //Warning: do not use this in production code!
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            //These are need tobe comment in PROD server
            // END 
            SmtpServer.Send(mail);
            Res = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GlobalORTPS");
            Res = false;
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        // System.Data.SqlClient.SqlDependency.Stop(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        //try
        //{
        //    // Context.Response.Redirect("CustomizeError.aspx");//aAdded by Chinmaya
        //    //   Context.Response.Redirect("CustomizeError.aspx");//aAdded by Chinmaya
        //    HttpContext ctx = HttpContext.Current;
        //    Exception exception = ctx.Server.GetLastError();
        //    string str_PageUrl = ctx.Request.Url.ToString();
        //    string str_ErrorMessage = exception.InnerException.Message.ToString();
        //    string EmailContent = "<strong>Trace :</strong></br>" + exception.StackTrace.ToString() + "<br></br><strong>Message :</strong></br>" + "<br></br> <br></br> " + exception.Message.ToString() + "<br></br><strong>Inner Exception:</strong></br>" + "<br></br> <br></br> " + str_ErrorMessage + "<br></br><strong>Page URL :</strong></br>" + "<br></br> <br></br> " + str_PageUrl;

        //    //Cls_ExceptionLog objLog = new Cls_ExceptionLog(str_PageUrl, str_ErrorMessage);
        //    //System.Threading.Thread th_ = new System.Threading.Thread(new System.Threading.ThreadStart(objLog.Sb_AddExceptionLog));
        //    //th_.Start();
        //    string EmailId = "error.goswift@gmail.com";
        //    string strATASubject = "Application Error||" + DateTime.Now.ToString() + "||GOSWIFT";
        //    string[] ATAtoEmail = new string[1];
        //    ATAtoEmail[0] = EmailId;
        //    comm.sendMail(strATASubject, EmailContent, ATAtoEmail, true);
        //}
        //catch (Exception ex)
        //{
        //    //throw ex;
        //}
    }

    void Session_Start(object sender, EventArgs e)
    {


    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        ////// I take the url referer host. (manipulating the query string this value is null or your local address)
        ////string strRefererHost = Request.UrlReferrer == null ? string.Empty : Request.UrlReferrer.Host;

        ////// This is the host name the application 
        ////string strUrlHost = Request.Url.Host;

        ////// I read the query string parameters
        ////string strQSPars = Request.Url.Query ?? string.Empty;

        ////// If the referer is not the application host (... someone manipulated the qs)...  
        ////// and    there is a query string parameter (be sure of this otherwise nobody can access the default page of your site
        ////// because this page has always a local referer...)
        ////if (strRefererHost != strUrlHost && strQSPars != string.Empty && strQSPars != "?lout=1")
        ////{
        ////    Response.Redirect("~/Login.aspx"); // your error page
        ////}
    }

</script>
