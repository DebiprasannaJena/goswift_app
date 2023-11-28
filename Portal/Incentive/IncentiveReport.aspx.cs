/*
 File Name         : IncentiveReport.aspx.cs
 Description       : To show the summary of incentive applied in dashboard
 Created by        : Ritika lath
 Created On        : 12th December 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using System.Web.UI.HtmlControls;
using DataAcessLayer.Incentive;

public partial class Portal_Incentive_IncentiveReport : System.Web.UI.Page
{
    List<InctDashBoard_Entity> gLstDashboardMainEntity = new List<InctDashBoard_Entity>();
    protected void Page_Load(object sender, EventArgs e)
    {
        FillGrid();
    }

    private void FillGrid()
    {
        grdIncentiveMain.DataSource = null;
        grdIncentiveMain.DataBind();

        InctSearch objSearch = new InctSearch()
        {
            intDistrict = 0,
            intYear = DateTime.Today.Year,
            intUnitType = 0,
            intStatus = 0
        };
        if (Request.QueryString["Distid"] != null)
        {
            objSearch.intDistrict = Convert.ToInt32(Request.QueryString["Distid"]);
        }
        if (Request.QueryString["IncentiveYear"] != null)
        {
            objSearch.intYear = Convert.ToInt32(Request.QueryString["IncentiveYear"]);
        }
        if (Request.QueryString["unitType"] != null)
        {
            objSearch.intUnitType = Convert.ToInt32(Request.QueryString["unitType"]);
        }
        if (Request.QueryString["policy"] != null)
        {
            objSearch.intPolicyType = Convert.ToInt32(Request.QueryString["policy"]);
        }
        if (Session["desId"].ToString() == "127" || Session["desId"].ToString() == "97")//if psmsme user dashboard
        {
            objSearch.intStatus = 1;
        }

        IncentiveMasterBusinessLayer objInctBuisness = new IncentiveMasterBusinessLayer();
        gLstDashboardMainEntity = objInctBuisness.ViewInctDashBoardDetails(objSearch);

        if (gLstDashboardMainEntity.Count > 0)
        {
            List<InctDashBoard_Entity> lstMain = new List<InctDashBoard_Entity>();
            var lst = gLstDashboardMainEntity.Where(x => x.intMainId == 1).ToList();
            if (lst != null)
            {
                lstMain = (List<InctDashBoard_Entity>)lst;
                grdIncentiveMain.DataSource = lstMain;
                grdIncentiveMain.DataBind();
            }
        }
    }

    protected void grdIncentiveMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intMainId = Convert.ToInt32(grdIncentiveMain.DataKeys[e.Row.RowIndex].Value);
            GridView grdFirstLevel = (GridView)e.Row.FindControl("grdFirstLevel");
            List<InctDashBoard_Entity> lstMain = new List<InctDashBoard_Entity>();
            var lst = gLstDashboardMainEntity.Where(x => x.intParentId == intMainId).ToList();
            if (lst != null)
            {
                lstMain = (List<InctDashBoard_Entity>)lst;
                grdFirstLevel.DataSource = lstMain;
                grdFirstLevel.DataBind();

            }

            HyperLink hypCount = (HyperLink)e.Row.FindControl("hypCount");
            Label lblCount = (Label)e.Row.FindControl("lblCount");
            if (lblCount.Text != "0")
            {
                lblCount.Visible = false;
                hypCount.Visible = true;
                hypCount.NavigateUrl = string.Format("ViewIncentiveDetailsRpt.aspx?type={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&uType={5}&Did={6}&year={7}&policy={8}", intMainId.ToString(), Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], Request.QueryString["unitType"], Request.QueryString["Distid"], Request.QueryString["IncentiveYear"], Request.QueryString["policy"]);
            }
            else
            {
                lblCount.Visible = true;
                hypCount.Visible = false;
            }
        }
    }

    protected void grdFirstLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BindInnerGrid("hdnFirstLevelMainId", "grdSecondLevel", e.Row, "spFirstLevelLink");
        }
    }

    protected void grdSecondLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BindInnerGrid("hdnSecondLevelMainId", "grdThirdLevel", e.Row, "spSecondLevelLink");
        }
    }

    private void BindInnerGrid(string strDatakeyHiddenFieldId, string strGridviewId, GridViewRow objRow, string strPanelName)
    {
        HiddenField hdn = (HiddenField)objRow.FindControl(strDatakeyHiddenFieldId);
        int intMainId = Convert.ToInt32(hdn.Value);
        GridView grd = (GridView)objRow.FindControl(strGridviewId);
        List<InctDashBoard_Entity> lstMain = new List<InctDashBoard_Entity>();
        var lst = gLstDashboardMainEntity.Where(x => x.intParentId == intMainId).ToList();
        if (lst != null)
        {
            lstMain = (List<InctDashBoard_Entity>)lst;
            grd.DataSource = lstMain;
            grd.DataBind();
            if (grd.Rows.Count == 0)
            {
                HtmlGenericControl span = (HtmlGenericControl)objRow.FindControl(strPanelName);
                span.Visible = false;

                //Panel pnl = (Panel)objRow.FindControl(strPanelName);
                //pnl.Visible = false;
            }
        }
        HyperLink hypCount = (HyperLink)objRow.FindControl("hypCount");
        Label lblCount = (Label)objRow.FindControl("lblCount");
        if (lblCount.Text != "0")
        {
            lblCount.Visible = false;
            hypCount.Visible = true;
            hypCount.NavigateUrl = string.Format("ViewIncentiveDetailsRpt.aspx?type={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&uType={5}&Did={6}&year={7}&policy={8}", intMainId.ToString(), Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], Request.QueryString["unitType"], Request.QueryString["Distid"], Request.QueryString["IncentiveYear"], Request.QueryString["policy"]);
        }
        else
        {
            lblCount.Visible = true;
            hypCount.Visible = false;
        }
    }

    protected void grdThirdLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow objRow = e.Row;
            HiddenField hdn = (HiddenField)objRow.FindControl("hdnThirdLevelMainId");
            int intMainId = Convert.ToInt32(hdn.Value);
            HyperLink hypCount = (HyperLink)objRow.FindControl("hypCount");
            Label lblCount = (Label)objRow.FindControl("lblCount");
            if (lblCount.Text != "0")
            {
                lblCount.Visible = false;
                hypCount.Visible = true;
                hypCount.NavigateUrl = string.Format("ViewIncentiveDetailsRpt.aspx?type={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&uType={5}&Did={6}&year={7}&policy={8}", intMainId.ToString(), Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], Request.QueryString["unitType"], Request.QueryString["Distid"], Request.QueryString["IncentiveYear"], Request.QueryString["policy"]);
            }
            else
            {
                lblCount.Visible = true;
                hypCount.Visible = false;
            }
        }

    }
}