/*
 * File Name : MapPealwithServiceRpt.aspx
 * Class Name : Portal_MISReport_MapPealwithServiceRpt
 * Created By :
 * Created On :
 * 
 */


using System;
using System.Collections.Generic;
using System.Configuration;
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


public partial class Portal_MISReport_MapPealwithServiceRpt : System.Web.UI.Page
{
    
   // int pageIndex = 1;
    int pageSize = 10;
    int intRetVal = 0;
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    private int pageIndex
    {
        get
        {
            return ViewState["pageIndex"] == null ? 1 : int.Parse(ViewState["pageIndex"].ToString());
        }
        set { ViewState["pageIndex"] = value; }
    }
    private int tableDataCount
    {
        get
        {
            return ViewState["PAGE.SIZE"] == null ? 10 : int.Parse(ViewState["PAGE.SIZE"].ToString());
        }
        set { ViewState["PAGE.SIZE"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            try
            {
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");              
                BindRange();
                litStart.Text = "";
                litTotalRecord.Text = "";
                litEnd.Text = "";
                
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PealService");
            }
        }
        CallPaging(); 
    }
   
    private void BindRange()
    {
        drpInvestmentAmt.Items.Clear();
        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("-ALL-", "0"));
        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("50-1000 CR (SLSWCA)", "2"));
        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("> 1000 Cr (HLCA)", "3"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ddlrecord.SelectedValue = "10";
        DisplayPeal();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtcompany.Text = "";
        txtpeal.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlrecord.SelectedValue = "10";
        grdPealDetails.DataSource = null;
        grdPealDetails.DataBind();
        divPaging.Visible = false;
        divPagingShow.Visible = false;
    }
    public void DisplayPeal()
    {
        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_PEAL_MisReport_MapPealService"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@P_CHAR_ACTION", SqlDbType.VarChar).Value = "Peal";
                    cmd.Parameters.Add("@P_VCH_PEAL", SqlDbType.VarChar).Value = txtpeal.Text;
                    cmd.Parameters.Add("@P_VCH_INV_NAME", SqlDbType.VarChar).Value = txtcompany.Text;
                    cmd.Parameters.Add("@P_INT_INVESTOR_ID", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@P_INT_SERVICEID", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@P_INT_INVEST_AMT", SqlDbType.Int).Value = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
                    cmd.Parameters.Add("@pIntPageIndex", SqlDbType.Int).Value = pageIndex;
                    cmd.Parameters.Add("@pIntPageSize", SqlDbType.Int).Value = pageSize;                    
                    cmd.Parameters.Add("@PDtmFromDate", SqlDbType.VarChar).Value = txtFromDate.Text.Trim();
                    cmd.Parameters.Add("@PDtmToDate", SqlDbType.VarChar).Value = txtToDate.Text.Trim();
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
                if (PnlDt.Rows.Count > 0)
                {
                    grdPealDetails.DataSource = PnlDt;
                    grdPealDetails.DataBind();
                    hdnCurrentIndex.Value = pageIndex.ToString(CultureInfo.InvariantCulture);
                    uclPager.AddPageLinks(Convert.ToInt32(PnlDt.Rows[0]["TotalCount"].ToString()), pageSize, pageIndex);
                    ShowPageIndex(pageIndex, pageSize, Convert.ToInt32(PnlDt.Rows[0]["TotalCount"].ToString()));
                    divPaging.Visible = false;
                    divPagingShow.Visible = true;
                }
                else
                {
                    grdPealDetails.DataSource = null;
                    grdPealDetails.DataBind();
                    divPaging.Visible = false;
                    divPagingShow.Visible = false;

                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PealService");
                con.Close();
            }
        }
    }
    private void ShowPageIndex(int pageNumber, int pageSize, int totalRecords)
    {
        litStart.Text = (((pageNumber - 1) * pageSize) + 1).ToString();
        litTotalRecord.Text = totalRecords.ToString();
        int last = (((pageNumber - 1) * pageSize) + pageSize);
        litEnd.Text = (last > totalRecords ? totalRecords : last).ToString();        
    }
    private void CallPaging()
    {
        this.uclPager.PaginationLinkClicked += new EventHandler(paginationLink_Click);
        //int tableDataCount = 1;
        string defaultInitialValueForHiddenControl = "Blank Value";
        int indexFromPreviousDataRetrieval = 1;
        if (!String.Equals(hdnCurrentIndex.Value, defaultInitialValueForHiddenControl))
        {
            indexFromPreviousDataRetrieval = Convert.ToInt32(hdnCurrentIndex.Value, CultureInfo.InvariantCulture);
        }
        uclPager.PreviousIndex = indexFromPreviousDataRetrieval;
        uclPager.PreAddAllLinks(tableDataCount, pageSize, indexFromPreviousDataRetrieval);
    }
    protected void paginationLink_Click(object sender, EventArgs e)
    {
        //When link is clicked, set the pageIndex from user control property
        pageIndex = uclPager.CurrentClickedIndex;
        DisplayPeal();        
    }
    protected void grdPealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string lsDataKeyValue = grdPealDetails.DataKeys[e.Row.RowIndex].Values[0].ToString();
            GridView gvSummary = (GridView)e.Row.FindControl("grdServiceDetails");
            DataTable dt = new DataTable();
            dt = DisplayAppliedService(Convert.ToInt32(lsDataKeyValue));
            gvSummary.DataSource = dt;
            gvSummary.DataBind();
        }
    }
    private DataTable DisplayAppliedService(int lsDataKeyValue)
    {
        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_PEAL_MisReport_MapPealService"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@P_CHAR_ACTION", SqlDbType.VarChar).Value = "Service";
                    cmd.Parameters.Add("@P_VCH_PEAL", SqlDbType.VarChar).Value = txtpeal.Text;
                    cmd.Parameters.Add("@P_VCH_INV_NAME", SqlDbType.VarChar).Value = txtcompany.Text;
                    cmd.Parameters.Add("@P_INT_INVESTOR_ID", SqlDbType.Int).Value = Convert.ToInt32(lsDataKeyValue);
                    cmd.Parameters.Add("@P_INT_SERVICEID", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@P_INT_INVEST_AMT", SqlDbType.Int).Value = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
                    cmd.Parameters.Add("@pIntPageIndex", SqlDbType.Int).Value = pageIndex;
                    cmd.Parameters.Add("@pIntPageSize", SqlDbType.Int).Value = pageSize;
                    cmd.Parameters.Add("@PDtmFromDate", SqlDbType.VarChar).Value = txtFromDate.Text.Trim();
                    cmd.Parameters.Add("@PDtmToDate", SqlDbType.VarChar).Value = txtToDate.Text.Trim();
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }                
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PealService");
                con.Close();
            }
        }
        return PnlDt;
    }

    //protected void grdServiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {           
    //        GridViewRow gvMasterRow = (GridViewRow)e.Row.Parent.Parent.Parent.Parent;
    //        GridView gvSummary = ((GridView)e.Row.Parent.Parent.Parent.FindControl("grdServiceDetails"));            
    //        string ParentKey = grdPealDetails.DataKeys[gvMasterRow.RowIndex].Values[0].ToString();
    //        string ChildKey = gvSummary.DataKeys[e.Row.RowIndex].Values[0].ToString();
    //        Label lblapplieddate = (Label)e.Row.FindControl("lblaplieddate");
    //        DataTable dtchild = new DataTable();
    //        dtchild = DisplayAppliedServiceDetails(Convert.ToInt32(ParentKey), Convert.ToInt32(ChildKey));
    //        //for (int i = 0; i <= gvSummary.Rows.Count - 1; i++)
    //        //{
    //        //    lblapplieddate.Text = dtchild.Rows[i]["Applieddate"].ToString();
    //        //}
    //    }
    //}
    //private DataTable DisplayAppliedServiceDetails(int ParentKey,int ChildKey)
    //{
    //    DataTable PnlDt = new DataTable();
    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        try
    //        {
    //            using (SqlCommand cmd = new SqlCommand("USP_PEAL_MisReport_MapPealService"))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.Add("@P_CHAR_ACTION", SqlDbType.VarChar).Value = "ServiceDetails";
    //                cmd.Parameters.Add("@P_VCH_PEAL", SqlDbType.VarChar).Value = txtpeal.Text;
    //                cmd.Parameters.Add("@P_VCH_INV_NAME", SqlDbType.VarChar).Value = txtcompany.Text;
    //                cmd.Parameters.Add("@P_INT_INVESTOR_ID", SqlDbType.Int).Value = Convert.ToInt32(ParentKey);
    //                cmd.Parameters.Add("@P_INT_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(ChildKey);
    //                cmd.Parameters.Add("@P_INT_INVEST_AMT", SqlDbType.Int).Value = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
    //                cmd.Parameters.Add("@pIntPageIndex", SqlDbType.Int).Value = pageIndex;
    //                cmd.Parameters.Add("@pIntPageSize", SqlDbType.Int).Value = pageSize;
    //                cmd.Parameters.Add("@PDtmFromDate", SqlDbType.VarChar).Value = txtFromDate.Text.Trim();
    //                cmd.Parameters.Add("@PDtmToDate", SqlDbType.VarChar).Value = txtToDate.Text.Trim();
    //                cmd.Connection = con;
    //                con.Open();
    //                PnlDt.Load(cmd.ExecuteReader());
    //                con.Close();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "PealService");
    //            con.Close();
    //        }
    //    }
    //    return PnlDt;
    //}
    protected void lnkbtnmode_Click(object sender, EventArgs e)
    {
        if (lnkbtnmode.Text == "All")
        {
            lnkbtnmode.Text = "Paging";           
            pageIndex = 1;
            pageSize = Convert.ToInt16(litTotalRecord.Text);
            DisplayPeal();
            litEnd.Text = litTotalRecord.Text;
            divPaging.Visible = false;
        }
        else
        {
            lnkbtnmode.Text = "All";           
            pageIndex = 1;
            DisplayPeal();
            divPaging.Visible = false;
        }
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("MisPealServiceReport", grdPealDetails);
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("MisPealServiceReport", grdPealDetails, "Mis Report on PEAL", "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        GridViewExportUtil.Export("MisPealServiceReport.xls", grdPealDetails);
        DisplayPeal();

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

    protected void ddlrecord_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageIndex = 1;
        pageSize = Convert.ToInt32(ddlrecord.SelectedValue);
        DisplayPeal();
        lnkbtnmode.Text = "All";       
        divPaging.Visible = false;
    }
}