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
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.Web.Services;
using Ionic.Zip;

public partial class Portal_Report_MailStatusReport : System.Web.UI.Page
{
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
            BusinessLogicLayer.Service.ServiceBusinessLayer objService = new ServiceBusinessLayer();
            List<SMSAndMAILCls> objProposalList = objService.MailAndMailStatusReport().ToList();
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
            //objProposal = null;
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


}