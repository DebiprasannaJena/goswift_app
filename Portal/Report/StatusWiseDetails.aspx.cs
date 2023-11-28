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

public partial class Portal_Report_StatusWiseDetails : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    ServiceDetails objService1 = new ServiceDetails();
    string strRetval = "";
    string filepath = "";
    string PEALCertificatefilepath = "";
    int intRetVal = 0;
    decimal PortColl = 0, Collection = 0, AbstSanPjt = 0, PortSanpjt = 0, Sanproject = 0, AbstUtlzAmnt = 0, PortUtlzaAmnt = 0, utiliZation = 0, AbstColl = 0;
    double intReceive = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridDetails();
            //BindDistrict();
           // BindStatus();
        }
    }
    //public void fillGridview()
    //{
    //    try
    //    {
    //        objProposal.strAction = "L";
    //        objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
    //        objProposal.intsts = Convert.ToInt32(drpStatus.SelectedValue);
    //        //objProposal.intDist = Convert.ToInt32(ddlDistrict.SelectedValue);
    //        //objProposal.intApplicFor = Convert.ToInt32(ddlProjrctType.SelectedValue);
    //        objProposal.strFrom = Request["txtFromdate"];
    //        objProposal.strTo = Request["txtTodate"];
    //        List<ProposalDet> objProposalList = objService.getProposalDetailsMIS(objProposal).ToList();
    //        gvService.DataSource = objProposalList;
    //        gvService.DataBind();
    //        intRetVal = gvService.Rows.Count;
    //        DisplayPaging();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objProposal = null;
    //    }
    //}
    private void BindGridDetails()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objProjList = new List<ServiceDetails>();
        objService1.VCH_APPLICATION_NO = txtAppno.Text;
        objService1.status = Convert.ToString(drpStatus.SelectedValue);
        objService1.SMFrmDat = txtFromdate.Text;
        objService1.SMToDt = txtTodate.Text;
        objProjList = objService.StausReport(objService1).ToList();
        gvService.DataSource = objProjList;
        gvService.DataBind();
        txtFromdate.Text = txtFromdate.Text;
        txtTodate.Text = txtTodate.Text;
        intRetVal = objProjList.Count();
        DisplayPaging();
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
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            HyperLink hyAppli = (HyperLink)e.Row.FindControl("hyAppli");
            hyAppli.NavigateUrl = "DepartmentViewDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ApplicationNo=" + gvService.DataKeys[e.Row.RowIndex].Values[1]  +"&ServiceId=" + gvService.DataKeys[e.Row.RowIndex].Values[0] +  "&status=D";
            Label lblChallan = (Label)e.Row.FindControl("lblChallan");
            if (hyAppli.Text == "0")
            {
                hyAppli.Enabled = false;
            }
            if (lblChallan.Text == "0")
            {
                lblChallan.Enabled = false;
            }
            string var = lblChallan.Text;
          intReceive = intReceive + Convert.ToDouble(lblChallan.Text);
            
            //if (e.Row.Cells[2].Text == "1")
            //{
            //    e.Row.Cells[2].Text = "Large Industries";
            //}
            //else if (e.Row.Cells[2].Text == "2")
            //{
            //    e.Row.Cells[2].Text = "MSME";
            //}
            //HiddenField hdnTextVal1 = (HiddenField)e.Row.FindControl("hdnTextVal1");
            //HyperLink hprlnkproposal = (HyperLink)e.Row.FindControl("hypLink");
            //hprlnkproposal.NavigateUrl = "../Proposal/ProposalDetails.aspx?Pno=" + hdnTextVal1.Value;
        }
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[0].ColumnSpan = 1;
            e.Row.Cells[1].Text = "Total";
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[3].Text = Convert.ToString(intReceive);
            e.Row.Cells[3].Font.Bold = true;

           // e.Row.Cells[0].ColumnSpan = 3;
           // e.Row.Cells[0].Text = "Total";
           // e.Row.Cells[0].Style.Add("text-align", "right");
            //e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[3].Text = Convert.ToString(intResolved);
            //e.Row.Cells[3].Font.Bold = true;
            //e.Row.Cells[4].Text = Convert.ToString(intPending);
            //e.Row.Cells[4].Font.Bold = true;
            //e.Row.Cells[5].Text = Convert.ToString(intACRCNT);
            //e.Row.Cells[5].Font.Bold = true;
            //e.Row.Cells[6].Text = Convert.ToString(intURBQCNT);
            //e.Row.Cells[6].Font.Bold = true;
            //e.Row.Cells[7].Text = Convert.ToString(intACRCNT);
            //e.Row.Cells[7].Font.Bold = true;
        }
      
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            BindGridDetails();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            BindGridDetails();
        }
    }
    //private void BindDistrict()
    //{
    //    try
    //    {
    //        List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //        ProjectInfo objProp = new ProjectInfo();
    //        objProp.strAction = "DT";
    //        objProp.vchProposalNo = " ";
    //        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
    //        ddlDistrict.DataSource = objProjList;
    //        ddlDistrict.DataTextField = "vchDistName";
    //        ddlDistrict.DataValueField = "intDistId";
    //        ddlDistrict.DataBind();
    //        ListItem list = new ListItem();
    //        list.Text = "--Select--";
    //        list.Value = "0";
    //        ddlDistrict.Items.Insert(0, list);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private void BindStatus()
    //{
    //    try
    //    {
    //        List<ServiceDetails> objProjList = new List<ServiceDetails>();
    //        ProjectInfo objProp = new ProjectInfo();
    //        objProp.strAction = "SM";
    //        objProp.vchProposalNo = " ";
    //        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
    //        drpStatus.DataSource = objProjList;
    //        drpStatus.DataTextField = "vchStatusName";
    //        drpStatus.DataValueField = "intStatusId";
    //        drpStatus.DataBind();
    //        ListItem list = new ListItem();
    //        list.Text = "--Select--";
    //        list.Value = "0";
    //        drpStatus.Items.Insert(0, list);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridDetails();
    }
}