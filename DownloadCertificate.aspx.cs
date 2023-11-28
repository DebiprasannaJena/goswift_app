using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.Service;
using BusinessLogicLayer.Service;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;
using System.Xml;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using ProfessionalTax;
using EntityLayer.Service;
using Newtonsoft.Json.Linq;


public partial class DownloadCertificate : SessionCheck
{
    #region Declare Global Variable and Class
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    ServiceStatus objServcStatus = new ServiceStatus();
   
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        string servicetype = Request.QueryString["ServiceType"];
        string ApplicationNo = Request.QueryString["ApplicationNo"];
        string ServiceId = Request.QueryString["ServiceId"];
        string proposalId = Request.QueryString["ProposalId"];
        if (!IsPostBack)
        {
            if (servicetype == "1")
            {
                if (ServiceId == "52")
                {

                    string ulbCode = objService.GetULBCode(ApplicationNo);
                    Response.Redirect("https://www.ulbodisha.gov.in/or/ulb/citizen-services?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Fcv%2Frender&ulb=%2Fulb001&ulbCode=" + ulbCode + "&applicationNo=" + ApplicationNo + "");

                }


                #region ForPartnershipFirm
                else if (ServiceId == "49")
                {
                    PartnershipFirm.Service1Client obj = new PartnershipFirm.Service1Client();
                    PartnershipFirm.FirmDtl objfirm = new PartnershipFirm.FirmDtl();
                   
                    objfirm = obj.GetStatus(ApplicationNo, proposalId);
                    string StatusID = objfirm.StatusId.ToString();
                    string StatusName = objfirm.StatusName.ToString();
                    IsDirectoryCreated("PF");
                    string[] msg;
                    if (StatusID == "2")
                    {
                        string appViewDetailUrl = ConfigurationManager.AppSettings["PartnershipFirmDetail"].ToString();

                        string Url = appViewDetailUrl + "?UserId=" + Session["InvestorId"].ToString() + "&ApplicationID=" + ApplicationNo;
                        msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, StatusName, Convert.ToInt32(StatusID), Url, "EXT");
                        Response.Redirect(Url);

                    }
                    else
                    {
                        msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, StatusName, Convert.ToInt32(StatusID), "", "EXT");
                    }


                }
                #endregion
                #region WaterAllotment
                else if (ServiceId == "29")
                {
                    string inputJson = (new JavaScriptSerializer()).Serialize(ApplicationNo);
                    inputJson = inputJson.TrimStart('[').TrimEnd(']');
                    string serviceUrl = "http://erp.idco.in/" + "sendStatusOfWaterApplFromERPtoSWP?wa_ref_no=" + ApplicationNo;
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
                    httpRequest.Accept = "application/json";
                    httpRequest.ContentType = "application/json";
                    httpRequest.Method = "GET";
                    using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                    {
                        using (Stream stream = httpResponse.GetResponseStream())
                        {
                            string strResult = (new StreamReader(stream)).ReadToEnd();
                            string Status = JObject.Parse(strResult)["status_code"].ToString();
                            string RefNo = JObject.Parse(strResult)["refno"].ToString();
                            string StatusMessage = JObject.Parse(strResult)["status_message"].ToString();

                            string[] msg = objServcStatus.UpdateStatus(RefNo, Convert.ToInt32(ServiceId), proposalId, StatusMessage, Convert.ToInt32(Status), "", "EXT");

                        }
                    }

                }
                #endregion
                #region ForProfessionalTax
                else if (ServiceId == "10")
                {
                    WebServiceSoapClient obj = new WebServiceSoapClient();
                    string str = obj.PTRegistrationStatus(ConfigurationManager.AppSettings["PTKEY"].ToString(), ApplicationNo, "");

                    List<CPT> list = JsonConvert.DeserializeObject<List<CPT>>(str);
                    string[] msg = objServcStatus.UpdateStatus(list[0].TokenNo.ToString(), Convert.ToInt32(ServiceId), proposalId, list[0].PT_Status.ToString(), Convert.ToInt32(list[0].Status.ToString()), "", "EXT");

                }
                #endregion
                #region ForLandAllotment
                else if (ServiceId == "28")
                {

                    string[] returnval = GetIntegrationStatusPEALWithIDCO(ApplicationNo);
                    string[] msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, Convert.ToString(returnval[2].ToString()), Convert.ToInt32(returnval[5].ToString()), Convert.ToString(returnval[4].ToString()), "EXT");
                }
                #endregion
                #region ForBuildingPlanApproval
                else if (ServiceId == "20")
                {
                    string RestServiceURL = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                    
                    try
                    {
                        using (var client = new WebClient()) //WebClient  
                        {
                            //Variable given by User
                            string _uniquecode = ApplicationNo; string _SWPCode = Session["InvestorId"].ToString();


                            string _msg = string.Empty;
                            client.Headers.Add("Content-Type:application/json"); //Content-Type  
                            client.Headers.Add("Accept:application/json");
                            var result = client.DownloadString(RestServiceURL + "/FetchApplicationsByUniqueCode?_uniquecode=" + _uniquecode + "&_SWPCode=" + _SWPCode + "");
                            string reslt = result;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();

                            object a = serializer.Deserialize(result, typeof(object));
                            string str = a.ToString();
                            if (str == "No data found.")
                            {


                                string[] msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, "No data found.", 0, "", "EXT");

                            }
                            else
                            {
                                DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
                                if (Convert.ToInt32(DynTable.Rows[0]["applicationstatusid"].ToString()) == 2)
                                {


                                    using (var client1 = new WebClient()) //WebClient  
                                    {
                                        var result1 = client.DownloadString(RestServiceURL + "/FetchCertificate?uniquecode=" + _uniquecode + "&swpusercode=" + _SWPCode + "&_msg=a");

                                        if (result1 != "null")
                                        {

                                            var response = result1
                                            .Substring(1, result1.Length - 2)
                                            .Replace(@"\/", "/");
                                            byte[] d = Convert.FromBase64String(response);


                                            string filename1 = string.Empty;
                                            string prefix = "BPAS";
                                            string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                                            filename1 = string.Format("SWP_" + prefix + "_{0:yyyy_MM_dd_hh_mm_ss_tt_}" + ".pdf", DateTime.Now);
                                            IsDirectoryCreated(prefix);
                                            System.IO.File.WriteAllBytes(apppath + "/File/" + prefix + "/" + filename1, d);
                                            string[] msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, Convert.ToString(DynTable.Rows[0]["applicationstatus"].ToString()), Convert.ToInt32(DynTable.Rows[0]["applicationstatusid"].ToString()), "~/File/" + prefix + "/" + filename1, "EXT");

                                            Response.Redirect("~/File/" + prefix + "/" + filename1);


                                        }
                                        else
                                        {

                                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('File not exists at physical location!!!Please try after Sometime')", true);


                                        }

                                    }

                                }
                                else
                                {
                                    string[] msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, Convert.ToString(DynTable.Rows[0]["applicationstatus"].ToString()), Convert.ToInt32(DynTable.Rows[0]["applicationstatusid"].ToString()), "", "EXT");

                                }

                            }


                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex.InnerException;
                    }


                }
                #endregion
                #region ForTreeTransit
                else if (ServiceId == "25" || ServiceId == "26")
                {
                    // string Url = "117.247.252.221/ttpermit_beta/users/getStatus";
                    // string Url = " https://ttpermitodisha.in/users/getStatus";
                    string Url = ConfigurationManager.AppSettings["TreecheckStatus"].ToString();

                   
                    Response.Redirect( "" + Url + "?service_code=" + ServiceId + "&UserId=" +  Convert.ToString( Session["InvestorId"]) + "&appln_id=" + ApplicationNo + "");




                }
                #endregion
                #region ForHealthAndFamily
                else if (ServiceId == "30" || ServiceId == "31" || ServiceId == "32")
                {
                    // string url = "117.247.252.220:8484/dcodisha/dcstatus.php";
                    // string url = "http://dcodishaonline.nic.in/dcodisha/dcstatus.php";
                    string url = ConfigurationManager.AppSettings["DrugcheckStatus"].ToString();
                    string[] returnval = Health(ServiceId, ApplicationNo, url);
                    string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                    if (returnval[2] == null)
                    {
                        returnval[2] = "";
                    }

                    string[] msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, returnval[0].ToString(), Convert.ToInt32(returnval[1].ToString()), returnval[2].ToString(), "EXT");


                }
                #endregion
                #region ForPollutionControlBoard
                else if (ServiceId == "43" || ServiceId == "44" || ServiceId == "45" || ServiceId == "46" || ServiceId == "50" || ServiceId == "53" || ServiceId == "54")
                {
                    string url = ConfigurationManager.AppSettings["PollutioncheckStatus"].ToString();

                    XmlDocument doc1 = new XmlDocument();
                   
                    doc1.Load("" + url + "?serviceId=" + ServiceId + "&applicationNo=" + ApplicationNo + " ");
                    XmlElement root = doc1.DocumentElement;
                    XmlNodeList nodes = root.SelectNodes("/applicationDetail");
                    string filename1 = "";
                    string applicationID = "";
                    string serviceId = "";
                    string statusMsg = "";
                    string statusId = "";
                    string certifictUrl = "";
                    string[] msg = new string[2];
                    string prefix = string.Empty;
                    foreach (XmlNode node in nodes)
                    {
                        applicationID = node["applicationID"].InnerText;
                        serviceId = node["serviceId"].InnerText;
                        statusMsg = node["statusMsg"].InnerText;
                        statusId = node["status"].InnerText;
                        certifictUrl = node["certificateURL"].InnerText;

                        if (certifictUrl != "")
                        {

                            if (ServiceId == "43")
                            {
                                prefix = "CEW";
                            }
                            else if (ServiceId == "44")
                            {
                                prefix = "CEA";
                            }
                            else if (ServiceId == "45")
                            {
                                prefix = "COW";
                            }
                            else if (ServiceId == "46")
                            {
                                prefix = "COA";
                            }
                            else if (ServiceId == "53")
                            {
                                prefix = "CEB";
                            }
                            else if (ServiceId == "54")
                            {
                                prefix = "COB";
                            }
                            else if (ServiceId == "50")
                            {
                                prefix = "HWM";
                            }
                            WebClient client = new WebClient();
                            byte[] data = client.DownloadData(certifictUrl);
                            string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                            filename1 = string.Format("SWP_" + prefix + "_{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + applicationID + ".pdf", DateTime.Now);
                            IsDirectoryCreated(prefix);

                            System.IO.File.WriteAllBytes(apppath + "/File/" + prefix + "/" + filename1, data);
                        }
                        if (certifictUrl != "")
                        {
                            msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, statusMsg, Convert.ToInt32(statusId), "~/File/" + prefix + "/" + filename1, "EXT");
                            Response.Redirect("~/File/" + prefix + "/" + filename1);

                        }
                        else
                        {
                            msg = objServcStatus.UpdateStatus(ApplicationNo, Convert.ToInt32(ServiceId), proposalId, statusMsg, Convert.ToInt32(statusId), "", "EXT");

                        }


                    }

                }
                #endregion

            }
           
        }
    }


    







    private string[] Health(string ServiceId, string ApplicationNo, string Url)
    {
        string filename1 = string.Empty;
        string[] retunval;
        using (WebClient client = new WebClient())
        {
            var reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("ServiceID", ServiceId);
            reqparm.Add("ApplicationNumber", ApplicationNo);
            reqparm.Add("app_type", "New");
            byte[] responsebytes = client.UploadValues(Url, "POST", reqparm);
            string responsebody = Encoding.UTF8.GetString(responsebytes);

            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, string>>(responsebody);
            NameValueCollection nvc = null;
            if (dict != null)
            {
                nvc = new NameValueCollection(dict.Count);
                foreach (var k in dict)
                {
                    nvc.Add(k.Key, k.Value);
                }
            }

            retunval = new string[] { nvc["Statusmsg"], nvc["Status"], nvc["CertificateURL"] };

        }
        return retunval;
    }

    private string[] GetIntegrationStatusPEALWithIDCO(string oas_cafno)
    {
        string[] retunval;
        NameValueCollection nvc = null;
        string serviceUrl = "http://erp.idco.in/" + "sendStatusOfApplicationFromERPtoSWP?oas_cafno=" + oas_cafno;
        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
        httpRequest.Accept = "application/json";
        httpRequest.ContentType = "application/json";
        httpRequest.Method = "GET";
        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        {
            using (Stream stream = httpResponse.GetResponseStream())
            {
                string strResult = (new StreamReader(stream)).ReadToEnd();
                var jss = new JavaScriptSerializer();
                var dict = jss.Deserialize<Dictionary<string, string>>(strResult);

                if (dict != null)
                {
                    nvc = new NameValueCollection(dict.Count);
                    foreach (var k in dict)
                    {
                        nvc.Add(k.Key, k.Value);
                    }
                }
                retunval = new string[] { nvc["unique_application_id_from_swp"], nvc["cafno"], nvc["status_message"], nvc["industry_code"], nvc["allotment_order_url"], nvc["status_code"] };
            }
        }
        return retunval;
    }

    public void IsDirectoryCreated(string prefix)
    {
        string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        if (!Directory.Exists(apppath + "/File/" + prefix))
        {
            Directory.CreateDirectory(apppath + "/File/" + prefix);
        }
    }
}