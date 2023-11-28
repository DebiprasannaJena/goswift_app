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
using BusinessLogicLayer.Service;
using EntityLayer.Service;
using System.Globalization;
using System.Data.SqlClient;
using System.Web.UI;
#endregion

public partial class Portal_Dashboard_PS__Finance_Dashboard : SessionCheck
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
        if (Session["desId"].ToString() == "98")
        {

            if (!IsPostBack)
            {

                spLastUpdate.InnerText = DateTime.Now.ToString();

                /*-----------------------------------------------------------------*/
                ///Fill Dropdownlist for Financial Year
                /*-----------------------------------------------------------------*/
                commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlCSRYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYearWithYear(ddlspmgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform
                commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
                commonMethodobj.FillFinancialYear(ddlPealYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlYearInvest); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlYearEmployement); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value 1 and  text January    Month value  on Normal platform
                DashboradCommon.MONTHBIND(ddlCICGMonth); //bind both value 1 and  text January    Month value  on Normal platform
                DashboradCommon.YEARBIND(ddlYearCICG);  //bind both value 2016 and  text 2016    Year value  on Normal platform
                commonMethodobj.FillFinancialYear(ddlIncentiveYear); //bind bothe value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                //commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                //commonMethodobj.FillFinancialYear(ddlLandFinYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYearWithYear(ddlgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform


                /*-----------------------------------------------------------------*/
                //// Master Tracker
                /*-----------------------------------------------------------------*/
                BindUnderEvalution();// Single Window Application Status
                BindPealRecieved(); //Single Window Application Status
                BindPealApproved();//Single Window Application Status
                IncentiveMasterBind(); // Incentive Details
                FillTrackerInvestment(); //Investment
                BindMasterGrievanceportlet();//Grievance Status
                if (ConfigurationManager.AppSettings["CSR"] == "ON")
                {
                    InsertCSRMaster(ddlFinacialYear);//CSR Spend
                }
                   
                FillTrackerEmployement(); //Employment



                /*-----------------------------------------------------------------*/
                ////Portlet Section
                /*-----------------------------------------------------------------*/
                commonMethodobj.FillDist(ddlPEALDistrict); // bind dist in PEAL dropdown list for SINGLE WINDOW APPLICATION STATUS 
                FillProposalDetails();//SINGLE WINDOW APPLICATION STATUS 
                FillProposalDistwiseDetails();//SINGLE WINDOW APPLICATION STATUS 
                FillProposalDetailsIT();//SINGLE WINDOW APPLICATION STATUS 
                FillProposalDetailsTourism();//SINGLE WINDOW APPLICATION STATUS 

                commonMethodobj.FillDist(chkYearwise); //Bind dist in checkbox list for YEAR WISE INVESTMENT
                FillProposalCapital();//YEAR WISE INVESTMENT


                // BindDistrictEmployeeMentYearwise();
                commonMethodobj.FillDist(CheckBoxList1); //Bind dist in checkbox list for YEAR WISE EMPLOYMENT
                FillProposalEmployement();//YEAR WISE EMPLOYMENT


                commonMethodobj.FillDist(chkCSRDistrct);// bind dist in csr checkbox list
                if (ConfigurationManager.AppSettings["CSR"] == "ON")
                {
                    CSRPortletSatus(); //CSR ACTIVITIES
                }
                   

                commonMethodobj.FillDist(ddlIncentiveDistrict);// bind dist in dropdown list  for INCENTIVE DETAILS
                IncentiveBind();//INCENTIVE DETAILS

               


                commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
                BindGrievanceportlet();//GRIEVANCE STATUS


            }
        }
        else
        {
            Response.Redirect("~/Portal/Default.aspx");
        }
    }
    #endregion

    #region CSR Portlet
    private void InsertCSRStatus(HiddenField hdnCSRDistrct, DropDownList ddlCSRYear)
    {
        string finalquery = string.Empty;
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = "1";
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string District = "";
        if (hdnCSRDistrct.Value != "")
        {
            District = hdnCSRDistrct.Value.ToString();
        }
        else
        {
            District = "0";
        }
        //string District = string.IsNullOrEmpty(ddlDistrict.SelectedValue) ? default(string) : ddlDistrict.SelectedValue.ToString();
        //string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlCSRYear.SelectedItem.Text) ? default(string) : ddlCSRYear.SelectedItem.Text;
        if (Year == "--Select--")
        {
            Year = "0";
        }
        else
        {
            Year = ddlCSRYear.SelectedItem.Text;
        }
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";
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
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT ADMINID FROM T_CSR_SpentDtls_Admin WHERE CONVERT(DATE,dtmCreatedon)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_CSR_SpentDtls_Admin SET adminid=" + Convert.ToInt32(Session["Userid"]) + ",TotalAmount=" + finalOut[2].ToString() + ",TotalProject=" + DynTable.Rows[0]["TotalProject"].ToString() + "," +
                              "dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',vchYear='" + Year + "',intDistrictId='" + District + "' WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
                    }
                    else
                    {
                        if (finalOut[2].ToString() != "")
                        {
                            ViewState["finalvalue"] = finalOut[2].ToString(); ;
                        }
                        else
                        { ViewState["finalvalue"] = "0"; }
                        finalquery = "INSERT INTO T_CSR_SpentDtls_Admin(adminid,TotalAmount,TotalProject,dtmCreatedon,vchYear,intDistrictId)" +
                    "VALUES(" + Convert.ToInt32(Session["Userid"]) + "," + ViewState["finalvalue"] + "," + DynTable.Rows[0]["TotalProject"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "','" + Year + "'," + District + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch
        {
        }
    }
   
    protected void btnCSRStatus_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRStatus(hdnCSRDistrct, ddlCSRYear);
            CSRPortletSatus();
        }
            
    }
    private void CSRPortletSatus() //view  CSR ACTIVITIES on normal platfrom
    {
        //////// Dispaly CSR data directly from service implemented by Sushant Jena on Dt. 29-May-2018
        try
        {
            string strYear = "0";
            string strDistrict = "0";

            if (hdnCSRDistrct.Value != "")
            {
                strDistrict = hdnCSRDistrct.Value;
            }

            if (ddlCSRYear.SelectedItem.Text != "--Select--")
            {
                strYear = ddlCSRYear.SelectedItem.Text;
            }

            DataTable DynTable = commonMethodobj.CSRPortletSatus(strYear, strDistrict);
            if (DynTable.Rows.Count > 0)
            {
                string output = DynTable.Rows[0]["GetTotalAmountResult"].ToString();
                string[] finalOut = output.Split(':');

                SPProject.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalProject"]);
                SPRecommendedCouncil.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalRecommendProject"]);
                decimal TotAmount = Convert.ToDecimal(DynTable.Rows[0]["TotalAmount"]) / 100;
                SPSpent.InnerHtml = Convert.ToString(Math.Round(TotAmount));
            }
            else
            {
                SPProject.InnerHtml = "0";
                SPRecommendedCouncil.InnerHtml = "0";
                SPSpent.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    //private void FillFinYr()
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();

    //    objSWP.strAction = "FY";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddlCSRYear.DataSource = objCICGFINYear;
    //    ddlCSRYear.DataTextField = "Year";
    //    ddlCSRYear.DataValueField = "Year";
    //    ddlCSRYear.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlCSRYear.Items.Insert(0, list);

    //}
    
    private void InsertCSRMaster(DropDownList ddlyear) // Method for binding CSR Spend on master tracker section
    {
        try
        {
            string Year = string.IsNullOrEmpty(ddlyear.SelectedItem.Text) ? default(string) : ddlyear.SelectedItem.Text;
            if (Year == "--Select--")
            {
                Year = "0";
            }
            else
            {
                Year = ddlyear.SelectedItem.Text;
            }

            DataTable DynTable = new DataTable();
            DynTable = commonMethodobj.InsertCSRMaster(Year);
            decimal TotAmount = Convert.ToDecimal(DynTable.Rows[0]["TotalAmount"]) / 100; //// Added by Sushant Jena on Dt. 28-May-2018 for converting lakhs to Crore
            SPNetSpent.InnerText = Math.Round(TotAmount).ToString();
        }
        catch (Exception ex)
        {
            SPNetSpent.InnerHtml = "0";
            Util.LogError(ex, "Dashboard");
        }
    }
    #endregion

    #region Service Portlet
    
   
    
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindService();
        commonMethodobj.FillService(ddldept, ddlService);// bind service record in  dropdown list
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowSearchpanel()", true);

    }
    protected void btnStatusOfApproval_Click(object sender, EventArgs e)
    {
       // ViewServiceStatus();
    }
    
    #endregion

    #region PEAL Portlet
    private void BindUnderEvalution()  // Method for binding data from  Peal pending data on master Tracker dashbord  .
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();

            objswpDashboardList = commonMethodobj.BindUnderEvalution(ddlFinacialYear.SelectedValue);
            lblTrackerEvalution.InnerText = objswpDashboardList[0].strUnderEvaltion;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    //[WebMethod]
    //public static List<SWPDashboard> EmployeementPealDetailsBind(string Pealyear, string Pealdistrictdtls)
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PED";
    //        //objDashboard.intYearId = Convert.ToInt16(Pealyear);
    //        objDashboard.strFinacialYear = Pealyear.ToString();
    //        if (Pealdistrictdtls != "")
    //        {
    //            objDashboard.strDistrictDtl = Pealdistrictdtls.ToString();
    //        }
    //        else
    //        {
    //            objDashboard.strDistrictDtl = "";
    //        }
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}
    ////ENDED BY SUROJ
    ////MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    //[WebMethod]
    //public static List<SWPDashboard> EmployeementCapitalPealDetailsBind(string Pealyear, string Pealdistrictdtls)
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PID";
    //        objDashboard.strFinacialYear = Pealyear.ToString();
    //        // objDashboard.intYearId = Convert.ToInt16(Pealyear);
    //        //objDashboard.intDistrictid = Convert.ToInt16(Pealdistrict);
    //        if (Pealdistrictdtls != "")
    //        {
    //            objDashboard.strDistrictDtl = Pealdistrictdtls.ToString();
    //        }
    //        else
    //        {
    //            objDashboard.strDistrictDtl = "";
    //        }
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}
    ////ENDED BY SUROJ
    //[WebMethod]
    //public static List<SWPDashboard> PealDetailsBind()
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PPD";
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}


    protected void btnPealEmployement_Click(object sender, EventArgs e)
    {
        FillProposalEmployement();
    }
    protected void btnPealcapitalsubmit_Click(object sender, EventArgs e)
    {
        FillProposalCapital();
    }

    protected void btnPealsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            FillProposalDetails();
            FillProposalDistwiseDetails();
            //ADDED BY NIBEDITA BEHERA FOR SPECIAL SINGLW WINDOW ON 01-01-2018
            FillProposalDetailsTourism();
            FillProposalDetailsIT();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }
    #region"Added by suroj to view Proposal dtls"
    private void FillProposalDetails()  // Mothod for view data for Proposal details   on noramal platform
    {
        try
        {

            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();


            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDetails(Convert.ToInt16(PealQuareter), ddlPealYear.SelectedValue.ToString(), Convert.ToInt32(PealDistrict), Convert.ToInt32(UserId));
            if (objswpDashboardList.Count > 0)
            {
                lblPealApplied.Text = objswpDashboardList[0].strApplied;
                lblPealRejected.Text = objswpDashboardList[0].strRejected;
                lblPealApproved.Text = objswpDashboardList[0].strApproved;
                //lblPealEmployeemnet.Text = objswpDashboardList[0].strTotNoCapitalPropApproved;
                lblPealDeferred.Text = objswpDashboardList[0].strDeferred;
                //added by nibedita
                lblPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
                lblPealQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                Lbl_Peal_ORTPSA_State.Text = objswpDashboardList[0].strPealOrtpsaCrossedState; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                lblPealApplied.Text = "0";
                lblPealRejected.Text = "0";
                lblPealApproved.Text = "0";
                lblPealQueryRaise.Text = "0";
                lblPealUnderEvalution.Text = "0";
                lblPealDeferred.Text = "0";
                Lbl_Peal_ORTPSA_State.Text = "0"; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    #endregion
    #region"Added by suroj to view Proposal dtls"
    //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    private void FillProposalCapital()  // view year wise investment on normal platfrom 
    {
        try
        {
            string PealYear = string.IsNullOrEmpty(ddlYearInvest.SelectedValue) ? default(string) : ddlYearInvest.SelectedValue.ToString();

            lblYearInvestment.Text = ddlYearInvest.SelectedItem.Text;

            string strDistrictDtl = string.Empty;
            if (hddnValue1.Value != "")
            {
                strDistrictDtl = hddnValue1.Value.ToString();
            }
            else
            {
                strDistrictDtl = "";
            }


            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalCapital(Convert.ToInt32(UserId), strDistrictDtl, PealYear);
            if (objswpDashboardList.Count > 0)
            {
                //decimal EmpInvestment = Convert.ToDecimal((Convert.ToDouble(objswpDashboardList[0].strTotCapitalPropApproved) / 100));
                lblEmpInvestment.Text = objswpDashboardList[0].strTotCapitalPropApproved;
                //lblEmpInvestment.Text = EmpInvestment.ToString("0.##");
            }
            else
            {
                lblEmpInvestment.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    //ENDED BY SUROJ
    #endregion

    #region"Added by suroj to view Proposal dtls"
    //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    private void FillProposalEmployement() // view year wise employeement on normal platfrom
    {
        try
        {


            string PealYear = string.IsNullOrEmpty(ddlYearEmployement.SelectedValue) ? default(string) : ddlYearEmployement.SelectedValue.ToString();
            lblYearEmployement.Text = ddlYearEmployement.SelectedItem.Text;

            string strDistrictDtl = string.Empty;
            if (hddnValue.Value != "")
            {
                strDistrictDtl = hddnValue.Value.ToString();
            }
            else
            {
                strDistrictDtl = "";
            }

            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalEmployement(Convert.ToInt32(UserId), strDistrictDtl, PealYear);
            if (objswpDashboardList.Count > 0)
            {
                lblPealEmployeemnet.Text = objswpDashboardList[0].strTotNoCapitalPropApproved;
            }
            else
            {
                lblPealEmployeemnet.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    //ENDED BY SUROJ
    #endregion

    private void FillTrackerEmployement() // Mothod for bind Employment on master tracker
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();

            objswpDashboardList = commonMethodobj.FillTrackerEmployement(Convert.ToInt32(UserId), ddlFinacialYear.SelectedValue);
            SPEmpGen.InnerHtml = objswpDashboardList[0].strTotNoCapitalPropApproved;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void FillTrackerInvestment()  // Mothod for bind Investment on master tracker
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillTrackerInvestment(Convert.ToInt32(UserId), ddlFinacialYear.SelectedValue);
            lblmastinv.Text = objswpDashboardList[0].strTotCapitalPropApproved.ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void BindPealRecieved()  // Method for binding data from  Peal recived data on master Tracker dashbord  .
    {
        try
        {

            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.BindPealRecieved(ddlFinacialYear.SelectedValue);
            lblProposalRecieved.InnerText = objswpDashboardList[0].strPealRecived;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void BindPealApproved() // Method for binding data from  Peal Approved data on master Tracker dashbord  .
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.BindPealApproved(ddlFinacialYear.SelectedValue);
            lblProposalapproved.InnerText = objswpDashboardList[0].strPealApproved;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }


    //modified by suroj on 24-10-2017
    
    
    
    #region"Added by suroj to view Proposal dtls"
    private void FillProposalDistwiseDetails() // for view proposal dist  wise on normal platform
    {
        try
        {

            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();

            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDistwiseDetails(Convert.ToInt16(PealQuareter), ddlPealYear.SelectedValue.ToString(), Convert.ToInt32(PealDistrict), Convert.ToInt32(UserId));
            if (objswpDashboardList.Count > 0)
            {
                lblPealdistApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealdistRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealdistApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealdistUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealdistQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealdistDeferred.Text = objswpDashboardList[0].strDistDeferred;
                //lblPealDeferred.Text = "0";

                Lbl_Peal_ORTPSA_Dist.Text = objswpDashboardList[0].strPealOrtpsaCrossedDist; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                Lbl_Peal_ORTPSA_Dist.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    #endregion

    private void FillProposalDetailsTourism() // view proposal detail of Tourism department on normal platform
    {
        try
        {

            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            int intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdTOURISM"]);
            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDetailsTourism(PealYear, Convert.ToInt32(PealDistrict), intSecId);
            if (objswpDashboardList.Count > 0)
            {
                lblPealTourismApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealTourismRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealTourismApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealTourismUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealTourismQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealTourismDeferred.Text = objswpDashboardList[0].strDistDeferred;

                Lbl_Peal_ORTPSA_Tourism.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                lblPealTourismApplied.Text = "0";
                lblPealTourismRejected.Text = "0";
                lblPealTourismApproved.Text = "0";
                lblPealTourismUnderEvalution.Text = "0";
                lblPealTourismDeferred.Text = "0";
                lblPealTourismQueryRaise.Text = "0";

                Lbl_Peal_ORTPSA_Tourism.Text = "0"; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void FillProposalDetailsIT()  // view proposal detail of IT department on normal platform
    {
        try
        {

            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();

            int intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdIT"]);

            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDetailsIT(PealYear, Convert.ToInt32(PealDistrict), intSecId);
            if (objswpDashboardList.Count > 0)
            {
                lblPealITApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealITRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealITApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealITUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealITQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealITDeferred.Text = objswpDashboardList[0].strDistDeferred;
                Lbl_Peal_ORTPSA_IT.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                lblPealITApplied.Text = "0";
                lblPealITRejected.Text = "0";
                lblPealITApproved.Text = "0";
                lblPealITUnderEvalution.Text = "0";
                lblPealITDeferred.Text = "0";
                lblPealITQueryRaise.Text = "0";
                Lbl_Peal_ORTPSA_IT.Text = "0"; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    #endregion

    #region MEMBER FUNCTION
    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

   

    //modified by suroj on 24-10-2017

    
    

    #endregion

    #region Incentive Portlet
    #region"ADDED BY SUROJ FOR INCENTIVE PORTLET BIND"
    private void IncentiveBind()  //view INCENTIVE DETAILS on normal platfrom
    {
        try
        {

            string IncentiveDistrict = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
            string IncentiveYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
            List<SWPDashboard> objswpDashboardList = commonMethodobj.IncentiveBind(IncentiveYear, Convert.ToInt16(IncentiveDistrict));
            lblIncApplied.Text = objswpDashboardList[0].INCAPLLIED;
            lblIncsanctioed.Text = objswpDashboardList[0].INCSANCTIONED;
            lblIncpending.Text = objswpDashboardList[0].INCPENDING;
            lblIncrejected.Text = objswpDashboardList[0].INCREJECTED;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    #endregion
    private void IncentiveMasterBind()  // Method for  bind Incetive data 
    {
        try
        {

            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.IncentiveMasterBind(ddlFinacialYear.SelectedValue);
            spIncPending.InnerText = objswpDashboardList[0].INCPENDING;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    protected void btnIncentiveSubmit_Click(object sender, EventArgs e)
    {
        IncentiveBind();
    }
   
    #endregion
    protected void ddlFinacialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUnderEvalution();// Single Window Application Status
        BindPealRecieved(); //Single Window Application Status
        BindPealApproved();//Single Window Application Status
        IncentiveMasterBind(); // Incentive Details
        FillTrackerInvestment(); //Investment
        BindMasterGrievanceportlet();//Grievance Status
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRMaster(ddlFinacialYear);//CSR Spend
        }
           
        FillTrackerEmployement(); //Employment
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        spLastUpdate.InnerText = DateTime.Now.ToString();

        /*-----------------------------------------------------------------*/
        ///Fill Dropdownlist for Financial Year
        /*-----------------------------------------------------------------*/
        commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlCSRYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYearWithYear(ddlspmgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform
        commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
        commonMethodobj.FillFinancialYear(ddlPealYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlYearInvest); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlYearEmployement); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
        DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value 1 and  text January    Month value  on Normal platform
        DashboradCommon.MONTHBIND(ddlCICGMonth); //bind both value 1 and  text January    Month value  on Normal platform
        DashboradCommon.YEARBIND(ddlYearCICG);  //bind both value 2016 and  text 2016    Year value  on Normal platform
        commonMethodobj.FillFinancialYear(ddlIncentiveYear); //bind bothe value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
      //commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
      //commonMethodobj.FillFinancialYear(ddlLandFinYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYearWithYear(ddlgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform


        /*-----------------------------------------------------------------*/
        //// Master Tracker
        /*-----------------------------------------------------------------*/
        BindUnderEvalution();// Single Window Application Status
        BindPealRecieved(); //Single Window Application Status
        BindPealApproved();//Single Window Application Status
        IncentiveMasterBind(); // Incentive Details
        FillTrackerInvestment(); //Investment
        BindMasterGrievanceportlet();//Grievance Status
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRMaster(ddlFinacialYear);//CSR Spend
        }
           
        FillTrackerEmployement(); //Employment



        /*-----------------------------------------------------------------*/
        ////Portlet Section
        /*-----------------------------------------------------------------*/
        commonMethodobj.FillDist(ddlPEALDistrict); // bind dist in PEAL dropdown list for SINGLE WINDOW APPLICATION STATUS 
        FillProposalDetails();//SINGLE WINDOW APPLICATION STATUS 
        FillProposalDistwiseDetails();//SINGLE WINDOW APPLICATION STATUS 
        FillProposalDetailsIT();//SINGLE WINDOW APPLICATION STATUS 
        FillProposalDetailsTourism();//SINGLE WINDOW APPLICATION STATUS 

        commonMethodobj.FillDist(chkYearwise); //Bind dist in checkbox list for YEAR WISE INVESTMENT
        FillProposalCapital();//YEAR WISE INVESTMENT


        // BindDistrictEmployeeMentYearwise();
        commonMethodobj.FillDist(CheckBoxList1); //Bind dist in checkbox list for YEAR WISE EMPLOYMENT
        FillProposalEmployement();//YEAR WISE EMPLOYMENT


        commonMethodobj.FillDist(chkCSRDistrct);// bind dist in csr checkbox list
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            CSRPortletSatus(); //CSR ACTIVITIES
        }
           

        commonMethodobj.FillDist(ddlIncentiveDistrict);// bind dist in dropdown list  for INCENTIVE DETAILS
        IncentiveBind();//INCENTIVE DETAILS




        commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
        BindGrievanceportlet();//GRIEVANCE STATUS



    }

    private void BindMasterGrievanceportlet() // Mothod for bind Grievance Status on master tracker
    {
        try
        {
            DataTable Gdt = new DataTable();
            Gdt = commonMethodobj.BindMasterGrievanceportlet(Convert.ToInt32(Session["Userid"]), ddlFinacialYear.SelectedItem.Text);
            if (Gdt.Rows.Count > 0)
            {
                Spangpending.InnerText = Gdt.Rows[0]["PENDING"].ToString();
            }
            else
            {
                Spangpending.InnerText = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void BindGrievanceportlet() //Method for  GRIEVANCE STATUS on normal platfrom
    {
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
            Util.LogError(ex, "Dashboard");
        }
    }
    protected void btnGSearch_Click(object sender, EventArgs e)
    {
        BindGrievanceportlet();
    }
}