#region Namespaces
using EntityLayer.GrievanceEntity;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

public partial class GRIEVANCE_AddGrievance : SessionCheck
{
    string FilePathAttach1 = "";
    string FilePathAttach2 = "";
    string Result = "";
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/LogOut.aspx", true);
        }

        if (!IsPostBack)
        {
            try
            {
                BindDistrict();
                BindGrievanceType();
                BindUnitDetail();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "AddGrievance");
            }
        }
    }

    #region FunctionUsed
    private void BindDistrict()
    {
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BD"
            };

            Dt = GrievanceServices.FillDistrict(objSearch);

            DrpDwnDistrict.DataSource = Dt;
            DrpDwnDistrict.DataTextField = "vchDistrictName";
            DrpDwnDistrict.DataValueField = "intDistrictId";
            DrpDwnDistrict.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DrpDwnDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindGrievanceType()
    {
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BGT"
            };

            Dt = GrievanceServices.FillGrievanceType(objSearch);

            DrpDwnGrivType.DataSource = Dt;
            DrpDwnGrivType.DataTextField = "vchGrivType";
            DrpDwnGrivType.DataValueField = "intGrivTypeId";
            DrpDwnGrivType.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DrpDwnGrivType.Items.Insert(0, list);

            ListItem list_sub_type = new ListItem();
            list_sub_type.Text = "--Select--";
            list_sub_type.Value = "0";
            DrpDwnGrivSubType.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindGrievanceSubType()
    {
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BGST",
                intInvestorId = Convert.ToInt32(Session["InvestorId"].ToString()),
                intGrivTypeId = Convert.ToInt32(DrpDwnGrivType.SelectedValue)
            };

            Dt = GrievanceServices.FillGrievanceSubType(objSearch);

            DrpDwnGrivSubType.DataSource = Dt;
            DrpDwnGrivSubType.DataTextField = "vchGrivSubType";
            DrpDwnGrivSubType.DataValueField = "intGrivSubTypeId";
            DrpDwnGrivSubType.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DrpDwnGrivSubType.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindUnitDetail()
    {
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BUD",
                intInvestorId = Convert.ToInt32(Session["InvestorId"].ToString())
            };

            Dt = GrievanceServices.FillUnitDetail(objSearch);
            if (Dt.Rows.Count > 0)
            {
                LblCompanyName.Text = Dt.Rows[0]["VCH_INV_NAME"].ToString();
                TxtApplicantName.Text = Dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                TxtDesignation.Text = Dt.Rows[0]["VCH_DESIG"].ToString();
                TxtMobileNo.Text = Dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                TxtEmail.Text = Dt.Rows[0]["VCH_EMAIL"].ToString();
                DrpDwnDistrict.ClearSelection();
                DrpDwnDistrict.Items.FindByValue(Dt.Rows[0]["INT_DISTRICT"].ToString()).Selected = true;
                DrpDwnInvestmentLevel.Items.FindByValue(Dt.Rows[0]["INT_CATEGORY"].ToString()).Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    #endregion

    protected void DrpDwnGrivType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpDwnGrivType.SelectedIndex > 0)
            {
                BindGrievanceSubType();
            }
            else
            {
                DrpDwnGrivSubType.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "AddGrievance");
        }
    }

    private bool IsFileValidAttach1(FileUpload FileUpload1)
    {
        string strFiletype = "";
        string fileExt = "";
        int count = 0;

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

        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Grievance/Attachment/"));

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
            {
                string strFileExt = Path.GetExtension(fileUpldAttach1.FileName).ToLower();
                if ((strFileExt != ".pdf") && (strFileExt != ".png") && (strFileExt != ".jpg") && (strFileExt != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only pdf, png, jpg, and jpeg files are allowed.')", true);
                    return false;
                }

                int fileSize = fileUpldAttach1.PostedFile.ContentLength;
                if (fileSize > (5 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large.The maximum file size allowed is 5 MB.')", true);
                    return false;
                }

                FilePathAttach1 = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "GrievanceA1" + Path.GetExtension(fileUpldAttach1.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(fileUpldAttach1.FileName))
                {
                    if (dir.Exists)
                    {
                        fileUpldAttach1.SaveAs(Server.MapPath("~/Grievance/Attachment/" + FilePathAttach1));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Grievance/Attachment"));
                        fileUpldAttach1.SaveAs(Server.MapPath("~/Grievance/Attachment/" + FilePathAttach1));
                    }

                    HdnAttach1.Value = FilePathAttach1;
                    HypLnkAttach1.NavigateUrl = "~/Grievance/Attachment/" + FilePathAttach1;

                    LnkBtnUploadAttach1.Visible = false;
                    HypLnkAttach1.Visible = true;
                    LnkBtnDelAttach1.Visible = true;
                    LblAttach1.Visible = true;
                    fileUpldAttach1.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File mime type is not correct !</strong>','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    protected void LnkBtnUploadAttach1_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidAttach1(fileUpldAttach1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        
    }
    protected void LnkBtnDelAttach1_Click(object sender, EventArgs e)
    {
        UpdFileRemove(HdnAttach1, LnkBtnUploadAttach1, LnkBtnDelAttach1, HypLnkAttach1, LblAttach1, fileUpldAttach1);
    }

    private bool IsFileValidAttach2(FileUpload FileUpload1)
    {
        string strFiletype = "";
        string fileExt = "";
        int count = 0;

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
           
        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Grievance/Attachment/"));

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype)  && imageExtension.Contains(fileExt) && count == 1)
            {
                string strFileExt = Path.GetExtension(fileUpldAttach2.FileName).ToLower();
                if ((strFileExt != ".pdf") && (strFileExt != ".png") && (strFileExt != ".jpg") && (strFileExt != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only pdf, png, jpg, and jpeg files are allowed.')", true);
                    return false;
                }

                int fileSize = fileUpldAttach2.PostedFile.ContentLength;
                if (fileSize > (5 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large.The maximum file size allowed is 5 MB.')", true);
                    return false;
                }

                FilePathAttach2 = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "GrievanceA2" + Path.GetExtension(fileUpldAttach2.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(fileUpldAttach2.FileName))
                {
                    if (dir.Exists)
                    {
                        fileUpldAttach2.SaveAs(Server.MapPath("~/Grievance/Attachment/" + FilePathAttach2));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Grievance/Attachment"));
                        fileUpldAttach2.SaveAs(Server.MapPath("~/Grievance/Attachment/" + FilePathAttach2));
                    }

                    HdnAttach2.Value = FilePathAttach2;
                    HypLnkAttach2.NavigateUrl = "~/Grievance/Attachment/" + FilePathAttach2;

                    LnkBtnUploadAttach2.Visible = false;
                    HypLnkAttach2.Visible = true;
                    LnkBtnDelAttach2.Visible = true;
                    LblAttach2.Visible = true;
                    fileUpldAttach2.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File mime type is not correct !</strong>','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    protected void LnkBtnUploadAttach2_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidAttach2(fileUpldAttach2);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        
    }
    protected void LnkBtnDelAttach2_Click(object sender, EventArgs e)
    {
        UpdFileRemove(HdnAttach2, LnkBtnUploadAttach2, LnkBtnDelAttach2, HypLnkAttach2, LblAttach2, fileUpldAttach2);
    }

    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile)
    {
        string filename = hdnFile.Value;
        string path = "~/Grievance/Attachment/" + filename;
        string completePath = Server.MapPath(path);
        if (System.IO.File.Exists(completePath))
        {
            System.IO.File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddGrievance.aspx");
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //// Reqular expression validation for email format
            Regex regEmail = new Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");

            if (LblCompanyName.Text.Trim() == "")
            {
                LblCompanyName.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter company name.</strong>');", true);
                return;
            }
            else if (TxtApplicantName.Text.Trim() == "")
            {
                TxtApplicantName.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter applicant name.</strong>');", true);
                return;
            }
            else if (TxtDesignation.Text.Trim() == "")
            {
                TxtDesignation.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter designation.</strong>');", true);
                return;
            }
            else if (TxtMobileNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter mobile number.</strong>');", true);
                TxtMobileNo.Focus();
                return;
            }
            else if (TxtMobileNo.Text.Trim().Substring(0, 1) == "0")
            {
                TxtMobileNo.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Mobile number should not be start with zero.</strong>');", true);
                return;
            }
            else if (TxtMobileNo.Text.Trim().Length != 10)
            {
                TxtMobileNo.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Mobile number should be 10 digits.</strong>');", true);
                return;
            }
            else if (DrpDwnInvestmentLevel.SelectedIndex <= 0)
            {
                DrpDwnInvestmentLevel.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select investment level.</strong>');", true);
                return;
            }
            else if (DrpDwnDistrict.SelectedIndex <= 0)
            {
                DrpDwnDistrict.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select district.</strong>');", true);
                return;
            }
            else if (TxtEmail.Text.Trim() == "")
            {
                TxtEmail.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter email address.</strong>');", true);
                return;
            }
            else if (!regEmail.IsMatch(TxtEmail.Text.Trim()))
            {
                TxtEmail.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid email address.</strong>');", true);
                return;
            }
            else if (DrpDwnGrivType.SelectedIndex <= 0)
            {
                DrpDwnGrivType.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select grievance type.</strong>');", true);
                return;
            }
            else if (DrpDwnGrivSubType.SelectedIndex <= 0)
            {
                DrpDwnGrivSubType.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select grievance sub type.</strong>');", true);
                return;
            }
            else if (TxtGrievanceTitle.Text.Trim() == "")
            {
                TxtGrievanceTitle.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter grievance title.</strong>');", true);
                return;
            }
            else if (TxtGrievanceDetail.Text.Trim() == "")
            {
                TxtGrievanceDetail.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter grievance details.</strong>');", true);
                return;
            }
            else
            {
                GrievanceEntity objSearch = new GrievanceEntity()
                {
                    StrAction = "SD",
                    intInvestorId = Convert.ToInt32(Session["InvestorId"].ToString()),
                    intDistrictId = Convert.ToInt32(DrpDwnDistrict.SelectedValue),
                    vchDistrictName = DrpDwnDistrict.SelectedItem.Text,
                    intGrivTypeId = Convert.ToInt32(DrpDwnGrivType.SelectedValue),
                    vchGrivSubTypeId = DrpDwnGrivSubType.SelectedValue,
                    intInvestmentLevel = Convert.ToInt32(DrpDwnInvestmentLevel.SelectedValue),
                    vchApplicantName = TxtApplicantName.Text,
                    vchDesignation = TxtDesignation.Text,
                    vchMobileNo = TxtMobileNo.Text,
                    vchEmail = TxtEmail.Text,
                    vchGrivTitle = TxtGrievanceTitle.Text,
                    vchGrivDetail = TxtGrievanceDetail.Text,
                    vchAttachment1 = HdnAttach1.Value,
                    vchAttachment2 = HdnAttach2.Value,
                    IntIndustryCategory = Convert.ToInt32(Session["IndustryType"].ToString())  // Add by anil sahoo for Industry & non industery
                };

                Result = GrievanceServices.SaveGrievanceDetail(objSearch);
                string[] arrResult = Result.Split('_');

                if (arrResult[0] == "1")
                {
                    string strGrivId = Convert.ToString(arrResult[1]);
                    string strIndustryName = Convert.ToString(arrResult[2]);
                    string strPhoneNo = Convert.ToString(arrResult[3]);
                    string strOfficerFullName = Convert.ToString(arrResult[4]);
                    string strOfficerEmail = Convert.ToString(arrResult[5]);

                    SendEmailSms(TxtEmail.Text, TxtMobileNo.Text, strGrivId, 28, strIndustryName, ""); // after add grievance send email and sms to investor
                    SendEmailSms(strOfficerEmail, strPhoneNo, strGrivId, 29, strIndustryName, strOfficerFullName); //after add grievance send email and SMS to department

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = '../Grievance.aspx';}); </script>", false);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong.</strong>');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "AddGrievance");
        }
    }

    #region For send sms and email to Inverster and Addmin.  add Anil Sahoo 

    private void SendEmailSms(string strEmailId, string strMobileNo, string strGrivId, int smsId, string IndusteryName, string OfficerName)
    {
        CommonHelperCls objComm = new CommonHelperCls();
        GrievanceEntity objGrivEntity = new GrievanceEntity();
        GrievanceServices objGriService = new GrievanceServices();

        string strEmailContent = string.Empty;
        try
        {
            /*------------------------------------------------------*/
            ////// Mail Section
            /*------------------------------------------------------*/
            string strSubject = "GOSWIFT || Grievance Registration ";
            if (OfficerName == "")
            {
                strEmailContent = "Dear Investor,"
                                      + "</br>" + "</br>"
                                      + "Your complaint on  " + DrpDwnGrivType.SelectedItem.Text + " for " + IndusteryName + " is registered. Your Grievance No is " + strGrivId + " ."
                                      + "</br>" + "</br>"
                                      + "Regards"
                                      + "</br>"
                                      + "Team Invest Odisha";
            }
            else if (OfficerName != "")
            {
                strEmailContent = "Dear " + OfficerName + ","
                                      + "</br>" + "</br>"
                                      + "The " + IndusteryName + "," + " has been registered a grievance with Grievance No : " + strGrivId + " ."
                                      + "</br>" + "</br>"
                                      + "Regards"
                                      + "</br>"
                                      + "Team Invest Odisha";
            }

            string[] InvToEmail = new string[1];
            InvToEmail[0] = strEmailId;

            ////Send Email
            bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

            /*------------------------------------------------------*/
            ////// SMS Section
            /*------------------------------------------------------*/
            objGrivEntity.StrAction = "GSMS";
            objGrivEntity.IntSmsId = smsId;
            DataTable dtcontent = objGriService.GetGrievanceSmsContent(objGrivEntity);
            if (dtcontent.Rows.Count > 0)
            {
                /*----------------------------------------------------------------*/
                ////// Prepare SMS and Mail Content
                /*----------------------------------------------------------------*/
                strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                string strSMSContent = string.Empty;
                if (dtcontent.Rows[0]["vchSMSContent"].ToString().Contains("[INDUSTRY NAME]"))
                {
                    string strSMSContent1 = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[INDUSTRY NAME]", IndusteryName);
                    strSMSContent = strSMSContent1.Replace("[GRIV ID]", strGrivId);
                }
                else
                {
                    strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[GRIV ID]", strGrivId);
                }

                string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();

                /*----------------------------------------------------------------*/
                ///// Send SMS
                /*----------------------------------------------------------------*/
                bool smsStatus = objComm.SendSmsWithTemplate(strMobileNo, strSMSContent, strTemplateId, strMsgType);

                /*----------------------------------------------------------------*/
                ////// Update SMS and Email Status in Transaction Table
                /*----------------------------------------------------------------*/
                string str = objComm.UpdateMailSMSStaus("AddGrievance", strMobileNo, strEmailId, strSubject, "0", "0", 0, "0", strSMSContent, strEmailContent, smsStatus, mailStatus);
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objComm = null;
        }
    }


    #endregion 
}