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

public partial class Portal_Report_Service_status : SessionCheck
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

            objProp.strAction = "S";
            objProp.intLevelDetailId =Convert.ToInt32( Request.QueryString["DID"].ToString());
            //objProp.INT_SERVICEID = Convert.ToInt32(Request.QueryString["SID"].ToString());


            objProjList = objdata.ServiceWise_Report(objProp).ToList(); 
            if (objProjList != null && objProjList.Count > 0)
                gvService.DataSource = objProjList; ;
            gvService.DataBind();
            intRetVal = objProjList.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
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

            Util.LogError(ex, "Helpdesk");
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

                HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hyRcvd");
                hyRcvd.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=V" + "&TID=V";

                HyperLink hyAppl = (HyperLink)e.Row.FindControl("hyAppl");
                hyAppl.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=A1" + "&TID=A1";


                HyperLink hyRejt = (HyperLink)e.Row.FindControl("hyRejt");
                hyRejt.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=R1" + "&TID=R1";

                HyperLink hyApprove = (HyperLink)e.Row.FindControl("hyApprove");
                hyApprove.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=AP1" + "&TID=AP1";

                HyperLink hyQueryRasied = (HyperLink)e.Row.FindControl("hyQueryRasied");
                hyQueryRasied.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=RS1" + "&TID=RS1";

                HyperLink hyQueryReverted = (HyperLink)e.Row.FindControl("hyQueryReverted");
                hyQueryReverted.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=QR1" + "&TID=QR1";

                HyperLink hyDiffered = (HyperLink)e.Row.FindControl("hyDiffered");
                hyDiffered.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=D1" + "&TID=D1";

                HyperLink hyInProgress = (HyperLink)e.Row.FindControl("hyInProgress");
                hyInProgress.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=I1" + "&TID=I1";

                HyperLink hyPending = (HyperLink)e.Row.FindControl("hyPending");
                hyPending.NavigateUrl = "Application_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=P1" + "&TID=P1";

                HyperLink hyForwarded = (HyperLink)e.Row.FindControl("hyForwarded");
                hyForwarded.NavigateUrl = "ApplicationStatus_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=F1" + "&TID=F1";

                HyperLink hyGen = (HyperLink)e.Row.FindControl("hyGen");
                hyGen.NavigateUrl = "ApplicationStatus_report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&DID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=G1" + "&TID=G1";

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
                if (hyForwarded.Text == "0")
                {
                    hyForwarded.Enabled = false;
                }
                if (hyGen.Text == "0")
                {
                    hyGen.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    #endregion

}