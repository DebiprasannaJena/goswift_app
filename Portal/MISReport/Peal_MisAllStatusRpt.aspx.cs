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
            Txt_From_Date.Attributes.Add("readonly", "readonly");
            Txt_To_Date.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);

            try
            {
                int IntLevelId = Convert.ToInt32(Session["LevelID"]);
                int IntDesignationId = Convert.ToInt32(Session["desId"].ToString());

                BindDistrict();

                if (IntLevelId == 1)
                {
                    if (IntDesignationId == 97) //psmsme user
                    {
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        DrpDwn_InvestmentAmt.SelectedIndex = 0;
                    }
                    else // all other admin user
                    {
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("-ALL-", "0"));
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("50-1000 CR (SLSWCA)", "2"));
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("> 1000 Cr (HLCA)", "3"));
                    }
                }

                /*-----------------------------------------------------------------------------*/

                if ((Convert.ToInt32(Session["UserId"]) == 166) || (Convert.ToInt32(Session["UserId"]) == 167)) //psmsme user
                {
                    DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                    DrpDwn_InvestmentAmt.SelectedIndex = 0;
                }
                else if (IntLevelId == 4) //// DIC & Collector Level User
                {
                    ///// Set respective district for DIC/Collector user.    
                    // SetDrpForDistrictUser(); //// Commented by Sushant Jena On Dt:13-Feb-2020 in order to display all the district data to DIC user.

                    if (IntDesignationId == 126 || (Convert.ToInt32(Session["UserId"]) == 557)) ///// Collector  
                    {
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("-ALL-", "0"));
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("50-1000 CR (SLSWCA)", "2"));
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("> 1000 Cr (HLCA)", "3"));
                    }                                    
                    else
                    {
                        ///// For DIC
                        DrpDwn_InvestmentAmt.Items.Add(new System.Web.UI.WebControls.ListItem("0-50 Cr (DLSWCA)", "1"));
                        DrpDwn_InvestmentAmt.SelectedIndex = 0;
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

            int IntLevelId = Convert.ToInt32(Session["LevelID"]);

            /*---------------------------------------------------------------------------------------------*/

            string StrFromDate = string.Empty;
            string StrToDate = string.Empty;
            int IntMonth = DateTime.Today.Month;
            if (IntMonth == 1)
            {
                StrFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                StrFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            /*---------------------------------------------------------------------------------------------*/

            PealSearch objSearch = new PealSearch()
            {
                strActionCode = "V",
                intDistrictId = DrpDwn_District.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_District.SelectedValue) : 0,
                intProjectType = 0,
                intSectorId = DrpDwn_Sector.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_Sector.SelectedValue) : 0,
                strFromDate = string.IsNullOrEmpty(Txt_From_Date.Text.Trim()) ? StrFromDate : Txt_From_Date.Text.Trim(),
                strToDate = string.IsNullOrEmpty(Txt_To_Date.Text.Trim()) ? StrToDate : Txt_To_Date.Text.Trim(),
                intInvestmentAmt = DrpDwn_InvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue) : 0,
                intUserId = Convert.ToInt32(Session["UserId"])
            };

            if (Session["desId"].ToString() == "97")//Principal Secretary(MSME)
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }

            if ((Convert.ToInt32(Session["UserId"]) == 166) || (Convert.ToInt32(Session["UserId"]) == 167)) //IT Toursim
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }

            if (IntLevelId == 4)
            {
                if (Session["desId"].ToString() == "126") ///// For collector
                {
                    objSearch.strActionCode = "c";
                    objSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
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
                    objSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
                    if(Convert.ToInt32(Session["UserId"]) == 557) // for idco user login
                    {
                        objSearch.intProjectType = 0;
                    }
                    else
                    {
                        objSearch.intProjectType = 2;
                    }
                   
                    objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
                }
            }

            /*---------------------------------------------------------------------------------------------*/
            /// DQL Operation to get District wise MIS report count for proposals.
            /*---------------------------------------------------------------------------------------------*/
            
            List<PealMisReport> ListPealRpt = MisReportServices.PEAL_MisReportLogic2(objSearch);


            /*---------------------------------------------------------------------------------------------*/
            /// After getting district wise MIS counts, Get the MIS report count for sector IT and Tourism.
            /// Display the MIS report count for IT and tourism along with the counts for each district.
            /*---------------------------------------------------------------------------------------------*/
            if (DrpDwn_Sector.SelectedIndex > 0)
            {
                #region IfSectorSelected

                if (DrpDwn_Sector.SelectedValue == "10") //// IT Sector
                {
                    
                    List<PealMisReport> ListPealRptIt = BindITPeal();

                    var deliveryModel = new PealMisReport
                    {
                        intDistrictId = Convert.ToInt32(ListPealRptIt[0].intDistrictId),
                        strDistrictName = Convert.ToString(ListPealRptIt[0].strDistrictName),
                        cnt_Total = Convert.ToInt32(ListPealRptIt[0].cnt_Total),
                        cnt_Pending = Convert.ToInt32(ListPealRptIt[0].cnt_Pending),
                        cnt_Approved = Convert.ToInt32(ListPealRptIt[0].cnt_Approved),
                        cnt_rejected = Convert.ToInt32(ListPealRptIt[0].cnt_rejected),
                        cnt_Query = Convert.ToInt32(ListPealRptIt[0].cnt_Query),
                        cnt_Proposed_Emp = Convert.ToInt32(ListPealRptIt[0].cnt_Proposed_Emp),
                        total_Capital_Investment = Convert.ToDecimal(ListPealRptIt[0].total_Capital_Investment),
                        cnt_landAssessment = Convert.ToInt32(ListPealRptIt[0].cnt_landAssessment),
                        cnt_landAllotment = Convert.ToInt32(ListPealRptIt[0].cnt_landAllotment),
                        cnt_AvgDaysApproval = Convert.ToInt32(ListPealRptIt[0].cnt_AvgDaysApproval),
                        cnt_AvgDaysAllotment = Convert.ToInt32(ListPealRptIt[0].cnt_AvgDaysAllotment),
                        cnt_Total_AvgDaysAllotment = Convert.ToInt32(ListPealRptIt[0].cnt_Total_AvgDaysAllotment),
                        cnt_Total_AvgDaysApproval = Convert.ToInt32(ListPealRptIt[0].cnt_Total_AvgDaysApproval),
                        cnt_Total_ORTPSAtimeline = Convert.ToInt32(ListPealRptIt[0].cnt_Total_ORTPSAtimeline),
                        cnt_deferred = Convert.ToInt32(ListPealRptIt[0].cnt_deferred),
                        cnt_Land_Allotment_ORTPSA = Convert.ToInt32(ListPealRptIt[0].cnt_Land_Allotment_ORTPSA),
                        cnt_CarryFwd_pending = Convert.ToInt32(ListPealRptIt[0].cnt_CarryFwd_pending),
                        int_Total_Pending = Convert.ToInt32(ListPealRptIt[0].int_Total_Pending)
                    };

                    ListPealRpt.Add(deliveryModel);
                }
                else if (DrpDwn_Sector.SelectedValue == "38") //// Tourism Sector
                {
                   
                    List<PealMisReport> ListPealRptTurs = BindToursimPeal();

                    var deliveryModel = new PealMisReport
                    {
                        intDistrictId = Convert.ToInt32(ListPealRptTurs[0].intDistrictId),
                        strDistrictName = Convert.ToString(ListPealRptTurs[0].strDistrictName),
                        cnt_Total = Convert.ToInt32(ListPealRptTurs[0].cnt_Total),
                        cnt_Pending = Convert.ToInt32(ListPealRptTurs[0].cnt_Pending),
                        cnt_Approved = Convert.ToInt32(ListPealRptTurs[0].cnt_Approved),
                        cnt_rejected = Convert.ToInt32(ListPealRptTurs[0].cnt_rejected),
                        cnt_Query = Convert.ToInt32(ListPealRptTurs[0].cnt_Query),
                        cnt_Proposed_Emp = Convert.ToInt32(ListPealRptTurs[0].cnt_Proposed_Emp),
                        total_Capital_Investment = Convert.ToDecimal(ListPealRptTurs[0].total_Capital_Investment),
                        cnt_landAssessment = Convert.ToInt32(ListPealRptTurs[0].cnt_landAssessment),
                        cnt_landAllotment = Convert.ToInt32(ListPealRptTurs[0].cnt_landAllotment),
                        cnt_AvgDaysApproval = Convert.ToInt32(ListPealRptTurs[0].cnt_AvgDaysApproval),
                        cnt_AvgDaysAllotment = Convert.ToInt32(ListPealRptTurs[0].cnt_AvgDaysAllotment),
                        cnt_Total_AvgDaysAllotment = Convert.ToInt32(ListPealRptTurs[0].cnt_Total_AvgDaysAllotment),
                        cnt_Total_AvgDaysApproval = Convert.ToInt32(ListPealRptTurs[0].cnt_Total_AvgDaysApproval),
                        cnt_Total_ORTPSAtimeline = Convert.ToInt32(ListPealRptTurs[0].cnt_Total_ORTPSAtimeline),
                        cnt_deferred = Convert.ToInt32(ListPealRptTurs[0].cnt_deferred),
                        cnt_Land_Allotment_ORTPSA = Convert.ToInt32(ListPealRptTurs[0].cnt_Land_Allotment_ORTPSA),
                        cnt_CarryFwd_pending = Convert.ToInt32(ListPealRptTurs[0].cnt_CarryFwd_pending),
                        int_Total_Pending = Convert.ToInt32(ListPealRptTurs[0].int_Total_Pending)
                    };

                    ListPealRpt.Add(deliveryModel);
                }

                #endregion
            }
            else
            {
                #region IfSectorNotSelected

                /*----------------------------------------------------------------------*/
                ///// Add MIS report counts for IT sector.
                /*----------------------------------------------------------------------*/
               
                List<PealMisReport> LstPealRptIT = BindITPeal();

                var deliveryModel1 = new PealMisReport
                {
                    intDistrictId = Convert.ToInt32(LstPealRptIT[0].intDistrictId),
                    strDistrictName = Convert.ToString(LstPealRptIT[0].strDistrictName),
                    cnt_Total = Convert.ToInt32(LstPealRptIT[0].cnt_Total),
                    cnt_Pending = Convert.ToInt32(LstPealRptIT[0].cnt_Pending),
                    cnt_Approved = Convert.ToInt32(LstPealRptIT[0].cnt_Approved),
                    cnt_rejected = Convert.ToInt32(LstPealRptIT[0].cnt_rejected),
                    cnt_Query = Convert.ToInt32(LstPealRptIT[0].cnt_Query),
                    cnt_Proposed_Emp = Convert.ToInt32(LstPealRptIT[0].cnt_Proposed_Emp),
                    total_Capital_Investment = Convert.ToDecimal(LstPealRptIT[0].total_Capital_Investment),
                    cnt_landAssessment = Convert.ToInt32(LstPealRptIT[0].cnt_landAssessment),
                    cnt_landAllotment = Convert.ToInt32(LstPealRptIT[0].cnt_landAllotment),
                    cnt_AvgDaysApproval = Convert.ToInt32(LstPealRptIT[0].cnt_AvgDaysApproval),
                    cnt_AvgDaysAllotment = Convert.ToInt32(LstPealRptIT[0].cnt_AvgDaysAllotment),
                    cnt_Total_AvgDaysAllotment = Convert.ToInt32(LstPealRptIT[0].cnt_Total_AvgDaysAllotment),
                    cnt_Total_AvgDaysApproval = Convert.ToInt32(LstPealRptIT[0].cnt_Total_AvgDaysApproval),
                    cnt_Total_ORTPSAtimeline = Convert.ToInt32(LstPealRptIT[0].cnt_Total_ORTPSAtimeline),
                    cnt_deferred = Convert.ToInt32(LstPealRptIT[0].cnt_deferred),
                    cnt_Land_Allotment_ORTPSA = Convert.ToInt32(LstPealRptIT[0].cnt_Land_Allotment_ORTPSA),
                    cnt_CarryFwd_pending = Convert.ToInt32(LstPealRptIT[0].cnt_CarryFwd_pending),
                    int_Total_Pending = Convert.ToInt32(LstPealRptIT[0].int_Total_Pending)
                };

                ListPealRpt.Add(deliveryModel1);


                /*----------------------------------------------------------------------*/
                /// Add MIS report counts for Tourism sector.
                /*----------------------------------------------------------------------*/
               
                List<PealMisReport> ListPealRptTourist = BindToursimPeal();

                var deliveryModel2 = new PealMisReport
                {
                    intDistrictId = Convert.ToInt32(ListPealRptTourist[0].intDistrictId),
                    strDistrictName = Convert.ToString(ListPealRptTourist[0].strDistrictName),
                    cnt_Total = Convert.ToInt32(ListPealRptTourist[0].cnt_Total),
                    cnt_Pending = Convert.ToInt32(ListPealRptTourist[0].cnt_Pending),
                    cnt_Approved = Convert.ToInt32(ListPealRptTourist[0].cnt_Approved),
                    cnt_rejected = Convert.ToInt32(ListPealRptTourist[0].cnt_rejected),
                    cnt_Query = Convert.ToInt32(ListPealRptTourist[0].cnt_Query),
                    cnt_Proposed_Emp = Convert.ToInt32(ListPealRptTourist[0].cnt_Proposed_Emp),
                    total_Capital_Investment = Convert.ToDecimal(ListPealRptTourist[0].total_Capital_Investment),
                    cnt_landAssessment = Convert.ToInt32(ListPealRptTourist[0].cnt_landAssessment),
                    cnt_landAllotment = Convert.ToInt32(ListPealRptTourist[0].cnt_landAllotment),
                    cnt_AvgDaysApproval = Convert.ToInt32(ListPealRptTourist[0].cnt_AvgDaysApproval),
                    cnt_AvgDaysAllotment = Convert.ToInt32(ListPealRptTourist[0].cnt_AvgDaysAllotment),
                    cnt_Total_AvgDaysAllotment = Convert.ToInt32(ListPealRptTourist[0].cnt_Total_AvgDaysAllotment),
                    cnt_Total_AvgDaysApproval = Convert.ToInt32(ListPealRptTourist[0].cnt_Total_AvgDaysApproval),
                    cnt_Total_ORTPSAtimeline = Convert.ToInt32(ListPealRptTourist[0].cnt_Total_ORTPSAtimeline),
                    cnt_deferred = Convert.ToInt32(ListPealRptTourist[0].cnt_deferred),
                    cnt_Land_Allotment_ORTPSA = Convert.ToInt32(ListPealRptTourist[0].cnt_Land_Allotment_ORTPSA),
                    cnt_CarryFwd_pending = Convert.ToInt32(ListPealRptTourist[0].cnt_CarryFwd_pending),
                    int_Total_Pending = Convert.ToInt32(ListPealRptTourist[0].int_Total_Pending)
                };

                ListPealRpt.Add(deliveryModel2);

                #endregion
            }

            /*---------------------------------------------------------------*/
            /// Bind each district's data, as well as the data from the IT and Tourism sections, to GridView.
            /*---------------------------------------------------------------*/
            GrdPealDetails.DataSource = ListPealRpt;
            GrdPealDetails.DataBind();           

            if (GrdPealDetails.Rows.Count > 0)
            {
                /*------------------------------------------------------------------------------------*/
                /// Calculate the total for just the records in each district.
                /// Data from the IT and tourism sectors are excluded from the total summation because they are already included in the district level data.
                /*------------------------------------------------------------------------------------*/

                GridViewRow gRowFooter = GrdPealDetails.FooterRow;

                gRowFooter.Cells[1].Text = "Total";
                gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Proposed_Emp).ToString());
                gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.total_Capital_Investment).ToString());
                gRowFooter.Cells[13].Text = IncentiveCommonFunctions.FormatString(ListPealRpt[0].cnt_Total_AvgDaysApproval.ToString());
                gRowFooter.Cells[14].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_landAssessment).ToString());
                gRowFooter.Cells[15].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_landAllotment).ToString());
                gRowFooter.Cells[16].Text = IncentiveCommonFunctions.FormatString(ListPealRpt[0].cnt_Total_AvgDaysAllotment.ToString());
                gRowFooter.Cells[17].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Land_Allotment_ORTPSA).ToString());

                Label LblCarryFwdPendingFooter = (Label)gRowFooter.FindControl("lblCarryFwdPendingFooter");
                LblCarryFwdPendingFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_CarryFwd_pending).ToString());

                Label LblRcvdFooter = (Label)gRowFooter.FindControl("lblRcvdFooter");
                LblRcvdFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Total).ToString());

                Label LblApprovedFooter = (Label)gRowFooter.FindControl("lblApprovedFooter");
                LblApprovedFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Approved).ToString());

                Label LblRejectedFooter = (Label)gRowFooter.FindControl("lblRejectedFooter");
                LblRejectedFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_rejected).ToString());

                Label LblDefferecFooter = (Label)gRowFooter.FindControl("lblDefferecFooter");
                LblDefferecFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_deferred).ToString());

                Label LblQuery1Footer = (Label)gRowFooter.FindControl("lblQuery1Footer");
                LblQuery1Footer.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Query).ToString());

                Label LblPendingFooter = (Label)gRowFooter.FindControl("lblPendingFooter");
                LblPendingFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Pending).ToString());

                Label LblTotalPendingFooter = (Label)gRowFooter.FindControl("lblTotalPendingFooter");
                LblTotalPendingFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.int_Total_Pending).ToString());

                Label LblORTPSFooter = (Label)gRowFooter.FindControl("lblORTPSFooter");
                LblORTPSFooter.Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Where(x => x.intDistrictId != 0).Sum(x => x.cnt_Total_ORTPSAtimeline).ToString());
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
            ProposalBAL ObjService = new ProposalBAL();
           
            ProjectInfo ObjProp = new ProjectInfo();

            ObjProp.strAction = "DT";
            ObjProp.vchProposalNo = " ";
            List<ProjectInfo> ObjProjList = ObjService.PopulateProjDropdowns(ObjProp).ToList();

            DrpDwn_District.DataSource = ObjProjList;
            DrpDwn_District.DataTextField = "vchDistName";
            DrpDwn_District.DataValueField = "intDistId";
            DrpDwn_District.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DrpDwn_District.Items.Insert(0, list);
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
            ProposalBAL ObjService = new ProposalBAL();
           
            ProjectInfo ObjProp = new ProjectInfo();

            ObjProp.strAction = "SE";
            ObjProp.vchProposalNo = "";
            List<ProjectInfo> ObjProjList = ObjService.PopulateProjDropdowns(ObjProp).ToList();

            DrpDwn_Sector.DataSource = ObjProjList;
            DrpDwn_Sector.DataTextField = "vchSectorName";
            DrpDwn_Sector.DataValueField = "intSectorId";
            DrpDwn_Sector.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DrpDwn_Sector.Items.Insert(0, list);
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

            string StrFromDate;
            string StrToDate;

            int IntMonth = DateTime.Today.Month;
            if (IntMonth == 1)
            {
                StrFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                StrFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            /*---------------------------------------------------------------------------------------------*/

            PealSearch ObjSearch = new PealSearch()
            {
                strActionCode = "IT",
                intDistrictId = DrpDwn_District.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_District.SelectedValue) : 0,
                intProjectType = 0,
                intSectorId = DrpDwn_Sector.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_Sector.SelectedValue) : 0,
                strFromDate = string.IsNullOrEmpty(Txt_From_Date.Text.Trim()) ? StrFromDate : Txt_From_Date.Text.Trim(),
                strToDate = string.IsNullOrEmpty(Txt_To_Date.Text.Trim()) ? StrToDate : Txt_To_Date.Text.Trim(),
                intInvestmentAmt = DrpDwn_InvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue) : 0,
                intUserId = Convert.ToInt32(Session["UserId"])
            };

            /*---------------------------------------------------------------------------------------------*/

            if (Session["desId"].ToString() == "97")
            {
                ObjSearch.intProjectType = 2;
                ObjSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }

            if ((Convert.ToInt32(Session["UserId"]) == 167)) //IT 
            {
                ObjSearch.intProjectType = 2;
                ObjSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }

            /*---------------------------------------------------------------------------------------------*/

            int IntLevelId = Convert.ToInt32(Session["LevelID"]);
            if (IntLevelId == 4)
            {
                if (Session["desId"].ToString() == "126")
                {
                    ObjSearch.strActionCode = "Ic";
                    ObjSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
                    ObjSearch.intUserId = 0;
                }
                else
                {
                    ObjSearch.strActionCode = "Iu";
                    ObjSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
                    if(Convert.ToInt32(Session["UserId"]) == 557) // for idco user login
                    {
                        ObjSearch.intProjectType = 0;
                    }
                    else
                    {
                        ObjSearch.intProjectType = 2;
                    }

                    ObjSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
                }
            }

            List<PealMisReport> ListPealRptIt = MisReportServices.PEAL_MisReportLogic2(ObjSearch);
            return ListPealRptIt;

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

            string StrFromDate;
            string StrToDate;

            int IntMonth = DateTime.Today.Month;
            if (IntMonth == 1)
            {
                StrFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                StrFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            /*---------------------------------------------------------------------------------------------*/

            PealSearch objSearch = new PealSearch()
            {
                strActionCode = "TOURSIM",
                intDistrictId = DrpDwn_District.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_District.SelectedValue) : 0,
                intProjectType = 0,
                intSectorId = DrpDwn_Sector.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_Sector.SelectedValue) : 0,
                strFromDate = string.IsNullOrEmpty(Txt_From_Date.Text.Trim()) ? StrFromDate : Txt_From_Date.Text.Trim(),
                strToDate = string.IsNullOrEmpty(Txt_To_Date.Text.Trim()) ? StrToDate : Txt_To_Date.Text.Trim(),
                intInvestmentAmt = DrpDwn_InvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue) : 0,
                intUserId = Convert.ToInt32(Session["UserId"])
            };

            /*---------------------------------------------------------------------------------------------*/

            if (Session["desId"].ToString() == "97")
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }

            if ((Convert.ToInt32(Session["UserId"]) == 166)) //Toursim
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }

            /*---------------------------------------------------------------------------------------------*/

            int IntLevelId = Convert.ToInt32(Session["LevelID"]);
            if (IntLevelId == 4)
            {
                if (Session["desId"].ToString() == "126")
                {
                    objSearch.strActionCode = "Tc";
                    objSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
                    objSearch.intUserId = 0;
                }
                else
                {
                    objSearch.strActionCode = "Tu";
                    objSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
                    if (Convert.ToInt32(Session["UserId"]) == 557)
                    {
                        objSearch.intProjectType = 0;
                    }
                    else
                    {
                        objSearch.intProjectType = 2;
                    }
                        
                    objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
                }
            }

            List<PealMisReport> ListPealRptTourst = MisReportServices.PEAL_MisReportLogic2(objSearch);
            return ListPealRptTourst;
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
            int IntLevelId = Convert.ToInt32(Session["LevelID"]);
            Grid_Excel_Export.DataSource = null;
            Grid_Excel_Export.DataBind();

            string StrFromDate = string.Empty;
            string StrToDate = string.Empty;
            int IntMonth = DateTime.Today.Month;
            if (IntMonth == 1)
            {
                StrFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                StrFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            PealSearch objSearch = new PealSearch()
            {
                strActionCode = "V",
                intDistrictId = DrpDwn_District.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_District.SelectedValue) : 0,
                intProjectType = 0,
                intSectorId = DrpDwn_Sector.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_Sector.SelectedValue) : 0,
                strFromDate = string.IsNullOrEmpty(Txt_From_Date.Text.Trim()) ? StrFromDate : Txt_From_Date.Text.Trim(),
                strToDate = string.IsNullOrEmpty(Txt_To_Date.Text.Trim()) ? StrToDate : Txt_To_Date.Text.Trim(),
                intInvestmentAmt = DrpDwn_InvestmentAmt.SelectedIndex > 0 ? Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue) : 0,
                intUserId = Convert.ToInt32(Session["UserId"])
            };

            if (Session["desId"].ToString() == "97")
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }
            if ((Convert.ToInt32(Session["UserId"]) == 166) || (Convert.ToInt32(Session["UserId"]) == 167)) //IT Toursim
            {
                objSearch.intProjectType = 2;
                objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
            }
            if (IntLevelId == 4)
            {
                if (Session["desId"].ToString() == "126")
                {
                    objSearch.strActionCode = "c";
                    objSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
                    objSearch.intUserId = 0;
                }
                else
                {
                    objSearch.strActionCode = "u";
                    objSearch.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
                    objSearch.intProjectType = 2;
                    objSearch.intInvestmentAmt = Convert.ToInt32(DrpDwn_InvestmentAmt.SelectedValue);
                }
            }

            List<PealMisReport> ListPealRpt = MisReportServices.PEAL_MisReportLogic2(objSearch);

            Grid_Excel_Export.DataSource = ListPealRpt;
            Grid_Excel_Export.DataBind();

            if (Grid_Excel_Export.Rows.Count > 0)
            {
                GridViewRow gRowFooter = Grid_Excel_Export.FooterRow;
                gRowFooter.Cells[1].Text = "Total";
                gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_CarryFwd_pending).ToString());
                gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_Total).ToString());
                gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_Approved).ToString());
                gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_rejected).ToString());
                gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_deferred).ToString());
                gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_Query).ToString());
                gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_Pending).ToString());
                gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.int_Total_Pending).ToString());
                gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_Total_ORTPSAtimeline).ToString());
                gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_Proposed_Emp).ToString());
                gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(ListPealRpt.Sum(x => x.total_Capital_Investment).ToString());
                gRowFooter.Cells[13].Text = IncentiveCommonFunctions.FormatString(ListPealRpt[0].cnt_Total_AvgDaysApproval.ToString());
                gRowFooter.Cells[14].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_landAssessment).ToString());
                gRowFooter.Cells[15].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_landAllotment).ToString());
                gRowFooter.Cells[16].Text = IncentiveCommonFunctions.FormatString(ListPealRpt[0].cnt_Total_AvgDaysAllotment.ToString());
                gRowFooter.Cells[17].Text = IncentiveCommonFunctions.FormatString(ListPealRpt.Sum(x => x.cnt_Land_Allotment_ORTPSA).ToString());
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
            string Str = string.Empty;
            if (string.IsNullOrEmpty(Txt_From_Date.Text.Trim()))
            {
                Str = "jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", Str, true);
            }
            else if (string.IsNullOrEmpty(Txt_To_Date.Text.Trim()))
            {
                Str = "jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", Str, true);
            }
            else if (Convert.ToDateTime(Txt_From_Date.Text.Trim()) > Convert.ToDateTime(Txt_To_Date.Text.Trim()))
            {
                Str = "jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", Str, true);
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
            string StrFileName = "MISReportPEAL_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
            IncentiveCommonFunctions.CreatePdf(StrFileName, GrdPealDetails);
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
            string StrFileName = "MISReportPEAL_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", StrFileName));
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
            int IntLevelId = Convert.ToInt32(Session["levelId"]);
            hdnLavelVal.Value = IntLevelId.ToString();
            hdnDesgid.Value = Session["desId"].ToString();

            for (int cnt = 2; cnt < e.Row.Cells.Count; cnt++)
            {
                e.Row.Cells[cnt].Style["text-align"] = "right";
            }
        }
    }     
    protected void Grid_Excel_Export_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int IntLevelId = Convert.ToInt32(Session["levelId"]);
            hdnLavelVal.Value = IntLevelId.ToString();
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