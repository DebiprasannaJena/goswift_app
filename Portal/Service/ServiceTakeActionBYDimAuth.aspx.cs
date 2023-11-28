using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using Ionic.Zip;
using EntityLayer.Service;
using Common;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Collections.Specialized;

public partial class Portal_Service_ServiceTakeActionBYDimAuth : SessionCheck
{
    #region "Global Variable"
    /// <summary>
    /// Radhika Rani Patri
    /// All global variable declared here
    /// </summary>
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    DataTable dt = new DataTable();
    ServiceDetails objService1 = new ServiceDetails();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
    string FormHeader = "";
    string FormFooter = "";
    int intAllignment = 0;
    string strUnqId = "";
    int intRetVal = 0;
    bool smsStatus;
    bool mailStatus;
    int intsmsStatus = 0;
    int intmailsts = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                BindDept();
                // AutoselectDept();
                BindGridDetails();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Service");
            }
        }
    }
    private void BindDept()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindDepartment("DP").ToList();
        ddldept.DataSource = objServicelist;
        ddldept.DataTextField = "strdeptname";
        ddldept.DataValueField = "Deptid";
        ddldept.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldept.Items.Insert(0, list);

    }
    private void AutoselectDept()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        try
        {
            objServicelist = objService.FindUserDepartment("FD", Session["UserId"].ToString()).ToList();
            if (objServicelist.Count > 0)
            {
                if (objServicelist[0].Deptid.ToString() != "0")
                {
                    if (objServicelist[0].Deptid.ToString() == "6" || objServicelist[0].Deptid.ToString() == "363")
                    {
                        ddldept.SelectedValue = "8";
                        ddldept.Enabled = false;
                        BindService();
                        BindGridDetails();
                    }
                    else
                    {
                        ddldept.SelectedValue = objServicelist[0].Deptid.ToString();
                        ddldept.Enabled = false;
                        BindService();
                        BindGridDetails();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }


    }
    private void BindGridDetails()
    {
        try
        {
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objService1.strAction = "A";
            objService1.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
            objService1.Deptid = Convert.ToInt32(ddldept.SelectedValue);
            objService1.str_ApplicationNo = txtAppno.Text;
            objService1.strProposalId = txtProposalno.Text;
            objService1.strFromdate = txtFromdate.Text;
            objService1.strTodate = txtTodate.Text;
            objService1.intActionTobeTakenBy = Convert.ToInt32(Session["UserId"].ToString());
            objServicelist = objService.ViewSErviceTakeActionDetails(objService1);
            gvService.DataSource = objServicelist;
            gvService.DataBind();
            txtFromdate.Text = txtFromdate.Text;
            txtTodate.Text = txtTodate.Text;
            intRetVal = objServicelist.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindService();
    }
    private void BindService()
    {
        try
        {
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
            ddlService.DataSource = objServicelist;
            ddlService.DataTextField = "strServiceName";
            ddlService.DataValueField = "intServiceId";
            ddlService.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlService.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //HiddenField hdnProposal=(HiddenField)e.Row.FindControl("hdnProposal");
            //Label lblRouted = (Label)e.Row.FindControl("lblRouted");
            //if(hdnProposal.Value=="NA" || hdnProposal.Value=="")
            //{
            //    lblRouted.Text = "Not Routed Through SLFC/DLFC";
            //}
            HyperLink hlnkproposal = (HyperLink)e.Row.FindControl("hlnkproposal");
            if (gvService.DataKeys[e.Row.RowIndex].Values[6].ToString().Trim() != "NA")
            {
                hlnkproposal.NavigateUrl = "ProposalDetails.aspx?StrPropNo=" + gvService.DataKeys[e.Row.RowIndex].Values[6];
                hlnkproposal.Visible = true;
            }
            else
            {
                hlnkproposal.NavigateUrl = "#";
            }
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);


            GridView gvSummary = (GridView)e.Row.FindControl("gvIntimateSent");
            LinkButton btn = new LinkButton();
            btn = (LinkButton)e.Row.FindControl("LinkButton1");

            HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hlnkTkn");
            hyRcvd.NavigateUrl = "ServiceDetailsView.aspx?ApplicationNo=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&ServiceId=" + gvService.DataKeys[e.Row.RowIndex].Values[0];
         

            ProposalBAL objService = new ProposalBAL();
            List<ProposalDet> objProjList = new List<ProposalDet>();
            ProposalDet objProp = new ProposalDet();
            DropDownList drpStatus = (DropDownList)e.Row.FindControl("drpStatus");
            objProp.strAction = "S";
            objProjList = objService.PopulateStatus(objProp).ToList();

            drpStatus.DataSource = objProjList;
            drpStatus.DataTextField = "strStatus";
            drpStatus.DataValueField = "intStatus";
            drpStatus.DataBind();
            drpStatus.Items.RemoveAt(2);
            drpStatus.Items.RemoveAt(1);
            //ListItem list = new ListItem();
            //list.Text = "--Select--";
            //list.Value = "0";
            //drpStatus.Items.Insert(0, list);
            //ListItem lstforward = new ListItem();
            //lstforward.Text = "Forward";
            //lstforward.Value = "8";
            //drpStatus.Items.Insert(4, lstforward);
            ////-------------------------------------For demand generation------------------------------
            //if (ddldept.SelectedValue == "8" || ddldept.SelectedValue == "10" || ddldept.SelectedValue == "3")
            //{
            //    ListItem lstdemand = new ListItem();
            //    lstdemand.Text = "Generate Demand Note";
            //    lstdemand.Value = "9";
            //    drpStatus.Items.Insert(5, lstdemand);
            //}
            ////-----------------------------------------------------------------------------------------
            //ServiceBusinessLayer objServ = new ServiceBusinessLayer();
            //List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            //objServicelist = objServ.BindService("U", int.Parse(ddldept.SelectedValue)).ToList();
            //ddlUser.DataSource = objServicelist;
            //ddlUser.DataTextField = "strServiceName";
            //ddlUser.DataValueField = "intServiceId";
            //ddlUser.DataBind();
            //ListItem lst = new ListItem();
            //lst.Text = "--Select--";
            //lst.Value = "0";
            //ddlUser.Items.Insert(0, lst);


            ////Added By Pranay Kumar on 11-Sept-2017 for Showing Query History
            //int intQueryStatus = Convert.ToInt32(gvService.DataKeys[e.Row.RowIndex].Values[4]);
            //LinkButton lnkbtnraise = (e.Row.FindControl("lnkbtnraise") as LinkButton);

            //if (intQueryStatus == 1)//Query Raised
            //{
            //    lnkbtnraise.Visible = true;
            //}
            //else if (intQueryStatus == 0) //IF Query Date is Blank
            //{
            //    lnkbtnraise.Visible = false;
            //}
            //else if (intQueryStatus == 2) //If Query Date is Expired
            //{
            //    lnkbtnraise.Visible = false;

            //}

            
        }
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGridDetails();
    }
    #region "Gridview Row created"
    /// <summary>
    /// Added By Subhasmita Behera on 31-Jul-2017 for grid view row created of approval status
    /// Code approval status row created
    /// </summary>
    /// 
    protected void gvService_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.RowSpan = 1;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Style["background-color"] = "#F5F5F5";
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Action Details";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.RowSpan = 1;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Style["background-color"] = "#F5F5F5";
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.RowSpan = 1;
            //HeaderCell.ForeColor = syDrawing.Color.Black;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderCell.BackColor = Drawing.Color.WhiteSmoke;
            HeaderCell.Style["background-color"] = "#F5F5F5";
            HeaderGridRow.Cells.Add(HeaderCell);

            gvService.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }

    #endregion
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
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                return true;
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    #region "Approval add"
    /// <summary>
    /// Added By Subhasmita Behera on 01-Aug-2017 for add approval details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Uploadname = "";
        string Uploadname1 = "";
        Button btnSubmit = (Button)sender;
        try
        {
            FileUpload docUpload = (FileUpload)btnSubmit.FindControl("docUpload");
            HiddenField hdnServiceId1 = (HiddenField)btnSubmit.FindControl("hdnServiceId");
            HiddenField hdnProposalId1 = (HiddenField)btnSubmit.FindControl("hdnProposalId");
            HiddenField hdnInvesterName1 = (HiddenField)btnSubmit.FindControl("hdnInvesterName");
            HiddenField hdnApplicationUnqKey1 = (HiddenField)btnSubmit.FindControl("hdnApplicationUnqKey");
            HiddenField hdnlevel1 = (HiddenField)btnSubmit.FindControl("hdnlevel");
            TextBox txtRemarks = (TextBox)btnSubmit.FindControl("txtRemarks");

            DropDownList drpStatus = (DropDownList)btnSubmit.FindControl("drpStatus");
            HiddenField hdnstrServiceName = (HiddenField)btnSubmit.FindControl("hdnstrServiceName");


            FileUpload fluApprovalCert = (FileUpload)btnSubmit.FindControl("fluApprovalCert");
            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + docUpload.FileName, DateTime.Now);
             bool rtnval = IsFileValidFile(docUpload);
            if (rtnval == true)
            {
            if (docUpload.HasFile)
            {

                if (Path.GetExtension(docUpload.FileName) != ".pdf")
                {
                    string strmsg11 = "alert('Only .pdf file accepted!');";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
                    return;
                }
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("../ApprovalDocs"));
            if (!string.IsNullOrEmpty(docUpload.FileName))
            {
                if (dir.Exists)
                {
                    docUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("../ApprovalDocs"));
                    docUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));

                }
                Uploadname = filepath;
            }
            else { Uploadname = ""; }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return;

            }
            string filepath1 = string.Format("{0:yyyy_MM_dd}" + "_" + hdnApplicationUnqKey1.Value + System.IO.Path.GetExtension(fluApprovalCert.FileName.ToString()), DateTime.Now);
            rtnval = IsFileValidFile(fluApprovalCert);
            if (rtnval == true)
            {
            if (fluApprovalCert.HasFile)
            {
                if ((fluApprovalCert.FileName.Contains(".pdf")) || (fluApprovalCert.FileName.Contains(".jpg")) || (fluApprovalCert.FileName.Contains(".jpeg")) || (fluApprovalCert.FileName.Contains(".JPEG")) || (fluApprovalCert.FileName.Contains(".png")))
                {
                    System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(Server.MapPath("../Portal/ApprovalDocs"));
                    if (!string.IsNullOrEmpty(fluApprovalCert.FileName))
                    {
                        if (dir1.Exists)
                        {
                            fluApprovalCert.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath1));
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("../Portal/ApprovalDocs"));
                            fluApprovalCert.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath1));

                        }
                        Uploadname1 = filepath1;
                    }
                    else { Uploadname1 = ""; }
                }
            }
            ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
            ServiceDetails objservice = new ServiceDetails();
            objservice.strAction = "V";
            objservice.intServiceId = Convert.ToInt32(hdnServiceId1.Value);
            objservice.strProposalId = hdnProposalId1.Value;
            objservice.strInvesterName = hdnInvesterName1.Value;
            objservice.strApplicationUnqKey = hdnApplicationUnqKey1.Value;
            //if (Uploadname1 != "")
            //{
            //    objservice.intStatus = 8;
            //}
            //else {
            objservice.intStatus = Convert.ToInt32(drpStatus.SelectedValue.Trim());
            //}
            objservice.intActionTobeTakenBy = Convert.ToInt32(Session["UserId"]);


            objservice.strReferenceFilename = Uploadname;
            objservice.strCertificateFilename = Uploadname1;
            objservice.strRemark = txtRemarks.Text;
            objservice.intCreatedBy = Convert.ToInt32(Session["UserId"]);

            string strRetVal = objServiceDet.UpdateServiceDet(objservice);
            if (strRetVal == "2")
            {
                CommonHelperCls comm = new CommonHelperCls();
                List<ServiceDetails> objServicelist = new List<ServiceDetails>();
                objServicelist = objServiceDet.GetEmailAndMobile("IM", hdnApplicationUnqKey1.Value);
                string mobile = "";
                string inestorName = "";
                string[] toEmail = new string[1];
                mobile = Convert.ToString(objServicelist[0].strMobileno);
                toEmail[0] = Convert.ToString(objServicelist[0].Email);
                inestorName = Convert.ToString(objServicelist[0].InvestorName);
                //-------------------------------
                string SMSContent = "";
                string strSubject = "";
                InvestorDetails objInvDet = new InvestorDetails();
                DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
                InvestorRegistration objInvService = new InvestorRegistration();
                objInvDet.strAction = "ST";
                DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
                string strServiceName = "";
                if (dtcontent.Rows.Count > 0)
                {
                    strSubject = dtcontent.Rows[0]["vchEvent"].ToString().Replace("[Status]", drpStatus.SelectedItem.Text.Trim());
                    SMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", inestorName);
                    strServiceName = hdnstrServiceName.Value;
                    //SMSContent = SMSContent.Replace("[Department]", ddldept.SelectedItem.Text.Trim().Replace("/"," Or "));
                    SMSContent = SMSContent.Replace("[ServiceName]", strServiceName);
                    if (drpStatus.SelectedValue == "9")
                    {
                        SMSContent = SMSContent.Replace("[Status]", "Generated");
                        SMSContent = SMSContent.Replace("application", "demand note");

                    }
                    else
                    {
                        SMSContent = SMSContent.Replace("[Status]", drpStatus.SelectedItem.Text.Trim());
                    }
                    SMSContent = SMSContent.Replace("&", "And");
                }
                //-----------------------------

                //string strSubject = "Single Window Portal - Approval of Child Services ";
                string strBody = SMSContent;

                //comm.sendMail(strSubject, strBody, toEmail, true);
                //comm.SendSms(mobile, SMSContent);
                mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                smsStatus = comm.SendSmsNew(mobile, SMSContent);
                // FOR SMS and Mail Status Update

                string str = comm.UpdateMailSMSStaus("Service", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), hdnServiceId1.Value, Convert.ToInt32(drpStatus.SelectedValue.Trim()), hdnApplicationUnqKey1.Value, SMSContent, strBody, smsStatus, mailStatus);
                // FOR SMS and Mail Status Update

                string rawURL = Request.RawUrl;
                string strStatus = drpStatus.SelectedItem.Text.Trim();
                string strShowMsg = "";
                if (drpStatus.SelectedValue == "9")
                {
                    strShowMsg = "Demand Note Generated Successfully!";
                }
                else
                {
                    strShowMsg = "Application " + strStatus + " Successfully !";
                }
                // Response.Write("<script>alert('Record Saved Successfully.');document.location.href='" + rawURL + "';;</script>");
                string ff = "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
            }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void btnQuerySubmit_Click(object sender, EventArgs e)
    {
        ProposalDet objProposal = new ProposalDet();
        ProposalBAL objService = new ProposalBAL();
        Button btnSubmit = (Button)sender;
        string Uploadname = "";
        try
        {
            FileUpload docUpload = (FileUpload)btnSubmit.FindControl("docqueryUpload");

            HiddenField hdnQryServiceId1 = (HiddenField)btnSubmit.FindControl("hdnQryServiceId");
            HiddenField hdnQryApplicationUnqKey1 = (HiddenField)btnSubmit.FindControl("hdnQryApplicationUnqKey");
            TextBox txtRemarks = (TextBox)btnSubmit.FindControl("txtQueryRemarks");
            objProposal.strAction = "Q";
            objProposal.intQueryId = Convert.ToInt32(hdnQryServiceId1.Value);
            objProposal.strProposalNo = hdnQryApplicationUnqKey1.Value;
            objProposal.intStatus = 5;
            objProposal.strRemarks = txtRemarks.Text;
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);

            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_Query_Dept_" + Session["UserId"].ToString() + ".pdf", DateTime.Now);
            if (docUpload.HasFile)
            {

                if (Path.GetExtension(docUpload.FileName) != ".pdf")
                {
                    string strmsg11 = "alert('Only .pdf file accepted!');";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
                    return;
                }
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/QueryFiles/Services/"));
            if (!string.IsNullOrEmpty(docUpload.FileName))
            {
                if (dir.Exists)
                {
                    docUpload.SaveAs(Server.MapPath("~/QueryFiles/Services/" + filepath));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/QueryFiles/Services/"));
                    docUpload.SaveAs(Server.MapPath("~/QueryFiles/Services/" + filepath));

                }
                Uploadname = filepath;
            }
            else { Uploadname = ""; }
            objProposal.strFileName = Uploadname;
            string strRetVal = objService.ServiceProposalRaiseQuery(objProposal);
            string strShowMsg = "";
            if (strRetVal == "2")
            {
                strShowMsg = "Query Raised Successfully!";
                //FOR SENDING MAIL & SMS
                CommonHelperCls comm = new CommonHelperCls();
                List<ProposalDet> objProposalList = new List<ProposalDet>();
                ProposalDet objProp = new ProposalDet();

                objProp.strAction = "S";
                objProp.strProposalNo = hdnQryApplicationUnqKey1.Value;
                objProp.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                objProposalList = objService.ServicegetRaisedQueryDetails(objProp).ToList();
                string mobile = "";
                string smsContent = "";
                string strSubject = "";
                string strBody = "";
                string[] toEmail = new string[1];


                if (objProposalList.Count > 0)
                {
                    mobile = Convert.ToString(objProposalList[0].MobileNo);
                    smsContent = Convert.ToString(objProposalList[0].strSMSContent);
                    toEmail[0] = Convert.ToString(objProposalList[0].EmailID);
                    strSubject = Convert.ToString(objProposalList[0].EmailSubject);
                    strBody = Convert.ToString(objProposalList[0].EmailBody);
                    comm.sendMail(strSubject, strBody, toEmail, true);
                    comm.SendSms(mobile, smsContent);
                }
                //For Sending SMS TO HOD
                objProp.strAction = "T";
                objProp.strProposalNo = hdnQryApplicationUnqKey1.Value;
                objProposalList = objService.ServicegetRaisedQueryDetails(objProp).ToList();

                if (objProposalList.Count > 0)
                {
                    if (objProposalList[0].intNoOfTimes >= 2) // for fetching how many times query raised by dept
                    {
                        objService1.INT_SERVICEID = Convert.ToInt32(hdnQryServiceId1.Value); //service id for peal is 500
                        objService1.strSubject = strSubject;
                        objService1.strBody = strBody;
                        objService1.smsContent = smsContent;

                        objDepartmntSms.DepartmentSendSms(objService1);
                    }
                }
            }
            else if (strRetVal == "4")
            { strShowMsg = "Error Occured!"; }

            string rawURL = Request.RawUrl;
            string ff = "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objProposal = null;
        }
    }
    #endregion
    #region "Gridview PageIndexChanging"
    protected void grdLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }
    #endregion
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            BindGridDetails();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            BindGridDetails();
        }
    }
    #region "Display Google Paging"

    private void DisplayPaging()
    {
        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + intRetVal + "</b> Of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvService.Rows[0].Cells[0].Text) + (gvService.PageSize - 1)) + "</b> Of <b>" + intRetVal + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
    private void FillComplaintLog(int intComplaintID, GridView gvControl)
    {
        try
        {
            int received = 0;
            int pending = 0;
            int resolved = 0;
            int discarded = 0;
            int forwarded = 0;
            int reopen = 0;
            int escalate = 0;

            received = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Open New"].ToString());
            pending = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Pending"].ToString());
            resolved = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Resolved"].ToString());
            discarded = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Reject"].ToString());
            forwarded = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Forward"].ToString());
            reopen = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Reopen"].ToString());
            escalate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Escalate"].ToString());

            //gvControl.DataSource = objService.ViewComplaintLog(intComplaintID, received, pending, resolved, discarded, escalate, forwarded, reopen);
            //gvControl.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
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
                    zip.AddFile(filePath, "QueryFiles");
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
}