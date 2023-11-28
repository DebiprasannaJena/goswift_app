﻿#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Configuration;

#endregion

public partial class Portal_Dashboard_SPMGDeptwiseStatusdtls : SessionCheck
{
    #region global variable
    int intRecCount = 0;
    string SPMGStatus = "";
    CommonDashboardFunction DashboradComnFunction = new CommonDashboardFunction();
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (Request.QueryString["SPMGStatus"].ToString() != "")
        {
            if (Request.QueryString["Level"].ToString() == "1")
            {
                InsertSPMGGRIDStatus();
            }
            else if (Request.QueryString["Level"].ToString() == "2")
            {
                InsertSPMGGRIDDistrictStatus();
            }            
        }
    }
    public string MakeRandom(int pl)
    {
        string possibles = "0123456789";
        char[] passwords = new char[pl];
        Random rd = new Random();

        for (int i = 0; i < pl; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }
    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
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
    private void SPMGGridSatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(dept).ToString();
            if (Request.QueryString["Level"].ToString() == "1")
            {
                objSWP.strAction = "DEPT";
            }
            else if (Request.QueryString["Level"].ToString() == "2")
            {
                objSWP.strAction = "DEPTDIST";
            }
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.intYearId = Convert.ToInt32(Request.QueryString["Year"].ToString());
            objSWP.StrStatus = Request.QueryString["SPMGStatus"].ToString();
            objSWP.intDeptId = Convert.ToInt32(strdeptid);
            //objSWP.intDeptId = Convert.ToInt32("16");
            List<SWPDashboard> objSPMGStatus = objserviceDashboard.GetSPMGDetailService(objSWP).ToList();
            intRecCount = objSPMGStatus.Count;
            if (objSPMGStatus.Count > 0)
            {
                grdSPMGDtl.DataSource = objSPMGStatus;
                grdSPMGDtl.DataBind();
                DisplayPaging();
            }
            else
            {
                grdSPMGDtl.DataSource = null;
                grdSPMGDtl.DataBind();
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
    protected void grdSPMGDtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSPMGDtl.PageIndex = e.NewPageIndex;
        SPMGGridSatus();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            grdSPMGDtl.PageIndex = 0;
            grdSPMGDtl.AllowPaging = false;
            SPMGGridSatus();
        }
        else
        {
            lbtnAll.Text = "All";
            grdSPMGDtl.AllowPaging = true;
            SPMGGridSatus();
        }
    }
    protected void grdSPMGDtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.grdSPMGDtl.PageIndex * this.grdSPMGDtl.PageSize) + e.Row.RowIndex + 1);
        }
    }
    #region "Display Paging in Gridview..."
    protected void DisplayPaging()
    {
        if (grdSPMGDtl.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (grdSPMGDtl.PageIndex + 1 == grdSPMGDtl.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + grdSPMGDtl.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + grdSPMGDtl.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(grdSPMGDtl.Rows[0].Cells[0].Text) + (grdSPMGDtl.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
    private void SPMGGridSatus1()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(dept).ToString();
            if (Request.QueryString["Level"].ToString() == "1")
            {
                objSWP.strAction = "DEPT";
            }
            else if (Request.QueryString["Level"].ToString() == "2")
            {
                objSWP.strAction = "DEPTDIST";
            }
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.intYearId = Convert.ToInt32(Request.QueryString["Year"].ToString());
            objSWP.StrStatus = Request.QueryString["SPMGStatus"].ToString();
            objSWP.intDeptId = Convert.ToInt32(strdeptid);
            //objSWP.intDeptId = Convert.ToInt32("16");
            List<SWPDashboard> objSPMGStatus = objserviceDashboard.GetSPMGDetailService(objSWP).ToList();
            intRecCount = objSPMGStatus.Count;
            if (objSPMGStatus.Count > 0)
            {
                GridView1.DataSource = objSPMGStatus;
                GridView1.DataBind();
             
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
        SPMGGridSatus1();
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

    #region SPMG Portlet District Level Added By Manoj Kumar Behera on 29-08-2019

    private void InsertSPMGGRIDStatus()
    {
        try
        {            
            objserviceDashboard = new DashboardBusinessLayer();
            //Random number generate
            string strrandomgen = MakeRandom(10);
            var plainran = Encoding.UTF8.GetBytes(strrandomgen);
            string randno = Convert.ToBase64String(plainran);
            //Timestamp
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());
            string plunixtime = Convert.ToBase64String(plainut);

            //PasswordDigest
            string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();
            SHA256 mySHA256 = SHA256Managed.Create();
            string finalstr = GetSha256FromString(ranpss);
            byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
            string ranpss1 = Convert.ToBase64String(bytes);

            //Financial year
            string FinYear = Request.QueryString["Year"].ToString();
            SPMGStatus = Request.QueryString["SPMGStatus"].ToString();
            string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(dept).ToString();
            //strdeptid = "16";
            string serviceUrl = "https://164.100.58.41//odisha/restservices/RestServer.php?view=issuedetailsbydepartmentid";
            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear,
                Status = SPMGStatus,
                DepartmentID = strdeptid
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.ContentType = "application/json";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            using (var requestStream = webRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
            }

            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var responseData = reader.ReadToEnd();
                        webResponse.Close();

                        string strResult = responseData.ToString();
                        DataTable DynTable = DashboradComnFunction.JsonStringToDataTable(strResult);
                        if (DynTable.Rows.Count > 0)
                        {

                            DataTable DynamicForm = new DataTable();
                            DataTable PnlDt = new DataTable();
                            DynamicForm.TableName = "MySPMGTable";
                            DynamicForm.Columns.Add(new DataColumn("INT_DEPTID"));
                            DynamicForm.Columns.Add(new DataColumn("Project_Name"));
                            DynamicForm.Columns.Add(new DataColumn("Project_Department"));
                            DynamicForm.Columns.Add(new DataColumn("Type_Of_Issue"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Date"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Description"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Category"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Days"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Department"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Department_Type"));
                            DynamicForm.Columns.Add(new DataColumn("Name_Of_Investor"));
                            DynamicForm.Columns.Add(new DataColumn("DTM_CREATED_ON"));
                            DynamicForm.Columns.Add(new DataColumn("int_Year"));
                            DynamicForm.Columns.Add(new DataColumn("vch_Status"));
                            DynamicForm.Columns.Add(new DataColumn("INT_LEVEL"));
                            for (int i = 0; i < DynTable.Rows.Count; i++)
                            {
                                DataRow dr = DynamicForm.NewRow();
                                dr["INT_DEPTID"] = Convert.ToInt32(strdeptid);
                                dr["Project_Name"] = DynTable.Rows[i]["Project Name"].ToString();
                                dr["Project_Department"] = DynTable.Rows[i]["Project Department"].ToString();//
                                dr["Type_Of_Issue"] = DynTable.Rows[i]["Type Of Issue"].ToString();
                                dr["Issue_Date"] = DynTable.Rows[i]["Issue Date"].ToString();//
                                dr["Issue_Description"] = DynTable.Rows[i]["Issue Description"].ToString();//
                                dr["Issue_Category"] = DynTable.Rows[i]["Issue Category"].ToString();
                                dr["Pending_Days"] = DynTable.Rows[i]["Pending Days"].ToString();
                                dr["Pending_Department"] = DynTable.Rows[i]["Pending Department"].ToString();//
                                dr["Pending_Department_Type"] = DynTable.Rows[i]["Pending Department Type"].ToString();//
                                dr["Name_Of_Investor"] = DynTable.Rows[i]["Name Of Investor"].ToString();
                                dr["DTM_CREATED_ON"] = DateTime.Now.ToString("dd-MMM-yy");
                                dr["int_Year"] = FinYear;
                                dr["vch_Status"] = SPMGStatus;
                                dr["INT_LEVEL"] = 1;
                                DynamicForm.Rows.Add(dr);
                            }
                            string xmltable = GetSTRXMLResult(DynamicForm);
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    using (SqlCommand cmd = new SqlCommand("USP_GRID_XML"))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@P_XMLTABLE", SqlDbType.Xml).Value = xmltable;
                                        cmd.Parameters.Add("@P_INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(FinYear);
                                        cmd.Parameters.Add("@P_Status", SqlDbType.VarChar).Value = SPMGStatus;
                                        cmd.Parameters.Add("@P_INTLEVEL", SqlDbType.Int).Value = Convert.ToInt32(1);
                                        cmd.Parameters.Add("@P_INT_DEPTID", SqlDbType.Int).Value = Convert.ToInt32(strdeptid);
                                        cmd.Parameters.Add("@P_ACTION", SqlDbType.VarChar).Value = "SPMGDEPT";
                                        cmd.Connection = con;
                                        con.Open();
                                        PnlDt.Load(cmd.ExecuteReader());
                                        con.Close();
                                    }
                                    if (PnlDt.Rows.Count > 0)
                                    {
                                        List<SWPDashboard> list = new List<SWPDashboard>();
                                        for (int i = 0; i <= PnlDt.Rows.Count - 1; i++)
                                        {
                                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                                            objDashboardInfo1.ProjectName = (PnlDt.Rows[i]["Project_Name"]).ToString();
                                            objDashboardInfo1.Project_Department = (PnlDt.Rows[i]["Project_Department"]).ToString();
                                            objDashboardInfo1.Type_Of_Issue = (PnlDt.Rows[i]["Type_Of_Issue"]).ToString();
                                            objDashboardInfo1.Issue_Date = (PnlDt.Rows[i]["Issue_Date"]).ToString();
                                            objDashboardInfo1.Issue_Description = (PnlDt.Rows[i]["Issue_Description"]).ToString();
                                            objDashboardInfo1.IssueCategory = (PnlDt.Rows[i]["Issue_Category"]).ToString();
                                            objDashboardInfo1.Name_Of_Investor = (PnlDt.Rows[i]["Name_Of_Investor"]).ToString();
                                            objDashboardInfo1.Pending_Department = (PnlDt.Rows[i]["Pending_Department"]).ToString();
                                            objDashboardInfo1.Pending_Department_Type = (PnlDt.Rows[i]["Pending_Department_Type"]).ToString();
                                            objDashboardInfo1.PendingDays = (PnlDt.Rows[i]["Pending_Days"]).ToString();
                                            list.Add(objDashboardInfo1);
                                        }
                                        grdSPMGDtl.DataSource = list;
                                        grdSPMGDtl.DataBind();
                                        intRecCount = list.Count;
                                        DisplayPaging();
                                    }
                                    else
                                    {
                                        grdSPMGDtl.DataSource = null;
                                        grdSPMGDtl.DataBind();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    con.Close();
                                    Util.LogError(ex, "Dashboard");
                                    SPMGGridSatus();
                                }
                            }
                        }
                    }
                }
            }
        }      
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
            SPMGGridSatus();
        }
    }
    private void InsertSPMGGRIDDistrictStatus()
    {
        try
        {
            //Random number generate
            objserviceDashboard = new DashboardBusinessLayer();
            string strrandomgen = MakeRandom(10);
            var plainran = Encoding.UTF8.GetBytes(strrandomgen);
            string randno = Convert.ToBase64String(plainran);

            //Timestamp
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());
            string plunixtime = Convert.ToBase64String(plainut);

            //PasswordDigest
            string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();
            SHA256 mySHA256 = SHA256Managed.Create();
            string finalstr = GetSha256FromString(ranpss);
            byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
            string ranpss1 = Convert.ToBase64String(bytes);


            //Financial year
            string FinYear = Request.QueryString["Year"].ToString();
            SPMGStatus = Request.QueryString["SPMGStatus"].ToString();

            string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(dept).ToString();

            string serviceUrl = "https://esuvidha.gov.in/odishadi/restservices/RestServer.php?view=issuedetails";

            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear,
                Status = SPMGStatus,
                DepartmentID = strdeptid
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.ContentType = "application/json";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            using (var requestStream = webRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
            }
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {

                        var responseData = reader.ReadToEnd();
                        webResponse.Close();
                        string strResult = responseData.ToString();
                        DataTable DynTable = DashboradComnFunction.JsonStringToDataTable(strResult);
                        if (DynTable.Rows.Count > 0)
                        {

                            DataTable DynamicForm = new DataTable();
                            DataTable PnlDt = new DataTable();
                            DynamicForm.TableName = "MySPMGTable";                           
                            DynamicForm.Columns.Add(new DataColumn("INT_DEPTID"));
                            DynamicForm.Columns.Add(new DataColumn("Project_Name"));
                            DynamicForm.Columns.Add(new DataColumn("Project_Department"));
                            DynamicForm.Columns.Add(new DataColumn("Type_Of_Issue"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Date"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Description"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Category"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Days"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Department"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Department_Type"));
                            DynamicForm.Columns.Add(new DataColumn("Name_Of_Investor"));
                            DynamicForm.Columns.Add(new DataColumn("DTM_CREATED_ON"));
                            DynamicForm.Columns.Add(new DataColumn("int_Year"));
                            DynamicForm.Columns.Add(new DataColumn("vch_Status"));
                            DynamicForm.Columns.Add(new DataColumn("INT_LEVEL"));
                            for (int i = 0; i < DynTable.Rows.Count; i++)
                            {
                                DataRow dr = DynamicForm.NewRow();
                                dr["INT_DEPTID"] = Convert.ToInt32(strdeptid);
                                dr["Project_Name"] = DynTable.Rows[i]["Project Name"].ToString();
                                dr["Project_Department"] = DynTable.Rows[i]["Project Department"].ToString();//
                                dr["Type_Of_Issue"] = DynTable.Rows[i]["Type Of Issue"].ToString();
                                dr["Issue_Date"] = DynTable.Rows[i]["Issue Date"].ToString();//
                                dr["Issue_Description"] = DynTable.Rows[i]["Issue Description"].ToString();//
                                dr["Issue_Category"] = DynTable.Rows[i]["Issue Category"].ToString();
                                dr["Pending_Days"] = DynTable.Rows[i]["Pending Days"].ToString();
                                dr["Pending_Department"] = DynTable.Rows[i]["Pending Department"].ToString();//
                                dr["Pending_Department_Type"] = DynTable.Rows[i]["Pending Department Type"].ToString();//
                                dr["Name_Of_Investor"] = DynTable.Rows[i]["Name Of Investor"].ToString();
                                dr["DTM_CREATED_ON"] = DateTime.Now.ToString("dd-MMM-yy");
                                dr["int_Year"] = FinYear;
                                dr["vch_Status"] = SPMGStatus;
                                dr["INT_LEVEL"] = 2;
                                DynamicForm.Rows.Add(dr);
                            }
                            string xmltable = GetSTRXMLResult(DynamicForm);
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    using (SqlCommand cmd = new SqlCommand("USP_GRID_XML"))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@P_XMLTABLE", SqlDbType.Xml).Value = xmltable;
                                        cmd.Parameters.Add("@P_INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(FinYear);
                                        cmd.Parameters.Add("@P_Status", SqlDbType.VarChar).Value = SPMGStatus;
                                        cmd.Parameters.Add("@P_INTLEVEL", SqlDbType.Int).Value = Convert.ToInt32(2);
                                        cmd.Parameters.Add("@P_INT_DEPTID", SqlDbType.Int).Value = Convert.ToInt32(strdeptid);
                                        cmd.Parameters.Add("@P_ACTION", SqlDbType.VarChar).Value = "SPMGDEPT";
                                        cmd.Connection = con;
                                        con.Open();
                                        PnlDt.Load(cmd.ExecuteReader());
                                        con.Close();
                                    }
                                    if (PnlDt.Rows.Count > 0)
                                    {
                                        List<SWPDashboard> list = new List<SWPDashboard>();
                                        for (int i = 0; i <= PnlDt.Rows.Count - 1; i++)
                                        {
                                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                                            objDashboardInfo1.ProjectName = (PnlDt.Rows[i]["Project_Name"]).ToString();
                                            objDashboardInfo1.Project_Department = (PnlDt.Rows[i]["Project_Department"]).ToString();
                                            objDashboardInfo1.Type_Of_Issue = (PnlDt.Rows[i]["Type_Of_Issue"]).ToString();
                                            objDashboardInfo1.Issue_Date = (PnlDt.Rows[i]["Issue_Date"]).ToString();
                                            objDashboardInfo1.Issue_Description = (PnlDt.Rows[i]["Issue_Description"]).ToString();
                                            objDashboardInfo1.IssueCategory = (PnlDt.Rows[i]["Issue_Category"]).ToString();
                                            objDashboardInfo1.Name_Of_Investor = (PnlDt.Rows[i]["Name_Of_Investor"]).ToString();
                                            objDashboardInfo1.Pending_Department = (PnlDt.Rows[i]["Pending_Department"]).ToString();
                                            objDashboardInfo1.Pending_Department_Type = (PnlDt.Rows[i]["Pending_Department_Type"]).ToString();
                                            objDashboardInfo1.PendingDays = (PnlDt.Rows[i]["Pending_Days"]).ToString();
                                            list.Add(objDashboardInfo1);
                                        }
                                        grdSPMGDtl.DataSource = list;
                                        grdSPMGDtl.DataBind();
                                        intRecCount = list.Count;
                                        DisplayPaging();
                                    }
                                    else
                                    {
                                        grdSPMGDtl.DataSource = null;
                                        grdSPMGDtl.DataBind();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    con.Close();
                                    Util.LogError(ex, "Dashboard");
                                    SPMGGridSatus();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
            SPMGGridSatus();
        }
    }
    #endregion
}