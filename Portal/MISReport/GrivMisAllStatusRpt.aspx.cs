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
using EntityLayer.GrievanceEntity;
using EntityLayer.Proposal;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Portal_MISReport_GrivMisAllStatusRpt : System.Web.UI.Page
{
    ///// Page Load     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);

            try
            {
              

                BindDistrict();
                BindGrievanceType();

                if (Convert.ToString(Session["desId"]) == "126") ////Collector
                {
                    int intDistrictId = GetDistrictIdByUser();
                    ddlDistrict.SelectedValue = intDistrictId.ToString();
                    ddlDistrict.Enabled = false;
                    FillGridView();
                }
                else
                {
                    ddlDistrict.Enabled = true;
                    FillGridView();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "GrievanceMIS");
            }
        }
    }

    #region FunctionUsed 

    private int GetDistrictIdByUser()
    {
        int intDistrictId = 0;

        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();

        try
        {
            objGrivEntity.intUserId = Convert.ToInt32(Session["UserId"]);

            ////Get District Id By User
            DataTable dt = objBAL.GetDistrictIdByUser(objGrivEntity);
            if (dt.Rows.Count > 0)
            {
                intDistrictId = Convert.ToInt32(dt.Rows[0]["intDistrict"]);
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
       
        return intDistrictId;
    }
    private void FillGridView()
    {
        try
        {
            GrdGrivDetails.DataSource = null;
            GrdGrivDetails.DataBind();

            /*---------------------------------------------------------------*/
            string strFromDate = string.Empty;
            string strToDate = string.Empty;
            int intMonth = DateTime.Today.Month;
            if (intMonth == 1)
            {
                strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            /*---------------------------------------------------------------*/
            GrievanceMisSearch objSearch = new GrievanceMisSearch()
            {
                StrActionCode = "V",
                IntDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
                IntGrivTypeId = drpGrievance.SelectedIndex > 0 ? Convert.ToInt32(drpGrievance.SelectedValue) : 0,
                StrFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
                StrToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim()
            };
            /*---------------------------------------------------------------*/

          
            DataTable dt = MisReportServices.GetGrievanceMISReportDtls(objSearch);

            GrdGrivDetails.DataSource = dt;
            GrdGrivDetails.DataBind();

            /*---------------------------------------------------------------*/
            if (GrdGrivDetails.Rows.Count > 0)
            {
                GridViewRow gRowFooter = GrdGrivDetails.FooterRow;
                gRowFooter.Cells[1].Text = "Total";

                Label LblCarryFwdPendingFooter = (Label)gRowFooter.FindControl("LblCarryFwdPendingFooter");
                LblCarryFwdPendingFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_CarryForwardPending")).ToString());

                Label LblRcvdFooter = (Label)gRowFooter.FindControl("LblRcvdFooter");
                LblRcvdFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_Total")).ToString());

                Label LblApprovedFooter = (Label)gRowFooter.FindControl("LblApprovedFooter");
                LblApprovedFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_Approved")).ToString());

                Label LblRejectedFooter = (Label)gRowFooter.FindControl("LblRejectedFooter");
                LblRejectedFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_rejected")).ToString());

                Label LblDeferredFooter = (Label)gRowFooter.FindControl("LblDeferredFooter");
                LblDeferredFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_Deferred")).ToString());

                Label LblForwardedFooter = (Label)gRowFooter.FindControl("LblForwardedFooter");
                LblForwardedFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_forwarded")).ToString());

                Label LblPendingFooter = (Label)gRowFooter.FindControl("LblPendingFooter");
                LblPendingFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_Pending")).ToString());

                Label LblTotalPendingFooter = (Label)gRowFooter.FindControl("LblTotalPendingFooter");
                LblTotalPendingFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_TotalPending")).ToString());

                Label LblORTPSAFooter = (Label)gRowFooter.FindControl("LblORTPSAFooter");
                LblORTPSAFooter.Text = IncentiveCommonFunctions.FormatString(dt.AsEnumerable().Sum(row => row.Field<int>("cnt_Total_Pending30days")).ToString());
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GrievanceMIS");
        }
    }

    /// <summary>
    /// Function used to bind all districts
    /// </summary>
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
            Util.LogError(ex, "GrievanceMIS");
        }
    }

    /// <summary>
    /// Function used to fill all sectors
    /// </summary>
    private void BindGrievanceType()
    {
        DataTable dt = new DataTable();
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BGT"
            };
            dt = GrievanceServices.FillGrievanceType(objSearch);
            drpGrievance.DataSource = dt;
            drpGrievance.DataTextField = "vchGrivType";
            drpGrievance.DataValueField = "intGrivTypeId";
            drpGrievance.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpGrievance.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GrievanceMIS");
        }
    }

    #endregion

    #region ButtonClickEvent

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                str = "jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                str = "jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (Convert.ToDateTime(txtFromDate.Text.Trim()) > Convert.ToDateTime(txtToDate.Text.Trim()))
            {
                str = "jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else
            {
                FillGridView();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GrievanceMIS");
        }
    }

    #endregion

    protected void GrdGrivDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intLevelId = Convert.ToInt32(Session["levelId"]);
            hdnLavelVal.Value = intLevelId.ToString();
            hdnDesgid.Value = Session["desId"].ToString();

            for (int cnt = 2; cnt < e.Row.Cells.Count; cnt++)
            {
                e.Row.Cells[cnt].Style["text-align"] = "right";
            }
        }
    }

    protected void LnkBtnPdf_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.CreatePdf("GrievanceMisReport", GrdGrivDetails);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GrievanceMIS");
        }
    }
    protected void LnkBtnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            FillGridView();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GrievanceMisReport.xls"));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            viewTable.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GrievanceMIS");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}