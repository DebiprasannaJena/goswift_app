#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web;
#endregion

public partial class CICGGrid : SessionCheck
{
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    int intRecCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InsertCICGGridStatus();
            CICGGridSatus();
        }
    }

    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

    private void InsertCICGGridStatus()
    {
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        //string Department = string.IsNullOrEmpty(ddldeptCIF.SelectedValue) ? default(string) : ddldeptCIF.SelectedValue.ToString();
        //string Month = string.IsNullOrEmpty(ddlCICGMonth.SelectedValue) ? default(string) : ddlCICGMonth.SelectedValue.ToString();
        // string Year = string.IsNullOrEmpty(ddlYearCICG.SelectedValue) ? default(string) : ddlYearCICG.SelectedValue.ToString();
        
        int Department = Convert.ToInt32(Request.QueryString["Dept"]);
        int Month = Convert.ToInt32(Request.QueryString["month"]);
        int Year = Convert.ToInt32(Request.QueryString["Year"]);
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CICGServiceURL"] + "GetNotUploadedIns/" + Department + "/" + Month + "/" + Year;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    JObject o = JObject.Parse(strResult);
                    string strRes = o["GetNotUploadedInsResult"].ToString();
                    string strFinalRes = strRes.Remove(strRes.Trim().Length - 3).Trim().Substring(3);
                    JObject oj = JObject.Parse(strFinalRes);
                    string str = oj["objNotUploadedInspection"].ToString();
                    DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
                    //string strResult = (new StreamReader(stream)).ReadToEnd();
                    //DataTable DynTable = JsonStringToDataTable(strResult);
                    //string output = DynTable.Rows[0]["getSWPDataDetailResult"].ToString();
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "DELETE FROM T_CICG_GRID_Status";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    DataTable DynamicForm = new DataTable();
                    DynamicForm.TableName = "MyCICGTable";
                    DynamicForm.Columns.Add(new DataColumn("adminid"));
                    DynamicForm.Columns.Add(new DataColumn("IndustryName"));
                    DynamicForm.Columns.Add(new DataColumn("InspectorName"));
                    DynamicForm.Columns.Add(new DataColumn("TOTALHOUR"));
                    DynamicForm.Columns.Add(new DataColumn("dtmCreatedon"));
                    DynamicForm.Columns.Add(new DataColumn("intYearId"));
                    DynamicForm.Columns.Add(new DataColumn("intMonthId"));
                    DynamicForm.Columns.Add(new DataColumn("intDeptId"));
                    for (int i = 0; i < DynTable.Rows.Count; i++)
                    {

                        DataRow dr = DynamicForm.NewRow();
                        dr["adminid"] = Convert.ToInt32(Session["Userid"]);
                        dr["IndustryName"] = DynTable.Rows[i]["IndustryName"].ToString();
                        dr["InspectorName"] = DynTable.Rows[i]["InspectorName"].ToString();
                        dr["TOTALHOUR"] = DynTable.Rows[i]["TOTALHOUR"].ToString();
                        dr["dtmCreatedon"] = DateTime.Now.ToString("dd-MMM-yy");
                        dr["intYearId"] = Year;
                        dr["intMonthId"] = Month;
                        dr["intDeptId"] = Department;
                        DynamicForm.Rows.Add(dr);
                    }
                    string xmltable = GetSTRXMLResult(DynamicForm);
                    cmd = new SqlCommand("USP_GRID_XML", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
                    cmd.Parameters.AddWithValue("@P_ACTION", "CICG");
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = string.Empty;
        if (dtTable.Rows.Count > 0)
        {
            StringWriter sw = new StringWriter();
            dtTable.WriteXml(sw);
            strXMLResult = sw.ToString();
            sw.Close();
            sw.Dispose();
        }

        return strXMLResult;
    }

    private void CICGGridSatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "CICG";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objCICGStatus = objserviceDashboard.GetDashboardCICGGrid(objSWP).ToList();
            intRecCount = objCICGStatus.Count;
            if (objCICGStatus.Count > 0)
            {
                gvCICGStatus.DataSource = objCICGStatus;
                gvCICGStatus.DataBind();
                DisplayPaging();
            }
            else
            {
                gvCICGStatus.DataSource = null;
                gvCICGStatus.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }

    }
    protected void gvCICGStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCICGStatus.PageIndex = e.NewPageIndex;
        CICGGridSatus();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvCICGStatus.PageIndex = 0;
            gvCICGStatus.AllowPaging = false;
            CICGGridSatus();
        }
        else
        {
            lbtnAll.Text = "All";
            gvCICGStatus.AllowPaging = true;
            CICGGridSatus();
        }
    }
    protected void gvCICGStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvCICGStatus.PageIndex * this.gvCICGStatus.PageSize) + e.Row.RowIndex + 1);
        }
    }
    #region "Display Paging in Gridview..."
    protected void DisplayPaging()
    {
        if (gvCICGStatus.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvCICGStatus.PageIndex + 1 == gvCICGStatus.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvCICGStatus.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvCICGStatus.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvCICGStatus.Rows[0].Cells[0].Text) + (gvCICGStatus.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
    private void CICGGridSatus1()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "CICG";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objCICGStatus = objserviceDashboard.GetDashboardCICGGrid(objSWP).ToList();
            intRecCount = objCICGStatus.Count;
            if (objCICGStatus.Count > 0)
            {
                GridView1.DataSource = objCICGStatus;
                GridView1.DataBind();
                DisplayPaging();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }

    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        CICGGridSatus1();
        tbldv.Visible = true;
        lblCaption.Text = "CICG Details in " + Request.QueryString["year"];
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Details.xls"));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        viewTable.RenderControl(htw);
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}