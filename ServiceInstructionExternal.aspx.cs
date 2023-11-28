using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BPAS;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using RestSharp;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class ServiceInstructionExternal : System.Web.UI.Page
{
    #region Globalvariable
    /// <summary>
    /// Prasun Kali
    /// All global variable are declared here
    /// </summary>
    string str_FormId = "";
    int Int_ServiceType = 0;

    string str_ProposalNo = "";
    string str_Amount = "0";
    string str_RequestMode = "";
    CmsBusinesslayer objService = new CmsBusinesslayer();
   
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
   
    
    ExternalServiceIntegration objSvc = new ExternalServiceIntegration();
   
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        myNavbar.Visible = false;

        if (Request.QueryString["ReqMode"] != "" && Request.QueryString["ReqMode"] != null)
        {
            str_RequestMode = Request.QueryString["ReqMode"].ToString();

            if (Request.QueryString["FormId"] != "" && Request.QueryString["FormId"] != null)
            {
                str_FormId = Request.QueryString["FormId"].ToString();
                Int_ServiceType = Convert.ToInt32(Request.QueryString["ServiceType"].ToString());
                str_ProposalNo = Request.QueryString["ProposalNo"].ToString();
                str_Amount = Request.QueryString["Amount"].ToString();
                FillContent();
                lblService.Text = GetServiceName(Request.QueryString["FormId"].ToString());
            }
        }
    }

    public string GetServiceName(string strServiceID)
    {
        string strRes = "";
        SqlCommand cmd = new SqlCommand("SELECT VCH_SERVICENAME,(SELECT nvchLevelName FROM M_ADM_LevelDetails WHERE intLevelId=2 AND intLevelDetailId=INT_DEPARTMENT_ID) As Department FROM M_SERVICEMASTER_TBL WHERE  INT_SERVICEID=" + strServiceID, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            strRes = ds.Tables[0].Rows[0][1].ToString() + " > " + ds.Tables[0].Rows[0][0].ToString();

        }
        return strRes;
    }
    private void FillContent()
    {
        try
        {
            EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();
            objServiceEntity.StrAction = "GCD";
            objServiceEntity.IntServiceId = Convert.ToInt32(str_FormId);
            IList<CMSDetails> obj = objService.GetCMSData(objServiceEntity);
            if (obj.Count > 0)
            {
                divabout.InnerHtml = obj[0].StrContent;
                HyprLnk.NavigateUrl = obj[0].strAttachment;
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
   

    protected void BtnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            if (str_FormId == "52") // Trade Licenece Service
            {
                Response.Redirect("TradeLicenceData.aspx?FormId=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo);
            }
            else if (str_FormId == "30" || str_FormId == "31" || str_FormId == "32") // DrugLicense Service
            {
                lblMessage.Text = "Health and Family Welfare";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "43" || str_FormId == "44" || str_FormId == "45" || str_FormId == "46" || str_FormId == "50" || str_FormId == "53" || str_FormId == "54" || str_FormId == "65" || str_FormId == "66" || str_FormId == "76" || str_FormId == "77" || str_FormId == "78" || str_FormId == "79") // Pollution Control Board
            {
                lblMessage.Text = "Odisha State Pollution Control Board";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "25" || str_FormId == "26") // Tree Transit Service
            {
                lblMessage.Text = "Forest and Environment";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "28") // Land Allocated Service
            {
                Response.Redirect("LandAllocated.aspx?ServiceID=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo + "&UserId=" + Session["InvestorId"].ToString());
            }
            else if (str_FormId == "49") // PartnershipFirm Service
            {
                lblMessage.Text = "Revenue & Disaster Management";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "10")
            {
                Response.Redirect("ProfessionalTaxData.aspx?FormId=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo);
            }
            else if (str_FormId == "20") // Building Plan Approval Service
            {
                lblMessage.Text = "Housing and Urban Development";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "29") // Obtaining water connection
            {
                lblMessage.Text = "IDCO";
                ServiceModalPopup.Show();
            }           
            else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72" || str_FormId == "37") //F&B and Labour Service form id 37 add by anil
            {
                lblMessage.Text = "PAReSHRAM";
                ServiceModalPopup.Show();
            }
            
            else if (str_FormId == "62" || str_FormId == "63") // Fire Service
            {
                lblMessage.Text = "Directorate General Fire Services, Home Guards & Civil Defence";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "67" || str_FormId == "68") // Excise Service
            {
                lblMessage.Text = "Odisha State Excise";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "69") // OSBC Service
            {
                lblMessage.Text = "Odisha State Beverages Corporation Limited";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "73") // Mobile Tower Service
            {
                lblMessage.Text = "Department of Electronics and Information Technologies (E & IT)";
                ServiceModalPopup.Show();
            }
            else if (str_FormId == "74") // DG SET INSTALLATION
            {
                lblMessage.Text = "Department of Energy";
                ServiceModalPopup.Show();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ServiceRedirect");
        }
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ServiceModalPopup.Hide();
        string Data = "";
        string Result = "";

        if (str_FormId == "30" || str_FormId == "31" || str_FormId == "32") // DrugLicense Service
        {
            Response.Redirect(ConfigurationManager.AppSettings["DrugLicense"].ToString() + "?ServiceID=" + str_FormId.ToString() + "&UserId=" + Session["InvestorId"].ToString() + "&ProposalNo=" + str_ProposalNo);
        }
        else if (str_FormId == "43" || str_FormId == "44" || str_FormId == "45" || str_FormId == "46" || str_FormId == "50" || str_FormId == "53" || str_FormId == "54" || str_FormId == "65" || str_FormId == "66" || str_FormId == "76" || str_FormId == "77" || str_FormId == "78" || str_FormId == "79") // Pollution Control Board
        {
            Response.Redirect(ConfigurationManager.AppSettings["PollutionControl"].ToString() + "?ServiceID=" + str_FormId.ToString() + "&UserId=" + Session["InvestorId"].ToString() + "&StrProposalID=" + str_ProposalNo);
        }
        else if (str_FormId == "25" || str_FormId == "26") // Tree Transit (Forest department) Service
        {
           
            Data= FOREST();
            Response.Redirect(ConfigurationManager.AppSettings["TreeTransit"].ToString() + "?" + Data + "");
        }
        else if (str_FormId == "49") // PartnershipFirm Service
        {
            Response.Redirect(ConfigurationManager.AppSettings["PartnershipFirm"].ToString() + "?UserId=" + Session["InvestorId"].ToString() + "&ProposalId=" + str_ProposalNo);
        }
        else if (str_FormId == "20") // Building Plan Approval Service
        {
            Data = BPAS();
            Response.Redirect(ConfigurationManager.AppSettings["BPASRedirectionURL"].ToString() + Data);
        }
        else if (str_FormId == "29") // Obtaining water connection
        {
            Data = GOIPAS();
            Response.Redirect(ConfigurationManager.AppSettings["GOIPASRedirectionURL"].ToString() + "?Query=" + Data + "");
        }
        else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72" || str_FormId == "37") //F&B and Labour Service -- 37 Add by anil
        {
            Data = PAReSHRAM();
            Response.Redirect(ConfigurationManager.AppSettings["PARESHRAMREDIRECTIONURL"].ToString() + "?" + Data + "");
        }
       
        else if (str_FormId == "62" || str_FormId == "63") // Fire Service
        {
            Data = FIRE();
            Response.Redirect(ConfigurationManager.AppSettings["FIREREDIRECTIONURL"].ToString() + "?Query=" + Data + "");
        }
        else if (str_FormId == "67" || str_FormId == "68") // Excise Service
        {
            if (str_FormId == "67")
            {
                Data = EXCISE();
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISESRREDIRECTIONURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"EncryptedString\":\"" + Data + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Result = response.StatusCode.ToString();
                if (Result != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall67", client.ToString(), Result.ToString());
                }
                else
                {
                    Response.Redirect(response.ResponseUri.ToString());
                }
            }
            else
            {
                Data = EXCISE();
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISEGNSREDIRECTIONURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"EncryptedString\":\"" + Data + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Result = response.StatusCode.ToString();
                if (Result != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall68", client.ToString(), Result.ToString());
                }
                else
                {
                    Response.Redirect(response.ResponseUri.ToString());
                }
            }
        }
        else if (str_FormId == "69") // OSBC Service
        {
            Data = OSBC();
            Util.LogRequestResponse("ExciseServiceCall69", "QuerystringData :-", Data);
            Response.Redirect(ConfigurationManager.AppSettings["OSBCREDIRECTIONURL"].ToString() + "?encData=" + Data + "");
        }
        else if (str_FormId == "73") // MT Service
        {
            Data = EIT();
            Response.Redirect(ConfigurationManager.AppSettings["MobileTowerRedirectionUrl"].ToString() + "?" + Data + "");
        }
        else if (str_FormId == "74") // DG SET INSTALLATION
        {
            Response.Redirect(ConfigurationManager.AppSettings["DGSETREDIRECTIONURL"].ToString());
        }
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ServiceModalPopup.Hide();
        Response.Redirect("ServiceInstructionExternal.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo + "&Amount=" + str_Amount + "&ServiceType=" + Int_ServiceType + "&ReqMode=" + str_RequestMode);
    }

    private string BPAS()
    {
        DataTable dt = new DataTable();
        string EncryptValue = "";
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_BPAS_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "BPASSP");
            }
            finally
            {
                
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                    var client = new RestClient("" + AppBmcUrl + "");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "~::~" + dt.Rows[0]["VCH_EMAIL"].ToString() + "~::~" + dt.Rows[0]["VCH_INV_USERID"].ToString() + "~::~" + output + "~::~" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "~::~" + "1" + "\"}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    string JSON = response.Content;
                    var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                    EncryptValue = dict["result"].ToString();
                }
                else
                {
                    string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                    var client = new RestClient("" + AppBmcUrl + "");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + output + "~::~" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "~::~" + "1" + "\"}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    string JSON = response.Content;
                    var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                    EncryptValue = dict["result"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "BPASENC");
        }
        return EncryptValue;
    }
    private string GOIPAS()
    {
        string EncryptValue = "";
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GOIPAS_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "GOIPASSP");
            }
            finally
            {
               
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                    string Data = "" + dt.Rows[0]["VCH_PAN"].ToString() + "&" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&" + dt.Rows[0]["VCH_UNIQUEID"].ToString() + "&" + dt.Rows[0]["INT_DISTRICT"].ToString() + "&" + output + "&" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "";
                    EncryptValue = obj.Encrypt(Data, "m8s3e3k5");
                }
                else
                {
                    EncryptValue = "";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GOIPASENC");
        }
        return EncryptValue;
    }
    private string PAReSHRAM()
    {
        string EncryptValue = "";
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_PAReSHRAM_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PAReSHRAMSP");
                
            }
            finally
            {
                
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    string Industry_name = Uri.EscapeDataString(dt.Rows[0]["VCH_INV_NAME"].ToString());
                    EncryptValue = "appln_id=" + output + "&service_code=" + str_FormId.ToString() + "&pan=" + dt.Rows[0]["VCH_PAN"].ToString() + "&name=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&mobile_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&est_name=" + Industry_name;
                }
                else
                {
                    EncryptValue = "appln_id=" + output + "&service_code=" + str_FormId.ToString() + "&pan=" + "" + "&name=" + "" + "&mobile_number=" + "" + "&email=" + "" + "&est_name=''";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PAReSHRAMENC");
        }
        return EncryptValue;
    }

    private string FOREST()
    {
        string EncryptValue = "";
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_FOREST_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Forest");
               
            }
            finally
            {
               
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    string Industry_name = Uri.EscapeDataString(dt.Rows[0]["VCH_INV_NAME"].ToString());
                    EncryptValue = "appln_id=" + output + "&service_code=" + str_FormId.ToString() + "&pan=" + dt.Rows[0]["VCH_PAN"].ToString() + "&name=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&mobile_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&est_name=" + Industry_name+ "&UserId="+ Session["InvestorId"].ToString();
                }
                else
                {
                    EncryptValue = "appln_id=" + output + "&service_code=" + str_FormId.ToString() + "&pan=" + "" + "&name=" + "" + "&mobile_number=" + "" + "&email=" + "" + "&est_name=''"+ "&UserId=''";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Forest");
        }
        return EncryptValue;
    }

    private string FIRE()
    {
        string EncryptValue = "";
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_FIRE_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "FIRESP");
            }
            finally
            {
               
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    var client = new RestClient("" + ConfigurationManager.AppSettings["FIRECHECKSTATUSURL"].ToString() + "?name='" + dt.Rows[0]["VCH_INV_NAME"].ToString() + "'&email='" + dt.Rows[0]["VCH_EMAIL"].ToString() + "'&mobile='" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "'&serviceId=" + str_FormId + "&applicationId='" + output + "'&mode=" + 1 + "&source='GOSWIFT'&returnUrl='" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "'");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AlwaysMultipartFormData = true;
                    IRestResponse response = client.Execute(request);
                    string JSON = response.Content;
                    var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                    EncryptValue = dict["result"].ToString();
                }
                else
                {
                    var client = new RestClient("" + ConfigurationManager.AppSettings["FIRECHECKSTATUSURL"].ToString() + "?name=''&email=''&mobile=''&serviceId=" + str_FormId + "&applicationId='" + output + "'&mode=" + 1 + "&source='GOSWIFT'&returnUrl='" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "'");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AlwaysMultipartFormData = true;
                    IRestResponse response = client.Execute(request);
                    string JSON = response.Content;
                    var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                    EncryptValue = dict["result"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "FIREENC");
        }
        return EncryptValue;
    }
    private string EXCISE()
    {
        string strplaintext = "";
        string EncryptValue = "";
        string strSuppliedKey = "?åLˆ'KX¾p ;™¶%M8º}ÌqE-ƒU§©	;±½";
        byte[] key = { };
        key = Encoding.ASCII.GetBytes(strSuppliedKey);
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_EXCISE_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "EXCISESP");
            }
            finally
            {
                
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    strplaintext = "" + dt.Rows[0]["VCH_INV_NAME"].ToString() + "|" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "|" + str_FormId + "|" + output + "|1";
                    EncryptValue = ExciseEncryptAlgorthim(strplaintext, key);
                }
                else
                {
                    strplaintext = "NA|NA|NA|NA|1";
                    EncryptValue = ExciseEncryptAlgorthim(strplaintext, key);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EXCISEENC");
        }
        return EncryptValue;
    }
    public static string ExciseEncryptAlgorthim(string plainText, byte[] Key)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        byte[] encrypted;
        string base64encrypted;
        using (AesManaged aesAlg = new AesManaged())
        {
            aesAlg.Key = Key;
            aesAlg.Mode = CipherMode.ECB;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        base64encrypted = Convert.ToBase64String(encrypted, 0, encrypted.Length);
        return base64encrypted.Replace("/", "-");
    }
    public string EIT()
    {
        string EncryptValue = "";
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_EIT_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "EITSP");
                
            }
            finally
            {
                
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    EncryptValue = "authorised_person=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&phone_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&district=" + dt.Rows[0]["vchDistrictName"].ToString() + "&uniqid=" + output + "&ServiceId=" + str_FormId + "";
                }
                else
                {
                    EncryptValue = "authorised_person=" + "" + "&phone_number=" + "" + "&email=" + "" + "&district=" + "" + "&uniqid=" + output + "&ServiceId=" + str_FormId + "";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EITENC");
        }
        return EncryptValue;
    }
    private string OSBC()
    {
        string EncryptValue = "";
        DataTable dt = new DataTable();
        ExciseOSBCServiceReference.OSBCSoftSoapClient objEx = new ExciseOSBCServiceReference.OSBCSoftSoapClient();
        ExciseOSBCServiceReference.SupDetails objEntity = new ExciseOSBCServiceReference.SupDetails();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_OSBC_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "OSBCSP");
            }
            finally
            {
                
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            Util.LogRequestResponse("ExciseServiceCall69", "Output :-", output);
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    objEntity.Application_No = output;
                    objEntity.GoSwiftUserID = dt.Rows[0]["VCH_INV_USERID"].ToString();
                    objEntity.MobileNo = dt.Rows[0]["VCH_OFF_MOBILE"].ToString(); ;
                    objEntity.Name = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                    objEntity.Email = dt.Rows[0]["VCH_EMAIL"].ToString();
                    objEntity.Sector_Type = dt.Rows[0]["VCH_SECTOR"].ToString();
                    objEntity.Sector_Subtype = dt.Rows[0]["vchSubSectorName"].ToString();
                    objEntity.ServiceID = str_FormId;
                    objEntity.Source = "GOSWIFT";
                    objEntity.Active_Status = "Yes";
                    objEntity.ReturnURL = "http://203.193.144.114/SWP/Login.aspx";
                    JavaScriptSerializer jsonSerialiser1 = new JavaScriptSerializer();
                    var json1 = jsonSerialiser1.Serialize(objEntity);
                    
                    Util.LogRequestResponse("ExciseServiceCall69", "pushdataforEncription :-", json1);
                    EncryptValue = objEx.AESEncryptForSignUP(objEntity);
                    Util.LogRequestResponse("ExciseServiceCall69", "encriptedvalues :-", EncryptValue);
                }
                else
                {
                    objEntity.Application_No = output;
                    objEntity.GoSwiftUserID = "";
                    objEntity.MobileNo = "";
                    objEntity.Name = "";
                    objEntity.Email = "";
                    objEntity.Sector_Type = "";
                    objEntity.Sector_Subtype = "";
                    objEntity.ServiceID = str_FormId;
                    objEntity.Source = "GOSWIFT";
                    objEntity.Active_Status = "Yes";
                    objEntity.ReturnURL = "http://203.193.144.114/SWP/Login.aspx";
                    EncryptValue = objEx.AESEncryptForSignUP(objEntity);
                    Util.LogRequestResponse("ExciseServiceCall69", "encriptedvalues :-", EncryptValue);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OSBCENC");
        }
        return EncryptValue;
    }
}
