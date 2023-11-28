using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using EntityLayer.Service;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
public partial class ServicePayment : System.Web.UI.Page
{
    DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    DataTable dt = new DataTable();
    ArrayList lst = new ArrayList();
    ArrayList hdnlst = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //FileMatching();
            if (Request.QueryString["T"] != null)
            {
                //hrfPaymentConfrim.Visible = true;
                hrfPaymentConfrim.Visible = false; //// Above line commented and this line added by sushant jena on Dt.27-Dec-2018
                // hrfPaymentConfrim.NavigateUrl = "ServicePaymentConfirm.aspx?reqid=" + Request.QueryString["ApplicationKey"].ToString();
            }
            else
            {
                hrfPaymentConfrim.Visible = false;
            }

            /*-------------------------------------------------------*/

            if (Request.QueryString["ApplicationKey"] != null)
            {
                if (Request.QueryString["Amount"] != null)
                {
                    txtAmount.Text = Request.QueryString["Amount"].ToString();
                    txtAmount.ReadOnly = true;
                    if (Convert.ToDouble(txtAmount.Text) <= 0)
                    {
                        Response.Redirect("PaymentThankYou.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"] + "");
                        objData.UpdatePaymentService(Request.QueryString["ApplicationKey"].ToString(), 0, 0);
                    }
                    else
                    {
                        decimal decAppFee = Convert.ToDecimal(Request.QueryString["AppFee"]);
                        DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
                        objData.UpdatePaymentService(Request.QueryString["ApplicationKey"].ToString(), Convert.ToDouble(txtAmount.Text), decAppFee);
                    }
                }
            }
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        Session["ApplicationKey"] = Request.QueryString["ApplicationKey"].ToString();
        Session["amnt"] = txtAmount.Text;
        Payment();
        //Response.Redirect("PaymentThankYou.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"] + "");
    }

    string GenerateServieOrder(string applicationkey)
    {
        string strRes = "ES" + applicationkey + "-" + DateTime.Now.ToString("ddMMyyhhmmss");
        int intRes = 0;

        string queryCnt = "INSERT INTO [T_Service_Order]([vchOrderNo],[intServiceId]";
        queryCnt = queryCnt + ",[vchApplicationNo],[dtmOrderDate],[intPaymentStatus],[intCreatedBy],[vchChallanAmount],[IntReqid])VALUES";
        queryCnt = queryCnt + "('" + strRes + "'," + GetFormId(applicationkey) + ",'" + applicationkey + "',GETDATE()," + 0 + "," + Session["InvestorId"].ToString() + "," + Request.QueryString["Amount"].ToString() + ",'" + applicationkey + "')";

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString()))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                intRes = Convert.ToInt32(cmd1.ExecuteNonQuery().ToString());
                con.Close();
            }
        }
        return strRes;
    }
    public int GetFormId(string applicationkey)
    {
        //int intInvestorId = Convert.ToInt32(Session["InvestorId"]);
        string queryCnt = " select INT_SERVICEID from T_APPLICATION_TBL where VCH_APPLICATION_UNQ_KEY='" + applicationkey + "'";
        int strFormId = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString()))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strFormId = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
                con.Close();
            }
        }
        return strFormId;
    }
    void Payment()
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        try
        {
            string strQuery = "SELECT dbo.M_SWP_SERVICEACCOUNT.vchServiceName, dbo.M_SWP_SERVICEACCOUNT.intServiceid, dbo.M_SERVICEMASTER_TBL.INT_DEPARTMENT_ID, dbo.M_ADM_LevelDetails.nvchLevelName, dbo.M_SWP_SERVICEACCOUNT.vchServiceType, dbo.M_SWP_SERVICEACCOUNT.vchAccountHead ";
            strQuery = strQuery + " FROM dbo.M_SERVICEMASTER_TBL INNER JOIN  dbo.M_SWP_SERVICEACCOUNT ON dbo.M_SERVICEMASTER_TBL.INT_SERVICEID = dbo.M_SWP_SERVICEACCOUNT.intServiceid INNER JOIN";
            strQuery = strQuery + " dbo.M_ADM_LevelDetails ON dbo.M_SERVICEMASTER_TBL.INT_DEPARTMENT_ID = dbo.M_ADM_LevelDetails.intLevelDetailId WHERE (dbo.M_SWP_SERVICEACCOUNT.intServiceid = " + Request.QueryString["ServiceID"].ToString() + ")";

            cmd.CommandText = strQuery;
            cmd.Connection = con;
            da.SelectCommand = cmd;

            DataSet ds = new DataSet();
            da.Fill(ds);

            string DATA = string.Empty;
            string FAccountHead = "";
            string AccountHead = "";
            string Investorname = string.Empty;
            string Address = string.Empty;
            string State = string.Empty;
            string District = string.Empty;
            string Pin = string.Empty;
            string Mobile = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {

                string rurl = System.Configuration.ConfigurationManager.AppSettings["ServiceReturnURL"];
                string surl = System.Configuration.ConfigurationManager.AppSettings["Responseurl"];

                string Amount = "0";
                string strOrderNo = GenerateServieOrder(Request.QueryString["ApplicationKey"].ToString());

                if (System.Configuration.ConfigurationManager.AppSettings["ActualAmount"].ToString() == "Yes")
                {
                    Amount = txtAmount.Text;
                }
                else
                {
                    Amount = "1";
                }

                if (Request.QueryString["ServiceID"].ToString() == "48")/////Change of Land Use
                {
                    string strSrvc48ApplicationFee = System.Configuration.ConfigurationManager.AppSettings["strSrvc48ApplicationFee"];
                    string strSrvc48ConversionFee = System.Configuration.ConfigurationManager.AppSettings["strSrvc48ConversionFee"];

                    DataTable dt48 = new DataTable();                   
                    try
                    {
                        dt48 = TREASURYDETAILS();
                        if (dt48.Rows.Count > 0)
                        {
                            Investorname = dt48.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                            Address = dt48.Rows[0]["VCH_ADDRESS"].ToString();
                            State= dt48.Rows[0]["State"].ToString();
                            District= dt48.Rows[0]["vchDistrictName"].ToString();
                            Pin= dt48.Rows[0]["Pin"].ToString();
                            Mobile= dt48.Rows[0]["VCH_OFF_MOBILE"].ToString();
                        }
                        else
                        {
                            Investorname = "NA";
                            Address = "NA";
                            State = "NA";
                            District = "NA";
                            Pin = "NA";
                            Mobile = "NA";
                        }
                    }
                    catch (Exception ex)
                    {
                        Investorname = "NA";
                        Address = "NA";
                        State = "NA";
                        District = "NA";
                        Pin = "NA";
                        Mobile = "NA";
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    if (Amount == "50")
                    {
                            DATA =
                        "IND"
                        + "|" + strOrderNo

                        + "|" + strSrvc48ApplicationFee
                        + "|" + "Application Fee and Notice Fee"
                        + "|" + 30

                        + "|" + strSrvc48ConversionFee
                        + "|" + "Conversion fee"
                        + "|" + 20

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + 50
                        + "|" + Investorname
                        + "|" + Address
                        + "|" + ""
                        + "|" + State
                        + "|" + District
                        + "|" + Pin
                        + "|" + Mobile
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + rurl;
                    }
                    else
                    {
                            DATA =
                        "IND"
                        + "|" + strOrderNo

                        + "|" + strSrvc48ApplicationFee
                        + "|" + "Application Fee and Notice Fee"
                        + "|" + 30

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + 30
                        + "|" + Investorname
                        + "|" + Address
                        + "|" + ""
                        + "|" + State
                        + "|" + District
                        + "|" + Pin
                        + "|" + Mobile
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + rurl;
                    }

                }
                else if (Request.QueryString["ServiceID"].ToString() == "41") ////Permission to Draw Water
                {
                    decimal decAppFee = Convert.ToDecimal(Request.QueryString["AppFee"]);
                    decimal TotalAmount = Math.Round(Convert.ToDecimal(Request.QueryString["Amount"]));
                    decimal applicationFee = Math.Round(Convert.ToDecimal(Request.QueryString["Amount"])) - 1000;
                    string strSrvc41AcountHdProce = System.Configuration.ConfigurationManager.AppSettings["strSrvc41AcountHdProce"];
                    string strSrvc41AcountHdSecurity = System.Configuration.ConfigurationManager.AppSettings["strSrvc41AcountHdSecurity"];

                    DataTable dt41 = new DataTable();                    
                    try
                    {
                        dt41 = TREASURYDETAILS();
                        if (dt41.Rows.Count > 0)
                        {
                            Investorname = dt41.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                            Address = dt41.Rows[0]["VCH_ADDRESS"].ToString();
                            State = dt41.Rows[0]["State"].ToString();
                            District = dt41.Rows[0]["vchDistrictName"].ToString();
                            Pin = dt41.Rows[0]["Pin"].ToString();
                            Mobile = dt41.Rows[0]["VCH_OFF_MOBILE"].ToString();
                        }
                        else
                        {
                            Investorname = "NA";
                            Address = "NA";
                            State = "NA";
                            District = "NA";
                            Pin = "NA";
                            Mobile = "NA";
                        }
                    }
                    catch (Exception ex)
                    {
                        Investorname = "NA";
                        Address = "NA";
                        State = "NA";
                        District = "NA";
                        Pin = "NA";
                        Mobile = "NA";
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    if (Convert.ToInt32(decAppFee) == 0)
                    {
                        DATA =
                        "IND"
                        + "|" + strOrderNo

                        + "|" + strSrvc41AcountHdProce
                        + "|" + "Application Fee and Notice Fee"
                        + "|" + 1000

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + TotalAmount
                        + "|" + Investorname
                        + "|" + Address
                        + "|" + ""
                        + "|" + State
                        + "|" + District
                        + "|" + Pin
                        + "|" + Mobile
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + rurl;
                    }
                    else
                    {

                        DATA =
                        "IND"
                        + "|" + strOrderNo

                        + "|" + strSrvc41AcountHdProce
                        + "|" + "Application Fee and Notice Fee"
                        + "|" + 1000

                        + "|" + strSrvc41AcountHdSecurity
                        + "|" + "Security fee"
                        + "|" + applicationFee

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""

                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + TotalAmount
                        + "|" + Investorname
                        + "|" + Address
                        + "|" + ""
                        + "|" + State
                        + "|" + District
                        + "|" + Pin
                        + "|" + Mobile
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + ""
                        + "|" + rurl;
                    }

                }
                else if (Request.QueryString["ServiceID"].ToString() == "7")////Contract Labour Service
                {
                    decimal decAppFee = Convert.ToDecimal(Request.QueryString["AppFee"]);
                    decimal decSecurityFee = Math.Round(Convert.ToDecimal(Request.QueryString["Amount"])) - decAppFee;

                    string strSrvc7AcountHdProce = ds.Tables[0].Rows[0]["vchAccountHead"].ToString();
                    string strSrvc7AcountHdSecurity = System.Configuration.ConfigurationManager.AppSettings["strSrvc7AcountHdSecurity"];

                    DataTable dt7 = new DataTable();                    
                    try
                    {
                        dt7 = TREASURYDETAILS();
                        if (dt7.Rows.Count > 0)
                        {
                            Investorname = dt7.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                            Address = dt7.Rows[0]["VCH_ADDRESS"].ToString();
                            State = dt7.Rows[0]["State"].ToString();
                            District = dt7.Rows[0]["vchDistrictName"].ToString();
                            Pin = dt7.Rows[0]["Pin"].ToString();
                            Mobile = dt7.Rows[0]["VCH_OFF_MOBILE"].ToString();
                        }
                        else
                        {
                            Investorname = "NA";
                            Address = "NA";
                            State = "NA";
                            District = "NA";
                            Pin = "NA";
                            Mobile = "NA";
                        }
                    }
                    catch (Exception ex)
                    {
                        Investorname = "NA";
                        Address = "NA";
                        State = "NA";
                        District = "NA";
                        Pin = "NA";
                        Mobile = "NA";
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    int totalAmount = Convert.ToInt32(decAppFee + decSecurityFee);

                    DATA =
                    "IND"
                    + "|" + strOrderNo

                    + "|" + strSrvc7AcountHdProce
                    + "|" + "Application Fee and Notice Fee"
                    + "|" + decAppFee

                    + "|" + strSrvc7AcountHdSecurity
                    + "|" + "Security fee"
                    + "|" + decSecurityFee

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + totalAmount
                    + "|" + Investorname
                    + "|" + Address
                    + "|" + ""
                    + "|" + State
                    + "|" + District
                    + "|" + Pin
                    + "|" + Mobile
                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + rurl;

                }
                else
                {
                    DataTable dt = new DataTable();                    
                    try
                    {
                        dt = TREASURYDETAILS();
                        if (dt.Rows.Count > 0)
                        {
                            Investorname = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                            Address = dt.Rows[0]["VCH_ADDRESS"].ToString();
                            State = dt.Rows[0]["State"].ToString();
                            District = dt.Rows[0]["vchDistrictName"].ToString();
                            Pin = dt.Rows[0]["Pin"].ToString();
                            Mobile = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                        }
                        else
                        {
                            Investorname = "NA";
                            Address = "NA";
                            State = "NA";
                            District = "NA";
                            Pin = "NA";
                            Mobile = "NA";
                        }
                    }
                    catch (Exception ex)
                    {
                        Investorname = "NA";
                        Address = "NA";
                        State = "NA";
                        District = "NA";
                        Pin = "NA";
                        Mobile = "NA";
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AccountHead = AccountHead + ds.Tables[0].Rows[i]["vchAccountHead"].ToString() + ",";
                    }
                    FAccountHead = AccountHead.ToString().Remove(AccountHead.Length - 1);
                    if (FAccountHead.Contains(','))
                    {
                        string[] xx = FAccountHead.Split(',');
                        int Count = xx.Length;
                        if (Count == 1)
                        {
                                    DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + xx[0].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + Amount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;

                        }
                        else if (Count==2)
                        {
                            int TotalAmount = Convert.ToInt32(Amount) +  Convert.ToInt32(Amount);

                                    DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + xx[0].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[1].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + TotalAmount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;
                        }
                        else if (Count == 3)
                        {

                            int TotalAmount = Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount);

                                    DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + xx[0].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[1].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[2].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + TotalAmount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;
                        }
                        else if (Count == 4)
                        {
                            int TotalAmount = Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount);

                                    DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + xx[0].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[1].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[2].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[3].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + TotalAmount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;
                        }
                        else if (Count == 5)
                        {
                            int TotalAmount = Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount);

                                    DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + xx[0].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[1].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[2].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[3].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[4].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + TotalAmount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;
                        }
                        else if (Count == 6)
                        {
                            int TotalAmount = Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount) + Convert.ToInt32(Amount);

                                    DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + xx[0].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[1].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[2].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[3].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[4].ToString()
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + xx[5].ToString()
                            + "|" + "NA"
                            + "|" + Amount
                            + "|" + TotalAmount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;
                        }                        
                    }
                    else
                    {
                        if (Request.QueryString["ServiceID"].ToString() == "16")
                        {
                            string AcntHd = PowerConnectionAccountHead();//"0852-80-800-0234-02233-000";//
                            string strSrvc16UserFee = ds.Tables[0].Rows[0]["strSrvc16UserFee"].ToString();

                            int TotalAmount = Convert.ToInt32(Amount) + 1;

                                    DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + AcntHd
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + strSrvc16UserFee
                            + "|" + "User Fee"
                            + "|" + 1

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + TotalAmount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;
                            
                        }
                        else
                        {
                            DATA =
                            "IND"
                            + "|" + strOrderNo

                            + "|" + FAccountHead
                            + "|" + "NA"
                            + "|" + Amount

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""

                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + Amount
                            + "|" + Investorname
                            + "|" + Address
                            + "|" + ""
                            + "|" + State
                            + "|" + District
                            + "|" + Pin
                            + "|" + Mobile
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + ""
                            + "|" + rurl;                           
                        }
                    }
                }
                string INCRRIPT = HmacSHA256(DATA, "c3c6e92a");
                string datta = DATA + "|" + INCRRIPT;
                datta = encrypt(datta, "c3c6e92a");
                RedirectAndPOST(this.Page, surl, datta);
            }
            else
            {
                string rurl = System.Configuration.ConfigurationManager.AppSettings["ServiceReturnURL"];
                string surl = System.Configuration.ConfigurationManager.AppSettings["Responseurl"];
                string strOrderNo = GenerateServieOrder(Request.QueryString["ApplicationKey"].ToString());
                string Amount = "0";
                if (System.Configuration.ConfigurationManager.AppSettings["ActualAmount"].ToString() == "Yes")
                {
                    Amount = txtAmount.Text;
                }
                else
                {
                    Amount = "1";
                }

                DATA =
                    "IND"
                    + "|" + strOrderNo

                    + "|" + "0852-80-800-0234-02233-000"
                    + "|" + "NA"
                    + "|" + Amount

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + Amount
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "111111"
                    + "|" + "9999999999"
                    + "|" + ""
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + rurl;


                string INCRRIPT = HmacSHA256(DATA, "c3c6e92a");
                string datta = DATA + "|" + INCRRIPT;
                datta = encrypt(datta, "c3c6e92a");
                RedirectAndPOST(this.Page, surl, datta);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {

        }
    }

    /// <summary>
    /// Treasury Post Coding Begin
    /// </summary>
    /// <param name="page"></param>
    /// <param name="destinationUrl"></param>
    /// <param name="data"></param>
    /// 

    private byte[] GetKey()
    {
        byte[] byteKey = File.ReadAllBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        return byteKey;
    }

    public static void RedirectAndPOST(Page page, string destinationUrl, String data)
    {
        //Prepare the Posting form
        string strForm = PrepareScript(destinationUrl, data);
        //Add a literal control the specified page holding 
        //the Post Form, this is to submit the Posting form with the request.
        page.Controls.Add(new LiteralControl(strForm));
    }


    private static String PrepareScript(string url, String data)
    {
        //Set a name for the form
        string formID = "eGrassClient";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\" >");//target=\"TheWindow\"

        strForm.Append("<input type=\"hidden\" name=\"msg\" value=\"" + data + "\">");
        strForm.Append("<input type=\"hidden\" name=\"deptCode\" value=\"IND\">");

        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        //strScript.Append("javascript:void window.open('','TheWindow','menubar= 1,scrollbars=1,width=600,height=400');");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");

        return strForm.ToString() + strScript.ToString();
    }


    private static byte[] GetFileBytes(String filename)
    {
        if (!File.Exists(filename))
            return null;
        Stream stream = new FileStream(filename, FileMode.Open);
        int datalen = (int)stream.Length;
        byte[] filebytes = new byte[datalen];
        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(filebytes, 0, datalen);
        stream.Close();
        return filebytes;
    }

    private string HmacSHA256(string message, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key"); //encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }

    public string encrypt(string plainText, string secret)
    {

        System.Text.UTF32Encoding UTF32 = new System.Text.UTF32Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateEncryptor();
        byte[] plain = Encoding.UTF8.GetBytes(plainText);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        return Convert.ToBase64String(cipher);

    }

    /// <summary>
    /// Treasury Post Coding End
    /// </summary>
    /// <param name="page"></param>
    /// <param name="destinationUrl"></param>
    /// <param name="data"></param>

    private void Upload_File(string imgName, FileUpload fileName)
    {
        string gFilePath = "Portal/Document/Upload/";
        string strtime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        if (!Directory.Exists(Server.MapPath(gFilePath)))
        {
            // Create the directory.
            Directory.CreateDirectory(Server.MapPath(gFilePath));
        }
        gFilePath = Server.MapPath(gFilePath + imgName);
        if (File.Exists(gFilePath))
        {
            File.Delete(gFilePath);
        }
        fileName.PostedFile.SaveAs(gFilePath);
    }


    public string PowerConnectionAccountHead()
    {
        string strUtility = "";
        string strAccountHd = "";
        string strInsert1 = " select Utility from table_16 where VCH_APPLICATION_UNQ_KEY='" + Request.QueryString["ApplicationKey"].ToString() + "'";
        if (strInsert1 != "" && strInsert1 != null)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(strInsert1, con);
            strUtility = cmd.ExecuteScalar().ToString();
            con.Close();
        }

        if (strUtility != "" && strUtility != null)
        {
            if (strUtility == "NESCO Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-004";
            }
            else if (strUtility == "SOUTHCO Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-006";
            }
            else if (strUtility == "WESCO Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-005";
            }
            else if (strUtility == "CESU Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-003";
            }
        }

        return strAccountHd;
    }

    /// <summary>
    /// Used for payment confirmation
    /// Code section commented by Sushant jena on Dt 27-Dec-2018
    /// Because it makes payment status as paid without checking the payment from Treasury or any other table.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        //#region Modification for Demo

        //Session["amnt"] = txtAmount.Text;
        //SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());


        //SqlCommand cmd1 = new SqlCommand("Update T_APPLICATION_TBL set INT_PAYMENT_STATUS=1 where VCH_APPLICATION_UNQ_KEY='" + Request.QueryString["ApplicationKey"].ToString() + "'", con1);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataSet ds1 = new DataSet();
        //da1.Fill(ds1);
        //Response.Redirect("PaymentThankYou.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"].ToString());
        //return;
        //#endregion
    }

    private DataTable TREASURYDETAILS()
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString()))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_FETCHTREASURYDETAILS";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@P_SERVICEID", Convert.ToInt32(Request.QueryString["ServiceID"].ToString()));
                    cmd.Parameters.AddWithValue("@P_VCHAPPLICATIONKEY", Request.QueryString["ApplicationKey"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        return dt;
    }
}