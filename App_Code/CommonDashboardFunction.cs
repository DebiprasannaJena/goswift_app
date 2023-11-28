using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Text;
using System.Web.Script.Serialization;
using System.Net;
using System.Security.Cryptography;
using BusinessLogicLayer.Dashboard;

/// <summary>
/////Added by nibedita for dashboard commonfunction on 30-Nov-2017
/// </summary>
public class CommonDashboardFunction
{
     string SPMGStatus = "";

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

    public string GetSTRXMLResult(DataTable dtTable)
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
    public void YEARBIND(DropDownList ddlYear)
    {
        ddlYear.Items.Clear();
        ListItem lt = new ListItem();
        for (int i = DateTime.Now.Year; i >= 2016; i--)
        {
            lt = new ListItem();
            lt.Text = i.ToString();
            lt.Value = i.ToString();
            ddlYear.Items.Add(lt);
        }
        ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public void MONTHBIND(DropDownList ddlMonth)
    {
        ddlMonth.Items.Clear();
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
        }
    }

    public void FillFinYrPortlet()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();

        objSWP.strAction = "FY";
        List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();
        string[] myArray = objCICGFINYear.Select(i => i.ToString()).ToArray();// objCICGFINYear.ToArray();

        //Year.DataSource = objCICGFINYear;
        //Year.DataTextField = "Year";
        //ddl.DataValueField = "Year";
        //ddl.DataBind();
        if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 3)
        {
            string myArray1 = (Convert.ToInt32(DateTime.Now.Year.ToString()) - 1) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")));
        }
        else
        {
            string myArray2 = (Convert.ToInt32(DateTime.Now.Year.ToString())) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")) + 1);
        }
    }
    public static string GetFinancialYear(string cdate)
    {
        string finyear = "";
        DateTime dt = Convert.ToDateTime(cdate);
        int m = dt.Month;
        int y = dt.Year;
        if (m > 3)
        {
            finyear = y.ToString().Substring(2, 2) + "-" + Convert.ToString((y + 1)).Substring(2, 2);
            //get last  two digits (eg: 10 from 2010);
        }
        else
        {
            finyear = Convert.ToString((y - 1)).Substring(2, 2) + "-" + y.ToString().Substring(2, 2);
        }
        return finyear;
    }

    /// <summary>
    /// SPMG portlet created by radhika for scheduler
    /// </summary>
    /// <param name="strData"></param>
    /// <returns></returns>
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
    public void InsertSPMGGRIDStatus(int year)
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
        string FinYear = year.ToString();
        SPMGStatus = "1";

        string serviceUrl = "https://esuvidha.gov.in/odisha/restservices/RestServer.php?view=issuestatus";
        object input = new
        {
            RandomNonce = randno,
            TimeStamp = plunixtime,
            PasswordDigest = ranpss1,
            FinancialYear = FinYear,
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
        try
        {
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var responseData = reader.ReadToEnd();
                        webResponse.Close();
                        CommonDashboardFunction DashboradComnFunction = new CommonDashboardFunction();
                        string strResult = responseData.ToString();
                        DataTable DynTable = DashboradComnFunction.JsonStringToDataTable(strResult);
                        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        string query1 = "DELETE FROM T_SPMG_STATUSDETAILS where int_Year='" + FinYear + "'";
                        cmd = new SqlCommand(query1, con);
                        cmd.ExecuteNonQuery();

                        DataTable DynamicForm = new DataTable();
                        DynamicForm.TableName = "MySPMGTable";
                        DynamicForm.Columns.Add(new DataColumn("INT_ID"));
                        DynamicForm.Columns.Add(new DataColumn("Project_Name"));
                        DynamicForm.Columns.Add(new DataColumn("Type_Of_Issue"));
                        DynamicForm.Columns.Add(new DataColumn("Issue_Category"));
                        DynamicForm.Columns.Add(new DataColumn("Pending_Days"));
                        DynamicForm.Columns.Add(new DataColumn("Name_Of_Investor"));
                        DynamicForm.Columns.Add(new DataColumn("DTM_CREATED_ON"));
                        DynamicForm.Columns.Add(new DataColumn("int_Year"));
                        DynamicForm.Columns.Add(new DataColumn("vch_Status"));
                        for (int i = 0; i < DynTable.Rows.Count; i++)
                        {

                            DataRow dr = DynamicForm.NewRow();
                            dr["INT_ID"] = Convert.ToInt32(1);
                            dr["Project_Name"] = DynTable.Rows[i]["Project Name"].ToString();
                            dr["Type_Of_Issue"] = DynTable.Rows[i]["Type Of Issue"].ToString();
                            dr["Issue_Category"] = DynTable.Rows[i]["Issue Category"].ToString();
                            dr["Pending_Days"] = DynTable.Rows[i]["Pending Days"].ToString();
                            dr["Name_Of_Investor"] = DynTable.Rows[i]["Name Of Investor"].ToString();
                            dr["DTM_CREATED_ON"] = DateTime.Now.ToString("dd-MMM-yy");
                            dr["int_Year"] = FinYear;
                            dr["vch_Status"] = SPMGStatus;
                            DynamicForm.Rows.Add(dr);
                        }
                        string xmltable = GetSTRXMLResult(DynamicForm);
                        cmd = new SqlCommand("USP_GRID_XML", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
                        cmd.Parameters.AddWithValue("@P_ACTION", "SPMGSTATUS");
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
        }
        //}
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    
}
}