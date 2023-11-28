using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicDashBoardPeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            string strFromDate = string.Empty, strTodate = string.Empty;
            GetDefaultFromAndToDate(out strFromDate, out strTodate);
            txtFromDate.Text = strFromDate;
            txtToDate.Text = strTodate;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues('" + strFromDate + "','" + strTodate + "');</script>", false);
            drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("-ALL-", "0"));
            drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
            drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("50-1000 CR (SLSWCA)", "2"));
            drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("> 1000 Cr (HLCA)", "3"));
            BindSector();
            BindDistrict();
            fillGridview();
            //Added on 13-10-2022 by Arabinda Tripathy
            DateTime crdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            divLastUpdate.InnerText = "Last reviewed and updated on : " + crdate.ToString("dd-MMM-yyyy"); //DateTime.Now.ToShortDateString(); //#aaaeb7
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
            Util.LogError(ex, "PublicDashboardProposalMIS");
        }
    }
    private void GetDefaultFromAndToDate(out string strFromDate, out string strToDate)
    {
        strFromDate = string.Empty;
        strToDate = string.Empty;
        int intMonth = DateTime.Today.Month;
        if (intMonth == 1)
        {
            strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        else
        {
            strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString().Substring(0, 3) + "-" + (DateTime.Today.Year).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }
    private void BindSector()
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "PublicDashboardProposalMIS");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
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
            fillGridview();
        }
    }

    private void fillGridview()
    {
        try
        {
            // int intLevelId = Convert.ToInt32(Session["LevelID"]);
            grdPealDetails.DataSource = null;
            grdPealDetails.DataBind();

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

            PealSearch objSearch = new PealSearch()
            {
                strActionCode = "V",
                intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
                intProjectType = 0,
                intSectorId = ddlSector.SelectedIndex > 0 ? Convert.ToInt32(ddlSector.SelectedValue) : 0,
                strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
                strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim(),
                intInvestmentAmt = drpInvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(drpInvestmentAmt.SelectedValue) : 0,
                intUserId = Convert.ToInt32(0)
            };

            //if (Session["desId"].ToString() == "97")
            //{
            //    objSearch.intProjectType = 2;
            //    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            //}

            //if ((Convert.ToInt32(Session["UserId"]) == 166) || (Convert.ToInt32(Session["UserId"]) == 167)) //IT Toursim
            //{
            //    objSearch.intProjectType = 2;
            //    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            //}

            //if (intLevelId == 4)
            //{
            //    if (Session["desId"].ToString() == "126") ///// For collector
            //    {
            //        objSearch.strActionCode = "c";
            //        objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            //        objSearch.intUserId = 0;
            //    }
            //    else
            //    {
            //        /*---------------------------------------------------------------*/
            //        //// In case of DIC user,If you want to display MIS report for respective district then use action 'U'
            //        //// In case of DIC user,If you want to display MIS report for all districts then use action 'V' (As Admin User)
            //        /*---------------------------------------------------------------*/
            //        objSearch.strActionCode = "V"; //// Added by Sushant Jena On Dt:- 12-Feb-2020
            //        //objSearch.strActionCode = "u"; //// Commented by Sushant Jena On Dt:- 12-Feb-2020
            //        objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            //        objSearch.intProjectType = 2;
            //        objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            //    }
            //}

            /*---------------------------------------------------------------*/
            //// Display IT and Tourism details with district wise details.
            /*---------------------------------------------------------------*/
            List<PealMisReport> lstPealRpt = new List<PealMisReport>();
            lstPealRpt = MisReportServices.PEAL_MisReportLogic2(objSearch);

            if (ddlSector.SelectedIndex > 0)
            {
                if (ddlSector.SelectedValue == "10") //// IT Sector
                {
                    List<PealMisReport> lstPealRpt1 = new List<PealMisReport>();
                    lstPealRpt1 = BindITPeal();

                    var deliveryModel = new PealMisReport();
                    deliveryModel.intDistrictId = Convert.ToInt32(lstPealRpt1[0].intDistrictId);
                    deliveryModel.strDistrictName = Convert.ToString(lstPealRpt1[0].strDistrictName);
                    deliveryModel.cnt_Total = Convert.ToInt32(lstPealRpt1[0].cnt_Total);
                    deliveryModel.cnt_Pending = Convert.ToInt32(lstPealRpt1[0].cnt_Pending);
                    deliveryModel.cnt_Approved = Convert.ToInt32(lstPealRpt1[0].cnt_Approved);
                    deliveryModel.cnt_rejected = Convert.ToInt32(lstPealRpt1[0].cnt_rejected);
                    deliveryModel.cnt_Query = Convert.ToInt32(lstPealRpt1[0].cnt_Query);
                    deliveryModel.cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt1[0].cnt_Proposed_Emp);
                    deliveryModel.total_Capital_Investment = Convert.ToDecimal(lstPealRpt1[0].total_Capital_Investment);
                    deliveryModel.cnt_landAssessment = Convert.ToInt32(lstPealRpt1[0].cnt_landAssessment);
                    deliveryModel.cnt_landAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_landAllotment);
                    deliveryModel.cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysApproval);
                    deliveryModel.cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysAllotment);
                    deliveryModel.cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysAllotment);
                    deliveryModel.cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysApproval);
                    deliveryModel.cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].cnt_Total_ORTPSAtimeline);
                    deliveryModel.cnt_deferred = Convert.ToInt32(lstPealRpt1[0].cnt_deferred);
                    deliveryModel.cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt1[0].cnt_Land_Allotment_ORTPSA);
                    deliveryModel.cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt1[0].cnt_CarryFwd_pending);
                    deliveryModel.int_Total_Pending = Convert.ToInt32(lstPealRpt1[0].int_Total_Pending);
                    deliveryModel.cnt_median = Convert.ToInt32(lstPealRpt1[0].cnt_median);
                    deliveryModel.intORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].intORTPSAtimeline);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    deliveryModel.intMinApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMinApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    deliveryModel.intMaxApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMaxApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020

                    lstPealRpt.Add(deliveryModel);
                }
                else if (ddlSector.SelectedValue == "38") //// Tourism Sector
                {
                    List<PealMisReport> lstPealRpt1 = new List<PealMisReport>();
                    lstPealRpt1 = BindToursimPeal();

                    var deliveryModel = new PealMisReport();
                    deliveryModel.intDistrictId = Convert.ToInt32(lstPealRpt1[0].intDistrictId);
                    deliveryModel.strDistrictName = Convert.ToString(lstPealRpt1[0].strDistrictName);
                    deliveryModel.cnt_Total = Convert.ToInt32(lstPealRpt1[0].cnt_Total);
                    deliveryModel.cnt_Pending = Convert.ToInt32(lstPealRpt1[0].cnt_Pending);
                    deliveryModel.cnt_Approved = Convert.ToInt32(lstPealRpt1[0].cnt_Approved);
                    deliveryModel.cnt_rejected = Convert.ToInt32(lstPealRpt1[0].cnt_rejected);
                    deliveryModel.cnt_Query = Convert.ToInt32(lstPealRpt1[0].cnt_Query);
                    deliveryModel.cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt1[0].cnt_Proposed_Emp);
                    deliveryModel.total_Capital_Investment = Convert.ToDecimal(lstPealRpt1[0].total_Capital_Investment);
                    deliveryModel.cnt_landAssessment = Convert.ToInt32(lstPealRpt1[0].cnt_landAssessment);
                    deliveryModel.cnt_landAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_landAllotment);
                    deliveryModel.cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysApproval);
                    deliveryModel.cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysAllotment);
                    deliveryModel.cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysAllotment);
                    deliveryModel.cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysApproval);
                    deliveryModel.cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].cnt_Total_ORTPSAtimeline);
                    deliveryModel.cnt_deferred = Convert.ToInt32(lstPealRpt1[0].cnt_deferred);
                    deliveryModel.cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt1[0].cnt_Land_Allotment_ORTPSA);
                    deliveryModel.cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt1[0].cnt_CarryFwd_pending);
                    deliveryModel.int_Total_Pending = Convert.ToInt32(lstPealRpt1[0].int_Total_Pending);
                    deliveryModel.cnt_median = Convert.ToInt32(lstPealRpt1[0].cnt_median);
                    deliveryModel.intORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].intORTPSAtimeline);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    deliveryModel.intMinApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMinApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    deliveryModel.intMaxApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMaxApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    lstPealRpt.Add(deliveryModel);
                }
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    if (i == 0)
                    {
                        List<PealMisReport> lstPealRpt1 = new List<PealMisReport>();
                        lstPealRpt1 = BindITPeal();

                        var deliveryModel = new PealMisReport();
                        deliveryModel.intDistrictId = Convert.ToInt32(lstPealRpt1[0].intDistrictId);
                        deliveryModel.strDistrictName = Convert.ToString(lstPealRpt1[0].strDistrictName);
                        deliveryModel.cnt_Total = Convert.ToInt32(lstPealRpt1[0].cnt_Total);
                        deliveryModel.cnt_Pending = Convert.ToInt32(lstPealRpt1[0].cnt_Pending);
                        deliveryModel.cnt_Approved = Convert.ToInt32(lstPealRpt1[0].cnt_Approved);
                        deliveryModel.cnt_rejected = Convert.ToInt32(lstPealRpt1[0].cnt_rejected);
                        deliveryModel.cnt_Query = Convert.ToInt32(lstPealRpt1[0].cnt_Query);
                        deliveryModel.cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt1[0].cnt_Proposed_Emp);
                        deliveryModel.total_Capital_Investment = Convert.ToDecimal(lstPealRpt1[0].total_Capital_Investment);
                        deliveryModel.cnt_landAssessment = Convert.ToInt32(lstPealRpt1[0].cnt_landAssessment);
                        deliveryModel.cnt_landAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_landAllotment);
                        deliveryModel.cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysApproval);
                        deliveryModel.cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysAllotment);
                        deliveryModel.cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysAllotment);
                        deliveryModel.cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysApproval);
                        deliveryModel.cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].cnt_Total_ORTPSAtimeline);
                        deliveryModel.cnt_deferred = Convert.ToInt32(lstPealRpt1[0].cnt_deferred);
                        deliveryModel.cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt1[0].cnt_Land_Allotment_ORTPSA);
                        deliveryModel.cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt1[0].cnt_CarryFwd_pending);
                        deliveryModel.int_Total_Pending = Convert.ToInt32(lstPealRpt1[0].int_Total_Pending);
                        deliveryModel.cnt_median = Convert.ToInt32(lstPealRpt1[0].cnt_median);
                        deliveryModel.intORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].intORTPSAtimeline);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                        deliveryModel.intMinApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMinApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                        deliveryModel.intMaxApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMaxApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                        lstPealRpt.Add(deliveryModel);
                    }

                    if (i == 1)
                    {
                        List<PealMisReport> lstPealRpt1 = new List<PealMisReport>();
                        lstPealRpt1 = BindToursimPeal();

                        var deliveryModel = new PealMisReport();
                        deliveryModel.intDistrictId = Convert.ToInt32(lstPealRpt1[0].intDistrictId);
                        deliveryModel.strDistrictName = Convert.ToString(lstPealRpt1[0].strDistrictName);
                        deliveryModel.cnt_Total = Convert.ToInt32(lstPealRpt1[0].cnt_Total);
                        deliveryModel.cnt_Pending = Convert.ToInt32(lstPealRpt1[0].cnt_Pending);
                        deliveryModel.cnt_Approved = Convert.ToInt32(lstPealRpt1[0].cnt_Approved);
                        deliveryModel.cnt_rejected = Convert.ToInt32(lstPealRpt1[0].cnt_rejected);
                        deliveryModel.cnt_Query = Convert.ToInt32(lstPealRpt1[0].cnt_Query);
                        deliveryModel.cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt1[0].cnt_Proposed_Emp);
                        deliveryModel.total_Capital_Investment = Convert.ToDecimal(lstPealRpt1[0].total_Capital_Investment);
                        deliveryModel.cnt_landAssessment = Convert.ToInt32(lstPealRpt1[0].cnt_landAssessment);
                        deliveryModel.cnt_landAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_landAllotment);
                        deliveryModel.cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysApproval);
                        deliveryModel.cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysAllotment);
                        deliveryModel.cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysAllotment);
                        deliveryModel.cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysApproval);
                        deliveryModel.cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].cnt_Total_ORTPSAtimeline);
                        deliveryModel.cnt_deferred = Convert.ToInt32(lstPealRpt1[0].cnt_deferred);
                        deliveryModel.cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt1[0].cnt_Land_Allotment_ORTPSA);
                        deliveryModel.cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt1[0].cnt_CarryFwd_pending);
                        deliveryModel.int_Total_Pending = Convert.ToInt32(lstPealRpt1[0].int_Total_Pending);
                        deliveryModel.cnt_median = Convert.ToInt32(lstPealRpt1[0].cnt_median);
                        deliveryModel.intORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].intORTPSAtimeline);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                        deliveryModel.intMinApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMinApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                        deliveryModel.intMaxApprovalDays = Convert.ToInt32(lstPealRpt1[0].intMaxApprovalDays);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                        lstPealRpt.Add(deliveryModel);
                    }
                }
            }

            /*---------------------------------------------------------------*/

            grdPealDetails.DataSource = lstPealRpt;
            grdPealDetails.DataBind();

            /*---------------------------------------------------------------*/

            if (grdPealDetails.Rows.Count > 0)
            {
                GridViewRow gRowFooter = grdPealDetails.FooterRow;
                gRowFooter.Cells[1].Text = "Total";
                gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Proposed_Emp).ToString());
                gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(lstPealRpt.Sum(x => x.total_Capital_Investment).ToString());
                gRowFooter.Cells[13].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysApproval.ToString());
                gRowFooter.Cells[14].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAssessment).ToString());
                gRowFooter.Cells[15].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAllotment).ToString());
                gRowFooter.Cells[16].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysAllotment.ToString());
                gRowFooter.Cells[17].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Land_Allotment_ORTPSA).ToString());


                Label lblCarryFwdPendingFooter = (Label)gRowFooter.FindControl("lblCarryFwdPendingFooter");
                lblCarryFwdPendingFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_CarryFwd_pending).ToString());

                Label lblRcvdFooter = (Label)gRowFooter.FindControl("lblRcvdFooter");
                lblRcvdFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total).ToString());

                Label lblApprovedFooter = (Label)gRowFooter.FindControl("lblApprovedFooter");
                lblApprovedFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Approved).ToString());

                Label lblRejectedFooter = (Label)gRowFooter.FindControl("lblRejectedFooter");
                lblRejectedFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_rejected).ToString());

                Label lblDefferecFooter = (Label)gRowFooter.FindControl("lblDefferecFooter");
                lblDefferecFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_deferred).ToString());

                Label lblQuery1Footer = (Label)gRowFooter.FindControl("lblQuery1Footer");
                lblQuery1Footer.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Query).ToString());

                Label lblPendingFooter = (Label)gRowFooter.FindControl("lblPendingFooter");
                lblPendingFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Pending).ToString());

                Label lblTotalPendingFooter = (Label)gRowFooter.FindControl("lblTotalPendingFooter");
                lblTotalPendingFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.int_Total_Pending).ToString());

                Label lblORTPSFooter = (Label)gRowFooter.FindControl("lblORTPSFooter");
                lblORTPSFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total_ORTPSAtimeline).ToString());
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PublicDashboardProposalMIS");
        }
    }
    private List<PealMisReport> BindITPeal()
    {

        //int intLevelId = Convert.ToInt32(Session["LevelID"]);
        grdPealDetails.DataSource = null;
        grdPealDetails.DataBind();

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
        PealSearch objSearch = new PealSearch()
        {
            strActionCode = "IT",
            intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
            intProjectType = 0,
            intSectorId = ddlSector.SelectedIndex > 0 ? Convert.ToInt32(ddlSector.SelectedValue) : 0,
            strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
            strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim(),
            intInvestmentAmt = drpInvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(drpInvestmentAmt.SelectedValue) : 0,
            intUserId = Convert.ToInt32(0)
        };
        //if (Session["desId"].ToString() == "97")
        //{
        //    objSearch.intProjectType = 2;
        //    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        //}
        //if ((Convert.ToInt32(Session["UserId"]) == 167)) //IT 
        //{
        //    objSearch.intProjectType = 2;
        //    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        //}
        //if (intLevelId == 4)
        //{
        //    if (Session["desId"].ToString() == "126")
        //    {
        //        objSearch.strActionCode = "Ic";
        //        objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        //        objSearch.intUserId = 0;
        //    }
        //    else
        //    {
        //        objSearch.strActionCode = "Iu";
        //        objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        //        objSearch.intProjectType = 2;
        //        objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        //    }
        //}
        List<PealMisReport> lstPealRpt1 = new List<PealMisReport>();
        lstPealRpt1 = MisReportServices.PEAL_MisReportLogic2(objSearch);
        return lstPealRpt1;

    }
    private List<PealMisReport> BindToursimPeal()
    {

        //int intLevelId = Convert.ToInt32(Session["LevelID"]);
        grdPealDetails.DataSource = null;
        grdPealDetails.DataBind();

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
        PealSearch objSearch = new PealSearch()
        {
            strActionCode = "TOURSIM",
            intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
            intProjectType = 0,
            intSectorId = ddlSector.SelectedIndex > 0 ? Convert.ToInt32(ddlSector.SelectedValue) : 0,
            strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
            strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim(),
            intInvestmentAmt = drpInvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(drpInvestmentAmt.SelectedValue) : 0,
            intUserId = Convert.ToInt32(0)
        };
        //if (Session["desId"].ToString() == "97")
        //{
        //    objSearch.intProjectType = 2;
        //    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        //}
        //if ((Convert.ToInt32(Session["UserId"]) == 166)) //Toursim
        //{
        //    objSearch.intProjectType = 2;
        //    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        //}
        //if (intLevelId == 4)
        //{
        //    if (Session["desId"].ToString() == "126")
        //    {
        //        objSearch.strActionCode = "Tc";
        //        objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        //        objSearch.intUserId = 0;
        //    }
        //    else
        //    {
        //        objSearch.strActionCode = "Tu";
        //        objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        //        objSearch.intProjectType = 2;
        //        objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
        //    }
        //}
        List<PealMisReport> lstPealRpt1 = new List<PealMisReport>();
        lstPealRpt1 = MisReportServices.PEAL_MisReportLogic2(objSearch);
        return lstPealRpt1;
    }

    protected void grdPealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //int intLevelId = Convert.ToInt32(Session["levelId"]);
            //hdnLavelVal.Value = intLevelId.ToString();
            //hdnDesgid.Value = Session["desId"].ToString();
            for (int cnt = 2; cnt < e.Row.Cells.Count; cnt++)
            {
                e.Row.Cells[cnt].Style["text-align"] = "right";
            }
        }
    }
}