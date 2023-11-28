/*
 * File Name : MISAllServiceStatusRpt.aspx
 * Class Name : Portal_MISReport_MISAllServiceStatusRpt
 * Created By :
 * Created On :
 * 
 */


using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


public partial class Portal_MISReport_MISAllServiceStatusRpt : System.Web.UI.Page
{
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            try
            {
                BindDistrict();
                BindIndustry();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "MISAllServiceStatusRpt");
            }
        }
    }
    private void BindDistrict()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();          
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            ddlDistrict.DataSource = objProjList;
            ddlDistrict.DataTextField = "vchDistName";
            ddlDistrict.DataValueField = "intDistId";
            ddlDistrict.DataBind();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MISAllServiceStatusRpt");
        }
    }
    private void BindBlock()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();          
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "BL";
            objProp.INTDISTRICTID = Convert.ToInt32(ddlDistrict.SelectedValue);
            objProp.intBlockId = 0;
            objProp.IntInvestorId = 0;
            DataTable dt = objService.ALLSERVICEDETAILS(objProp);
            ddlblock.DataSource = dt;
            ddlblock.DataTextField = "vchBlockName";
            ddlblock.DataValueField = "intBlockId";
            ddlblock.DataBind();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlblock.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MISAllServiceStatusRpt");
        }
    }
    private void BindIndustry()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
           
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "INDUSTRY";
            if (ddlDistrict.SelectedIndex > 0)
            {
                objProp.INTDISTRICTID = Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            else
            {
                objProp.INTDISTRICTID = 0;
            }
            if (ddlblock.SelectedIndex > 0)
            {
                objProp.intBlockId = Convert.ToInt32(ddlblock.SelectedValue);
            }
            else
            {
                objProp.intBlockId = 0;
            }
            objProp.IntInvestorId = 0;
            DataTable dt = objService.ALLSERVICEDETAILS(objProp);
            ddlindustry.DataSource = dt;
            ddlindustry.DataTextField = "VCH_INV_NAME";
            ddlindustry.DataValueField = "INT_INVESTOR_ID";
            ddlindustry.DataBind();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlindustry.Items.Insert(0, list);

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MISAllServiceStatusRpt");
        }
    }
    private void BindUnity()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();          
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "UNITY";           
            objProp.INTDISTRICTID = 0;           
            objProp.intBlockId = 0;            
            objProp.IntInvestorId = Convert.ToInt32(ddlindustry.SelectedValue);
            DataTable dt = objService.ALLSERVICEDETAILS(objProp);
            ddlunity.DataSource = dt;
            ddlunity.DataTextField = "VCH_INV_NAME";
            ddlunity.DataValueField = "INT_INVESTOR_ID";
            ddlunity.DataBind();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlunity.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MISAllServiceStatusRpt");
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex > 0)
        {
            BindBlock();
            BindIndustry();
        }
        else
        {
            ddlblock.Items.Clear();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlblock.Items.Insert(0, list);
            BindIndustry();
            ddlunity.Items.Clear();
            ddlunity.Items.Insert(0, list);
        }
    }
    protected void ddlblock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlblock.SelectedIndex > 0)
        {
            BindIndustry();
        }
        else
        {
            if (ddlDistrict.SelectedIndex <= 0)
            {                
                System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
                list.Text = "--Select--";
                list.Value = "0";
                BindIndustry();
                ddlunity.Items.Clear();
                ddlunity.Items.Insert(0, list);
            }
            else if (ddlDistrict.SelectedIndex > 0)
            {
                BindIndustry();
                System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
                list.Text = "--Select--";
                list.Value = "0";
                ddlunity.Items.Clear();
                ddlunity.Items.Insert(0, list);
            }
        }
    }
    protected void ddlindustry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlindustry.SelectedIndex > 0)
        {
            BindUnity();
        }
        else
        {
            ddlunity.Items.Clear();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlunity.Items.Insert(0, list);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        BindDistrict();
        BindIndustry();
        System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlblock.Items.Clear();      
        ddlunity.Items.Clear();
        ddlblock.Items.Insert(0, list);        
        ddlunity.Items.Insert(0, list);
        grdPealDetails.DataSource = null;
        grdPealDetails.DataBind();
        lbtnAll.Visible = false;
        lblPaging.Visible = false;
    }
    private void BindGrid()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
            DataTable dt = new DataTable();
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "ALLSERVICE";
            if (ddlDistrict.SelectedIndex > 0)
            {
                objProp.INTDISTRICTID = Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            else
            {
                objProp.INTDISTRICTID = 0;
            }
            if (ddlblock.SelectedIndex > 0)
            {
                objProp.intBlockId = Convert.ToInt32(ddlblock.SelectedValue);
            }
            else
            {
                objProp.intBlockId = 0;
            }         
            if (ddlunity.SelectedIndex > 0)
            {
                objProp.IntInvestorId = Convert.ToInt32(ddlunity.SelectedValue);
                objProp.intNodalOfficerId= Convert.ToInt32(ddlunity.SelectedValue);
            }
            else
            {
                if (ddlindustry.SelectedIndex > 0)
                {
                    objProp.IntInvestorId = Convert.ToInt32(ddlindustry.SelectedValue);
                    objProp.intNodalOfficerId = 0;
                }
                else
                {
                    objProp.IntInvestorId = 0;
                    objProp.intNodalOfficerId = 0;
                }                
            }
            dt.Reset();
            dt = objService.ALLSERVICEDETAILS(objProp);
            grdPealDetails.DataSource = dt;
            grdPealDetails.DataBind();
            intRetVal = dt.Rows.Count;
            if (intRetVal > 0)
            {
                DisplayPaging();
            }
            else
            {
                lbtnAll.Visible = false;
                lblPaging.Visible = false;
            }           
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MISAllServiceStatusRpt");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("MisReport", grdPealDetails);
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
       
        BindGrid();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MISReport.xls"));
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
    private void DisplayPaging()
    {
        if (this.grdPealDetails.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.grdPealDetails.PageIndex + 1 == this.grdPealDetails.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdPealDetails.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdPealDetails.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdPealDetails.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdPealDetails.PageSize - 1))) + "</b> of <b>" + intRetVal + "</b>";
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            grdPealDetails.PageIndex = 0;
            grdPealDetails.AllowPaging = false;
            BindGrid();
        }
        else
        {
            lbtnAll.Text = "All";
            grdPealDetails.AllowPaging = true;
            BindGrid();
        }
    }
    protected void grdPealDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPealDetails.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}