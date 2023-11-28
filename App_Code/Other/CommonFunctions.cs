using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Data.SqlClient;
using CSMPDK_3_0;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;

using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security;
using System.Globalization;


using System.Web.UI;


/// <summary>
/// Summary description for CommonFunction
/// </summary>
public class CommonFunctions
{

    public CommonFunctions()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void AddColumnsToGridView(GridView gv, DataTable table)
    {
        foreach (DataColumn column in table.Columns)
        {
            BoundField field = new BoundField();
            field.DataField = column.ColumnName;
            field.HeaderText = column.ColumnName;
            gv.Columns.Add(field);
        }
    }
    public string GetCurrentFinacialYear(int intEndMonth)
    {
        string strFinYear = "";
        if (DateTime.Now.Month <= intEndMonth)
        {
            strFinYear = (DateTime.Now.Year - 1).ToString() + "-" + DateTime.Now.Year.ToString();

        }
        else
        {

            strFinYear = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString();
        }
        return strFinYear;

    }
    public string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = "";
        if ((dtTable != null))
        {
            if (dtTable.Rows.Count > 0)
            {
                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw);
                strXMLResult = sw.ToString();
                sw.Close();
                sw.Dispose();
            }
        }

        return strXMLResult;
    }
    public DataTable ToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

        DataTable table = new DataTable();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            table.Columns.Add(prop.Name, prop.PropertyType);
        }
        object[] values = new object[props.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            table.Rows.Add(values);
        }
        return table;
    }
    #region "To Get button and Tab"
    /// <summary>
    /// Author     :Barsa Pradhan
    /// Created On : 11-Feb-2015
    /// </summary>
    public static string GetButtonTabId(string strAction, string strPath)
    {
        string str = "";
        try
        {
            CommonDLL objComnDll = new CommonDLL();
            string strQry = "EXEC USP_POR_ADMIN_BUTTON_TAB_DETAILS '" + strAction + "' , '" + strPath + "'";
            str = Convert.ToString(objComnDll.ExeScalar("ConnectionString", strQry, 1));
        }
        catch (Exception ex)
        {
        }
        return str;
    }
    #endregion
    #region "To Get Global Link"
    /// <summary>
    /// Author     :Barsa Pradhan
    /// Created On : 11-Feb-2015
    /// </summary>
    public static string GetGlinkName(int Glinkid)
    {
        CommonDLL objCmmDll = new CommonDLL();
        string StrQry = "";
        string ButtonName = "";
        try
        {
            StrQry = "SELECT nvchGLinkName FROM M_ADM_GlobalLink WHERE intGLinkId=" + Glinkid + "";
            ButtonName = objCmmDll.ExeScalar("ConnectionString", StrQry, 0).ToString();
        }
        catch (Exception ex)
        {
        }
        return ButtonName;
    }
    #endregion
    #region "To Get Primary Link"
    /// <summary>
    /// Author     :Barsa Pradhan
    /// Created On : 11-Feb-2015
    /// </summary>
    public static string GetPlinkName(int Plinkid)
    {
        CommonDLL objCmmDll = new CommonDLL();
        string StrQry = "";
        string ButtonName = "";
        try
        {
            StrQry = "SELECT nvchPlinkName FROM M_ADM_PrimaryLink WHERE intpLinkId=" + Plinkid + "";
            ButtonName = objCmmDll.ExeScalar("ConnectionString", StrQry, 0).ToString();
        }
        catch (Exception ex)
        {
        }
        return ButtonName;
    }
    #endregion
    #region Get rights permission for User]
    /// <summary>
    /// Author     :Barsa Pradhan
    /// Created On : 11-Feb-2015
    /// </summary>
    public static int GetAccessPer(int UserId, int PlinkId)
    {
        CommonDLL objCmmDll = new CommonDLL();
        int intReturn = 0;
        string strQuery = "";
        try
        {
            strQuery = "SELECT tinAccess from M_POR_LinkAccess where bitStatus<>'0' AND intUserID='" + UserId + "' and intPlinkId='" + PlinkId + "'";
            intReturn = Convert.ToInt32(objCmmDll.ExeScalar("ConnectionString", strQuery, 1));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return intReturn;
    }
    #endregion
    public string GenderDisplay(string strgn)
    {
        string strResult = "";
        if (strgn == "M")
        {
            strResult = "Male";
        }
        else if (strgn == "F")
        {
            strResult = "Female";
        }
        else
        {
            strResult = "Others";
        }
        return strResult;
    }
    public String changeCurrencyToWords(String numb, bool isCurrency, string currencyName, string currencyPoints)
    {
        return changeToWords(numb, isCurrency, currencyName, currencyPoints);


    }
    private String changeToWords(String numb, bool isCurrency, string currencyName, string currencyPoints)
    {


        String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = (isCurrency) ? ("Only") : ("");


        try
        {
            int decimalPlace = numb.IndexOf("."); if (decimalPlace > 0)
            {


                wholeNo = numb.Substring(0, decimalPlace);


                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {


                    andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents
                    endStr = (isCurrency) ? ("Paisa " + endStr) : ("");


                    pointStr = translateCents(points);


                }


            }
            val = String.Format("{0} {1} {2}{3} {4} {5}", currencyName, translateWholeNumber(wholeNo).Trim(), andStr, pointStr, currencyPoints, endStr);

        }
        catch { ;} return val;


    }
    private String translateWholeNumber(String number)
    {


        string word = "";


        try
        {


            bool beginsZero = false;//tests for 0XX


            bool isDone = false;//test if already translated
            double dblAmt = (Convert.ToDouble(number));


            //if ((dblAmt > 0) && number.StartsWith("0"))
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric
                beginsZero = number.StartsWith("0");


                int numDigits = number.Length;


                int pos = 0;//store digit grouping


                String place = "";//digit grouping name:hundres,thousand,etc...
                switch (numDigits)
                {


                    case 1://ones' range


                        word = ones(number);
                        isDone = true;


                        break;


                    case 2://tens' range


                        word = tens(number);
                        isDone = true;


                        break;


                    case 3://hundreds' range


                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";


                        break;


                    case 4://thousands' range
                    case 5:


                        pos = (numDigits % 4) + 1;


                        place = " Thousand ";
                        break;


                    case 6://Lakhs' range
                    case 7:


                        pos = (numDigits % 6) + 1;


                        place = " Lakh ";
                        break;


                    case 8://Crores' range
                    case 9:


                        pos = (numDigits % 8) + 1;


                        place = " Crore ";
                        break;


                    case 10://Arabs range
                    case 11:


                        pos = (numDigits % 10) + 1;
                        place = " Arab ";


                        break;




                    //add extra case options for anything above Billion...
                    default:


                        isDone = true;
                        break;


                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)


                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));


                    //check for trailing zeros
                    if (beginsZero) word = " and " + word.Trim();


                }


                //ignore digit grouping names
                if (word.Trim().Equals(place.Trim())) word = "";


            }


            String Result = word.Trim();
            Result = Result.Replace("and Hundred", "");
            Result = Result.Replace("and Thousand", "");
            Result = Result.Replace("and Lakh", "");
            Result = Result.Replace("and Crore", "");
            Result = Result.Replace(" and ", " ");


            word = Result;
        }
        catch { ;} return word.Trim();


    }
    private String tens(String digit)
    {


        int digt = Convert.ToInt32(digit);
        String name = null; switch (digt)
        {


            case 10:
                name = "Ten";


                break;
            case 11:


                name = "Eleven";
                break;


            case 12:
                name = "Twelve";


                break;
            case 13:


                name = "Thirteen";
                break;


            case 14:
                name = "Fourteen";


                break;
            case 15:


                name = "Fifteen";
                break;


            case 16:
                name = "Sixteen";


                break;
            case 17:


                name = "Seventeen";
                break;


            case 18:
                name = "Eighteen";


                break;
            case 19:


                name = "Nineteen";
                break;


            case 20:
                name = "Twenty";


                break;
            case 30:


                name = "Thirty";
                break;


            case 40:
                name = "Fourty";


                break;
            case 50:


                name = "Fifty";
                break;


            case 60:
                name = "Sixty";


                break;
            case 70:


                name = "Seventy";
                break;


            case 80:
                name = "Eighty";


                break;
            case 90:


                name = "Ninety";
                break;


            default:
                if (digt > 0)
                {
                    name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));


                }
                break;


        }
        return name;


    }
    private String ones(String digit)
    {
        int digt = Convert.ToInt32(digit);


        String name = "";
        switch (digt)
        {
            case 1:


                name = "One";
                break;


            case 2:
                name = "Two";


                break;
            case 3:


                name = "Three";
                break;


            case 4:
                name = "Four";


                break;
            case 5:


                name = "Five";
                break;


            case 6:
                name = "Six";


                break;
            case 7:


                name = "Seven";
                break;


            case 8:
                name = "Eight";


                break;
            case 9:


                name = "Nine";
                break;


        }
        return name;


    }
    private String translateCents(String cents)
    {
        String cts = "", digit = "", engOne = "";
        for (int i = 0; i < cents.Length; i++)
        {
            digit = cents[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Zero";
            }
            else
            {
                engOne = ones(digit);
            }
            cts += " " + engOne;
            if (i == 1)
            {
                if (Convert.ToInt32(cents) > 9 && Convert.ToInt32(cents) < 21)
                {
                    cts = " " + tens(cents);
                }
                else
                {
                    digit = cents[0].ToString();
                    cts = " " + tens(digit + "0");
                    digit = cents[1].ToString();
                    cts += " " + ones(digit);
                }
            }
        }
        return cts;
    }
    public string GetRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path;
    }
    public string GenerateRandomString(int length)
    {
        //It will generate string with combination of small,capital letters and numbers
        char[] charArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        string randomString = string.Empty;
        Random objRandom = new Random();
        for (int i = 0; i < length; i++)
        {
            //Don't Allow Repetation of Characters
            int x = objRandom.Next(1, charArr.Length);
            if (!randomString.Contains(charArr.GetValue(x).ToString()))
                randomString += charArr.GetValue(x);
            else
                i--;
        }
        return randomString;
    }

    public bool SendEmail(string strSubject, string strBody, Attachment attchfile, string[] toEmail, bool enbleSSl)
    {
        bool Res = false;
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smptp"].ToString());
            mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"].ToString());
            for (int i = 0; i < toEmail.Length; i++)
            {
                mail.To.Add(toEmail[i]);
            }
            mail.Subject = strSubject;
            mail.Body = strBody;
            if (attchfile != null)
            {
                mail.Attachments.Add(attchfile);
            }
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = enbleSSl;
            mail.IsBodyHtml = true;

            //Warning: do not use this in production code!
            ServicePointManager.ServerCertificateValidationCallback =
    delegate(object s, X509Certificate certificate,
             X509Chain chain, SslPolicyErrors sslPolicyErrors)
    { return true; };

            //These are need tobe comment in PROD server
            // END 
            SmtpServer.Send(mail);
            Res = true;
        }
        catch
        {
            Res = false;
        }
        return Res;
    }
    public void SendSms(string strMobNo, string Sms)
    {

        string fb_url = "http://123.63.33.43/blank/sms/user/urlsmstemp.php?username=kapbulk&pass=kapbulk@user!123&senderid=KAPMSG&message=" + Sms + "&dest_mobileno=" + strMobNo + "&response=Y";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fb_url);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        var response = (HttpWebResponse)request.GetResponse();
        var reader = new StreamReader(response.GetResponseStream());
        var objText = reader.ReadToEnd();
        //var json = JObject.Parse(objText);
        //var array = json["data"];
        //JArray jsonDat = JArray.Parse(array.ToString());
    }

    public void SendSingleSms(string smsContent, string MobNo)
    {

        String username = ConfigurationManager.AppSettings["username"].ToString();
        String password = ConfigurationManager.AppSettings["password"].ToString();
        String senderid = ConfigurationManager.AppSettings["senderid"].ToString();
        String smsgtwy = ConfigurationManager.AppSettings["SMSgateway"].ToString();
        String message = smsContent;
        String mobileNo = MobNo;
        Stream dataStream;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(smsgtwy);
        request.ProtocolVersion = HttpVersion.Version10;
        //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
        ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98;DigExt)";
        request.Method = "POST";
        String smsservicetype = "singlemsg"; //For single message.
        String query = "username=" + HttpUtility.UrlEncode(username) +
        "&password=" + HttpUtility.UrlEncode(password) +
        "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
        "&content=" + HttpUtility.UrlEncode(message) +
            //"&content=" + message +
        "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
        "&senderid=" + HttpUtility.UrlEncode(senderid);
        byte[] byteArray = Encoding.ASCII.GetBytes(query);
        //byte[] byteArray = Encoding.Unicode.GetBytes(query);
        //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(query);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;

        dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        String Status = ((HttpWebResponse)response).StatusDescription;
        dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();
        reader.Close();
        dataStream.Close();
        response.Close();
    }
    public void SendBulkSms(string smsContent, string MobNos)
    {

        String username = ConfigurationManager.AppSettings["username"].ToString();
        String password = ConfigurationManager.AppSettings["password"].ToString();
        String senderid = ConfigurationManager.AppSettings["senderid"].ToString();
        String smsgtwy = ConfigurationManager.AppSettings["SMSgateway"].ToString();
        String message = smsContent;
        String mobileNos = MobNos;// "9856XXXXX, 9856XXXXX "
        Stream dataStream;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(smsgtwy);
        request.ProtocolVersion = HttpVersion.Version10;
        ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
        //((HttpWebRequest)request).UserAgent="Mozilla/4.0 (compatible; MSIE 5.0; Windows 98;DigExt)";
        request.Method = "POST";
        String smsservicetype = "bulkmsg"; // for bulk msg
        String query = "username=" + HttpUtility.UrlEncode(username) + "&password=" + HttpUtility.UrlEncode(password) + "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) + "&content=" + HttpUtility.UrlEncode(message) + "&bulkmobno=" + HttpUtility.UrlEncode(mobileNos) + "&senderid=" + HttpUtility.UrlEncode(senderid);
        byte[] byteArray = Encoding.ASCII.GetBytes(query);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;
        dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        String Status = ((HttpWebResponse)response).StatusDescription;
        dataStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(dataStream);
        string responseFromServer = reader.ReadToEnd();
        reader.Close();
        dataStream.Close();
        response.Close();
    }
    public DataTable MergeAll(IList<DataTable> tables, String primaryKeyColumn)
    {
        if (!tables.Any())
            throw new ArgumentException("Tables must not be empty", "tables");
        if (primaryKeyColumn != null)
            foreach (DataTable t in tables)
                if (!t.Columns.Contains(primaryKeyColumn))
                    throw new ArgumentException("All tables must have the specified primarykey column " + primaryKeyColumn, "primaryKeyColumn");

        if (tables.Count == 1)
            return tables[0];

        DataTable table = new DataTable("TblUnion");
        table.BeginLoadData(); // Turns off notifications, index maintenance, and constraints while loading data
        foreach (DataTable t in tables)
        {
            table.Merge(t); // same as table.Merge(t, false, MissingSchemaAction.Add);
        }
        table.EndLoadData();

        if (primaryKeyColumn != null)
        {
            // since we might have no real primary keys defined, the rows now might have repeating fields
            // so now we're going to "join" these rows ...
            var pkGroups = table.AsEnumerable()
                .GroupBy(r => r[primaryKeyColumn]);
            var dupGroups = pkGroups.Where(g => g.Count() > 1);
            foreach (var grpDup in dupGroups)
            {
                // use first row and modify it
                DataRow firstRow = grpDup.First();
                foreach (DataColumn c in table.Columns)
                {
                    if (firstRow.IsNull(c))
                    {
                        DataRow firstNotNullRow = grpDup.Skip(1).FirstOrDefault(r => !r.IsNull(c));
                        if (firstNotNullRow != null)
                            firstRow[c] = firstNotNullRow[c];
                    }
                }
                // remove all but first row
                var rowsToRemove = grpDup.Skip(1);
                foreach (DataRow rowToRemove in rowsToRemove)
                    table.Rows.Remove(rowToRemove);
            }
        }

        return table;
    }



    public static DataTable ConvertToDataTable1<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        if (table.Columns.Contains("ExtensionData"))
        {
            table.Columns.Remove("ExtensionData");
        }
        return table;

    }

    #region added by Ritika Lath on 7th September 2017

    /// <summary>
    /// function to bind the repeater for paging
    /// </summary>
    /// <param name="rptPager">repeater control</param>
    /// <param name="recordCount">total count of records</param>
    /// <param name="currentPage">current index of page</param>
    /// <param name="PageSize">size of page selected</param>
    public static void PopulatePager(Repeater rptPager, int recordCount, int currentPage, int PageSize)
    {
        List<ListItem> pages = new List<ListItem>();
        int startIndex, endIndex;
        int pagerSpan = 5;

        if (PageSize > recordCount)
        {
            PageSize = recordCount;
        }


        //Calculate the Start and End Index of pages to be displayed.
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        if (currentPage > pagerSpan % 2)
        {
            if (currentPage == 2)
            {
                endIndex = 5;
            }
            else
            {
                endIndex = currentPage + 2;
            }
        }
        else
        {
            endIndex = (pagerSpan - currentPage) + 1;
        }

        if (endIndex - (pagerSpan - 1) > startIndex)
        {
            startIndex = endIndex - (pagerSpan - 1);
        }

        if (endIndex > pageCount)
        {
            endIndex = pageCount;
            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        }

        //Add the First Page Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("First", "1"));
        }

        //Add the Previous Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        }

        //Add the Next Button.
        if (currentPage < pageCount)
        {
            pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
        }

        //Add the Last Button.
        if (currentPage != pageCount)
        {
            pages.Add(new ListItem("Last", pageCount.ToString()));
        }

        if (pages.Count == 1)
        {
            rptPager.DataSource = null;

        }
        else
        {
            rptPager.DataSource = pages;
        }
        rptPager.DataBind();
        // return pages;
    }

    /// <summary>
    /// Populate page size dropdown above gridview
    /// </summary>
    /// <param name="ddl"></param>
    public static void PopulatePageSize(DropDownList ddl)
    {
        object[] objArray = new object[]  
        {
        5, 10,
        20,50,100,500
     };

        ddl.DataSource = objArray;
        ddl.DataBind();

    }
    #endregion
    private byte[] GetPasswordBytesNew()
    {
        // The real password characters is stored in System.SecureString
        // Below code demonstrates on converting System.SecureString into Byte[]
        // Credit: http://social.msdn.microsoft.com/Forums/vstudio/en-US/f6710354-32e3-4486-b866-e102bb495f86/converting-a-securestring-object-to-byte-array-in-net

        byte[] ba = null;
        //char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        // ba = Encoding.GetEncoding("UTF-8").GetBytes(chars);
        //if (secureTextBox1.Text.Length == 0)
        ba = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        //else
        //{
        //Convert System.SecureString to Pointer
        SecureString _secureEntry = new SecureString();
        string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //foreach (char c in s)
        //{
        //    _secureEntry.AppendChar(c);
        //}
        //IntPtr unmanagedBytes = Marshal.SecureStringToGlobalAllocAnsi(_secureEntry);
        //try
        //{
        //    // You have to mark your application to allow unsafe code
        //    // Enable it at Project's Properties > Build
        //    unsafe
        //    {
        //        byte* byteArray = (byte*)unmanagedBytes.ToPointer();

        //        // Find the end of the string
        //        byte* pEnd = byteArray;
        //        while (*pEnd++ != 0) { }
        //        // Length is effectively the difference here (note we're 1 past end) 
        //        int length = (int)((pEnd - byteArray) - 1);

        //        ba = new byte[length];

        //        for (int i = 0; i < length; ++i)
        //        {
        //            // Work with data in byte array as necessary, via pointers, here
        //            byte dataAtIndex = *(byteArray + i);
        //            ba[i] = dataAtIndex;
        //        }
        //    }
        //}
        //finally
        //{
        //    // This will completely remove the data from memory
        //    Marshal.ZeroFreeGlobalAllocAnsi(unmanagedBytes);
        //}
        ////}

        byte[] array = new byte[32];
        Random random = new Random();
        random.NextBytes(array);


        return System.Security.Cryptography.SHA256.Create().ComputeHash(array);
    }
    public string AESEncrypt(string stringToEncrypt)
    {
        try
        {
            byte[] passwordBytes = GetPasswordBytesNew();
            return AESSH256.Encrypt(stringToEncrypt.Trim(), passwordBytes);

            //return AES.Encrypt(stringToEncrypt.Trim(), EncryptionKey);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string AESDecrypt(string stringToEncrypt)
    {
        try
        {
            byte[] passwordBytes = GetPasswordBytesNew();
            return AESSH256.Decrypt(stringToEncrypt.Trim(), passwordBytes);
            //return AES.Decrypt(stringToEncrypt, EncryptionKey);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}


public sealed class ResponseHelper
{
    public static void Redirect(string url, string target, string windowFeatures)
    {
        HttpContext context = HttpContext.Current;

        if ((string.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && string.IsNullOrEmpty(windowFeatures))
        {
            context.Response.Redirect(url);
        }
        else
        {
            Page page = (Page)context.Handler;
            if (page == null)
            {
                throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
            }
            url = page.ResolveClientUrl(url);

            string script = null;
            if (!string.IsNullOrEmpty(windowFeatures))
            {
                script = "window.open(\"{0}\", \"{1}\", \"{2}\");";
            }
            else
            {
                script = "window.open(\"{0}\", \"{1}\");";
            }

            script = string.Format(script, url, target, windowFeatures);
            ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
        }
    }
}

    

