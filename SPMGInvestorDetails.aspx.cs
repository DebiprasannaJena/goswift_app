#region Namespaces
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

#endregion

public partial class SPMGInvestorDetails : SessionCheck
{
    #region global variable

    int intRecCount = 0;
    string SPMGStatus = "";
    CommonDashboardFunction DashboradComnFunction = new CommonDashboardFunction();
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["SPMGStatus"].ToString() != "")
        {
            InsertSPMGGRIDStatus();
            SPMGGridSatus();
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

    private void InsertSPMGGRIDStatus()
    {
        try
        {
            SqlCommand cmd;
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
            string InvestorID = Request.QueryString["InvestorId"].ToString();
            SPMGStatus = Request.QueryString["SPMGStatus"].ToString();

            //// SPMG Service Consume
            //string serviceUrl = "https://164.100.58.41//odisha/restservices/RestServer.php?view=issuedetailsbyinvestorid";
            string serviceUrl = "https://esuvidha.gov.in//odisha/restservices/RestServer.php?view=issuedetailsbyinvestorid";
            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear,
                InvestorID = InvestorID,               
                Status = SPMGStatus
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
                        if (strResult != "")
                        {
                            DataTable DynTable = DashboradComnFunction.JsonStringToDataTable(strResult);

                            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            string query1 = "DELETE FROM T_INVESTOR_SPMG_ISSUE_DTLS where int_Year='" + FinYear + "'";
                            cmd = new SqlCommand(query1, con);
                            cmd.ExecuteNonQuery();

                            DataTable DynamicForm = new DataTable();
                            DynamicForm.TableName = "MyINVSPMGTable";
                            DynamicForm.Columns.Add(new DataColumn("INT_ID"));
                            DynamicForm.Columns.Add(new DataColumn("Investor_Id"));
                            DynamicForm.Columns.Add(new DataColumn("Project_Name"));
                            DynamicForm.Columns.Add(new DataColumn("Project_Department"));
                            DynamicForm.Columns.Add(new DataColumn("Type_Of_Issue"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Date"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Description"));
                            DynamicForm.Columns.Add(new DataColumn("Issue_Category"));
                            DynamicForm.Columns.Add(new DataColumn("Name_Of_Investor"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Department"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Department_Type"));
                            DynamicForm.Columns.Add(new DataColumn("Pending_Days"));
                            DynamicForm.Columns.Add(new DataColumn("DTM_CREATED_ON"));
                            DynamicForm.Columns.Add(new DataColumn("int_Year"));
                            DynamicForm.Columns.Add(new DataColumn("vch_Status"));
                            for (int i = 0; i < DynTable.Rows.Count; i++)
                            {
                                DataRow dr = DynamicForm.NewRow();
                                dr["INT_ID"] = Convert.ToInt32(Session["InvestorId"]);
                                dr["Investor_Id"] = Convert.ToString(Session["UID"]);
                                dr["Project_Name"] = DynTable.Rows[i]["Project Name"].ToString();
                                dr["Project_Department"] = DynTable.Rows[i]["Project Department"].ToString();
                                dr["Type_Of_Issue"] = DynTable.Rows[i]["Type Of Issue"].ToString();
                                dr["Issue_Date"] = DynTable.Rows[i]["Issue Date"].ToString();
                                dr["Issue_Description"] = DynTable.Rows[i]["Issue Description"].ToString();
                                dr["Issue_Category"] = DynTable.Rows[i]["Issue Category"].ToString();
                                dr["Name_Of_Investor"] = DynTable.Rows[i]["Name Of Investor"].ToString();
                                dr["Pending_Department"] = DynTable.Rows[i]["Pending Department"].ToString();
                                dr["Pending_Department_Type"] = DynTable.Rows[i]["Pending Department Type"].ToString();
                                dr["Pending_Days"] = DynTable.Rows[i]["Pending Days"].ToString();
                                dr["DTM_CREATED_ON"] = DateTime.Now.ToString("dd-MMM-yy");
                                dr["int_Year"] = FinYear;
                                dr["vch_Status"] = SPMGStatus;
                                DynamicForm.Rows.Add(dr);
                            }

                            string xmltable = GetSTRXMLResult(DynamicForm);
                            cmd = new SqlCommand("USP_GRID_XML", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
                            cmd.Parameters.AddWithValue("@P_ACTION", "SPMGINV");
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void SPMGGridSatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "SPMGD";
            // objSWP.intUserid = Convert.ToInt32(Session["InvestorId"]);
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objSWP.intYearId = Convert.ToInt32(Request.QueryString["Year"].ToString());
            objSWP.StrStatus = Request.QueryString["SPMGStatus"].ToString();
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
    protected void grdSPMGDtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.grdSPMGDtl.PageIndex * this.grdSPMGDtl.PageSize) + e.Row.RowIndex + 1);
        }
    }

    #region "Display Paging in Gridview..."

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
}