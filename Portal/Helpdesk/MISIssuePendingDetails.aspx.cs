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
using BusinessLogicLayer.HelpDesk;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class Portal_HelpDesk_MISIssuePendingDetails : System.Web.UI.Page
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    DataTable dt = new DataTable();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    IssueRegistration objswp = new IssueRegistration();
    ServiceDetails objService1 = new ServiceDetails();
    CommonHelperCls comm = new CommonHelperCls();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    private void BindGridView()
    {
        try
        {
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objlist = new List<IssueRegistration>();
            if (Request.QueryString["FrmDate"] != "")
            {
                objswp.vchFromDate = Request.QueryString["FrmDate"];
            }
            if (Request.QueryString["ToDate"] != "")
            {
                objswp.vchToDate = Request.QueryString["ToDate"];
            }
            if (Request.QueryString["MID"] == "0")
            {
                objswp.Action = "K";
                objswp.int_CategoryId = Convert.ToInt32(Request.QueryString["CID"]);
                objlist = objlayer.ViewIssueRegistrationMIS(objswp);
                gvService.DataSource = objlist;
                gvService.DataBind();
            }
            else if (Request.QueryString["MID"] == "2")
            {
                objswp.Action = "N";
                objswp.int_CategoryId = Convert.ToInt32(Request.QueryString["CID"]);
                objlist = objlayer.ViewIssueRegistrationMIS(objswp);
                gvService.DataSource = objlist;
                gvService.DataBind();
            }
            else if (Request.QueryString["MID"] == "3")
            {
                objswp.Action = "O";
                objswp.int_CategoryId = Convert.ToInt32(Request.QueryString["CID"]);
                objlist = objlayer.ViewIssueRegistrationMIS(objswp);
                GridView1.DataSource = objlist;
                GridView1.DataBind();

            }
            else
            {
                objswp.Action = "M";
                objswp.int_CategoryId = Convert.ToInt32(Request.QueryString["CID"]);
                objlist = objlayer.ViewIssueRegistrationMIS(objswp);
                gvService.DataSource = objlist;
                gvService.DataBind();
            }

        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
}