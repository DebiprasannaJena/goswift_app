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

public partial class Portal_Report_SmsAndMailStatusReport : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    SMSAndMAILCls objService1 = new SMSAndMAILCls();
    string strRetval = "";
    string filepath = "";
    string PEALCertificatefilepath = "";
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridDetails();
            BindDept();

        }
    }

    //#region "Display Google Paging"

    private void DisplayPaging()
    {
        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(gvService.Rows[0].Cells[0].Text) + gvService.PageSize - 1) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    //#endregion
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);
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

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindService();
    }
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
    private void BindService()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
        ddlService.DataSource = objServicelist;
        ddlService.DataTextField = "strServiceName";
        ddlService.DataValueField = "intServiceId";
        ddlService.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlService.Items.Insert(0, list);

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGridDetails();
    }
    private void BindGridDetails()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<SMSAndMAILCls> objProjList = new List<SMSAndMAILCls>();
        objService1.SMServiceID = Convert.ToInt32(ddlService.SelectedValue);
        objService1.SMDEPTID = Convert.ToInt32(ddldept.SelectedValue);
      
        objService1.SMFrmDat = txtFromdate.Text;
        objService1.SMToDt = txtTodate.Text;
        objService1.SMType = ddlType.SelectedValue.ToString();
        objProjList = objService.SMSAndMailStatusReport(objService1).ToList();
        gvService.DataSource = objProjList;
        gvService.DataBind();
        txtFromdate.Text = txtFromdate.Text;
        txtTodate.Text = txtTodate.Text;
        intRetVal = objProjList.Count();
        DisplayPaging();
    }
}