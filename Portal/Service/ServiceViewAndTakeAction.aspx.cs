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
//using Common;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json.Linq;

public partial class Service_ServiceViewAndTakeAction : SessionCheck
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
    int intUserId = 0;

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
                txtFromdate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
                //BindDept();
                //AutoselectDept();
                BindGridDetails();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Service");
            }
        }
    }
    //private void BindDept()
    //{
    //    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //    objServicelist = objService.BindDepartment("DP").ToList();
    //    ddldept.DataSource = objServicelist;
    //    ddldept.DataTextField = "strdeptname";
    //    ddldept.DataValueField = "Deptid";
    //    ddldept.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddldept.Items.Insert(0, list);

    //}
    private void AutoselectDept()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.FindUserDepartment("FD", Session["UserId"].ToString()).ToList();
        if (objServicelist.Count > 0)
        {
            if (objServicelist[0].Deptid.ToString() != "0")
            {

            }
        }


    }
    private void BindGridDetails()
    {

        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        try
        {
            objService1.strAction = "V";
            //objService1.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
            //objService1.Deptid = Convert.ToInt32(ddldept.SelectedValue);
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

    //protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindService();
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Service");
    //    }
    //}
    //private void BindService()
    //{
    //    try
    //    {
    //        ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //        objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
    //        ddlService.DataSource = objServicelist;
    //        ddlService.DataTextField = "strServiceName";
    //        ddlService.DataValueField = "intServiceId";
    //        ddlService.DataBind();
    //        ListItem list = new ListItem();
    //        list.Text = "--Select--";
    //        list.Value = "0";
    //        ddlService.Items.Insert(0, list);
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Service");
    //    }

    //}
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);

            //Changes by manoj kumar behera as suggest by ipicol when query revert date excide then take action byyton will enable whatever query status not matter//

            HiddenField hdncurrentquerystatus = (HiddenField)e.Row.FindControl("hdncurrentquerystatus");
            DateTime QueryValidDate = Convert.ToDateTime(hdncurrentquerystatus.Value);
            DateTime TodayDate = DateTime.Now;

            //Changes by manoj kumar behera as suggest by ipicol when query revert date excide then take action byyton will enable whatever query status not matter//

            //HiddenField hdnProposal=(HiddenField)e.Row.FindControl("hdnProposal");
            //Label lblRouted = (Label)e.Row.FindControl("lblRouted");
            //if(hdnProposal.Value=="NA" || hdnProposal.Value=="")
            //{
            //    lblRouted.Text = "Not Routed Through SLFC/DLFC";
            //}
            Label lbldistrict = (Label)e.Row.FindControl("lbldistrict");
            try
            {
                lbldistrict.Text = District(gvService.DataKeys[e.Row.RowIndex].Values[0].ToString(), gvService.DataKeys[e.Row.RowIndex].Values[1].ToString());
            }
            catch
            {
                lbldistrict.Text = "NA";
            }

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


            GridView gvSummary = (GridView)e.Row.FindControl("gvIntimateSent");
            LinkButton btn = new LinkButton();
            btn = (LinkButton)e.Row.FindControl("LinkButton1");

            HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hlnkTkn");
            hyRcvd.NavigateUrl = "ServiceDetailsView.aspx?ApplicationNo=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&ServiceId=" + gvService.DataKeys[e.Row.RowIndex].Values[0];
            DropDownList ddlUser = (DropDownList)e.Row.FindControl("ddlUser");

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
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpStatus.Items.Insert(0, list);

            //ListItem lstforward = new ListItem();
            //lstforward.Text = "Forward";
            //lstforward.Value = "8";
            //drpStatus.Items.Insert(4, lstforward);


            //-------------------------------------For demand generation------------------------------

            ServiceBusinessLayer objService1 = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objServicelist = objService1.FindUserDepartment("FD", Session["UserId"].ToString()).ToList();
            if (objServicelist.Count > 0)
            {
                if (objServicelist[0].Deptid.ToString() != "0")
                {
                    if (objServicelist[0].Deptid.ToString() == "8" || objServicelist[0].Deptid.ToString() == "10")
                    {
                        ListItem lstdemand = new ListItem();
                        lstdemand.Text = "Generate Demand Note";
                        lstdemand.Value = "9";
                        drpStatus.Items.Insert(drpStatus.Items.Count, lstdemand);
                    }
                    intUserId = Convert.ToInt32(objServicelist[0].Deptid.ToString());
                }
            }

            //--------------------------------------ADD Forward OPTION ACCORDING TO DEPT---------------------------------------------------

            if (intUserId != 878 && intUserId != 6 && intUserId !=188) // Except Works Dept.
            {
                ListItem lstforward = new ListItem();
                lstforward.Text = "Forward";
                lstforward.Value = "8";
                drpStatus.Items.Insert(drpStatus.Items.Count, lstforward);
            }


            //--------------------------------------ENERGY UTILITY USER ACCORDING TO DIVISION---------------------------------------------------

            ServiceBusinessLayer objServ = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist1 = new List<ServiceDetails>();
            objServicelist1 = objServ.BindServiceOnlyForward("U", intUserId, Convert.ToInt32(Session["UserId"].ToString()), Convert.ToInt32(gvService.DataKeys[e.Row.RowIndex].Values[0])).ToList();
            ddlUser.DataSource = objServicelist1;
            ddlUser.DataTextField = "strServiceName";
            ddlUser.DataValueField = "intServiceId";
            ddlUser.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "--Select--";
            lst.Value = "0";
            ddlUser.Items.Insert(0, lst);


            //Added By Pranay Kumar on 11-Sept-2017 for Showing Query History
            int intQueryStatus = Convert.ToInt32(gvService.DataKeys[e.Row.RowIndex].Values[4]);
            LinkButton lnkbtnraise = (e.Row.FindControl("lnkbtnraise") as LinkButton);

            if (intQueryStatus == 1)//Query Raised
            {
                lnkbtnraise.Visible = true;
            }
            else if (intQueryStatus == 0) //IF Query Date is Blank
            {
                lnkbtnraise.Visible = false;
            }
            else if (intQueryStatus == 2) //If Query Date is Expired
            {
                lnkbtnraise.Visible = false;

            }

            List<ProposalDet> objProposalList = new List<ProposalDet>();
            objProp = new ProposalDet();
            objProp.strAction = "QD";
            objProp.strProposalNo = gvService.DataKeys[e.Row.RowIndex].Values[1].ToString();
            objProposalList = objService.ServicegetRaisedQueryDetails(objProp).ToList();
            HtmlGenericControl QueryHist = (HtmlGenericControl)e.Row.FindControl("QueryHist");
            HtmlGenericControl QueryHist1 = (HtmlGenericControl)e.Row.FindControl("QueryHist1");

            if (objProposalList.Count > 0)
            {
                string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th>Query Reference No.</th><th>User Name</th><th> Query Details</th><th>Date</th><th>Files</th></tr>";
                for (int i = 0; i < objProposalList.Count; i++)
                {
                    if (objProposalList[i].strFileName == null || objProposalList[i].strFileName == "")
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryStatus + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a target='' href='#'>--</a>" + "</td></tr>";
                    }
                    else
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryStatus + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a target='_blank' href='../../QueryFiles/Services/" + objProposalList[i].strFileName + "'>Download</a>" + "</td></tr>";
                    }
                }
                strHTMlQuery = strHTMlQuery + "</table>";
                QueryHist.InnerHtml = strHTMlQuery;
                QueryHist1.InnerHtml = strHTMlQuery;
            }


            HtmlGenericControl OrderList = (HtmlGenericControl)e.Row.FindControl("OrderList");
            HtmlGenericControl OrderList1 = (HtmlGenericControl)e.Row.FindControl("OrderList1");
            List<ServiceDetails> objOrderList = new List<ServiceDetails>();
            ServiceDetails objProp1 = new ServiceDetails();
            objProp1.STRACTION = "D";
            objProp1.strApplicationUnqKey = gvService.DataKeys[e.Row.RowIndex].Values[1].ToString();
            //PaymentOrderDetails(ServiceDetails objService)
            ServiceBusinessLayer objService2 = new ServiceBusinessLayer();
            objOrderList = objService2.PaymentOrderDetails(objProp1).ToList();
            if (objOrderList.Count > 0)
            {

                DataTable dt = CommonHelperCls.ConvertToDataTable<ServiceDetails>(objOrderList);
                DataView dv = new DataView(dt);
                dv.RowFilter = "strStatus = 'Success'";

                string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th> Date</th><th>Order No.</th><th>Amount</th></tr>";
                Decimal TotalAmt = 0;
                for (int i = 0; i < dv.ToTable().Rows.Count; i++)
                {

                    strHTMlQuery = strHTMlQuery + "<tr><td>" + dv.ToTable().Rows[i]["dtmCreatedOn"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchOrderNo"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchAmount"].ToString() + "</td></tr>";
                    TotalAmt = TotalAmt + Convert.ToDecimal(dv.ToTable().Rows[i]["vchAmount"]);

                }
                strHTMlQuery = strHTMlQuery + "<tr><td></td><td>Total</td><td>" + TotalAmt + "</td></tr></table>";
                OrderList.InnerHtml = strHTMlQuery;

                dv = new DataView(dt);
                dv.RowFilter = "strStatus = 'Pending'";
                TotalAmt = 0;
                strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th> Date</th><th>Order No.</th><th>Amount</th></tr>";
                for (int i = 0; i < dv.ToTable().Rows.Count; i++)
                {

                    strHTMlQuery = strHTMlQuery + "<tr><td>" + dv.ToTable().Rows[i]["dtmCreatedOn"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchOrderNo"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchAmount"].ToString() + "</td></tr>";
                    TotalAmt = TotalAmt + Convert.ToDecimal(dv.ToTable().Rows[i]["vchAmount"]);

                }
                strHTMlQuery = strHTMlQuery + "<tr><td></td><td>Total</td><td>" + TotalAmt + "</td></tr></table>";
                OrderList1.InnerHtml = strHTMlQuery;
                // QueryHist1.InnerHtml = strHTMlQuery;
            }


            // OrderList.InnerHtml = "<table class='table table-bordered table-hover'><tr><th>aaaa.</th><th>User Name</th><th>bbbb</th><th>Date</th><th>Files</th></tr></table>";
            //Ended By Pranay Kumar on 11-Sept-2017 for Showing Query History
            string strCurrQueryStatus = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[5]);
            LinkButton lbtnQueryDtls = (e.Row.FindControl("lbtnQueryDtls") as LinkButton);
            if (strCurrQueryStatus == "--")
            {
                lbtnQueryDtls.Visible = false;
                lbtnQueryDtls.Text = "--";
            }
            else if (strCurrQueryStatus == "Completed")
            {
                lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
                lbtnQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
            }
            else if (strCurrQueryStatus == "QUERY RAISED")
            {
                lbtnQueryDtls.Text = strCurrQueryStatus;
                lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
            }
            else if (strCurrQueryStatus == "QUERY RESPONDED")
            {
                lbtnQueryDtls.Text = strCurrQueryStatus;
                lbtnQueryDtls.CssClass = "label-warning label label-default";
                lbtnQueryDtls.Visible = false;
                lnkbtnraise.Text = strCurrQueryStatus;
                lnkbtnraise.CssClass = "label-warning label label-default";
            }
            if (gvService.DataKeys[e.Row.RowIndex].Values[2].ToString() == Session["UserId"].ToString())
            {
                //if (strCurrQueryStatus == "QUERY RAISED")
                //{
                //    btn.Visible = false;
                //}
                //else
                //{
                //    btn.Visible = true;
                //}

                //Changes by manoj kumar behera as suggest by ipicol when query revert date excide then take action byyton will enable whatever query status not matter//

                if (strCurrQueryStatus == "QUERY RAISED" && QueryValidDate.Date >= TodayDate.Date)
                {
                    btn.Visible = false;
                }
                else
                {
                    btn.Visible = true;
                }

                //Changes by manoj kumar behera as suggest by ipicol when query revert date excide then take action byyton will enable whatever query status not matter//

            }
            else
            {
                /*-------------------------------------------------------------*/
                //// If user is IPICOLADMIN the dispaly Take Action button
                //// But here ipicol admin can only forward the application.It can't do any approval,rejection or differ
                /*-------------------------------------------------------------*/
                if (Convert.ToString(Session["UserId"]) == "1157")
                {
                    btn.Visible = true;
                    drpStatus.Items.Clear();

                    drpStatus.DataSource = null;
                    drpStatus.DataTextField = "strStatus";
                    drpStatus.DataValueField = "intStatus";
                    drpStatus.DataBind();

                    ListItem lstad1 = new ListItem();
                    lstad1.Text = "--Select--";
                    lstad1.Value = "0";
                    drpStatus.Items.Insert(0, lstad1);
                    ListItem lstad2 = new ListItem();
                    lstad2.Text = "Forward";
                    lstad2.Value = "8";
                    drpStatus.Items.Insert(1, lstad2);

                }
                else
                {
                    btn.Visible = false;
                }
            }

            //Permission for road cutting changes by manoj kumar behera

            HtmlGenericControl div = e.Row.FindControl("insupload") as HtmlGenericControl;
            HtmlGenericControl div1 = e.Row.FindControl("resupload") as HtmlGenericControl;
            if (gvService.DataKeys[e.Row.RowIndex].Values[0].ToString() == "51")
            {
                div.Visible = true;
                div1.Visible = true;
            }
            else
            {
                div.Visible = false;
                div1.Visible = false;
            }
            //End of permission           
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
            HeaderCell.ColumnSpan = 6;
            HeaderCell.RowSpan = 1;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Style["background-color"] = "#F5F5F5";
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 4;
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

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
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
        string INSUploadname = "";
        string RESUploadname = "";
        Button btnSubmit = (Button)sender;
        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
        ServiceDetails objservice = new ServiceDetails();
        DataTable dtcontent = new DataTable();
        try
        {
            FileUpload docUpload = (FileUpload)btnSubmit.FindControl("docUpload");
            HiddenField hdnServiceId1 = (HiddenField)btnSubmit.FindControl("hdnServiceId");
            HiddenField hdnProposalId1 = (HiddenField)btnSubmit.FindControl("hdnProposalId");
            HiddenField hdnInvesterName1 = (HiddenField)btnSubmit.FindControl("hdnInvesterName");
            HiddenField hdnApplicationUnqKey1 = (HiddenField)btnSubmit.FindControl("hdnApplicationUnqKey");
            HiddenField hdnlevel1 = (HiddenField)btnSubmit.FindControl("hdnlevel");
            TextBox txtRemarks = (TextBox)btnSubmit.FindControl("txtRemarks");
            TextBox txtEstimatedAmt = (TextBox)btnSubmit.FindControl("txtEstimatedAmt");
            DropDownList drpStatus = (DropDownList)btnSubmit.FindControl("drpStatus");
            HiddenField hdnstrServiceName = (HiddenField)btnSubmit.FindControl("hdnstrServiceName");
            DropDownList ddlUser = (DropDownList)btnSubmit.FindControl("ddlUser");
            TextBox txtApplicationNo = (TextBox)btnSubmit.FindControl("txtApplicationNo");//Application no used for Legal meterology

            FileUpload fluApprovalCert = (FileUpload)btnSubmit.FindControl("fluApprovalCert");
            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + docUpload.FileName.Trim(), DateTime.Now);
            bool rtnval = IsFileValidFile(docUpload);
            if (rtnval == true)
            {
                if (docUpload.HasFile)
                {
                    int fileSize = docUpload.PostedFile.ContentLength;
                    if (Path.GetExtension(docUpload.FileName) != ".pdf")
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return;

            }

            //Permiision for road Cutting changes by manoj kumar behera

            if (hdnServiceId1.Value == "51")
            {
                FileUpload fluinsUpload = (FileUpload)btnSubmit.FindControl("fluinsUpload");
                string INSfilepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + fluinsUpload.FileName.Trim(), DateTime.Now);
                rtnval = IsFileValidFile(fluinsUpload);
                if (rtnval == true)
                {
                    if (fluinsUpload.HasFile)
                    {
                        int fileSize = fluinsUpload.PostedFile.ContentLength;
                        if (Path.GetExtension(fluinsUpload.FileName) != ".pdf")
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
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("../ApprovalDocs"));
                    if (!string.IsNullOrEmpty(fluinsUpload.FileName))
                    {
                        if (dir.Exists)
                        {
                            fluinsUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + INSfilepath));
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("../ApprovalDocs"));
                            fluinsUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + INSfilepath));
                        }
                        INSUploadname = INSfilepath;
                    }
                    else { INSUploadname = ""; }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                    return;
                }

                FileUpload fluresUpload = (FileUpload)btnSubmit.FindControl("fluresUpload");
                string RESfilepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + fluresUpload.FileName.Trim(), DateTime.Now);
                rtnval = IsFileValidFile(fluresUpload);
                if (rtnval == true)
                {
                    if (fluresUpload.HasFile)
                    {
                        int fileSize = fluresUpload.PostedFile.ContentLength;
                        if (Path.GetExtension(fluresUpload.FileName) != ".pdf")
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
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("../ApprovalDocs"));
                    if (!string.IsNullOrEmpty(fluresUpload.FileName))
                    {
                        if (dir.Exists)
                        {
                            fluresUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + RESfilepath));
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("../ApprovalDocs"));
                            fluresUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + RESfilepath));
                        }
                        RESUploadname = RESfilepath;
                    }
                    else { RESUploadname = ""; }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                    return;
                }
            }
            else
            {
                INSUploadname = "";
                RESUploadname = "";
            }

            //Permiision for road Cutting changes by manoj kumar behera end

            rtnval = IsFileValidFile(fluApprovalCert);
            if (rtnval == true)
            {
                string filepath1 = string.Format("{0:yyyy_MM_dd}" + "_" + hdnApplicationUnqKey1.Value + System.IO.Path.GetExtension(fluApprovalCert.FileName.ToString().Trim()), DateTime.Now);
                if (fluApprovalCert.HasFile)
                {
                    int fileSize = fluApprovalCert.PostedFile.ContentLength;

                    if (fileSize > (4 * 1024 * 1024))
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                        return;
                    }
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

                objservice.strAction = "V";
                objservice.intServiceId = Convert.ToInt32(hdnServiceId1.Value);
                objservice.strProposalId = hdnProposalId1.Value;
                objservice.strInvesterName = hdnInvesterName1.Value;
                objservice.strApplicationUnqKey = hdnApplicationUnqKey1.Value;
                if (drpStatus.SelectedValue.Trim() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Select Status !','" + Messages.TitleOfProject + "'); </script>", false);
                    return;
                }
                else
                {
                    objservice.intStatus = Convert.ToInt32(drpStatus.SelectedValue.Trim());
                    if (drpStatus.SelectedValue.ToString() == "2")
                    {
                        if (!fluApprovalCert.HasFile)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Approval Certificate !','" + Messages.TitleOfProject + "'); </script>", false);
                            return;
                        }
                    }
                }


                //else
                //{

                //}

                objservice.intActionTobeTakenBy = Convert.ToInt32(Session["UserId"]);
                if (txtEstimatedAmt.Text == "" || txtEstimatedAmt.Text == "0.00")
                {
                    objservice.intPaymentStatus = 1;
                    txtEstimatedAmt.Text = "0.00";
                }
                else { objservice.intPaymentStatus = 0; objservice.strPaymentAmount = txtEstimatedAmt.Text; }
                objservice.strReferenceFilename = Uploadname;
                objservice.strCertificateFilename = Uploadname1;
                objservice.VCHINSPECTIONFILENAME = INSUploadname;//Added By manoj Kumar Behera
                objservice.VCHRESTRATIONFILENAME = RESUploadname;//Added By manoj Kumar Behera
                objservice.strRemark = txtRemarks.Text;
                objservice.str_ApplicationNo = txtApplicationNo.Text;
                //  objservice.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                if (Convert.ToInt32(drpStatus.SelectedValue.Trim()) == 8)
                {
                    objservice.intActionTakenBy = Convert.ToInt32(Session["UserId"]);
                    objservice.intActionTobeTakenBy = Convert.ToInt32(ddlUser.SelectedValue);
                }
                else
                {
                    objservice.intActionTakenBy = Convert.ToInt32(Session["UserId"]);
                }
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
                    dtcontent = objInvService.GetSMSContent(objInvDet);
                    string strServiceName = "";
                    if (dtcontent.Rows.Count > 0)
                    {
                        strSubject = dtcontent.Rows[0]["vchEvent"].ToString().Replace("[Status]", drpStatus.SelectedItem.Text.Trim());
                        SMSContent = (dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", inestorName)).Replace("[ApplicationNo]", hdnApplicationUnqKey1.Value.ToString());
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

                    mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                    smsStatus = comm.SendSmsNew(mobile, SMSContent);
                    // FOR SMS and Mail Status Update
                    string str = comm.UpdateMailSMSStaus("Service", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), hdnServiceId1.Value, Convert.ToInt32(drpStatus.SelectedValue.Trim()), hdnApplicationUnqKey1.Value, SMSContent, strBody, mailStatus, smsStatus);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objServiceDet = null;
            dtcontent = null;
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

            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_FFF_tt}" + "_Query_Dept_" + Session["UserId"].ToString() + ".pdf", DateTime.Now);
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
                    string strDeptSMSSubject = "";
                    string strDeptMailSubject = "";
                    string strDeptSMSBody = "";
                    string[] toEmail = new string[1];


                    if (objProposalList.Count > 0)
                    {
                        mobile = Convert.ToString(objProposalList[0].MobileNo);
                        smsContent = Convert.ToString(objProposalList[0].strSMSContent);
                        toEmail[0] = Convert.ToString(objProposalList[0].EmailID);
                        strSubject = Convert.ToString(objProposalList[0].EmailSubject);
                        strBody = Convert.ToString(objProposalList[0].EmailBody);
                        strDeptSMSSubject = Convert.ToString(objProposalList[0].strDeptSMSSub);
                        strDeptSMSBody = Convert.ToString(objProposalList[0].strDeptSMSContent);
                        strDeptMailSubject = Convert.ToString(objProposalList[0].strDeptMailContent);

                        //comm.sendMail(strSubject, strBody, toEmail, true);
                        //comm.SendSms(mobile, smsContent);

                        //Added BY Pritiprangya on 25-Oct-2017
                        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                        smsStatus = comm.SendSmsNew(mobile, smsContent);
                        // FOR SMS and Mail Status Update

                        string str = comm.UpdateMailSMSStaus("ServiceQuery", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), hdnQryServiceId1.Value, 5, hdnQryApplicationUnqKey1.Value, smsContent, strBody, smsStatus, mailStatus);
                        // FOR SMS and Mail Status Update


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
                            objService1.strSubject = strDeptSMSSubject;
                            objService1.strBody = strDeptMailSubject;
                            objService1.smsContent = strDeptSMSBody;

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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return;

            }
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
                    //zip.AddFile(filePath, "QueryFiles");
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
    public string EncryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Encrypt(strQueryString, "m8s3e3k5");
    }


    //Begin of Changes By Manoj Kumar Behera For Getting District Name On 29-11-2019

    private string District(string Serviceid, string vchapplicationunqkey)
    {
        string District = "";
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_DISPLAYDISTRICT_SERVICEVIEWTAKEACTION";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_SERVICEID", Convert.ToInt32(Serviceid));
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", vchapplicationunqkey);
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", "DI");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd = null;
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                District = dt.Rows[0]["District"].ToString();
            }
            else
            {
                District = "NA";
            }
        }
        catch (Exception ex)
        {
            conn.Close();
            Console.WriteLine("Error" + ex);
        }
        return District;
    }

    //End of Changes By Manoj Kumar Behera For Getting District Name On 29-11-2019

   



}
