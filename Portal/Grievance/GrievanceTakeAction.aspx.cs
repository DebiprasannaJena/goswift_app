using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using Ionic.Zip;
using System.Collections.Specialized;
using EntityLayer.GrievanceEntity;

public partial class Portal_Grievance_GrievanceTakeAction : System.Web.UI.Page
{
    #region "Global Variable"

    int intRecordCount = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }

        if (!Page.IsPostBack)
        {
            txtFromdate.Attributes.Add("readonly", "readonly");
            txtTodate.Attributes.Add("readonly", "readonly");

            try
            {
                

                if (Convert.ToString(Session["desId"]) == "126") // change by Anil sahoo
                {
                    BindDistrict();
                    
                    int intDistrictId = GetDistrictIdByUser();
                    DrpDwn_District.Enabled = false;
                    DrpDwn_District.SelectedValue = intDistrictId.ToString();

                    // DrpDwn_Investment_Level.SelectedValue = "2";
                    // DrpDwn_Investment_Level.Enabled = false;
                    BindGridDetails();
                }
                else
                {
                    BindDistrict();
                    
                    int intDistrictId = GetDistrictIdByUser();
                    DrpDwn_District.Enabled = true;
                    DrpDwn_Investment_Level.Enabled = true;
                    BindGridDetails();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Grievance");
            }
        }
    }

    #region FunctionUsed

    private void BindGridDetails()
    {
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();

        DataTable dt = new DataTable();

        try
        {
            objGrivEntity.StrAction = "VG";
            objGrivEntity.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
            objGrivEntity.intInvestmentLevel = Convert.ToInt32(DrpDwn_Investment_Level.SelectedItem.Value);// change by anil sahoo
            objGrivEntity.strFromDate = txtFromdate.Text.Trim();
            objGrivEntity.strToDate = txtTodate.Text.Trim();
            objGrivEntity.strGrivId = Txt_Griv_Id.Text.Trim();
            objGrivEntity.intUserId = Convert.ToInt32(Session["UserId"]);

            ////Bind grievance details for whom action to be taken
            dt = objBAL.ViewGrivTakeActionDetails(objGrivEntity);

            GridView1.DataSource = dt;
            GridView1.DataBind();
           
            intRecordCount = dt.Rows.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
            objBAL = null;
            objGrivEntity = null;
        }
    }
    private void BindDistrict()
    {
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();
        DataTable dt = new DataTable();
        try
        {
            objGrivEntity.StrAction = "BD";
            dt = GrievanceServices.FillDistrict(objGrivEntity);

            DrpDwn_District.DataTextField = "vchDistrictName";
            DrpDwn_District.DataValueField = "intDistrictId";
            DrpDwn_District.DataSource = dt;
            DrpDwn_District.DataBind();
            DrpDwn_District.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
            objBAL = null;
            objGrivEntity = null;
        }
    }
    private int GetDistrictIdByUser()
    {
        int intDistrictId = 0;

        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();

        DataTable dt = new DataTable();
        try
        {
            objGrivEntity.intUserId = Convert.ToInt32(Session["UserId"]);

            ////Get District Id By User
            dt = objBAL.GetDistrictIdByUser(objGrivEntity);
            if (dt.Rows.Count > 0)
            {
                intDistrictId = Convert.ToInt32(dt.Rows[0]["intDistrict"]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
            objBAL = null;
            objGrivEntity = null;
        }
        return intDistrictId;
    }

    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           

            HyperLink HypLnk_Griv_Id = (HyperLink)e.Row.FindControl("HypLnk_Griv_Id");
            HypLnk_Griv_Id.NavigateUrl = "GrievanceApplicationDetails.aspx?GrivId=" + GridView1.DataKeys[e.Row.RowIndex].Values["vchGrivId"] + "&RequestId=1";

            /*-------------------------------------------------------------------------------------------*/

            HiddenField Hid_Status = (HiddenField)e.Row.FindControl("Hid_Status");
            Label Lbl_Status = (Label)e.Row.FindControl("Lbl_Status");
            if (Hid_Status.Value == "3" || Hid_Status.Value == "7")
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Green;
            }

            /*-------------------------------------------------------------------------------------------*/

            HiddenField Hid_Invest_Level = (HiddenField)e.Row.FindControl("Hid_Invest_Level");
            Label Lbl_Invest_Level = (Label)e.Row.FindControl("Lbl_Invest_Level");
            if (Hid_Invest_Level.Value == "1")
            {
                Lbl_Invest_Level.ForeColor = System.Drawing.Color.Orange;
            }
            else
            {
                Lbl_Invest_Level.ForeColor = System.Drawing.Color.YellowGreen;
            }

            GrievanceServices objBAL = new GrievanceServices();
            GrievanceEntity objGrivEntity = new GrievanceEntity();

            int Intuserid = Convert.ToInt32(Session["UserId"].ToString());

            DropDownList ddldist = (DropDownList)e.Row.FindControl("ddldist");
            DropDownList drpStatus = (DropDownList)e.Row.FindControl("drpStatus");

            objGrivEntity.StrAction = "BD";
            DataTable dt = new DataTable();
            dt = GrievanceServices.FillDistrict(objGrivEntity);

            ddldist.DataTextField = "vchDistrictName";
            ddldist.DataValueField = "intDistrictId";
            ddldist.DataSource = dt;
            ddldist.DataBind();
            ddldist.Items.Insert(0, new ListItem("Select", "0"));

            if (Intuserid == 124) // Except ipicol Dept.
            {
                ListItem lstforward = new ListItem();
                lstforward.Text = "Forward";
                lstforward.Value = "8";
                drpStatus.Items.Insert(drpStatus.Items.Count, lstforward);
            }

            HiddenField Hid_Action_TakenBy = (HiddenField)e.Row.FindControl("hdnactiontakenby");
            LinkButton Linkaction = (LinkButton)e.Row.FindControl("LinkButton1");

            if (Hid_Action_TakenBy.Value == Session["UserId"].ToString())
            {
                Linkaction.Visible = true;
            }
            else if (Intuserid == 124)
            {
                Linkaction.Visible = true;
            }
            else
            {
                Linkaction.Visible = false;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridDetails();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridDetails();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }

    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Txt_Griv_Id.Text = "";
        txtFromdate.Text = "";
        txtTodate.Text = "";

        DrpDwn_District.SelectedIndex = 0;
        DrpDwn_Investment_Level.SelectedIndex = 0;
    }

    private bool IsFileValidFile(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt.ToLower()) && count == 1)
            {
                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }

        return true;
    }

    #region "Approval add"
    /// <summary>
    ///  
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string Uploadname = "";
        Button btnSubmit = (Button)sender;
        DataTable dtcontent = new DataTable();
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();
        try
        {

            FileUpload docUpload = (FileUpload)btnSubmit.FindControl("docUpload");
            HiddenField hdnApplicationUnqKey = (HiddenField)btnSubmit.FindControl("hdnApplicationUnqKey");  // Get value grievance Id 
            TextBox txtRemarks = (TextBox)btnSubmit.FindControl("txtRemarks");
            DropDownList drpStatus = (DropDownList)btnSubmit.FindControl("drpStatus");
            DropDownList ddldist = (DropDownList)btnSubmit.FindControl("ddldist");

            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + docUpload.FileName.Trim(), DateTime.Now);
            bool rtnval = IsFileValidFile(docUpload);

            if (rtnval == true)
            {
                if (docUpload.HasFile)
                {
                    int fileSize = docUpload.PostedFile.ContentLength;
                    if (Path.GetExtension(docUpload.FileName.ToLower()) != ".pdf")
                    {
                        string strmsg11 = "alert('Only .pdf file accepted!');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
                        return;
                    }
                    if (fileSize > (4 * 1024 * 1024))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 4 MB !','" + Messages.TitleOfProject + "'); </script>", false);
                        return;
                    }
                }

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Portal/Grievance/ApprovalDocGriv"));

                if (!string.IsNullOrEmpty(docUpload.FileName))
                {
                    if (dir.Exists)
                    {
                        docUpload.SaveAs(Server.MapPath("~/Portal/Grievance/ApprovalDocGriv/" + filepath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Portal/Grievance/ApprovalDocGriv"));
                        docUpload.SaveAs(Server.MapPath("~/Portal/Grievance/ApprovalDocGriv/" + filepath));
                    }

                    Uploadname = filepath;
                }
                else
                {
                    Uploadname = "";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }

            if (rtnval == true)
            {
                objGrivEntity.StrAction = "TA";
                objGrivEntity.strGrivId = hdnApplicationUnqKey.Value;

                if (drpStatus.SelectedValue.Trim() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Select Status !','" + Messages.TitleOfProject + "'); </script>", false);
                    return;
                }
                else
                {
                    objGrivEntity.intStatus = Convert.ToInt32(drpStatus.SelectedValue.Trim());
                }

                objGrivEntity.strReferenceFilename = Uploadname;
                objGrivEntity.strRemark = txtRemarks.Text;
                if (Convert.ToInt32(drpStatus.SelectedValue.Trim()) == 8)
                {
                    objGrivEntity.intActionTakenBy = Convert.ToInt32(Session["UserId"]);
                    objGrivEntity.intDistrictId = Convert.ToInt32(ddldist.SelectedValue);
                }
                else
                {
                    objGrivEntity.intActionTakenBy = Convert.ToInt32(Session["UserId"]);
                    objGrivEntity.intDistrictId = 0;
                }

                objGrivEntity.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                string strRetVal = objBAL.TakeActionDetail(objGrivEntity);
                // string[] arrResult = strRetVal.Split('_');

                if (strRetVal == "1")
                {
                    string rawURL = Request.RawUrl;
                    string strStatus = drpStatus.SelectedItem.Text.Trim();
                    int IntStatus = Convert.ToInt32(drpStatus.SelectedValue);

                    /*------------------------------------------------------------------------*/
                    /////Send SMS and Email in case of Resolved, Reject and Forward
                    /*------------------------------------------------------------------------*/
                    if (IntStatus == 13 || IntStatus == 3 || IntStatus == 8)
                    {
                        SendEmailSms(hdnApplicationUnqKey.Value, 1, IntStatus);
                    }

                    string strShowMsg = "Application " + strStatus + " Successfully !";
                    ////Response.Write("<script>alert('Record Saved Successfully.');document.location.href='" + rawURL + "';;</script>");
                    string ff = "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Something Went Wrong !','" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }
        finally
        {
            objGrivEntity = null;
            dtcontent = null;
        }
    }

    #endregion


    #region For send sms and email
    private void SendEmailSms(string strGrivId, int smsId, int IntStatus)
    {
        CommonHelperCls objComm = new CommonHelperCls();
        GrievanceEntity objGrivEntity = new GrievanceEntity();
        GrievanceServices objGriService = new GrievanceServices();
        // string strEmailContent = string.Empty;

        objGrivEntity.StrAction = "GESD";
        objGrivEntity.strGrivId = strGrivId;
        DataTable EmailSmsData = objGriService.GetUserInformationSmsEmailSend(objGrivEntity);

        string GrivId = EmailSmsData.Rows[0]["vchGrivId"].ToString();
        //string Status = EmailSmsData.Rows[0]["intStatus"].ToString();
        string InvesterMobilNo = EmailSmsData.Rows[0]["vchInvestorMobileNo"].ToString();
        string InvesterEmailId = EmailSmsData.Rows[0]["vchInvestorEmailId"].ToString();
        string CompanyName = EmailSmsData.Rows[0]["COMPANY_NAME"].ToString();
        string CurrentDistrictName = EmailSmsData.Rows[0]["vchCurrentDistrictName"].ToString();
        string CurrentDeptMobileNo = EmailSmsData.Rows[0]["vchCurrentDeptMobileNo"].ToString();
        string vchCurrentDeptEmailId = EmailSmsData.Rows[0]["vchCurrentDeptEmailId"].ToString();
        string CurrentCollectorName = EmailSmsData.Rows[0]["vchCurrentCollectorName"].ToString();
        string ForwardDistrictFromCollectorName = EmailSmsData.Rows[0]["vchForwardDistrictFromCollectorName"].ToString();

        string ForwardDistrictFrom = EmailSmsData.Rows[0]["intForwardDistrictFrom"].ToString();
        string ForwardUserMobNoFrom = EmailSmsData.Rows[0]["intForwardUserMobNoFrom"].ToString();
        string ForwardUserEmailIdFrom = EmailSmsData.Rows[0]["intForwardUserEmailIdFrom"].ToString();


        try
        {
            string strSubject = "GOSWIFT || Grievance Take Action ";

            if (IntStatus == 13)////Resolved
            {
                #region MyRegionResolved

                /*----------------------------------------------------------------*/
                ////Email Section
                /*----------------------------------------------------------------*/
                string strEmailContent = "Dear Investor,"
                                         + "</br></br>"
                                         + "This is to confirm that your Grievance with Grievance No: " + strGrivId + " has been resolved and closed . We thank you for your patience. "
                                         + "</br></br>"
                                         + "Regards"
                                         + "</br>"
                                         + "Team Invest Odisha";

                string[] InvToEmail = new string[1];
                InvToEmail[0] = InvesterEmailId;

                ////Send Email
                bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);


                /*----------------------------------------------------------------*/
                ////SMS Section
                /*----------------------------------------------------------------*/
                objGrivEntity.StrAction = "GSMS";
                objGrivEntity.IntSmsId = 30;
                DataTable dtcontent = objGriService.GetGrievanceSmsContent(objGrivEntity);

                string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();
                string strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);

                ///// Send SMS
                bool smsStatus = objComm.SendSmsWithTemplate(InvesterMobilNo, strSMSContent, strTemplateId, strMsgType);

                /*----------------------------------------------------------------*/
                ////// Update SMS and Email Status in Transaction Table
                /*----------------------------------------------------------------*/
                string str = objComm.UpdateMailSMSStaus("GrievanceTakeAction", InvesterMobilNo, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent, strEmailContent, smsStatus, mailStatus);

                #endregion
            }
            else if (IntStatus == 3)////Rejected
            {
                #region MyRegionRejected

                /*----------------------------------------------------------------*/
                ////Email Section
                /*----------------------------------------------------------------*/
                string strEmailContent = "Dear Investor,"
                                  + "</br>" + "</br>"
                                  + "Your grievance with Grievance No: " + strGrivId + " has rejected."
                                  + "</br></br>"
                                  + "Regards"
                                  + "</br>"
                                  + "Team Invest Odisha";

                string[] InvToEmail = new string[1];
                InvToEmail[0] = InvesterEmailId;

                /////Send Email
                bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

                /*----------------------------------------------------------------*/
                ////SMS Section
                /*----------------------------------------------------------------*/
                objGrivEntity.StrAction = "GSMS";
                objGrivEntity.IntSmsId = 31;
                DataTable dtcontent = objGriService.GetGrievanceSmsContent(objGrivEntity);

                string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();
                string strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);

                ///// Send SMS
                bool smsStatus = objComm.SendSmsWithTemplate(InvesterMobilNo, strSMSContent, strTemplateId, strMsgType);

                /*----------------------------------------------------------------*/
                ////// Update SMS and Email Status in Transaction Table
                /*----------------------------------------------------------------*/
                string str = objComm.UpdateMailSMSStaus("GrievanceTakeAction", InvesterMobilNo, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent, strEmailContent, smsStatus, mailStatus);

                #endregion
            }
            else if (IntStatus == 8)////Forwarded
            {
                #region MyRegionForwarded

                /*--------------------------------------------------------------------------------------------------------------------------------------*/
                /////Send SMS and Email to Investor after the application forwarded by the department
                /*--------------------------------------------------------------------------------------------------------------------------------------*/

                #region MyRegion

                ////Send Email to Investor
                string strEmailContent1 = "Dear Investor,"
                                 + "</br>" + "</br>"
                                 + "Your complaint has registered under Grievance No: " + strGrivId + " and is forwarded to " + CurrentDistrictName + " District nodal officer to take further action . We thank you for your patience. "
                                 + "</br>" + "</br>"
                                 + "Regards"
                                 + "</br>"
                                 + "Team Invest Odisha";

                string[] InvToEmail = new string[1];
                InvToEmail[0] = InvesterEmailId;
                bool mailStatus1 = objComm.sendMail(strSubject, strEmailContent1, InvToEmail, true);

                /*-------------------------------------------------------------*/

                ////Send SMS to Investor
                objGrivEntity.StrAction = "GSMS";
                objGrivEntity.IntSmsId = 32;
                DataTable dt1 = objGriService.GetGrievanceSmsContent(objGrivEntity);

                string strTemplateId1 = dt1.Rows[0]["vchTemplateId"].ToString();
                string strMsgType1 = dt1.Rows[0]["vchMsgType"].ToString();
                string strSMSContent1 = dt1.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);
                strSMSContent1 = strSMSContent1.Replace("[DISTRICT NAME]", CurrentDistrictName);

                ///// Send SMS
                bool smsStatus1 = objComm.SendSmsWithTemplate(InvesterMobilNo, strSMSContent1, strTemplateId1, strMsgType1);

                /*------------------------------------------------------------*/

                ////// Update SMS and Email Status in Transaction Table                
                string str1 = objComm.UpdateMailSMSStaus("GrievanceTakeAction", InvesterMobilNo, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent1, strEmailContent1, smsStatus1, mailStatus1);

                #endregion

                /*--------------------------------------------------------------------------------------------------------------------------------------*/
                /////Send SMS and Email to the department from which the application get forwarded.
                /*--------------------------------------------------------------------------------------------------------------------------------------*/

                #region MyRegion

                ////Send Email to existing department user (Collector)
                string strEmailContent2 = "Dear " + ForwardDistrictFromCollectorName + " ,"
                                         + "</br>" + "</br>"
                                         + "The Grievance of " + CompanyName + " has been registered under Grievance No:  " + strGrivId + " and is forwarded to " + CurrentDistrictName + " District Nodal officer for further action . "
                                         + "</br>" + "</br>"
                                         + "Regards" + "</br>"
                                         + "Team Invest Odisha";

                InvToEmail[0] = ForwardUserEmailIdFrom;
                ///// Send Email       
                bool mailStatus2 = objComm.sendMail(strSubject, strEmailContent2, InvToEmail, true);

                /*------------------------------------------------------------*/

                ////Send SMS to existing department user (Collector)
                objGrivEntity.StrAction = "GSMS";
                objGrivEntity.IntSmsId = 33;
                DataTable dt2 = objGriService.GetGrievanceSmsContent(objGrivEntity);

                string strTemplateId2 = dt2.Rows[0]["vchTemplateId"].ToString();
                string strMsgType2 = dt2.Rows[0]["vchMsgType"].ToString();
                string strSMSContent2 = dt2.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);
                strSMSContent2 = strSMSContent2.Replace("[INDUSTRY NAME]", CompanyName);
                strSMSContent2 = strSMSContent2.Replace("[DISTRICT NAME]", CurrentDistrictName);

                ///// Send SMS          
                bool smsStatus2 = objComm.SendSmsWithTemplate(ForwardUserMobNoFrom, strSMSContent2, strTemplateId2, strMsgType2);

                /*------------------------------------------------------------*/

                ////// Update SMS and Email Status in Transaction Table            
                string str2 = objComm.UpdateMailSMSStaus("GrievanceTakeAction", ForwardUserMobNoFrom, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent2, strEmailContent2, smsStatus2, mailStatus2);

                #endregion

                /*--------------------------------------------------------------------------------------------------------------------------------------*/
                /////Send SMS and Email to the department to which the application will be forwarded.
                /*--------------------------------------------------------------------------------------------------------------------------------------*/

                ////Send Email to current department user (Collector)
                string strEmailContent3 = "Dear " + CurrentCollectorName + " ,"
                                          + "</br>" + "</br>"
                                          + CompanyName + ", has been registered a grievance with Complaint No:  " + strGrivId + " . "
                                          + "</br>" + "</br>"
                                          + "Regards"
                                          + "</br>"
                                          + "Team Invest Odisha";

                InvToEmail[0] = vchCurrentDeptEmailId;
                ///// Send Email       
                bool mailStatus3 = objComm.sendMail(strSubject, strEmailContent3, InvToEmail, true);

                /*------------------------------------------------------------*/

                ////Send SMS to existing department user (Collector)
                objGrivEntity.StrAction = "GSMS";
                objGrivEntity.IntSmsId = 29;
                DataTable dt3 = objGriService.GetGrievanceSmsContent(objGrivEntity);

                string strTemplateId3 = dt3.Rows[0]["vchTemplateId"].ToString();
                string strMsgType3 = dt3.Rows[0]["vchMsgType"].ToString();
                string strSMSContent3 = dt3.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);
                strSMSContent3 = strSMSContent3.Replace("[INDUSTRY NAME]", CompanyName);

                ///// Send SMS      
                bool smsStatus3 = objComm.SendSmsWithTemplate(CurrentDeptMobileNo, strSMSContent3, strTemplateId3, strMsgType3);

                /*------------------------------------------------------------*/

                ////// Update SMS and Email Status in Transaction Table            
                string str3 = objComm.UpdateMailSMSStaus("GrievanceTakeAction", CurrentDeptMobileNo, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent3, strEmailContent3, smsStatus3, mailStatus3);

                #endregion
            }



            ///*------------------------------------------------------*/
            //////// Mail Section
            ///*------------------------------------------------------*/

            //if (smsId == 30)  // For Resolved
            //{
            //    strEmailContent = "Dear Investor,"
            //                     + "</br>" + "</br>"
            //                     + "This is to confirm that your Grievance with Grievance No: " + strGrivId + " has been resolved and closed . We thank you for your patience. " + "</br>" + "</br>" + "Regards" + "</br>" + "Team Invest Odisha";


            //    ////Send Email
            //    ///
            //    string[] InvToEmail = new string[1];
            //    InvToEmail[0] = InvesterEmailId;
            //    bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

            //}
            //else if (smsId == 32) // For forward send Invester
            //{
            //    strEmailContent = "Dear Investor,"
            //                    + "</br>" + "</br>"
            //                    + "Your complaint has registered under Grievance No: " + strGrivId + " and is forwarded to " + CurrentDistrictName + " District nodal officer to take further action . We thank you for your patience. " + "</br>" + "</br>" + "Regards" + "</br>" + "Team Invest Odisha";

            //    string[] InvToEmail = new string[1];
            //    InvToEmail[0] = InvesterEmailId;
            //    bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

            //}
            //else if (smsId == 33)  //  For forward send ForwardDistrictFrom
            //{
            //    strEmailContent = "Dear " + ForwardDistrictFromCollectorName + " ,"
            //                    + "</br>" + "</br>"
            //                    + "The Grievance of " + CompanyName + " has been registered under Grievance No:  " + strGrivId + " and is forwarded to " + CurrentDistrictName + " District Nodal officer for further action . " + "</br>" + "</br>" + "Regards" + "</br>" + "Team Invest Odisha";

            //    string[] InvToEmail = new string[1];
            //    InvToEmail[0] = ForwardUserEmailIdFrom;
            //    bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

            //}
            //else if (smsId == 29) //  For forward send CurrentDeptEmailId
            //{
            //    strEmailContent = "Dear " + CurrentCollectorName + " ,"
            //                   + "</br>" + "</br>"
            //                   + CompanyName + ", has been registered a grievance with Complaint No:  " + strGrivId + " . " + "</br>" + "</br>" + "Regards" + "</br>" + "Team Invest Odisha";

            //    string[] InvToEmail = new string[1];
            //    InvToEmail[0] = vchCurrentDeptEmailId;
            //    bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);


            //}

            ///*------------------------------------------------------*/
            //////// SMS Section
            ///*------------------------------------------------------*/

            //if (smsId == 30)  // For Resolved
            //{
            //    strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);

            //    ///// Send SMS
            //    bool smsStatus = objComm.SendSmsWithTemplate(InvesterMobilNo, strSMSContent, strTemplateId, strMsgType);
            //}
            //else if (smsId == 31) // For Reject
            //{
            //    strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);


            //    ///// Send SMS
            //    bool smsStatus = objComm.SendSmsWithTemplate(InvesterMobilNo, strSMSContent, strTemplateId, strMsgType);
            //}
            //else if (smsId == 32) // For Forward  Invester
            //{
            //    string strSMSContent1 = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);
            //    strSMSContent = strSMSContent1.Replace("[DISTRICT NAME]", CurrentDistrictName);


            //    ///// Send SMS
            //    bool smsStatus = objComm.SendSmsWithTemplate(InvesterMobilNo, strSMSContent, strTemplateId, strMsgType);
            //}
            //else if (smsId == 33)  // For Forward   ForwardUserMobNoFrom
            //{
            //    string strSMSContent1 = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[INDUSTRY NAME]", CompanyName);
            //    string strSMSContent2 = strSMSContent1.Replace("[GRIV ID]", strGrivId);
            //    strSMSContent = strSMSContent2.Replace("[DISTRICT NAME]", CurrentDistrictName);


            //    ///// Send SMS
            //    bool smsStatus = objComm.SendSmsWithTemplate(ForwardUserMobNoFrom, strSMSContent, strTemplateId, strMsgType);
            //}
            //else if (smsId == 29)  // For Forward   CurrentDeptMobileNo
            //{
            //    string strSMSContent1 = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[INDUSTRY NAME]", CompanyName);
            //    strSMSContent = strSMSContent1.Replace("[GRIV ID]", strGrivId);


            //    ///// Send SMS
            //    bool smsStatus = objComm.SendSmsWithTemplate(CurrentDeptMobileNo, strSMSContent, strTemplateId, strMsgType);
            //}


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }
        finally
        {

        }
    }

    #endregion


    #region "ZIP DOWNLOAD"
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("QueryFiles");
            if (hdnFileNames.Value != "")
            {
                string[] arrFileName = hdnFileNames.Value.Split(',');
                for (int i = 0; i <= arrFileName.Count() - 1; i++)
                {
                    string FileName = "../../QueryFiles/Services/" + Convert.ToString(arrFileName[i]);
                    string filePath = Server.MapPath(FileName);
                    if (File.Exists(filePath))
                    {
                        zip.AddFile(filePath, "QueryFiles");
                    }
                }
            }
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("QueryFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }
    #endregion

    #region "Display Google Paging"

    private void DisplayPaging()
    {
        if (GridView1.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (GridView1.PageIndex + 1 == GridView1.PageCount)
            {
                lblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRecordCount + "</b> of <b>" + intRecordCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)GridView1.Rows[0].FindControl("lblsl")).Text) + GridView1.PageSize - 1) + "</b> of <b>" + intRecordCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                GridView1.PageIndex = 0;
                GridView1.AllowPaging = false;
                BindGridDetails();
            }
            else
            {
                lbtnAll.Text = "All";
                GridView1.AllowPaging = true;
                BindGridDetails();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }
    }

    #endregion
}
