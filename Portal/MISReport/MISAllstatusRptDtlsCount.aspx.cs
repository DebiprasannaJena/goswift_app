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
using System.Data.SqlClient;
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


public partial class Portal_MISReport_MISAllServiceStatusRptCount : System.Web.UI.Page
{
    int intRetVal = 0;
    string str1 = "";
    string str2 = ""; 
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            try
            {                
                BindIndustry();
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");               
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "MISAllServiceStatusRptCount");
            }
        }
    }
    private void BindIndustry()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();           
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "INDUSTRY";
            objProp.INTDISTRICTID = 0;
            objProp.intBlockId = 0;
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
            Util.LogError(ex, "MISAllServiceStatusRptCount");
        }
    }
    private void BindUnity()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();           
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "UNITYGRID";
            objProp.INTDISTRICTID = 0;
            objProp.intBlockId = 0;
            objProp.IntInvestorId = Convert.ToInt32(ddlindustry.SelectedValue);
            DataTable dt = objService.ALLSERVICEDETAILS(objProp);
            if (dt.Rows.Count > 0)
            {
                gvunity.DataSource = dt;
                gvunity.DataBind();
            }
            else
            {
                gvunity.DataSource = null;
                gvunity.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MISAllServiceStatusRptCount");
        }
    }   
    protected void ddlindustry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlindustry.SelectedIndex > 0)
        {
            BindUnity();
            grdPealDetails.DataSource = null;
            grdPealDetails.DataBind();
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
        }
        else
        {
            gvunity.DataSource = null;
            gvunity.DataBind();
            grdPealDetails.DataSource = null;
            grdPealDetails.DataBind();
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        BindIndustry();
        gvunity.DataSource = null;
        gvunity.DataBind();
        grdPealDetails.DataSource = null;
        grdPealDetails.DataBind();
        lbtnAll.Visible = false;
        lblPaging.Visible = false;
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }
    private void BindGrid()
    {
        try
        {
            for (int i = 0; i <= gvunity.Rows.Count - 1; i++)
            {
                CheckBox chkItem = ((CheckBox)(gvunity.Rows[i].FindControl("chkSelectAdd")));
                if (chkItem.Checked)
                {
                    str1 = gvunity.DataKeys[i].Values[0].ToString();
                    if (i == 0)
                    {
                        str2 = str1;
                    }
                    else
                    {
                        str2 = str2 + "," + str1;
                    }
                    count = count + 1;
                }
            }
            if (count > 0)
            {
                ProposalBAL objService = new ProposalBAL();
                DataTable dt = new DataTable();
                ProposalDet objProp = new ProposalDet();
                objProp.strAction = "ALLSERVICEDEPT";
                objProp.INTDISTRICTID = 0;
                objProp.intBlockId = 0;
                objProp.IntInvestorId = 0;
                objProp.intNodalOfficerId = 0;
                objProp.strATOFrom = txtFromDate.Text;
                objProp.strATOTo = txtToDate.Text;
                objProp.VCH_INVESTOR_ID = str2.TrimStart(',').TrimEnd(',');
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
            else
            {
                grdPealDetails.DataSource = null;
                grdPealDetails.DataBind();
                string str = "jAlert('<strong>Please Select At Least One Industry</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MISAllServiceStatusRptCount");
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
        IncentiveCommonFunctions.ExportToExcel("PealQueryReportDtls", grdPealDetails, "Mis Report on PEAL", "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        GridViewExportUtil.Export("MISReport.xls", grdPealDetails);
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
    protected void grdPealDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        int index = row.RowIndex;
        int DEPTID = Convert.ToInt32(grdPealDetails.DataKeys[index].Value);
        int stats = 0;
        if (e.CommandName.ToString() == "Pending")
        {
            stats = 1;
        }
        else if (e.CommandName.ToString() == "Approved")
        {
            stats = 2;
        }
        else if (e.CommandName.ToString() == "Rejected")
        {
            stats = 3;
        }
        else if (e.CommandName.ToString() == "Received")
        {
            stats = 0;
        }
        ViewState["DEPTID"] = DEPTID.ToString();
        ViewState["stats"] = stats.ToString();       
        BindPopupGrid();
        
    }
    private void BindPopupGrid()
    {
        for (int i = 0; i <= gvunity.Rows.Count - 1; i++)
        {
            CheckBox chkItem = ((CheckBox)(gvunity.Rows[i].FindControl("chkSelectAdd")));
            if (chkItem.Checked)
            {
                str1 = gvunity.DataKeys[i].Values[0].ToString();
                if (i == 0)
                {
                    str2 = str1;
                }
                else
                {
                    str2 = str2 + "," + str1;
                }
                count = count + 1;
            }
        }
        if (count > 0)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_ALL_SERVICEDTL_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", "ALLSERVICECOUNT");
                cmd.Parameters.AddWithValue("@PDistrictid", 0);
                cmd.Parameters.AddWithValue("@PBlockid", 0);
                cmd.Parameters.AddWithValue("@PINT_INVESTOR_ID", Convert.ToInt32(ViewState["DEPTID"].ToString()));
                cmd.Parameters.AddWithValue("@CINT_INVESTOR_ID", Convert.ToInt32(ViewState["stats"].ToString()));
                cmd.Parameters.AddWithValue("@MINT_INVESTOR_ID", str2.TrimStart(',').TrimEnd(','));
                cmd.Parameters.AddWithValue("@VCH_FROMDATE", txtFromDate.Text);
                cmd.Parameters.AddWithValue("@VCH_TODATE", txtToDate.Text);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            grdService.DataSource = dt;
            grdService.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ViewModal() ;", true);
        }
        else
        {
            grdPealDetails.DataSource = null;
            grdPealDetails.DataBind();
            string str = "jAlert('<strong>Please Select At Least One Industry</strong>', 'GO-SWIFT');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            return;
        }
    }
    protected void grdService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdService.PageIndex = e.NewPageIndex;
        BindPopupGrid();
    }
}