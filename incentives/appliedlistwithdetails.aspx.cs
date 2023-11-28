//******************************************************************************************************************
// File Name             :   appliedlistwithdetails.aspx.cs
// Description           :   Profile Summary with List of Eligible Incentives
// Created by            :   Sushant Kumar Jena
// Created on            :   15th Sept 2017
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
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Data;
using System.IO;
using System.Xml;

public partial class incentives_incentiveoffered : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    //// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("incentiveoffered.aspx");
        }

        if (!IsPostBack)
        {
            ModalPopupExtender1.Hide();

            //if (validateInctApply())
            //{
            if (validateEC())
            {
                fillSummary();
                fillGrid();
            }
            //}
        }
    }

    //// Function Used
    #region Function Used

    //// validate page either PEAL or PC available
    private bool validateInctApply()
    {
        bool b_returnValue = true;

        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Validate_Inct_Apply_Entity objEntity = new Validate_Inct_Apply_Entity();

        try
        {
            objEntity.intUserID = Convert.ToInt32(Session["InvestorId"]);

            string strReturnValue = objBAL.Validate_Inct_Apply(objEntity);
            if (strReturnValue == "B")
            {
                string message = @"You cannot apply for incentives because there is no valid Industrial Unit/Enterprise data against your login. \n \n1.To apply for project evaluation and(or) allotment of land go to the proposal tab. \n\n2.If your unit/enterprise is in production ,first apply for production certificate.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "');</script>", false);
                b_returnValue = false;
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

        return b_returnValue;
    }
    //// Check Empowered Committee
    private bool validateEC()
    {
        bool b_returnValue = true;
        string strInctFlow = "N"; ///// Normal Flow

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
                string strEligibleDate = ds.Tables[0].Rows[0]["dtmEligibleDate"].ToString();

                if (strStatus == "1")
                {
                    b_returnValue = false;
                    string strMsg = @"<strong>You will be able to apply after your application for condonation of delay has been approved by the Empowered Committee !!</strong>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + strMsg + "');</script>", false);
                }
                else if (strStatus == "2")
                {
                    if (DateTime.Now.Date <= Convert.ToDateTime(strEligibleDate))
                    {
                        strInctFlow = "E"; ///// EC Flow                        
                    }
                }
            }

            ViewState["InctFlow"] = strInctFlow;
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

        return b_returnValue;
    }
    //// Fill Summary
    private void fillSummary()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();

        try
        {
            objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strAction = "S";

            IList<Inct_Applied_With_PC_Entity> list = new List<Inct_Applied_With_PC_Entity>();
            list = objBAL.View_Summary_With_PC(objEntity);

            if (list.Count > 0)
            {
                Lbl_Industry_Code.Text = Session["UnitCode"].ToString();
                Lbl_Comp_Name.Text = list[0].strCompName.ToString();

                string strFFCI = list[0].dtmFFCI.ToString();
                if (strFFCI != "")
                {
                    Lbl_FFCI_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strFFCI));
                }
                else
                {
                    Lbl_FFCI_Date.Text = "";
                }

                string strProdComm = list[0].dtmProdComm.ToString();
                if (strProdComm != "")
                {
                    Div_Prod_Comm.Visible = true;
                    Lbl_Prod_Comm_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strProdComm));
                }
                else
                {
                    Div_Prod_Comm.Visible = false;
                    Lbl_Prod_Comm_Date.Text = "";
                }

                Lbl_Unit_Type.Text = list[0].strUnitCat.ToString();
                Lbl_District_Category.Text = list[0].strDistCat.ToString();

                if (list[0].strRating.ToString() != "")
                {
                    Lbl_Empl_Invest_Rating.Text = list[0].strRating.ToString();
                    Div_Empl_Rating.Visible = true;
                }
                else
                {
                    Div_Empl_Rating.Visible = false;
                    Lbl_Empl_Invest_Rating.Text = "";
                }
                Lbl_Total_Capital_Invest.Text = list[0].strTotCapInvest.ToString();
                Lbl_PM_Invest.Text = list[0].strPlantMachInvest.ToString();
                Lbl_Sector_Name.Text = list[0].strSectorName.ToString();
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
    //// Fill Incentive List with Eligibility
    private void fillGrid()
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity = new Basic_Unit_Details_Entity();

        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strIndustryCode = Session["UnitCode"].ToString();
            objEntity.strInctFlow = ViewState["InctFlow"].ToString();

            ds = objLayer.Bind_Inct_With_Eligible(objEntity);

            ///// Bind Applicable Policies
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                /*-------------------------------------------------------------*/
                ///// Parent Policies

                ds.Tables[0].DefaultView.RowFilter = "intPlcCat = 1";
                dt = (ds.Tables[0].DefaultView).ToTable();

                Grd_Parent_Policy.DataSource = dt;
                Grd_Parent_Policy.DataBind();

                /*-------------------------------------------------------------*/
                ///// Sectoral Policies

                ds.Tables[0].DefaultView.RowFilter = "intPlcCat = 2";
                dt = (ds.Tables[0].DefaultView).ToTable();

                Grd_Sectoral_Policy.DataSource = dt;
                Grd_Sectoral_Policy.DataBind();

                /*-------------------------------------------------------------*/
                ///// Other  Policies

                ds.Tables[0].DefaultView.RowFilter = "intPlcCat = 3";
                dt = (ds.Tables[0].DefaultView).ToTable();

                Grd_Other_policy.DataSource = dt;
                Grd_Other_policy.DataBind();
            }

            ////// Bind Eligible Incentives
            if (ds.Tables[1].Rows.Count > 0)
            {
                Grd_Incentives.DataSource = ds.Tables[1];
                Grd_Incentives.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objLayer = null;
            objEntity = null;
        }
    }

    #endregion

    //// Gridview RowDataBound
    protected void Grd_Incentives_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet ds = new DataSet();

            try
            {
                HiddenField Hid_OG_Doc = (HiddenField)e.Row.FindControl("Hid_OG_Doc");
                HyperLink Hlnk_Policy_Name = (HyperLink)e.Row.FindControl("Hlnk_Policy_Name");
                HiddenField Hid_Inct_Id = (HiddenField)e.Row.FindControl("Hid_Inct_Id");

                //Hlnk_Policy_Name.NavigateUrl = "../Portal/OGDoc/" + Hid_OG_Doc.Value;
                Hlnk_Policy_Name.NavigateUrl = Hid_OG_Doc.Value;

                HiddenField Hid_Prov_File_Name = (HiddenField)e.Row.FindControl("Hid_Prov_File_Name");
                LinkButton LnkBtn_Read_Provision = (LinkButton)e.Row.FindControl("LnkBtn_Read_Provision");
                if (Hid_Prov_File_Name.Value != "")
                {
                    LnkBtn_Read_Provision.Visible = true;
                }
                else
                {
                    LnkBtn_Read_Provision.Visible = false;
                }

                HiddenField Hid_First_FY_Status = (HiddenField)e.Row.FindControl("Hid_First_FY_Status");
                HiddenField Hid_Periodicity = (HiddenField)e.Row.FindControl("Hid_Periodicity");
                HiddenField Hid_FY = (HiddenField)e.Row.FindControl("Hid_FY");
                DropDownList DrpDwn_FY = (DropDownList)e.Row.FindControl("DrpDwn_FY");

                if (Hid_FY.Value != "")
                {
                    //ds.ReadXml(new XmlTextReader(new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?><ArrayOfDistInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + Hid_FY.Value + "</ArrayOfDistInfo>")));
                    ds.ReadXml(new XmlTextReader(new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Hid_FY.Value)));

                    DrpDwn_FY.DataSource = ds;
                    DrpDwn_FY.DataTextField = "FY_TEXT";
                    DrpDwn_FY.DataValueField = "FY_VALUE";
                    DrpDwn_FY.DataBind();

                    DrpDwn_FY.Visible = true;
                }
                else
                {
                    DrpDwn_FY.Visible = false;
                    if (Hid_Periodicity.Value == "ANNUAL")
                    {
                        if (Hid_First_FY_Status.Value == "N")
                        {
                            e.Row.ToolTip = "You are not eligible to apply for this incentive because,your financial year yet to be completed.";
                        }
                        else if (Hid_First_FY_Status.Value == "Y")
                        {
                            e.Row.ToolTip = "You have already applied for all pending Incentives.";
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
                ds = null;
            }
        }
    }

    //// Gridview Row Button Click For Apply
    protected void LnkBtn_Apply_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();

        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_First_FY_Status = (HiddenField)row.FindControl("Hid_First_FY_Status");
            HiddenField Hid_Periodicity = (HiddenField)row.FindControl("Hid_Periodicity");
            HiddenField Hid_Form_Id = (HiddenField)row.FindControl("Hid_Form_Id");
            HiddenField Hid_Inct_Id = (HiddenField)row.FindControl("Hid_Inct_Id");
            DropDownList DrpDwn_FY = (DropDownList)row.FindControl("DrpDwn_FY");

            if (Hid_Form_Id.Value != "")
            {
                objEntity.intInctId = Convert.ToInt32(Hid_Inct_Id.Value);
                objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
                objEntity.strInctFlow = ViewState["InctFlow"].ToString();

                if (ViewState["InctFlow"].ToString() == "N")
                {
                    objEntity.intFinancialYear = 0;
                }
                else if (ViewState["InctFlow"].ToString() == "E")
                {
                    if (DrpDwn_FY.SelectedValue != "")
                    {
                        objEntity.intFinancialYear = Convert.ToInt32(DrpDwn_FY.SelectedValue);
                    }
                    else
                    {
                        if (Hid_Periodicity.Value == "ANNUAL")
                        {
                            if (Hid_First_FY_Status.Value == "N")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this incentive because,your financial year yet to be completed. !</strong>','" + strProjName + "')", true);
                                return;
                            }
                            else if (Hid_First_FY_Status.Value == "Y")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You have already applied for all pending Incentives !</strong>','" + strProjName + "')", true);
                                return;
                            }
                        }
                        else if (Hid_Periodicity.Value == "ONETIME")
                        {
                            objEntity.intFinancialYear = 0;
                        }
                    }
                }

                ///// Check Time Frame
                string strReturnStatus = objBAL.Check_Time_Frame(objEntity);

                /*--------------------------------------------------*/
                string strfy = "";
                if (strReturnStatus.Contains('-'))
                {
                    string[] ret = strReturnStatus.Split('-');
                    strReturnStatus = ret[0].ToString();
                    strfy = ret[1].ToString();
                }
                /*--------------------------------------------------*/
                //Session["FyYear"] = strfy;
                //Session["ApplySource"] = "1";
                //Response.Redirect(Hid_Form_Id.Value + "?IncentiveNo=" + Hid_Inct_Id.Value + "", false);
                if (strReturnStatus == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You have already applied for this incentive. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "2")
                {
                    Session["FyYear"] = strfy;
                    Session["ApplySource"] = "1";
                    Response.Redirect(Hid_Form_Id.Value + "?IncentiveNo=" + Hid_Inct_Id.Value + "", false);
                }
                else if (strReturnStatus == "3") ///// Time Frame Crossed for ONETIME Application
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible for apply this incentive,because your time frame for filing your application has expired. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "4")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You have already applied for last financial year. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "5")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Your application is drafted, please go through drafted application details. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "6") ///// Time Frame Crossed for ANNUAL Application
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this financial year because,you have exceeded the time frame of the application. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "8")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this incentive because,your maximum time frame for applying for this incentive has expired. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "9")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this incentive because,your financial year yet to be completed. !</strong>','" + strProjName + "')", true);
                    return;
                }
                //else if (strReturnStatus == "7")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this incentive because,you have already applied for the maximum numbers of times that this incentive can be availed. !</strong>','" + strProjName + "')", true);
                //    return;
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No form available for this incentive !</strong>','" + strProjName + "')", true);
                return;
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
    //// Gridview Row Button Click For View Provision
    protected void LnkBtn_Read_Provision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Prov_File_Name = (HiddenField)row.FindControl("Hid_Prov_File_Name");
            if (Hid_Prov_File_Name.Value != "")
            {
                Img_Provision.Attributes.Add("src", "../incentives/Files/Provision/" + Hid_Prov_File_Name.Value);
                ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No Form Available for this Incentive !</strong>','" + strProjName + "')", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
}