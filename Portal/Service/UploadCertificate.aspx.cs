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
public partial class Portal_Service_UploadCertificate : SessionCheck
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
    string FormHeader = "";
    string FormFooter = "";
    int intAllignment = 0;
    string strUnqId = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDept();
            AutoselectDept();
            BindGridDetails();
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
    private void BindGridDetails()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objService1.strAction = "U";
        objService1.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
        objService1.Deptid = Convert.ToInt32(ddldept.SelectedValue);
        objService1.str_ApplicationNo = txtAppno.Text;
        objService1.strProposalId = txtProposalno.Text;
        objService1.strFromdate = txtFromdate.Text;
        objService1.strTodate = txtTodate.Text;
        objService1.intActionTobeTakenBy = Convert.ToInt32(Session["UserId"].ToString());
        gvService.DataSource = objService.ViewSErviceTakeActionDetails(objService1);
        gvService.DataBind();
        txtFromdate.Text = txtFromdate.Text;
        txtTodate.Text = txtTodate.Text;

    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindService();
    }
    private void BindService()
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
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GridView gvSummary = (GridView)e.Row.FindControl("gvIntimateSent");
            LinkButton btn = new LinkButton();
            btn = (LinkButton)e.Row.FindControl("LinkButton1");
            if (gvService.DataKeys[e.Row.RowIndex].Values[2].ToString() == Session["UserId"].ToString())
            {
                btn.Visible = true;
            }
            else
            {
                btn.Visible = false;
            } 
           
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
            HeaderCell.ColumnSpan = 2;
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
    #region "Approval add"
    /// <summary>
    /// Added By Subhasmita Behera on 01-Aug-2017 for add approval details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Uploadname = "";
        Button btnSubmit = (Button)sender;
        FileUpload docUpload = (FileUpload)btnSubmit.FindControl("docUpload");
        HiddenField hdnServiceId1 = (HiddenField)btnSubmit.FindControl("hdnServiceId");
        HiddenField hdnProposalId1 = (HiddenField)btnSubmit.FindControl("hdnProposalId");
        HiddenField hdnInvesterName1 = (HiddenField)btnSubmit.FindControl("hdnInvesterName");
        HiddenField hdnApplicationUnqKey1 = (HiddenField)btnSubmit.FindControl("hdnApplicationUnqKey");
        HiddenField hdnlevel1 = (HiddenField)btnSubmit.FindControl("hdnlevel");
   
        string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + docUpload.FileName, DateTime.Now);
        if (docUpload.HasFile)
        {
            if ((docUpload.FileName.Contains(".pdf")) || (docUpload.FileName.Contains(".jpg")) || (docUpload.FileName.Contains(".jpeg")) || (docUpload.FileName.Contains(".JPEG")) || (docUpload.FileName.Contains(".png")))
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("../Portal/ApprovalDocs"));
                if (!string.IsNullOrEmpty(docUpload.FileName))
                {
                    if (dir.Exists)
                    {
                        docUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("../Portal/ApprovalDocs"));
                        docUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));

                    }
                    Uploadname = filepath;
                }
                else { Uploadname = ""; }
                ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
                ServiceDetails objservice = new ServiceDetails();
                objservice.strAction = "C";
                objservice.intServiceId = Convert.ToInt32(hdnServiceId1.Value);
                objservice.strProposalId = hdnProposalId1.Value;
                objservice.strInvesterName = hdnInvesterName1.Value;
                objservice.strApplicationUnqKey = hdnApplicationUnqKey1.Value;
                objservice.intStatus = 8;
                objservice.intActionTakenBy = Convert.ToInt32(Session["UserId"]);


                objservice.strReferenceFilename = Uploadname;
                objservice.strCertificateFilename = "";

                objservice.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                string strRetVal = objServiceDet.UpdateServiceDet(objservice);
                string rawURL = Request.RawUrl;
                if (strRetVal == "4")
                {
                    string strShowMsg = "Certificate Uploaded Successfully !";

                    string ff = "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
                }
                else { ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Some error Occured', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false); }

            }
            else
            {
                string strmsg11 = "alert('Only  jpg or png or pdf file accepted!');";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
                return;
            }
        }
        
    }
   
    #endregion
}