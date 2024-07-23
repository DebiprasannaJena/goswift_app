using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RestSharp;


public partial class Portal_MISReport_GoGreenChildDistricwiseRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindDataToGridView();
        }
    }
    protected void BindDataToGridView()
    {
        try
        {
            if (Request.QueryString["DistrictId"] != null && Request.QueryString["Status"] != null)
            {
                // Retrieve DistrictId and Status and ProjectId from the query string
                string districtId = Request.QueryString["DistrictId"];
                string status = Request.QueryString["Status"];              
                string apiUrl = "http://gridcodemo.demoapplication.in/api/Goswift?Status=" + status + "&DistrictId=" + districtId;
                var client = new RestClient(apiUrl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Basic R3JpZGNvOkdyaWRjb0AxMjM0");
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                string jsonResponse = response.Content.Replace("\\", "").Trim(new char[1] { '"' });
                List<DataModel> data = JsonConvert.DeserializeObject<List<DataModel>>(jsonResponse);

                GrdChildRpt.DataSource = data;
                GrdChildRpt.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public class DataModel
    {
        [JsonProperty("DistrictId")]
        public int DistrictId { get; set; }

        [JsonProperty("DistrictName")]
        public string DistrictName { get; set; }

        [JsonProperty("PROJECT_MST_ID")] // Adjusted to match the JSON key
        public int ProjectId { get; set; }

        [JsonProperty("Project_name")] // Adjusted to match the JSON key
        public string ProjectName { get; set; }

        [JsonProperty("status_count")]
        public int status_count { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}