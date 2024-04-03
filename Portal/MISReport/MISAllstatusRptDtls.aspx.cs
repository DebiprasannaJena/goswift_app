using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.UI;
using System.Linq;
using System.Text;
using System.Globalization;

public partial class Portal_MISReport_MISAllstatusRptDtls : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                CommonFunctions.PopulatePageSize(DrpDwn_NoOfRec);
                Hdn_Pgindex.Value = "1";
                if (!string.IsNullOrEmpty(Request.QueryString["hdn"]))
                {
                    Hdn_Pgindex.Value = Request.QueryString["hdn"];
                }
                else
                {
                    Hdn_Pgindex.Value = "1";
                }
                if (Request.QueryString["pSize"] != null)
                {
                    DrpDwn_NoOfRec.SelectedValue = Request.QueryString["pSize"];
                }
                else
                {
                    DrpDwn_NoOfRec.SelectedValue = "10";
                }
                FillGrid(Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ProposalMIS");
            }
           
        }
    }

    #region Data Paging
    /// <summary>
    /// Click event for all the link button created for the paging control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            Hdn_Pgindex.Value = (sender as LinkButton).CommandArgument;
            FillGrid(Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }

    /// <summary>
    /// selected index change event for the dropdown that contains different size for the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNoOfRec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Hdn_Pgindex.Value = "1";
            FillGrid(Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }
    #endregion

    protected void GrdPealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try 
        { 
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
              int Rowid = 0;
              if (Convert.ToInt32(Hdn_Pgindex.Value) > 1)
              {
                  Rowid = (Convert.ToInt32(Hdn_Pgindex.Value) - 1) * Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
              }
              else
              {
                  Rowid = e.Row.DataItemIndex + 1;
              }
              e.Row.Cells[0].Text = Rowid.ToString();
             
          }
          
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
              HyperLink hypProposalNo = (HyperLink)e.Row.FindControl("hypProposalNo");
              hypProposalNo.NavigateUrl = string.Format("../Proposal/MISproposaldetails.aspx?Pno={0}", hypProposalNo.Text);
          
              HiddenField HdnFieldRemarks =(HiddenField)e.Row.FindControl("HdnFieldRemarks");
          
              e.Row.ToolTip = HdnFieldRemarks.Value;
          }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }

    private void FillGrid(int IntPageIndex, int IntPageSize)
    {
        try
        {
            GrdPealDetails.DataSource = null;
            GrdPealDetails.DataBind();
            divExport.Visible = false;
            Lbl_Search_Details.Text = string.Empty;
            string StrFromDate = string.Empty;
            string StrToDate = string.Empty;
            int IntMonth = DateTime.Today.Month;
            //Changes By Manoj
            int SectorId = 0;
            if (Request.QueryString["Secid"].ToString() != "0")
            {
                SectorId = Convert.ToInt32(Request.QueryString["Secid"].ToString());
            }
            else
            {
                if (Request.QueryString["Secid"].ToString() == "0")
                {
                    if (Request.QueryString["distname"].ToString().Contains("IT"))
                    {
                        SectorId = 10;
                    }
                    else if (Request.QueryString["distname"].ToString().Contains("Tourism"))
                    {
                        SectorId = 38;
                    }
                    else
                    {
                        SectorId = 0;
                    }
                }
                else
                {
                    SectorId = 0;
                }
            }
            //END OF CHANGES
            if (IntMonth == 1)
            {
                StrFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                StrFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + (DateTime.Today.Year).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            PealSearch objSearch = new PealSearch()
            {
                strActionCode = Request.QueryString["Act"],
                intDistrictId = !string.IsNullOrEmpty(Request.QueryString["dstid"]) ? Convert.ToInt32(Request.QueryString["dstid"]) : 0,
                intProjectType = !string.IsNullOrEmpty(Request.QueryString["projctType"]) ? Convert.ToInt32(Request.QueryString["projctType"]) : 0,
                intSectorId = SectorId,
                strFromDate = string.IsNullOrEmpty(Request.QueryString["fDate"]) ? StrFromDate : Request.QueryString["fDate"],
                strToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? StrToDate : Request.QueryString["tDate"],
                intInvestmentAmt = !string.IsNullOrEmpty(Request.QueryString["InvAmt"]) ? Convert.ToInt32(Request.QueryString["InvAmt"]) : 0,
                intPageSize = IntPageSize,
                intIntPageIndex = IntPageIndex,
                intUserId = Convert.ToInt32(Session["UserId"]),
                intStatus = !string.IsNullOrEmpty(Request.QueryString["status"]) ? Convert.ToInt32(Request.QueryString["status"]) : 0
            };

            if (Convert.ToInt32(Session["UserId"]) == 557)// for idco user only
            {
                objSearch.intProjectType = 0;
            }

            List<Mis_ChildServiceDtls> LstDetails = null;

            if (!string.IsNullOrEmpty(Request.QueryString["Logic"]))
            {
                LstDetails = MisReportServices.PEAL_MisReportLogic2_Details(objSearch);
            }
            else
            {
                LstDetails = MisReportServices.PealMisRpt_Details(objSearch);
            }
            Util.LogRequestResponse("PEALMISReport", "GetPEALMISReport", "Data Load Count" + LstDetails.Count.ToString());
           
            GrdPealDetails.DataSource = LstDetails;
            GrdPealDetails.DataBind();

            if (GrdPealDetails.Rows.Count > 0)
            {
                DrpDwn_NoOfRec.Visible = true;
                rptPager.Visible = true;
                divExport.Visible = true;
                CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(LstDetails[0].intRowCount), Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));

                GridViewRow gRowFooter = GrdPealDetails.FooterRow;
                gRowFooter.Cells[1].Text = "Total";
                gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatDecimalString(LstDetails.Sum(x => x.decInvestment).ToString());
                gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(LstDetails.Sum(x => x.intPropEmployment).ToString());
                gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatDecimalString(LstDetails.Sum(x => x.decTotalLandRequired).ToString());
                gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(LstDetails.Sum(x => x.decLandRecommendedToIdco).ToString());

                /****************code to show paging details in the label************/
                int IntPIndex = Convert.ToInt32(Hdn_Pgindex.Value);
                int IntStartIndex = 1, IntEndIndex = 0;
                int IntPSize = Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue);
                IntStartIndex = ((IntPIndex - 1) * IntPSize) + 1;
                if (IntPSize == GrdPealDetails.Rows.Count)
                {
                    IntEndIndex = IntPSize * IntPIndex;
                }
                else
                {
                    IntEndIndex = GrdPealDetails.Rows.Count + (IntPSize * (IntPIndex - 1));
                }
                Lbl_Details.Text = IntStartIndex.ToString() + "-" + IntEndIndex.ToString() + " of " + Convert.ToInt32(LstDetails[0].intRowCount).ToString();

                StringBuilder strSearch = new StringBuilder();
                if (!string.IsNullOrEmpty(Request.QueryString["dstid"]) && Request.QueryString["dstid"] != "0")
                {
                    strSearch.Append("<strong>District - </strong>");
                    strSearch.Append(LstDetails[0].ServiceName);
                   
                    strSearch.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["Secid"]) && Request.QueryString["Secid"] != "0")
                {
                    strSearch.Append("<strong>Sector - </strong>");
                    strSearch.Append(LstDetails[0].strSector);
                    strSearch.Append("<br/>");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["fDate"]))
                {
                    strSearch.Append("<strong>From Date - </strong>");
                    strSearch.Append(Convert.ToDateTime(Request.QueryString["fDate"]).ToString("dd-MMM-yyyy"));
                    
                    strSearch.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["tDate"]))
                {
                    strSearch.Append("<strong>To Date - </strong>");
                    strSearch.Append(Convert.ToDateTime(Request.QueryString["tDate"]).ToString("dd-MMM-yyyy"));
                    strSearch.Append("<br/>");
                    
                }
                if (!string.IsNullOrEmpty(Request.QueryString["InvAmt"]) && Request.QueryString["InvAmt"] != "0")
                {
                    strSearch.Append("<strong>Investment Amount - </strong>");
                    string strIndType = Request.QueryString["InvAmt"];
                    if (strIndType == "1")
                    {
                        strSearch.Append("less than 50 crore");
                        strSearch.Append("<br/>");
                    }
                    else if (strIndType == "2")
                    {
                        strSearch.Append("greater than 50 crore");
                        strSearch.Append("<br/>");
                    }
                    else if (strIndType == "3")
                    {
                        strSearch.Append("greater than 1000 crore");
                        strSearch.Append("<br/>");
                    }

                }
                if (!string.IsNullOrEmpty(Request.QueryString["Status"]) && Request.QueryString["Status"] != "0")
                {
                    string Status = Request.QueryString["Status"];
                    strSearch.Append("<strong>Status - </strong>");
                    if (Status == "1")
                    {
                        strSearch.Append("<span style='color: blue;font-weight:bold;'>Current period pending</span>");
                    }
                    else if (Status == "2")
                    {
                        strSearch.Append("<span style='color: green;font-weight:bold;'>Approved</span>");
                    }
                    else if (Status == "3")
                    {
                        strSearch.Append("<span style='color: red;font-weight:bold;'>Rejected</span>");
                    }
                    else if (Status == "7")
                    {
                        strSearch.Append("<span style='color: violet;font-weight:bold;'>Deferred</span>");
                    }
                    else if (Status == "-2")
                    {
                        strSearch.Append("Application pending beyond 30 days");
                    }
                    else if (Status == "8")
                    {
                        strSearch.Append("Opening Balance");
                    }
                    else if (Status == "9")
                    {
                        strSearch.Append("Total Pending applications(Opening Balance + Current period pending)");
                    }
                }

                Util.LogRequestResponse("PEALMISReport", "GetPEALMISReport", "Label data show" + strSearch.ToString());
                Lbl_Search_Details.Text = strSearch.ToString();


            }

            else
            {
                DrpDwn_NoOfRec.Visible = false;
                rptPager.Visible = false;
                Hdn_Pgindex.Value = "1";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        try 
        { 
        
        IncentiveCommonFunctions.CreatePdf("PealMisReportDtls", GrdPealDetails);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        { 
        IncentiveCommonFunctions.ExportToExcel("PealMisReportDtls", GrdPealDetails, "Mis Report on PEAL", Lbl_Search_Details.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


}