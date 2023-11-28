#region  PAGE INFO

#endregion

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
using System.Web;
using System.Web.UI;
#endregion

public partial class APAARequestsGrid : SessionCheck
{
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    int intRecCount = 0;
    string APAADrillType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InsertAppaGridStatus();
            APAAGridSatus();
        }
        tbldv.Visible = false;
    }

    private void InsertAppaGridStatus()
    {
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();

        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        //string District = string.IsNullOrEmpty(ddlAPAADistrict.SelectedValue) ? default(string) : ddlAPAADistrict.SelectedValue.ToString();
        //string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        //string Year = string.IsNullOrEmpty(ddlAppaYear.SelectedValue) ? default(string) : ddlAppaYear.SelectedValue.ToString();
        //string UniqueKey = Session["UID"].ToString();
        int District = Convert.ToInt32(Request.QueryString["dist"]);
        int Deptid = Convert.ToInt32(Request.QueryString["deptid"]);
        APAADrillType = Request.QueryString["APAAStatus"];
        string UniqueKey = "0";
        int Month = Convert.ToInt32(Request.QueryString["month"]);
        string Year = Request.QueryString["year"];
        int Type = Convert.ToInt32(Request.QueryString["Type"]);
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objlist = objserviceDashboard.CheckAppastatus(objSWP);
        //if (objlist.Count > 0)
        //{
        //    if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
        //    {
        //        Type = "0";
        //    }
        //    else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
        //    {
        //        Type = "1";
        //    }
        //    else//MSME
        //    {
        //        Type = "2";
        //    }
        //}
        try
        {
            string serviceUrl = "";
            if (Session["desId"].ToString() == "126")
            {
                serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPDataDetail/" + UniqueKey + "/" + District + "/" + 0 + "/" + 0 + "/" + Year + "/" + Month + "/" + APAADrillType;
            }
            else if (Session["desId"].ToString() == "101") //// DI User can view only MSME data (So type=2)- Added by Sushant Jena On dt:-01-Apr-2019
            {
                serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPDataDetail/" + UniqueKey + "/" + District + "/" + 0 + "/" + 2 + "/" + Year + "/" + Month + "/" + APAADrillType;
            }
            else
            {
                serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPDataDetail/" + UniqueKey + "/" + District + "/" + Deptid + "/" + Type + "/" + Year + "/" + Month + "/" + APAADrillType;
            }

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
                    string query = "DELETE FROM T_APAA_Objection_request_Admin";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    DataTable DynamicForm = new DataTable();
                    DynamicForm.TableName = "MyAPAATable";
                    DynamicForm.Columns.Add(new DataColumn("adminid"));
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
                        dr["adminid"] = Convert.ToInt32(Session["Userid"]);
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
                    cmd.Parameters.AddWithValue("@P_ACTION", "APG");
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
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objAPAAStatus = objserviceDashboard.GetDashboardAPAAGrid(objSWP).ToList();
            intRecCount = objAPAAStatus.Count;
            if (objAPAAStatus.Count > 0)
            {
                gvAPAAStatus.DataSource = objAPAAStatus;
                gvAPAAStatus.DataBind();
                DisplayPaging();
                if (APAADrillType != "E")
                {
                    gvAPAAStatus.Columns[4].Visible = false;
                }
                else
                {
                    gvAPAAStatus.Columns[4].Visible = true;
                }
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
    private void APAAGridSatus1()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            APAADrillType = Request.QueryString["APAAStatus"];
            objSWP.strAction = "APOB";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            List<SWPDashboard> objAPAAStatus = objserviceDashboard.GetDashboardAPAAGrid(objSWP).ToList();
            intRecCount = objAPAAStatus.Count;
            if (objAPAAStatus.Count > 0)
            {
                GridView1.DataSource = objAPAAStatus;
                GridView1.DataBind();
                DisplayPaging();
                if (APAADrillType != "E")
                {
                    GridView1.Columns[4].Visible = false;
                }
                else
                {
                    GridView1.Columns[4].Visible = true;
                }
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
        APAAGridSatus1();
        tbldv.Visible = true;
        lblCaption.Text = "IDCO POST ALLOTMENT APPLICATIONS Details in " + Request.QueryString["year"];
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