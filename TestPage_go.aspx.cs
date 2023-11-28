using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Data;


public partial class TestPage_go : System.Web.UI.Page
{
    //SWPDashboard objSWP = new SWPDashboard();
    //DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    //CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();

   // CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //protected void Btn_Payment_Service_Test_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TreasuryService.IndVerificationService objTreasurySrv = new TreasuryService.IndVerificationService();
    //        string ordernumber = Txt_Order_No.Text.Trim();
    //        string amount = Txt_Challan_Amount.Text;

    //        string retVal = objTreasurySrv.getStatus(ordernumber, amount);

    //        Lbl_Payment_Service_Response.Text = retVal;
    //        Lbl_Payment_Service_Response.ForeColor = System.Drawing.Color.Blue;
    //    }
    //    catch (Exception ex)
    //    {
    //        Lbl_Payment_Service_Response.Text = ex.Message.ToString();
    //        Lbl_Payment_Service_Response.ForeColor = System.Drawing.Color.Red;
    //    }
    //}


    ///////Test Treasury Payment RestFul Service     
    //protected void Btn_Pay_REST_Click(object sender, EventArgs e)
    //{
    //    Lbl_Msg_Restful.Text = "";

    //    try
    //    {
    //        string serviceUrl = "https://www.odishatreasury.gov.in/echallanrws/IND/verify?deptRefNo=" + Txt_Order_No_REST.Text;
    //        //string serviceUrl = "http://164.100.141.167/echallanrws/LGM/verify?deptRefNo=" + Txt_Order_No_REST.Text;
    //        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
    //        httpRequest.Accept = "application/json";
    //        httpRequest.ContentType = "application/json";
    //        httpRequest.Method = "POST";
    //        httpRequest.Timeout = 15000;

    //        //using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
    //        //{
    //        //    string json = "{\"deptRefNo\": \"ADV122420181\"}";

    //        //    streamWriter.Write(json);
    //        //    streamWriter.Flush();
    //        //    streamWriter.Close();
    //        //}

    //        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
    //        {
    //            using (Stream stream = httpResponse.GetResponseStream())
    //            {
    //                string strResult = (new StreamReader(stream)).ReadToEnd();

    //                Lbl_Msg_Restful.Text = strResult;
    //                Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Blue;

    //                DataTable dt = DashboradCommon.JsonStringToDataTable(strResult);
    //                string output = dt.Rows[0]["deptRefId"].ToString();

    //                string strOrderNo = dt.Rows[0]["deptRefId"].ToString();
    //                string strBankTranId = dt.Rows[0]["bankTransId"].ToString();
    //                string strTreasuryRefId = dt.Rows[0]["challanRefId"].ToString();
    //                string strBankTranTimeStamp = dt.Rows[0]["bankTransTimeStamp"].ToString();
    //                string strBankTranStatus = dt.Rows[0]["bankTransStatus"].ToString();
    //                string strTransAmount = dt.Rows[0]["transactionAmount"].ToString();

    //                //objPromoter.bankname = retVal.Split('|')[0].Split('=')[1].ToString();
    //                //objPromoter.ifsccode = retVal.Split('|')[1].Split('=')[1].ToString();
    //                //objPromoter.dealername = retVal.Split('|')[2].Split('=')[1].ToString();
    //                //objPromoter.ordernumber = retVal.Split('|')[3].Split('=')[1].ToString();
    //                //objPromoter.bankcode = retVal.Split('|')[4].Split('=')[1].ToString();
    //                //objPromoter.treasuryrefno = retVal.Split('|')[5].Split('=')[1].ToString();
    //                //objPromoter.banktranstimestamp = retVal.Split('|')[6].Split('=')[1].ToString();
    //                //objPromoter.banktransstatus = retVal.Split('|')[7].Split('=')[1].ToString();

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Lbl_Msg_Restful.Text = ex.Message.ToString();
    //        Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Red;
    //    }
    //}

    ///////Test CRM RestFul Service (For GOSWIFT Child Service)
    //protected void Btn_CRM_Service_Click(object sender, EventArgs e)
    //{
    //    string URL = "http://192.168.201.66/swp_service/CRMService.svc/getServiceInfo";

    //    // string DATA = @"{""strSSOId"":""d48225db-3125-438b-ab59-6580099f1737"",""strSecurityKey"":""4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC""}";

    //    string DATA = "{\"strSSOId\":\"" + Txt_SSO_Id.Text + "\",\"strSecurityKey\":\"4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC\"}";

    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
    //    request.Method = "POST";
    //    request.ContentType = "application/json";
    //    request.ContentLength = DATA.Length;
    //    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
    //    requestWriter.Write(DATA);
    //    requestWriter.Close();

    //    try
    //    {
    //        WebResponse webResponse = request.GetResponse();
    //        Stream webStream = webResponse.GetResponseStream();
    //        StreamReader responseReader = new StreamReader(webStream);
    //        string response = responseReader.ReadToEnd();
    //        Lbl_CRM_Response.Text = response;
    //        responseReader.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    ///////Test CRM RestFul Service (For GOSWIFT PEAL)    
    //protected void Btn_CRM_PEAL_Click(object sender, EventArgs e)
    //{
    //    string URL = "http://192.168.201.66/swp_service/CRMService.svc/getPEALInfo";
    //    //string DATA = @"{""strSSOId"":""d48225db-3125-438b-ab59-6580099f1737"",""strSecurityKey"":""4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC""}";
    //    string DATA = "{\"strSSOId\":\"" + Txt_SSO_Id.Text + "\",\"strSecurityKey\":\"4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC\"}";

    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
    //    request.Method = "POST";
    //    request.ContentType = "application/json";
    //    request.ContentLength = DATA.Length;
    //    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
    //    requestWriter.Write(DATA);
    //    requestWriter.Close();

    //    try
    //    {
    //        WebResponse webResponse = request.GetResponse();
    //        Stream webStream = webResponse.GetResponseStream();
    //        StreamReader responseReader = new StreamReader(webStream);
    //        string response = responseReader.ReadToEnd();
    //        Lbl_CRM_Response.Text = response;
    //        responseReader.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
}