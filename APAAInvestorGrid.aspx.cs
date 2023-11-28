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
#endregion

public partial class APAAInvestorGrid : SessionCheck
{
   SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    int intRecCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InsertAppaGridStatus();
            APAAGridSatus();
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

    private void InsertAppaGridStatus()
    {
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string UniqueKey = Session["UID"].ToString();
        int District = Convert.ToInt32(Request.QueryString["dist"]);
        int Deptid = 0;
        string APAADrillType = Request.QueryString["APAAStatus"];
        //string UniqueKey = "0";
        int Month = Convert.ToInt32(Request.QueryString["month"]);
        int Year = Convert.ToInt32(Request.QueryString["year"]);
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["InvestorId"]);
        
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPDataDetail/" + UniqueKey + "/" + District + "/" + Deptid + "/" + Type + "/" + Year + "/" + Month + "/" + APAADrillType;
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
                    string strRes = o["getSWPDataDetailResult"].ToString();
                    string strFinalRes = strRes.Remove(strRes.Trim().Length - 3).Trim().Substring(3);
                    JObject oj = JObject.Parse(strFinalRes);
                    string str = oj["objDataDetail"].ToString();
                    DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "DELETE FROM T_APAA_Objection_request";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    DataTable DynamicForm = new DataTable();
                    DynamicForm.TableName = "MyAPAAInvTable";
                    DynamicForm.Columns.Add(new DataColumn("InvestorId"));
                    DynamicForm.Columns.Add(new DataColumn("ApplicationName"));
                    DynamicForm.Columns.Add(new DataColumn("IEName"));
                    DynamicForm.Columns.Add(new DataColumn("PartyName"));
                    DynamicForm.Columns.Add(new DataColumn("PendingDays"));
                    DynamicForm.Columns.Add(new DataColumn("RequestDate"));
                    DynamicForm.Columns.Add(new DataColumn("dtmCreatedon"));
                    DynamicForm.Columns.Add(new DataColumn("intYearId"));
                    DynamicForm.Columns.Add(new DataColumn("intDistrictId"));
                    DynamicForm.Columns.Add(new DataColumn("intMonthId"));
                    for (int i = 0; i < DynTable.Rows.Count; i++)
                    {
                        
                        DataRow dr = DynamicForm.NewRow();
                        dr["investorid"] = Convert.ToInt32(Session["InvestorId"]);
                        dr["ApplicationName"] = DynTable.Rows[i]["ApplicationName"].ToString();
                        dr["IEName"] = DynTable.Rows[i]["IEName"].ToString();
                        dr["PartyName"] = DynTable.Rows[i]["PartyName"].ToString();
                        dr["PendingDays"] = DynTable.Rows[i]["PendingDays"].ToString();
                        dr["RequestDate"] = DynTable.Rows[i]["RequestDate"].ToString();
                        dr["dtmCreatedon"] = DateTime.Now.ToString("dd-MMM-yy");
                        dr["intYearId"] = Year;
                        dr["intDistrictId"] = District;
                        dr["intMonthId"] = Month;
                        DynamicForm.Rows.Add(dr);
                   }
                    string xmltable = GetSTRXMLResult(DynamicForm);
                    cmd = new SqlCommand("USP_GRID_XML", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
                    cmd.Parameters.AddWithValue("@P_ACTION", "APGINV");
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
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

    private void APAAGridSatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "APOB";
            objSWP.intUserid = Convert.ToInt32(Session["InvestorId"]);
            List<SWPDashboard> objAPAAStatus = objserviceDashboard.GetDashboardAPAAGrid(objSWP).ToList();
            intRecCount = objAPAAStatus.Count;
            if (objAPAAStatus.Count > 0)
            {
                gvAPAAStatus.DataSource = objAPAAStatus;
                gvAPAAStatus.DataBind();
                DisplayPaging();
            }
            else
            {
                gvAPAAStatus.DataSource = null;
                gvAPAAStatus.DataBind();
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
    protected void gvAPAAStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAPAAStatus.PageIndex = e.NewPageIndex;
        APAAGridSatus();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvAPAAStatus.PageIndex = 0;
            gvAPAAStatus.AllowPaging = false;
            APAAGridSatus();
        }
        else
        {
            lbtnAll.Text = "All";
            gvAPAAStatus.AllowPaging = true;
            APAAGridSatus();
        }
    }
    protected void gvAPAAStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvAPAAStatus.PageIndex * this.gvAPAAStatus.PageSize) + e.Row.RowIndex + 1);
        }
    }
    #region "Display Paging in Gridview..."
    protected void DisplayPaging()
    {
        if (gvAPAAStatus.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvAPAAStatus.PageIndex + 1 == gvAPAAStatus.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvAPAAStatus.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvAPAAStatus.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvAPAAStatus.Rows[0].Cells[0].Text) + (gvAPAAStatus.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
}
