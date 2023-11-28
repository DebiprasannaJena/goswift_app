#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

#endregion

public partial class CSRGridStatus : SessionCheck
{
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    int intRecCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InsertCSRGridStatus();
            CSRGridSatus();
            tbldv.Visible = false;
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
    private void InsertCSRGridStatus()
    {
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string District = Request.QueryString["dist"];
        string Year = Request.QueryString["year"];
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";
        // string Type = "1";
         try
        {
        string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetCategoryWiseAmount/" + District + "/" + Year + "/" + UniqueKey;
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
                string strRes = o["GetCategoryWiseAmountResult"].ToString();
                string strFinalRes = strRes.Remove(strRes.Trim().Length - 3).Trim().Substring(3);
                JObject oj = JObject.Parse(strFinalRes);
                string str = oj["objAmountSpent"].ToString();
                DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
                //string strResult = (new StreamReader(stream)).ReadToEnd();
                //DataTable DynTable = JsonStringToDataTable(strResult);
                //string output = DynTable.Rows[0]["GetCategoryWiseAmountResult"].ToString();
                //string[] finalOut = output.Split(':');
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "DELETE FROM T_CSR_CategoryWiseAmountDtls_Admin";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                DataTable DynamicForm = new DataTable();
                DynamicForm.TableName = "MyCSRTable";
                DynamicForm.Columns.Add(new DataColumn("adminid"));
                DynamicForm.Columns.Add(new DataColumn("AmountSpent"));
                DynamicForm.Columns.Add(new DataColumn("CategoryName"));
                DynamicForm.Columns.Add(new DataColumn("dtmCreatedOn"));
                DynamicForm.Columns.Add(new DataColumn("vchYear"));
                DynamicForm.Columns.Add(new DataColumn("intDistrictId"));
                //DynamicForm.Columns.Add(new DataColumn("intMonthId"));
                for (int i = 0; i < DynTable.Rows.Count; i++)
                {
                    DataRow dr = DynamicForm.NewRow();
                    dr["adminid"] = Convert.ToInt32(Session["Userid"]);
                    dr["AmountSpent"] = DynTable.Rows[i]["AmountSpent"].ToString();
                    dr["CategoryName"] = DynTable.Rows[i]["CategoryName"].ToString();
                    dr["dtmCreatedOn"] = DateTime.Now.ToString("dd-MMM-yy");
                    dr["vchYear"] = Year;
                    dr["intDistrictId"] = District;
                    //dr["intMonthId"] = MONTHID;
                    DynamicForm.Rows.Add(dr);
                }
                string xmltable = GetSTRXMLResult(DynamicForm);
                cmd = new SqlCommand("USP_GRID_XML", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
                cmd.Parameters.AddWithValue("@P_ACTION", "CSG");
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
    private void CSRGridSatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "VCA";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objCSRStatus = objserviceDashboard.GetDashboardCSRCatDtls(objSWP).ToList();
            intRecCount = objCSRStatus.Count;
            if (objCSRStatus.Count > 0)
            {
                gvCSRStatus.DataSource = objCSRStatus;
                gvCSRStatus.DataBind();
                DisplayPaging();
            }
            else
            {
                gvCSRStatus.DataSource = null;
                gvCSRStatus.DataBind();
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
    private void DisplayPaging()
    {
        if (gvCSRStatus.Rows.Count > 0)
        {
            this.lblPaging1.Visible = true;
            lbtnAll1.Visible = true;
            if (gvCSRStatus.PageIndex + 1 == gvCSRStatus.PageCount)
            {
                this.lblPaging1.Text = "Results <b>" + gvCSRStatus.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging1.Text = "Results <b>" + gvCSRStatus.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvCSRStatus.Rows[0].Cells[0].Text) + (gvCSRStatus.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging1.Visible = false;
            lbtnAll1.Visible = false;
        }
    }
    #region Events of CSR Grid
    protected void gvCSRStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCSRStatus.PageIndex = e.NewPageIndex;
        CSRGridSatus();
    }
    protected void gvCSRStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvCSRStatus.PageIndex * this.gvCSRStatus.PageSize) + e.Row.RowIndex + 1);
        }
    }
    protected void lbtnAll1_Click(object sender, EventArgs e)
    {
        if (lbtnAll1.Text == "All")
        {
            lbtnAll1.Text = "Paging";
            gvCSRStatus.PageIndex = 0;
            gvCSRStatus.AllowPaging = false;
            CSRGridSatus();
        }
        else
        {
            lbtnAll1.Text = "All";
            gvCSRStatus.AllowPaging = true;
            CSRGridSatus();
        }
    }
    #endregion
    private void CSRGridSatus1()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "VCA";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objCSRStatus = objserviceDashboard.GetDashboardCSRCatDtls(objSWP).ToList();
            intRecCount = objCSRStatus.Count;
            if (objCSRStatus.Count > 0)
            {
                GridView1.DataSource = objCSRStatus;
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1);
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        CSRGridSatus1();
        tbldv.Visible = true;
        lblCaption.Text = "CSR Details in " + Request.QueryString["year"];
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