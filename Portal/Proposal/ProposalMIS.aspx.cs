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

public partial class Mastermodule_ProposalMIS : System.Web.UI.Page
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
            BindDistrict();
            BindStatus();
        }
    }
    public void fillGridview()
    {
        try
        {
            objProposal.strAction = "L";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.intsts = Convert.ToInt32(drpStatus.SelectedValue);
            objProposal.intDist = Convert.ToInt32(ddlDistrict.SelectedValue);
            objProposal.intApplicFor = Convert.ToInt32(ddlProjrctType.SelectedValue);
            objProposal.strFrom = Request["txtFromdate"];
            objProposal.strTo = Request["txtTodate"];
            List<ProposalDet> objProposalList = objService.getProposalDetailsMIS(objProposal).ToList();
            gvService.DataSource = objProposalList;
            gvService.DataBind();
            intRetVal = gvService.Rows.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProposal = null;
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
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + intRetVal + "</b> Of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvService.Rows[0].Cells[0].Text) + (gvService.PageSize - 1)) + "</b> Of <b>" + intRetVal + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);
           
            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "Large Industries";
            }
            else if (e.Row.Cells[2].Text == "2")
            {
                e.Row.Cells[2].Text = "MSME";
            }
            HiddenField hdnTextVal1 = (HiddenField)e.Row.FindControl("hdnTextVal1");
            HyperLink hprlnkproposal = (HyperLink)e.Row.FindControl("hypLink");
            hprlnkproposal.NavigateUrl = "../Proposal/ProposalDetails.aspx?Pno=" + hdnTextVal1.Value;
        }
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        fillGridview();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
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
    private void BindDistrict()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            ddlDistrict.DataSource = objProjList;
            ddlDistrict.DataTextField = "vchDistName";
            ddlDistrict.DataValueField = "intDistId";
            ddlDistrict.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindStatus()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "SM";
            objProp.vchProposalNo = " ";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            drpStatus.DataSource = objProjList;
            drpStatus.DataTextField = "vchStatusName";
            drpStatus.DataValueField = "intStatusId";
            drpStatus.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpStatus.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridview();
    }
}