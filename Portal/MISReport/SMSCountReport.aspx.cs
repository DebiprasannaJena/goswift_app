using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Text;

public partial class Portal_MISReport_SMSCountReport : System.Web.UI.Page
{
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TxtFromdate.Attributes.Add("readonly", "readonly");
            TxtTodate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);

            try
            {

                FillGrid();
                FillSMSType();


            }
            catch (Exception ex)
            {
                Util.LogError(ex, "SMScountreport");
            }


        }

    }
    private void DisplayPaging()
    {
        try
        {
            if (this.GrdSMS.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                GrdSMS.Visible = true;
            }
            if (this.GrdSMS.PageIndex + 1 == this.GrdSMS.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdSMS.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdSMS.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)GrdSMS.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((GrdSMS.PageSize - 1))) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SMScountreport");
        }

    }

    private void FillSMSType()
    {
        try
        {


            SMSCountReport objSearch = new SMSCountReport()
            {
                strActionCode = "VST",
            };

            List<SMSCountReport> list = MisReportServices.View_SMSType(objSearch);

            lbSmsType.DataSource = list;
            lbSmsType.DataTextField = "StrSMSType";
            lbSmsType.DataValueField = "StrSMSType";
            lbSmsType.DataBind();

        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    private void FillGrid()
    {
        try
        {


            /*---------------------------------------------------------------*/
            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            /*---------------------------------------------------------------*/
            List<string> selectedItems = new List<string>();
            foreach (ListItem item in lbSmsType.Items)
            {
                if (item.Selected)
                {
                    selectedItems.Add(item.Value);
                }
            }


            string selectedSmsTypes = string.Join(",", selectedItems);

            SMSCountReport objSearch = new SMSCountReport()
            {
                strActionCode = "SCR",
                StrFromDate = string.IsNullOrEmpty(TxtFromdate.Text.Trim()) ? strFromDate : TxtFromdate.Text.Trim(),
                StrToDate = string.IsNullOrEmpty(TxtTodate.Text.Trim()) ? strToDate : TxtTodate.Text.Trim(),
                // StrSMSType= Convert.ToString(string.IsNullOrEmpty(Convert.ToString(lbSmsType.SelectedValue)) ? "" : Convert.ToString(lbSmsType.SelectedValue)),
                StrSMSType = string.IsNullOrEmpty(selectedSmsTypes) ? "" : selectedSmsTypes,

            };
            List<SMSCountReport> list = MisReportServices.View_SMSCounrReport(objSearch);
            GrdSMS.DataSource = list;
            GrdSMS.DataBind();
            GridViewExcel.DataSource = list;
            GridViewExcel.DataBind();

            GridViewRow gRowFooter = GrdSMS.FooterRow;
            gRowFooter.Cells[1].Text = "<span class='bold-cell'>Total</span>";
            gRowFooter.Cells[2].Text = "<span class='bold-cell'>" + IncentiveCommonFunctions.FormatString(list.Sum(x => x.intNumberofSMS).ToString()) + "</span>";

            GridViewRow grdRowFooter = GridViewExcel.FooterRow;
            grdRowFooter.Cells[1].Text = "<span class='bold-cell'>Total</span>";
            grdRowFooter.Cells[2].Text = "<span class='bold-cell'>" + IncentiveCommonFunctions.FormatString(list.Sum(x => x.intNumberofSMS).ToString()) + "</span>";

            string SearchDetails = string.Empty;
            SearchDetails = "Search Detail For : ";

            if (lbSmsType.Text.Trim() == "")
            {
                SearchDetails = SearchDetails + " SMS Type :- " + "" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " SMS Type :- " + selectedSmsTypes + " | ";
            }

            if (TxtFromdate.Text.Trim() == "")
            {
                SearchDetails = SearchDetails + " From Date :- " + strFromDate + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " From Date :- " + TxtFromdate.Text.Trim() + " | ";
            }
            if (TxtTodate.Text.Trim() == "")
            {
                SearchDetails = SearchDetails + " To Date :- " + strToDate;
            }
            else
            {
                SearchDetails = SearchDetails + " To Date :- " + TxtTodate.Text.Trim();
            }



            LblSearchDetails.Text = SearchDetails;
            intRetVal = list.Count;
            if (list.Count > 0)
            {
                DisplayPaging();
            }
            else
            {
                lblPaging.Visible = false;
                LbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    private void ShowHideHyperlink(HyperLink hyp, Label lbl, GridViewRow gRow, string strActionCode, string strPageName)
    {
        if (!string.IsNullOrEmpty(hyp.Text) && hyp.Text != "-" && hyp.Text != "0")
        {

            hyp.Visible = true;
            StringBuilder strNavigateUrl = new StringBuilder();
            strNavigateUrl.Append("~/Portal/MISReport/");
            strNavigateUrl.Append(strPageName);
            strNavigateUrl.Append("?SMSTypeBind=");
            strNavigateUrl.Append(strActionCode);
            strNavigateUrl.Append("&SMSSelectedType=");
            strNavigateUrl.Append(lbSmsType.SelectedValue);
            strNavigateUrl.Append("&fDate=");
            strNavigateUrl.Append(TxtFromdate.Text.Trim());
            strNavigateUrl.Append("&tDate=");
            strNavigateUrl.Append(TxtTodate.Text.Trim());
            hyp.NavigateUrl = strNavigateUrl.ToString();

        }
        else
        {
            if (lbl != null)
            {
                lbl.Visible = true;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SMScountreport");
        }
    }



    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.ExportToExcel("SMSCountReport", GridViewExcel, "SMS Count details report ", LblSearchDetails.Text + "<br/>Report generation date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SMScountreport");
        }
    }

    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (LbtnAll.Text == "All")
            {
                LbtnAll.Text = "Paging";
                GrdSMS.PageIndex = 0;
                GrdSMS.AllowPaging = false;
                FillGrid();
            }
            else
            {
                LbtnAll.Text = "All";
                GrdSMS.AllowPaging = true;
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SMScountreport");
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {


    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("SMSCountReport", GridViewExcel);
    }






    protected void GrdSMS_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hidStrSMSType = (HiddenField)e.Row.FindControl("HidStrSMSType");
            string strSMSTypeValue = hidStrSMSType.Value;
            HyperLink hypNumberofSMS = (HyperLink)e.Row.FindControl("hypNumberofSMS");
            ShowHideHyperlink(hypNumberofSMS, null, e.Row, strSMSTypeValue, "ChildSMSDetailsReport.aspx");

        }

    }

    protected void GrdSMS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdSMS.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SMScountreport");
        }
    }
}