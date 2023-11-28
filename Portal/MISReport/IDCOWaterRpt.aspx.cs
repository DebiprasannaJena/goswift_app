using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class IDCOWaterRpt : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();   
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            try
            {
                BindDistrict();
                txtFromdate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "IDCOWaterMIS");
            }
        }
    }
    private void BindDistrict()
    {
        try
        {         
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            ddlDistrict.DataSource = objProjList;
            ddlDistrict.DataTextField = "vchDistName";
            ddlDistrict.DataValueField = "intDistId";
            ddlDistrict.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IDCOWaterMIS");
        }
    }
    private void FillGridview()
    {
        string strToDate = txtTodate.Text;
        string strFromDate = txtFromdate.Text;
        int districtid = 0;
        if (ddlDistrict.SelectedIndex > 0)
        {
            districtid = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        else
        {
            districtid = 0;
        }
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["GOIPASMISREPORT"].ToString() + "WaterAllotmentCountReport/" + districtid + "/" + strFromDate + "/" + strToDate;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                    string output = DynTable.Rows[0]["objWaterAllotmentCount"].ToString();
                    string[] finalOut = output.Split(':');

                    List<MIS_ChildServiceRpt> lstChildServices = new List<MIS_ChildServiceRpt>();
                    MIS_ChildServiceRpt objChildService = new MIS_ChildServiceRpt();
                    objChildService.strParentName = Convert.ToString("IDCO - Water Connection");
                    objChildService.strDeptName = Convert.ToString("Obtaining water connection");
                    objChildService.intCarryFwdPending = Convert.ToInt32(DynTable.Rows[0]["OpeningBalence"].ToString());
                    objChildService.intTotalApplication = Convert.ToInt32(DynTable.Rows[0]["TotalApplied"].ToString());
                    objChildService.intTotalApproved = Convert.ToInt32(DynTable.Rows[0]["TotalApprove"].ToString());
                    objChildService.intTotalRejected = Convert.ToInt32(DynTable.Rows[0]["TotalReject"].ToString());
                    objChildService.intTotalQueryRaised = Convert.ToInt32(DynTable.Rows[0]["TotalQuery"].ToString());
                    objChildService.intTotalPending = Convert.ToInt32(DynTable.Rows[0]["TotalPendingCurrent"].ToString());
                    objChildService.intAllTotalPending = Convert.ToInt32(DynTable.Rows[0]["TotalPending"].ToString());
                    objChildService.intTotalORTPSAtimelinePassed = Convert.ToInt32(DynTable.Rows[0]["Pending30day"].ToString());
                    objChildService.strDistName = Convert.ToString(ddlDistrict.SelectedItem.Text);
                    objChildService.intAvgDaysApproval = Convert.ToInt32(finalOut[1].ToString());
                    lstChildServices.Add(objChildService);
                    grdDepartment.DataSource = lstChildServices;
                    grdDepartment.DataBind();
                    if (grdDepartment.Rows.Count > 0)
                    {
                        GridViewRow gRowFooter = grdDepartment.FooterRow;
                        gRowFooter.Cells[1].Text = "Total";
                        gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intCarryFwdPending).ToString());
                        gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApplication).ToString());
                        gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApproved).ToString());
                        gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalRejected).ToString());
                        gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalQueryRaised).ToString());
                        gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalPending).ToString());
                        gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intAllTotalPending).ToString());
                        gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalORTPSAtimelinePassed).ToString());
                        gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(lstChildServices[0].intAvgDaysApprovalTotal.ToString());

                        StringBuilder strSearch = new StringBuilder();
                        strSearch.Append("<strong>Department - </strong>");
                        strSearch.Append(lstChildServices[0].strParentName);
                        strSearch.Append("<br/>");

                        strSearch.Append("<strong>Service - </strong>");
                        strSearch.Append(lstChildServices[0].strDeptName);
                        strSearch.Append("<br/>");

                        if (!string.IsNullOrEmpty(txtFromdate.Text))
                        {
                            strSearch.Append("<strong>From Date - </strong>");
                            strSearch.Append(Convert.ToDateTime(txtFromdate.Text).ToString("dd-MMM-yyyy"));
                            strSearch.Append("<br/>");
                        }
                        if (!string.IsNullOrEmpty(txtTodate.Text))
                        {
                            strSearch.Append("<strong>To Date - </strong>");
                            strSearch.Append(Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy"));
                            strSearch.Append("<br/>");
                        }
                        if (!string.IsNullOrEmpty(ddlDistrict.SelectedItem.Text) && ddlDistrict.SelectedValue != "0")
                        {
                            strSearch.Append("<strong>District - </strong>");
                            strSearch.Append(lstChildServices[0].strDistName);
                            strSearch.Append("<br/>");
                        }
                        lblSearchDetails.Text = strSearch.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IDCOWaterMIS");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGridview();
    }
 
    protected void grdDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["Action"] = "A";
            if (e.CommandName == "re")
            {
                mp1.Show();
                ViewState["Action"] = "A";
                FillPopupGrid();
            }
            else if (e.CommandName == "ap")
            {
                mp1.Show();
                ViewState["Action"] = "B";
                FillPopupGrid();
            }
            else if (e.CommandName == "rj")
            {
                mp1.Show();
                ViewState["Action"] = "C";
                FillPopupGrid();
            }
            else if (e.CommandName == "query")
            {
                mp1.Show();
                ViewState["Action"] = "D";
                FillPopupGrid();
            }
            if (e.CommandName == "cp" || e.CommandName == "p")
            {
                mp1.Show();
                ViewState["Action"] = "E";
                FillPopupGrid();
            }
            else if (e.CommandName == "ortps")
            {
                mp1.Show();
                ViewState["Action"] = "F";
                FillPopupGrid();
            }
            else
            {
                mp1.Hide();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IDCOWaterMIS");
        }
    }
    private void FillPopupGrid()
    {
        string strToDate = txtTodate.Text;
        string strFromDate = txtFromdate.Text;
        int districtid = 0;
        if (ddlDistrict.SelectedIndex > 0)
        {
            districtid = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        else
        {
            districtid = 0;
        }
        string json = "{" + "\"District\" : " + "\"" + districtid + "\","
                                           + "\"Drilldowntype\" : " + "\"" + ViewState["Action"].ToString() + "\","
                                           + "\"FromDate\" : " + "\"" + strFromDate + "\","
                                           + "\"ToDate\" : " + "\"" + strToDate + "\""
                                        + "}";
        try
        {
            string serviceUrl1 = ConfigurationManager.AppSettings["GOIPASMISREPORT"].ToString() + "WaterAllotmentDetailsReport";
            HttpWebRequest httpRequest1 = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl1));
            httpRequest1.Accept = "application/json";
            httpRequest1.ContentType = "application/json";
            httpRequest1.Method = "POST";
            using (var streamWriter = new StreamWriter(httpRequest1.GetRequestStream()))
            {
                //initiate the request
                var resToWrite = json;
                streamWriter.Write(resToWrite);
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (HttpWebResponse httpResponse1 = (HttpWebResponse)httpRequest1.GetResponse())
            {
                using (Stream stream1 = httpResponse1.GetResponseStream())
                {
                    string strResult1 = (new StreamReader(stream1)).ReadToEnd();
                    string Message = JObject.Parse(strResult1)["WaterAllotmentDetailsReportResult"].ToString();
                    DataTable dt = new DataTable();
                    dt.Reset();
                    dt = (DataTable)JsonConvert.DeserializeObject(Message, (typeof(DataTable)));
                    gvpopup.DataSource = dt;
                    gvpopup.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "IDCOWaterMIS");
        }
    }
   
}