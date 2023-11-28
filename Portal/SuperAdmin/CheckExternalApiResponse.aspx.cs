using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_SuperAdmin_CheckExternalApiResponse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///// This page can only be accessed by goadmin.
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Convert.ToInt32(Session["UserId"]) != 1)
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    #region Treasury_Section

    ///Test Treasury Payment RestFul Service     
    protected void BtnPaymentStatusTreasury_Click(object sender, EventArgs e)
    {
        Lbl_Treasury_Response.Text = "";

        try
        {
            string strOrderNo = Txt_Order_No_Treasury.Text.Trim();
            decimal decChallanAmt = Convert.ToDecimal(Txt_Challan_Amount_Treasury.Text);

            TreasuryPaymentTracking objTreasury = new TreasuryPaymentTracking();
            string strTreasuryRes = objTreasury.GetPaymentStatusFromTreasury(strOrderNo, decChallanAmt);

            Lbl_Treasury_Response.Text = strTreasuryRes;
            Lbl_Treasury_Response.ForeColor = System.Drawing.Color.Blue;
        }
        catch (Exception ex)
        {
            Lbl_Treasury_Response.Text = ex.Message.ToString();
            Lbl_Treasury_Response.ForeColor = System.Drawing.Color.Red;
        }
    }

    #endregion

    #region PAN_Section

    protected void BtnPANStatus_Click(object sender, EventArgs e)
    {
       
        string strData = ConfigurationManager.AppSettings["PanUserID"].ToString();
      
        string URL = ConfigurationManager.AppSettings["PanURL"];
     
        string PFXPassword = ConfigurationManager.AppSettings["PanPWD"];
        string Certificatename = ConfigurationManager.AppSettings["Certificatename"];
     

        Util.LogRequestResponse("PANintegrationTEST", "RequestDateTime :-", DateTime.Now.ToString()+":"+ "[PAN USER ID]:- "+ strData+":"+ " [PAN_URL]:- "+ URL+":"+ " [Certificate name]:-"+ Certificatename +":"+ "[PAN Number]:-"+ Txt_PAN.Text);

        Hid_PAN_Credential.Value = "[PAN USER ID]:- " + strData + " [PAN_URL]:-  " + URL + " [PAN_PWD]:- " + PFXPassword + " [PAN_CERTIFICATE]:-  " + Certificatename;

        try
        {
            PANValidationNSDL objPan = new PANValidationNSDL();
            string strPANResponse = objPan.GetPANStatusFromNSDL(Txt_PAN.Text);
            Util.LogRequestResponse("PANintegrationTEST", "PANResponse :", strPANResponse);

            Lbl_PAN_Response.Text = strPANResponse;
            Lbl_PAN_Response.ForeColor = System.Drawing.Color.Blue;
        }
        catch (Exception ex)
        {
            Util.LogRequestResponse("PANintegrationTEST", "PANException :", ex.Message.ToString());
            Lbl_PAN_Response.Text = ex.Message.ToString();
            Lbl_PAN_Response.ForeColor = System.Drawing.Color.Red;
        }
    }

    #endregion

    #region NSWS_Section
    protected void BtnTokenNSWS_Click(object sender, EventArgs e)
    {
        try
        {
            Lbl_Token.Text = "";
            Lbl_Token_Addrees.Text = "";
            Lbl_Token_Response.Text = "";

            string strTokenUrl = ConfigurationManager.AppSettings["NswsTokenGenerationUrl"].ToString();
            Lbl_Token_Addrees.Text = "<b>TOKEN_ADDRESS:- </b>" + strTokenUrl;

            /*----------------------------------------------------------------------*/
            ///Generate Access Token
            /*----------------------------------------------------------------------*/
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            RestClient client1 = new RestClient(strTokenUrl);
            client1.Timeout = -1;
            RestRequest request1 = new RestRequest(Method.POST);
            //string strAuthKey = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("sws_state:643790eb-2b2a-4187-8c43-54a663b840eb"));
            //request1.AddHeader("Authorization", strAuthKey);
            request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request1.AddParameter("grant_type", "password");
            request1.AddParameter("username", "odisha");
            request1.AddParameter("password", "Odisha@nsws");
            request1.AddParameter("client_secret", "643790eb-2b2a-4187-8c43-54a663b840eb");
            request1.AddParameter("client_id", "sws_state");
            IRestResponse responseToken = client1.Execute(request1);

            Lbl_Token_Response.Text = "<b>TOKEN_RESPONSE:-  </b>" + responseToken.Content.ToString();

            if (responseToken.Content.ToString() != "")
            {
                ///Get the Access Token
                string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();
                Lbl_Token.Text = "<b>ACCESS_TOKEN:- </b>" + strAccessToke;
            }
        }
        catch (Exception ex)
        {
            Lbl_Token.Text = "<b>EXCEPTION:- </b>" + ex.Message.ToString();
        }
    }

    #endregion

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

    protected void Btn_Msg_2_Click(object sender, EventArgs e)
    {
        Lbl_Response_2.Text = "";
        Lbl_Response_2.Text = SendSms(Txt_Full_Url.Text.Trim());
    }
}