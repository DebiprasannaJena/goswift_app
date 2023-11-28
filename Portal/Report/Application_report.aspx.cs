using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.ServiceModel;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.Web.Services;
using Ionic.Zip;
using EntityLayer.Service;
using BusinessLogicLayer.Service;

public partial class Portal_Report_Application_report :SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    string strRetval = "";
    string filepath = "";
    string PEALCertificatefilepath = "";
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGridview();

        }
    }
    public void fillGridview()
    {
        try
        {
            BusinessLogicLayer.Service.ServiceBusinessLayer objdata = new ServiceBusinessLayer();
            //List<ServiceDetails> BindDepartmentWise(int intLevelDetailId)
            List<ServiceDetails> objProjList = new List<ServiceDetails>();
            ServiceDetails objProp = new ServiceDetails();

            objProp.strAction = Request.QueryString["status"].ToString();
            objProp.strAction = Request.QueryString["TID"].ToString();
           // objProp.intLevelDetailId = Convert.ToInt32(Request.QueryString["DID"].ToString());
            objProp.INT_SERVICEID = Convert.ToInt32(Request.QueryString["SID"].ToString());




            objProjList = objdata.ApplicationWise_Report(objProp).ToList();
            if (objProjList != null && objProjList.Count > 0)
                gvService.DataSource = objProjList; ;
            gvService.DataBind();
            intRetVal = objProjList.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    #region "Display Google Paging"

    private void DisplayPaging()
    {
        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                lblPaging.Text = "Results <b>" + ((Label)gvService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)gvService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)gvService.Rows[0].FindControl("lblsl")).Text) + gvService.PageSize - 1) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        fillGridview();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                gvService.PageIndex = 0;
                gvService.AllowPaging = false;
                fillGridview();
            }
            else
            {
                lbtnAll.Text = "All";
                gvService.AllowPaging = true;
                fillGridview();
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    #region "Gridview RowDataBound"
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = ((gvService.PageIndex * gvService.PageSize) + e.Row.RowIndex + 1).ToString();
                HyperLink hyAppli = (HyperLink)e.Row.FindControl("hyAppli");
                hyAppli.NavigateUrl = "DepartmentViewDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ApplicationNo=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&ServiceId=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=D";

                HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hyRcvd");
                hyRcvd.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] +"&status=D";

                HyperLink hyAppl = (HyperLink)e.Row.FindControl("hyAppl");
                hyAppl.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=A";


                HyperLink hyRejt = (HyperLink)e.Row.FindControl("hyRejt");
                hyRejt.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=R";

                HyperLink hyApprove = (HyperLink)e.Row.FindControl("hyApprove");
                hyApprove.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=AP";

                HyperLink hyQueryRasied = (HyperLink)e.Row.FindControl("hyQueryRasied");
                hyQueryRasied.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=R";

                HyperLink hyQueryReverted = (HyperLink)e.Row.FindControl("hyQueryReverted");
                hyQueryReverted.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=AP";

                HyperLink hyDiffered = (HyperLink)e.Row.FindControl("hyDiffered");
                hyDiffered.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=R";

                HyperLink hyInProgress = (HyperLink)e.Row.FindControl("hyInProgress");
                hyInProgress.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=AP";

                HyperLink hyPending = (HyperLink)e.Row.FindControl("hyPending");
                hyPending.NavigateUrl = "Service_status.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&status=P";

                if (hyRcvd.Text == "0")
                {
                    hyRcvd.Enabled = false;
                }
                if (hyAppl.Text == "0")
                {
                    hyAppl.Enabled = false;
                }
                if (hyRejt.Text == "0")
                {
                    hyRejt.Enabled = false;
                }
                if (hyApprove.Text == "0")
                {
                    hyApprove.Enabled = false;
                }
                if (hyQueryRasied.Text == "0")
                {
                    hyQueryRasied.Enabled = false;
                }
                if (hyQueryReverted.Text == "0")
                {
                    hyQueryReverted.Enabled = false;
                }
                if (hyDiffered.Text == "0")
                {
                    hyDiffered.Enabled = false;
                }
                if (hyInProgress.Text == "0")
                {
                    hyInProgress.Enabled = false;
                }
                if (hyPending.Text == "0")
                {
                    hyPending.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    #endregion
}