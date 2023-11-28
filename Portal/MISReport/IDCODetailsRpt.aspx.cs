using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;

public partial class IDCODetailsRpt : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    PromoterDet objprom = new PromoterDet();

    int intRetVal = 0;
    int intRecordCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            try
            {
                txtFromdate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
                BindDistrict();
                BindNodalOffcr();
                BindSector();
                fillGridview();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ProposalIDCOMISReport");
            }
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
            throw ex.InnerException;
        }
    }
    private void BindSector()
    {
        try
        {
          
            
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SE";
            objProp.vchProposalNo = "";
            List<ProjectInfo> objProjList  = objService.PopulateProjDropdowns(objProp).ToList();

            ddlSector.DataSource = objProjList;
            ddlSector.DataTextField = "vchSectorName";
            ddlSector.DataValueField = "intSectorId";
            ddlSector.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlSector.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindNodalOffcr()
    {
        try
        {
            CICGService.CICGService AddAgenda = new CICGService.CICGService();
            drpNodalOffcr.DataSource = AddAgenda.FillNodalOfficer();
            drpNodalOffcr.DataTextField = "Name";
            drpNodalOffcr.DataValueField = "Id";
            drpNodalOffcr.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpNodalOffcr.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void fillGridview()
    {
        try
        {
            objprom.strAction = "A";
            objprom.vchProposalNo = txtProposalNo.Text.Trim();
            objprom.intSectorId = Convert.ToInt32(ddlSector.SelectedValue);
            objprom.intSubSectorId = Convert.ToInt32(drpInvestment.SelectedValue);
            objprom.vchCompName = txtUnitName.Text.Trim();
            objprom.intCordist = Convert.ToInt32(ddlDistrict.SelectedValue);
            objprom.intNodalOfcrID = Convert.ToInt32(drpNodalOffcr.SelectedValue);
            objprom.dtmFromDate = txtFromdate.Text.Trim();
            objprom.dtmToDate = txtTodate.Text.Trim();
            objprom.vchApplication = txtApplicationNumber.Text.Trim();

            List<PromoterDet> objProposalList = objService.GetIDCOMISDetailsRPT(objprom).ToList();

            grdIDCO.DataSource = objProposalList;
            grdIDCO.DataBind();

            txtFromdate.Text = txtFromdate.Text;
            txtTodate.Text = txtTodate.Text;

            intRecordCount = objProposalList.Count;
            intRetVal = grdIDCO.Rows.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objprom = null;
        }
    }
    private void DisplayPaging()
    {
        if (grdIDCO.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (grdIDCO.PageIndex + 1 == grdIDCO.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + grdIDCO.Rows[0].Cells[0].Text + "</b> - <b>" + intRecordCount + "</b> Of <b>" + intRecordCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + grdIDCO.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(grdIDCO.Rows[0].Cells[0].Text) + (grdIDCO.PageSize - 1)) + "</b> Of <b>" + intRecordCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }

    protected void grdIDCO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.grdIDCO.PageIndex * this.grdIDCO.PageSize) + e.Row.RowIndex + 1);
        }
    }
    protected void grdIDCO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIDCO.PageIndex = e.NewPageIndex;
        fillGridview();
    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                grdIDCO.PageIndex = 0;
                grdIDCO.AllowPaging = false;
                fillGridview();
            }
            else
            {
                lbtnAll.Text = "All";
                grdIDCO.AllowPaging = true;
                fillGridview();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalIDCOMISReport");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            fillGridview();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalIDCOMISReport");
        }
    }
    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("MisIDCOServiceReport", grdIDCO);
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("MisIDCOServiceReport", grdIDCO, "Mis Report on PEAL", "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        GridViewExportUtil.Export("MisIDCOServiceReport.xls", grdIDCO);
        fillGridview();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MisPealServiceReport.xls"));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        viewTable.RenderControl(htw);
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}