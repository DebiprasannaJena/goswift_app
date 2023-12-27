//******************************************************************************************************************
// File Name             :   incentiveoffered.aspx.cs
// Description           :   Incentive Landing Page,View Application Counts and Validate Certification
// Created by            :   Sushant Kumar Jena
// Created on            :   21st Sept 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.Common;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using System.Data;
using BusinessLogicLayer.Dashboard;

public partial class incentives_incentiveoffered : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillInvestorChildUnit();
            fillAppCount();
        }
    }

    /////// Common Function Used   
    #region FunctionUsed

    /////// Application Count for Dashboard    
    private void fillAppCount()
    {
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        Inct_Basic_Unit_Details_WPC_Entity objEntity = new Inct_Basic_Unit_Details_WPC_Entity();

        try
        {
            string strFilterMode = "";
            string strInvestorId = "";
            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                strFilterMode = "I";
                strInvestorId = DrpDwn_Investor_Unit.SelectedValue;
            }
            else
            {
                strFilterMode = "C";
                strInvestorId = Convert.ToString(Session["InvestorId"]);
            }

            objEntity.strUserID = strInvestorId;
            objEntity.strFilterMode = strFilterMode;

            IList<Inct_Basic_Unit_Details_WPC_Entity> list = new List<Inct_Basic_Unit_Details_WPC_Entity>();
            list = ObjIMB.Inct_Application_Count(objEntity);

            if (list.Count > 0)
            {
                /*--------------------------------------------------------------*/
                ///// Drafted Application Count

                int draft_count = Convert.ToInt32(list[0].intDraftCount);

                if (draft_count > 0)
                {
                    TagDraft.Attributes.Add("href", "draftedapplicationdetails.aspx?UserId=" + objEntity.strUserID + "");
                }
                else
                {
                    TagDraft.Attributes.Add("href", "#");
                }

                Lbl_Drafted_App.Text = draft_count.ToString();

                /*--------------------------------------------------------------*/
                ///// Approved Application Count

                int approved_count = Convert.ToInt32(list[0].intApprovedCount);

                if (approved_count > 0)
                {
                    TagApproved.Attributes.Add("href", "ViewApplicationStatus.aspx?Sts=A1");
                }
                else
                {
                    TagApproved.Attributes.Add("href", "#");
                }

                Lbl_Approved.Text = approved_count.ToString();

                /*--------------------------------------------------------------*/
                ///// Scrutiny Application Count

                int scrutiny_count = Convert.ToInt32(list[0].intScrutinyCount);

                if (scrutiny_count > 0)
                {
                    TagScrutiny.Attributes.Add("href", "ViewApplicationStatus.aspx?Sts=A2");
                }
                else
                {
                    TagScrutiny.Attributes.Add("href", "#");
                }

                Lbl_Scrutiny.Text = scrutiny_count.ToString();


                /*--------------------------------------------------------------*/
                ///// Rejected Application Count

                int rejected_count = Convert.ToInt32(list[0].intRejectedCount);

                if (rejected_count > 0)
                {
                    TagRejected.Attributes.Add("href", "ViewApplicationStatus.aspx?Sts=A3");
                }
                else
                {
                    TagRejected.Attributes.Add("href", "#");
                }

                Lbl_Rejected.Text = rejected_count.ToString();


                /*--------------------------------------------------------------*/
                ///// Total Application Count

                int total_app_count = Convert.ToInt32(list[0].intTotalAppCount);

                if (total_app_count > 0)
                {
                    TagTotalApp.Attributes.Add("href", "ViewApplicationStatus.aspx?Sts=A");
                }
                else
                {
                    TagTotalApp.Attributes.Add("href", "#");
                }

                Lbl_Total_App.Text = total_app_count.ToString();

                /*--------------------------------------------------------------*/
                ///// Disburse Application Count

                int disburse_count = Convert.ToInt32(list[0].intDisburseCount);

                if (disburse_count > 0)
                {
                    TagDisbursed.Attributes.Add("href", "ViewApplicationStatus.aspx?Sts=A4");
                }
                else
                {
                    TagDisbursed.Attributes.Add("href", "#");
                }

                Lbl_Disbursed.Text = disburse_count.ToString();

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ObjIMB = null;
            objEntity = null;
        }
    }
    /////// Validate Certification   
    private void validateCertification(string strInctId)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();
        DataTable objdt = new DataTable();

        try
        {
            //// strMode --> 1 for ONLINE and 2 for OFFLINE 

            string strMode = "";
            if (strInctId.Contains('~'))
            {
                string[] ret = strInctId.Split('~');
                strInctId = ret[0].ToString();
                strMode = ret[1].ToString();
            }

            /*-------------------------------------------------------------------*/

            objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objEntity.intInctId = Convert.ToInt32(strInctId);

            objdt = objBAL.ValidateCertification(objEntity);
            if (objdt.Rows.Count > 0)
            {
                int intReturnStatus = Convert.ToInt32(objdt.Rows[0]["INT_RETURN_STATUS"]);
                string strPcStatus = Convert.ToString(objdt.Rows[0]["VCH_PC_STATUS"]);
                int intPriorityStatus = Convert.ToInt32(objdt.Rows[0]["INT_PRIORITY_STATUS"]);
                int intPioneerStatus = Convert.ToInt32(objdt.Rows[0]["INT_PIONEER_STATUS"]);
                string strPealStatus= Convert.ToString(objdt.Rows[0]["VCH_PEAL_STATUS"]);

                if (intReturnStatus == 1)
                {
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Your application is already in drafted stage,Please go through drafted application details. !!</strong>');", true);
                    return;
                }
                else if (intReturnStatus == 2)
                {
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You have already applied this application !!</strong>');", true);
                    return;
                }
                else if (intReturnStatus == 3)
                {
                    if (strInctId == "10100103") //// Pioneer
                    {
                        if (intPriorityStatus != 1)
                        {
                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for pioneer certification as you donot have a priority certificate !!</strong>');", true);
                            return;
                        }
                        else
                        {
                            Response.Redirect("Basic_Details.aspx?key=" + strInctId + "", false);
                        }
                    }
                    else if (strInctId == "10100119") //// Priority
                    {
                        if (strPcStatus == "N")
                        {
                            
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for priority certification as you donot have a production certificate !!</strong>');", true);
                            return;

                        }
                        else
                        {
                            Response.Redirect("Basic_Details.aspx?key=" + strInctId + "~" + strMode + "", false);
                        }
                    }
                    else if (strInctId == "10100102") //// Provisional Priority
                    {
                        if (strPcStatus == "Y")
                        {
                          

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for provisional priority certification as you have a production certificate !!</strong>');", true);
                            return;
                        }
                        else
                        {
                            Response.Redirect("Basic_Details.aspx?key=" + strInctId + "", false);
                        }
                    }
                    else if (strInctId == "11400101") //  Apply Provisional Thrust or Priority Certificate IPR 2022
                    {
                        if (strPealStatus == "N")  // og ipr 2022  term 4 b (need to be discous)
                        {
                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for Thrust or priority Sector certification of IPR-2022 as you donot have a Approve PEAL !!</strong>');", true);
                            return;
                        }
                        else
                        {
                            Response.Redirect("ThrustPrioritysectorstatus.aspx?key=" + strInctId + "", false);
                        }
                    }
                    else if (strInctId == "11400102") //  Apply  Stamp Duty Exemption Certificate IPR 2022
                    {
                        if (strPealStatus == "N")  // og ipr 2022  term 4 b (need to be discous)
                        {
                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for Stamp Duty Exemption certification of IPR-2022 as you donot have a Approve PEAL !!</strong>');", true);
                            return;
                        }
                        else
                        {
                            Response.Redirect("StampDutyExemption.aspx?key=" + strInctId + "", false);
                        }
                    }
                    else if (strInctId == "11400103") //  Apply Conversion Of Land For Industrial Use Certificate IPR 2022
                    {
                        if (strPealStatus == "N")  // og ipr 2022  term 4 b (need to be discous)
                        {
                          
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for Conversion Of Land For Industrial Use certification of IPR-2022 as you donot have a Approve PEAL !!</strong>');", true);
                            return;
                        }
                        else
                        {
                            Response.Redirect("ExemptionLandforIndustrialUseIPR2022.aspx?key=" + strInctId + "", false);
                        }
                    }
                    else if (strInctId == "11400104") //  Apply Migrated Industrial Unit  IPR 2022
                    {
                        if (strPealStatus == "N")  // og ipr 2022  term 4 b (need to be discous)
                        {
                            
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for Exercising Option by Migrated Industrial Unit IPR-2022 as you donot have a Approve PEAL !!</strong>');", true);
                            return;
                        }
                        else
                        {
                            Response.Redirect("MigratedIndustrialUnitIPR2022.aspx?key=" + strInctId + "", false);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion

    /////// Apply Provisional Priority Certificate   
    protected void LnkBtn_Prov_Priority_Click(object sender, EventArgs e)
    {
        validateCertification("10100102");
    }
    /////// Apply Priority Certificate (Online)  
    protected void LnkBtn_Priority_Online_Click(object sender, EventArgs e)
    {
        validateCertification("10100119~1");
    }
    /////// Apply Priority Certificate (Offline)  
    protected void LnkBtn_Priority_Offline_Click(object sender, EventArgs e)
    {
        validateCertification("10100119~2");
    }
    /////// Apply Pioneer Certificate   
    protected void LnkBtn_Pioneer_Click(object sender, EventArgs e)
    {
        validateCertification("10100103");
    }

    /////// Apply Incentive  
    protected void LnkBtn_Inct_Apply_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strAction = "D";

            ds = objBAL.Inct_EC_Delay_Reason_VIEW(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string strStatus = ds.Tables[0].Rows[0]["intStatus"].ToString();

                if (strStatus == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You will be able to apply after your application for condonation of delay has been approved by the Empowered Committee !!</strong>','" + strProjName + "')", true);
                    return;
                }
                else
                {
                    Response.Redirect("Basic_Details.aspx?key=INCENTIVE", false);
                }
                //else if (strStatus == "3")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Your application has been rejected by the Empowered Committee. Hence, you cannot proceed to apply for incentives. In case of any clarifications contact your concerned DIC !!</strong>','" + strProjName + "')", true);
                //    return;
                //}
            }
            else
            {
                Response.Redirect("Basic_Details.aspx?key=INCENTIVE", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
        }
    }

    protected void LnkBtn_Inct_Apply_Sectoral_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/incentives_pro/Basic_Details_Sectoral.aspx?key=INCENTIVE", false);
        // above file not exist.
    }


    /// <summary>
    /// Added by Sushant Jena On Dt.14-Aug-2018
    /// To get child unit name and self unit name for a investor.
    /// </summary>
    private void fillInvestorChildUnit()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "INVUNIT";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

            DataTable dt = new DataTable();
            dt = objserviceDashboard.getInvestorChildUnit(objSWP);

            if (dt.Rows.Count > 1)
            {
                DrpDwn_Investor_Unit.DataTextField = "VCH_INV_NAME";
                DrpDwn_Investor_Unit.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_Investor_Unit.DataSource = dt;
                DrpDwn_Investor_Unit.DataBind();
                DrpDwn_Investor_Unit.Items.Insert(0, new ListItem("-Select Unit-", "0"));

                DrpDwn_Investor_Unit.Visible = true;
            }
            else
            {
                DrpDwn_Investor_Unit.Items.Clear();
                DrpDwn_Investor_Unit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }


    /// <summary>
    /// Added by Sushant Jena On Dt.14-Aug-2018
    /// Filter details by unit 
    /// C-Chain (Means Including child users)
    /// I-Indivisual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DrpDwn_Investor_Unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillAppCount();
    }

    protected void LnkBtn_Prov_Thrust_Priority_Click(object sender, EventArgs e)
    {
       

        validateCertification("11400101");//  Apply  Thrust or Priority Certificate IPR 2022

        
    }

    protected void LnkBtn_Stam_Duty_Exeption_Click(object sender, EventArgs e)
    {
        validateCertification("11400102");
    }

    protected void LnkBtn_Land_Industrial_Use_Click(object sender, EventArgs e)
    {
        validateCertification("11400103");
    }

    protected void LnkBtn_Migrated_Unit_Click(object sender, EventArgs e)
    {
        validateCertification("11400104");
    }
}