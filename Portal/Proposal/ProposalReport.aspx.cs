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

public partial class Mastermodule_ProposalReport : SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    int intRetVal = 0;
    decimal totalCaptalInv = 0;
    int totalProposedEmpmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGridview();
            BindDistrict();
            BindStatus();
            BindSector();
        }
    }
    public void fillGridview()
    {
        try
        {
            objProposal.strAction = "V";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.intsts = Convert.ToInt32(drpStatus.SelectedValue);
            objProposal.intDist = Convert.ToInt32(ddlDistrict.SelectedValue);
            objProposal.intApplicFor = Convert.ToInt32(ddlProjrctType.SelectedValue);
            objProposal.strFrom = Convert.ToString(txtFromdate.Text.Trim());
            objProposal.strTo = Convert.ToString(txtAmount.Text.Trim());
            objProposal.intBlockId = Convert.ToInt32(ddlSector.SelectedValue);
            objProposal.intStsdet = Convert.ToInt32(ddlSubSector.SelectedValue);
            objProposal.intDistId = Convert.ToInt32(ddlPrioritySector.SelectedValue);
            objProposal.strApplicationFrom = Convert.ToString(txtApplicationfrom.Text.Trim());
            objProposal.strApplicationTo = Convert.ToString(txtApplicationTo.Text.Trim());
            objProposal.strEmployeemntTo = Convert.ToString(txtEmployemntTo.Text.Trim());
            objProposal.strProposedInvTo = Convert.ToString(txtProposedTo.Text.Trim());
            objProposal.intQueryRaisedValue = Convert.ToInt32(drpQueryStatus.SelectedValue);
            objProposal.strATOFrom = Convert.ToString(txtAnnualturnOverFrom.Text.Trim());
            objProposal.strATOTo = Convert.ToString(txtAnnualturnOverTo.Text.Trim());
            if (rdnLandRqd.SelectedValue != "")
            {
                objProposal.intLandReqd = Convert.ToInt32(rdnLandRqd.SelectedValue);
            }
            List<ProposalDet> objProposalList = objService.getProposalDetailsMIS(objProposal).ToList();
            gvService.DataSource = objProposalList;
            gvService.DataBind();
            intRetVal = objProposalList.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
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
            totalCaptalInv = Convert.ToDecimal(totalCaptalInv) + Convert.ToDecimal(e.Row.Cells[6].Text.ToString());
            totalProposedEmpmt = Convert.ToInt32(totalProposedEmpmt) + Convert.ToInt32(e.Row.Cells[5].Text.ToString());
            
            if (e.Row.Cells[13].Text == "Applied")
            {
                e.Row.Cells[13].ForeColor= System.Drawing.Color.Blue;
            }
           else if (e.Row.Cells[13].Text == "Approved")
            {
                e.Row.Cells[13].ForeColor = System.Drawing.Color.Green;
            }
            else if (e.Row.Cells[13].Text == "Rejected")
            {
                e.Row.Cells[13].ForeColor = System.Drawing.Color.Red;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
               e.Row.Cells[1].Text = "Total";
               e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
               e.Row.Cells[1].Font.Bold = true;
             e.Row.Cells[6].Text = totalCaptalInv.ToString("0.00");
             e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Cells[6].Font.Bold = true;
             e.Row.Cells[5].Text = totalProposedEmpmt.ToString("0.00");
             e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Cells[5].Font.Bold = true;

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
           
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
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
            Util.LogError(ex, "ProposalMIS");
        }
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridview();
    }
    private void BindStatus()
    {
        try
        {
            
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "SM";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
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
            Util.LogError(ex, "ProposalMIS");
        }
    }
    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSector(ddlSector.SelectedValue);
    }
    private void BindSubSector(string strstate)
    {
       
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "SU";
        objProp.vchProposalNo = strstate;
        List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        ddlSubSector.DataSource = objProjList;
        ddlSubSector.DataTextField = "vchSectorName";
        ddlSubSector.DataValueField = "intSectorId";
        ddlSubSector.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlSubSector.Items.Insert(0, list);
    }
    private void BindSector()
    {
        
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "SE";
        objProp.vchProposalNo = "";
        List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlSector.DataSource = objProjList;
        ddlSector.DataTextField = "vchSectorName";
        ddlSector.DataValueField = "intSectorId";
        ddlSector.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlSector.Items.Insert(0, list);
    }
}