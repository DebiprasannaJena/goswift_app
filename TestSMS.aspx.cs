using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Service;
using EntityLayer.Service;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class TestSMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindYear();
    }

    private void BindYear()
    {
        DrpDwn_Year.Items.Clear();       
        DrpDwn_Year.Items.Insert(0, new ListItem("--Select--", "0"));

        try
        {
            var currentYear = DateTime.Today.Year;
            for (int i = 0; i <= 10; i++)
            {
                DrpDwn_Year.Items.Add((currentYear - i).ToString());
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = SendSmsNewGoSwift(TextBox1.Text.ToString(), TextBox2.Text);
    }
    public string SendSmsNewGoSwift(string strMobNo, string Sms)
    {
        string Res = "";
        try
        {
            //string fb_url = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=goswift.sms&pin=Np%40%236745&message=" + Sms + "&mnumber=91" + strMobNo + "&signature=GOSWFT";
            //string fb_url = "http://message.websoftservices.com/api/sendsms.php?username=websoftdemo&password=websoft@123&sender=WEBSOF&mobile="+ strMobNo + "&type=1&product=1&message=" + Sms;
            //https://smsgw.sms.gov.in/failsafe/HttpLink?username=goswift.sms&pin=Np%40%236745&message=Hi%20Smruti,%20How%20r%20u?&mnumber=919861957393&signature=GOSWFT
            //https://smsgw.sms.gov.in/failsafe/MttpLink?username=goswift.sms&pin=Np%40%236745&message=TestSMS&mnumber=919853199073&signature=GOSWIT&dlt_entity_id=1001936451134336346
            // string fb_url = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=goswift.sms&pin=Np%40%236745&message=" + Sms + "&mnumber=91" + strMobNo + "&signature=GOSWIT&dlt_entity_id=1001936451134336346&dlt_template_id=1405159366835079933";

            string fb_url = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=goswift.sms&pin=Np%40%236745&message=" + Sms + "&mnumber=91" + strMobNo + "&signature=IPICOL&dlt_entity_id=1001936451134336346";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fb_url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();
            Res = objText;
        }
        catch (Exception ex)
        {
            Res = ex.Message;
        }
        return Res;
    }


    protected void Btn_Msg_Configure_Click(object sender, EventArgs e)
    {
        Lbl_Msg_Configure.Text = "";
        Lbl_Response_1.Text = "";

        string sURL = "";

        sURL = sURL + Txt_Main_Url.Text.Trim();
        sURL = sURL + "username=" + Txt_User_Name.Text.Trim();
        sURL = sURL + "&pin=" + Txt_PIN.Text.Trim();
        sURL = sURL + "&message=" + Txt_Msg.Text.Trim();
        sURL = sURL + "&mnumber=91" + Txt_Mobile_No.Text.Trim();
        sURL = sURL + "&signature=" + Txt_Signature.Text.Trim();
        sURL = sURL + "&dlt_entity_id=" + Txt_DLT_Entity_Id.Text.Trim();

        if (Txt_DLT_Template_Id.Text.Trim() != "")
        {
            sURL = sURL + "&dlt_template_id=" + Txt_DLT_Template_Id.Text.Trim();
        }

        Lbl_Msg_Configure.Text = sURL;
        Lbl_Response_1.Text = SendSms(sURL);
    }
    protected void Btn_Msg_2_Click(object sender, EventArgs e)
    {
        Lbl_Response_2.Text = "";
        Lbl_Response_2.Text = SendSms(Txt_Full_Url.Text.Trim());
    }
    public string SendSms(string strUrl)
    {
        string Res = "";
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();
            Res = objText;
        }
        catch (Exception ex)
        {
            Res = ex.Message;
        }
        return Res;
    }
}