using BusinessLogicLayer.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InvesterDashboardNonIndustry : System.Web.UI.Page
{
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/LogOut.aspx", true);
        }
        if (!IsPostBack)
        {
           

            FillInvestorChildUnit();
            FillInvestorGrievance();
            FillInvestorGrievanceMasterTracker();
        }
    }

    #endregion

    /// To get child unit name and self unit name for a investor.
    /// </summary>
    private void FillInvestorChildUnit()
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

    private void FillInvestorGrievance()
    {
        SWPDashboard objSWPEntity = new SWPDashboard();
     
        try
        {
            objSWPEntity.strAction = "IGD";
            objSWPEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            
            DataTable dt = objserviceDashboard.getInvestorGrievance(objSWPEntity);
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

    private void FillInvestorGrievanceMasterTracker()
    {
        SWPDashboard objSWPent = new SWPDashboard();
      
        try
        {
            objSWPent.strAction = "IGD";
            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                objSWPent.intInvestorId = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }
            else
            {
                objSWPent.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            }
          
            DataTable dt = objserviceDashboard.getInvestorGrievance(objSWPent);
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
}