using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using BusinessLogicLayer.Service;
using EntityLayer.Service;

public partial class InnerInvestorDashboard : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDept();
            BindInnerStatusApproval();
        }
    }

    protected void ddldept_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindInnerStatusApproval();
    }

    #region FunctionUsed

    ///// Bind Gridview
    private void BindInnerStatusApproval()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "DI";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objSWP.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
            objSWP.strFilterMode = Request.QueryString["FilterMode"];

            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetDashboardServiceStatusDtls(objSWP).ToList();
            grdServiceDtl.DataSource = objServiceStatus;
            grdServiceDtl.DataBind();
            if (Request.QueryString["Status"].ToString() == "1")
            {
                grdServiceDtl.Columns[1].Visible = true;
                grdServiceDtl.Columns[2].Visible = false;
            }
            else if (Request.QueryString["Status"].ToString() == "2")
            {
                grdServiceDtl.Columns[2].Visible = true;
                grdServiceDtl.Columns[1].Visible = false;
            }
            else
            {
                grdServiceDtl.Columns[1].Visible = true;
                grdServiceDtl.Columns[2].Visible = true;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    ///// Bind Department
    private void BindDept()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindDepartment("DP").ToList();
        ddldept.DataSource = objServicelist;
        ddldept.DataTextField = "strdeptname";
        ddldept.DataValueField = "Deptid";
        ddldept.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldept.Items.Insert(0, list);

    }

    #endregion
}