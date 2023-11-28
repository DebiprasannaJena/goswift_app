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
using System.Net;

public partial class Portal_Service_ViewServiceApplication : SessionCheck
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
    int intRetVal = 0;
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

                BindDept();
                AutoselectDept();
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
                    if(objServicelist[0].Deptid.ToString() == "188")
                    {
                        ddldept.Enabled = true;
                    }
                    else
                    {
                        ddldept.Enabled = false;
                    }
                   // ddldept.Enabled = false;
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
        try
        {
            objService1.strAction = "E";
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
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        try
        {
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

            GridView gvSummary = (GridView)e.Row.FindControl("gvIntimateSent");
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);
            HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hlnkTkn");
            hyRcvd.NavigateUrl = "ApplicationDetails.aspx?ApplicationNo=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&ServiceId=" + gvService.DataKeys[e.Row.RowIndex].Values[0];
            LinkButton lnkCert = (LinkButton)e.Row.FindControl("lnkCert");

            HyperLink hplnkqrydtl = (HyperLink)e.Row.FindControl("hplnkqrydtl");

            if (gvService.DataKeys[e.Row.RowIndex].Values[4].ToString() != "NA" && gvService.DataKeys[e.Row.RowIndex].Values[5].ToString() == "2")
            {

                lnkCert.Visible = true;
            }
            else
            {
                lnkCert.Visible = false;
            }

            if (gvService.DataKeys[e.Row.RowIndex].Values[6].ToString() != "5" && gvService.DataKeys[e.Row.RowIndex].Values[6].ToString() != "6")
            {
                hplnkqrydtl.Visible = false;
            }
            else
            {
                hplnkqrydtl.NavigateUrl = "ApplicationStatusDetails.aspx?type=S&ApplicationNo=" + gvService.DataKeys[e.Row.RowIndex].Values[1].ToString();
                hplnkqrydtl.Visible = true;
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
            HeaderCell.ColumnSpan = 6;
            HeaderCell.RowSpan = 1;
            HeaderCell.ForeColor = System.Drawing.Color.Black;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Style["background-color"] = "#F5F5F5";
            HeaderGridRow.Cells.Add(HeaderCell);


            gvService.Controls[0].Controls.AddAt(0, HeaderGridRow);
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
    public static void DownLoadFileFromServer(string path, string file)
    {
        //This is used to get Project Location.
        string filePath = path;
        //This is used to get the current response.
        HttpResponse res = GetHttpResponse();
        res.Clear();
        res.AppendHeader("content-disposition", "attachment; filename=" + file);
        res.ContentType = "application/octet-stream";
        res.WriteFile(filePath);
        res.Flush();
       res.End();     
    }
    public static string ServerMapPath(string path)
    {
        return HttpContext.Current.Server.MapPath(path);
    }
    public static HttpResponse GetHttpResponse()
    {
        return HttpContext.Current.Response;
    }
    protected void lnkCert_Click(object sender, EventArgs e)
    {
      

        LinkButton lnk = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnk.NamingContainer;
        HiddenField hdnfileval = row.FindControl("hdnfileval") as HiddenField;
        try
        {
            
            string path = Server.MapPath("../ApprovalDocs/" + hdnfileval.Value);
            if (File.Exists(path))
            {
                DownLoadFileFromServer(path, hdnfileval.Value);//Download File 
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('File not found !','" + Messages.TitleOfProject + "');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    
    }
}