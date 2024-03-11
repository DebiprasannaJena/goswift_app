using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_MISReport_GoGreenChildDetailsRpt : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
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
            // Call the method to fetch and bind data to GridView
            BindGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }

    }
   
    protected void BindGrid(int intPageIndex, int intPageSize)
    {
        try
        {
            if (Request.QueryString["DistrictId"] != null && Request.QueryString["Status"] != null && Request.QueryString["ProjectId"] != null)
            {
                lblSearchDetails.Text = string.Empty;
                GrdDetilsChildRpt.DataSource = null;
                GrdDetilsChildRpt.DataBind();
                divExport.Visible = false;

                // Retrieve DistrictId and Status and ProjectId from the query string
                string district = Request.QueryString["DistrictId"];
                string distname= Request.QueryString["DistrictName"];
                string status = Request.QueryString["Status"];               
                string projectid = Request.QueryString["ProjectId"];
                string fromdate= Request.QueryString["FromDate"];
                string todate = Request.QueryString["ToDate"];

                string apiUrl = "http://gridcodemo.demoapplication.in/api/Goswift/GetDetails?Status=" + status + "&DistrictId=" + district+ "&ProjectId=" + projectid+ "&FromDate=" + fromdate + "&ToDate=" + todate;
                var client = new RestClient(apiUrl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Basic R3JpZGNvOkdyaWRjb0AxMjM0");
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);              
                string jsonResponse = response.Content.Replace("\\", "").Trim('"');
                List<DataModel> data = JsonConvert.DeserializeObject<List<DataModel>>(jsonResponse);
                // Apply pagination
                int startIndex = (intPageIndex - 1) * intPageSize;
                int endIndex = Math.Min(startIndex + intPageSize, data.Count);
                // Filter data based on pagination
                List<DataModel> filteredData = data.Skip(startIndex).Take(endIndex - startIndex).ToList();

                GrdDetilsChildRpt.DataSource = filteredData;
                GrdDetilsChildRpt.DataBind();

                if (filteredData.Count > 0)
                {
                    divExport.Visible = true;
                    ddlNoOfRec.Visible = true;
                    rptPager.Visible = true;
                    // Populate pagination controls
                    CommonFunctions.PopulatePager(rptPager, data.Count, intPageIndex, intPageSize);
                    // Show paging details in the label
                    int intStartIndex = ((intPageIndex - 1) * intPageSize) + 1;
                    int intEndIndex = intStartIndex + intPageSize - 1;
                    lblDetails.Text = intStartIndex + "-" + intEndIndex + " of " + data.Count;
                }
                else
                {
                    ddlNoOfRec.Visible = false;
                    rptPager.Visible = false;
                }
                StringBuilder strSearchDetails = new StringBuilder();
                if (!string.IsNullOrEmpty(Request.QueryString["DistrictId"]) && Request.QueryString["DistrictId"] != "0")
                {
                    strSearchDetails.Append("<strong>District - </strong>");
                    strSearchDetails.Append(distname);
                    strSearchDetails.Append("<br/>");
                }
                
                if (!string.IsNullOrEmpty(Request.QueryString["Status"]) && Request.QueryString["Status"] !="")
                {
                    strSearchDetails.Append("<strong>Status - </strong>");
                    strSearchDetails.Append(status);
                    strSearchDetails.Append("<br/>");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["FromDate"]))
                {
                    strSearchDetails.Append("<strong>From Date - </strong>");
                    strSearchDetails.Append(Convert.ToDateTime(Request.QueryString["FromDate"]).ToString("dd-MMM-yyyy"));                  
                    strSearchDetails.Append("<br/>");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ToDate"]))
                {
                    strSearchDetails.Append("<strong>To Date - </strong>");
                    strSearchDetails.Append(Convert.ToDateTime(Request.QueryString["ToDate"]).ToString("dd-MMM-yyyy"));
                    strSearchDetails.Append("<br/>");
                }
                
                lblSearchDetails.Text = strSearchDetails.ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenChildDetailsRpt");
        }
    }

    public class DataModel
    {
        [JsonProperty("Application_No")]//To match the JSON key
        public string Application_No { get; set; }

        [JsonProperty("Developer_Name")]//To match the JSON key
        public string Developer_Name { get; set; }

        [JsonProperty("project")] // To match the JSON key
        public string project { get; set; }

        [JsonProperty("CreatedOn")] // To match the JSON key
        public string CreatedOn { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        public string Total_Investment { get; set; }
        public string Total_Employment { get; set; }
        public string Location { get; set; }

    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        AddSerialNumberColumn();
        IncentiveCommonFunctions.ExportToExcel("GoGreenChildDetailsRpt", GrdDetilsChildRpt, "District wise report for Child Services", lblSearchDetails.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        AddSerialNumberColumn();
        IncentiveCommonFunctions.CreatePdf("GoGreenChildDetailsRpt", GrdDetilsChildRpt);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
   
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = (string)((sender as LinkButton).CommandArgument);
            BindGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenChildDetailsRpt");
        }
    }

    protected void GrdDetilsChildRpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Rowid = 0;           
            if (Convert.ToInt32(hdnPgindex.Value) > 1)
            {
                Rowid = (Convert.ToInt32(hdnPgindex.Value) - 1) * Convert.ToInt32(ddlNoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
            }
            else
            {
                Rowid = e.Row.DataItemIndex + 1;
            }
            e.Row.Cells[0].Text = Rowid.ToString();
            Label LblStatus = (e.Row.FindControl("LblStatus") as Label);
            if(LblStatus.Text == "Approved")
            {
                LblStatus.ForeColor = System.Drawing.Color.Green;
                LblStatus.Font.Bold = true;
            }
            else if(LblStatus.Text == "Pending")
            {
                LblStatus.ForeColor = System.Drawing.Color.DarkOrange;
                LblStatus.Font.Bold = true;
            }
            else if (LblStatus.Text == "Rejected")
            {
                LblStatus.ForeColor = System.Drawing.Color.Red;
                LblStatus.Font.Bold = true;
            }
        }
    }

    protected void ddlNoOfRec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = "1";
            BindGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenChildDetailsRpt");
        }
    }


    private void AddSerialNumberColumn()
    {
        // Add a column for serial number at the beginning of the GridView
        BoundField serialNumberColumn = new BoundField();
        serialNumberColumn.HeaderText = "Serial No.";
        serialNumberColumn.DataField = null; 
        GrdDetilsChildRpt.Columns.Insert(0, serialNumberColumn);

        // Loop through GridView rows to populate serial numbers
        for (int i = 0; i < GrdDetilsChildRpt.Rows.Count; i++)
        {
            GridViewRow row = GrdDetilsChildRpt.Rows[i];
            row.Cells[0].Text = (i + 1).ToString();
        }
    }
}