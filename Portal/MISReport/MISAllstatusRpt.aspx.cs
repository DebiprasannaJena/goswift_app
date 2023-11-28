/*
 * File Name : MISAllstatusRpt.aspx
 * Class Name : Portal_MISReport_MISAllstatusRpt
 * Created By :
 * Created On :
 * 
 */


using System;
using System.Collections.Generic;
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


public partial class Portal_MISReport_MISAllstatusRpt : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
            try
            {
                int intLevelId = Convert.ToInt32(Session["LevelID"]);
                int designationId = Convert.ToInt32(Session["desId"].ToString());
                BindDistrict();
                if (intLevelId == 1)
                {
                    if (designationId == 97) //psmsme user
                    {
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        drpInvestmentAmt.SelectedIndex = 0;
                    }
                    else // all other admin user
                    {
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("-ALL-", "0"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("50-1000 CR (SLSWCA)", "2"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("> 1000 Cr (HLCA)", "3"));
                    }

                }
                else if (intLevelId == 4)
                {
                    SetDrpForDistrictUser();
                    if (designationId == 126)
                    {
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("-ALL-", "0"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("50-1000 CR (SLSWCA)", "2"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("> 1000 Cr (HLCA)", "3"));
                    }
                    else
                    {
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        drpInvestmentAmt.SelectedIndex = 0;
                    }
                }
                BindSector();
                fillGridview();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ProposalMIS");
            }

        }
    }

    private void SetDrpForDistrictUser()
    {
        int intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());

        System.Web.UI.WebControls.ListItem lstDistrict= ddlDistrict.Items.FindByValue(intDistrictid.ToString());

        ddlDistrict.Items.Clear();
        ddlDistrict.Items.Add(lstDistrict);
        ddlDistrict.SelectedIndex = 0;
    }



    public void PopYear(DropDownList selYear)
    {

        int currentyear = DateTime.Now.Year;
        int startyear = 2016;
        selYear.Items.Clear();
        int intMonth = DateTime.Now.Month;
        if (intMonth >= 1 && intMonth <= 3)
        {
            currentyear = currentyear - 1;
        }
        for (int j = startyear; j <= currentyear; j++)
        {
            selYear.Items.Add(new System.Web.UI.WebControls.ListItem(j.ToString() + "-" + (j + 1).ToString().Substring(2, 2), j.ToString()));
        }
        selYear.Items.Insert(0, (new System.Web.UI.WebControls.ListItem("--Select--", "0")));
    }

    public void fillGridview()
    {
        int intLevelId = Convert.ToInt32(Session["LevelID"]);
        grdPealDetails.DataSource = null;
        grdPealDetails.DataBind();

        string strFromDate = string.Empty;
        string strToDate = string.Empty;
        int intMonth =DateTime.Today.Month;
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

        PealSearch objSearch = new PealSearch()
        {
            strActionCode = "V",
            intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
            intProjectType = 0,
            intSectorId = ddlSector.SelectedIndex > 0 ? Convert.ToInt32(ddlSector.SelectedValue) : 0,
            strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
            strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim(),
            intInvestmentAmt = drpInvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(drpInvestmentAmt.SelectedValue) : 0,
            intUserId = Convert.ToInt32(Session["UserId"])
        };
        if (Session["desId"].ToString() == "97")
        {
            objSearch.intProjectType = 2;
            objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        }
        if (intLevelId == 4)
        {
            if (Session["desId"].ToString() == "126")
            {
                objSearch.strActionCode = "c";
                objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                objSearch.intUserId = 0;
            }
            else
            {
                objSearch.strActionCode = "u";
                objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }
        }
        List<PealMisReport> lstPealRpt = new List<PealMisReport>();

        lstPealRpt = MisReportServices.PealUserwiseMisRpt(objSearch);

        grdPealDetails.DataSource = lstPealRpt;
        grdPealDetails.DataBind();

        if (grdPealDetails.Rows.Count > 0)
        {
            GridViewRow gRowFooter = grdPealDetails.FooterRow;
            gRowFooter.Cells[1].Text = "Total";
            gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total).ToString());
            gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_rejected).ToString());
            gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_deferred).ToString());
            gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Approved).ToString());
            gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Query).ToString());
            gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Pending).ToString());
            gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_CarryFwd_pending).ToString());
            gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.int_Total_Pending).ToString());
            gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total_ORTPSAtimeline).ToString());
            gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Proposed_Emp).ToString());
            gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(lstPealRpt.Sum(x => x.total_Capital_Investment).ToString());
            gRowFooter.Cells[13].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysApproval.ToString());
            gRowFooter.Cells[14].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAssessment).ToString());
            gRowFooter.Cells[15].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAllotment).ToString());
            gRowFooter.Cells[16].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysAllotment.ToString());
            gRowFooter.Cells[17].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Land_Allotment_ORTPSA.ToString());

        }
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("PealMisReport", grdPealDetails);
    }

    private void CreatePdfWith_Header_Footer(string strFilename, Control grd)
    {
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFilename + ".pdf");
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        grd.RenderControl(htmlTextWriter);

        StringReader stringReader = new StringReader(stringWriter.ToString());
        Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(Doc);
        PdfWriter.GetInstance(Doc, HttpContext.Current.Response.OutputStream);

        Doc.Open();

        htmlparser.Parse(stringReader);
        Doc.Close();
        HttpContext.Current.Response.Output.Write(stringWriter);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }


    protected void grdPealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intLevelId = Convert.ToInt32(Session["levelId"]);
            hdnLavelVal.Value = intLevelId.ToString();
            hdnDesgid.Value = Session["desId"].ToString();

            for (int cnt=2; cnt < e.Row.Cells.Count; cnt++)
            {
                e.Row.Cells[cnt].Style["text-align"] = "right";
            }
        }
    }

    private void BindDistrict()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();
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
            Util.LogError(ex, "ProposalMIS");
        }
    }

    private void BindSector()
    {
        ProposalBAL objService = new ProposalBAL();
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "SE";
        objProp.vchProposalNo = "";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlSector.DataSource = objProjList;
        ddlSector.DataTextField = "vchSectorName";
        ddlSector.DataValueField = "intSectorId";
        ddlSector.DataBind();
        System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlSector.Items.Insert(0, list);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridview();
    }
    public void fillGridview1()
    {
        int intLevelId = Convert.ToInt32(Session["LevelID"]);
        GridView1.DataSource = null;
        GridView1.DataBind();

        string strFromDate = string.Empty;
        string strToDate = string.Empty;
        int intMonth =DateTime.Today.Month;
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

        PealSearch objSearch = new PealSearch()
        {
            strActionCode = "V",
            intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
            intProjectType = 0,
            intSectorId = ddlSector.SelectedIndex > 0 ? Convert.ToInt32(ddlSector.SelectedValue) : 0,
            strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
            strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim(),
            intInvestmentAmt = drpInvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(drpInvestmentAmt.SelectedValue) : 0,
            intUserId = Convert.ToInt32(Session["UserId"])
        };
        if (Session["desId"].ToString() == "97")
        {
            objSearch.intProjectType = 2;
            objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        }
        if (intLevelId == 4)
        {
            if (Session["desId"].ToString() == "126")
            {
                objSearch.strActionCode = "c";
                objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                objSearch.intUserId = 0;
            }
            else
            {
                objSearch.strActionCode = "u";
                objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }
        }
        List<PealMisReport> lstPealRpt = new List<PealMisReport>();
        ProposalBAL objProposalBal = new ProposalBAL();
        lstPealRpt = MisReportServices.PealUserwiseMisRpt(objSearch);

        GridView1.DataSource = lstPealRpt;
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
        {
            GridViewRow gRowFooter = GridView1.FooterRow;
            gRowFooter.Cells[1].Text = "Total";
            gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total).ToString());
            gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_rejected).ToString());
            gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_deferred).ToString());
            gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Approved).ToString());
            gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Query).ToString());
            gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Pending).ToString());
            gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_CarryFwd_pending).ToString());
            gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.int_Total_Pending).ToString());
            gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total_ORTPSAtimeline).ToString());
            gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Proposed_Emp).ToString());
            gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(lstPealRpt.Sum(x => x.total_Capital_Investment).ToString());
            gRowFooter.Cells[13].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysApproval.ToString());
            gRowFooter.Cells[14].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAssessment).ToString());
            gRowFooter.Cells[15].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAllotment).ToString());
            gRowFooter.Cells[16].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysAllotment.ToString());
            gRowFooter.Cells[17].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Land_Allotment_ORTPSA.ToString());

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        //IncentiveCommonFunctions.ExportToExcel("PealQueryReportDtls", grdPealDetails, "Mis Report on PEAL", "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        //GridViewExportUtil.Export("MISReport.xls", grdPealDetails);
        fillGridview1();

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

}