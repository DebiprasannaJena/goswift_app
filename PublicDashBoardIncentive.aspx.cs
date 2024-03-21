using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using BusinessLogicLayer.Dashboard;

public partial class PublicDashBoardIncentive : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    MisReportServices objserviceDashboard = new MisReportServices();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");

                string strFromDate = string.Empty, strTodate = string.Empty;
                GetDefaultFromAndToDate(out strFromDate, out strTodate);
                txtFromDate.Text = strFromDate;
                txtToDate.Text = strTodate;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues('" + strFromDate + "','" + strTodate + "');</script>", false);
                BindDistrict();
                BindGridView();              
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PublicDashboardIncentiveMIS");
            }
        }
    }

    #region FunctionUsed

    private void GetDefaultFromAndToDate(out string strFromDate, out string strToDate)
    {
        try
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
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindDistrict()
    {
        try
        {            
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlIncentiveDistrict.DataSource = objProjList;
            ddlIncentiveDistrict.DataTextField = "vchDistName";
            ddlIncentiveDistrict.DataValueField = "intDistId";
            ddlIncentiveDistrict.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlIncentiveDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindGridView()
    {
        try
        {
            lblSearchDetails.Text = string.Empty;
            grdIncentive.DataSource = null;
            grdIncentive.DataBind();

            string strFromDate = string.Empty, strTodate = string.Empty;
            GetDefaultFromAndToDate(out strFromDate, out strTodate);
            RptSearch objSearch = new RptSearch()
            {
                strActionCode = "I",
                intDistrictId = Convert.ToInt32(ddlIncentiveDistrict.SelectedValue),
                intPageSize = 0,
                intIntPageIndex = 0,
                strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
                strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strTodate : txtToDate.Text.Trim()

            };

            objserviceDashboard = new MisReportServices();      
            List<IncentiveMisReport> objswpDashboardList = objserviceDashboard.GetIncentiveMISReportDtls(objSearch).ToList();

            grdIncentive.DataSource = objswpDashboardList;
            grdIncentive.DataBind();
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridView();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PublicDashboardIncentiveMIS");
        }
    }
}