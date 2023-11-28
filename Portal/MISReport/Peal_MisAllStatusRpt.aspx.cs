//******************************************************************************************************************
// File Name             :   Peal_MisAllStatusRpt.aspx
// Class Name            :   Portal_MISReport_MISAllstatusRpt
// Description           :   MIS report for PEAL
// Created by            :   NA
// Created on            :   NA
// Modification History  :
//       <CR no.>              <Date>             <Modified by>                <Modification Summary>'                                                          
//         1                 12-Feb-2020           Sushant Jena           (1) Access permission to all DIC with all districts.
//                                                                        (2) Code commenting.Exception handling.
//********************************************************************************************************************

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

public partial class Portal_MISReport_Peal_MisAllStatusRpt : System.Web.UI.Page
{
    /// Page Load     
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

                /*-----------------------------------------------------------------------------*/

                if ((Convert.ToInt32(Session["UserId"]) == 166) || (Convert.ToInt32(Session["UserId"]) == 167)) //psmsme user
                {
                    drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                    drpInvestmentAmt.SelectedIndex = 0;
                }
                else if (intLevelId == 4) //// DIC & Collector Level User
                {
                    ///// Set respective district for DIC/Collector user.    
                    // SetDrpForDistrictUser(); //// Commented by Sushant Jena On Dt:13-Feb-2020 in order to display all the district data to DIC user.

                    if (designationId == 126) ///// Collector
                    {
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("-ALL-", "0"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("50-1000 CR (SLSWCA)", "2"));
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("> 1000 Cr (HLCA)", "3"));
                    }
                    else
                    {
                        ///// For DIC
                        drpInvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        drpInvestmentAmt.SelectedIndex = 0;
                    }
                }

                /*-----------------------------------------------------------------------------*/

                BindSector();
                FillGridView();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ProposalMIS");
            }
        }
    }

    #region FunctionUsed

   
  
     

    /// <summary>
    /// This function is used to display MIS report counts in GridView
    /// </summary>
    private void FillGridView()
    {
        try
        {
            GrdPealDetails.DataSource = null;
            GrdPealDetails.DataBind();

            int intLevelId = Convert.ToInt32(Session["LevelID"]);

            /*---------------------------------------------------------------------------------------------*/

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

            /*---------------------------------------------------------------------------------------------*/

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

            if (Session["desId"].ToString() == "97")//Principal Secretary(MSME)
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }

            if ((Convert.ToInt32(Session["UserId"]) == 166) || (Convert.ToInt32(Session["UserId"]) == 167)) //IT Toursim
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }

            if (intLevelId == 4)
            {
                if (Session["desId"].ToString() == "126") ///// For collector
                {
                    objSearch.strActionCode = "c";
                    objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                    objSearch.intUserId = 0;
                }
                else
                {
                    /*---------------------------------------------------------------------------------------------*/
                    /// In case of DIC user,If you want to display MIS report for respective district then use action 'U'
                    /// In case of DIC user,If you want to display MIS report for all districts then use action 'V' (As Admin User)
                    /*---------------------------------------------------------------------------------------------*/
                    objSearch.strActionCode = "V"; //// Added by Sushant Jena On Dt:- 12-Feb-2020
                    //objSearch.strActionCode = "u"; //// Commented by Sushant Jena On Dt:- 12-Feb-2020
                    objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                    objSearch.intProjectType = 2;
                    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
                }
            }

            /*---------------------------------------------------------------------------------------------*/
            /// DQL Operation to get District wise MIS report count for proposals.
            /*---------------------------------------------------------------------------------------------*/
            
            List<PealMisReport> lstPealRpt = MisReportServices.PEAL_MisReportLogic2(objSearch);


            /*---------------------------------------------------------------------------------------------*/
            /// After getting district wise MIS counts, Get the MIS report count for sector IT and Tourism.
            /// Display the MIS report count for IT and tourism along with the counts for each district.
            /*---------------------------------------------------------------------------------------------*/
            if (ddlSector.SelectedIndex > 0)
            {
                #region IfSectorSelected

                if (ddlSector.SelectedValue == "10") //// IT Sector
                {
                    
                    List<PealMisReport> lstPealRpt1 = BindITPeal();

                    var deliveryModel = new PealMisReport
                    {
                        intDistrictId = Convert.ToInt32(lstPealRpt1[0].intDistrictId),
                        strDistrictName = Convert.ToString(lstPealRpt1[0].strDistrictName),
                        cnt_Total = Convert.ToInt32(lstPealRpt1[0].cnt_Total),
                        cnt_Pending = Convert.ToInt32(lstPealRpt1[0].cnt_Pending),
                        cnt_Approved = Convert.ToInt32(lstPealRpt1[0].cnt_Approved),
                        cnt_rejected = Convert.ToInt32(lstPealRpt1[0].cnt_rejected),
                        cnt_Query = Convert.ToInt32(lstPealRpt1[0].cnt_Query),
                        cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt1[0].cnt_Proposed_Emp),
                        total_Capital_Investment = Convert.ToDecimal(lstPealRpt1[0].total_Capital_Investment),
                        cnt_landAssessment = Convert.ToInt32(lstPealRpt1[0].cnt_landAssessment),
                        cnt_landAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_landAllotment),
                        cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysApproval),
                        cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysAllotment),
                        cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysAllotment),
                        cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysApproval),
                        cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].cnt_Total_ORTPSAtimeline),
                        cnt_deferred = Convert.ToInt32(lstPealRpt1[0].cnt_deferred),
                        cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt1[0].cnt_Land_Allotment_ORTPSA),
                        cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt1[0].cnt_CarryFwd_pending),
                        int_Total_Pending = Convert.ToInt32(lstPealRpt1[0].int_Total_Pending)
                    };

                    lstPealRpt.Add(deliveryModel);
                }
                else if (ddlSector.SelectedValue == "38") //// Tourism Sector
                {
                   
                    List<PealMisReport> lstPealRpt1 = BindToursimPeal();

                    var deliveryModel = new PealMisReport
                    {
                        intDistrictId = Convert.ToInt32(lstPealRpt1[0].intDistrictId),
                        strDistrictName = Convert.ToString(lstPealRpt1[0].strDistrictName),
                        cnt_Total = Convert.ToInt32(lstPealRpt1[0].cnt_Total),
                        cnt_Pending = Convert.ToInt32(lstPealRpt1[0].cnt_Pending),
                        cnt_Approved = Convert.ToInt32(lstPealRpt1[0].cnt_Approved),
                        cnt_rejected = Convert.ToInt32(lstPealRpt1[0].cnt_rejected),
                        cnt_Query = Convert.ToInt32(lstPealRpt1[0].cnt_Query),
                        cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt1[0].cnt_Proposed_Emp),
                        total_Capital_Investment = Convert.ToDecimal(lstPealRpt1[0].total_Capital_Investment),
                        cnt_landAssessment = Convert.ToInt32(lstPealRpt1[0].cnt_landAssessment),
                        cnt_landAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_landAllotment),
                        cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysApproval),
                        cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysAllotment),
                        cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysAllotment),
                        cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysApproval),
                        cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].cnt_Total_ORTPSAtimeline),
                        cnt_deferred = Convert.ToInt32(lstPealRpt1[0].cnt_deferred),
                        cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt1[0].cnt_Land_Allotment_ORTPSA),
                        cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt1[0].cnt_CarryFwd_pending),
                        int_Total_Pending = Convert.ToInt32(lstPealRpt1[0].int_Total_Pending)
                    };

                    lstPealRpt.Add(deliveryModel);
                }

                #endregion
            }
            else
            {
                #region IfSectorNotSelected

                /*----------------------------------------------------------------------*/
                ///// Add MIS report counts for IT sector.
                /*----------------------------------------------------------------------*/
               
                List<PealMisReport> lstPealRpt1 = BindITPeal();

                var deliveryModel1 = new PealMisReport
                {
                    intDistrictId = Convert.ToInt32(lstPealRpt1[0].intDistrictId),
                    strDistrictName = Convert.ToString(lstPealRpt1[0].strDistrictName),
                    cnt_Total = Convert.ToInt32(lstPealRpt1[0].cnt_Total),
                    cnt_Pending = Convert.ToInt32(lstPealRpt1[0].cnt_Pending),
                    cnt_Approved = Convert.ToInt32(lstPealRpt1[0].cnt_Approved),
                    cnt_rejected = Convert.ToInt32(lstPealRpt1[0].cnt_rejected),
                    cnt_Query = Convert.ToInt32(lstPealRpt1[0].cnt_Query),
                    cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt1[0].cnt_Proposed_Emp),
                    total_Capital_Investment = Convert.ToDecimal(lstPealRpt1[0].total_Capital_Investment),
                    cnt_landAssessment = Convert.ToInt32(lstPealRpt1[0].cnt_landAssessment),
                    cnt_landAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_landAllotment),
                    cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysApproval),
                    cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_AvgDaysAllotment),
                    cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysAllotment),
                    cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt1[0].cnt_Total_AvgDaysApproval),
                    cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt1[0].cnt_Total_ORTPSAtimeline),
                    cnt_deferred = Convert.ToInt32(lstPealRpt1[0].cnt_deferred),
                    cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt1[0].cnt_Land_Allotment_ORTPSA),
                    cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt1[0].cnt_CarryFwd_pending),
                    int_Total_Pending = Convert.ToInt32(lstPealRpt1[0].int_Total_Pending)
                };

                lstPealRpt.Add(deliveryModel1);


                /*----------------------------------------------------------------------*/
                /// Add MIS report counts for Tourism sector.
                /*----------------------------------------------------------------------*/
               
                List<PealMisReport> lstPealRpt2 = BindToursimPeal();

                var deliveryModel2 = new PealMisReport
                {
                    intDistrictId = Convert.ToInt32(lstPealRpt2[0].intDistrictId),
                    strDistrictName = Convert.ToString(lstPealRpt2[0].strDistrictName),
                    cnt_Total = Convert.ToInt32(lstPealRpt2[0].cnt_Total),
                    cnt_Pending = Convert.ToInt32(lstPealRpt2[0].cnt_Pending),
                    cnt_Approved = Convert.ToInt32(lstPealRpt2[0].cnt_Approved),
                    cnt_rejected = Convert.ToInt32(lstPealRpt2[0].cnt_rejected),
                    cnt_Query = Convert.ToInt32(lstPealRpt2[0].cnt_Query),
                    cnt_Proposed_Emp = Convert.ToInt32(lstPealRpt2[0].cnt_Proposed_Emp),
                    total_Capital_Investment = Convert.ToDecimal(lstPealRpt2[0].total_Capital_Investment),
                    cnt_landAssessment = Convert.ToInt32(lstPealRpt2[0].cnt_landAssessment),
                    cnt_landAllotment = Convert.ToInt32(lstPealRpt2[0].cnt_landAllotment),
                    cnt_AvgDaysApproval = Convert.ToInt32(lstPealRpt2[0].cnt_AvgDaysApproval),
                    cnt_AvgDaysAllotment = Convert.ToInt32(lstPealRpt2[0].cnt_AvgDaysAllotment),
                    cnt_Total_AvgDaysAllotment = Convert.ToInt32(lstPealRpt2[0].cnt_Total_AvgDaysAllotment),
                    cnt_Total_AvgDaysApproval = Convert.ToInt32(lstPealRpt2[0].cnt_Total_AvgDaysApproval),
                    cnt_Total_ORTPSAtimeline = Convert.ToInt32(lstPealRpt2[0].cnt_Total_ORTPSAtimeline),
                    cnt_deferred = Convert.ToInt32(lstPealRpt2[0].cnt_deferred),
                    cnt_Land_Allotment_ORTPSA = Convert.ToInt32(lstPealRpt2[0].cnt_Land_Allotment_ORTPSA),
                    cnt_CarryFwd_pending = Convert.ToInt32(lstPealRpt2[0].cnt_CarryFwd_pending),
                    int_Total_Pending = Convert.ToInt32(lstPealRpt2[0].int_Total_Pending)
                };

                lstPealRpt.Add(deliveryModel2);

                #endregion
            }

            /*---------------------------------------------------------------*/
            /// Bind each district's data, as well as the data from the IT and Tourism sections, to GridView.
            /*---------------------------------------------------------------*/
            GrdPealDetails.DataSource = lstPealRpt;
            GrdPealDetails.DataBind();           

            if (GrdPealDetails.Rows.Count > 0)
            {
                /*------------------------------------------------------------------------------------*/
                /// Calculate the total for just the records in each district.
                /// Data from the IT and tourism sectors are excluded from the total summation because they are already included in the district level data.
                /*------------------------------------------------------------------------------------*/

                GridViewRow gRowFooter = GrdPealDetails.FooterRow;

                gRowFooter.Cells[1].Text = "Total";
                gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Proposed_Emp).ToString());
                gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.total_Capital_Investment).ToString());
                gRowFooter.Cells[13].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysApproval.ToString());
                gRowFooter.Cells[14].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_landAssessment).ToString());
                gRowFooter.Cells[15].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_landAllotment).ToString());
                gRowFooter.Cells[16].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysAllotment.ToString());
                gRowFooter.Cells[17].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Land_Allotment_ORTPSA).ToString());

                Label lblCarryFwdPendingFooter = (Label)gRowFooter.FindControl("lblCarryFwdPendingFooter");
                lblCarryFwdPendingFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_CarryFwd_pending).ToString());

                Label lblRcvdFooter = (Label)gRowFooter.FindControl("lblRcvdFooter");
                lblRcvdFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Total).ToString());

                Label lblApprovedFooter = (Label)gRowFooter.FindControl("lblApprovedFooter");
                lblApprovedFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Approved).ToString());

                Label lblRejectedFooter = (Label)gRowFooter.FindControl("lblRejectedFooter");
                lblRejectedFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_rejected).ToString());

                Label lblDefferecFooter = (Label)gRowFooter.FindControl("lblDefferecFooter");
                lblDefferecFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_deferred).ToString());

                Label lblQuery1Footer = (Label)gRowFooter.FindControl("lblQuery1Footer");
                lblQuery1Footer.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Query).ToString());

                Label lblPendingFooter = (Label)gRowFooter.FindControl("lblPendingFooter");
                lblPendingFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Pending).ToString());

                Label lblTotalPendingFooter = (Label)gRowFooter.FindControl("lblTotalPendingFooter");
                lblTotalPendingFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.int_Total_Pending).ToString());

                Label lblORTPSFooter = (Label)gRowFooter.FindControl("lblORTPSFooter");
                lblORTPSFooter.Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Total_ORTPSAtimeline).ToString());
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
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
            Util.LogError(ex, "ProposalMIS");
        }
    }

    /// <summary>
    /// Function used to fill all sectors
    /// </summary>
    private void BindSector()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
           
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SE";
            objProp.vchProposalNo = "";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();

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
            Util.LogError(ex, "ProposalMIS");
        }
    }

    /// <summary>
    /// Get MIS report count for IT sectors
    /// </summary>
    /// <returns></returns>
    private List<PealMisReport> BindITPeal()
    {
        try
        {          
            GrdPealDetails.DataSource = null;
            GrdPealDetails.DataBind();

            /*---------------------------------------------------------------------------------------------*/

            string strFromDate;
            string strToDate;

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

            /*---------------------------------------------------------------------------------------------*/

            PealSearch objSearch = new PealSearch()
            {
                strActionCode = "IT",
                intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
                intProjectType = 0,
                intSectorId = ddlSector.SelectedIndex > 0 ? Convert.ToInt32(ddlSector.SelectedValue) : 0,
                strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
                strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim(),
                intInvestmentAmt = drpInvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(drpInvestmentAmt.SelectedValue) : 0,
                intUserId = Convert.ToInt32(Session["UserId"])
            };

            /*---------------------------------------------------------------------------------------------*/

            if (Session["desId"].ToString() == "97")
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }

            if ((Convert.ToInt32(Session["UserId"]) == 167)) //IT 
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }

            /*---------------------------------------------------------------------------------------------*/

            int intLevelId = Convert.ToInt32(Session["LevelID"]);
            if (intLevelId == 4)
            {
                if (Session["desId"].ToString() == "126")
                {
                    objSearch.strActionCode = "Ic";
                    objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                    objSearch.intUserId = 0;
                }
                else
                {
                    objSearch.strActionCode = "Iu";
                    objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                    objSearch.intProjectType = 2;
                    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
                }
            }

            List<PealMisReport> lstPealRpt1 = MisReportServices.PEAL_MisReportLogic2(objSearch);
            return lstPealRpt1;

        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    /// <summary>
    /// Get MIS report count for Tourism sectors
    /// </summary>
    /// <returns></returns>
    private List<PealMisReport> BindToursimPeal()
    {
        try
        {            
            GrdPealDetails.DataSource = null;
            GrdPealDetails.DataBind();

            /*---------------------------------------------------------------------------------------------*/

            string strFromDate;
            string strToDate;

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

            /*---------------------------------------------------------------------------------------------*/

            PealSearch objSearch = new PealSearch()
            {
                strActionCode = "TOURSIM",
                intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
                intProjectType = 0,
                intSectorId = ddlSector.SelectedIndex > 0 ? Convert.ToInt32(ddlSector.SelectedValue) : 0,
                strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
                strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim(),
                intInvestmentAmt = drpInvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(drpInvestmentAmt.SelectedValue) : 0,
                intUserId = Convert.ToInt32(Session["UserId"])
            };

            /*---------------------------------------------------------------------------------------------*/

            if (Session["desId"].ToString() == "97")
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }

            if ((Convert.ToInt32(Session["UserId"]) == 166)) //Toursim
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }

            /*---------------------------------------------------------------------------------------------*/

            int intLevelId = Convert.ToInt32(Session["LevelID"]);
            if (intLevelId == 4)
            {
                if (Session["desId"].ToString() == "126")
                {
                    objSearch.strActionCode = "Tc";
                    objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                    objSearch.intUserId = 0;
                }
                else
                {
                    objSearch.strActionCode = "Tu";
                    objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                    objSearch.intProjectType = 2;
                    objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
                }
            }

            List<PealMisReport> lstPealRpt1 = MisReportServices.PEAL_MisReportLogic2(objSearch);
            return lstPealRpt1;
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    /// <summary>
    /// This function is used to get MIS report counts, which will be utilised in the Excel export.
    /// </summary>
    public void FillGridViewForExcelExport()
    {
        try
        {
            int intLevelId = Convert.ToInt32(Session["LevelID"]);
            GridView1.DataSource = null;
            GridView1.DataBind();

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
                intUserId = Convert.ToInt32(Session["UserId"])
            };

            if (Session["desId"].ToString() == "97")
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(drpInvestmentAmt.SelectedValue);
            }
            if ((Convert.ToInt32(Session["UserId"]) == 166) || (Convert.ToInt32(Session["UserId"]) == 167)) //IT Toursim
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

            List<PealMisReport> lstPealRpt = MisReportServices.PEAL_MisReportLogic2(objSearch);

            GridView1.DataSource = lstPealRpt;
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
            {
                GridViewRow gRowFooter = GridView1.FooterRow;
                gRowFooter.Cells[1].Text = "Total";
                gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_CarryFwd_pending).ToString());
                gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total).ToString());
                gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Approved).ToString());
                gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_rejected).ToString());
                gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_deferred).ToString());
                gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Query).ToString());
                gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Pending).ToString());
                gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.int_Total_Pending).ToString());
                gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Total_ORTPSAtimeline).ToString());
                gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Proposed_Emp).ToString());
                gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(lstPealRpt.Sum(x => x.total_Capital_Investment).ToString());
                gRowFooter.Cells[13].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysApproval.ToString());
                gRowFooter.Cells[14].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAssessment).ToString());
                gRowFooter.Cells[15].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_landAllotment).ToString());
                gRowFooter.Cells[16].Text = IncentiveCommonFunctions.FormatString(lstPealRpt[0].cnt_Total_AvgDaysAllotment.ToString());
                gRowFooter.Cells[17].Text = IncentiveCommonFunctions.FormatString(lstPealRpt.Sum(x => x.cnt_Land_Allotment_ORTPSA).ToString());
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
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
            Util.LogError(ex, "ProposalMIS");
        }
    }
    protected void LnkBtnPdfExport_Click(object sender, EventArgs e)
    {
        try
        {
            string strFileName = "MISReportPEAL_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
            IncentiveCommonFunctions.CreatePdf(strFileName, GrdPealDetails);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }
    protected void LnkBtnExcelExport_Click(object sender, EventArgs e)
    {
        try
        {
            /*------------------------------------------------------------------*/
            ///Fill Gridview for excel file export.
            /*------------------------------------------------------------------*/
            FillGridViewForExcelExport();

            /*------------------------------------------------------------------*/
            ///Export the gridview to excel file.
            /*------------------------------------------------------------------*/
            string strFileName = "MISReportPEAL_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", strFileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            viewTable.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }

    #endregion
    
    protected void GrdPealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}