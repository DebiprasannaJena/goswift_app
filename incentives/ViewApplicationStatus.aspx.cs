//******************************************************************************************************************
// File Name             :  ViewApplicationStatus.aspx.cs
// Description           :  View Application Status
// Created by            :  Sushant Kumar Jena
// Created on            :  13th Sept 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//          1                         11-OCT-2017         Pranay Kumar                 Implementation of Query Management
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data;
using BusinessLogicLayer.Dashboard;

public partial class incentives_ViewApplicationStatus : SessionCheck
{
    ///// Page Load
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("incentiveoffered.aspx");
        }

        if (!IsPostBack)
        {
            fillInvestorChildUnit();
            fillInctName();

            if (Request.QueryString["Sts"] != null)
            {
                string strStatus = Request.QueryString["Sts"].ToString();
                if (strStatus == "A1")
                {
                    DrpDwn_Status.SelectedValue = "2";
                }
                else if (strStatus == "A2")
                {
                    DrpDwn_Status.SelectedValue = "1";
                }
                else if (strStatus == "A3")
                {
                    DrpDwn_Status.SelectedValue = "3";
                }
                else if (strStatus == "A4")
                {
                    DrpDwn_Status.SelectedValue = "7";
                }

                fillGrid(strStatus);
            }
            else
            {
                fillGrid("A");
            }
        }
    }

    #region Function Used

    ///// Bind Incentive Name
    private void fillInctName()
    {
        try
        {
            IncentiveMaster objIncentive = new IncentiveMaster();
            IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
            objIncentive.Action = "N";
            objIncentive.Param = Convert.ToInt32(Session["InvestorId"]);

            objLayer.BindDropdown(DrpDwn_Inct_Name, objIncentive);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", ex.Message.ToString(), true);
        }
        finally
        {
            //objIncentive = null;
        }
    }
    ///// Bind Gridview
    private void fillGrid(string strAction)
    {
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        Inct_Application_Details_Entity objBU_Entity = new Inct_Application_Details_Entity();

        /*---------------------------------------------------------------*/
        int? i_inct_id = null;
        if (DrpDwn_Inct_Name.SelectedIndex > 0)
        {
            i_inct_id = Convert.ToInt32(DrpDwn_Inct_Name.SelectedValue);
        }
        /*---------------------------------------------------------------*/
        int? i_status_id = null;
        if (DrpDwn_Status.SelectedIndex > 0)
        {
            i_status_id = Convert.ToInt32(DrpDwn_Status.SelectedValue);
        }
        /*---------------------------------------------------------------*/
        string strAppNo = null;
        if (Txt_App_No.Text != "")
        {
            strAppNo = Txt_App_No.Text;
        }
        /*---------------------------------------------------------------*/
        string strFilterMode = "";
        string strInvestorId = "";
        if (DrpDwn_Investor_Unit.SelectedIndex > 0)
        {
            strFilterMode = "I";
            strInvestorId = DrpDwn_Investor_Unit.SelectedValue;
        }
        else
        {
            strFilterMode = "C";
            strInvestorId = Convert.ToString(Session["InvestorId"]);
        }
        /*---------------------------------------------------------------*/

        objBU_Entity.strUserID = strInvestorId;
        objBU_Entity.strAction = strAction;
        objBU_Entity.intInctId = i_inct_id;
        objBU_Entity.intStatus = i_status_id;
        objBU_Entity.strAppNo = strAppNo;
        objBU_Entity.strFilterMode = strFilterMode;

        IList<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
        list = ObjIMB.View_Application_Details(objBU_Entity);

        Grd_Application.DataSource = list;
        Grd_Application.DataBind();
    }

    #endregion

    ///// Button Search
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        fillGrid("B");
    }

    #region "Added By Pranay Kumar"
    #region "GridView RowDataBound"
    protected void Grd_Application_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                #region CR by GS Chhotray 16/Nov/2017
                try
                {
                    HiddenField Hid_Form_Preview_Id = (HiddenField)e.Row.FindControl("Hid_Form_Preview_Id");
                    HiddenField Hid_Unique_Id = (HiddenField)e.Row.FindControl("Hid_Unique_Id");
                    HyperLink HypLnk_App_No = (e.Row.FindControl("HypLnk_App_No") as HyperLink);
                    HypLnk_App_No.NavigateUrl = Hid_Form_Preview_Id.Value + "?InctUniqueNo=" + Hid_Unique_Id.Value;

                }
                catch (Exception x)
                {
                    Util.LogError(x, "Incentive");
                }
                #endregion


                HyperLink hypQueryDtls = (e.Row.FindControl("hypQueryDtls") as HyperLink);
                HyperLink Hy_Sanction_Doc = (e.Row.FindControl("Hy_Sanction_Doc") as HyperLink);

                HiddenField Hid_Sanction_File_Name = (e.Row.FindControl("Hid_Sanction_File_Name") as HiddenField);

                if (Hid_Sanction_File_Name.Value == "")
                {
                    Hy_Sanction_Doc.Visible = false;
                }
                else
                {
                    Hy_Sanction_Doc.Visible = true;
                }

                // hypQueryDtls.NavigateUrl="~/Portal/Proposal/QueryProposalRevert.aspx?ProposalNo=" + strProposalNo + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
                string strCurrQueryStatus = Convert.ToString(Grd_Application.DataKeys[e.Row.RowIndex].Values[1]);
                string strInctUniqueNo = Convert.ToString(Grd_Application.DataKeys[e.Row.RowIndex].Values[0]);
                if (strCurrQueryStatus == "--")
                {
                    hypQueryDtls.Text = "--";
                }
                else if (strCurrQueryStatus == "Completed")
                {
                    hypQueryDtls.NavigateUrl = "QueryIncentiveRevert.aspx?IncUnqNo=" + strInctUniqueNo + "";
                    hypQueryDtls.CssClass = "btn btn-success btn-sm";
                    hypQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
                }
                else if (strCurrQueryStatus == "QUERY RAISED")
                {
                    hypQueryDtls.NavigateUrl = "QueryIncentiveRevert.aspx?IncUnqNo=" + strInctUniqueNo + "";
                    hypQueryDtls.Text = strCurrQueryStatus;
                    hypQueryDtls.CssClass = "btn btn-success btn-sm";
                }
                else if (strCurrQueryStatus == "QUERY RESPONDED")
                {
                    hypQueryDtls.NavigateUrl = "QueryIncentiveRevert.aspx?IncUnqNo=" + strInctUniqueNo + "";
                    hypQueryDtls.Text = strCurrQueryStatus;
                    hypQueryDtls.CssClass = "label-warning label label-default";
                }

                HiddenField hdnDisburedId = (HiddenField)e.Row.FindControl("hdnDisburedId");
                IList<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
                Inct_Application_Details_Entity objBU_Entity = new Inct_Application_Details_Entity();
                LinkButton lbtnDisbursedDtls = (e.Row.FindControl("lbtnDisbursedDtls") as LinkButton);
                objBU_Entity.strAction = "DA";
                objBU_Entity.INTINCUNQUEID = Convert.ToInt32(hdnDisburedId.Value);
                list = ObjIMB.View_Application_ApprveFetch(objBU_Entity);
                HtmlGenericControl DisbursedList = (HtmlGenericControl)e.Row.FindControl("DisbursedList");

                DateTime strTime = DateTime.Now;
                string strDatetime = "";
                if (list.Count > 0)
                {
                    string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th>Application No.</th><th>Unit Name</th><th>Bank Name</th><th>UTR (Transaction ID)</th><th>Disbursed Amount</th><th>Date</th><th>Time</th><th>Files</th></tr>";

                    if (!string.IsNullOrEmpty(list[0].DisburseTime.ToString()) && list[0].DisburseTime.ToString() != "")
                        strTime = Convert.ToDateTime(list[0].DisburseTime.ToString());
                    else
                        strTime = DateTime.Now;

                    if (!string.IsNullOrEmpty(list[0].DisburseDate.ToString()) && list[0].DisburseDate.ToString() != "")
                        strDatetime = list[0].DisburseDate.ToString();
                    else
                        strDatetime = "";

                    if (list[0].DisbursementDocument == null || list[0].DisbursementDocument == "")
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + list[0].strApplicationNum.ToString() + "</td><td>" + list[0].strUnitName + "</td><td>" + list[0].BankName + "</td><td>" + list[0].DisburseNo + "</td><td>" + list[0].DisburseAmount + "</td><td>" + strDatetime + "</td><td>" + strTime.ToString("hh:mm tt", CultureInfo.InvariantCulture) + "</td><td>" + "<a target='' href='#'>--</a>" + "</td></tr>";
                    }
                    else
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + list[0].strApplicationNum.ToString() + "</td><td>" + list[0].strUnitName + "</td><td>" + list[0].BankName + "</td><td>" + list[0].DisburseNo + "</td><td>" + list[0].DisburseAmount + "</td><td>" + strDatetime + "</td><td>" + strTime.ToString("hh:mm tt", CultureInfo.InvariantCulture) + "</td><td>" + "<a target='_blank' href='../Portal/Incentive/Disbursement/" + list[0].DisbursementDocument + "'>Download</a>" + "</td></tr>";
                    }

                    strHTMlQuery = strHTMlQuery + "</table>";

                    DisbursedList.InnerHtml = strHTMlQuery;

                    lbtnDisbursedDtls.CssClass = "btn btn-success btn-sm";
                    lbtnDisbursedDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
                }
                else
                {
                    lbtnDisbursedDtls.Visible = false;
                }
            }
        }
        catch (Exception)
        {
        }
    }
    #endregion
    #endregion

    /// <summary>
    /// Added by Sushant Jena On Dt.14-Aug-2018
    /// To get child unit name and self unit name for a investor.
    /// </summary>
    private void fillInvestorChildUnit()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "INVUNIT";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

            DataTable dt = new DataTable();
            dt = objserviceDashboard.getInvestorChildUnit(objSWP);

            if (dt.Rows.Count > 1)
            {
                DrpDwn_Investor_Unit.DataTextField = "VCH_INV_NAME";
                DrpDwn_Investor_Unit.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_Investor_Unit.DataSource = dt;
                DrpDwn_Investor_Unit.DataBind();
                DrpDwn_Investor_Unit.Items.Insert(0, new ListItem("-Select Unit-", "0"));

                divUnitName.Visible = true;
            }
            else
            {
                DrpDwn_Investor_Unit.Items.Clear();
                divUnitName.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }
    protected void DrpDwn_Investor_Unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillAppCount();
    }
}