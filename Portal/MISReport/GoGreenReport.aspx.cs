using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_MISReport_GoGreenReport : System.Web.UI.Page
{
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Call the method to fetch and bind data to GridView
            BindDataToGridView();
        }
    }

    protected void BindDataToGridView(string districtName = null, string status = null,string Formdate=null,string Todate=null)
    {       
        try
        {           
            var client = new RestClient();
            string baseUrl = "http://gridcodemo.demoapplication.in/api/Goswift";

            // Construct the URL based on conditions
            if (districtName != null && status == null && Formdate == null && Todate == null)
            {
                client = new RestClient(baseUrl + "?DistrictId=" + districtName);
            }
            else if (status != null && districtName == null && Formdate == null && Todate == null)
            {
                client = new RestClient(baseUrl + "?Status=" + status);
            }
            else if (districtName != null && status != null && Formdate == null && Todate == null)
            {
                client = new RestClient(baseUrl + "?DistrictId=" + districtName + "&Status=" + status);
            }
            else if(districtName == null && status == null && Formdate!=null && Todate!=null)
            {
                client = new RestClient(baseUrl + "?FromDate=" + Formdate + "&ToDate=" + Todate);
            }
            else if (status != null && districtName == null && Formdate != null && Todate != null)
            {
                client = new RestClient(baseUrl + "?Status=" + status +"&FromDate=" + Formdate + "&ToDate=" + Todate);
            }
            else if (status == null && districtName != null && Formdate != null && Todate != null)
            {
                client = new RestClient(baseUrl + "?DistrictId=" + districtName + "&FromDate=" + Formdate + "&ToDate=" + Todate);
            }
            else if (status != null && districtName != null && Formdate != null && Todate != null)
            {
                client = new RestClient(baseUrl + "?DistrictId=" + districtName + "&Status=" + status + "&FromDate=" + Formdate + "&ToDate=" + Todate);
            }
            else
            {
                client = new RestClient(baseUrl);
            }

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic R3JpZGNvOkdyaWRjb0AxMjM0");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            string jsonResponse = response.Content.Replace("\\", "").Trim('"');
            List<DataModel> data = JsonConvert.DeserializeObject<List<DataModel>>(jsonResponse);

            var groupedData = data.OrderBy(d => d.DistrictName)
                      .GroupBy(d => new { d.DistrictName, d.ProjectName, d.DistrictId, d.ProjectId });

            DataTable dataTable = new DataTable();

            // Add columns to the DataTable
            dataTable.Columns.Add("DistrictId", typeof(int));
            dataTable.Columns.Add("DistrictName", typeof(string));
            dataTable.Columns.Add("ProjectId", typeof(int));
            dataTable.Columns.Add("ProjectName", typeof(string));         
            dataTable.Columns.Add("ApprovedCount", typeof(int));
            dataTable.Columns.Add("stsApprove", typeof(string));
            dataTable.Columns.Add("PendingCount", typeof(int));
            dataTable.Columns.Add("stsPending", typeof(string));
            dataTable.Columns.Add("RejectedCount", typeof(int));
            dataTable.Columns.Add("stsReject", typeof(string));
            dataTable.Columns.Add("FromDate", typeof(string));
            dataTable.Columns.Add("ToDate", typeof(string));
            dataTable.Columns.Add("TotalCount", typeof(int)); // New column for total count

            // Populate the DataTable with data from groupedData
            foreach (var group in groupedData)
            {
                // Initialize counts for each status category
                int approvedCount = 0;
                int pendingCount = 0;
                int rejectedCount = 0;
                string stsApprove = "";
                string stsPending = "";
                string stsReject = "";
                string FromDate = "";
                string ToDate = "";

                // Calculate counts for each status category within the group
                foreach (var item in group)
                {
                    if (item.Status == "Approved" )
                    {
                        approvedCount += item.status_count;
                        
                    }
                    else if (item.Status == "Pending" )
                    {
                        pendingCount += item.status_count;
                       
                    }
                    else if (item.Status == "Rejected" )
                    {
                        rejectedCount += item.status_count;
                       
                    } 
                    if(item.Approved== "Approved" || item.Approved ==null)
                    {
                        stsApprove = "Approved";
                    }
                    if (item.Pending == "Pending" || item.Pending == null)
                    {
                        stsPending = "Pending";
                    }
                    if (item.Rejected == "Rejected" || item.Rejected == null)
                    {
                        stsReject = "Rejected";
                    }
                    if(TxtFromdate.Text!="" && TxtFromdate.Text != null)
                    {
                        FromDate = TxtFromdate.Text;
                    }
                    if (TxtTodate.Text!="" && TxtTodate.Text!=null)
                    {
                        ToDate = TxtTodate.Text;
                    }
                }
                
                int totalCount = approvedCount + pendingCount + rejectedCount; // Calculate total count

                // Add the calculated counts to the DataTable
                dataTable.Rows.Add(group.Key.DistrictId,group.Key.DistrictName, group.Key.ProjectId, group.Key.ProjectName, approvedCount, stsApprove, pendingCount, stsPending, rejectedCount, stsReject, FromDate, ToDate, totalCount);
            }

            GrdGoGreenRpt.DataSource = dataTable;
            GrdGoGreenRpt.DataBind();
            GridViewExcel.DataSource = dataTable;
            GridViewExcel.DataBind();
            intRetVal = data.Count;
            if (data.Count > 0)
            {
                DisplayPaging();
            }
            else
            {
                lblPaging.Visible = false;
                LbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenreport");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {


    }

    // Define your custom data model class
    public class DataModel
    {
        [JsonProperty("DistrictId")]
        public int DistrictId { get; set; }

        [JsonProperty("DistrictName")]
        public string DistrictName { get; set; }

        [JsonProperty("PROJECT_MST_ID")] // To match the JSON key
        public int ProjectId { get; set; }

        [JsonProperty("Project_name")] // To match the JSON key
        public string ProjectName { get; set; }

        [JsonProperty("status_count")]
        public int status_count { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
        public string Approved { get; set; }
        public string Pending { get; set; }
        public string Rejected { get; set; }
        public int TotalCount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    private void DisplayPaging()
    {
        try
        {
            int startRowIndex = (GrdGoGreenRpt.PageIndex * GrdGoGreenRpt.PageSize) + 1;
            int endRowIndex = startRowIndex + GrdGoGreenRpt.Rows.Count - 1;
            endRowIndex = endRowIndex > intRetVal ? intRetVal : endRowIndex;

            if (this.GrdGoGreenRpt.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                GrdGoGreenRpt.Visible = true;

                if (this.GrdGoGreenRpt.PageIndex + 1 == this.GrdGoGreenRpt.PageCount)
                {
                    this.lblPaging.Text = "Results <b>" + startRowIndex + "</b> - </b>" + endRowIndex + "</b> of <b>" + endRowIndex + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results <b>" + startRowIndex + "</b>-<b>" + endRowIndex + "</b> of <b>" + endRowIndex + "</b>";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenreport");
        }
    }

    protected void GrdGoGreenRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdGoGreenRpt.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenreport");
        }
    }

    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (LbtnAll.Text == "All")
            {
                LbtnAll.Text = "Paging";
                GrdGoGreenRpt.PageIndex = 0;
                GrdGoGreenRpt.AllowPaging = false;
                BindDataToGridView();
            }
            else
            {
                LbtnAll.Text = "All";
                GrdGoGreenRpt.AllowPaging = true;
                BindDataToGridView();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenreport");
        }
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ReportDetails", GridViewExcel);
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.ExportToExcel("ReportDetails", GridViewExcel, "District wise project count details report ", LblSearchDetails.Text + "<br/>Report generation date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenreport");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string districtName = drpDristrict.SelectedValue;
        string status = ddlStatus.SelectedValue;
        string Formdate = TxtFromdate.Text;
        string Todate = TxtTodate.Text;
        if (districtName == "")
        {
            districtName = null;
        }
        if (status == "")
        {
            status = null;
        }
        if (Formdate == "")
        {
            Formdate = null;
        }
        if (Todate == "")
        {
            Todate = null;
        }

        BindDataToGridView(districtName, status, Formdate, Todate);
    }
}