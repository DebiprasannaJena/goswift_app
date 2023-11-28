#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   InvestorProfile.aspx.cs
// Description           :   View Investor dashboard
// Created by            :   Nibedita Behera
// Created On            :   24 July 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.Investor;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;
#endregion

public partial class InvesterProfile : SessionCheck
{
    InvestorBusinessLayer objService = new InvestorBusinessLayer();
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objservice = new ProposalBAL();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();

    string District, Month, Year = "0";

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["IndustryType"] == null)
        {
            Response.Redirect("~/LogOut.aspx");            
        }

        if (!IsPostBack)
        {
            fillInvestorChildUnit();
            FillFinYr(ddlCSRYear);
            
            DashboradCommon.YEARBIND(ddlYearCICG);
            BindDistrict();

            YEARBIND(ddlAppaYear);
            MONTHBIND(ddlAppaMonth);
            ViewApaaStatus();
            ViewServiceStatus("C");
            ViewPEALStatus();

            /*-------------------------------------------------------*/
            ///// CSR Portlet
           

            viewCSRPortletData();
            /*-------------------------------------------------------*/

            
            PEALGridSatus("C");
            PEALSTATUS("C");
            ViewIncentiveStatus("C");
            InsertAppaStatus(District, Month, Year);
            InsertCICGStatus(ddlYearCICG);
            ViewCICGData();

            /*-------------------------------------------------------*/
            ///// SPMG Section
            YEARBIND(ddlspmgyear);
            ddlspmgyear.SelectedValue = DateTime.Now.Year.ToString();// "2017";
            InsertSPMGStatus();
            ViewSPMGData();
            /*-------------------------------------------------------*/
            fillInvestorGrievance();
            fillInvestorGrievanceMasterTracker();
        }
    }

    #endregion

    #region MEMBER FUNCTION
    #region APAAStatus using table
    private void ViewApaaStatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "AP";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            List<SWPDashboard> objPEALStatusList = objserviceDashboard.GetDashboardAPAAtatus(objSWP).ToList();
            if (objPEALStatusList.Count > 0)
            {
                hdchngrqstApplied.InnerHtml = objPEALStatusList[0].strChngReqApplied.ToString();
                hdchngreqdispose.InnerHtml = objPEALStatusList[0].strChngReqDispose.ToString();
                hdhngreqPendAtIDCO.InnerHtml = objPEALStatusList[0].strChngReqPendingAtIDCO.ToString();
                hdchngReqCrossThirty.InnerHtml = objPEALStatusList[0].strChngReqPendAtUnit.ToString();
                divApp.InnerHtml = "<span  class='counter'>" + objPEALStatusList[0].strChngReqCrossthirty.ToString() + "</span>";
            }
            else
            {
                hdchngrqstApplied.InnerHtml = "0";
                hdchngreqdispose.InnerHtml = "0";
                hdhngreqPendAtIDCO.InnerHtml = "0";
                hdchngReqCrossThirty.InnerHtml = "0";
                divApp.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    #endregion
    private void ViewPEALStatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "PL";
            objSWP.intUserid = Convert.ToInt32(Session["InvestorId"]);
           
            List<SWPDashboard> objPEALStatusList = objserviceDashboard.GetDashboardPEALStatusDtls(objSWP).ToList();
            if (objPEALStatusList.Count > 0)
            {
                spIndays.InnerHtml = objPEALStatusList[0].PEALIndays.ToString();
                spStatus.InnerHtml = objPEALStatusList[0].StrStatus.ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    private void FillFinYr(DropDownList ddlFinYear)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();

        objSWP.strAction = "FY";
        List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();
        ddlFinYear.DataSource = objCICGFINYear;
        ddlFinYear.DataTextField = "Year";
        ddlFinYear.DataValueField = "Year";
        ddlFinYear.DataBind();

        if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 3)
        {
            ddlFinYear.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString()) - 1) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")));
        }
        else
        {
            ddlFinYear.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString())) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")) + 1);
        }
    }

    #region APAA portlet

    private void InsertAppaStatus(string District, string Month, string Year)
    {
        string finalquery = string.Empty;
        SqlCommand cmd = null;
    
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        District = "0";
        Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        Year = string.IsNullOrEmpty(ddlAppaYear.SelectedValue) ? default(string) : ddlAppaYear.SelectedValue.ToString();
        string Deptid = "0";
        string UniqueKey = Session["UID"].ToString();
        
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["InvestorId"]);

        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + District + "/" + Deptid + "/" + 0 + "/" + Year + "/" + Month;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                    string output = DynTable.Rows[0]["getSWPConsolidationDataResult"].ToString();
                    string[] finalOut = output.Split(':');
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT investorid FROM T_APAA_Service WHERE CONVERT(DATE,dtmCreatedon)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_APAA_Service SET investorid=" + Convert.ToInt32(Session["InvestorId"]) + ",TotalApplied=" + finalOut[2].ToString() + ",TotalDisposed=" + DynTable.Rows[0]["TotalDisposed"].ToString() + "," +
                             "TotalMajorPendingIdco=" + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + ",TotalPendingIdco=" + DynTable.Rows[0]["TotalPendingIdco"].ToString() + ",TotalPendingUnit=" + DynTable.Rows[0]["TotalPendingUnit"].ToString() + ",dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',intYearId=" + Year + ",intDistrictId=" + District + ",intMonthId=" + Month + " WHERE investorid='" + ds.Tables[0].Rows[0]["investorid"].ToString() + "'";
                    }
                    else
                    {
                        finalquery = "INSERT INTO T_APAA_Service(investorid,TotalApplied,TotalDisposed,TotalMajorPendingIdco,TotalPendingIdco,TotalPendingUnit,dtmCreatedon,intYearId,intDistrictId,intMonthId)" +
                    "VALUES(" + Convert.ToInt32(Session["InvestorId"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["TotalDisposed"].ToString() + "," + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingUnit"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "'," + Year + "," + District + "," + Month + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }

   

    protected void btnAPAASubmit_Click(object sender, EventArgs e)
    {
        InsertAppaStatus(District, Month, Year);
        ViewApaaStatus();
    }

    #endregion

    #region Added by nibedita behera on 22-09-2017

    private void PEALGridSatus(string strFilterMode)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "PC";
            objSWP.strFilterMode = strFilterMode;

            if (strFilterMode == "C")
            {
                objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            }
            else if (strFilterMode == "I")
            {
                objSWP.intInvestorId = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }

            List<SWPDashboard> objPEALStatus = objserviceDashboard.GetDashboardPEALDtls(objSWP).ToList();
            if (objPEALStatus.Count > 0)
            {
                grdPEALStatus.DataSource = objPEALStatus;
                grdPEALStatus.DataBind();
            }
            else
            {
                grdPEALStatus.DataSource = null;
                grdPEALStatus.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    private void PEALSTATUS(string strFilterMode)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                string sss = "C";
            }
            else
            {
                string sscdfdss = "I";
            }


            objSWP.strAction = "PS";
            objSWP.strFilterMode = strFilterMode;

            if (strFilterMode == "C")
            {
                objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            }
            else if (strFilterMode == "I")
            {
                objSWP.intInvestorId = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }

            List<SWPDashboard> objCSRCATStatus = objserviceDashboard.GetDashboardPEALFORMDtls(objSWP).ToList();
            if (objCSRCATStatus.Count > 0)
            {
                SPPENDING.InnerHtml = objCSRCATStatus[0].strPending.ToString();
                SPREJECT.InnerHtml = objCSRCATStatus[0].strRejected.ToString();
            }
            else
            {
                SPPENDING.InnerHtml = "0";
                SPREJECT.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    private void BindDistrict()
    {
      
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        List<ProjectInfo> objProjList = objservice.PopulateProjDropdowns(objProp).ToList();
        ddlCSRDistrict.DataSource = objProjList;
        ddlCSRDistrict.DataTextField = "vchDistName";
        ddlCSRDistrict.DataValueField = "intDistId";
        ddlCSRDistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlCSRDistrict.Items.Insert(0, list);
    }
    #endregion
    private void ViewServiceStatus(string strFilterMode)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "S";
            objSWP.strFilterMode = strFilterMode;

            if (strFilterMode == "C")
            {
                objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            }
            else if (strFilterMode == "I")
            {
                objSWP.intInvestorId = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }

            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                hdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
                hdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
                hdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
                hdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();
                SPRejectedMaster.InnerHtml = objServiceStatus[0].strRejected.ToString();
                spPendingMaster.InnerHtml = objServiceStatus[0].strPending.ToString();
            }
            else
            {

                hdApplied.InnerHtml = "--";
                hdApprove.InnerHtml = "--";
                hdPending.InnerHtml = "--";
                hdReject.InnerHtml = "--";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    private void YEARBIND(DropDownList ddlYear)
    {
        ddlYear.Items.Clear();
      

        for (int i = DateTime.Now.Year; i >= 2007; i--)
        {
            ListItem lt = new ListItem();
            lt.Text = i.ToString();
            lt.Value = i.ToString();
            ddlYear.Items.Add(lt);
        }
        ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void MONTHBIND(DropDownList ddlMonth)
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
        }
    }

    #endregion

    
    #region Dashboard SPMG Data

    private void InsertSPMGStatus()
    {
        string finalquery = "";
        SqlCommand cmd;

        //Random number generate
        string strrandomgen = MakeRandom(10);
        var plainran = Encoding.UTF8.GetBytes(strrandomgen);
        string randno = Convert.ToBase64String(plainran);

        //Timestamp
        TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        double unixTime = span.TotalSeconds;
        var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());
        string plunixtime = Convert.ToBase64String(plainut);

        //PasswordDigest
        string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();
        SHA256 mySHA256 = SHA256Managed.Create();
        string finalstr = GetSha256FromString(ranpss);
        byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
        string ranpss1 = Convert.ToBase64String(bytes);

        //Financial year
        string FinYear = ddlspmgyear.SelectedValue;

        //// SPMG Service Consume
        string serviceUrl = "https://esuvidha.gov.in//odisha/restservices/RestServer.php?view=issuestatusbyinvestorid";
        object input = new
        {
            RandomNonce = randno,
            TimeStamp = plunixtime,
            PasswordDigest = ranpss1,
            FinancialYear = FinYear,
            InvestorID = Convert.ToString(Session["UID"])
        };

        //"2285753E-55FC-4D1D-873F-1599C1B127EC"

        string inputJson = (new JavaScriptSerializer()).Serialize(input);
        var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
        webRequest.Method = WebRequestMethods.Http.Post;
        webRequest.ContentType = "application/json";
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
        using (var requestStream = webRequest.GetRequestStream())
        {
            using (var writer = new StreamWriter(requestStream))
            {
                writer.Write(json);
            }
        }
        try
        {
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        try
                        {
                            var responseData = reader.ReadToEnd();
                            webResponse.Close();

                            string strResult = responseData.ToString();
                            if (strResult != "")
                            {
                                DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                }

                                string query = "SELECT INT_YEAR FROM T_INVESTOR_SPMG_DTLS WHERE INT_YEAR='" + FinYear + "' AND Investor_id='" + Convert.ToString(Session["UID"]) + "'";
                                SqlDataAdapter adp = new SqlDataAdapter(query, con);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    finalquery = "UPDATE T_INVESTOR_SPMG_DTLS SET ISSUES_RECEIVED=" + DynTable.Rows[0]["Issues Received"].ToString() + ",ISSUES_RESOLVED=" + DynTable.Rows[0]["Issues Resolved"].ToString() + "," +
                                         "ISSUES_PENDING=" + DynTable.Rows[0]["Issues Pending"].ToString() + ",ISSUES_EXCEED=" + DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString() + ",DTM_CREATED_ON='" + DateTime.Now.ToString("dd-MMM-yy")
                                        + "'" + " WHERE INT_YEAR=" + "'" + FinYear + "' AND Investor_id='" + Convert.ToString(Session["UID"]) + "' ";
                                }
                                else
                                {
                                    finalquery = "INSERT INTO T_INVESTOR_SPMG_DTLS(Investor_Id,ISSUES_RECEIVED,ISSUES_RESOLVED,ISSUES_PENDING,ISSUES_EXCEED,DTM_CREATED_ON,INT_YEAR)" +
                                                 "VALUES(" + "'" + Convert.ToString(Session["UID"]) + "'," + DynTable.Rows[0]["Issues Received"].ToString() + "," + DynTable.Rows[0]["Issues Resolved"].ToString()
                                                 + "," + DynTable.Rows[0]["Issues Pending"].ToString() + "," + DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString() + "," +
                                                 "'" + DateTime.Now.ToString("dd-MMM-yy") + "'," + FinYear + ")";

                                }
                                cmd = new SqlCommand(finalquery, con);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            Util.LogError(ex, "InvestorDashboard");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }
    protected void btnspmg_Click(object sender, EventArgs e)
    {
        InsertSPMGStatus();
        ViewSPMGData();
    }
    private void ViewSPMGData()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.UniqueKey = Session["UID"].ToString();
            objSWP.strAction = "SPMGI";
            objSWP.intYearId = Convert.ToInt32(ddlspmgyear.SelectedValue);
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                spSpmgpnd.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
                spmgraised.InnerHtml = objServiceStatus[0].intSPMGRaised.ToString();
                spmgresolved.InnerHtml = objServiceStatus[0].intSPMGResolved.ToString();
                spmgpending.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
                spmgpendingexceed.InnerHtml = objServiceStatus[0].intSPMGIssuePending.ToString();
            }
            else
            {
                spSpmgpnd.InnerHtml = "0";
                spmgraised.InnerHtml = "0";
                spmgresolved.InnerHtml = "0";
                spmgpending.InnerHtml = "0";
                spmgpendingexceed.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    #endregion

    #region CSR Portlet

    protected void btnCSRStatus_Click(object sender, EventArgs e)
    {
       
        viewCSRPortletData();
    }
    /// <summary>
    /// Added by Sushant Jena On Dt:-18-Apr-2019
    /// This method is used to display CSR activities of a Investors.
    /// Here CSR values are showing by fetching data directly from CSR service.Database interaction are not used here.
    /// This method replicates above two methods (InsertCSRStatus and CSRPortletSatus)
    /// </summary>
    private void viewCSRPortletData()
    {
        string finalquery = string.Empty;
        string Type = "2";
        string District = string.IsNullOrEmpty(ddlCSRDistrict.SelectedValue) ? default(string) : ddlCSRDistrict.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlCSRYear.SelectedItem.Text) ? default(string) : ddlCSRYear.SelectedItem.Text;
        if (Year == "--Select--")
        {
            Year = "0";
        }
        else
        {
            Year = ddlCSRYear.SelectedItem.Text;
        }
        string UniqueKey = Session["UID"].ToString(); ///// Investor's Unique Id

        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + UniqueKey + "/" + Type + "/" + District + "/" + Year;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                    string output = DynTable.Rows[0]["GetTotalAmountResult"].ToString();
                    string[] finalOut = output.Split(':');

                    if (DynTable.Rows.Count > 0)
                    {
                        
                        hdNoPrj.InnerHtml = DynTable.Rows[0]["RecCSRProjectsUnderTakenByCorp"].ToString();
                        hdSpent.InnerHtml = finalOut[2].ToString(); //CSRTotalAmount
                        try
                        {
                            decimal TotAmount = Convert.ToDecimal(finalOut[2].ToString()) / 100;
                            spSpent.InnerHtml = Convert.ToString(Math.Round(TotAmount));
                            hdSpent.InnerHtml = Convert.ToString(Math.Round(TotAmount)) + " Cr.";
                        }
                        catch (Exception ex)
                        {
                            Util.LogError(ex, "InvestorDashboard");
                        }
                    }
                    else
                    {
                        hdNoPrj.InnerHtml = "0";
                        hdSpent.InnerHtml = "0";
                        spSpent.InnerHtml = "0";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }

    #endregion

    #region CICG Portlet

    private void InsertCICGStatus(DropDownList ddlYearCICG)
    {
        string finalquery = string.Empty;
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
       
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string Year = string.IsNullOrEmpty(ddlYearCICG.SelectedValue) ? default(string) : ddlYearCICG.SelectedValue.ToString();
        string strMonth = "0";
        string UniqueKey = Session["UID"].ToString();
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CICGServiceURL"] + "GetIndustryInspection/" + UniqueKey + "/" + strMonth + "/" + Year;
           
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                    string output = DynTable.Rows[0]["GetIndustryInspectionResult"].ToString();
                    string[] finalOut = output.Split(':');
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT INT_ID FROM T_CICG_DASHBOARD_INVESTOR WHERE CONVERT(DATE,DTM_CREATED_ON)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_CICG_DASHBOARD_INVESTOR SET INT_ID=" + Convert.ToInt32(Session["InvestorId"]) + ",INT_INS_COMPLETED=" + finalOut[2].ToString() + ",INT_INS_SCHEDULED=" + DynTable.Rows[0]["InspectionScheduled"].ToString() +
                            ",INT_ReportNot_Uploaded=" + DynTable.Rows[0]["ReportNotUploaded"].ToString() + ",INT_UNATTENDED_INS=" + DynTable.Rows[0]["UnAttendedInspection"].ToString() + ",DTM_CREATED_ON='" + DateTime.Now.ToString("dd-MMM-yy")
                             + "',vchYear='" + Year + "'" + " WHERE INT_ID='" + ds.Tables[0].Rows[0]["INT_ID"].ToString() + "'";
                    }
                    else
                    {
                        finalquery = "INSERT INTO T_CICG_DASHBOARD_INVESTOR(INT_ID,INT_INS_COMPLETED,INT_INS_SCHEDULED,INT_ReportNot_Uploaded,INT_UNATTENDED_INS,DTM_CREATED_ON,vchYear)" +
                        "VALUES(" + Convert.ToInt32(Session["InvestorId"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["InspectionScheduled"].ToString() + ","
                        + DynTable.Rows[0]["ReportNotUploaded"].ToString() + "," + DynTable.Rows[0]["UnAttendedInspection"].ToString() + "," +
                        "'" + DateTime.Now.ToString("dd-MMM-yy") + "','" + Year + "')";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }
    private void ViewCICGData()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "VCINV";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objSWP.Year = ddlYearCICG.SelectedValue;
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetCICGDashboardService(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                h3cicgapplied.InnerHtml = objServiceStatus[0].INT_INS_SCHEDULED.ToString();
                h3cicgcompleted.InnerHtml = objServiceStatus[0].INT_INS_COMPLETED.ToString();
                h3unattInsdash.InnerHtml = objServiceStatus[0].INT_UNATTENDED_INS.ToString();
                h3ReprtNotUploaded.InnerHtml = objServiceStatus[0].INT_REPORTNOT_UPLOADED.ToString();
            }
            else
            {
                h3cicgapplied.InnerHtml = "0";
                h3cicgcompleted.InnerHtml = "0";
                h3unattInsdash.InnerHtml = "0";
                h3ReprtNotUploaded.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    protected void btnCICGStatus_Click(object sender, EventArgs e)
    {
        InsertCICGStatus(ddlYearCICG);
        ViewCICGData();
    }
    
    #endregion

    #region"Added by Suroj kumar Pradhan For Incentive Dashboard"
    private void ViewIncentiveStatus(string strFilterMode)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "B";
            objSWP.strFilterMode = strFilterMode;

            if (strFilterMode == "C")
            {
                objSWP.intUserid = Convert.ToInt32(Session["InvestorId"]);
            }
            else if (strFilterMode == "I")
            {
                objSWP.intUserid = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }

            List<SWPDashboard> objPEALStatusList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
            if (objPEALStatusList.Count > 0)
            {
                ltlincApplied.InnerHtml = objPEALStatusList[0].INCAPLLIED.ToString();
                ltlincSanctioned.InnerHtml = objPEALStatusList[0].INCSANCTIONED.ToString();
                ltlincPending.InnerHtml = objPEALStatusList[0].INCPENDING.ToString();
                ltlincRejected.InnerHtml = objPEALStatusList[0].INCREJECTED.ToString();
                SPPendinginc.InnerHtml = objPEALStatusList[0].INCPENDING.ToString();
                SPRejectinc.InnerHtml = objPEALStatusList[0].INCREJECTED.ToString();
            }
            else
            {
                ltlincApplied.InnerHtml = "0";
                ltlincSanctioned.InnerHtml = "0";
                ltlincPending.InnerHtml = "0";
                ltlincRejected.InnerHtml = "0";
                SPPendinginc.InnerHtml = "0";
                SPRejectinc.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    #endregion

    #region "Added by suroj kumar pardhan web method to fetch particular status of INCENTIVE"
    [WebMethod]
    public static List<SWPDashboard> ServiceDetail(string id, string Tid)
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objPEALStatusList = new List<SWPDashboard>();
        try
        {
            objSWP.strAction = "IS";
            objSWP.strincRESStatus = id;
            objSWP.intUserid = Convert.ToInt16(Tid);
            objPEALStatusList = objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
        
        return objPEALStatusList;
    }
    #endregion

    protected void grdPEALStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //lblPending
            Label lblProposalNo = (Label)e.Row.FindControl("lblPending");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            LinkButton lbtnQuery = (LinkButton)e.Row.FindControl("lbtnQuery");
            HiddenField hdnQueryStatus = (HiddenField)e.Row.FindControl("HiddenField3");
            HtmlControl myservcIframe = (HtmlControl)e.Row.FindControl("myservcIframe");
            string QueryStatus = hdnQueryStatus.Value;
            if (QueryStatus == "5")
            {
                lbtnQuery.Visible = true;
                lblStatus.Visible = false;
                myservcIframe.Attributes.Add("src", "InvestorPealDtls.aspx?ProposalNo=" + lblProposalNo.Text + "&QueryStatus=" + hdnQueryStatus.Value);

               
            }
            else
            {
                lbtnQuery.Visible = false;
                lblStatus.Visible = true;
            }
        }
    }
    protected void grdPEALStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }

    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
    public string MakeRandom(int pl)
    {
        string possibles = "0123456789";
        char[] passwords = new char[pl];
        Random rd = new Random();

        for (int i = 0; i < pl; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }


    #region Added by Sushant Jena

    /// <summary>
    /// Added by Sushant Jena On Dt.14-Aug-2018
    /// To get child unit name and self unit name for a investor.
    /// </summary>
    private void fillInvestorChildUnit()
    {
        try
        {
            objSWP.strAction = "INVUNIT";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

           
            DataTable dt = objserviceDashboard.getInvestorChildUnit(objSWP);

            if (dt.Rows.Count > 0)
            {
                DrpDwn_Investor_Unit.DataTextField = "VCH_INV_NAME";
                DrpDwn_Investor_Unit.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_Investor_Unit.DataSource = dt;
                DrpDwn_Investor_Unit.DataBind();
                DrpDwn_Investor_Unit.Items.Insert(0, new ListItem("-Select Unit-", "0"));

                DrpDwn_Investor_Unit.Visible = true;
            }
            else
            {
                DrpDwn_Investor_Unit.Items.Clear();
                DrpDwn_Investor_Unit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt.14-Aug-2018
    /// Filter details by unit 
    /// C-Chain (Means Including child users)
    /// I-Indivisual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DrpDwn_Investor_Unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpDwn_Investor_Unit.SelectedIndex > 0)
        {
            PEALSTATUS("I");
            ViewServiceStatus("I");
            ViewIncentiveStatus("I");
            PEALGridSatus("I");

        }
        else
        {
            PEALSTATUS("C");
            ViewServiceStatus("C");
            ViewIncentiveStatus("C");
            PEALGridSatus("C");

        }
        fillInvestorGrievanceMasterTracker();
    }


    private void fillInvestorGrievance()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP = new SWPDashboard();
            objSWP.strAction = "IGD";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
          
            DataTable dt = objserviceDashboard.getInvestorGrievance(objSWP);
            if (dt.Rows.Count > 0)
            {
                Gapplied.InnerText = dt.Rows[0]["APPLIED"].ToString();
                Gresolved.InnerText = dt.Rows[0]["RESOLVED"].ToString();
                Gpending.InnerText = dt.Rows[0]["PENDING"].ToString();
                Grejected.InnerText = dt.Rows[0]["REJECTED"].ToString();
            }
            else
            {
                Gapplied.InnerText = "0";
                Gresolved.InnerText = "0";
                Gpending.InnerText = "0";
                Grejected.InnerText = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }

    private void fillInvestorGrievanceMasterTracker()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "IGD";
            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                objSWP.intInvestorId = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }
            else
            {
                objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            }
            
            DataTable dt = objserviceDashboard.getInvestorGrievance(objSWP);
            if (dt.Rows.Count > 0)
            {
                Spangresolved.InnerText = dt.Rows[0]["RESOLVED"].ToString();
                Spangpending.InnerText = dt.Rows[0]["PENDING"].ToString();
            }
            else
            {
                Spangresolved.InnerText = "0";
                Spangpending.InnerText = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }
    #endregion
}