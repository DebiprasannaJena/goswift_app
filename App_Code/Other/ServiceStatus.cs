using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BusinessLogicLayer.Service;
using System.Net;
using System.IO;
using TradeLicenceServiceReference;
using EntityLayer.Service;
using System.Configuration;
using System.Xml;
using System.Globalization;
/// <summary>
/// Summary description for ServiceStatus
/// </summary>
public class ServiceStatus
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    ServiceBusinessLayer objService = new ServiceBusinessLayer();

    static string Str_UsrName = "";
    public ServiceStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string CheckStatus(string ApplicationNo, int ExistingType)
    {
        string status = "";

        SqlCommand cmd = new SqlCommand();
        SqlDataReader sqlReader = null;

        try
        {
            if (ExistingType == 0)
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_GetApplicationDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_MODE", "GS");
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", ApplicationNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        status = Convert.ToString(sqlReader["vchStatusName"]);
                    }
                    sqlReader.Close();
                }
                conn.Close();
            }

        }
        catch (NullReferenceException ex)
        { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
        return status;

    }


    public string GetUrl(string ApplicationNo, int ExistingType, string ServiceId, string ProposalId, string USrID, string strAppliedDate)
    {
        string appViewDetailUrl = "";
        string Url = "";
        DataTable dtNew = new DataTable();

        if (ExistingType == 1)
        {
            if (ServiceId == "52")
            {
                appViewDetailUrl = ConfigurationManager.AppSettings["TradeLicenseDetail"].ToString();
                string ulbCode = objService.GetULBCode(ApplicationNo);
                Url = "" + appViewDetailUrl + "?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ulbCode + "&applicationNo=" + ApplicationNo + "";

                // Url = "http://117.240.239.40/or/ulb/citizen-services?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ulbCode + "&applicationNo=" + ApplicationNo + "";
                // Url = "https://www.ulbodisha.gov.in/or/ulb/citizen-services?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ulbCode + "&applicationNo=" + ApplicationNo + "";
            }
            else if (ServiceId == "30" || ServiceId == "31" || ServiceId == "32")
            {
                appViewDetailUrl = ConfigurationManager.AppSettings["DrugLicenseDetail"].ToString();
                Url = "" + appViewDetailUrl + "?ServiceID=" + ServiceId.ToString() + "&UserId=" + USrID.ToString() + "&ProposalNo=" + ProposalId + "&page=view_Details";

                // Url = "http://117.247.252.220:8484/dcodisha/dcservice.php?ServiceID=" + ServiceId.ToString() + "&UserId=" + USrID.ToString() + "&ProposalNo=" + ProposalId + "&page=query";
                // Url = "http://dcodishaonline.nic.in/dcodisha/dcservice.php?ServiceID=" + ServiceId.ToString() + "&UserId=" + USrID.ToString() + "&ProposalNo=" + ProposalId + "&page=query";
            }
            else if (ServiceId == "28")
            {
                appViewDetailUrl = ConfigurationManager.AppSettings["CNETLicenseQuery"].ToString();
                Url = "" + appViewDetailUrl + "?proposal_no=" + ProposalId + "";
            }
            else if (ServiceId == "25" || ServiceId == "26")
            {
                appViewDetailUrl = ConfigurationManager.AppSettings["TreeTransitDetail"].ToString();
                // Url = "" + appViewDetailUrl + "?ServiceID=" + ServiceId + "&UserId=" + USrID + "&ProposalNo=201709002&page=view_details";

                Url = "" + appViewDetailUrl + "?service_code=" + ServiceId + "&UserId=" + USrID + "&appln_id="+ApplicationNo+"";


               
            }
            else if (ServiceId == "10" || ServiceId == "20")
            {
                Url = "ServiceApplicationDetails.aspx?ApplicationNo=" + ApplicationNo + "&ServiceId=" + ServiceId + "";
            }
            else if (ServiceId == "49")
            {
                appViewDetailUrl = ConfigurationManager.AppSettings["PartnershipFirmDetail"].ToString();
                Url = "" + appViewDetailUrl + "?UserId=" + USrID + "&ApplicationID=" + ApplicationNo + "";

                // Url = "http://igrodisha.gov.in/swp_firm/ADMIN/Firm/OPER/firmStatus.aspx?UserId=" + USrID + "&ApplicationID=" + ApplicationNo + "";
                //Url = "http://igrodisha.gov.in/ADMIN/Firm/OPER/firmStatus.aspx?UserId=" + USrID + "&ApplicationID=" + ApplicationNo + "";
            }
            else if (ServiceId == "43" || ServiceId == "44" || ServiceId == "45" || ServiceId == "46" || ServiceId == "50" || ServiceId == "53" || ServiceId == "54" || ServiceId == "65" || ServiceId == "66")
            {
                appViewDetailUrl = ConfigurationManager.AppSettings["PollutionControlDetail"].ToString();
                Url = "" + appViewDetailUrl + "?applicationNo=" + ApplicationNo + "&serviceId=" + ServiceId + " ";

                //Url = "http://164.100.163.18/OSPCB/industryRegMaster/industryHome?applicationNo=" + ApplicationNo + "&serviceId=" + ServiceId + " ";
                //Url = "http://odocmms.nic.in/OCMMS/industryRegMaster/industryHome?applicationNo=" + ApplicationNo + "&serviceId=" + ServiceId + " ";
            }
            else if (ServiceId == "5" || ServiceId == "6" || ServiceId == "7" || ServiceId == "34" || ServiceId == "35" || ServiceId == "36" || ServiceId == "39" || ServiceId == "40" || ServiceId == "70" || ServiceId == "71" || ServiceId == "72" || ServiceId == "37") //F&B and Labour Service add by anil service 37
            {
                CultureInfo culture = new CultureInfo("es-ES");
                if (DateTime.Parse(strAppliedDate, culture).Date > DateTime.Parse("15/01/2021", culture).Date)
                {
                    dtNew = PAReSHRAM(ApplicationNo);
                    appViewDetailUrl = ConfigurationManager.AppSettings["PARESHRAMVIEWSTATUSURL"].ToString();
                    Url = "" + appViewDetailUrl + "?appln_id=" + ApplicationNo + "&pan=" + dtNew.Rows[0]["VCH_PAN"].ToString() + "&mobile_number=" + dtNew.Rows[0]["VCH_OFF_MOBILE"].ToString() + "";
                }
                else
                {
                    Url = "ParticularApplicationDetails.aspx?ApplicationNo=" + ApplicationNo + "&ServiceId=" + ServiceId + "";
                }
            }
        }
        else if (ExistingType == 0)
        {
            Url = "ParticularApplicationDetails.aspx?ApplicationNo=" + ApplicationNo + "&ServiceId=" + ServiceId + "";
        }
        return Url;
    }

    public string QueryUrl(string ApplicationNo, int ExistingType, string ServiceId, string USrID, string strAppliedDate)
    {
        string appQueryUrl = "";
        string Url = "";
        DataTable dtNew = new DataTable();
        if (ExistingType == 1)
        {
            if (ServiceId == "52")
            {
                appQueryUrl = ConfigurationManager.AppSettings["TradeLicenseQuery"].ToString();
                string ulbCode = objService.GetULBCode(ApplicationNo);
                Url = "" + appQueryUrl + "?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ulbCode + "&applicationNo=" + ApplicationNo + "&typeOfTrade=SWP";


                //Url = "http://117.240.239.40/or/ulb/citizen-services?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ulbCode + "&applicationNo=" + ApplicationNo + "&typeOfTrade=SWP";
                //Url = "https://www.ulbodisha.gov.in/or/ulb/citizen-services?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ulbCode + "&applicationNo=" + ApplicationNo + "&typeOfTrade=SWP";
            }
            else if (ServiceId == "30" || ServiceId == "31" || ServiceId == "32")
            {
                appQueryUrl = ConfigurationManager.AppSettings["DrugLicenseQuery"].ToString();
                Url = "" + appQueryUrl + "?ServiceID=" + ServiceId + "&UserId=" + USrID + "&ProposalNo=201709002&page=query";

                // Url = "http://117.247.252.220:8484/dcodisha/dcservice.php?ServiceID="+ServiceId+"&UserId="+USrID+"&ProposalNo=201709002&page=query";
                //Url = "http://dcodishaonline.nic.in/dcodisha/dcservice.php?ServiceID=" + ServiceId + "&UserId=" + USrID + "&ProposalNo=201709002&page=query";
            }
            else if (ServiceId == "25" || ServiceId == "26")
            {
                appQueryUrl = ConfigurationManager.AppSettings["TreeTransitQuery"].ToString();
                Url = "" + appQueryUrl + "?ServiceID=" + ServiceId + "&UserId=" + USrID + "&ProposalNo=201709002&page=query";

                // Url = "http://117.247.252.221/ttpermit_beta/users/swpLogin?ServiceID="+ServiceId+"&UserId="+USrID+"&ProposalNo=201709002&page=query";
                //Url = "https://ttpermitodisha.in/users/swpLogin?ServiceID=" + ServiceId + "&UserId=" + USrID + "&ProposalNo=201709002&page=query";
            }
            else if (ServiceId == "10")
            {
                Url = "ServiceApplicationDetails.aspx?ApplicationNo=" + ApplicationNo + "&ServiceId=" + ServiceId + "";
            }
            else if (ServiceId == "49")
            {
                appQueryUrl = ConfigurationManager.AppSettings["PartnershipFirmQuery"].ToString();
                Url = "" + appQueryUrl + "?UserId=" + USrID + "&ApplicationID=" + ApplicationNo + "";

                //Url = "http://igrodisha.gov.in/ADMIN/Firm/OPER/firmStatus.aspx?UserId=" + USrID + "&ApplicationID=" + ApplicationNo + "";
            }
            else if (ServiceId == "43" || ServiceId == "44" || ServiceId == "45" || ServiceId == "46" || ServiceId == "50" || ServiceId == "53" || ServiceId == "54" || ServiceId == "65" || ServiceId == "66")
            {
                appQueryUrl = ConfigurationManager.AppSettings["PollutionControlQuery"].ToString();
                Url = appQueryUrl + ApplicationNo + "&serviceId=" + ServiceId;

                // Url = "http://164.100.163.18/OSPCB/industryRegMaster/industryHome?applicationNo=" + ApplicationNo + "&serviceId=" + ServiceId + "  ";
                //Url = "http://odocmms.nic.in/OCMMS/industryRegMaster/industryHome?applicationNo=" + ApplicationNo + "&serviceId=" + ServiceId + "  ";
            }
            else if (ServiceId == "5" || ServiceId == "6" || ServiceId == "7" || ServiceId == "34" || ServiceId == "35" || ServiceId == "36" || ServiceId == "39" || ServiceId == "40" || ServiceId == "70" || ServiceId == "71" || ServiceId == "72" || ServiceId == "37") //F&B and Labour Service
            {
                CultureInfo culture = new CultureInfo("es-ES");
                if (DateTime.Parse(strAppliedDate, culture).Date > DateTime.Parse("15/01/2021", culture).Date)
                {
                    dtNew = PAReSHRAM(ApplicationNo);
                    appQueryUrl = ConfigurationManager.AppSettings["PARESHRAMQUERYSTATUSURL"].ToString();
                    Url = "" + appQueryUrl + "?appln_id=" + ApplicationNo + "&pan=" + dtNew.Rows[0]["VCH_PAN"].ToString() + "&mobile_number=" + dtNew.Rows[0]["VCH_OFF_MOBILE"].ToString() + "";
                }
                else
                {
                    Url = "ApplicationStatusDetails.aspx?ApplicationNo=" + ApplicationNo + "&ServiceId=" + ServiceId + "";
                }
            }
        }
        else if (ExistingType == 0)
        {
            Url = "ApplicationStatusDetails.aspx?ApplicationNo=" + ApplicationNo + "&ServiceId=" + ServiceId + "";
        }
        return Url;
    }

    public string QueryUrlCNET(string ProposalId, int ExistingType, string ServiceId)
    {
        string appQueryUrl = "";
        string Url = "";
        if (ExistingType == 1)
        {
            if (ServiceId == "28")
            {
                string appQueryUrl1 = ConfigurationManager.AppSettings["CNETLicenseQuery"].ToString();
                Url = "" + appQueryUrl1 + "?proposal_no=" + ProposalId + "";
            }
        }
        return Url;
    }

    public string[] UpdateStatus(string ApplicationNo, int ServiceId, string proposalno, string strApplicationStatus, int intStatus, string certificateUrl, string Action)
    {
        string status = "";
        ServiceDetails objServiceDetails = new ServiceDetails();
        objServiceDetails.str_ApplicationNo = ApplicationNo;
        objServiceDetails.intServiceId = ServiceId;
        objServiceDetails.strProposalId = proposalno;
        objServiceDetails.intStatus = intStatus;
        objServiceDetails.strStatus = strApplicationStatus;
        objServiceDetails.strCertificateFilename = certificateUrl;
        objServiceDetails.strAction = Action;
        string[] retrnVal = objService.UpdateExistingServiceStatus(objServiceDetails);
        if (retrnVal[0] == "1")
        {
            retrnVal[0] = strApplicationStatus;
        }

        return retrnVal;
    }

    public string UpdateStatusNew(string ApplicationNo)
    {

        ServiceDetails objServiceDetails = new ServiceDetails();
        objServiceDetails.str_ApplicationNo = ApplicationNo;
        objServiceDetails.strAction = "NEW";
        string retrnVal = objService.UpdateApplicationStatus(objServiceDetails);
        return retrnVal;
    }

    public string CheckDemandStatus(string ApplicationNo)
    {
        string status = "";
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sqlReader = null;
        try
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GetApplicationDetails";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_VCH_MODE", "GN");
            cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", ApplicationNo);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    status = Convert.ToString(sqlReader["vchDemandStatus"]);
                }
                sqlReader.Close();
            }
            conn.Close();
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
        return status;

    }

    public string GetPaymntURlForWater(string appNo)
    {
        string status = "";
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sqlReader = null;
        try
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_Water_Allotment";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "D");
            cmd.Parameters.AddWithValue("@P_VCH_REFNO", appNo);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    status = Convert.ToString(sqlReader["PaymentUrl"]);
                }
                sqlReader.Close();
            }
            conn.Close();
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
        return status;
    }

    #region ForPollutionControlBoard

    public void AutoPCBStatusUpdate()
    {
        string strAppKey = "";
        string ServiceId = "";
        string proposalId = "";

        CommonHelperCls comm = new CommonHelperCls();
        try
        {
            ServiceDetails objServiceDetails = new ServiceDetails();
            objServiceDetails.strAction = "P";
            List<ServiceDetails> pcbList = objService.PCBAutoUpdate(objServiceDetails);

            if (pcbList.Count > 0)
            {
                for (int i = 0; i < pcbList.Count; i++)
                {
                    ServiceId = "";
                    strAppKey = "";
                    proposalId = "";

                    ServiceId = Convert.ToString(pcbList[i].intServiceId);
                    strAppKey = pcbList[i].strApplicationUnqKey;
                    proposalId = pcbList[i].strProposalId;

                    if (ServiceId == "43" || ServiceId == "44" || ServiceId == "45" || ServiceId == "46" || ServiceId == "50" || ServiceId == "53" || ServiceId == "54" || ServiceId == "65" || ServiceId == "66" || ServiceId == "76" || ServiceId == "77" || ServiceId == "78" || ServiceId == "79")
                    {
                        string url = ConfigurationManager.AppSettings["PollutioncheckStatus"].ToString();

                        /*-------------------------------------------------------------------------*/
                        ////// Write Request Data
                        Util.LogRequestResponse("OspcbScheduler", "AutoPCBStatusUpdate", url + "?serviceId=" + ServiceId + "&applicationNo=" + strAppKey + "");
                        /*-------------------------------------------------------------------------*/

                        XmlDocument doc1 = new XmlDocument();
                        //doc1.Load(" http://164.100.163.18/ODWS/checkStatus?serviceId=" + ServiceId + "&applicationNo=" + lblApplctionNo.Text + " ");
                        //doc1.Load(" http://odocmms.nic.in/ODWS/checkStatus?serviceId=" + ServiceId + "&applicationNo=" + lblApplctionNo.Text + " ");
                        doc1.Load("" + url + "?serviceId=" + ServiceId + "&applicationNo=" + strAppKey + " ");
                        XmlElement root = doc1.DocumentElement;
                        XmlNodeList nodes = root.SelectNodes("/applicationDetail");

                        /*-------------------------------------------------------------------------*/
                        ////// Write Response Data
                        Util.LogRequestResponse("OspcbScheduler", "AutoPCBStatusUpdate", doc1.InnerXml.ToString());
                        /*-------------------------------------------------------------------------*/

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
                                else if (ServiceId == "65")
                                {
                                    prefix = "EWM";
                                }
                                else if (ServiceId == "66")
                                {
                                    prefix = "PWM";
                                }
                                else if (ServiceId == "76")
                                {
                                    prefix = "BMW";
                                }
                                else if (ServiceId == "77")
                                {
                                    prefix = "MSW";
                                }

                                WebClient client = new WebClient();
                                byte[] data = client.DownloadData(certifictUrl);
                                string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                                filename1 = string.Format("SWP_" + prefix + "_{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + applicationID + ".pdf", DateTime.Now);
                                IsDirectoryCreated(prefix);

                                System.IO.File.WriteAllBytes(apppath + "/File/" + prefix + "/" + filename1, data);
                            }
                            /*-------------------------------------------------------------------------*/
                            ////// Check status value coming from OSPCB
                            /*-------------------------------------------------------------------------*/
                            if (statusId != "" && statusId != "0")
                            {
                                if (certifictUrl != "")
                                {
                                    msg = UpdateStatus(strAppKey, Convert.ToInt32(ServiceId), proposalId, statusMsg, Convert.ToInt32(statusId), "~/File/" + prefix + "/" + filename1, "EXT");
                                }
                                else
                                {
                                    msg = UpdateStatus(strAppKey, Convert.ToInt32(ServiceId), proposalId, statusMsg, Convert.ToInt32(statusId), "", "EXT");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string[] toEmail = new string[3];
            toEmail[0] = ConfigurationManager.AppSettings["InternalMail1"].ToString();
            toEmail[1] = ConfigurationManager.AppSettings["InternalMail2"].ToString();
            toEmail[2] = ConfigurationManager.AppSettings["InternalMail3"].ToString();
            comm.sendMail("Service Down || OSPCB", "Application Number :- " + strAppKey + "<br>Service Id:- " + ServiceId + "<br><br>Error Msg:-" + ex.Message, toEmail, true);//Repoting authority
            throw;
        }
    }

    #endregion

    public void IsDirectoryCreated(string prefix)
    {
        string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        if (!Directory.Exists(apppath + "/File/" + prefix))
        {
            Directory.CreateDirectory(apppath + "/File/" + prefix);
        }
    }

    public DataTable CheckPaymentType(string ApplicationNo)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GetApplicationDetails";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_VCH_MODE", "BPASPTYPE");
            cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", ApplicationNo);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
        return dt;
    }

    private DataTable PAReSHRAM(string strApplicationKey)
    {
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
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", 0);
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PAReSHRAMSTATUS");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PAReSHRAMRET");
        }
        return dt;
    }
}
