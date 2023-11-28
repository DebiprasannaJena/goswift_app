using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using System.Configuration;
using System.Data.SqlClient;
using RestSharp;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;

public partial class DraftedServices : System.Web.UI.Page
{
    DataTable dtable;
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    ProposalDet objProposal = new ProposalDet();
    string strRetval = "";
    int intRetVal = 0;
    int dtval = 0;
    int Exdtval = 0;
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserId"] != null)
            {
                BindGridDraft();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }

    #region Member Function
    private void BindGridDraft()
    {
        objProposal = new ProposalDet();
        try
        {
            List<ProposalDet> objProposalList = new List<ProposalDet>();
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "D";
            objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);

            ///Get All Drafted Applications
            List<ServiceDetails> ServiceDetail = objService.GetAllDraftedApplicationDetails(Session["UserId"].ToString()).ToList();

            ///Filter Internal Services
            List<EntityLayer.Service.ServiceDetails> ObjInternalSvc = ServiceDetail.Where(n => n.str_checkStatus == "0" && n.intStatus==10).ToList() ;
            gvDraftService.DataSource = ObjInternalSvc;
            gvDraftService.DataBind();
            MergeRows(gvDraftService);

            ///Filter External Services
            List<EntityLayer.Service.ServiceDetails> ObjExternalSvc = ServiceDetail.Where(n => n.str_checkStatus == "1").ToList();
            gvExDraftService.DataSource = ObjExternalSvc;
            gvExDraftService.DataBind();

            if (ObjInternalSvc.Count > 0)
            {
                icon.Visible = true;
            }
            else
            {
                icon.Visible = false;
            }

            if (ObjExternalSvc.Count > 0)
            {
                Exicon.Visible = true;
            }
            else
            {
                Exicon.Visible = false;
            }

            dtval = ObjInternalSvc.Count;
            Exdtval = ObjExternalSvc.Count;

            DisplayPaging();
            ExDisplayPaging();

            Session["SvcMasterData"] = null;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftService");
        }
        finally
        {
            objProposal = null;
        }
    }
    #endregion

    protected void gvDraftService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDraftService.PageIndex = e.NewPageIndex;
        BindGridDraft();
    }
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        LinkButton LnkSeletedRow = sender as LinkButton;
        GridViewRow row = LnkSeletedRow.NamingContainer as GridViewRow;

        Label LblServiceName = (Label)row.FindControl("LblServiceName");
        HiddenField Hid_Service_Type = (HiddenField)row.FindControl("Hid_Service_Type");
        HiddenField Hid_Dept_Name = (HiddenField)row.FindControl("Hid_Dept_Name");        

        int intServiceId = Convert.ToInt32(gvDraftService.DataKeys[row.RowIndex].Values["intServiceId"]);
        string strApplicationKey = Convert.ToString(gvDraftService.DataKeys[row.RowIndex].Values["str_ApplicationNo"]);
        string strProposalNo = Convert.ToString(gvDraftService.DataKeys[row.RowIndex].Values["strProposalId"]);
        string strGroupKey = Convert.ToString(gvDraftService.DataKeys[row.RowIndex].Values["vchTranscationNo"]);
        int intExternalType = Convert.ToInt32(gvDraftService.DataKeys[row.RowIndex].Values["intExternalType"]);
        string strUrl = Convert.ToString(gvDraftService.DataKeys[row.RowIndex].Values["Str_ExtrnalServiceUrl"]); 

        string strServiceName = LblServiceName.Text;       
        int intServiceType = Convert.ToInt32(Hid_Service_Type.Value);
        string strDeptName = Hid_Dept_Name.Value;
        decimal decAmount = 0;
        int intCompletedStatus = 0;

        /*-----------------------------------------------------------------------------------*/

        DataTable dt = new DataTable();
        dt.Columns.Add("intSlNo", typeof(int));
        dt.Columns.Add("intServiceId", typeof(string));       
        dt.Columns.Add("vchServiceName", typeof(string));
        dt.Columns.Add("intServiceType", typeof(string));
        dt.Columns.Add("intExternalType", typeof(string));
        dt.Columns.Add("vchProposalNo", typeof(string));
        dt.Columns.Add("decAmount", typeof(string));
        dt.Columns.Add("intCompletedStatus", typeof(string));
        dt.Columns.Add("vchApplicationKey", typeof(string));
        dt.Columns.Add("vchUrl", typeof(string));
        dt.Columns.Add("vchUpdateUrl", typeof(string));
        dt.Columns.Add("vchDeptName", typeof(string));
        dt.Columns.Add("intHoaAccount", typeof(string));
        dt.Columns.Add("vchTrackingId", typeof(string));

        if (strGroupKey != "")
        {
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            DataTable dtApp = objService.GetApplicationByTrackingId(strGroupKey);
            if (dtApp.Rows.Count > 0)
            {
                for (int i = 0; i < dtApp.Rows.Count; i++)
                {
                    strApplicationKey = Convert.ToString(dtApp.Rows[i]["VCH_APPLICATION_UNQ_KEY"]);
                    intServiceId = Convert.ToInt32(dtApp.Rows[i]["INT_SERVICEID"]);
                    strProposalNo = Convert.ToString(dtApp.Rows[i]["VCH_PROPOSALID"]);
                    decAmount = Convert.ToDecimal(dtApp.Rows[i]["NUM_PAYMENT_AMOUNT"]);
                    intExternalType = Convert.ToInt32(dtApp.Rows[i]["INT_EXTERNAL_TYPE"]);
                    strServiceName = Convert.ToString(dtApp.Rows[i]["VCH_SERVICENAME"]);
                    intServiceType = Convert.ToInt32(dtApp.Rows[i]["INT_SERVICE_TYPE"]);
                    strUrl = Convert.ToString(dtApp.Rows[i]["VCH_EXTERNAL_SERVICE_URL"]);
                    strDeptName = Convert.ToString(dtApp.Rows[i]["nvchLevelName"]);

                    //if (intServiceType == 0 && intExternalType == 0) ////If Internal service
                    //{
                    //    intCompletedStatus = 1;
                    //}

                    dt.Rows.Add(i + 1, intServiceId, strServiceName, intServiceType, intExternalType, strProposalNo, decAmount, intCompletedStatus, strApplicationKey, strUrl, "", strDeptName, 0, strGroupKey);
                }
            }
        }
        else
        {
            //if(intServiceType==0 && intExternalType==0)
            //{
            //    intCompletedStatus = 1;
            //}

            dt.Rows.Add(1, intServiceId, strServiceName, intServiceType, intExternalType, strProposalNo, decAmount, intCompletedStatus, strApplicationKey, strUrl, "", strDeptName, 0, strGroupKey);
        }

        Session["SvcMasterData"] = dt;
        Response.Redirect("ServiceProcess.aspx?FormId=" + intServiceId + "&AppKey=" + strApplicationKey + "&ProposalNo=" + strProposalNo + "&ReqMode=M" + "&GroupKey=" + strGroupKey);
    }
    private string GOIPAS(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "GOIPASSP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                string Data = "" + dt.Rows[0]["VCH_PAN"].ToString() + "&" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&" + dt.Rows[0]["VCH_UNIQUEID"].ToString() + "&" + dt.Rows[0]["INT_DISTRICT"].ToString() + "&" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "&" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "";
                EncryptValue = obj.Encrypt(Data, "m8s3e3k5");
            }
            else
            {
                EncryptValue = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GOIPASENC");
        }
        return EncryptValue;
    }
    private string BPAS(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "BPASSP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }

            if (dt.Rows.Count > 0)
            {
                string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                var client = new RestClient("" + AppBmcUrl + "");
                var request = new RestRequest(Method.POST);
                request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "~::~" + dt.Rows[0]["VCH_EMAIL"].ToString() + "~::~" + dt.Rows[0]["VCH_INV_USERID"].ToString() + "~::~" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "~::~" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "~::~" + "2" + "\"}", ParameterType.RequestBody);
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
                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "~::~" + "2" + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string JSON = response.Content;
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                EncryptValue = dict["result"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "BPASENC");
        }
        return EncryptValue;
    }
    private string PAReSHRAM(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PAReSHRAMSP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                string Industry_name = Uri.EscapeDataString(dt.Rows[0]["VCH_INV_NAME"].ToString());
                EncryptValue = "appln_id=" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "&service_code=" + dt.Rows[0]["INT_SERVICEID"].ToString() + "&pan=" + dt.Rows[0]["VCH_PAN"].ToString() + "&name=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&mobile_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&est_name=" + Industry_name;
            }
            else
            {
                EncryptValue = "appln_id=" + "" + "&service_code=" + "" + "&pan=" + "" + "&name=" + "" + "&mobile_number=" + "" + "&email=" + "" + "&est_name=''";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PAReSHRAMENC");
        }
        return EncryptValue;
    }

    private string FOREST(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "FORESTSP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                string Industry_name = Uri.EscapeDataString(dt.Rows[0]["VCH_INV_NAME"].ToString());
                EncryptValue = "appln_id=" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "&service_code=" + dt.Rows[0]["INT_SERVICEID"].ToString() + "&pan=" + dt.Rows[0]["VCH_PAN"].ToString() + "&name=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&mobile_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&est_name=" + Industry_name + "&UserId=" + Session["InvestorId"].ToString();
            }
            else
            {
                EncryptValue = "appln_id=" + "" + "&service_code=" + "" + "&pan=" + "" + "&name=" + "" + "&mobile_number=" + "" + "&email=" + "" + "&est_name=''" + "&UserId=''";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "FORESTSP");
        }
        return EncryptValue;
    }
    private string FIRE(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "FIRESP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                var client = new RestClient("" + ConfigurationManager.AppSettings["FIRECHECKSTATUSURL"].ToString() + "?name='" + dt.Rows[0]["VCH_INV_NAME"].ToString() + "'&email='" + dt.Rows[0]["VCH_EMAIL"].ToString() + "'&mobile='" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "'&serviceId=" + dt.Rows[0]["INT_SERVICEID"].ToString() + "&applicationId='" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "'&mode=" + 2 + "&source='GOSWIFT'&returnUrl='" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "'");
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
                var client = new RestClient("" + ConfigurationManager.AppSettings["FIRECHECKSTATUSURL"].ToString() + "?name=''&email=''&mobile=''&serviceId=" + 0 + "&applicationId=''&mode=" + 2 + "&source='GOSWIFT'&returnUrl='" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "'");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AlwaysMultipartFormData = true;
                IRestResponse response = client.Execute(request);
                string JSON = response.Content;
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                EncryptValue = dict["result"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "FIREENC");
        }
        return EncryptValue;
    }
    private string EXCISE(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "EXCISSP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                strplaintext = "" + dt.Rows[0]["VCH_INV_NAME"].ToString() + "|" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "|" + dt.Rows[0]["INT_SERVICEID"].ToString() + "|" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "|2";
                EncryptValue = Exciseencryptalgorthim(strplaintext, key);
            }
            else
            {
                strplaintext = "NA|NA|NA|NA|2";
                EncryptValue = Exciseencryptalgorthim(strplaintext, key);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EXCISEENC");
        }
        return EncryptValue;
    }
    public static string Exciseencryptalgorthim(string plainText, byte[] Key)
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
    public string EIT(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "EITSP");
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                EncryptValue = "authorised_person=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&phone_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&district=" + dt.Rows[0]["vchDistrictName"].ToString() + "&uniqid=" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "&ServiceId=" + dt.Rows[0]["INT_SERVICEID"].ToString() + "";
            }
            else
            {
                EncryptValue = "authorised_person=" + "" + "&phone_number=" + "" + "&email=" + "" + "&district=" + "" + "&uniqid=" + "" + "&ServiceId=" + "" + "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EITENC");
        }
        return EncryptValue;
    }
    private string OSBC(string strApplicationKey)
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
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "OSBCSP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                objEntity.Application_No = dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString();
                objEntity.GoSwiftUserID = dt.Rows[0]["VCH_INV_USERID"].ToString();
                objEntity.MobileNo = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                objEntity.Name = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                objEntity.Email = dt.Rows[0]["VCH_EMAIL"].ToString();
                objEntity.Sector_Type = dt.Rows[0]["VCH_SECTOR"].ToString();
                objEntity.Sector_Subtype = dt.Rows[0]["vchSubSectorName"].ToString();
                objEntity.ServiceID = dt.Rows[0]["INT_SERVICEID"].ToString();
                objEntity.Source = "GOSWIFT";
                objEntity.Active_Status = "Yes";
                EncryptValue = objEx.AESEncryptForSignUP(objEntity);
            }
            else
            {
                objEntity.Application_No = "";
                objEntity.GoSwiftUserID = "";
                objEntity.MobileNo = "";
                objEntity.Name = "";
                objEntity.Email = "";
                objEntity.Sector_Type = "";
                objEntity.Sector_Subtype = "";
                objEntity.ServiceID = "";
                objEntity.Source = "GOSWIFT";
                objEntity.Active_Status = "Yes";
                EncryptValue = objEx.AESEncryptForSignUP(objEntity);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OSBCENC");
        }
        return EncryptValue;
    }

    protected void BtnApplyMultipe_Click(object sender, EventArgs e)
    {
        try
        {
            int intCheckStatus = 0;
            int intFormCount = 0;

            /*------------------------------------------------------------------------------------*/
            ///// The Description for below DataTable provided in "DepartmentClearance.aspx" Page.
            /*------------------------------------------------------------------------------------*/
            DataTable dt = new DataTable();

            dt.Columns.Add("intSlNo", typeof(int));
            dt.Columns.Add("intServiceId", typeof(string));
            dt.Columns.Add("vchFormName", typeof(string));
            dt.Columns.Add("vchServiceName", typeof(string));
            dt.Columns.Add("intServiceType", typeof(string));
            dt.Columns.Add("intExternalType", typeof(string));//changed by sourav on dated 10-03-2021
            dt.Columns.Add("vchProposalNo", typeof(string));
            dt.Columns.Add("decAmount", typeof(string));
            dt.Columns.Add("intCompletedStatus", typeof(string));
            dt.Columns.Add("vchApplicationKey", typeof(string));
            dt.Columns.Add("vchUrl", typeof(string));
            dt.Columns.Add("vchUpdateUrl", typeof(string));
            dt.Columns.Add("vchDeptName", typeof(string));
            dt.Columns.Add("intHoaAccount", typeof(string));

            for (int i = 0; i < gvDraftService.Rows.Count; i++)
            {
                int intServiceId = Convert.ToInt32(gvDraftService.DataKeys[i].Values["intServiceId"]);
                decimal decAmount = Convert.ToInt32(gvDraftService.DataKeys[i].Values["Dec_Amount"]);//changed by sourav on dated 10-03-2021
                int intServiceType = Convert.ToInt32(gvDraftService.DataKeys[i].Values["str_checkStatus"]);
                string strProposalNo = gvDraftService.DataKeys[i].Values["strProposalId"].ToString();
                string strApplicationKey = gvDraftService.DataKeys[i].Values["str_ApplicationNo"].ToString();
                int intExternalType = Convert.ToInt32(gvDraftService.DataKeys[i].Values["intExternalType"]);//changed by sourav on dated 10-03-2021
                string vchUrl = gvDraftService.DataKeys[i].Values["Str_ExtrnalServiceUrl"].ToString();
                CheckBox ChkBxSelect = (CheckBox)gvDraftService.Rows[i].FindControl("ChkBxSelect");
                Label LblServiceName = (Label)gvDraftService.Rows[i].FindControl("LblServiceName");

                if (ChkBxSelect.Checked)
                {
                    intCheckStatus = 1;
                    intFormCount = intFormCount + 1;

                    string strFormName = LblServiceName.Text.Length > 20 ? LblServiceName.Text.Substring(0, 20) + "..." : LblServiceName.Text;
                    int intcompletedstatus = 0;
                    string strRes = "SELECT ApplicationNo from T_CMN_FIN_DETAILS_LOG WHERE  ApplicationNo in('" + strApplicationKey + "')";

                    SqlCommand cmd = new SqlCommand(strRes, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < ds.Tables[0].Rows.Count; i1++)
                        {
                            intcompletedstatus = 1;
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (vchUrl != "")
                        {
                            dt.Rows.Add(intFormCount, intServiceId, strFormName, LblServiceName.Text, intServiceType, intExternalType, strProposalNo, decAmount, intcompletedstatus, strApplicationKey, vchUrl + "&ReqMode=M");
                        }
                        else
                        {
                            dt.Rows.Add(intFormCount, intServiceId, strFormName, LblServiceName.Text, intServiceType, intExternalType, strProposalNo, decAmount, 1, strApplicationKey, "FormEditView1.aspx?FormId=" + intServiceId + "&AppKey=" + strApplicationKey + "&ProposalNo=" + strProposalNo);
                        }
                    }
                    else
                    {
                        if (vchUrl != "")
                        {
                            dt.Rows.Add(intFormCount, intServiceId, strFormName, LblServiceName.Text, intServiceType, intExternalType, strProposalNo, decAmount, intcompletedstatus, strApplicationKey, vchUrl + "&ReqMode=M");
                        }
                        else
                        {
                            dt.Rows.Add(intFormCount, intServiceId, strFormName, LblServiceName.Text, intServiceType, intExternalType, strProposalNo, decAmount, 1, strApplicationKey, "FormEditView1.aspx?FormId=" + intServiceId + "&AppKey=" + strApplicationKey + "&ProposalNo=" + strProposalNo);
                        }
                    }
                }
            }

            if (intCheckStatus == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "jAlert('<strong>Please select at least one check box to proceed !</strong>');", true);
                return;
            }

            Session["SvcMasterData"] = dt;
            Response.Redirect("ServiceProcess.aspx?ReqMode=M", false); ////M-Multiple Apply, S-Single Apply
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftService");
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                this.gvDraftService.PageIndex = 0;
                this.gvDraftService.AllowPaging = false;
            }
            else
            {
                lbtnAll.Text = "All";
                this.gvDraftService.AllowPaging = true;
            }

            BindGridDraft();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftService");
        }
    }
    protected void DisplayPaging()
    {
        try
        {
            if (gvDraftService.Rows.Count > 0)
            {
                lblPaging.Visible = true;
                lbtnAll.Visible = true;
                if (gvDraftService.PageIndex + 1 == gvDraftService.PageCount)
                {
                    lblPaging.Text = "Results <b>" + ((Label)gvDraftService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + dtval + "</b> of <b>" + dtval + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results <b>" + ((Label)gvDraftService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)gvDraftService.Rows[0].FindControl("lblsl")).Text) + gvDraftService.PageSize - 1) + "</b> of <b>" + dtval + "</b>";
                }
            }
            else
            {
                lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftService");
        }
    }


    protected void ExDisplayPaging()
    {
        try
        {
            if (gvExDraftService.Rows.Count > 0)
            {
                ExlblPaging.Visible = true;
                ExlbtnAll.Visible = true;
                if (gvExDraftService.PageIndex + 1 == gvExDraftService.PageCount)
                {
                    ExlblPaging.Text = "Results <b>" + ((Label)gvExDraftService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Exdtval + "</b> of <b>" + Exdtval + "</b>";
                }
                else
                {
                    this.ExlblPaging.Text = "Results <b>" + ((Label)gvExDraftService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)gvExDraftService.Rows[0].FindControl("lblsl")).Text) + gvExDraftService.PageSize - 1) + "</b> of <b>" + Exdtval + "</b>";
                }
            }
            else
            {
                ExlblPaging.Visible = false;
                ExlbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftService");
        }
    }


    protected void gvExDraftService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvExDraftService.PageIndex = e.NewPageIndex;
        BindGridDraft();
    }
    protected void ExLinkEdit_Click(object sender, EventArgs e)
    {
        LinkButton LnkSeletedRow = sender as LinkButton;
        GridViewRow row = LnkSeletedRow.NamingContainer as GridViewRow;
        string svcid1 = gvExDraftService.DataKeys[row.RowIndex].Values[1].ToString();
        string strApplicationKey = gvExDraftService.DataKeys[row.RowIndex].Values[8].ToString();
        string strProposalNo = gvExDraftService.DataKeys[row.RowIndex].Values[0].ToString();
        string EncryptValue = "";
        string Result = "";

        if (Convert.ToInt32(svcid1) == 20)
        {
            EncryptValue = BPAS(strApplicationKey);
            Response.Redirect(ConfigurationManager.AppSettings["BPASRedirectionURL"].ToString() + EncryptValue);
        }
        else if (Convert.ToInt32(svcid1) == 29)
        {
            EncryptValue = GOIPAS(strApplicationKey);
            Response.Redirect(ConfigurationManager.AppSettings["GOIPASRedirectionURL"].ToString() + "?Query=" + EncryptValue + "");
        }
        else if (Convert.ToInt32(svcid1) == 62 || Convert.ToInt32(svcid1) == 63)
        {
            EncryptValue = FIRE(strApplicationKey);
            Response.Redirect(ConfigurationManager.AppSettings["FIREREDIRECTIONURL"].ToString() + "?Query=" + EncryptValue + "");
        }
        else if (Convert.ToInt32(svcid1) == 67 || Convert.ToInt32(svcid1) == 68)
        {
            EncryptValue = EXCISE(strApplicationKey);
            if (Convert.ToInt32(svcid1) == 67)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISESRREDIRECTIONURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"EncryptedString\":\"" + EncryptValue + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Result = response.StatusCode.ToString();
                if (Result != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall67", client.ToString(), Result.ToString());
                    Response.Redirect("DraftedServices.aspx");
                }
                else
                {
                    Response.Redirect(response.ResponseUri.ToString());
                }
            }
            else
            {
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISEGNSREDIRECTIONURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"EncryptedString\":\"" + EncryptValue + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Result = response.StatusCode.ToString();
                if (Result != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall68", client.ToString(), Result.ToString());
                    Response.Redirect("DraftedServices.aspx");
                }
                else
                {
                    Response.Redirect(response.ResponseUri.ToString());
                }
            }
        }
        else if (Convert.ToInt32(svcid1) == 69)
        {
            EncryptValue = OSBC(strApplicationKey);
            Response.Redirect(ConfigurationManager.AppSettings["OSBCREDIRECTIONURL"].ToString() + "?encData=" + EncryptValue + "");
        }
        else if (Convert.ToInt32(svcid1) == 5 || Convert.ToInt32(svcid1) == 6 || Convert.ToInt32(svcid1) == 7 || Convert.ToInt32(svcid1) == 34 || Convert.ToInt32(svcid1) == 35 || Convert.ToInt32(svcid1) == 36 || Convert.ToInt32(svcid1) == 39 || Convert.ToInt32(svcid1) == 40 || Convert.ToInt32(svcid1) == 70 || Convert.ToInt32(svcid1) == 71 || Convert.ToInt32(svcid1) == 72 || Convert.ToInt32(svcid1) == 37) // add by anil service 37 
        {
            EncryptValue = PAReSHRAM(strApplicationKey);
            Response.Redirect(ConfigurationManager.AppSettings["PARESHRAMREDIRECTIONURL"].ToString() + "?" + EncryptValue + "");
        }
        else if (Convert.ToInt32(svcid1) == 73)
        {
            EncryptValue = EIT(strApplicationKey);
            Response.Redirect(ConfigurationManager.AppSettings["MobileTowerRedirectionUrl"].ToString() + "?" + EncryptValue + "");
        }
        else if (svcid1 == "25" || svcid1 == "26") // Tree Transit Service
        {
            // Response.Redirect(ConfigurationManager.AppSettings["TreeTransit"].ToString() + "?ServiceID=" + svcid1.ToString() + "&ProposalNo=" + strProposalNo + "&UserId=" + Session["InvestorId"].ToString() + "&page=");

            EncryptValue = FOREST(strApplicationKey);
            Response.Redirect(ConfigurationManager.AppSettings["TreeTransit"].ToString() + "?" + EncryptValue + "");
        }
    }
    protected void ExlbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ExlbtnAll.Text == "All")
            {
                ExlbtnAll.Text = "Paging";
                this.gvExDraftService.PageIndex = 0;
                this.gvExDraftService.AllowPaging = false;
            }
            else
            {
                ExlbtnAll.Text = "All";
                this.gvExDraftService.AllowPaging = true;
            }

            BindGridDraft();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftService");
        }
    }

    public static void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];
            Label lblgrouipid = (Label)row.Cells[0].FindControl("lblgrouipid");
            Label previousRowlblgrouipid = (Label)previousRow.Cells[0].FindControl("lblgrouipid");

            if (lblgrouipid.Text != "")
            {
                if (lblgrouipid.Text == previousRowlblgrouipid.Text)
                {
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;
                    previousRow.Cells[0].Visible = false;
                    row.Cells[0].Style["vertical-align"] = "middle";
                    row.Cells[0].Style["text-align"] = "left";
                    row.Cells[6].RowSpan = previousRow.Cells[6].RowSpan < 2 ? 2 :
                                          previousRow.Cells[6].RowSpan + 1;
                    previousRow.Cells[6].Visible = false;
                    row.Cells[6].Style["vertical-align"] = "middle";
                    row.Cells[6].Style["text-align"] = "center";
                }
            }
        }
    }
}