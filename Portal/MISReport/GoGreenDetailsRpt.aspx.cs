using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_MISReport_GoGreenDetailsRpt : System.Web.UI.Page  
{
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Call the method to fetch and bind data to GridView
            BindGridview();
        }
    }

    protected void BindGridview()
    {
        try
        {
            var client = new RestClient("http://gridcodemo.demoapplication.in/api/Goswift/GetDetails");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic R3JpZGNvOkdyaWRjb0AxMjM0");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            string jsonResponse = response.Content.Replace("\\", "").Trim(new char[1] { '"' });
            List<DataModel> data = JsonConvert.DeserializeObject<List<DataModel>>(jsonResponse);

            GrdDetailsRpt.DataSource = data;
            GrdDetailsRpt.DataBind();
            GridViewExcel.DataSource = data;
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
        catch(Exception ex)
        {
            Util.LogError(ex, "GoGreenDetailsRpt");
        }
    }
    public class DataModel
    {
        [JsonProperty("Application_No")]
        public string Application_No { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Developer_Name")] // Adjusted to match the JSON key
        public string Developer_Name { get; set; }

        [JsonProperty("project")] // Adjusted to match the JSON key
        public string project { get; set; }

        [JsonProperty("CreatedOn")]
        public string CreatedOn { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }
        
    }

    protected void GrdDetailsRpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField HidDistrict = (HiddenField)e.Row.FindControl("HidDistrict");
            HyperLink hypDistrict = (HyperLink)e.Row.FindControl("hypDistrict");
            Label Status = (Label)e.Row.FindControl("Status");
            string dist = HidDistrict.Value;
            string status = Status.Text;
           
            StringBuilder strNavigateUrl = new StringBuilder();
            strNavigateUrl.Append("");
            strNavigateUrl.Append("GoGreenChildDetailsRpt.aspx");
            strNavigateUrl.Append("?District=");
            strNavigateUrl.Append(dist);
            strNavigateUrl.Append("&Status=");
            strNavigateUrl.Append(status);
            hypDistrict.NavigateUrl = "javascript:void(0);"; // Prevent the hyperlink from navigating directly
            hypDistrict.Attributes["onclick"] = "ViewModal('" + strNavigateUrl.ToString() + "')";

            // ShowHideHyperlink(hypstatuscount, Status, ProjectId, e.Row, HidDistrictId.Value, "ChildDistricRpt.aspx");

        }
    }
    private void DisplayPaging()
    {
        try
        {
            if (this.GrdDetailsRpt.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                GrdDetailsRpt.Visible = true;
            }
            if (this.GrdDetailsRpt.PageIndex + 1 == this.GrdDetailsRpt.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdDetailsRpt.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdDetailsRpt.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)GrdDetailsRpt.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((GrdDetailsRpt.PageSize - 1))) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenDetailsRpt");
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ReportDetails", GridViewExcel);
    }



    protected void GrdDetailsRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdDetailsRpt.PageIndex = e.NewPageIndex;
            BindGridview();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenDetailsRpt");
        }
    }

    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (LbtnAll.Text == "All")
            {
                LbtnAll.Text = "Paging";
                GrdDetailsRpt.PageIndex = 0;
                GrdDetailsRpt.AllowPaging = false;
                BindGridview();
            }
            else
            {
                LbtnAll.Text = "All";
                GrdDetailsRpt.AllowPaging = true;
                BindGridview();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenDetailsRpt");
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.ExportToExcel("ReportDetails", GridViewExcel, "GoGreen application number wise report ", LblSearchDetails.Text + "<br/>Report generation date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GoGreenDetailsRpt");
        }
    }
}