﻿//******************************************************************************************************************
// File Name             :   DICDashboard.aspx.cs
// Description           :   Show the Portal related to DIC Login Dashboard
// Created by            :   Pranay Kumar
// Created on            :   04-OCT-2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Configuration;
using System.Web.Services;
using System.Globalization;
using System.Data.SqlClient;
using BusinessLogicLayer.Incentive;
using System.Security.Cryptography;
using System.Text;

#endregion

public partial class Portal_Dashboard_DICDashboard : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    CommonMethodForDashboard commonMethodobj = new CommonMethodForDashboard();
    string UserId = string.Empty;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        else
        {
            UserId = Session["UserId"].ToString();
        }
        if (Session["desId"].ToString() == "10" || Session["desId"].ToString() == "126" || Session["desId"].ToString() == "9")
        {
            if (Session["desId"].ToString() == "10")
            {
                h1Dashuser.InnerText = "DIC DashBoard";
                adashuser.InnerText = "DIC DashBoard";
            }
            else if (Session["desId"].ToString() == "126")
            {
                h1Dashuser.InnerText = "Collector DashBoard";
                adashuser.InnerText = "Collector DashBoard";
            }
            else if (Session["desId"].ToString() == "9")
            {
                h1Dashuser.InnerText = "RIC DashBoard";
                adashuser.InnerText = "RIC DashBoard";
            }
            if (!IsPostBack)
            {

                try
                {
                    spLastUpdate.InnerText = DateTime.Now.ToString();
                    /*-----------------------------------------------------------------*/
                    ///Fill Dropdownlist for Financial Year
                    /*-----------------------------------------------------------------*/
                    commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
                    commonMethodobj.FillFinancialYear(ddlPealYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                    commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text 2016-17  FinalcealYear on Normal platform
                    commonMethodobj.FillFinancialYearWithYear(ddlspmgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform
                    DashboradCommon.MONTHBIND(ddlServcMonth); //bind both value 1 and  text January    Month value  on Normal platform
                    commonMethodobj.FillFinancialYear(ddlserviceyear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                    DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value 1 and  text January    Month value  on Normal platform
                    commonMethodobj.FillFinancialYear(ddlIncentiveYear); //bind bothe value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                    commonMethodobj.FillFinancialYear(ddlLandFinYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                    commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                    commonMethodobj.FillFinancialYearWithYear(ddlgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform
                    CurrentMonthSelect(ddlPealQuarter);
                    CurrentMonthSelect(ddlIncentive);
                    /*-----------------------------------------------------------------*/
                    //// Master Tracker
                    /*-----------------------------------------------------------------*/
                    DistrictSelect(); //   for get   Session["Pealuserid"] value  
                    BindUnderEvalution();// Single Window Application Status
                    BindPealRecieved(); //Single Window Application Status
                    BindPealApproved();//Single Window Application Status 
                    commonMethodobj.FillDist(ddlIncentiveDistrict); // bind District record in incentive dropdown list 
                    IncentiveMasterBind(); // Incentive Details
                    ViewSPMGMasterData();//State Project Monitoring Group 
                    ViewServiceMaster();  //Pending Approvals
                    if (ConfigurationManager.AppSettings["IDCO"] == "ON")
                    {
                        InsertAppa(ddlFinacialYear);// IDCO Post Allotment Applications
                    }
                       
                    commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
                    ddlgdist.SelectedValue = Session["Pealuserid"].ToString();
                    ddlgdist.Enabled = false;
                    BindMasterGrievanceportlet();//Grievance Status
                    


                    /*-----------------------------------------------------------------*/
                    ////Portlet Section
                    /*-----------------------------------------------------------------*/

                    commonMethodobj.FillDist(ddlPEALDistrict); // bind dist in PEAL dropdown list for SINGLE WINDOW APPLICATION STATUS 
                    FillProposalDetails();//SINGLE WINDOW APPLICATION STATUS 
                                          //FillProposalDistwiseDetails();//SINGLE WINDOW APPLICATION STATUS 
                                          //FillProposalDetailsIT();//SINGLE WINDOW APPLICATION STATUS 
                                          // FillProposalDetailsTourism();//SINGLE WINDOW APPLICATION STATUS 



                    if (ConfigurationManager.AppSettings["SPMG"] == "ON")
                    {
                        // ViewSPMGServiceStateLevelDataDirect();// STATE PROJECT MONITORING GROUP
                        ViewSPMGServiceDistrictLevelDataDirect(); //STATE PROJECT MONITORING GROUP
                    }
                        


                    //DashboradCommon.MONTHBIND(ddlServcMonth); //bind both value 1 and  text January    Month value  on Normal platform
                    commonMethodobj.FillDist(ddlServcDistrict); // bind District record in dropdown list 
                    ViewServiceStatus();//DEPARTMENT WISE APPROVALS



                    commonMethodobj.FillDist(ddlAPAADistrict);// bind dist in dropdown list  for IDCO POST ALLOTMENT APPLICATIONS 
                    if (ConfigurationManager.AppSettings["IDCO"] == "ON")
                    {
                        InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
                    }
                       
                    ViewApaaStatus();//IDCO POST ALLOTMENT APPLICATIONS



                   // commonMethodobj.FillDist(ddlIncentiveDistrict);// bind dist in dropdown list  for INCENTIVE DETAILS
                    IncentiveBind();//INCENTIVE DETAILS


                    LandServiceBind();//LAND ALLOTMENT DETAILS


                    ViewQueryService(); //QUERY MONITORING
                    ViewQueryServicepeal();//QUERY MONITORING
                    ViewQueryServiceIncentive();//QUERY MONITORING

                    FillIncentiveApplicationDropdown();  // INCENTIVE APPLICATION DETAILS on portlet 

                    commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
                    BindGrievanceportlet();//GRIEVANCE STATUS





                    //  spLastUpdate.InnerText = DateTime.Now.ToString();
                    // FillMasterFinYr();

                    // BindDistrictDIC();
                   // FillFinYrPortlet(ddlserviceyear);
               // DashboradCommon.MONTHBIND(ddlServcMonth);
               // DistrictSelect();
               // InsertAppa(ddlFinacialYear);
               // BindDistrictSrvc();
               // ViewServiceMaster();
               // BindDistrictPEAL();
               // DashboradCommon.MONTHBIND(ddlAppaMonth);
               // FillFinYrPortlet(ddlAppaYear);
               // InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
               // FillFinYrPortlet(ddlPealYear);
               // FillFinYrPortlet(ddlIncentiveYear);
              //  BindDistrictAPAA();
               // FillFinYrPortletS(ddlspmgyear);//Added By Manoj Kumar Behera

               // CurrentMonthSelect(ddlPealQuarter);
               // CurrentMonthSelect(ddlIncentive);
               // FillProposalDetails();
               // IncentiveMasterBind();
               // IncentiveBind();
               // BindUnderEvalution();
               // ViewApaaStatus();
               // BindPealRecieved();
                //BindPealApproved();
               // ViewServiceStatus();
                //added by Ritika Lath For incentive portlet 13th dec 2017
               
              //  FillFinYrPortlet(ddlLandFinYear);
               // LandServiceBind();
             //   FillFinYrPortlet(ddlyearquery);

               // ViewQueryService(); ///added by nibedita behera on 21-12-2017
               // ViewQueryServicepeal();
                //ViewQueryServiceIncentive();                               
               // ViewSPMGServiceDistrictLevelDataDirect();//added by manoj kumar behera
               // ViewSPMGMasterData();//added by manoj kumar behera

                ////Grievance
             //   FillFinYrPortletS(ddlgyear);
               // BindDistrictGrievance();
               // BindMasterGrievanceportlet();
               // BindGrievanceportlet();
                }
                catch(Exception ex)
                {
                    Util.LogError(ex, "Dashboard");
                }
            }
        }
        else
        {
            Response.Redirect("~/Portal/Default.aspx");
        }
    }

    #region SPMG Portlet Added By Manoj Kumar Behera On 30/08/2019
    private void ViewSPMGMasterData() // Method for  bind SPMG  on master tracker
    {
        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    objSWP.strAction = "SPMGDIC";
        //    string Finyear = ddlFinacialYear.SelectedValue;
        //    objSWP.intYearId = Convert.ToInt32(Finyear.ToString().Split('-')[0]);
        //    List<SWPDashboard> objServiceStatus = objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        spSpmgpnd.InnerText = objServiceStatus[0].intSPMGPending.ToString();
        //    }
        //    else
        //    {
        //        spSpmgpnd.InnerText = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //    objserviceDashboard = null;
        //}


        try
        {

            string Finyear = ddlFinacialYear.SelectedValue;
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewDistrictlevelSPMGData(Convert.ToInt32(UserId), Convert.ToInt32(Finyear.ToString().Split('-')[0]));
            if (objServiceStatus.Count > 0)
            {
                spSpmgpnd.InnerText = objServiceStatus[0].intSPMGPending.ToString();
            }
            else
            {
                spSpmgpnd.InnerText = "0";
            }
        }
        catch (Exception ex)
        {
            //Util.LogError(ex, "Dashboard");
            throw ex;
        }

    }
    //private void FillFinYrPortletS(DropDownList ddl)
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();

    //    objSWP.strAction = "FS";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddl.DataSource = objCICGFINYear;
    //    ddl.DataTextField = "Year";
    //    ddl.DataValueField = "intYearId";
    //    ddl.DataBind();
    //}
    private void ViewSPMGServiceDistrictLevelDataDirect()
    {
        try
        {
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

            string serviceUrl = "https://esuvidha.gov.in/odishadi/restservices/RestServer.php?view=issuestatus";

            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.ContentType = "application/json";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            }
            catch
            {
            }
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            using (var requestStream = webRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
            }

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
                                if (DynTable.Rows.Count > 0)
                                {
                                    InsertViewDistrictlevelSPMGData(DynTable.Rows[0]["Issues Received"].ToString(), DynTable.Rows[0]["Issues Resolved"].ToString(),
                                    DynTable.Rows[0]["Issues Pending"].ToString(), DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString());
                                }
                                else
                                {
                                    spmgraised.InnerHtml = "0";
                                    spmgresolved.InnerHtml = "0";
                                    spmgpending.InnerHtml = "0";
                                    spmg30pending.InnerHtml = "0";
                                }
                            }
                            else
                            {
                                ViewDistrictlevelSPMGData();
                            }
                        }
                        catch (WebException ex)
                        {
                            Util.LogError(ex, "Dashboard");
                            ViewDistrictlevelSPMGData();
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Util.LogError(ex, "Dashboard");
            ViewDistrictlevelSPMGData();
        }
    }
    private void ViewDistrictlevelSPMGData() //STATE PROJECT MONITORING GROUP
    {
        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    objSWP.strAction = "SPMGDIC";
        //    objSWP.intYearId = Convert.ToInt32(ddlspmgyear.SelectedValue);
        //    List<SWPDashboard> objServiceStatus = objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        spmgraised.InnerHtml = objServiceStatus[0].intSPMGRaised.ToString();
        //        spmgresolved.InnerHtml = objServiceStatus[0].intSPMGResolved.ToString();
        //        spmgpending.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
        //        spmg30pending.InnerHtml = objServiceStatus[0].intSPMGIssuePending.ToString();
        //    }
        //    else
        //    {
        //        spmgraised.InnerHtml = "0";
        //        spmgresolved.InnerHtml = "0";
        //        spmgpending.InnerHtml = "0";
        //        spmg30pending.InnerHtml = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //    objserviceDashboard = null;
        //}


        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewDistrictlevelSPMGData(Convert.ToInt32(ddlspmgyear.SelectedValue));
            if (objServiceStatus.Count > 0)
            {
                spmgraised.InnerHtml = objServiceStatus[0].intSPMGRaised.ToString();
                spmgresolved.InnerHtml = objServiceStatus[0].intSPMGResolved.ToString();
                spmgpending.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
                spmg30pending.InnerHtml = objServiceStatus[0].intSPMGIssuePending.ToString();
            }
            else
            {
                spmgraised.InnerHtml = "0";
                spmgresolved.InnerHtml = "0";
                spmgpending.InnerHtml = "0";
                spmg30pending.InnerHtml = "0";
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void InsertViewDistrictlevelSPMGData(string recived, string resolved, string pending, string pendinglast)
    {
        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = Convert.ToInt32(Session["Userid"].ToString());
                    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(ddlspmgyear.SelectedValue);
                    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "2";//For ALL DIC LEVEL IS 2
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
                if (PnlDt.Rows.Count > 0)
                {
                    spmgraised.InnerHtml = PnlDt.Rows[0]["RAISED"].ToString();
                    spmgresolved.InnerHtml = PnlDt.Rows[0]["RESOLVED"].ToString();
                    spmgpending.InnerHtml = PnlDt.Rows[0]["PENDING"].ToString();
                    spmg30pending.InnerHtml = PnlDt.Rows[0]["ISSUEPENDING"].ToString();
                }
                else
                {
                    spmgraised.InnerHtml = "0";
                    spmgresolved.InnerHtml = "0";
                    spmgpending.InnerHtml = "0";
                    spmg30pending.InnerHtml = "0";
                }
            }
            catch (Exception ex)
            {
                // con.Close();
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

    }
    protected void btnspmg_Click(object sender, EventArgs e)
    {
        try
        {
            ViewSPMGServiceDistrictLevelDataDirect();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
       
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

    #endregion

    #region added by ritika lath 13th dec 2017
    private void FillIncentiveApplicationDropdown()  // Bind Unit Type ,District and Policy  in  INCENTIVE APPLICATION DETAILS portlet 
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        DataSet objDa = objBuisnessLayer.BindDropdown("rpt");
        if (objDa != null)
        {
            FillDropDown(objDa.Tables[0], "Unit Type", drpInctUnitType);
            FillDropDown(objDa.Tables[1], "District", drpInctAppDistrict);
            drpInctAppDistrict.SelectedValue = Session["Pealuserid"].ToString();
            drpInctAppDistrict.Enabled = false;
            FillDropDown(objDa.Tables[2], "Policy", drpPolicy);
        }

        for (int i = 2010; i <= DateTime.Today.Year; i++)
        {
            drpInctYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drpInctYear.Items.Insert(0, new ListItem("-Select Year-", "0"));
    }

    /// <summary>
    /// Function to bind the drodown, main code to bind the dropdown
    /// </summary>
    /// <param name="objDt">Datatable with all values</param>
    /// <param name="strHeaderType">type of data in dropdown</param>
    /// <param name="objDropdown">dropdown to bind</param>
    private void FillDropDown(DataTable objDt, string strHeaderType, DropDownList objDropdown)
    {
        objDropdown.Items.Clear();
        if (objDt != null && objDt.Rows.Count > 0)
        {
            objDropdown.DataSource = objDt;
            objDropdown.DataTextField = "NAME";
            objDropdown.DataValueField = "ID";
            objDropdown.DataBind();
        }
        objDropdown.Items.Insert(0, new ListItem(string.Format("-Select {0}-", strHeaderType), "0"));
    }
    #endregion

    private void IncentiveMasterBind()  // Method for  bind Incetive data 
    {
        try
        {
            //objSWP = new SWPDashboard();
            //objserviceDashboard = new DashboardBusinessLayer();
            //List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            //objSWP.strAction = "B";
            //// string PealQuareter = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
            //string IncentiveDist = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
            ////string PealYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
            //objSWP.intQuarter = 0;
            //objSWP.intYearId = 0;
            //objSWP.intDistrictid = Convert.ToInt16(IncentiveDist);
            //objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;//Added by suroj on 26-10-17 to check finacial yr
            //objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
            //objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
            //lblIncpendingdtls.Text = objswpDashboardList[0].INCPENDING;

            
                List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
                objswpDashboardList = commonMethodobj.IncentiveMasterBind(ddlFinacialYear.SelectedValue , Convert.ToInt16(ddlIncentiveDistrict.SelectedValue));
                lblIncpendingdtls.Text = objswpDashboardList[0].INCPENDING;

           



        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
    //private void FillFinYrPortlet(DropDownList ddl)
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();

    //    objSWP.strAction = "FY";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddl.DataSource = objCICGFINYear;
    //    ddl.DataTextField = "Year";
    //    ddl.DataValueField = "Year";
    //    ddl.DataBind();
    //    if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 3)
    //    {
    //        ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString()) - 1) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")));
    //    }
    //    else
    //    {
    //        ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString())) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")) + 1);
    //    }

    //}
    //private void FillMasterFinYr()
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();

    //    objSWP.strAction = "FY";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddlFinacialYear.DataSource = objCICGFINYear;
    //    ddlFinacialYear.DataTextField = "Year";
    //    ddlFinacialYear.DataValueField = "Year";
    //    ddlFinacialYear.DataBind();
    //    ListItem list = new ListItem();
    //    string year = DateTime.Now.Year.ToString();
    //    //ddlFinacialYear.SelectedIndex = 1;
    //}
    //private void BindDistrictDIC()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlIncentiveDistrict.DataSource = objProjList;
    //    ddlIncentiveDistrict.DataTextField = "vchDistName";
    //    ddlIncentiveDistrict.DataValueField = "intDistId";
    //    ddlIncentiveDistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlIncentiveDistrict.Items.Insert(0, list);

    //}
    //private void BindDistrictSrvc()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlServcDistrict.DataSource = objProjList;
    //    ddlServcDistrict.DataTextField = "vchDistName";
    //    ddlServcDistrict.DataValueField = "intDistId";
    //    ddlServcDistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlServcDistrict.Items.Insert(0, list);

    //}
    private void CurrentMonthSelect(DropDownList ddlQuarter)
    {
        decimal Month = Math.Ceiling(DateTime.Today.Month / 3m);
        ddlQuarter.SelectedValue = Month.ToString();
        //ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }
    private void DistrictSelect()  // to select district of collector login 
    {
        try
        {

        
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        objSWP.strAction = "SU";
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        if (objswpDashboardList.Count > 0)
        {
            Session["Pealuserid"] = objswpDashboardList[0].intDistrictid.ToString();
        }
        ddlPEALDistrict.SelectedValue = Session["Pealuserid"].ToString();
        ddlPEALDistrict.Enabled = false;
        ddlIncentiveDistrict.SelectedValue = Session["Pealuserid"].ToString();
        ddlIncentiveDistrict.Enabled = false;
        ddlAPAADistrict.SelectedValue = Session["Pealuserid"].ToString();
        ddlAPAADistrict.Enabled = false;
        ddlServcDistrict.SelectedValue = Session["Pealuserid"].ToString();
        ddlServcDistrict.Enabled = false;
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }
    #endregion
    private void ViewServiceMaster() // Method for  bind Pending approval  on master tracker
    {
        //SWPDashboard objDashboard = new SWPDashboard();
        //DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        //objDashboard.strAction = "DM";
        //objDashboard.intDeptId = 0;
        //objDashboard.intServiceId = 0;
        //objDashboard.strFinacialYear = ddlFinacialYear.SelectedValue;
        //objDashboard.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
        //objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
        //List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
        //if (objServiceStatus.Count > 0)
        //{
        //    spanapprove.InnerText = objServiceStatus[0].strPending.ToString();
        //}

        try
        {

            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.ViewServiceMasterDept(Convert.ToInt32(Session["Pealuserid"]),Convert.ToInt32( UserId), ddlFinacialYear.SelectedValue);
            if (objswpDashboardList.Count > 0)
            {
                spanapprove.InnerText = objswpDashboardList[0].strPending.ToString();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    #region CSR Portlet
    private void InsertCSRStatus(DropDownList ddlDistrict, DropDownList ddlCSRYear)
    {
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = "1";
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string District = string.IsNullOrEmpty(ddlDistrict.SelectedValue) ? default(string) : ddlDistrict.SelectedValue.ToString();
        //string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlCSRYear.SelectedValue) ? default(string) : ddlCSRYear.SelectedValue.ToString();
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";

        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + UniqueKey + "/" + Type;
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
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "DELETE FROM T_CSR_SpentDtls_Admin";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    string finalquery = "INSERT INTO T_CSR_SpentDtls_Admin(adminid,TotalAmount,TotalProject,dtmCreatedon,intYearId,intDistrictId)" +
                    "VALUES(" + Convert.ToInt32(Session["Userid"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["TotalProject"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "'," + Year + "," + District + ")";
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }


    #endregion

    #region Automated Post Allotment Application portlet

    private void InsertAppaStatus(DropDownList ddlDistrict, DropDownList ddlMonth, DropDownList ddlYear)
    {
        string finalquery = string.Empty;
        string deptId = "";
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string District = string.IsNullOrEmpty(ddlAPAADistrict.SelectedValue) ? default(string) : ddlAPAADistrict.SelectedValue.ToString();
        string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlAppaYear.SelectedValue) ? default(string) : ddlAppaYear.SelectedValue.ToString();
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";
        deptId = Session["deptid"].ToString();
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //if (Session["desId"].ToString() == "126")
        //{
        //    deptId = "0";
        //}
        //else
        //{
        deptId = Session["deptid"].ToString();
        //}
        objlist = objserviceDashboard.CheckAppastatus(objSWP);
        if (objlist.Count > 0)
        {
            if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
            {
                Type = "0";
            }
            else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
            {
                Type = "1";
            }
            else//MSME
            {
                Type = "2";
            }
        }
        try
        {
            District = Session["Pealuserid"].ToString();
            string serviceUrl = "";
            if (Session["desId"].ToString() == "126")
            {
                Type = "0";
                serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + District + "/" + 0 + "/" + Type + "/" + Year + "/" + Month;
            }
            else
            {
                serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + District + "/" + deptId + "/" + Type + "/" + Year + "/" + Month;
            }
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
                    string query = "SELECT ADMINID FROM T_APAA_Service_DIC WHERE CONVERT(DATE,dtmCreatedon)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_APAA_Service_DIC SET adminid=" + Convert.ToInt32(Session["Userid"]) + ",TotalApplied=" + finalOut[2].ToString() + ",TotalDisposed=" + DynTable.Rows[0]["TotalDisposed"].ToString() + "," +
                             "TotalMajorPendingIdco=" + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + ",TotalPendingIdco=" + DynTable.Rows[0]["TotalPendingIdco"].ToString() + ",TotalPendingUnit=" + DynTable.Rows[0]["TotalPendingUnit"].ToString() + ",dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',intYearId=" + Year + ",intDistrictId=" + District + ",intMonthId=" + Month + " WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
                    }
                    else
                    {
                        finalquery = "INSERT INTO T_APAA_Service_DIC(adminid,TotalApplied,TotalDisposed,TotalMajorPendingIdco,TotalPendingIdco,TotalPendingUnit,dtmCreatedon,intYearId,intDistrictId,intMonthId)" +
                    "VALUES(" + Convert.ToInt32(Session["Userid"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["TotalDisposed"].ToString() + "," + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingUnit"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "'," + Year + "," + District + "," + Month + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void ViewApaaStatus() //view IDCO POST ALLOTMENT APPLICATIONS on normal platfrom
    {

        //objSWP = new SWPDashboard();
       // objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    objSWP.strAction = "DIC";
        //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    // objSWP.intYearId = Convert.ToInt32(ddlAppaYear.SelectedValue);
        //    objSWP.Year = ddlAppaYear.SelectedItem.Text;
        //    objSWP.intMonthId = Convert.ToInt32(ddlAppaMonth.SelectedValue);
        //    objSWP.intDistrictid = Convert.ToInt32(ddlAPAADistrict.SelectedValue);
        //    List<SWPDashboard> objPEALStatusList = objserviceDashboard.GetDashboardAPAAtatus(objSWP).ToList();
        //    if (objPEALStatusList.Count > 0)
        //    {
        //        spchngrqstApplied.InnerHtml = objPEALStatusList[0].strChngReqApplied.ToString();
        //        spchngreqdispose.InnerHtml = objPEALStatusList[0].strChngReqDispose.ToString();
        //        spchngreqPendAtIDCO.InnerHtml = objPEALStatusList[0].strChngReqPendingAtIDCO.ToString();
        //        spchngReqCrossThirty.InnerHtml = objPEALStatusList[0].strChngReqCrossthirty.ToString();
        //        //spAPAAPending.InnerHtml = objPEALStatusList[0].strChngReqPendAtUnit.ToString();
        //        spnPendingatUnit.InnerHtml = objPEALStatusList[0].strChngReqPendAtUnit.ToString();
        //    }
        //    else
        //    {
        //        spchngrqstApplied.InnerHtml = "0";
        //        spchngreqdispose.InnerHtml = "0";
        //        spchngreqPendAtIDCO.InnerHtml = "0";
        //        spchngReqCrossThirty.InnerHtml = "0";
        //        //spAPAAPending.InnerHtml = "0";
        //        spnPendingatUnit.InnerHtml = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //    objserviceDashboard = null;
        //}

        try
        {
            List<SWPDashboard> objPEALStatusList = commonMethodobj.ViewApaaStatusDic(Convert.ToInt32(UserId), ddlAppaYear.SelectedItem.Text, Convert.ToInt32(ddlAppaMonth.SelectedValue), Convert.ToInt32(ddlAPAADistrict.SelectedValue));
            if (objPEALStatusList.Count > 0)
            {
                spchngrqstApplied.InnerHtml = objPEALStatusList[0].strChngReqApplied.ToString();
                spchngreqdispose.InnerHtml = objPEALStatusList[0].strChngReqDispose.ToString();
                spchngreqPendAtIDCO.InnerHtml = objPEALStatusList[0].strChngReqPendingAtIDCO.ToString();
                spchngReqCrossThirty.InnerHtml = objPEALStatusList[0].strChngReqCrossthirty.ToString();
                //spAPAAPending.InnerHtml = objPEALStatusList[0].strChngReqPendAtUnit.ToString();
                spnPendingatUnit.InnerHtml = objPEALStatusList[0].strChngReqPendAtUnit.ToString();
            }
            else
            {
                spchngrqstApplied.InnerHtml = "0";
                spchngreqdispose.InnerHtml = "0";
                spchngreqPendAtIDCO.InnerHtml = "0";
                spchngReqCrossThirty.InnerHtml = "0";
                //spAPAAPending.InnerHtml = "0";
                spnPendingatUnit.InnerHtml = "0";
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void btnAPAASubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ConfigurationManager.AppSettings["IDCO"] == "ON")
            {
                InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
            }
                
            ViewApaaStatus();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        
    }
    #endregion

    #region PEAL Portlet
    [WebMethod]
    public static List<SWPDashboard> EmployeementPealDetailsBind()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PED";
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        return objCSRStatus;

    }
    [WebMethod]
    public static List<SWPDashboard> EmployeementCapitalPealDetailsBind()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PID";
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        return objCSRStatus;

    }
    [WebMethod]
    public static List<SWPDashboard> PealDetailsBind()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PPD";
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        return objCSRStatus;

    }
    #endregion

    #region"Added by suroj to view Proposal dtls"

    private void FillProposalDetails()  // Mothod for view data for Proposal details   on noramal platform
    {
        //try
        //{
        //    SWPDashboard objSWP = new SWPDashboard();
        //    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    //objSWP.strAction = "SU";
        //    //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    //objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    //if (objswpDashboardList.Count > 0)
        //    //{
        //    //    Session["Pealuserid"] = objswpDashboardList[0].intDistrictid.ToString();
        //    //}

        //    //List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "PB";
        //    string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
        //    string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
        //    string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
        //    objSWP.intQuarter = 0;
        //    //objSWP.intYearId = Convert.ToInt16(PealYear);
        //    objSWP.strFinacialYear = PealYear.ToString();
        //    // objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
        //    objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
        //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    if (objswpDashboardList.Count > 0)
        //    {
        //        lblPealApplied.Text = objswpDashboardList[0].strApplied;
        //        lblPealRejected.Text = objswpDashboardList[0].strRejected;
        //        lblPealApproved.Text = objswpDashboardList[0].strApproved;

        //        //added by nibedita
        //        lblPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
        //        lblPealDeferred.Text = objswpDashboardList[0].strDeferred;
        //        lblQueryInprogress.Text = objswpDashboardList[0].QraiseTotal;
        //        Lbl_Peal_ORTPSA.Text = objswpDashboardList[0].strPealOrtpsaCrossedState;///// Added by Sushant Jena on Dt. 25-May-2018
        //    }
        //    else
        //    {
        //        lblPealApplied.Text = "0";
        //        lblPealRejected.Text = "0";
        //        lblPealApproved.Text = "0";
        //        lblPealUnderEvalution.Text = "0";
        //        lblPealDeferred.Text = "0";
        //        lblQueryInprogress.Text = "0";
        //        Lbl_Peal_ORTPSA.Text = "0";///// Added by Sushant Jena on Dt. 25-May-2018
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //}



        try
        {
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDetails(0, ddlPealYear.SelectedValue.ToString(), Convert.ToInt32(Session["Pealuserid"]), Convert.ToInt32(UserId));

            if (objswpDashboardList.Count > 0)
            {
                lblPealApplied.Text = objswpDashboardList[0].strApplied;
                lblPealRejected.Text = objswpDashboardList[0].strRejected;
                lblPealApproved.Text = objswpDashboardList[0].strApproved;

                //added by nibedita
                lblPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
                lblPealDeferred.Text = objswpDashboardList[0].strDeferred;
                lblQueryInprogress.Text = objswpDashboardList[0].QraiseTotal;
                Lbl_Peal_ORTPSA.Text = objswpDashboardList[0].strPealOrtpsaCrossedState;///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                lblPealApplied.Text = "0";
                lblPealRejected.Text = "0";
                lblPealApproved.Text = "0";
                lblPealUnderEvalution.Text = "0";
                lblPealDeferred.Text = "0";
                lblQueryInprogress.Text = "0";
                Lbl_Peal_ORTPSA.Text = "0";///// Added by Sushant Jena on Dt. 25-May-2018
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }

    #endregion

    #region MEMBER FUNCTION

    //private void BindDistrictAPAA()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlAPAADistrict.DataSource = objProjList;
    //    ddlAPAADistrict.DataTextField = "vchDistName";
    //    ddlAPAADistrict.DataValueField = "intDistId";
    //    ddlAPAADistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlAPAADistrict.Items.Insert(0, list);

    //}

    //private void BindDistrictPEAL()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlPEALDistrict.DataSource = objProjList;
    //    ddlPEALDistrict.DataTextField = "vchDistName";
    //    ddlPEALDistrict.DataValueField = "intDistId";
    //    ddlPEALDistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlPEALDistrict.Items.Insert(0, list);

    //}
    #endregion

    #region Service Portlet
    //private void BindService()
    //{
    //    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //    objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
    //    ddlService.DataSource = objServicelist;
    //    ddlService.DataTextField = "strServiceName";
    //    ddlService.DataValueField = "intServiceId";
    //    ddlService.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlService.Items.Insert(0, list);

    //}
    //private void BindDept()
    //{
    //    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //    objServicelist = objService.BindDepartment("DP").ToList();
    //    ddldept.DataSource = objServicelist;
    //    ddldept.DataTextField = "strdeptname";
    //    ddldept.DataValueField = "Deptid";
    //    ddldept.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddldept.Items.Insert(0, list);
    //}
    //protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindService();
    //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowSearchpanel()", true);

    //}
    protected void btnStatusOfApproval_Click(object sender, EventArgs e)
    {
        try
        {
            ViewServiceStatus();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        
    }
    private void ViewServiceStatus()  //view Department Wise Approvals on normal platfrom
    {
        //SWPDashboard objDashboard = new SWPDashboard();
       // DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        //try
        //{
        //    objDashboard.strAction = "DGM";
        //    //objDashboard.intInvestorId = 0;
        //    //objDashboard.intDistrictid = Convert.ToInt32(ddlServcDistrict.SelectedValue);
        //    objDashboard.intDistrictid = Convert.ToInt32(Session["Pealuserid"]);
        //    // objDashboard.intYearId = Convert.ToInt32(ddlserviceyear.SelectedValue);
        //    objDashboard.strFinacialYear = ddlserviceyear.SelectedValue;
        //    objDashboard.intMonthId = Convert.ToInt32(ddlServcMonth.SelectedValue);
        //    objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
        //    List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        hdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
        //        hdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
        //        hdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
        //        hdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();
        //        //hdExceed.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
        //        hdnqueryRaised.InnerHtml = objServiceStatus[0].QraiseTotal.ToString();
        //    }
        //    else
        //    {
        //        hdApplied.InnerHtml = "0";
        //        hdApprove.InnerHtml = "0";
        //        hdPending.InnerHtml = "0";
        //        // hdExceed.InnerHtml = "0";
        //        hdnqueryRaised.InnerHtml = "0";
        //        hdReject.InnerHtml = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objDashboard = null;
        //    objDashboardBal = null;
        //}



        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewServiceStatus(Convert.ToInt32(Session["Pealuserid"]), ddlserviceyear.SelectedValue, Convert.ToInt32(ddlServcMonth.SelectedValue), Convert.ToInt32(UserId));
            if (objServiceStatus.Count > 0)
            {
                hdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
                hdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
                hdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
                hdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();
                //hdExceed.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
                hdnqueryRaised.InnerHtml = objServiceStatus[0].QraiseTotal.ToString();
            }
            else
            {
                hdApplied.InnerHtml = "0";
                hdApprove.InnerHtml = "0";
                hdPending.InnerHtml = "0";
                // hdExceed.InnerHtml = "0";
                hdnqueryRaised.InnerHtml = "0";
                hdReject.InnerHtml = "0";
            }
        }
        catch(Exception  ex)
        {
            throw ex;
        }
    }
    #endregion
    protected void btnPealsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            FillProposalDetails();
            //FillProposalDistwiseDetails();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }


        //try
        //{
        //    SWPDashboard objSWP = new SWPDashboard();
        //    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "PB";
        //    string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
        //    string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
        //    string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
        //    objSWP.intQuarter = 0;
        //    //objSWP.intYearId = Convert.ToInt16(PealYear);
        //    objSWP.strFinacialYear = PealYear.ToString();
        //    objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());//Convert.ToInt16(PealDistrict);
        //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);

        //    objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    lblPealApplied.Text = objswpDashboardList[0].strApplied;
        //    lblPealRejected.Text = objswpDashboardList[0].strRejected;
        //    lblPealApproved.Text = objswpDashboardList[0].strApproved;
        //    lblQueryInprogress.Text = objswpDashboardList[0].QraiseTotal;
        //    //added by nibedita
        //    lblPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
        //    lblPealDeferred.Text = objswpDashboardList[0].strDeferred;
        //    Lbl_Peal_ORTPSA.Text = objswpDashboardList[0].strPealOrtpsaCrossedState;///// Added by Sushant Jena on Dt. 25-May-2018
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //}
    }
    #region"ADDED BY SUROJ FOR INCENTIVE PORTLET BIND"
    private void IncentiveBind()  //view INCENTIVE DETAILS on normal platfrom
    {
        //try
        //{
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "B";
        //    string IncentiveQtr = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
        //    string IncentiveDist = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
        //    string IncentiveYr = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
        //    objSWP.intQuarter = Convert.ToInt16(IncentiveQtr);
        //    objSWP.strFinacialYear = IncentiveYr.ToString();
        //    objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());//Convert.ToInt16(IncentiveDist);
        //    objSWP.intUserid = 0;
        //    objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
        //    lblIncApplied.Text = objswpDashboardList[0].INCAPLLIED;
        //    lblIncsanctioed.Text = objswpDashboardList[0].INCSANCTIONED;
        //    lblIncpending.Text = objswpDashboardList[0].INCPENDING;
        //    lblIncrejected.Text = objswpDashboardList[0].INCREJECTED;
        //    //lblIncpendingdtls.Text = objswpDashboardList[0].INCPENDING;

        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //}



        try
        {
            string IncentiveQtr = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
            //string IncentiveYr = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
           // string IncentiveDist = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
            List<SWPDashboard> objswpDashboardList = commonMethodobj.IncentiveBind(ddlIncentiveYear.SelectedValue ,Convert.ToInt32(Session["Pealuserid"]) , Convert.ToInt16(IncentiveQtr));

            if(objswpDashboardList.Count > 0)
            {
                lblIncApplied.Text = objswpDashboardList[0].INCAPLLIED;
                lblIncsanctioed.Text = objswpDashboardList[0].INCSANCTIONED;
                lblIncpending.Text = objswpDashboardList[0].INCPENDING;
                lblIncrejected.Text = objswpDashboardList[0].INCREJECTED;
                //lblIncpendingdtls.Text = objswpDashboardList[0].INCPENDING;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    protected void btnIncentiveSubmit_Click(object sender, EventArgs e)
    {
        //IncentiveBind();
        //List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //objSWP.strAction = "B";
        //string PealQuareter = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
        //string PealDistrict = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
        //string PealYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
        //objSWP.intQuarter = Convert.ToInt16(PealQuareter);
        ////objSWP.intYearId = Convert.ToInt16(PealYear);
        //objSWP.strFinacialYear = PealYear.ToString();
        //objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());//Convert.ToInt16(PealDistrict);
        //objSWP.intUserid = 0;
        //objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
        //lblIncApplied.Text = objswpDashboardList[0].INCAPLLIED;
        //lblIncsanctioed.Text = objswpDashboardList[0].INCSANCTIONED;
        //lblIncpending.Text = objswpDashboardList[0].INCPENDING;
        //lblIncrejected.Text = objswpDashboardList[0].INCREJECTED;

        try
        {
            IncentiveBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    private void BindUnderEvalution() // Method for binding data from  Peal pending data on master Tracker dashbord  .
    {
        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //objSWP.strAction = "UE";
        //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        //objSWP.intDistrictid = Convert.ToInt16(Session["Pealuserid"].ToString());
        //objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //lblTrackerEvalution.InnerText = objswpDashboardList[0].strUnderEvaltion;

        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();

            objswpDashboardList = commonMethodobj.BindUnderEvalution(ddlFinacialYear.SelectedValue, UserId ,Convert.ToInt32( Session["Pealuserid"]));
            lblTrackerEvalution.InnerText = objswpDashboardList[0].strUnderEvaltion;
        }
        catch (Exception ex)
        {
            // Util.LogError(ex, "Dashboard");
            throw ex;
        }


    }
    private void BindPealRecieved() // Method for binding data from  Peal recived data on master Tracker dashbord  .
    {
        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //objSWP.strAction = "RP";
        //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        //objSWP.intDistrictid = Convert.ToInt16(Session["Pealuserid"].ToString());
        //objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //lblProposalRecieved.InnerText = objswpDashboardList[0].strPealRecived;

        try
        {

            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.BindPealRecieved(ddlFinacialYear.SelectedValue, UserId ,Convert.ToInt32(Session["Pealuserid"]));
            lblProposalRecieved.InnerText = objswpDashboardList[0].strPealRecived;
        }
        catch (Exception ex)
        {
            // Util.LogError(ex, "Dashboard");
            throw ex;
        }


    }
    private void BindPealApproved() // Method for binding data from  Peal Approved data on master Tracker dashbord  .
    {
        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //objSWP.strAction = "RA";
        //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        //objSWP.intDistrictid = Convert.ToInt16(Session["Pealuserid"].ToString());
        //objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //lblProposalapproved.InnerText = objswpDashboardList[0].strPealApproved;



        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.BindPealApproved(ddlFinacialYear.SelectedValue, UserId ,Convert.ToInt32(Session["Pealuserid"]));
            lblProposalapproved.InnerText = objswpDashboardList[0].strPealApproved;
        }
        catch (Exception ex)
        {
            // Util.LogError(ex, "Dashboard");
            throw ex;
        }


    }
    private void InsertAppa(DropDownList ddlYear)
    {
        //string finalquery = string.Empty;
        //SqlCommand cmd = null;
        //DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        //string Type = string.Empty;
        //SWPDashboard objSWP = new SWPDashboard();
        //string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        //inputJson = inputJson.TrimStart('[').TrimEnd(']');
        ////string District = string.IsNullOrEmpty(ddlAPAADistrict.SelectedValue) ? default(string) : ddlAPAADistrict.SelectedValue.ToString();
        ////string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        //string Year = string.IsNullOrEmpty(ddlYear.SelectedValue) ? default(string) : ddlYear.SelectedValue.ToString();
        //string Deptid = "0";
        ////string UniqueKey = Session["UID"].ToString();
        //string UniqueKey = "0";
        //List<SWPDashboard> objlist = new List<SWPDashboard>();
        //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objlist = objserviceDashboard.CheckAppastatus(objSWP);
        //if (objlist.Count > 0)
        //{
        //    if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
        //    {
        //        Type = "0";
        //    }
        //    else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
        //    {
        //        Type = "1";
        //    }
        //    else//MSME
        //    {
        //        Type = "2";
        //    }
        //}
        //try
        //{
        //    string serviceUrl = "";
        //    if (Session["desId"].ToString() == "126")
        //    {
        //        Type = "0";
        //        serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + Convert.ToInt16(Session["Pealuserid"].ToString()) + "/" + 0 + "/" + Type + "/" + Year + "/" + 0;
        //    }
        //    else
        //    {
        //        serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + Convert.ToInt16(Session["Pealuserid"].ToString()) + "/" + Session["deptid"].ToString() + "/" + Type + "/" + Year + "/" + 0;
        //    }
        //    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
        //    httpRequest.Accept = "application/json";
        //    httpRequest.ContentType = "application/json";
        //    httpRequest.Method = "GET";
        //    using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        //    {
        //        using (Stream stream = httpResponse.GetResponseStream())
        //        {
        //            string strResult = (new StreamReader(stream)).ReadToEnd();
        //            DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
        //            string output = DynTable.Rows[0]["getSWPConsolidationDataResult"].ToString();
        //            string[] finalOut = output.Split(':');
        //            spAPAAPending.InnerHtml = DynTable.Rows[0]["TotalPendingIdco"].ToString();
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    spAPAAPending.InnerHtml = "0";
        //    Util.LogError(ex, "Dashboard");
        //}



        string Year = string.IsNullOrEmpty(ddlYear.SelectedValue) ? default(string) : ddlYear.SelectedValue.ToString();

        try
        {
            DataTable DynTable = commonMethodobj.InsertAppa(Year, Convert.ToInt32(UserId) , Convert.ToInt16(Session["Pealuserid"]));
            spAPAAPending.InnerText = DynTable.Rows[0]["TotalPendingIdco"].ToString();
        }
        catch (Exception ex)
        {
            spAPAAPending.InnerText = "0";
            // Util.LogError(ex, "Dashboard");
            throw ex;
        }

    }

    protected void ddlFinacialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DistrictSelect(); //   for get   Session["Pealuserid"] value  
            BindUnderEvalution();// Single Window Application Status
            BindPealRecieved(); //Single Window Application Status
            BindPealApproved();//Single Window Application Status 
            commonMethodobj.FillDist(ddlIncentiveDistrict); // bind District record in incentive dropdown list 
            IncentiveMasterBind(); // Incentive Details
            ViewSPMGMasterData();//State Project Monitoring Group 
            ViewServiceMaster();  //Pending Approvals
            if (ConfigurationManager.AppSettings["IDCO"] == "ON")
            {
                InsertAppa(ddlFinacialYear);// IDCO Post Allotment Applications
            }

               
            commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
            ddlgdist.SelectedValue = Session["Pealuserid"].ToString();
            ddlgdist.Enabled = false;
            BindMasterGrievanceportlet();//Grievance Status
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        //BindUnderEvalution();
        //InsertAppa(ddlFinacialYear);
        //ViewServiceMaster();
        //BindPealRecieved();
        //BindPealApproved();
        //ViewSPMGMasterData();//added by manoj kumar behera
        //BindMasterGrievanceportlet();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            spLastUpdate.InnerText = DateTime.Now.ToString();
            /*-----------------------------------------------------------------*/
            ///Fill Dropdownlist for Financial Year
            /*-----------------------------------------------------------------*/
            commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
            commonMethodobj.FillFinancialYear(ddlPealYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
            commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text 2016-17  FinalcealYear on Normal platform
            commonMethodobj.FillFinancialYearWithYear(ddlspmgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform
            DashboradCommon.MONTHBIND(ddlServcMonth); //bind both value 1 and  text January    Month value  on Normal platform
            commonMethodobj.FillFinancialYear(ddlserviceyear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
            DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value 1 and  text January    Month value  on Normal platform
            commonMethodobj.FillFinancialYear(ddlIncentiveYear); //bind bothe value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
            commonMethodobj.FillFinancialYear(ddlLandFinYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
            commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
            commonMethodobj.FillFinancialYearWithYear(ddlgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform
            CurrentMonthSelect(ddlPealQuarter);
            CurrentMonthSelect(ddlIncentive);
            /*-----------------------------------------------------------------*/
            //// Master Tracker
            /*-----------------------------------------------------------------*/
            DistrictSelect(); //   for get   Session["Pealuserid"] value  
            BindUnderEvalution();// Single Window Application Status
            BindPealRecieved(); //Single Window Application Status
            BindPealApproved();//Single Window Application Status 
            commonMethodobj.FillDist(ddlIncentiveDistrict); // bind District record in incentive dropdown list 
            IncentiveMasterBind(); // Incentive Details
            ViewSPMGMasterData();//State Project Monitoring Group 
            ViewServiceMaster();  //Pending Approvals
            if (ConfigurationManager.AppSettings["IDCO"] == "ON")
            {
                InsertAppa(ddlFinacialYear);// IDCO Post Allotment Applications
            }

               
            commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
            ddlgdist.SelectedValue = Session["Pealuserid"].ToString();
            ddlgdist.Enabled = false;
            BindMasterGrievanceportlet();//Grievance Status
            


            /*-----------------------------------------------------------------*/
            ////Portlet Section
            /*-----------------------------------------------------------------*/

            commonMethodobj.FillDist(ddlPEALDistrict); // bind dist in PEAL dropdown list for SINGLE WINDOW APPLICATION STATUS 
            FillProposalDetails();//SINGLE WINDOW APPLICATION STATUS 
                                  //FillProposalDistwiseDetails();//SINGLE WINDOW APPLICATION STATUS 
                                  //FillProposalDetailsIT();//SINGLE WINDOW APPLICATION STATUS 
                                  // FillProposalDetailsTourism();//SINGLE WINDOW APPLICATION STATUS 



            if (ConfigurationManager.AppSettings["SPMG"] == "ON")
            {
                // ViewSPMGServiceStateLevelDataDirect();// STATE PROJECT MONITORING GROUP
                ViewSPMGServiceDistrictLevelDataDirect(); //STATE PROJECT MONITORING GROUP
            }
                


            //DashboradCommon.MONTHBIND(ddlServcMonth); //bind both value 1 and  text January    Month value  on Normal platform
            commonMethodobj.FillDist(ddlServcDistrict); // bind District record in dropdown list 
            ViewServiceStatus();//DEPARTMENT WISE APPROVALS



            commonMethodobj.FillDist(ddlAPAADistrict);// bind dist in dropdown list  for IDCO POST ALLOTMENT APPLICATIONS 
            if (ConfigurationManager.AppSettings["IDCO"] == "ON")
            {
                InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
            }
               
            ViewApaaStatus();//IDCO POST ALLOTMENT APPLICATIONS



           // commonMethodobj.FillDist(ddlIncentiveDistrict);// bind dist in dropdown list  for INCENTIVE DETAILS
            IncentiveBind();//INCENTIVE DETAILS


            LandServiceBind();//LAND ALLOTMENT DETAILS


            ViewQueryService(); //QUERY MONITORING
            ViewQueryServicepeal();//QUERY MONITORING
            ViewQueryServiceIncentive();//QUERY MONITORING

            FillIncentiveApplicationDropdown();  // INCENTIVE APPLICATION DETAILS on portlet 

            commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
            BindGrievanceportlet();//GRIEVANCE STATUS

        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }





       // spLastUpdate.InnerText = DateTime.Now.ToString();
       // InsertAppa(ddlFinacialYear);
       //// FillMasterFinYr();
       // //BindDistrict();
       //// BindDistrictDIC();
       // FillFinYrPortlet(ddlserviceyear);
       // //DashboradCommon.YEARBIND(ddlserviceyear);
       // DashboradCommon.MONTHBIND(ddlServcMonth);
       // BindDistrictSrvc();

       // //BindDept();           
       // //BindDistrictPEAL();
       // DashboradCommon.MONTHBIND(ddlAppaMonth);
       // FillFinYrPortlet(ddlAppaYear);
       // // DashboradCommon.YEARBIND(ddlAppaYear);
       // DashboradCommon.YEARBIND(ddlPealYear);
       // DashboradCommon.YEARBIND(ddlIncentiveYear);
       // BindDistrictAPAA();
       // DistrictSelect();
       // //CurrentMonthSelect(ddlPealQuarter, ddlPealYear);
       // //ADDED BY NIBEDITA FOR INCENTIVE QUARTER SELECT BY DEFAULT
       // //CurrentMonthSelect(ddlIncentive, ddlIncentiveYear);
       // //ENDED BY NIBEDITA
       // FillProposalDetails();
       // IncentiveBind();
       // BindUnderEvalution();
       // //FillProposalDistwiseDetails();
       // ViewApaaStatus();
       // BindPealRecieved();
       // BindPealApproved();
       // ViewServiceStatus();
       // ViewServiceMaster();

       // FillFinYrPortlet(ddlyearquery);
       // ViewQueryService(); ///added by nibedita behera on 21-12-2017
       // ViewQueryServicepeal();
       // ViewQueryServiceIncentive();
       // FillFinYrPortlet(ddlLandFinYear);
       // LandServiceBind();
       // InsertAppa(ddlFinacialYear);
       // ViewSPMGServiceDistrictLevelDataDirect();//added by manoj kumar behera
       // ViewSPMGMasterData();//added by manoj kumar behera
       // FillFinYrPortletS(ddlgyear);

       // //BindDistrictGrievance();
       // BindMasterGrievanceportlet();
       // BindGrievanceportlet();
    }
    //#region"Added by suroj to view Proposal dtls"
    //private void FillProposalDistwiseDetails()
    //{
    //    try
    //    {
    //        objSWP = new SWPDashboard();
    //        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
    //        objSWP.strAction = "PDD";
    //        string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
    //        string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
    //        string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
    //        objSWP.intQuarter = Convert.ToInt16(PealQuareter);
    //        objSWP.intYearId = Convert.ToInt16(PealYear);
    //        objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
    //        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
    //        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
    //        if (objswpDashboardList.Count > 0)
    //        {




    //            lblPealdistApplied.Text = objswpDashboardList[0].strDistApplied;
    //            lblPealdistRejected.Text = objswpDashboardList[0].strDistRejected;
    //            lblPealdistApproved.Text = objswpDashboardList[0].strDistApproved;
    //            lblPealdistUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
    //            lblPealdistDeferred.Text = objswpDashboardList[0].strDistDeferred;
    //            //lblPealDeferred.Text = "0";
    //        }
    //        else
    //        {

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    finally
    //    {
    //        objSWP = null;
    //    }
    //}
    //#endregion

    #region Query
    private void ViewQueryService() //view QUERY MONITORING on normal platfrom
    {

        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    //if (Session["DeptId"].ToString() != null)
        //    //{
        //    //    if (ddldept.SelectedIndex > 0)
        //    //    {
        //    //        objDashboard.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
        //    //    }
        //    //    else
        //    //    {
        //    //        objDashboard.intDeptId = objDashboardBal.GetDepartment(Session["DeptId"].ToString());

        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    objDashboard.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
        //    //}
        //    objSWP.strAction = "QV";
        //    // objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    //objSWP.intDeptId = 0;//suggested by girija
        //    objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
        //    objSWP.Year = ddlyearquery.SelectedValue;
        //    List<SWPDashboard> objServiceStatus = objserviceDashboard.GetServicesQuery(objSWP).ToList();

        //    if (objServiceStatus.Count > 0)
        //    {
        //        spTotalQueryRaised.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
        //        spTotalQueryResponse.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
        //        spNotResponse.Text = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
        //        spAvgTimeTaken.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
        //        spTotalQueryPendings.Text = objServiceStatus[0].strTotalQueryPending.ToString();
        //    }
        //    else
        //    {
        //        spTotalQueryRaised.Text = "0";
        //        spTotalQueryResponse.Text = "0";
        //        spNotResponse.Text = "0";
        //        spAvgTimeTaken.Text = "0";
        //        spTotalQueryPendings.Text = "0";

        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //    objserviceDashboard = null;
        //}


        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewQueryService(ddlyearquery.SelectedValue , Convert.ToInt32(Session["Pealuserid"]));
            if (objServiceStatus.Count > 0)
            {
                spTotalQueryRaised.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
                spTotalQueryResponse.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
                spNotResponse.Text = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
                spAvgTimeTaken.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
                spTotalQueryPendings.Text = objServiceStatus[0].strTotalQueryPending.ToString();
            }
            else
            {
                spTotalQueryRaised.Text = "0";
                spTotalQueryResponse.Text = "0";
                spNotResponse.Text = "0";
                spAvgTimeTaken.Text = "0";
                spTotalQueryPendings.Text = "0";

            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void ViewQueryServicepeal()   //view QUERY MONITORING on normal platfrom
    {

        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    //objSWP.intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
        //    //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    objSWP.strAction = "QPV";
        //    objSWP.Year = ddlyearquery.SelectedValue;
        //    objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
        //    List<SWPDashboard> objServiceStatus = objserviceDashboard.GetPEALQuery(objSWP).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        spRaisedpeal.Text = objServiceStatus[0].strPEALQueryRaised.ToString();
        //        spResolvedpeal.Text = objServiceStatus[0].strPEALQueryResolved.ToString();
        //        spPendingpeal.Text = objServiceStatus[0].strPEALQueryPending.ToString();
        //        spResponcenotRecPeal.Text = objServiceStatus[0].strPEALQueryPendingPast.ToString();
        //        spAvgTimeQuerypeal.Text = objServiceStatus[0].strPEALQueryAvg.ToString();
        //    }
        //    else
        //    {
        //        spRaisedpeal.Text = "0";
        //        spResolvedpeal.Text = "0";
        //        spPendingpeal.Text = "0";
        //        spResponcenotRecPeal.Text = "0";
        //        spAvgTimeQuerypeal.Text = "0";

        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //    objserviceDashboard = null;
        //}


        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewQueryServicepeal(Convert.ToString(Session["Pealuserid"]), ddlyearquery.SelectedValue);
            if (objServiceStatus.Count > 0)
            {
                spRaisedpeal.Text = objServiceStatus[0].strPEALQueryRaised.ToString();
                spResolvedpeal.Text = objServiceStatus[0].strPEALQueryResolved.ToString();
                spPendingpeal.Text = objServiceStatus[0].strPEALQueryPending.ToString();
                spResponcenotRecPeal.Text = objServiceStatus[0].strPEALQueryPendingPast.ToString();
                spAvgTimeQuerypeal.Text = objServiceStatus[0].strPEALQueryAvg.ToString();
            }
            else
            {
                spRaisedpeal.Text = "0";
                spResolvedpeal.Text = "0";
                spPendingpeal.Text = "0";
                spResponcenotRecPeal.Text = "0";
                spAvgTimeQuerypeal.Text = "0";

            }

        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void ViewQueryServiceIncentive() //view QUERY MONITORING on normal platfrom
    {

        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    //objSWP.intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
        //    //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    objSWP.strAction = "IQV";
        //    objSWP.Year = ddlyearquery.SelectedValue;
        //    objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
        //    List<SWPDashboard> objServiceStatus = objserviceDashboard.GetiNCENTIVEQuery(objSWP).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        spRaisedIncentive.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
        //        spResolvedIncentive.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
        //        spPendingIncentive.Text = objServiceStatus[0].strTotalQueryPending.ToString();
        //        spResponcenotResIncentive.Text = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
        //        spAvgTimeQueryIncentive.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
        //    }
        //    else
        //    {
        //        spRaisedIncentive.Text = "0";
        //        spResolvedIncentive.Text = "0";
        //        spPendingIncentive.Text = "0";
        //        spResponcenotResIncentive.Text = "0";
        //        spAvgTimeQueryIncentive.Text = "0";

        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //    objserviceDashboard = null;
        //}



        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewQueryServiceIncentive(ddlyearquery.SelectedValue , Convert.ToInt32(Session["Pealuserid"]));
            if (objServiceStatus.Count > 0)
            {
                spRaisedIncentive.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
                spResolvedIncentive.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
                spPendingIncentive.Text = objServiceStatus[0].strTotalQueryPending.ToString();
                spResponcenotResIncentive.Text = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
                spAvgTimeQueryIncentive.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
            }
            else
            {
                spRaisedIncentive.Text = "0";
                spResolvedIncentive.Text = "0";
                spPendingIncentive.Text = "0";
                spResponcenotResIncentive.Text = "0";
                spAvgTimeQueryIncentive.Text = "0";

            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        try
        {
            ViewQueryService();
            ViewQueryServicepeal();
            ViewQueryServiceIncentive();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        
    }
    #endregion
    #region Land Service Portlet

    private void LandServiceBind()  // view LAND ALLOTMENT DETAILS on normal platfrom
    {
        //try
        //{
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "LANDV";
        //    objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
        //    objSWP.intDeptId = 0;
        //    objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
        //    objSWP.Year = ddlLandFinYear.SelectedValue;
        //    objswpDashboardList = objserviceDashboard.GETLandDetails(objSWP).ToList();
        //    spLandAssesment.InnerText = objswpDashboardList[0].LandAssessment;
        //    spLandAllotByIDCO.InnerText = objswpDashboardList[0].ApplnLandAllotedByIDCO;
        //    spPropIDCO.InnerText = objswpDashboardList[0].PropNoForLand;
        //    spLandAllot.InnerText = objswpDashboardList[0].AreaAllotLand;
        //    spORTPSALAnd.InnerText = objswpDashboardList[0].ApplnLandORTPS;
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //}


        try
        {
            List<SWPDashboard> objswpDashboardList = commonMethodobj.LandServiceBind(ddlLandFinYear.SelectedValue, Convert.ToInt32(Session["Pealuserid"]));
            if(objswpDashboardList.Count > 0)
            {
                spLandAssesment.InnerText = objswpDashboardList[0].LandAssessment;
                spLandAllotByIDCO.InnerText = objswpDashboardList[0].ApplnLandAllotedByIDCO;
                spPropIDCO.InnerText = objswpDashboardList[0].PropNoForLand;
                spLandAllot.InnerText = objswpDashboardList[0].AreaAllotLand;
                spORTPSALAnd.InnerText = objswpDashboardList[0].ApplnLandORTPS;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void btnLandSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            LandServiceBind();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        
    }

    #endregion


    //private void BindDistrictGrievance()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlgdist.DataSource = objProjList;
    //    ddlgdist.DataTextField = "vchDistName";
    //    ddlgdist.DataValueField = "intDistId";
    //    ddlgdist.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlgdist.Items.Insert(0, list);
    //    ddlgdist.SelectedValue = Session["Pealuserid"].ToString();
    //    ddlgdist.Enabled = false;
    //}

    private void BindMasterGrievanceportlet()
    {
        //try
        //{
        //    DataTable Gdt = new DataTable();
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    objSWP.strAction = "DGD";
        //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    objSWP.strFinacialYear = ddlFinacialYear.SelectedItem.Text;
        //    if (ddlgdist.SelectedIndex > 0)
        //    {
        //        objSWP.intDistrictId = Convert.ToInt32(ddlgdist.SelectedValue);
        //    }
        //    else
        //    {
        //        objSWP.intDistrictId = 0;
        //    }
        //    Gdt = objserviceDashboard.getDepartmentGrievance(objSWP);
        //    if (Gdt.Rows.Count > 0)
        //    {
        //        //Spangresolved.InnerText = Gdt.Rows[0]["RESOLVED"].ToString();
        //        Spangpending.InnerText = Gdt.Rows[0]["PENDING"].ToString();
        //    }
        //    else
        //    {
        //        //Spangresolved.InnerText = "";
        //        Spangpending.InnerText = "";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //}


        try
        {
            DataTable Gdt = new DataTable();
            Gdt = commonMethodobj.BindMasterGrievanceportlet(Convert.ToInt32(Session["Userid"]), ddlFinacialYear.SelectedValue , Convert.ToInt32(ddlgdist.SelectedValue));
            if (Gdt.Rows.Count > 0)
            {
                Spangpending.InnerText = Gdt.Rows[0]["PENDING"].ToString();
            }
            else
            {
                Spangpending.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
            // Util.LogError(ex, "Dashboard");
        }
    }

    private void BindGrievanceportlet()  //Method for  GRIEVANCE STATUS on normal platfrom
    {
        //try
        //{
        //    DataTable Gdt = new DataTable();
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    objSWP.strAction = "DGD";
        //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    objSWP.strFinacialYear = ddlgyear.SelectedItem.Text;
        //    if (ddlgdist.SelectedIndex > 0)
        //    {
        //        objSWP.intDistrictId = Convert.ToInt32(ddlgdist.SelectedValue);
        //    }
        //    else
        //    {
        //        objSWP.intDistrictId = 0;
        //    }
        //    Gdt = objserviceDashboard.getDepartmentGrievance(objSWP);
        //    if (Gdt.Rows.Count > 0)
        //    {
        //        lblGapplied.Text = Gdt.Rows[0]["APPLIED"].ToString();
        //        lblGrsolved.Text = Gdt.Rows[0]["RESOLVED"].ToString();
        //        lblGpending.Text = Gdt.Rows[0]["PENDING"].ToString();
        //        lblGrejected.Text = Gdt.Rows[0]["REJECTED"].ToString();
        //    }
        //    else
        //    {
        //        lblGapplied.Text = "0";
        //        lblGrsolved.Text = "0";
        //        lblGpending.Text = "0";
        //        lblGrejected.Text = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //}

        try
        {
            int intDistrictId = 0;
            if (ddlgdist.SelectedIndex > 0)
            {
                intDistrictId = Convert.ToInt32(ddlgdist.SelectedValue);
            }
            else
            {
                intDistrictId = 0;
            }
            DataTable Gdt = commonMethodobj.BindGrievanceportlet(Convert.ToInt32(UserId), ddlgyear.SelectedItem.Text, intDistrictId);
            if (Gdt.Rows.Count > 0)
            {
                lblGapplied.Text = Gdt.Rows[0]["APPLIED"].ToString();
                lblGrsolved.Text = Gdt.Rows[0]["RESOLVED"].ToString();
                lblGpending.Text = Gdt.Rows[0]["PENDING"].ToString();
                lblGrejected.Text = Gdt.Rows[0]["REJECTED"].ToString();
            }
            else
            {
                lblGapplied.Text = "0";
                lblGrsolved.Text = "0";
                lblGpending.Text = "0";
                lblGrejected.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGSearch_Click(object sender, EventArgs e)
    {
        BindGrievanceportlet();
    }
}