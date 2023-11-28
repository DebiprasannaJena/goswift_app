using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BPAS;
using System.Net;

public partial class BPAS_ApplicationSubmit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string str_ProposalNo = Request.QueryString["ProposalNo"];
        UserDetail _objUser = new UserDetail();
        ExternalServiceIntegration.Serviceinfo objservice = new ExternalServiceIntegration.Serviceinfo();
        ExternalServiceIntegration objSvc = new ExternalServiceIntegration();
        List<ExternalServiceIntegration.Serviceinfo> objServicelist = new List<ExternalServiceIntegration.Serviceinfo>();
        string output = objSvc.ExternalServiceData("GA", 20, str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), "");
        objservice.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
        objservice.vchAction = "GU";
        string _userID = Session["UserId"].ToString();
        hdnUsrId.Value = _userID;
        objServicelist = objSvc.GetUserDetail(objservice);
        string _password = "SWP_" + objServicelist[0].vchPassword.ToString().Substring(0, 6);
        hdnPassword.Value = _password;

        string RestServiceURL = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
        hdnChkStatusUrl.Value = RestServiceURL;
        string RedirectionURL = ConfigurationManager.AppSettings["BPASRedirectionURL"].ToString();
        hdn_RedirectionUrl.Value = RedirectionURL;
        try
        {
            using (var client = new WebClient()) //WebClient  
            {
                //Variable given by User
                // string _userID = "admin4@gmail.com"; string _password = "test@123";
                string _swpcode = Session["InvestorId"].ToString(); string _uniquecode = output;
                hdn_swpcode.Value = _swpcode; hdn_uniquecode.Value = _uniquecode;
                string _msg = string.Empty;
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(RestServiceURL + "/CheckUser?_userID=" + _userID + "&_password=" + _password + "");//URI  
                bool Results = Convert.ToBoolean(result);
                hdn_ChkUsrResult.Value = result.ToString();
                if (Results == true)
                {
                    var InsertResult = client.DownloadString(RestServiceURL + "/InsertUserDetails?_swpcode=" + _swpcode + "&_uniquecode=" + _uniquecode + ""); //URI  
                    InsertResult = InsertResult.Substring(1, 4);
                    hdn_InsertUserDtlResult1.Value = InsertResult.ToString();
                    if (InsertResult.ToString() == "done")
                    {

                        string pram3 = encrypt(_userID);  //enter username
                        hdnEncUsrId1.Value = pram3;
                        string pram4 = encrypt(_password);   //enter password
                        hdnEncPassword1.Value = pram4;
                        Response.Redirect(RedirectionURL + "?pram1=" + pram3 + "&pram2=" + pram4);
                        hdnRedirectUrl1.Value = RedirectionURL + "?pram1=" + pram3 + "&pram2=" + pram4;

                    }
                    else
                    {
                        // Redirect to your URL
                    }

                }
                else
                {
                    var RegisterResult = client.DownloadString(RestServiceURL + "/RegisterUser?UserName=" + _userID + "&Password=" + _password + "&SWPCode=" + _swpcode); //URI
                    int ln = RegisterResult.Length;
                    int Uln = ln - 3;
                    RegisterResult = RegisterResult.Substring(1, Uln);
                    hdn_RegisterResult.Value = RegisterResult.ToString();
                    if (RegisterResult.ToString() == "User registered successfully in system")
                    {
                        var InsertforUQ = client.DownloadString(RestServiceURL + "/InsertUserDetails?_swpcode=" + _swpcode + "&_uniquecode=" + _uniquecode + ""); //URI  
                        InsertforUQ = InsertforUQ.Substring(1, 4);
                        hdn_InsertUserDtlResult2.Value = InsertforUQ.ToString();
                        if (InsertforUQ.ToString() == "done")
                        {

                            string pram3 = encrypt(_userID);  //enter username
                            string pram4 = encrypt(_password);   //enter password
                            Response.Redirect(RedirectionURL + "?pram1=" + pram3 + "&pram2=" + pram4);
                            hdn_RedirectionUrl.Value = RedirectionURL + "?pram1=" + pram3 + "&pram2=" + pram4;

                        }
                        else
                        {
                            // Redirect to your URL
                        }

                    }
                }
            }


        }
        catch (Exception ex)
        {
            hdn_ErrorDetls.Value = ex.ToString();

        }
    }




    public static string encrypt(string toEncrypt)
    {
        try
        {
            return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(toEncrypt));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error" + ex);
            return null;
        }
    }

    public static string decrypt(string cypherString)
    {
        try
        {
            return System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error" + ex);
            return null;
        }
    }
}