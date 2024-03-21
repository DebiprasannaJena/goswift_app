using BusinessLogicLayer.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class PublicDashBoardQuery : System.Web.UI.Page
{
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    SWPDashboard objSWP = new SWPDashboard();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Attributes.Add("readonly", "readonly"); //Added by Bhagyashree Das on Dt. 03-Feb-2021
            txtToDate.Attributes.Add("readonly", "readonly"); //Added by Bhagyashree Das on Dt. 03-Feb-2021

            string strFromDate = string.Empty, strTodate = string.Empty;
            GetDefaultFromAndToDate(out strFromDate, out strTodate);
            txtFromDate.Text = strFromDate;
            txtToDate.Text = strTodate;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues('" + strFromDate + "','" + strTodate + "');</script>", false);           
            ViewQueryService();
            ViewQueryServicePeal();
            ViewQueryServiceIncentive();          
        }
    }

    #region FunctionUsed
    private void GetDefaultFromAndToDate(out string strFromDate, out string strToDate) //Added by Bhagyashree Das on Dt. 03-Feb-2021
    {
        strFromDate = string.Empty;
        strToDate = string.Empty;
        int intMonth = DateTime.Today.Month;
        if (intMonth == 1)
        {
            strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        else
        {
            strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString().Substring(0, 3) + "-" + (DateTime.Today.Year).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }
   
    private void ViewQueryService()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        string strFromDate = string.Empty, strTodate = string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        try
        {
            objSWP.strAction = "QV";         
            objSWP.strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(); //Added by Bhagyashree Das on Dt. 03-Feb-2021
            objSWP.strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strTodate : txtToDate.Text.Trim(); //Added by Bhagyashree Das on Dt. 03-Feb-2021
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetServicesQuery(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                spTimelineService.Text = "15"; //Added by Bhagyashree Das on Dated: 21-Dec-2020
                spQueryReceivedService.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
                spQueryRespondedService.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
                spQueryAvgTimeService.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
                spQueryMedianTimeService.Text = objServiceStatus[0].strMedianRaiseQuery.ToString();
                spQueryMinTimeService.Text = objServiceStatus[0].strMinRaiseQuery.ToString();
                spQueryMaxTimeService.Text = objServiceStatus[0].strMaxRaiseQuery.ToString();
            }
            else
            {
                spTimelineService.Text = "0";
                spQueryReceivedService.Text = "0";
                spQueryRespondedService.Text = "0";
                spQueryAvgTimeService.Text = "0";
                spQueryMedianTimeService.Text = "0";
                spQueryMinTimeService.Text = "0";
                spQueryMaxTimeService.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    private void ViewQueryServicePeal()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        string strFromDate = string.Empty, strTodate = string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        try
        {
            objSWP.strAction = "QPV";           
            objSWP.strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(); //Added by Bhagyashree Das on Dt. 03-Feb-2021
            objSWP.strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strTodate : txtToDate.Text.Trim(); //Added by Bhagyashree Das on Dt. 03-Feb-2021
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetPEALQuery(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                spTimelinePeal.Text = "15"; //Added by Bhagyashree Das on Dated: 21-Dec-2020
                spQueryReceivedPeal.Text = objServiceStatus[0].strPEALQueryRaised.ToString();
                spQueryRespondedPeal.Text = objServiceStatus[0].strPEALQueryResolved.ToString();
                spQueryAvgTimePeal.Text = objServiceStatus[0].strPEALQueryAvg.ToString();
                spQueryMedianTimePeal.Text = objServiceStatus[0].strMedianRaiseQuery.ToString();
                spQueryMinTimePeal.Text = objServiceStatus[0].strMinRaiseQuery.ToString();
                spQueryMaxTimePeal.Text = objServiceStatus[0].strMaxRaiseQuery.ToString();
            }
            else
            {
                spTimelinePeal.Text = "0";
                spQueryReceivedPeal.Text = "0";
                spQueryRespondedPeal.Text = "0";
                spQueryAvgTimePeal.Text = "0";
                spQueryMedianTimePeal.Text = "0";
                spQueryMinTimePeal.Text = "0";
                spQueryMaxTimePeal.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    private void ViewQueryServiceIncentive()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        string strFromDate = string.Empty, strTodate = string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        try
        {
            objSWP.strAction = "IQV";          
            objSWP.strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(); //Added by Bhagyashree Das on Dt. 03-Feb-2021
            objSWP.strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strTodate : txtToDate.Text.Trim(); //Added by Bhagyashree Das on Dt. 03-Feb-2021
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetiNCENTIVEQuery(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                spTimelineIncentive.Text = "15"; //Added by Bhagyashree Das on Dated: 21-Dec-2020
                spQueryReceivedIncentive.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
                spQueryRespondedIncentive.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
                spQueryAvgTimeIncentive.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
                spQueryMedianTimeIncentive.Text = objServiceStatus[0].strMedianRaiseQuery.ToString();
                spQueryMinTimeIncentive.Text = objServiceStatus[0].strMinRaiseQuery.ToString();
                spQueryMaxTimeIncentive.Text = objServiceStatus[0].strMaxRaiseQuery.ToString();
            }
            else
            {
                spTimelineIncentive.Text = "0";
                spQueryReceivedIncentive.Text = "0";
                spQueryRespondedIncentive.Text = "0";
                spQueryAvgTimeIncentive.Text = "0";
                spQueryMedianTimeIncentive.Text = "0";
                spQueryMinTimeIncentive.Text = "0";
                spQueryMaxTimeIncentive.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ViewQueryService();
            ViewQueryServicePeal();
            ViewQueryServiceIncentive();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PublicQueryDashboard");
        }
    }
}