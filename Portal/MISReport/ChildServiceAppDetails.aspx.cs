using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RestSharp;
using System.Web.Script.Serialization;
using System.IO;

public partial class Portal_MISReport_ChildServiceAppDetails : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }

        if (!IsPostBack)
        {
            CommonFunctions.PopulatePageSize(ddlNoOfRec);
            hdnPgindex.Value = "1";
            if (!string.IsNullOrEmpty(Request.QueryString["hdn"]))
            {
                hdnPgindex.Value = Request.QueryString["hdn"];
            }
            else
            {
                hdnPgindex.Value = "1";
            }
            if (Request.QueryString["pSize"] != null)
            {
                ddlNoOfRec.SelectedValue = Request.QueryString["pSize"];
            }
            else
            {
                ddlNoOfRec.SelectedValue = "10";
            }

            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
    }

    #region Data Paging
    /// <summary>
    /// Click event for all the link button created for the paging control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = (string)((sender as LinkButton).CommandArgument);
            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
    }

    /// <summary>
    /// selected index change event for the dropdown that contains different size for the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNoOfRec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = "1";
            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
    }
    #endregion

    protected void grdService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Rowid = 0;
            string EncryptValue = "";
            if (Convert.ToInt32(hdnPgindex.Value) > 1)
            {
                Rowid = (Convert.ToInt32(hdnPgindex.Value) - 1) * Convert.ToInt32(ddlNoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
            }
            else
            {
                Rowid = e.Row.DataItemIndex + 1;
            }
            e.Row.Cells[0].Text = Rowid.ToString();

            // HyperLink hypApplicationNo = (HyperLink)e.Row.FindControl("hypApplicationNo");
            //hypApplicationNo.NavigateUrl = "../Service/ServiceDetailsView.aspx?ApplicationNo=" + hypApplicationNo.Text + "&ServiceId=" + Request.QueryString["SId"] + "&type=1";

            // Add Remarks by anil
            HiddenField HdnFieldRemarks = (HiddenField)e.Row.FindControl("HdnFieldRemarks");

            e.Row.ToolTip = HdnFieldRemarks.Value;


            // Changes By Manoj Kumar Behera

            HyperLink hypApplicationNo = (HyperLink)e.Row.FindControl("hypApplicationNo");
            HiddenField Hid_Applied_Date = (HiddenField)e.Row.FindControl("hdnAppliedDate");
            int Serviceid = Convert.ToInt32(Request.QueryString["SId"].ToString());

            if (Serviceid == 20)
            {
                EncryptValue = BPAS(hypApplicationNo.Text.Remove(0, 1), "5");
                hypApplicationNo.NavigateUrl = "" + ConfigurationManager.AppSettings["BPASRedirectionURL"].ToString() + EncryptValue + "";
            }
            else if (Serviceid == 29)
            {
                EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                EncryptValue = obj.Encrypt(hypApplicationNo.Text.Remove(0, 1), "m8s3e3k5");
                hypApplicationNo.NavigateUrl = "" + ConfigurationManager.AppSettings["GOIPASServiceViewURL"].ToString() + "?GId=" + EncryptValue + "";
            }
            else if (Serviceid == 62 || Serviceid == 63)
            {
                EncryptValue = FIRE(hypApplicationNo.Text.Remove(0, 1), Serviceid);
                hypApplicationNo.NavigateUrl = ConfigurationManager.AppSettings["FIREREDIRECTIONURL"].ToString() + "?Query=" + EncryptValue + "";
            }
            else if (Serviceid == 67 || Serviceid == 68)
            {
                EncryptValue = EXCISE(hypApplicationNo.Text.Remove(0, 1), Serviceid);
                hypApplicationNo.NavigateUrl = EncryptValue;
            }
            else if (Serviceid == 69)
            {
                EncryptValue = OSBC(hypApplicationNo.Text.Remove(0, 1));
                hypApplicationNo.NavigateUrl = EncryptValue;
            }
            else if (Serviceid == 73)
            {
                EncryptValue = EIT(hypApplicationNo.Text.Remove(0, 1), Serviceid);
                hypApplicationNo.NavigateUrl = EncryptValue;
            }
            else if (Serviceid == 25 || Serviceid == 26) // add by anil F&E
            {
                EncryptValue = FOREST(hypApplicationNo.Text.Remove(0, 1));
                hypApplicationNo.NavigateUrl = EncryptValue;
            }
            else if (Serviceid == 5 || Serviceid == 6 || Serviceid == 7 || Serviceid == 34 || Serviceid == 35 || Serviceid == 36 || Serviceid == 39 || Serviceid == 40 || Serviceid == 70 || Serviceid == 71 || Serviceid == 72) //F&B and Labour Service
            {
                CultureInfo culture = new CultureInfo("es-ES");
                try
                {
                    if (DateTime.Parse(Hid_Applied_Date.Value, culture).Date > DateTime.Parse("15/01/2021", culture).Date)
                    {
                       
                        EncryptValue = PAReSHRAM(hypApplicationNo.Text.Remove(0, 1));
                        hypApplicationNo.NavigateUrl = EncryptValue;
                    }
                    else
                    {
                        hypApplicationNo.NavigateUrl = "../Service/ServiceDetailsView.aspx?ApplicationNo=" + hypApplicationNo.Text.Remove(0,1) + "&ServiceId=" + Serviceid + "&type=1";
                    }
                }
                catch(Exception ex)
                {
                    Util.LogError(ex, ""+ Hid_Applied_Date.Value + "");
                    hypApplicationNo.NavigateUrl = "../Service/ServiceDetailsView.aspx?ApplicationNo=" + hypApplicationNo.Text.Remove(0, 1) + "&ServiceId=" + Serviceid + "&type=1";
                }
            }
            else
            {
                hypApplicationNo.NavigateUrl = "../Service/ServiceDetailsView.aspx?ApplicationNo=" + hypApplicationNo.Text.Remove(0, 1) + "&ServiceId=" + Serviceid + "&type=1";
            }
        }
    }

    private void BindGridView(int intPageIndex, int intPageSize)
    {
        lblSearchDetails.Text = string.Empty;
        grdService.DataSource = null;
        grdService.DataBind();
        divExport.Visible = false;
        string strFromDate = string.Empty;
        string strToDate = string.Empty;
        int intMonth =DateTime.Today.Month;
        if (intMonth == 1)
        {
            strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        else
        {
            strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + (DateTime.Today.Year).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = Request.QueryString["Action"],
            intPageSize = intPageSize,
            intIntPageIndex = intPageIndex,
            strToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? strToDate : Request.QueryString["tDate"],
            strFromDate = string.IsNullOrEmpty(Request.QueryString["fdate"]) ? strFromDate : Request.QueryString["fdate"],
            intServiceId = string.IsNullOrEmpty(Request.QueryString["SId"]) ? 0 : Convert.ToInt32(Request.QueryString["SId"]),
            intStatus = string.IsNullOrEmpty(Request.QueryString["status"]) ? 0 : Convert.ToInt32(Request.QueryString["status"]),
            intDistrictId = string.IsNullOrEmpty(Request.QueryString["dist"]) ? 0 : Convert.ToInt32(Request.QueryString["dist"])
        };

        List<Mis_ChildServiceDtls> lstChildServices = new List<Mis_ChildServiceDtls>();
        if (string.Equals(objSearch.strActionCode, "sd", StringComparison.OrdinalIgnoreCase))
        {
            lstChildServices = MisReportServices.View_ChildServices_ApplicationDtls_MISReport(objSearch);
        }
        else if (string.Equals(objSearch.strActionCode, "sdn", StringComparison.OrdinalIgnoreCase))
        {
            objSearch.strActionCode = "sd";
            lstChildServices = MisReportServices.View_ChildServices_ApplicationDtls_MISReport_New(objSearch);
        }
        else if (string.Equals(objSearch.strActionCode, "dd", StringComparison.OrdinalIgnoreCase))
        {
            objSearch.intDepartmentId = string.IsNullOrEmpty(Request.QueryString["intId"]) ? 0 : Convert.ToInt32(Request.QueryString["intId"]);
            lstChildServices = MisReportServices.View_ChildServices_District_Details_Rpt(objSearch);
        }
        grdService.DataSource = lstChildServices;
        grdService.DataBind();
        if (grdService.Rows.Count > 0)
        {
            divExport.Visible = true;
            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstChildServices[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

            /****************code to show paging details in the label************/
            int intPIndex = Convert.ToInt32(hdnPgindex.Value);
            int intStartIndex = 1, intEndIndex = 0;
            int intPSize = Convert.ToInt32(ddlNoOfRec.SelectedValue);
            intStartIndex = ((intPIndex - 1) * intPSize) + 1;
            if (intPSize == grdService.Rows.Count)
            {
                intEndIndex = intPSize * intPIndex;
            }
            else
            {
                intEndIndex = grdService.Rows.Count + (intPSize * (intPIndex - 1));

            }
            lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(lstChildServices[0].intRowCount).ToString();
            StringBuilder strSearchDetails = new StringBuilder();

            if ((!string.IsNullOrEmpty(Request.QueryString["intId"]) && Request.QueryString["intId"] != "0") && (string.Equals(objSearch.strActionCode, "dd", StringComparison.OrdinalIgnoreCase)))
            {
                strSearchDetails.Append("<strong>Department - </strong>");
                strSearchDetails.Append(lstChildServices[0].strDepartment);
                strSearchDetails.Append("<br/>");
            }
            else if (string.Equals(objSearch.strActionCode, "sd", StringComparison.OrdinalIgnoreCase))
            {
                strSearchDetails.Append("<strong>Department - </strong>");
                strSearchDetails.Append(lstChildServices[0].strDepartment);
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SId"]) && Request.QueryString["SId"] != "0")
            {
                strSearchDetails.Append("<strong>Service - </strong>");
                strSearchDetails.Append(lstChildServices[0].ServiceName);
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fDate"]))
            {
                strSearchDetails.Append("<strong>From Date - </strong>");
                strSearchDetails.Append(Convert.ToDateTime(Request.QueryString["fDate"]).ToString("dd-MMM-yyyy"));
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["tDate"]))
            {
                strSearchDetails.Append("<strong>To Date - </strong>");
                strSearchDetails.Append(Convert.ToDateTime(Request.QueryString["tDate"]).ToString("dd-MMM-yyyy"));
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["dist"]) && Request.QueryString["dist"] != "0")
            {
                strSearchDetails.Append("<strong>District - </strong>");
                strSearchDetails.Append(lstChildServices[0].strDistName);
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Status"]) && Request.QueryString["Status"] != "0")
            {
                string Status = Request.QueryString["Status"];
                strSearchDetails.Append("<strong>Status - </strong>");
                if (Status == "1")
                {
                    strSearchDetails.Append("Application pending in current period");
                }
                else if (Status == "2")
                {
                    strSearchDetails.Append("<span style='color: green;font-weight:bold;'>Approved</span>");
                }
                else if (Status == "3")
                {
                    strSearchDetails.Append("<span style='color: red;font-weight:bold;'>Rejected</span>");
                }
                else if (Status == "4")
                {
                    strSearchDetails.Append("Application Passed ORTPS Timeline");
                }
                else if (Status == "7")
                {
                    strSearchDetails.Append("Deferred");
                }
                else if (Status == "8")
                {
                    strSearchDetails.Append("Forwarded");
                }
                else if (Status == "9")
                {
                    strSearchDetails.Append("Total Pending applications(Opening Balance + Current period pending)");
                }
                else if (Status == "15")
                {
                    strSearchDetails.Append("Opening Balance");
                }
                strSearchDetails.Append("<br/>");
            }
            lblSearchDetails.Text = strSearchDetails.ToString();
        }
        else
        {
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("ChildServicesDetailsRpt", grdService, "User wise report for Child Services", lblSearchDetails.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
      
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ChildServicesDetailsRpt", grdService);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private string BPAS(string strApplicationKey, string demandtype)
    {
        DataTable dt = new DataTable();       
        string EncryptValue = "";
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
           
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_BPAS_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(0));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
           
            

            if (dt.Rows.Count > 0)
            {
                string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                var client = new RestClient("" + AppBmcUrl + "");
                var request = new RestRequest(Method.POST);
                request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "~::~" + dt.Rows[0]["VCH_EMAIL"].ToString() + "~::~" + dt.Rows[0]["VCH_INV_USERID"].ToString() + "~::~" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "~::~" + ConfigurationManager.AppSettings["GOSWIFTAPPLICATIONURL"].ToString() + "~::~" + demandtype + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string JSON = response.Content;
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                EncryptValue = dict["result"].ToString();
            }
            else
            {
                string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                var client = new RestClient("" + AppBmcUrl + "");
                var request = new RestRequest(Method.POST);
                request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + ConfigurationManager.AppSettings["GOSWIFTAPPLICATIONURL"].ToString() + "~::~" + demandtype + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string JSON = response.Content;
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                EncryptValue = dict["result"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
        return EncryptValue;
    }
    private string FIRE(string strApplicationKey, int intServiceid)
    {
        string EncryptValue = "";
        try
        {
            var client = new RestClient("" + ConfigurationManager.AppSettings["FIREVIEWSTATUSURL"].ToString() + "?applicationId=" + strApplicationKey + "&serviceId=" + intServiceid + "&source='GOSWIFT'");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            string JSON = response.Content;
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
            EncryptValue = dict["result"].ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
        return EncryptValue;
    }
    private string EXCISE(string strApplicationKey, int intServiceid)
    {
        string EncryptValue = "";
        try
        {
            if (intServiceid == 67)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISESRVIEWSTATUSURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"gosiwft_id\":\"" + strApplicationKey + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall" + intServiceid.ToString() + "", client.ToString(), response.StatusCode.ToString());
                    EncryptValue = "";
                }
                else
                {
                    EncryptValue = response.ResponseUri.ToString();
                }
            }
            else
            {
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISEGNSRVIEWSTATUSURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"gosiwft_id\":\"" + strApplicationKey + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall" + intServiceid.ToString() + "", client.ToString(), response.StatusCode.ToString());
                    EncryptValue = "";
                }
                else
                {
                    EncryptValue = response.ResponseUri.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
        return EncryptValue;
    }
    private string EIT(string strApplicationKey, int intServiceid)
    {
        string EncryptValue = "";
        try
        {
            EncryptValue = ConfigurationManager.AppSettings["MobileTowerViewStatusUrl"].ToString() + "?uniqid=" + strApplicationKey + "&ServiceId=" + intServiceid + "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
        return EncryptValue;
    }
    private string OSBC(string strApplicationKey)
    {
        string EncryptValue = "";
        try
        {
            EncryptValue = ConfigurationManager.AppSettings["OSBCVIEWSTATUSURL"].ToString() + "?Application_No=" + strApplicationKey + "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
        return EncryptValue;
    }

    private string PAReSHRAM(string strApplicationKey)
    {
        DataTable dt = new DataTable();
        string EncryptValue = "";
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
           
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_PAReSHRAM_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
               //cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", 0);
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);  
                  
            EncryptValue = "" + ConfigurationManager.AppSettings["PARESHRAMVIEWSTATUSURL"].ToString() + "?appln_id=" + strApplicationKey + "&pan=" + dt.Rows[0]["VCH_PAN"].ToString() + "&mobile_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
        return EncryptValue;
    }


    private string FOREST(string strApplicationKey)
    {
        DataTable dt = new DataTable();
        string EncryptValue = "";
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_FOREST_SERVICE_DISPLAY";
            cmd.Parameters.Clear();
           
            cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            EncryptValue = "" + ConfigurationManager.AppSettings["TreeTransitDetail"].ToString() +"?service_code="+ dt.Rows[0]["INT_SERVICEID"].ToString() +"&UserId="+ dt.Rows[0]["INT_INVESTOR_ID"].ToString() +"&appln_id="+ strApplicationKey +"";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceAppDetal");
        }
        return EncryptValue;
    }
}