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

public partial class ServiceInstruction : SessionCheck
{
    #region Globalvariable
    /// <summary>
    /// Prasun Kali
    /// All global variable are declared here
    /// </summary>
    string str_FormId = "";
    int Int_ServiceType = 0;
    string str_ServiceType = "";
    string str_ProposalNo = "";
    string str_Amount = "0";
    string str_RequestMode = "";
    CmsBusinesslayer objService = new CmsBusinesslayer();
    List<EntityLayer.CMS.CMSDetails> onjentity = new List<EntityLayer.CMS.CMSDetails>();
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    UserDetail _objUser = new UserDetail();
    ExternalServiceIntegration.Serviceinfo objservice = new ExternalServiceIntegration.Serviceinfo();
    ExternalServiceIntegration objSvc = new ExternalServiceIntegration();
    List<ExternalServiceIntegration.Serviceinfo> objServicelist = new List<ExternalServiceIntegration.Serviceinfo>();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        // myNavbar.Visible = false;

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
                // lblService.Text = GetServiceName(Request.QueryString["FormId"].ToString());
            }

            /*----------------------------------------------------------------------------*/
            //// The below section will be executed when the request come for multiple services.
            /*----------------------------------------------------------------------------*/

            if (Request.QueryString["ReqMode"] == "M") //// Multiple Services
            {
                if (Session["SvcMasterData"] != null)
                {
                    DataTable dt = (DataTable)Session["SvcMasterData"];
                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder sbMenu = new StringBuilder();
                        sbMenu.Append("<ul class='nav nav-tabs'>");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int intSerialNo = Convert.ToInt32(dt.Rows[i]["intSlNo"]);
                            string strServiceId = Convert.ToString(dt.Rows[i]["intServiceId"]);
                            string strFormName = Convert.ToString(dt.Rows[i]["vchFormName"]);
                            string strServiceName = Convert.ToString(dt.Rows[i]["vchServiceName"]);
                            int intServiceType = Convert.ToInt32(dt.Rows[i]["intServiceType"]);
                            int intExternalType = Convert.ToInt32(dt.Rows[i]["intExternalType"]);
                            string strProposalNo = Convert.ToString(dt.Rows[i]["vchProposalNo"]);
                            decimal decAmount = Convert.ToDecimal(dt.Rows[i]["decAmount"]);

                            //// URL Formation                          
                            string strUrl = "ServiceInstruction.aspx?ReqMode=M&FormId=" + strServiceId + "&ServiceType=" + intServiceType + "&ProposalNo=" + strProposalNo + "&Amount=" + decAmount;
                            string act = "<span class='spanicon pending'><i class='fa fa-times' aria-hidden='true'></i> </span>";
                            if (Request.QueryString["FormId"] == "" || Request.QueryString["FormId"] == null)//// When the request come for 1st time,this section will be executed.
                            {
                                if (intSerialNo == 1) //// Keep the TAB in active stage for service present on SlNo-1
                                {
                                    sbMenu.Append("<li><a id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>\"" + strServiceName + "\"</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");

                                    FillContent(strServiceId);
                                    //lblService.Text = GetServiceName(strServiceId);
                                }
                                else
                                {
                                    sbMenu.Append("<li><a id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>\"" + strServiceName + "\"</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");

                                }
                            }
                            else
                            {
                                if (Request.QueryString["FormId"] == strServiceId)
                                {
                                    sbMenu.Append("<li><a id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>\"" + strServiceName + "\"</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");

                                }
                                else
                                {
                                    sbMenu.Append("<li><a id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>\"" + strServiceName + "\"</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");

                                }
                            }
                        }
                        sbMenu.Append("</ul>");

                    }
                }
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
                //  HyprLnk.NavigateUrl = obj[0].strAttachment;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    private void FillContent(string strFormId)
    {
        try
        {
            EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();
            objServiceEntity.StrAction = "GCD";
            objServiceEntity.IntServiceId = Convert.ToInt32(strFormId);
            IList<CMSDetails> obj = objService.GetCMSData(objServiceEntity);
            if (obj.Count > 0)
            {
                divabout.InnerHtml = obj[0].StrContent;
                // HyprLnk.NavigateUrl = obj[0].strAttachment;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #region "UniqueKey Generation"
    /// <summary>
    /// Radhika Rani Patri
    /// Using service id generate unique key from database level
    /// </summary>
    /// <param name="ServiceId"></param>
    /// <returns></returns>
    public string uniqueKeyGenerate(int ServiceId)
    {
        string EmpName = "";
        //SqlConnection connection = new SqlConnection(connectionString);

        using (SqlCommand cmd = new SqlCommand("USP_UNIQUE_KEY_COMMON"))
        {
            List<ListItem> customers = new List<ListItem>();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PINT_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(ServiceId);
            cmd.Parameters.Add("@P_VCH_PROPOSALID", SqlDbType.VarChar).Value = Request.QueryString["ProposalNo"].ToString();
            cmd.Parameters.Add("@P_VCH_INVESTOR_NAME", SqlDbType.VarChar).Value = Session["IndustryName"].ToString();
            cmd.Parameters.Add("@P_INT_STATUS", SqlDbType.Int).Value = 10;
            cmd.Parameters.Add("@P_INT_ACTION_TAKEN_BY", SqlDbType.Int).Value = Convert.ToInt32(Session["InvestorId"]);
            cmd.Parameters.Add("@P_INT_PAYMENT_STATUS", SqlDbType.Int).Value = 3;
            cmd.Parameters.Add("@P_NUM_PAYMENT_AMOUNT", SqlDbType.Money).Value = 0;
            cmd.Parameters.Add("@P_VCH_GROUP_ID", SqlDbType.VarChar).Value = Session["vchGROUPID"].ToString();
            cmd.Parameters.Add("@INT_CREATEDBY", SqlDbType.Int).Value = Convert.ToInt32(Session["InvestorId"]);
            cmd.Parameters.Add("@P_VCH_PAN", SqlDbType.VarChar).Value = Convert.ToString(Session["PAN"]);
            cmd.Parameters.Add("@PMSG_OUT", SqlDbType.VarChar, 50);
            cmd.Parameters["@PMSG_OUT"].Direction = ParameterDirection.Output;
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            EmpName = cmd.Parameters["@PMSG_OUT"].Value.ToString();
            conn.Close();
        }

        return EmpName;
    }
    #endregion
    protected void BtnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            if (Int_ServiceType == 0) //For Internal Service
            {
                if (str_RequestMode == "S")
                {
                    Response.Redirect("FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo + "&ReqMode=" + str_RequestMode);
                }
                else
                {
                    DataTable dt = (DataTable)Session["SvcMasterData"];
                    DataRow dr = dt.AsEnumerable().Where(r => ((string)r["intServiceId"].ToString()).Equals(str_FormId)).First();

                    if (dr["intExternalType"].ToString() == "1" && dr["intServiceType"].ToString() == "0")
                    {
                        string strUnqId = string.Empty;
                        bool exists = dt.Select().ToList().Exists(row => row["vchTrackingId"].ToString() != ""); // add by anil for check group code is alrady created or  not then created .
                        if (dr["vchApplicationKey"].ToString() == "")
                        {
                            if (!exists)
                            {
                                Session["vchGROUPID"] = GETGROUPID();  //generate group code 
                                dr["vchTrackingId"] = GETGROUPID();
                            }
                            else
                            {
                                Session["vchGROUPID"] = (from DataRow dr1 in dt.Rows
                                                         where (string)dr1["vchTrackingId"] != ""
                                                         select (string)dr1["vchTrackingId"]).FirstOrDefault();
                            }
                            //if (Session["vchGROUPID"] == null)
                            //{
                            //    Session["vchGROUPID"] = GETGROUPID();
                            //}

                            strUnqId = uniqueKeyGenerate(Convert.ToInt32(str_FormId.ToString()));
                            dr["vchApplicationKey"] = strUnqId;
                            dr["intCompletedStatus"] = 1;// for external service Consolidate status apply add by anil
                        }
                        else
                        {
                            strUnqId = dr["vchApplicationKey"].ToString();
                        }

                        string urlchk = string.Empty;
                        if (str_FormId.ToString() == "10")
                        {
                            string strReq1 = "Token=" + strUnqId + "&" + getuserprofile();
                            strReq1 = EncryptQueryString(strReq1);
                            urlchk = dr["vchUrl"].ToString() + "?" + strReq1;
                        }
                        else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72") //F&B and Labour Service
                        {
                            // string strData = PAReSHRAM();
                            //  urlchk = ConfigurationManager.AppSettings["PARESHRAMREDIRECTIONURL"].ToString() + "?" + strData + "";
                            DataTable Dtdetails = GetInveserDeatils(); // on url parameter   send to pareshram protal
                            urlchk = dr["vchUrl"].ToString().Replace("{{servid}}", str_FormId.ToString()).Replace("{{prno}}", str_ProposalNo.ToString()).Replace("{{uid}}", Session["InvestorId"].ToString()).Replace("{{apkey}}", strUnqId).Replace("{{name}}", Dtdetails.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString()).Replace("{{mobileno}}", Dtdetails.Rows[0]["VCH_OFF_MOBILE"].ToString()).Replace("{{panno}}", Dtdetails.Rows[0]["VCH_PAN"].ToString());
                        }
                        else
                        {
                            urlchk = dr["vchUrl"].ToString().Replace("{{servid}}", str_FormId.ToString()).Replace("{{uid}}", Session["InvestorId"].ToString()).Replace("{{apkey}}", strUnqId).Replace("{{ssoid}}", Session["UID"].ToString()).Replace("{{prno}}", dr["vchProposalNo"].ToString());
                        }

                        dr["vchUpdateUrl"] = urlchk;
                        Session["ProposalNo"] = str_ProposalNo;
                        Response.Redirect(urlchk, false);
                    }
                    else
                    {
                        string urlchk = "FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo + "&ReqMode=" + str_RequestMode;
                        dr["vchUpdateUrl"] = urlchk;
                        Response.Redirect("FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo + "&ReqMode=" + str_RequestMode);
                    }
                }
            }
            else  // For External Service
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
                else if (str_FormId == "43" || str_FormId == "44" || str_FormId == "45" || str_FormId == "46" || str_FormId == "50" || str_FormId == "53" || str_FormId == "54" || str_FormId == "65" || str_FormId == "66") // Pollution Control Board
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
                else if (str_FormId == "41") // Permission to draw Water
                {
                    if (str_ProposalNo != "" || str_ProposalNo != null)
                    {
                        Response.Redirect("FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo);
                    }
                    else
                    {
                        Response.Redirect("FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + 0);
                    }
                }
                else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72") //F&B and Labour Service
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
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ServiceRedirect");
        }
    }
    /// <summary>
    /// Get investor details  add by anil
    /// </summary>
    /// <returns></returns>
    private DataTable GetInveserDeatils() 
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
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
            throw ex;
        }
        return dt;
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
        else if (str_FormId == "43" || str_FormId == "44" || str_FormId == "45" || str_FormId == "46" || str_FormId == "50" || str_FormId == "53" || str_FormId == "54" || str_FormId == "65" || str_FormId == "66") // Pollution Control Board
        {
            Response.Redirect(ConfigurationManager.AppSettings["PollutionControl"].ToString() + "?ServiceID=" + str_FormId.ToString() + "&UserId=" + Session["InvestorId"].ToString() + "&StrProposalID=" + str_ProposalNo);
        }
        else if (str_FormId == "25" || str_FormId == "26") // Tree Transit Service
        {
            Response.Redirect(ConfigurationManager.AppSettings["TreeTransit"].ToString() + "?ServiceID=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo + "&UserId=" + Session["InvestorId"].ToString() + "&page=");
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
        else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72") //F&B and Labour Service
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
        Response.Redirect("ServiceInstruction.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo + "&Amount=" + str_Amount + "&ServiceType=" + Int_ServiceType + "&ReqMode=" + str_RequestMode);
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
                cmd = null;
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
                cmd = null;
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
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());// GENERATE NEW APPLICATION ID
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    EncryptValue = "appln_id=" + output + "&service_code=" + str_FormId.ToString() + "&pan=" + dt.Rows[0]["VCH_PAN"].ToString() + "&name=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&mobile_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&est_name=" + dt.Rows[0]["VCH_INV_NAME"].ToString();
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
                cmd = null;
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
                cmd = null;
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
                throw ex;
            }
            finally
            {
                cmd = null;
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
                cmd = null;
                conn.Close();
            }
            string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
            if (output != "")
            {
                if (dt.Rows.Count > 0)
                {
                    objEntity.Application_No = output;
                    objEntity.GoSwiftUserID = dt.Rows[0]["VCH_INV_USERID"].ToString();
                    objEntity.MobileNo = "8249761028";
                    objEntity.Name = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                    objEntity.Email = dt.Rows[0]["VCH_EMAIL"].ToString();
                    objEntity.Sector_Type = dt.Rows[0]["VCH_SECTOR"].ToString();
                    objEntity.Sector_Subtype = dt.Rows[0]["vchSubSectorName"].ToString();
                    objEntity.ServiceID = str_FormId;
                    objEntity.Source = "GOSWIFT";
                    objEntity.Active_Status = "Yes";
                    EncryptValue = objEx.AESEncryptForSignUP(objEntity);
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
                    EncryptValue = objEx.AESEncryptForSignUP(objEntity);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OSBCENC");
        }
        return EncryptValue;
    }

    private string GETGROUPID()
    {
        DataTable dt = new DataTable();
        string EncryptValue = "";

        SqlCommand cmd = new SqlCommand();
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GET_GROUP_ID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VCH_ACTION", "U");

            SqlParameter par;
            par = cmd.Parameters.Add("@P_VCHOUT", SqlDbType.VarChar, 100);
            par.Direction = System.Data.ParameterDirection.Output;
            cmd.Connection = conn;
            cmd.ExecuteReader();

            conn.Close();
            EncryptValue = (string)cmd.Parameters["@P_VCHOUT"].Value;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GETGROUPID");
        }
        finally
        {
            cmd = null;
            conn.Close();
        }

        return EncryptValue;
    }

    private string getuserprofile()
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
                cmd.CommandText = "Usp_GetUserProfile";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_UserId", Convert.ToInt32(Session["InvestorId"].ToString()));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    EncryptValue = "Phone=" + dt.Rows[0]["Unit_MobileNo"].ToString() + "&Email=" + dt.Rows[0]["Unit_EmailId"].ToString() + "&PanNo=" + dt.Rows[0]["VCH_PAN"].ToString();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "getuserprofile");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "getuserprofile");
        }
        return EncryptValue;
    }


    private string EncryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Encrypt(strQueryString, "m8s3e3k5");
    }



    /// <summary>
    /// Summary description for EncryptDecryptQueryString
    /// </summary>
    public class EncryptDecryptQueryString
    {
        private byte[] key = { };
        private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}