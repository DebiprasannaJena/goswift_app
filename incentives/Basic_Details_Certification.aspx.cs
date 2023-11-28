//******************************************************************************************************************
// File Name             :   Basic_Details_Certification.aspx.cs
// Description           :   Common Profile Details for Applying Provisional Priority,Priority and Pioneer Certificate
// Created by            :   Sushant Kumar Jena
// Created on            :   8th Nov 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.IO;
using System.Data;
using DataAcessLayer.Common;
using BusinessLogicLayer.Incentive;
using System.Collections;
using System.Text;

public partial class incentives_Basic_Details_Certification : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    string strInctId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request.QueryString["key"]) == null)
        {
            Response.Redirect("incentiveoffered.aspx");
        }
        strInctId = Request.QueryString["key"].ToString();

        Txt_EIN_IL_Date.Attributes.Add("readonly", "readonly");
        Txt_Prod_Comm_Date_Before.Attributes.Add("readonly", "readonly");
        Txt_Prod_Comm_Date_After.Attributes.Add("readonly", "readonly");
        Txt_PC_Issue_Date_Before.Attributes.Add("readonly", "readonly");
        Txt_PC_Issue_Date_After.Attributes.Add("readonly", "readonly");
        Txt_FFCI_Date_Before.Attributes.Add("readonly", "readonly");
        Txt_FFCI_Date_After.Attributes.Add("readonly", "readonly");
        Txt_TL_Sanction_Date.Attributes.Add("readonly", "readonly");
        Txt_TL_Availed_Date.Attributes.Add("readonly", "readonly");
        Txt_WC_Sanction_Date.Attributes.Add("readonly", "readonly");
        Txt_WC_Availed_Date.Attributes.Add("readonly", "readonly");

        if (!IsPostBack)
        {
            Lbl_Priority_Msg.Visible = false;
            Div_Ancillary.Visible = false;
            Txt_Other_Unit_After.Visible = false;
            Txt_Other_Unit_Before.Visible = false;
            Div_Unit_Type_Doc.Visible = false;

            ModalPopupExtender1.Hide();

            //validateInctApply();
            //fillAppCount();

            fillUnitMeasurment();
            fillNatureOfActivity();
            fillOrgType();
            fillSector();
            fillSubSector();
            fillDistrict();
            fillUnitType();
            fillUnitCategory();
            fillDynamicNameDoc();
            fillOtherDoc();
            fillData();
        }

        //// Call EnableDisable Function on each Postback
        EnableDisableControl(Hid_Data_Source.Value);
    }

    //// Function Used
    #region Function Used

    //// Fill Document Name From Document Master
    private void fillDynamicNameDoc()
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        DataSet ds = new DataSet();

        try
        {
            ds = objLayer.dynamic_name_doc_bind();
            ViewState["dynamic_name_doc"] = ds;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objLayer = null;
            ds = null;
        }
    }
    //// Bind Sector
    private void fillOrgType()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();

        try
        {
            objEntity.Action = "O";
            objBAL.BindDropdown(DrpDwn_Org_Type, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind Sector
    private void fillSector()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "L";
            objBAL.BindDropdown(DrpDwn_Sector, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind SubSector
    private void fillSubSector()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();

        try
        {
            objEntity.Action = "M";
            objBAL.BindDropdown(DrpDwn_Sub_Sector, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind Filtered SubSector
    private void fillSubSectorFiltered()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();

        try
        {
            objEntity.Action = "sub";
            objEntity.Param_2 = DrpDwn_Sector.SelectedValue;
            objBAL.BindDropdown(DrpDwn_Sub_Sector, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind District
    private void fillDistrict()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();

        try
        {
            objEntity.Action = "I";
            objBAL.BindDropdown(DrpDwn_District, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind Unit Type
    private void fillUnitType()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "J";
            objBAL.BindDropdown(DrpDwn_Unit_Type, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind Unit Category
    private void fillUnitCategory()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "B";
            objBAL.BindDropdown(DrpDwn_Unit_Cat, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Fill Data in Page Load
    private void fillData()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity = new Basic_Unit_Details_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            ds = objBAL.Basic_Unit_Details_V(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                /*----------------------------------------------------------------------------*/
                ///// Common Information 
                string strFilePath = "../incentives/Files/InctBasicDoc/";
                string strDataSource = ds.Tables[0].Rows[0]["vch_Data_Source"].ToString();
                string strPcStatus = ds.Tables[0].Rows[0]["vch_PC_Status"].ToString();
                string strIsExistBefore = ds.Tables[0].Rows[0]["vch_Is_Before_Exist"].ToString();
                string strIsExistAfter = ds.Tables[0].Rows[0]["vch_Is_After_Exist"].ToString();
                string strIndustryCode = ds.Tables[0].Rows[0]["vch_Industry_Code"].ToString();
                string strProposalNo = ds.Tables[0].Rows[0]["vch_Proposal_No"].ToString();
                string strProjectType = ds.Tables[0].Rows[0]["int_Project_Type"].ToString();
                string strNewPcFound = ds.Tables[0].Rows[0]["vch_New_PC_Found"].ToString();

                /*----------------------------------------------------------------------------*/
                ////// If new PC found then assign strDataSource=PC
                ////// Only when data present in basic table and a new PC found 
                /*----------------------------------------------------------------------------*/
                if (strDataSource == "BASIC")
                {
                    if (strNewPcFound == "Y")
                    {
                        strDataSource = "PC";
                    }
                }
                /*----------------------------------------------------------------------------*/
                ////// Value Assigned to HiddenField for use in Validation
                Hid_Is_Exist_Before.Value = strIsExistBefore;
                Hid_Is_Exist_After.Value = strIsExistAfter;
                Hid_Data_Source.Value = strDataSource;
                Hid_PC_Status.Value = strPcStatus;
                Hid_Project_Type.Value = strProjectType;
                /*----------------------------------------------------------------------------*/

                if (strDataSource == "BASIC")
                {
                    #region IndustrialUnitDetails

                    Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchEnterpriseName"].ToString();

                    ////// Organization Type Document Binding
                    DrpDwn_Org_Type.SelectedValue = ds.Tables[1].Rows[0]["intOrganisationType"].ToString();
                    Hid_Org_Doc_Type.Value = ds.Tables[1].Rows[0]["vchCertOfRegdDocCode"].ToString();
                    Hyp_View_Org_Doc.NavigateUrl = strFilePath + ds.Tables[1].Rows[0]["vchCertOfRegdDocFileName"].ToString();
                    Hid_Org_Doc_File_Name.Value = ds.Tables[1].Rows[0]["vchCertOfRegdDocFileName"].ToString();
                    Hyp_View_Org_Doc.Visible = true;
                    LnkBtn_Delete_Org_Doc.Visible = true;
                    FU_Org_Doc.Enabled = false;

                    DataSet dsOrg = (DataSet)ViewState["dynamic_name_doc"];
                    dsOrg.Tables[0].DefaultView.RowFilter = "slno = '" + DrpDwn_Org_Type.SelectedValue + "'";
                    DataTable dtOrg = (dsOrg.Tables[0].DefaultView).ToTable();
                    if (dtOrg.Rows.Count > 0)
                    {
                        Lbl_Org_Name_Type.Text = dtOrg.Rows[0]["vchOwnerType"].ToString();
                        Lbl_Org_Doc_Type.Text = dtOrg.Rows[0]["vchDocumentTypeName"].ToString();
                    }

                    Txt_Industry_Address.Text = ds.Tables[1].Rows[0]["vchIndustryAddress"].ToString();
                    DrpDwn_Unit_Cat.SelectedValue = ds.Tables[1].Rows[0]["intUnitCat"].ToString();
                    DrpDwn_Unit_Type.SelectedValue = ds.Tables[1].Rows[0]["intUnitType"].ToString();

                    ////// Unit Type Document Section
                    Hid_Unit_Type_Doc_Code.Value = ds.Tables[1].Rows[0]["vchDocCode"].ToString();
                    if (Hid_Unit_Type_Doc_Code.Value != "")
                    {
                        Hyp_View_Unit_Type_Doc.NavigateUrl = strFilePath + ds.Tables[1].Rows[0]["vchUnitTypeDoc"].ToString();
                        Hid_Unit_Type_File_Name.Value = ds.Tables[1].Rows[0]["vchUnitTypeDoc"].ToString();
                        Hyp_View_Unit_Type_Doc.Visible = true;
                        LnkBtn_Delete_Unit_Type_Doc.Visible = true;
                        FU_Unit_Type.Enabled = false;
                        Div_Unit_Type_Doc.Visible = true;
                    }

                    DataSet dsUnitType = (DataSet)ViewState["dynamic_name_doc"];
                    dsUnitType.Tables[1].DefaultView.RowFilter = "slno = '" + DrpDwn_Unit_Type.SelectedValue + "'";
                    DataTable dtUnitType = (dsUnitType.Tables[1].DefaultView).ToTable();
                    if (dtUnitType.Rows.Count > 0)
                    {
                        Lbl_Unit_Type_Doc_Name.Text = dtUnitType.Rows[0]["vchDocumentTypename"].ToString();
                    }

                    Txt_Regd_Office_Address.Text = ds.Tables[1].Rows[0]["vchRegisteredOfcAddress"].ToString();
                    DrpDwn_Gender_Partner.SelectedValue = ds.Tables[1].Rows[0]["vchManagingPartnerGender"].ToString();
                    Txt_Partner_Name.Text = ds.Tables[1].Rows[0]["vchManagingPartnerName"].ToString();

                    Txt_EIN_IL_NO.Text = ds.Tables[1].Rows[0]["vchEINNO"].ToString();
                    Txt_EIN_IL_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmEIN"]));

                    if (strPcStatus == "Y")
                    {
                        ///// Before Production Commencement
                        if (strIsExistBefore == "Y")
                        {
                            Div_Prod_Comm_Before.Visible = true;
                            Lbl_Text_PC_No.Text = "- After E/M/D";
                            Lbl_Text_Prod_Comm.Text = "- After E/M/D";
                            Lbl_Text_PC_Issue_Date.Text = "- After E/M/D";

                            Txt_PC_No_Before.Text = ds.Tables[1].Rows[0]["vchPcNoBefore"].ToString();
                            Hid_PC_App_No_Before.Value = ds.Tables[1].Rows[0]["vchAppNoBefore"].ToString();
                            Txt_Prod_Comm_Date_Before.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmProdCommBefore"]));
                            Txt_PC_Issue_Date_Before.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmPCIssueDateBefore"]));
                            Hid_Prod_Comm_Before_Doc_Code.Value = ds.Tables[1].Rows[0]["vchProdCommCertBeforeCode"].ToString();

                            //Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = strFilePath + ds.Tables[1].Rows[0]["vchProdCommCertBefore"].ToString();
                            //Hid_Prod_Comm_Before_File_Name.Value = ds.Tables[1].Rows[0]["vchProdCommCertBefore"].ToString();
                            //if (Hid_Prod_Comm_Before_File_Name.Value != "")
                            //{
                            //    Hyp_View_Prod_Comm_Before_Doc.Visible = true;
                            //    LnkBtn_Delete_Prod_Comm_Before_Doc.Visible = true;
                            //    FU_Prod_Comm_Before.Enabled = false;
                            //}
                            LnkBtn_View_Prod_Comm_Before_Doc.Visible = true;
                        }
                        else
                        {
                            Div_Prod_Comm_Before.Visible = false;
                            Lbl_Text_PC_No.Text = "";
                            Lbl_Text_Prod_Comm.Text = "";
                            Lbl_Text_PC_Issue_Date.Text = "";
                        }

                        if (strIsExistAfter == "Y")
                        {
                            ///// After Production Commencement
                            Txt_PC_No_After.Text = ds.Tables[1].Rows[0]["vchPcNoAfter"].ToString();
                            Hid_PC_App_No_After.Value = ds.Tables[1].Rows[0]["vchAppNoAfter"].ToString();
                            Txt_Prod_Comm_Date_After.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmProdCommAfter"]));
                            Txt_PC_Issue_Date_After.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmPCIssueDateAfter"]));
                            //Hid_Prod_Comm_After_Doc_Code.Value = ds.Tables[1].Rows[0]["vchProdCommCertAfterCode"].ToString();
                            //Hyp_View_Prod_Comm_After_Doc.NavigateUrl = strFilePath + ds.Tables[1].Rows[0]["vchProdCommCertAfter"].ToString();
                            //Hid_Prod_Comm_After_File_Name.Value = ds.Tables[1].Rows[0]["vchProdCommCertAfter"].ToString();
                            //Hyp_View_Prod_Comm_After_Doc.Visible = true;
                            LnkBtn_View_Prod_Comm_After_Doc.Visible = true;
                            //LnkBtn_Delete_Prod_Comm_After_Doc.Visible = true;
                            //FU_Prod_Comm_After.Enabled = false;
                        }
                        else
                        {
                            Div_Prod_Comm_After.Visible = false;
                            Lbl_Text_PC_No.Text = "";
                            Lbl_Text_Prod_Comm.Text = "";
                            Lbl_Text_PC_Issue_Date.Text = "";
                        }
                    }
                    else
                    {
                        Div_Prod_Comm_Before.Visible = false;
                        Div_Prod_Comm_After.Visible = false;
                    }

                    ////// Priority Pioneer Section   

                    DrpDwn_District.SelectedValue = ds.Tables[1].Rows[0]["intDistrictCode"].ToString();

                    /*-------------------------------------------------------------*/
                    DrpDwn_Sector.SelectedValue = ds.Tables[1].Rows[0]["intSectorId"].ToString();

                    ///// Fill Filtered Sub-Sector for Above Sector
                    fillSubSectorFiltered();

                    DrpDwn_Sub_Sector.SelectedValue = ds.Tables[1].Rows[0]["intSubSectorId"].ToString();
                    /*-------------------------------------------------------------*/

                    int intSecPlc = Convert.ToInt32(ds.Tables[1].Rows[0]["bitSectoralPolicy"]);
                    if (intSecPlc == 1)
                    {
                        ChkBx_Sectoral.Checked = true;
                    }
                    else
                    {
                        ChkBx_Sectoral.Checked = false;
                    }

                    Rad_Is_Priority.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["intPriority"]);
                    if (Rad_Is_Priority.SelectedValue == "1")
                    {
                        Div_Pioneer.Visible = true;

                        Rad_Is_Pioneer.SelectedValue = ds.Tables[1].Rows[0]["intPioneer"].ToString();
                        Hid_Pioneer_Doc_File_Name.Value = ds.Tables[1].Rows[0]["vchPioneerCertificate"].ToString();

                        if (Hid_Pioneer_Doc_File_Name.Value != "")
                        {
                            Hyp_View_Pioneer_Doc.NavigateUrl = strFilePath + ds.Tables[1].Rows[0]["vchPioneerCertificate"].ToString();
                            Hid_Pioneer_Doc_Code.Value = ds.Tables[1].Rows[0]["VCHPIONEERCERTIFICATEDOCCODE"].ToString();
                            Hyp_View_Pioneer_Doc.Visible = true;
                            LnkBtn_Delete_Pioneer_Doc.Visible = true;
                            FU_Pioneer_Doc.Enabled = false;
                        }
                        else
                        {
                            Hyp_View_Pioneer_Doc.Visible = false;
                            LnkBtn_Delete_Pioneer_Doc.Visible = false;
                            FU_Pioneer_Doc.Enabled = true;
                        }
                    }
                    else
                    {
                        Div_Pioneer.Visible = false;
                    }

                    string strDerivedSector = ds.Tables[1].Rows[0]["vchDerivedSector"].ToString();
                    if (strDerivedSector != "")
                    {
                        Lbl_Derived_Sector.Text = strDerivedSector;
                    }
                    else
                    {
                        Lbl_Derived_Sector.Text = "NA";
                    }

                    Txt_GSTIN.Text = ds.Tables[1].Rows[0]["vchGSTIN"].ToString();

                    Rad_Is_Ancillary.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["intIsAncillary"]);
                    if (Rad_Is_Ancillary.SelectedValue == "1")
                    {
                        Div_Ancillary.Visible = true;

                        Hyp_View_Ancillary_Doc.NavigateUrl = strFilePath + Convert.ToString(ds.Tables[1].Rows[0]["vchAncillary"]);
                        Hid_Ancillary_Doc_File_Name.Value = Convert.ToString(ds.Tables[1].Rows[0]["vchAncillary"]);
                        Hid_Ancillary_Doc_Code.Value = Convert.ToString(ds.Tables[1].Rows[0]["vchAncillaryDocCode"]);
                        Hyp_View_Ancillary_Doc.Visible = true;
                        LnkBtn_Delete_Ancillary_Doc.Visible = true;
                        FU_Ancillary_Doc.Enabled = false;
                    }
                    else
                    {
                        Div_Ancillary.Visible = false;
                    }

                    Rad_Nature_Of_Activity.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["intCompNature"]);
                    Rad_Priority_User.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["intPriorityUser"]);

                    ///// Check Priority Message
                    displayPriorityMsg();

                    #endregion

                    #region  Production and Employment Details

                    /*-------------------------------------------------------------------------*/
                    ///// Production and Employment Details Section Binding

                    if (strIsExistBefore == "Y")
                    {
                        Div_Prod_Emp_Before.Visible = true;
                        Lbl_Header_Prod_Emp.Text = "After E/M/D";

                        Txt_Direct_Emp_Before.Text = ds.Tables[2].Rows[0]["intDirectEmpBefore"].ToString();
                        Txt_Contract_Emp_Before.Text = ds.Tables[2].Rows[0]["intContractualEmpBefore"].ToString();

                        ///// Documents Section for Direct Employee Before

                        if (ds.Tables[2].Rows[0]["vchEmpDocBefore"].ToString() != "")
                        {
                            Hid_Direct_Emp_Before_Doc_Code.Value = ds.Tables[2].Rows[0]["vchEmpDocBeforeCode"].ToString();
                            Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = strFilePath + ds.Tables[2].Rows[0]["vchEmpDocBefore"].ToString();
                            Hid_Direct_Emp_Before_File_Name.Value = ds.Tables[2].Rows[0]["vchEmpDocBefore"].ToString();
                            Hyp_View_Direct_Emp_Before_Doc.Visible = true;
                            LnkBtn_Delete_Direct_Emp_Before_Doc.Visible = true;
                            FU_Direct_Emp_Before.Enabled = false;
                        }
                        else
                        {
                            Hyp_View_Direct_Emp_Before_Doc.Visible = false;
                            LnkBtn_Delete_Direct_Emp_Before_Doc.Visible = false;
                            FU_Direct_Emp_Before.Enabled = true;
                        }

                        Txt_Managarial_Before.Text = ds.Tables[2].Rows[0]["intManagerialBefore"].ToString();
                        Txt_Supervisor_Before.Text = ds.Tables[2].Rows[0]["intSupervisorBefore"].ToString();
                        Txt_Skilled_Before.Text = ds.Tables[2].Rows[0]["intSkilledBefore"].ToString();
                        Txt_Semi_Skilled_Before.Text = ds.Tables[2].Rows[0]["intSemiSkilledBefore"].ToString();
                        Txt_Unskilled_Before.Text = ds.Tables[2].Rows[0]["intUnskilledBefore"].ToString();
                        Txt_Total_Emp_Before.Text = ds.Tables[2].Rows[0]["intTotalEmpBefore"].ToString();
                        Txt_General_Before.Text = ds.Tables[2].Rows[0]["intGeneralBefore"].ToString();
                        Txt_SC_Before.Text = ds.Tables[2].Rows[0]["intSCBefore"].ToString();
                        Txt_ST_Before.Text = ds.Tables[2].Rows[0]["intSTBefore"].ToString();
                        Txt_Total_Cast_Emp_Before.Text = ds.Tables[2].Rows[0]["intTotalEmpCastBefore"].ToString();
                        Txt_Women_Before.Text = ds.Tables[2].Rows[0]["intWomenBefore"].ToString();
                        Txt_PHD_Before.Text = ds.Tables[2].Rows[0]["intDisabledBefore"].ToString();
                    }
                    else
                    {
                        Div_Prod_Emp_Before.Visible = false;
                        Lbl_Header_Prod_Emp.Text = "";
                    }

                    Txt_Direct_Emp_After.Text = ds.Tables[2].Rows[0]["intDirectEmpAfter"].ToString();
                    Txt_Contract_Emp_After.Text = ds.Tables[2].Rows[0]["intContractualEmpAfter"].ToString();

                    ///// Documents Section for Direct Employee After 
                    if (ds.Tables[2].Rows[0]["vchEmpDocAfter"].ToString() != "")
                    {
                        Hid_Direct_Emp_After_Doc_Code.Value = ds.Tables[2].Rows[0]["vchEmpDocAfterCode"].ToString();
                        Hyp_View_Direct_Emp_After_Doc.NavigateUrl = strFilePath + ds.Tables[2].Rows[0]["vchEmpDocAfter"].ToString();
                        Hid_Direct_Emp_After_File_Name.Value = ds.Tables[2].Rows[0]["vchEmpDocAfter"].ToString();
                        Hyp_View_Direct_Emp_After_Doc.Visible = true;
                        LnkBtn_Delete_Direct_Emp_After_Doc.Visible = true;
                        FU_Direct_Emp_After.Enabled = false;
                    }
                    else
                    {
                        Hyp_View_Direct_Emp_After_Doc.Visible = false;
                        LnkBtn_Delete_Direct_Emp_After_Doc.Visible = false;
                        FU_Direct_Emp_After.Enabled = true;
                    }

                    Txt_Managarial_After.Text = ds.Tables[2].Rows[0]["intManagerialAfter"].ToString();
                    Txt_Supervisor_After.Text = ds.Tables[2].Rows[0]["intSupervisorAfter"].ToString();
                    Txt_Skilled_After.Text = ds.Tables[2].Rows[0]["intSkilledAfter"].ToString();
                    Txt_Semi_Skilled_After.Text = ds.Tables[2].Rows[0]["intSemiSkilledAfter"].ToString();
                    Txt_Unskilled_After.Text = ds.Tables[2].Rows[0]["intUnskilledAfter"].ToString();
                    Txt_Total_Emp_After.Text = ds.Tables[2].Rows[0]["intTotalEmpAfter"].ToString();
                    Txt_General_After.Text = ds.Tables[2].Rows[0]["intGeneralAfter"].ToString();
                    Txt_SC_After.Text = ds.Tables[2].Rows[0]["intSCAfter"].ToString();
                    Txt_ST_After.Text = ds.Tables[2].Rows[0]["intSTAfter"].ToString();
                    Txt_Total_Cast_Emp_After.Text = ds.Tables[2].Rows[0]["intTotalEmpCastAfter"].ToString();
                    Txt_Women_After.Text = ds.Tables[2].Rows[0]["intWomenAfter"].ToString();
                    Txt_PHD_After.Text = ds.Tables[2].Rows[0]["intDisabledAfter"].ToString();

                    /////// Production Table Before
                    ds.Tables[5].DefaultView.RowFilter = "chItemFor = 'B'";
                    DataTable dt1 = (ds.Tables[5].DefaultView).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        Grd_Production_Before.DataSource = dt1;
                        Grd_Production_Before.DataBind();
                    }

                    /////// Production Table After
                    ds.Tables[5].DefaultView.RowFilter = "chItemFor = 'A'";
                    DataTable dt2 = (ds.Tables[5].DefaultView).ToTable();
                    if (dt2.Rows.Count > 0)
                    {
                        Grd_Production_After.DataSource = dt2;
                        Grd_Production_After.DataBind();
                    }

                    #endregion

                    #region Investment Details

                    /*-------------------------------------------------------------------------*/
                    ///// Investment Details Section Binding

                    if (strIsExistBefore == "Y")
                    {
                        Div_Investment_Before.Visible = true;
                        Lbl_Header_Investment.Text = "After E/M/D";

                        string strFFCIBefore = Convert.ToString(ds.Tables[3].Rows[0]["dtmFFCIDateBefore"]);
                        if (strFFCIBefore != "")
                        {
                            Txt_FFCI_Date_Before.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strFFCIBefore));
                        }
                        else
                        {
                            Txt_FFCI_Date_Before.Text = "";
                        }

                        ////// FFCI Document Before
                        Hid_FFCI_Before_Doc_Code.Value = ds.Tables[3].Rows[0]["vchFFCIDocBeforeCode"].ToString();
                        Hyp_View_FFCI_Before_Doc.NavigateUrl = strFilePath + ds.Tables[3].Rows[0]["vchFFCIDocBefore"].ToString();
                        Hid_FFCI_Before_File_Name.Value = ds.Tables[3].Rows[0]["vchFFCIDocBefore"].ToString();
                        if (Hid_FFCI_Before_File_Name.Value != "")
                        {
                            Hyp_View_FFCI_Before_Doc.Visible = true;
                            LnkBtn_Delete_FFCI_Before_Doc.Visible = true;
                            FU_FFCI_Before.Enabled = false;
                        }

                        Txt_Land_Before.Text = ds.Tables[3].Rows[0]["decLandAmtBefore"].ToString();
                        Txt_Building_Before.Text = ds.Tables[3].Rows[0]["decBuildingAmtBefore"].ToString();
                        Txt_Plant_Mach_Before.Text = ds.Tables[3].Rows[0]["decPlantMachAmtBefore"].ToString();
                        Txt_Other_Fixed_Asset_Before.Text = ds.Tables[3].Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
                        Txt_Total_Capital_Before.Text = ds.Tables[3].Rows[0]["decTotalAmtBefore"].ToString();

                        ////// Approved DPR Document Before
                        Hid_Approved_DPR_Before_Doc_Code.Value = ds.Tables[3].Rows[0]["vchProjectDocBeforeCode"].ToString();
                        Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = strFilePath + ds.Tables[3].Rows[0]["vchProjectDocBefore"].ToString();
                        Hid_Approved_DPR_Before_File_Name.Value = ds.Tables[3].Rows[0]["vchProjectDocBefore"].ToString();
                        if (Hid_Approved_DPR_Before_File_Name.Value != "")
                        {
                            Hyp_View_Approved_DPR_Before_Doc.Visible = true;
                            LnkBtn_Delete_Approved_DPR_Before_Doc.Visible = true;
                            FU_Approved_DPR_Before.Enabled = false;
                        }
                    }
                    else
                    {
                        Div_Investment_Before.Visible = false;
                        Lbl_Header_Investment.Text = "";
                    }

                    Txt_FFCI_Date_After.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[3].Rows[0]["dtmFFCIDateAfter"]));

                    ////// FFCI Document After
                    Hid_FFCI_After_Doc_Code.Value = ds.Tables[3].Rows[0]["vchFFCIDocAfterCode"].ToString();
                    Hyp_View_FFCI_After_Doc.NavigateUrl = strFilePath + ds.Tables[3].Rows[0]["vchFFCIDocAfter"].ToString();
                    Hid_FFCI_After_File_Name.Value = ds.Tables[3].Rows[0]["vchFFCIDocAfter"].ToString();
                    Hyp_View_FFCI_After_Doc.Visible = true;
                    LnkBtn_Delete_FFCI_After_Doc.Visible = true;
                    FU_FFCI_After.Enabled = false;

                    Txt_Land_After.Text = ds.Tables[3].Rows[0]["decLandAmtAfter"].ToString();
                    Txt_Building_After.Text = ds.Tables[3].Rows[0]["decBuildingAmtAfter"].ToString();
                    Txt_Plant_Mach_After.Text = ds.Tables[3].Rows[0]["decPlantMachAmtAfter"].ToString();
                    Txt_Other_Fixed_Asset_After.Text = ds.Tables[3].Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
                    Txt_Total_Capital_After.Text = ds.Tables[3].Rows[0]["decTotalAmtAfter"].ToString();

                    ////// Approved DPR Document Before
                    Hid_Approved_DPR_After_Doc_Code.Value = ds.Tables[3].Rows[0]["vchProjectDocAfterCode"].ToString();
                    Hyp_View_Approved_DPR_After_Doc.NavigateUrl = strFilePath + ds.Tables[3].Rows[0]["vchProjectDocAfter"].ToString();
                    Hid_Approved_DPR_After_File_Name.Value = ds.Tables[3].Rows[0]["vchProjectDocAfter"].ToString();
                    Hyp_View_Approved_DPR_After_Doc.Visible = true;
                    LnkBtn_Delete_Approved_DPR_After_Doc.Visible = true;
                    FU_Approved_DPR_After.Enabled = false;

                    #endregion

                    #region Means Of Finance

                    /*-------------------------------------------------------------------------*/
                    ///// Means Of Finance Section Binding

                    Txt_Equity_Amt.Text = ds.Tables[4].Rows[0]["decEquity"].ToString();
                    Txt_Loan_Bank_FI.Text = ds.Tables[4].Rows[0]["decLoanBankFI"].ToString();

                    if (ds.Tables[4].Rows[0]["vchTermLoanDoc"].ToString() != "")
                    {
                        Hid_Term_Loan_Doc_Code.Value = ds.Tables[4].Rows[0]["vchTermLoanDocCode"].ToString();
                        Hyp_View_Term_Loan_Doc.NavigateUrl = strFilePath + ds.Tables[4].Rows[0]["vchTermLoanDoc"].ToString();
                        Hid_Term_Loan_File_Name.Value = ds.Tables[4].Rows[0]["vchTermLoanDoc"].ToString();
                        Hyp_View_Term_Loan_Doc.Visible = true;
                        LnkBtn_Delete_Term_Loan_Doc.Visible = true;
                        FU_Term_Loan.Enabled = false;
                    }
                    else
                    {
                        Hyp_View_Term_Loan_Doc.Visible = false;
                        LnkBtn_Delete_Term_Loan_Doc.Visible = false;
                        FU_Term_Loan.Enabled = true;
                    }

                    Txt_FDI_Componet.Text = ds.Tables[4].Rows[0]["decFDIComponet"].ToString();

                    ////// Term Loan
                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        Grd_TL.DataSource = ds.Tables[6];
                        Grd_TL.DataBind();
                    }

                    ////// Working Capital Loan
                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        Grd_WC.DataSource = ds.Tables[7];
                        Grd_WC.DataBind();
                    }

                    #endregion
                }
                else if (strDataSource == "PC")
                {
                    #region DataFound From PC

                    Div_Prod_Comm_Before.Visible = false;
                    Lbl_Text_Prod_Comm.Text = "";
                    Lbl_Text_PC_Issue_Date.Text = "";
                    Div_Prod_Comm_After.Visible = false;
                    Div_Prod_Emp_Before.Visible = false;
                    Lbl_Header_Prod_Emp.Text = "";
                    Div_Investment_Before.Visible = false;
                    Lbl_Header_Investment.Text = "";
                    Div_Pioneer.Visible = false;

                    if (strIsExistBefore == "Y")
                    {
                        #region PCExistBefore

                        ///// Document in Support of Organization Type and Unit Type is not required inside PC Exist Before Section.
                        ///// This Section is handled in side PC Exist After Section

                        Hid_Is_Exist_Before.Value = "Y";
                        Div_Prod_Comm_Before.Visible = true;
                        Lbl_Text_Prod_Comm.Text = "- After E/M/D";
                        Lbl_Text_PC_Issue_Date.Text = "- After E/M/D";
                        Lbl_Text_PC_No.Text = "- After E/M/D";
                        Div_Prod_Emp_Before.Visible = true;
                        Lbl_Header_Prod_Emp.Text = "After E/M/D";
                        Lbl_Header_Investment.Text = "After E/M/D";
                        Div_Investment_Before.Visible = true;

                        DrpDwn_Unit_Type.SelectedValue = ds.Tables[1].Rows[0]["vchAppFor"].ToString();
                        Txt_EIN_IL_NO.Text = ds.Tables[1].Rows[0]["vchEINEMIIPMTNo"].ToString();
                        Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchCompName"].ToString();
                        DrpDwn_Unit_Cat.SelectedValue = ds.Tables[1].Rows[0]["intUnitCat"].ToString();
                        Txt_Industry_Address.Text = ds.Tables[1].Rows[0]["vchAddr"].ToString();
                        Txt_Regd_Office_Address.Text = ds.Tables[1].Rows[0]["vchOffcAddr"].ToString();
                        DrpDwn_Org_Type.SelectedValue = ds.Tables[1].Rows[0]["intOrgType"].ToString();
                        Txt_Partner_Name.Text = ds.Tables[1].Rows[0]["vchOwnerName"].ToString();

                        Rad_Nature_Of_Activity.SelectedValue = ds.Tables[1].Rows[0]["intUnitType"].ToString();

                        Txt_FFCI_Date_Before.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmFFCI"]));
                        Txt_Prod_Comm_Date_Before.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmProdComm"]));
                        Txt_PC_Issue_Date_Before.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmIssueDate"]));
                        Txt_PC_No_Before.Text = ds.Tables[1].Rows[0]["vchPcNo"].ToString();
                        Hid_PC_App_No_Before.Value = ds.Tables[1].Rows[0]["vchAppNo"].ToString();
                        DrpDwn_Sector.SelectedValue = ds.Tables[1].Rows[0]["intSectorId"].ToString();
                        DrpDwn_Sub_Sector.SelectedValue = ds.Tables[1].Rows[0]["intSubSectorId"].ToString();
                        DrpDwn_District.SelectedValue = ds.Tables[1].Rows[0]["intDistrict"].ToString();
                        Txt_Land_Before.Text = ds.Tables[1].Rows[0]["decLandInvestment"].ToString();
                        Txt_Building_Before.Text = ds.Tables[1].Rows[0]["decBuilding"].ToString();
                        Txt_Plant_Mach_Before.Text = ds.Tables[1].Rows[0]["decPlant"].ToString();
                        Txt_Other_Fixed_Asset_Before.Text = ds.Tables[1].Rows[0]["decOthers"].ToString();
                        Txt_Total_Capital_Before.Text = ds.Tables[1].Rows[0]["decTotalAmtBefore"].ToString();

                        LnkBtn_View_Prod_Comm_Before_Doc.Visible = true;

                        Txt_Equity_Amt.Text = ds.Tables[1].Rows[0]["decEquity"].ToString();
                        Txt_Loan_Bank_FI.Text = ds.Tables[1].Rows[0]["decLoan"].ToString();
                        Txt_FDI_Componet.Text = ds.Tables[1].Rows[0]["decFdi"].ToString();

                        Txt_GSTIN.Text = ds.Tables[1].Rows[0]["vchGSTIN"].ToString();

                        string strDerivedSector = ds.Tables[1].Rows[0]["DerivedSector"].ToString();
                        if (strDerivedSector != "")
                        {
                            Lbl_Derived_Sector.Text = strDerivedSector;
                        }
                        else
                        {
                            Lbl_Derived_Sector.Text = "NA";
                        }

                        int intSectoraPolicy = Convert.ToInt32(ds.Tables[1].Rows[0]["bitSectoralPolicy"]);
                        if (intSectoraPolicy == 1)
                        {
                            ChkBx_Sectoral.Checked = true;
                        }
                        else
                        {
                            ChkBx_Sectoral.Checked = false;
                        }

                        /*---------------------------------------------------------------------*/
                        //////// Employment Details
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            Txt_Women_Before.Text = ds.Tables[2].Rows[0]["intWomen"].ToString();
                            Txt_PHD_Before.Text = ds.Tables[2].Rows[0]["intDisabled"].ToString();
                            Txt_Managarial_Before.Text = ds.Tables[2].Rows[0]["intManaregailSkill"].ToString();
                            Txt_Supervisor_Before.Text = ds.Tables[2].Rows[0]["intSupervisor"].ToString();
                            Txt_Skilled_Before.Text = ds.Tables[2].Rows[0]["intSkilled"].ToString();
                            Txt_Semi_Skilled_Before.Text = ds.Tables[2].Rows[0]["intSemiSkilled"].ToString();
                            Txt_Unskilled_Before.Text = ds.Tables[2].Rows[0]["intUnskilled"].ToString();
                            Txt_Total_Emp_Before.Text = ds.Tables[2].Rows[0]["intTotalEmpBefore"].ToString();
                            Txt_General_Before.Text = ds.Tables[2].Rows[0]["intGeneral"].ToString();
                            Txt_SC_Before.Text = ds.Tables[2].Rows[0]["intScTotal"].ToString();
                            Txt_ST_Before.Text = ds.Tables[2].Rows[0]["intStTotal"].ToString();
                            Txt_Total_Cast_Emp_Before.Text = ds.Tables[2].Rows[0]["intTotalEmpCastBefore"].ToString();

                            Txt_Direct_Emp_Before.Text = ds.Tables[2].Rows[0]["intDirectEmp"].ToString();
                            Txt_Contract_Emp_Before.Text = ds.Tables[2].Rows[0]["intContractual"].ToString();
                        }

                        /*---------------------------------------------------------------------*/
                        ///// Production Details
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            Grd_Production_Before.DataSource = ds.Tables[3];
                            Grd_Production_Before.DataBind();
                        }

                        /*---------------------------------------------------------------------*/
                        ///// Term Loan
                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            Grd_TL.DataSource = ds.Tables[4];
                            Grd_TL.DataBind();
                        }

                        /*---------------------------------------------------------------------*/
                        ///// Working Capital Loan
                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            Grd_WC.DataSource = ds.Tables[5];
                            Grd_WC.DataBind();
                        }

                        #endregion
                    }

                    if (strIsExistAfter == "Y")
                    {
                        #region PCExistAfter

                        Div_Prod_Comm_After.Visible = true;

                        int intFirstTable = 0;
                        int intSecondTable = 0;
                        int intThirdTable = 0;
                        int intFourthTable = 0;
                        int intFifthTable = 0;

                        if (strIsExistBefore == "Y")
                        {
                            intFirstTable = 6;
                            intSecondTable = 7;
                            intThirdTable = 8;
                            intFourthTable = 9;
                            intFifthTable = 10;
                        }
                        else
                        {
                            intFirstTable = 1;
                            intSecondTable = 2;
                            intThirdTable = 3;
                            intFourthTable = 4;
                            intFifthTable = 5;
                        }

                        /*---------------------------------------------------------------------*/

                        DrpDwn_Unit_Type.SelectedValue = ds.Tables[intFirstTable].Rows[0]["vchAppFor"].ToString();
                        ///// Assign Unit Type Document Label Name
                        DataSet dsUnitType = (DataSet)ViewState["dynamic_name_doc"];
                        dsUnitType.Tables[1].DefaultView.RowFilter = "slno = '" + DrpDwn_Unit_Type.SelectedValue + "'";
                        DataTable dtUnitType = (dsUnitType.Tables[1].DefaultView).ToTable();
                        if (dtUnitType.Rows.Count > 0)
                        {
                            string strDocName = dtUnitType.Rows[0]["vchDocumentTypename"].ToString();
                            if (strDocName != "")
                            {
                                Lbl_Unit_Type_Doc_Name.Text = strDocName;
                                Div_Unit_Type_Doc.Visible = true;
                            }
                            else
                            {
                                Lbl_Unit_Type_Doc_Name.Text = "";
                                Div_Unit_Type_Doc.Visible = false;
                            }
                        }

                        /*---------------------------------------------------------------------*/

                        Txt_EIN_IL_NO.Text = ds.Tables[intFirstTable].Rows[0]["vchEINEMIIPMTNo"].ToString();
                        Txt_EnterPrise_Name.Text = ds.Tables[intFirstTable].Rows[0]["vchCompName"].ToString();
                        DrpDwn_Unit_Cat.SelectedValue = ds.Tables[intFirstTable].Rows[0]["intUnitCat"].ToString();
                        Txt_Industry_Address.Text = ds.Tables[intFirstTable].Rows[0]["vchAddr"].ToString();
                        Txt_Regd_Office_Address.Text = ds.Tables[intFirstTable].Rows[0]["vchOffcAddr"].ToString();
                        DrpDwn_Org_Type.SelectedValue = ds.Tables[intFirstTable].Rows[0]["intOrgType"].ToString();
                        Txt_Partner_Name.Text = ds.Tables[intFirstTable].Rows[0]["vchOwnerName"].ToString();

                        Rad_Nature_Of_Activity.SelectedValue = ds.Tables[intFirstTable].Rows[0]["intUnitType"].ToString();

                        Txt_FFCI_Date_After.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[intFirstTable].Rows[0]["dtmFFCI"]));
                        Txt_Prod_Comm_Date_After.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[intFirstTable].Rows[0]["dtmProdComm"]));
                        Txt_PC_Issue_Date_After.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[intFirstTable].Rows[0]["dtmIssueDate"]));
                        Txt_PC_No_After.Text = ds.Tables[intFirstTable].Rows[0]["vchPcNo"].ToString();
                        Hid_PC_App_No_After.Value = ds.Tables[1].Rows[0]["vchAppNo"].ToString();

                        /*-------------------------------------------------------------*/
                        DrpDwn_Sector.SelectedValue = ds.Tables[intFirstTable].Rows[0]["intSectorId"].ToString();

                        ///// Fill Filtered Sub-Sector for Above Sector
                        fillSubSectorFiltered();

                        DrpDwn_Sub_Sector.SelectedValue = ds.Tables[intFirstTable].Rows[0]["intSubSectorId"].ToString();
                        /*-------------------------------------------------------------*/

                        int intSectoraPolicy = Convert.ToInt32(ds.Tables[intFirstTable].Rows[0]["bitSectoralPolicy"]);
                        if (intSectoraPolicy == 1)
                        {
                            ChkBx_Sectoral.Checked = true;
                        }
                        else
                        {
                            ChkBx_Sectoral.Checked = false;
                        }

                        DrpDwn_District.SelectedValue = ds.Tables[intFirstTable].Rows[0]["intDistrict"].ToString();
                        Txt_Land_After.Text = ds.Tables[intFirstTable].Rows[0]["decLandInvestment"].ToString();
                        Txt_Building_After.Text = ds.Tables[intFirstTable].Rows[0]["decBuilding"].ToString();
                        Txt_Plant_Mach_After.Text = ds.Tables[intFirstTable].Rows[0]["decPlant"].ToString();
                        Txt_Other_Fixed_Asset_After.Text = ds.Tables[intFirstTable].Rows[0]["decOthers"].ToString();
                        Txt_Total_Capital_After.Text = ds.Tables[intFirstTable].Rows[0]["decTotalAmtAfter"].ToString();

                        /*---------------------------------------------------------------------*/

                        string strOrgTypeDoc = ds.Tables[intFirstTable].Rows[0]["vchFileorgTypeDocument"].ToString();
                        if (strOrgTypeDoc != "")
                        {
                            DataSet dsOrg = (DataSet)ViewState["dynamic_name_doc"];
                            dsOrg.Tables[0].DefaultView.RowFilter = "slno = '" + DrpDwn_Org_Type.SelectedValue + "'";
                            DataTable dtOrg = (dsOrg.Tables[0].DefaultView).ToTable();
                            if (dtOrg.Rows.Count > 0)
                            {
                                Hid_Org_Doc_Type.Value = dtOrg.Rows[0]["vchDocumentType"].ToString();
                                Lbl_Org_Name_Type.Text = dtOrg.Rows[0]["vchOwnerType"].ToString();
                                Lbl_Org_Doc_Type.Text = dtOrg.Rows[0]["vchDocumentTypeName"].ToString();
                            }

                            if (File.Exists(Server.MapPath(@"~/incentives/Files/Industry/" + strOrgTypeDoc)))
                            {
                                if (File.Exists(Server.MapPath(strFilePath + strOrgTypeDoc)))
                                {
                                    File.Delete(Server.MapPath(strFilePath + strOrgTypeDoc));
                                }

                                File.Copy(Server.MapPath(@"~/incentives/Files/Industry/" + strOrgTypeDoc), Server.MapPath(strFilePath + strOrgTypeDoc));

                                Hyp_View_Org_Doc.NavigateUrl = strFilePath + ds.Tables[intFirstTable].Rows[0]["vchFileorgTypeDocument"].ToString();
                                Hid_Org_Doc_File_Name.Value = ds.Tables[intFirstTable].Rows[0]["vchFileorgTypeDocument"].ToString();
                                Hyp_View_Org_Doc.Visible = true;
                                LnkBtn_Delete_Org_Doc.Visible = true;
                                FU_Org_Doc.Enabled = false;
                            }
                        }

                        LnkBtn_View_Prod_Comm_After_Doc.Visible = true;

                        Txt_Equity_Amt.Text = ds.Tables[intFirstTable].Rows[0]["decEquity"].ToString();
                        Txt_Loan_Bank_FI.Text = ds.Tables[intFirstTable].Rows[0]["decLoan"].ToString();
                        Txt_FDI_Componet.Text = ds.Tables[intFirstTable].Rows[0]["decFdi"].ToString();
                        Txt_GSTIN.Text = ds.Tables[intFirstTable].Rows[0]["vchGSTIN"].ToString();

                        ///// Derived Sector Assign
                        string strDerivedSector = ds.Tables[intFirstTable].Rows[0]["DerivedSector"].ToString();
                        if (strDerivedSector != "")
                        {
                            Lbl_Derived_Sector.Text = strDerivedSector;
                        }
                        else
                        {
                            Lbl_Derived_Sector.Text = "NA";
                        }

                        ///// Priority Assign
                        string strPriority = ds.Tables[intFirstTable].Rows[0]["intPriority"].ToString();
                        if (strPriority != "")
                        {
                            Rad_Is_Priority.SelectedValue = strPriority;
                        }
                        else
                        {
                            Rad_Is_Priority.SelectedValue = "2";
                        }

                        ////// Pioneer Assign
                        string strPioneer = ds.Tables[intFirstTable].Rows[0]["intPioneer"].ToString();
                        if (strPioneer != "")
                        {
                            Rad_Is_Pioneer.SelectedValue = strPioneer;
                        }
                        else
                        {
                            Rad_Is_Pioneer.SelectedValue = "2";
                        }

                        /*---------------------------------------------------------------------*/
                        //////// Employment Details
                        if (ds.Tables[intSecondTable].Rows.Count > 0)
                        {
                            Txt_Women_After.Text = ds.Tables[intSecondTable].Rows[0]["intWomen"].ToString();
                            Txt_PHD_After.Text = ds.Tables[intSecondTable].Rows[0]["intDisabled"].ToString();
                            Txt_Managarial_After.Text = ds.Tables[intSecondTable].Rows[0]["intManaregailSkill"].ToString();
                            Txt_Supervisor_After.Text = ds.Tables[intSecondTable].Rows[0]["intSupervisor"].ToString();
                            Txt_Skilled_After.Text = ds.Tables[intSecondTable].Rows[0]["intSkilled"].ToString();
                            Txt_Semi_Skilled_After.Text = ds.Tables[intSecondTable].Rows[0]["intSemiSkilled"].ToString();
                            Txt_Unskilled_After.Text = ds.Tables[intSecondTable].Rows[0]["intUnskilled"].ToString();
                            Txt_Total_Emp_After.Text = ds.Tables[intSecondTable].Rows[0]["intTotalEmpAfter"].ToString();

                            Txt_General_After.Text = ds.Tables[intSecondTable].Rows[0]["intGeneral"].ToString();
                            Txt_SC_After.Text = ds.Tables[intSecondTable].Rows[0]["intScTotal"].ToString();
                            Txt_ST_After.Text = ds.Tables[intSecondTable].Rows[0]["intStTotal"].ToString();
                            Txt_Total_Cast_Emp_After.Text = ds.Tables[intSecondTable].Rows[0]["intTotalEmpCastAfter"].ToString();

                            Txt_Direct_Emp_After.Text = ds.Tables[intSecondTable].Rows[0]["intDirectEmp"].ToString();
                            Txt_Contract_Emp_After.Text = ds.Tables[intSecondTable].Rows[0]["intContractual"].ToString();
                        }

                        /*---------------------------------------------------------------------*/
                        ///// Production Details
                        if (ds.Tables[intThirdTable].Rows.Count > 0)
                        {
                            Grd_Production_After.DataSource = ds.Tables[intThirdTable];
                            Grd_Production_After.DataBind();
                        }

                        /*---------------------------------------------------------------------*/
                        ///// Term Loan
                        if (ds.Tables[intFourthTable].Rows.Count > 0)
                        {
                            Grd_TL.DataSource = ds.Tables[intFourthTable];
                            Grd_TL.DataBind();
                        }

                        /*---------------------------------------------------------------------*/
                        ///// Working Capital Loan
                        if (ds.Tables[intFifthTable].Rows.Count > 0)
                        {
                            Grd_WC.DataSource = ds.Tables[intFifthTable];
                            Grd_WC.DataBind();
                        }

                        #endregion
                    }

                    #endregion
                }
                else if (strDataSource == "PEAL" || strDataSource == "REGD")
                {
                    #region DataFound From PEAL or Registration

                    Div_Prod_Comm_Before.Visible = false;
                    Lbl_Text_Prod_Comm.Text = "";
                    Lbl_Text_PC_Issue_Date.Text = "";
                    Div_Prod_Comm_After.Visible = false;
                    Div_Prod_Emp_Before.Visible = false;
                    Lbl_Header_Prod_Emp.Text = "";
                    Div_Investment_Before.Visible = false;
                    Lbl_Header_Investment.Text = "";
                    Div_Pioneer.Visible = false;

                    /*---------------------------------------------------------------------*/

                    DrpDwn_Unit_Type.SelectedValue = ds.Tables[1].Rows[0]["intapplicationfor"].ToString();

                    ///// Assign Unit Type Document Label Name
                    DataSet dsUnitType = (DataSet)ViewState["dynamic_name_doc"];
                    dsUnitType.Tables[1].DefaultView.RowFilter = "slno = '" + DrpDwn_Unit_Type.SelectedValue + "'";
                    DataTable dtUnitType = (dsUnitType.Tables[1].DefaultView).ToTable();
                    if (dtUnitType.Rows.Count > 0)
                    {
                        Lbl_Unit_Type_Doc_Name.Text = dtUnitType.Rows[0]["vchDocumentTypename"].ToString();
                    }

                    /*---------------------------------------------------------------------*/

                    Txt_EIN_IL_NO.Text = ds.Tables[1].Rows[0]["vchEINnIEMnIL"].ToString();

                    /*---------------------------------------------------------------------*/
                    DrpDwn_Sector.SelectedValue = ds.Tables[1].Rows[0]["intsectorid"].ToString();

                    ///// Fill Filtered Sub-Sector for Above Sector
                    fillSubSectorFiltered();

                    DrpDwn_Sub_Sector.SelectedValue = ds.Tables[1].Rows[0]["intSubSectorId"].ToString();
                    /*---------------------------------------------------------------------*/

                    Txt_Industry_Address.Text = ds.Tables[1].Rows[0]["vchCorAdd"].ToString();
                    Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchNameOfUnit"].ToString();

                    /*---------------------------------------------------------------------*/

                    DrpDwn_Org_Type.SelectedValue = ds.Tables[1].Rows[0]["organizationType"].ToString();
                    ///// Assign Organization Type Document Label Name
                    DataSet dsOrg = (DataSet)ViewState["dynamic_name_doc"];
                    dsOrg.Tables[0].DefaultView.RowFilter = "slno = '" + DrpDwn_Org_Type.SelectedValue + "'";
                    DataTable dtOrg = (dsOrg.Tables[0].DefaultView).ToTable();
                    if (dtOrg.Rows.Count > 0)
                    {
                        Hid_Org_Doc_Type.Value = dtOrg.Rows[0]["vchDocumentType"].ToString();
                        Lbl_Org_Name_Type.Text = dtOrg.Rows[0]["vchOwnerType"].ToString();
                        Lbl_Org_Doc_Type.Text = dtOrg.Rows[0]["vchDocumentTypeName"].ToString();
                    }

                    /*---------------------------------------------------------------------*/

                    Txt_Land_After.Text = ds.Tables[1].Rows[0]["decLandIncLandDev"].ToString();
                    Txt_Building_After.Text = ds.Tables[1].Rows[0]["decBuildingndConstruction"].ToString();
                    Txt_Plant_Mach_After.Text = ds.Tables[1].Rows[0]["decPlantndMachinery"].ToString();
                    Txt_Other_Fixed_Asset_After.Text = ds.Tables[1].Rows[0]["decOthers"].ToString();
                    Txt_Total_Capital_After.Text = ds.Tables[1].Rows[0]["decTotalAmtAfter"].ToString();

                    Txt_Managarial_After.Text = ds.Tables[1].Rows[0]["intManagerProp"].ToString();
                    Txt_Supervisor_After.Text = ds.Tables[1].Rows[0]["intSupervisorProp"].ToString();
                    Txt_Skilled_After.Text = ds.Tables[1].Rows[0]["intSkilledProp"].ToString();
                    Txt_Semi_Skilled_After.Text = ds.Tables[1].Rows[0]["intSemiSkilledProp"].ToString();
                    Txt_Unskilled_After.Text = ds.Tables[1].Rows[0]["intUnSkilledProp"].ToString();
                    Txt_Total_Emp_After.Text = ds.Tables[1].Rows[0]["intTotalEmpAfter"].ToString();

                    //DrpDwn_District.SelectedValue = ds.Tables[1].Rows[0]["intExisDistrict"].ToString();
                    DrpDwn_District.SelectedValue = ds.Tables[1].Rows[0]["intDistrictId"].ToString();
                    DrpDwn_Unit_Cat.SelectedValue = ds.Tables[1].Rows[0]["intProjectType"].ToString();
                    Txt_Partner_Name.Text = ds.Tables[1].Rows[0]["ownername"].ToString();
                    if (ds.Tables[1].Rows[0]["dtmFFCI"].ToString() != "")
                    {
                        Txt_FFCI_Date_After.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(ds.Tables[1].Rows[0]["dtmFFCI"]));
                    }
                    else
                    {
                        Txt_FFCI_Date_After.Text = "";
                    }
                    Txt_Equity_Amt.Text = ds.Tables[1].Rows[0]["decEquityContribution"].ToString();
                    Txt_Loan_Bank_FI.Text = ds.Tables[1].Rows[0]["decBankndInstitutionalFin"].ToString();
                    Txt_FDI_Componet.Text = ds.Tables[1].Rows[0]["decForeignInvestment"].ToString();
                    Txt_Direct_Emp_After.Text = ds.Tables[1].Rows[0]["intPropDirectEmployment"].ToString();
                    Txt_Contract_Emp_After.Text = ds.Tables[1].Rows[0]["intPropContractualEmployment"].ToString();

                    Txt_GSTIN.Text = ds.Tables[1].Rows[0]["vchGSTIN"].ToString();

                    /*---------------------------------------------------------------------*/
                    ////// Assign Sectoral Policy
                    int intSectoraPolicy = Convert.ToInt32(ds.Tables[1].Rows[0]["bitSectoralPolicy"]);
                    if (intSectoraPolicy == 1)
                    {
                        ChkBx_Sectoral.Checked = true;
                    }
                    else
                    {
                        ChkBx_Sectoral.Checked = false;
                    }

                    /*---------------------------------------------------------------------*/
                    ////// Pioneer Assign
                    int intPriorityIPR = Convert.ToInt32(ds.Tables[1].Rows[0]["intPriority"]);
                    if (intPriorityIPR > 0)
                    {
                        Rad_Is_Priority.SelectedValue = intPriorityIPR.ToString();
                        if (intPriorityIPR == 1)
                        {
                            Div_Pioneer.Visible = true;
                            Rad_Is_Pioneer.SelectedValue = "2";
                        }
                    }
                    else
                    {
                        Rad_Is_Priority.SelectedValue = "2";
                    }

                    /*---------------------------------------------------------------------*/
                    ////// Derived Sector Assign
                    string strDerivedSector = ds.Tables[1].Rows[0]["DerivedSector"].ToString();
                    if (strDerivedSector != "")
                    {
                        Lbl_Derived_Sector.Text = strDerivedSector;
                    }
                    else
                    {
                        Lbl_Derived_Sector.Text = "NA";
                    }

                    #endregion
                }

                /*---------------------------------------------------------------*/
                ///// Session Assigned Here

                Session["UnitCode"] = strIndustryCode;
                Session["ProposalNo"] = strProposalNo;
                Session["PCNo"] = Txt_PC_No_After.Text;
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
    //// Bind Other Documents
    private void fillOtherDoc()
    {
        try
        {
            DataSet ds = (DataSet)ViewState["dynamic_name_doc"];

            /*-----------------------------------------------------------------*/
            ///// Certificate on Date of Commencement of production

            ds.Tables[2].DefaultView.RowFilter = "vchDocId = 'D136'";
            DataTable dt1 = (ds.Tables[2].DefaultView).ToTable();

            if (dt1.Rows.Count > 0)
            {
                Lbl_Prod_Comm_Before_Doc_Name.Text = dt1.Rows[0]["vchDocName"].ToString();
                Hid_Prod_Comm_Before_Doc_Code.Value = dt1.Rows[0]["vchDocId"].ToString();

                Lbl_Prod_Comm_After_Doc_Name.Text = dt1.Rows[0]["vchDocName"].ToString();
                Hid_Prod_Comm_After_Doc_Code.Value = dt1.Rows[0]["vchDocId"].ToString();
            }

            /*-----------------------------------------------------------------*/
            ///// Document in Support of Number of Employes shown as directly employed

            ds.Tables[2].DefaultView.RowFilter = "vchDocId = 'D115'";
            DataTable dt2 = (ds.Tables[2].DefaultView).ToTable();

            if (dt2.Rows.Count > 0)
            {
                Lbl_Direct_Emp_Before_Doc_Name.Text = dt2.Rows[0]["vchDocName"].ToString();
                Hid_Direct_Emp_Before_Doc_Code.Value = dt2.Rows[0]["vchDocId"].ToString();

                Lbl_Direct_Emp_After_Doc_Name.Text = dt2.Rows[0]["vchDocName"].ToString();
                Hid_Direct_Emp_After_Doc_Code.Value = dt2.Rows[0]["vchDocId"].ToString();
            }

            /*-----------------------------------------------------------------*/
            ///// Document(s) in support of date of first investment in fixed capital i.e. land building / plant & machinery and balancing equipment

            ds.Tables[2].DefaultView.RowFilter = "vchDocId = 'D106'";
            DataTable dt3 = (ds.Tables[2].DefaultView).ToTable();

            if (dt3.Rows.Count > 0)
            {
                Lbl_FFCI_Before_Doc_Name.Text = dt3.Rows[0]["vchDocName"].ToString();
                Hid_FFCI_Before_Doc_Code.Value = dt3.Rows[0]["vchDocId"].ToString();

                Lbl_FFCI_After_Doc_Name.Text = dt3.Rows[0]["vchDocName"].ToString();
                Hid_FFCI_After_Doc_Code.Value = dt3.Rows[0]["vchDocId"].ToString();
            }

            /*-----------------------------------------------------------------*/
            ///// Document(s) in support of date of first investment in fixed capital i.e. land building / plant & machinery and balancing equipment

            ds.Tables[2].DefaultView.RowFilter = "vchDocId = 'D132'";
            DataTable dt4 = (ds.Tables[2].DefaultView).ToTable();

            if (dt4.Rows.Count > 0)
            {
                Lbl_Approved_DPR_Before_Doc_Name.Text = dt4.Rows[0]["vchDocName"].ToString();
                Hid_Approved_DPR_Before_Doc_Code.Value = dt4.Rows[0]["vchDocId"].ToString();

                Lbl_Approved_DPR_After_Doc_Name.Text = dt4.Rows[0]["vchDocName"].ToString();
                Hid_Approved_DPR_After_Doc_Code.Value = dt4.Rows[0]["vchDocId"].ToString();
            }

            /*-----------------------------------------------------------------*/
            ///// Term loan sanction order of Financial lnstitute (s) / Banks

            ds.Tables[2].DefaultView.RowFilter = "vchDocId = 'D133'";
            DataTable dt5 = (ds.Tables[2].DefaultView).ToTable();

            if (dt5.Rows.Count > 0)
            {
                Lbl_Term_Loan_Doc_Name.Text = dt5.Rows[0]["vchDocName"].ToString();
                Hid_Term_Loan_Doc_Code.Value = dt5.Rows[0]["vchDocId"].ToString();
            }

            /*-----------------------------------------------------------------*/
            ///// Pioneer Document

            ds.Tables[2].DefaultView.RowFilter = "vchDocId = 'D107'";
            DataTable dt6 = (ds.Tables[2].DefaultView).ToTable();

            if (dt6.Rows.Count > 0)
            {
                Lbl_Pioneer_Doc_Name.Text = dt6.Rows[0]["vchDocName"].ToString();
                Hid_Pioneer_Doc_Code.Value = dt6.Rows[0]["vchDocId"].ToString();
            }

            /*-----------------------------------------------------------------*/
            ///// Ancillary/DownStream Document

            ds.Tables[2].DefaultView.RowFilter = "vchDocId = 'D276'";
            DataTable dt7 = (ds.Tables[2].DefaultView).ToTable();

            if (dt7.Rows.Count > 0)
            {
                Lbl_Ancillary_Doc_Name.Text = dt7.Rows[0]["vchDocName"].ToString();
                Hid_Ancillary_Doc_Code.Value = dt7.Rows[0]["vchDocId"].ToString();
            }
            /*-----------------------------------------------------------------*/
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //// Fill Measurment Units
    private void fillUnitMeasurment()
    {
        DataTable table = new DataTable();
        try
        {
            string action = "A";
            CommonDataLayer objDataUnit = new CommonDataLayer();
            DrpDwn_Unit_After.DataTextField = "vchName";
            DrpDwn_Unit_After.DataValueField = "slno";
            DrpDwn_Unit_After.DataSource = objDataUnit.FillUnitType(action);
            DrpDwn_Unit_After.DataBind();
            DrpDwn_Unit_After.Items.Insert(0, new ListItem("-Select-", "0"));

            DrpDwn_Unit_Before.DataTextField = "vchName";
            DrpDwn_Unit_Before.DataValueField = "slno";
            DrpDwn_Unit_Before.DataSource = objDataUnit.FillUnitType(action);
            DrpDwn_Unit_Before.DataBind();
            DrpDwn_Unit_Before.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    //// Enable Disable Form Controls
    private void EnableDisableControl(string data_source)
    {
        try
        {
            if (data_source == "BASIC")
            {
                ///// Industrial Unit Section
                Txt_EnterPrise_Name.Enabled = false;
                DrpDwn_Org_Type.Enabled = false;
                //Txt_Industry_Address.Enabled = false;
                //DrpDwn_Unit_Cat.Enabled = false;
                //DrpDwn_Unit_Type.Enabled = false;
                //Txt_Regd_Office_Address.Enabled = false;
                Txt_Partner_Name.Enabled = false;
                DrpDwn_Gender_Partner.Enabled = false;
                Txt_EIN_IL_NO.Enabled = false;

                Div_Date_EIN.Attributes.Remove("class");
                Span_Date_EIN.Visible = false;

                //DrpDwn_District.Enabled = false;
                DrpDwn_Sector.Enabled = false;
                DrpDwn_Sub_Sector.Enabled = false;
                Rad_Is_Priority.Enabled = false;
                Rad_Is_Pioneer.Enabled = false;
                Rad_Nature_Of_Activity.Enabled = false;

                ChkSameData.Enabled = false;
                Txt_GSTIN.Enabled = false;

                //Rad_Is_Ancillary.Enabled = false;

                if (Hid_PC_Status.Value == "Y")
                {
                    if (Hid_Is_Exist_Before.Value == "Y")
                    {
                        Txt_PC_No_Before.Enabled = false;

                        Div_Date_Prod_Before.Attributes.Remove("class");
                        Span_Date_Prod_Before.Visible = false;

                        Div_Date_PC_Before.Attributes.Remove("class");
                        Span_Date_PC_Before.Visible = false;

                        if (Txt_FFCI_Date_Before.Text != "")
                        {
                            Div_Date_FFCI_Before.Attributes.Remove("class");
                            Span_Date_FFCI_Before.Visible = false;
                        }
                    }

                    if (Hid_Is_Exist_After.Value == "Y")
                    {
                        Txt_PC_No_After.Enabled = false;

                        Div_Date_Prod_After.Attributes.Remove("class");
                        Span_Date_Prod_After.Visible = false;

                        Div_Date_PC_After.Attributes.Remove("class");
                        Span_Date_PC_After.Visible = false;

                        Div_Date_FFCI_After.Attributes.Remove("class");
                        Span_Date_FFCI_After.Visible = false;
                    }
                }

                ///// Production and Employement and Investment Details Section
                if (Hid_Is_Exist_Before.Value == "Y")
                {
                    //Txt_Product_Name_Before.Enabled = false;
                    //Txt_Quantity_Before.Enabled = false;
                    //DrpDwn_Unit_Before.Enabled = false;
                    //Txt_Value_Before.Enabled = false;
                    //LnkBtn_Add_Item_Before.Enabled = false;
                    //Grd_Production_Before.Enabled = false;

                    //Txt_Direct_Emp_Before.Enabled = false;
                    //Txt_Contract_Emp_Before.Enabled = false;
                    //Txt_Managarial_Before.Enabled = false;
                    //Txt_Supervisor_Before.Enabled = false;
                    //Txt_Skilled_Before.Enabled = false;
                    //Txt_Semi_Skilled_Before.Enabled = false;
                    //Txt_Unskilled_Before.Enabled = false;
                    //Txt_General_Before.Enabled = false;
                    //Txt_SC_Before.Enabled = false;
                    //Txt_ST_Before.Enabled = false;
                    //Txt_Women_Before.Enabled = false;
                    //Txt_PHD_Before.Enabled = false;

                    Txt_Land_Before.Enabled = false;
                    Txt_Building_Before.Enabled = false;
                    Txt_Plant_Mach_Before.Enabled = false;
                    Txt_Other_Fixed_Asset_Before.Enabled = false;
                }

                if (Hid_Is_Exist_After.Value == "Y")
                {
                    //Txt_Product_Name_After.Enabled = false;
                    //Txt_Quantity_After.Enabled = false;
                    //DrpDwn_Unit_After.Enabled = false;
                    //Txt_Value_After.Enabled = false;
                    //LnkBtn_Add_Item_After.Enabled = false;
                    //Grd_Production_After.Enabled = false;

                    //Txt_Direct_Emp_After.Enabled = false;
                    //Txt_Contract_Emp_After.Enabled = false;

                    //Txt_Managarial_After.Enabled = false;
                    //Txt_Supervisor_After.Enabled = false;
                    //Txt_Skilled_After.Enabled = false;
                    //Txt_Semi_Skilled_After.Enabled = false;
                    //Txt_Unskilled_After.Enabled = false;
                    //Txt_General_After.Enabled = false;
                    //Txt_SC_After.Enabled = false;
                    //Txt_ST_After.Enabled = false;
                    //Txt_Women_After.Enabled = false;
                    //Txt_PHD_After.Enabled = false;

                    Txt_Land_After.Enabled = false;
                    Txt_Building_After.Enabled = false;
                    Txt_Plant_Mach_After.Enabled = false;
                    Txt_Other_Fixed_Asset_After.Enabled = false;
                }

                Txt_Equity_Amt.Enabled = false;
                Txt_Loan_Bank_FI.Enabled = false;
                Txt_FDI_Componet.Enabled = false;

                //LnkBtn_TL_Add_More.Visible = false;
                //LnkBtn_WC_Add_More.Visible = false;
                //Grd_TL.Enabled = false;
                //Grd_WC.Enabled = false;
            }
            else if (data_source == "PC")   ////// Data Found From PC
            {
                Txt_EnterPrise_Name.Enabled = false;
                DrpDwn_Org_Type.Enabled = false;
                Txt_Industry_Address.Enabled = false;
                DrpDwn_Unit_Cat.Enabled = false;
                DrpDwn_Unit_Type.Enabled = false;
                Txt_Regd_Office_Address.Enabled = false;
                Txt_Partner_Name.Enabled = false;
                DrpDwn_Gender_Partner.Enabled = false;
                Txt_EIN_IL_NO.Enabled = false;
                //Txt_EIN_IL_Date.Enabled = false;           
                DrpDwn_District.Enabled = false;
                DrpDwn_Sector.Enabled = false;
                DrpDwn_Sub_Sector.Enabled = false;
                Rad_Is_Priority.Enabled = false;
                Rad_Is_Pioneer.Enabled = false;
                Rad_Nature_Of_Activity.Enabled = false;

                ChkSameData.Enabled = false;
                Txt_GSTIN.Enabled = false;

                Rad_Nature_Of_Activity.Enabled = false;

                if (Hid_Is_Exist_Before.Value == "Y")
                {
                    Txt_PC_No_Before.Enabled = false;

                    Div_Date_Prod_Before.Attributes.Remove("class");
                    Span_Date_Prod_Before.Visible = false;

                    Div_Date_PC_Before.Attributes.Remove("class");
                    Span_Date_PC_Before.Visible = false;

                    //Txt_Product_Name_Before.Enabled = false;
                    //Txt_Quantity_Before.Enabled = false;
                    //DrpDwn_Unit_Before.Enabled = false;
                    //Txt_Value_Before.Enabled = false;
                    //LnkBtn_Add_Item_Before.Enabled = false;
                    //Grd_Production_Before.Enabled = false;

                    //Txt_Direct_Emp_Before.Enabled = false;
                    //Txt_Contract_Emp_Before.Enabled = false;
                    //Txt_Managarial_Before.Enabled = false;
                    //Txt_Supervisor_Before.Enabled = false;
                    //Txt_Skilled_Before.Enabled = false;
                    //Txt_Semi_Skilled_Before.Enabled = false;
                    //Txt_Unskilled_Before.Enabled = false;
                    //Txt_General_Before.Enabled = false;
                    //Txt_SC_Before.Enabled = false;
                    //Txt_ST_Before.Enabled = false;
                    //Txt_Women_Before.Enabled = false;
                    //Txt_PHD_Before.Enabled = false;

                    Div_Date_FFCI_Before.Attributes.Remove("class");
                    Span_Date_FFCI_Before.Visible = false;

                    Txt_Land_Before.Enabled = false;
                    Txt_Building_Before.Enabled = false;
                    Txt_Plant_Mach_Before.Enabled = false;
                    Txt_Other_Fixed_Asset_Before.Enabled = false;
                }

                Txt_PC_No_After.Enabled = false;

                Div_Date_Prod_After.Attributes.Remove("class");
                Span_Date_Prod_After.Visible = false;

                Div_Date_PC_After.Attributes.Remove("class");
                Span_Date_PC_After.Visible = false;

                //Txt_Quantity_After.Enabled = false;
                //DrpDwn_Unit_After.Enabled = false;
                //Txt_Value_After.Enabled = false;
                //LnkBtn_Add_Item_After.Enabled = false;
                //Grd_Production_After.Enabled = false;

                //Txt_Direct_Emp_After.Enabled = false;
                //Txt_Contract_Emp_After.Enabled = false;
                //Txt_Managarial_After.Enabled = false;
                //Txt_Supervisor_After.Enabled = false;
                //Txt_Skilled_After.Enabled = false;
                //Txt_Semi_Skilled_After.Enabled = false;
                //Txt_Unskilled_After.Enabled = false;
                //Txt_General_After.Enabled = false;
                //Txt_SC_After.Enabled = false;
                //Txt_ST_After.Enabled = false;
                //Txt_Women_After.Enabled = false;
                //Txt_PHD_After.Enabled = false;

                Div_Date_FFCI_After.Attributes.Remove("class");
                Span_Date_FFCI_After.Visible = false;

                Txt_Land_After.Enabled = false;
                Txt_Building_After.Enabled = false;
                Txt_Plant_Mach_After.Enabled = false;
                Txt_Other_Fixed_Asset_After.Enabled = false;
            }
            else if (data_source == "PEAL" || data_source == "REGD")
            {
                Txt_EnterPrise_Name.Enabled = false;
                //DrpDwn_Org_Type.Enabled = false;
                Txt_Industry_Address.Enabled = false;
                //DrpDwn_Unit_Cat.Enabled = false;
                // DrpDwn_Unit_Type.Enabled = false;
                Txt_GSTIN.Enabled = false;
                Txt_EIN_IL_NO.Enabled = false;

                Rad_Is_Priority.Enabled = false;
                Rad_Is_Pioneer.Enabled = false;
            }

            /*------------------------------------------------------*/

            if (Txt_EIN_IL_NO.Text != "")
            {
                Txt_EIN_IL_NO.Enabled = false;
            }
            else
            {
                Txt_EIN_IL_NO.Enabled = true;
            }

            /*------------------------------------------------------*/

            if (Txt_GSTIN.Text != "")
            {
                Txt_GSTIN.Enabled = false;
            }
            else
            {
                Txt_GSTIN.Enabled = true;
            }

            /*------------------------------------------------------*/
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //// Bind Nature of Activity (Company Nature)
    private void fillNatureOfActivity()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "P";
            objBAL.BindRadioButton(Rad_Nature_Of_Activity, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Display Priority Message to User
    private void displayPriorityMsg()
    {
        try
        {
            if (Rad_Priority_User.SelectedValue == "1")
            {
                if (Rad_Is_Priority.SelectedValue == "1")
                {
                    Lbl_Priority_Msg.Visible = false;
                }
                else
                {
                    Lbl_Priority_Msg.Visible = true;
                }
            }
            else if (Rad_Priority_User.SelectedValue == "2")
            {
                Lbl_Priority_Msg.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //// Used to Check Unit Category belongs to which category depending upon the investment on plant and machinary
    private string checkValidUnitCategory()
    {
        string strUnitType = string.Empty;

        decimal investmentAmt = Txt_Plant_Mach_After.Text == "" ? 0 : Convert.ToDecimal(Txt_Plant_Mach_After.Text);

        if (Rad_Nature_Of_Activity.SelectedValue == "40") //For Manufacturing
        {
            if (investmentAmt <= 25)
            {
                strUnitType = "MICRO";
            }
            else if (investmentAmt > 25 && investmentAmt <= 500)
            {
                strUnitType = "SMALL";
            }
            else if (investmentAmt > 500 && investmentAmt <= 1000)
            {
                strUnitType = "MEDIUM";
            }
            else if (investmentAmt > 1000)
            {
                strUnitType = "LARGE";
            }
        }
        else if (Rad_Nature_Of_Activity.SelectedValue == "41")//For Servicing
        {
            if (investmentAmt <= 10)
            {
                strUnitType = "MICRO";
            }
            else if (investmentAmt > 10 && investmentAmt <= 200)
            {
                strUnitType = "SMALL";
            }
            else if (investmentAmt > 200 && investmentAmt <= 500)
            {
                strUnitType = "MEDIUM";
            }
            else if (investmentAmt > 500)
            {
                strUnitType = "LARGE";
            }
        }

        return strUnitType;
    }

    #endregion

    //// Button Save and Proceed
    protected void BtnApply_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        try
        {
            //////// Validate Unit Category belongs to which category depending upon the investment on plant and machinary
            string strValidUnitCat = checkValidUnitCategory();
            if (DrpDwn_Unit_Cat.SelectedItem.Text.ToUpper() != strValidUnitCat)
            {
                string strMsg = "As per your investment <br/>in Plant and Machinery <br/>and nature of activity provided <br/>your unit category is " + strValidUnitCat + " <br/>and your current category is " + DrpDwn_Unit_Cat.SelectedItem.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong> " + strMsg + " !</strong>','" + strProjName + "')", true);
                return;
            }

            #region Production Item Before

            /*---------------------------------------------------------------------*/
            ////// Add Production Item Before

            BasicProductionItemBefore objProdBefore = new BasicProductionItemBefore();
            List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                BasicProductionItemBefore objItem = new BasicProductionItemBefore();

                Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label lblUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
                Label lblOtherUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label lblValue = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField hdnUnit = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");

                objItem.vchProductName = lblProductionName.Text;
                objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
                if (hdnUnit.Value != "")
                {
                    objItem.intUnitType = Convert.ToInt32(hdnUnit.Value);
                }
                objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
                objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
                objItem.chItemFor = "B"; //// Before

                listProdBefore.Add(objItem);
            }

            objEntity1.ProductionItem_BE = listProdBefore;

            #endregion

            #region Production Item After

            /*---------------------------------------------------------------------*/
            ////// Add Production Item After

            BasicProductionItemAfter objProd = new BasicProductionItemAfter();
            List<BasicProductionItemAfter> listProdAfter = new List<BasicProductionItemAfter>();

            for (int i = 0; i < Grd_Production_After.Rows.Count; i++)
            {
                BasicProductionItemAfter objItem = new BasicProductionItemAfter();

                Label lblProductionName = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Product_Name_After");
                Label lblQuantity = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Quantity_After");
                Label lblUnit = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Unit_After");
                Label lblOtherUnit = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Other_Unit_After");
                Label lblValue = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Value_After");
                HiddenField hdnUnit = (HiddenField)Grd_Production_After.Rows[i].FindControl("Hid_Unit_After");

                objItem.vchProductName = lblProductionName.Text;
                objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
                if (hdnUnit.Value != "")
                {
                    objItem.intUnitType = Convert.ToInt16(hdnUnit.Value);
                }
                objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
                objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
                objItem.chItemFor = "A";  //// After

                listProdAfter.Add(objItem);
            }
            objEntity1.ProductionItem_AF = listProdAfter;

            #endregion

            #region Term Loan
            /*---------------------------------------------------------------------*/
            ////// Add Term Loan

            BasicTermLoan objTL = new BasicTermLoan();
            List<BasicTermLoan> listTL = new List<BasicTermLoan>();

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                BasicTermLoan objItem = new BasicTermLoan();

                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_TL_Financial_Inst.Text;
                objItem.vchState = Lbl_TL_State.Text;
                objItem.vchCity = Lbl_TL_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_TL_Amount.Text == "" ? "0" : Lbl_TL_Amount.Text);
                objItem.dtmSanctionDate = Lbl_TL_Sanction_Date.Text == "" ? null : Lbl_TL_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_TL_Avail_Amt.Text == "" ? "0" : Lbl_TL_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_TL_Avail_Date.Text == "" ? null : Lbl_TL_Avail_Date.Text;

                listTL.Add(objItem);
            }

            objEntity1.TermLoan = listTL;
            #endregion

            #region Working Capital Loan

            /*---------------------------------------------------------------------*/
            ////// Add Working Capital Loan

            BasicWorkingCapitalLoan objWC = new BasicWorkingCapitalLoan();
            List<BasicWorkingCapitalLoan> listWC = new List<BasicWorkingCapitalLoan>();

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                BasicWorkingCapitalLoan objItem = new BasicWorkingCapitalLoan();

                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_WC_Financial_Inst.Text;
                objItem.vchState = Lbl_WC_State.Text;
                objItem.vchCity = Lbl_WC_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_WC_Amount.Text == "" ? "0" : Lbl_WC_Amount.Text);
                objItem.dtmSanctionDate = Lbl_WC_Sanction_Date.Text == "" ? null : Lbl_WC_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_WC_Avail_Amt.Text == "" ? "0" : Lbl_WC_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_WC_Avail_Date.Text == "" ? null : Lbl_WC_Avail_Date.Text;

                listWC.Add(objItem);
            }

            objEntity1.WorkingCapitalLoan = listWC;
            #endregion

            #region IndustrialUnitDetailsSection

            /*---------------------------------------------------------------------*/
            /////// Industrial Unit Details Section

            ////objEntity.intIndustrailUnit 
            objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
            objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
            objEntity1.strIndustryAddress = Txt_Industry_Address.Text;
            objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);
            objEntity1.intUnitType = Convert.ToInt32(DrpDwn_Unit_Type.SelectedValue);

            if (Lbl_Unit_Type_Doc_Name.Text != "")
            {
                if (Hid_Unit_Type_File_Name.Value != "")
                {
                    objEntity1.strDocCode = Hid_Unit_Type_Doc_Code.Value;
                    objEntity1.strUnitTypeDoc = Hid_Unit_Type_File_Name.Value;
                }
                else
                {
                    objEntity1.strDocCode = null;
                    objEntity1.strUnitTypeDoc = null;
                }
            }
            else
            {
                objEntity1.strDocCode = null;
                objEntity1.strUnitTypeDoc = null;
            }

            objEntity1.strRegisteredOfcAddress = Txt_Regd_Office_Address.Text;
            objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
            objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;
            objEntity1.strCertOfRegdDocCode = Hid_Org_Doc_Type.Value;
            objEntity1.strCertOfRegdDocFileName = Hid_Org_Doc_File_Name.Value;
            objEntity1.strEINNO = Txt_EIN_IL_NO.Text;

            objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;
            objEntity1.strPcNoBefore = Txt_PC_No_Before.Text == "" ? null : Txt_PC_No_Before.Text;
            objEntity1.dtmProdCommBefore = Txt_Prod_Comm_Date_Before.Text == "" ? null : Txt_Prod_Comm_Date_Before.Text;
            objEntity1.dtmPCIssueDateBefore = Txt_PC_Issue_Date_Before.Text == "" ? null : Txt_PC_Issue_Date_Before.Text;

            //if (Hid_Prod_Comm_Before_File_Name.Value != "")
            //{
            //    objEntity1.strProdCommCertBeforeCode = Hid_Prod_Comm_Before_Doc_Code.Value;
            //    objEntity1.strProdCommCertBefore = Hid_Prod_Comm_Before_File_Name.Value;
            //}
            //else
            //{
            //    objEntity1.strProdCommCertBeforeCode = null;
            //    objEntity1.strProdCommCertBefore = null;
            //}

            objEntity1.strProdCommCertBeforeCode = null;
            objEntity1.strProdCommCertBefore = null;

            objEntity1.strPcNoAfter = Txt_PC_No_After.Text == "" ? null : Txt_PC_No_After.Text;
            objEntity1.dtmProdCommAfter = Txt_Prod_Comm_Date_After.Text == "" ? null : Txt_Prod_Comm_Date_After.Text;
            objEntity1.dtmPCIssueDateAfter = Txt_PC_Issue_Date_After.Text == "" ? null : Txt_PC_Issue_Date_After.Text;

            //if (Hid_Prod_Comm_After_File_Name.Value != "")
            //{
            //    objEntity1.strProdCommCertAfterCode = Hid_Prod_Comm_After_Doc_Code.Value;
            //    objEntity1.strProdCommCertAfter = Hid_Prod_Comm_After_File_Name.Value;
            //}
            //else
            //{
            //    objEntity1.strProdCommCertAfterCode = null;
            //    objEntity1.strProdCommCertAfter = null;
            //}

            objEntity1.strProdCommCertAfterCode = null;
            objEntity1.strProdCommCertAfter = null;

            objEntity1.intDistrictCode = Convert.ToInt32(DrpDwn_District.SelectedValue);
            objEntity1.intSectorId = Convert.ToInt32(DrpDwn_Sector.SelectedValue);
            objEntity1.intSubSectorId = Convert.ToInt32(DrpDwn_Sub_Sector.SelectedValue);

            objEntity1.intCompNature = Convert.ToInt32(Rad_Nature_Of_Activity.SelectedValue);
            objEntity1.intPriorityUser = Convert.ToInt32(Rad_Priority_User.SelectedValue);

            objEntity1.intPriority = Convert.ToInt32(Rad_Is_Priority.SelectedValue);

            if (Rad_Is_Pioneer.SelectedIndex >= 0)
            {
                objEntity1.intPioneer = Convert.ToInt32(Rad_Is_Pioneer.SelectedValue);
            }
            else
            {
                objEntity1.intPioneer = 0;
            }

            if (Hid_Pioneer_Doc_File_Name.Value != "")
            {
                objEntity1.strPioneerCertificateCode = Hid_Pioneer_Doc_Code.Value;
                objEntity1.strPioneerCertificate = Hid_Pioneer_Doc_File_Name.Value;
            }
            else
            {
                objEntity1.strPioneerCertificateCode = null;
                objEntity1.strPioneerCertificate = null;
            }

            objEntity1.strDerivedSector = Lbl_Derived_Sector.Text == "" ? null : Lbl_Derived_Sector.Text;
            int intSecPlc = 0;
            if (ChkBx_Sectoral.Checked == true)
            {
                intSecPlc = 1;
            }
            objEntity1.bitSectoralPolicy = intSecPlc;
            objEntity1.bitPriorityIPR = 0;

            objEntity1.strGSTIN = Txt_GSTIN.Text == "" ? null : Txt_GSTIN.Text;

            objEntity1.intIsAncillary = Convert.ToInt16(Rad_Is_Ancillary.SelectedValue);
            if (Rad_Is_Ancillary.SelectedValue == "1")
            {
                objEntity1.strAncillaryDocCode = Hid_Ancillary_Doc_Code.Value == "" ? null : Hid_Ancillary_Doc_Code.Value;
                objEntity1.strAncillaryFileName = Hid_Ancillary_Doc_File_Name.Value == "" ? null : Hid_Ancillary_Doc_File_Name.Value;
            }
            else
            {
                objEntity1.strAncillaryDocCode = null;
                objEntity1.strAncillaryFileName = null;
            }

            /*-------------------------Industrial Unit Details End---------------------------------*/


            #endregion

            #region ProductionandEmploymentDetails Section

            /*----------------------------------------------------------*/
            ////// Production and Employment Details Section
            /*----------------------------------------------------------*/

            ///// Production and Employment (Before)
            objEntity1.intDirectEmpBefore = Convert.ToInt32(Txt_Direct_Emp_Before.Text == "" ? "0" : Txt_Direct_Emp_Before.Text);
            objEntity1.intContractualEmpBefore = Convert.ToInt32(Txt_Contract_Emp_Before.Text == "" ? "0" : Txt_Contract_Emp_Before.Text);
            if (Hid_Direct_Emp_Before_File_Name.Value != "")
            {
                objEntity1.strEmpDocBeforeCode = Hid_Direct_Emp_Before_Doc_Code.Value;
                objEntity1.strEmpDocBefore = Hid_Direct_Emp_Before_File_Name.Value;
            }
            else
            {
                objEntity1.strEmpDocBeforeCode = null;
                objEntity1.strEmpDocBefore = null;
            }
            /*----------------------------------------------------------*/

            int intManagerialBefore = 0;
            int intSupervisorBefore = 0;
            int intSkilledBefore = 0;
            int intSemiSkilledBefore = 0;
            int intUnskilledBefore = 0;

            if (Txt_Managarial_Before.Text != "")
            {
                intManagerialBefore = Convert.ToInt32(Txt_Managarial_Before.Text);
            }
            if (Txt_Supervisor_Before.Text != "")
            {
                intSupervisorBefore = Convert.ToInt32(Txt_Supervisor_Before.Text);
            }
            if (Txt_Skilled_Before.Text != "")
            {
                intSkilledBefore = Convert.ToInt32(Txt_Skilled_Before.Text);
            }
            if (Txt_Semi_Skilled_Before.Text != "")
            {
                intSemiSkilledBefore = Convert.ToInt32(Txt_Semi_Skilled_Before.Text);
            }
            if (Txt_Unskilled_Before.Text != "")
            {
                intUnskilledBefore = Convert.ToInt32(Txt_Unskilled_Before.Text);
            }
            int intTotalEmpBefore = intManagerialBefore + intSupervisorBefore + intSkilledBefore + intSemiSkilledBefore + intUnskilledBefore;

            objEntity1.intManagerialBefore = intManagerialBefore;
            objEntity1.intSupervisorBefore = intSupervisorBefore;
            objEntity1.intSkilledBefore = intSkilledBefore;
            objEntity1.intSemiSkilledBefore = intSemiSkilledBefore;
            objEntity1.intUnskilledBefore = intUnskilledBefore;
            objEntity1.intTotalEmpBefore = intTotalEmpBefore;
            /*----------------------------------------------------------*/
            int intGeneralBefore = 0;
            int intSCBefore = 0;
            int intSTBefore = 0;
            if (Txt_General_Before.Text != "")
            {
                intGeneralBefore = Convert.ToInt32(Txt_General_Before.Text);
            }
            if (Txt_SC_Before.Text != "")
            {
                intSCBefore = Convert.ToInt32(Txt_SC_Before.Text);
            }
            if (Txt_ST_Before.Text != "")
            {
                intSTBefore = Convert.ToInt32(Txt_ST_Before.Text);
            }
            int intTotalEmpCastBefore = intGeneralBefore + intSCBefore + intSTBefore;

            objEntity1.intGeneralBefore = intGeneralBefore;
            objEntity1.intSCBefore = intSCBefore;
            objEntity1.intSTBefore = intSTBefore;
            objEntity1.intTotalEmpCastBefore = intTotalEmpCastBefore;
            /*----------------------------------------------------------*/
            objEntity1.intWomenBefore = Convert.ToInt32(Txt_Women_Before.Text == "" ? "0" : Txt_Women_Before.Text);
            objEntity1.intDisabledBefore = Convert.ToInt32(Txt_PHD_Before.Text == "" ? "0" : Txt_PHD_Before.Text);

            /*----------------------------------------------------------*/
            ///// Production and Employment (After)

            objEntity1.intDirectEmpAfter = Convert.ToInt32(Txt_Direct_Emp_After.Text == "" ? "0" : Txt_Direct_Emp_After.Text);
            objEntity1.intContractualEmpAfter = Convert.ToInt32(Txt_Contract_Emp_After.Text == "" ? "0" : Txt_Contract_Emp_After.Text);

            if (Hid_Direct_Emp_After_File_Name.Value != "")
            {
                objEntity1.strEmpDocAfterCode = Hid_Direct_Emp_After_Doc_Code.Value;
                objEntity1.strEmpDocAfter = Hid_Direct_Emp_After_File_Name.Value;
            }
            else
            {
                objEntity1.strEmpDocAfterCode = null;
                objEntity1.strEmpDocAfter = null;
            }

            /*----------------------------------------------------------*/
            int intManagerialAfter = 0;
            int intSupervisorAfter = 0;
            int intSkilledAfter = 0;
            int intSemiSkilledAfter = 0;
            int intUnskilledAfter = 0;

            if (Txt_Managarial_After.Text != "")
            {
                intManagerialAfter = Convert.ToInt32(Txt_Managarial_After.Text);
            }
            if (Txt_Supervisor_After.Text != "")
            {
                intSupervisorAfter = Convert.ToInt32(Txt_Supervisor_After.Text);
            }
            if (Txt_Skilled_After.Text != "")
            {
                intSkilledAfter = Convert.ToInt32(Txt_Skilled_After.Text);
            }
            if (Txt_Semi_Skilled_After.Text != "")
            {
                intSemiSkilledAfter = Convert.ToInt32(Txt_Semi_Skilled_After.Text);
            }
            if (Txt_Unskilled_After.Text != "")
            {
                intUnskilledAfter = Convert.ToInt32(Txt_Unskilled_After.Text);
            }

            int intTotalEmpAfter = intManagerialAfter + intSupervisorAfter + intSkilledAfter + intSemiSkilledAfter + intUnskilledAfter;

            objEntity1.intManagerialAfter = intManagerialAfter;
            objEntity1.intSupervisorAfter = intSupervisorAfter;
            objEntity1.intSkilledAfter = intSkilledAfter;
            objEntity1.intSemiSkilledAfter = intSemiSkilledAfter;
            objEntity1.intUnskilledAfter = intUnskilledAfter;
            objEntity1.intTotalEmpAfter = intTotalEmpAfter;
            /*----------------------------------------------------------*/
            int intGeneralAfter = 0;
            int intSCAfter = 0;
            int intSTAfter = 0;
            if (Txt_General_After.Text != "")
            {
                intGeneralAfter = Convert.ToInt32(Txt_General_After.Text);
            }
            if (Txt_SC_After.Text != "")
            {
                intSCAfter = Convert.ToInt32(Txt_SC_After.Text);
            }
            if (Txt_ST_After.Text != "")
            {
                intSTAfter = Convert.ToInt32(Txt_ST_After.Text);
            }
            int intTotalEmpCastAfter = intGeneralAfter + intSCAfter + intSTAfter;

            objEntity1.intGeneralAfter = intGeneralAfter;
            objEntity1.intSCAfter = intSCAfter;
            objEntity1.intSTAfter = intSTAfter;
            objEntity1.intTotalEmpCastAfter = intTotalEmpCastAfter;
            /*----------------------------------------------------------*/
            objEntity1.intWomenAfter = Convert.ToInt32(Txt_Women_After.Text == "" ? "0" : Txt_Women_After.Text);
            objEntity1.intDisabledAfter = Convert.ToInt32(Txt_PHD_After.Text == "" ? "0" : Txt_PHD_After.Text);

            ///*-------------------------Production and Employment Details Section End---------------------------------*/

            #endregion

            #region InvestmentDetailsSection

            /*----------------------------------------------------------*/
            ////// Investment Details Section
            /*----------------------------------------------------------*/

            ///// Investment Details (Before)
            objEntity1.dtmFFCIDateBefore = Txt_FFCI_Date_Before.Text == "" ? null : Txt_FFCI_Date_Before.Text;
            if (Hid_FFCI_Before_File_Name.Value != "")
            {
                objEntity1.strFFCIDocBeforeCode = Hid_FFCI_Before_Doc_Code.Value;
                objEntity1.strFFCIDocBefore = Hid_FFCI_Before_File_Name.Value;
            }
            else
            {
                objEntity1.strFFCIDocBeforeCode = null;
                objEntity1.strFFCIDocBefore = null;
            }

            decimal decLandBefore = 0;
            decimal decBuildingBefore = 0;
            decimal decPlantMachBefore = 0;
            decimal decOtherFixedAssetBefore = 0;

            if (Txt_Land_Before.Text != "")
            {
                decLandBefore = Convert.ToDecimal(Txt_Land_Before.Text);
            }
            if (Txt_Building_Before.Text != "")
            {
                decBuildingBefore = Convert.ToDecimal(Txt_Building_Before.Text);
            }
            if (Txt_Plant_Mach_Before.Text != "")
            {
                decPlantMachBefore = Convert.ToDecimal(Txt_Plant_Mach_Before.Text);
            }
            if (Txt_Other_Fixed_Asset_Before.Text != "")
            {
                decOtherFixedAssetBefore = Convert.ToDecimal(Txt_Other_Fixed_Asset_Before.Text);
            }
            decimal decTotalCapitalBefore = decLandBefore + decBuildingBefore + decPlantMachBefore + decOtherFixedAssetBefore;

            objEntity1.decLandAmtBefore = decLandBefore;
            objEntity1.decBuildingAmtBefore = decBuildingBefore;
            objEntity1.decPlantMachAmtBefore = decPlantMachBefore;
            objEntity1.decOtheFixedAssetAmtBefore = decOtherFixedAssetBefore;
            objEntity1.decTotalAmtBefore = decTotalCapitalBefore;

            if (Hid_Approved_DPR_Before_File_Name.Value != "")
            {
                objEntity1.strProjectDocBeforeCode = Hid_Approved_DPR_Before_Doc_Code.Value;
                objEntity1.strProjectDocBefore = Hid_Approved_DPR_Before_File_Name.Value;
            }
            else
            {
                objEntity1.strProjectDocBeforeCode = null;
                objEntity1.strProjectDocBefore = null;
            }

            /*---------------------------------------------------------------------*/
            ///// Investment Details (After)

            objEntity1.dtmFFCIDateAfter = Txt_FFCI_Date_After.Text == "" ? null : Txt_FFCI_Date_After.Text;
            if (Hid_FFCI_After_File_Name.Value != "")
            {
                objEntity1.strFFCIDocAfterCode = Hid_FFCI_After_Doc_Code.Value;
                objEntity1.strFFCIDocAfter = Hid_FFCI_After_File_Name.Value;
            }
            else
            {
                objEntity1.strFFCIDocAfterCode = null;
                objEntity1.strFFCIDocAfter = null;
            }
            /*---------------------------------------------------------------------*/

            decimal decLandAfter = 0;
            decimal decBuildingAfter = 0;
            decimal decPlantMachAfter = 0;
            decimal decOtherFixedAssetAfter = 0;

            if (Txt_Land_After.Text != "")
            {
                decLandAfter = Convert.ToDecimal(Txt_Land_After.Text);
            }
            if (Txt_Building_After.Text != "")
            {
                decBuildingAfter = Convert.ToDecimal(Txt_Building_After.Text);
            }
            if (Txt_Plant_Mach_After.Text != "")
            {
                decPlantMachAfter = Convert.ToDecimal(Txt_Plant_Mach_After.Text);
            }
            if (Txt_Other_Fixed_Asset_After.Text != "")
            {
                decOtherFixedAssetAfter = Convert.ToDecimal(Txt_Other_Fixed_Asset_After.Text);
            }
            decimal decTotalCapitalAfter = decLandAfter + decBuildingAfter + decPlantMachAfter + decOtherFixedAssetAfter;

            objEntity1.decLandAmtAfter = decLandAfter;
            objEntity1.decBuildingAmtAfter = decBuildingAfter;
            objEntity1.decPlantMachAmtAfter = decPlantMachAfter;
            objEntity1.decOtheFixedAssetAmtAfter = decOtherFixedAssetAfter;
            objEntity1.decTotalAmtAfter = decTotalCapitalAfter;

            if (Hid_Approved_DPR_After_File_Name.Value != "")
            {
                objEntity1.strProjectDocAfterCode = Hid_Approved_DPR_After_Doc_Code.Value;
                objEntity1.strProjectDocAfter = Hid_Approved_DPR_After_File_Name.Value;
            }
            else
            {
                objEntity1.strProjectDocAfterCode = null;
                objEntity1.strProjectDocAfter = null;
            }

            /*----------------------------Investment Details Section End------------------------------*/
            #endregion

            #region MeansofFinanceSection

            /*----------------------------------------------------------*/
            ////// Means of Finance Section

            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text == "" ? "0" : Txt_Equity_Amt.Text);
            objEntity1.decLoanBankFI = Convert.ToDecimal(Txt_Loan_Bank_FI.Text == "" ? "0" : Txt_Loan_Bank_FI.Text);
            if (Hid_Term_Loan_File_Name.Value != "")
            {
                objEntity1.strTermLoanDocCode = Hid_Term_Loan_Doc_Code.Value;
                objEntity1.strTermLoanDoc = Hid_Term_Loan_File_Name.Value;
            }
            else
            {
                objEntity1.strTermLoanDocCode = null;
                objEntity1.strTermLoanDoc = null;
            }

            objEntity1.decFDIComponet = Convert.ToDecimal(Txt_FDI_Componet.Text == "" ? "0" : Txt_FDI_Componet.Text);

            #endregion

            /*----------------------------------------------------------*/

            objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);

            /*---------------------------------------------------------------*/
            /////// Data Insert and Update

            string strReturnStatus = objBAL.Basic_Unit_Details_AED(objEntity1);

            ///*---------------------------------------------------------------*/

            //////ScriptManager.RegisterStartupScript(Btn_Proceed, this.GetType(), "Myalert", "alert('" + Messages.ShowMessage(strReturnStatus) + "');window.location.href='appliedlistwithdetails.aspx?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong> " + Messages.ShowMessage(strReturnStatus) + " !</strong>','" + strProjName + "')", true);

            ////ModalPopupExtender1.Show();

            string strInctId = Request.QueryString["key"].ToString();

            if (strInctId == "10100102") ///// Provisional Priority
            {
                Response.Redirect("Grantprioritysectorsstatus.aspx?IncentiveNo=" + strInctId + "", false);
            }
            else if (strInctId == "10100119") ///// Priority
            {
                Response.Redirect("PostGrantprioritysectorsstatus.aspx?IncentiveNo=" + strInctId + "", false);
            }
            else if (strInctId == "10100103") ///// Pioneer
            {
                Response.Redirect("PioneerUnit.aspx?IncentiveNo=" + strInctId + "", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity1 = null;
        }
    }

    //// Button Submit on Modal Popup
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ChkBx_Agree.Checked == true)
            {
                ModalPopupExtender1.Hide();
                Response.Redirect("appliedlistwithdetails.aspx", false);
            }
            else
            {
                ChkBx_Agree.Focus();
                ModalPopupExtender1.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Please Click on CheckBox to Agree !</strong>','" + strProjName + "')", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    //// Addmore Section
    #region Addmore Section

    ////  Items of Manufacture/Activity Before
    protected void LnkBtn_Add_Item_Before_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));
            table.Rows.Add(Txt_Product_Name_Before.Text, Txt_Quantity_Before.Text, DrpDwn_Unit_Before.SelectedItem.Text, DrpDwn_Unit_Before.SelectedValue, Txt_Other_Unit_Before.Text == "" ? null : Txt_Other_Unit_Before.Text, Txt_Value_Before.Text);
            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                Label Lbl_Product_Name_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label Lbl_Quantity_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label Lbl_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
                Label Lbl_Other_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label Lbl_Value_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField Hid_Unit_Before = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");
                table.Rows.Add(Lbl_Product_Name_Before.Text, Lbl_Quantity_Before.Text, Lbl_Unit_Before.Text, Hid_Unit_Before.Value, Lbl_Other_Unit_Before.Text, Lbl_Value_Before.Text);
            }

            Grd_Production_Before.DataSource = table;
            Grd_Production_Before.DataBind();

            Txt_Product_Name_Before.Text = "";
            Txt_Quantity_Before.Text = "";
            DrpDwn_Unit_Before.SelectedIndex = 0;
            Txt_Value_Before.Text = "";
            Txt_Other_Unit_Before.Text = "";
            Txt_Other_Unit_Before.Visible = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    protected void ImgBtn_Delete_Before_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Product_Name_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                    Label Lbl_Quantity_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                    Label Lbl_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
                    Label Lbl_Other_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                    Label Lbl_Value_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                    HiddenField Hid_Unit_Before = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");
                    table.Rows.Add(Lbl_Product_Name_Before.Text, Lbl_Quantity_Before.Text, Lbl_Unit_Before.Text, Hid_Unit_Before.Value, Lbl_Other_Unit_Before.Text, Lbl_Value_Before.Text);
                }
            }

            Grd_Production_Before.DataSource = table;
            Grd_Production_Before.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }

    ////  Items of Manufacture/Activity After
    protected void LnkBtn_Add_Item_After_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));
            table.Rows.Add(Txt_Product_Name_After.Text, Txt_Quantity_After.Text, DrpDwn_Unit_After.SelectedItem.Text, DrpDwn_Unit_After.SelectedValue, Txt_Other_Unit_After.Text == "" ? null : Txt_Other_Unit_After.Text, Txt_Value_After.Text);
            for (int i = 0; i < Grd_Production_After.Rows.Count; i++)
            {
                Label Lbl_Product_Name_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Product_Name_After");
                Label Lbl_Quantity_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Quantity_After");
                Label Lbl_Unit_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Unit_After");
                Label Lbl_Other_Unit_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Other_Unit_After");
                Label Lbl_Value_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Value_After");
                HiddenField Hid_Unit_After = (HiddenField)Grd_Production_After.Rows[i].FindControl("Hid_Unit_After");
                table.Rows.Add(Lbl_Product_Name_After.Text, Lbl_Quantity_After.Text, Lbl_Unit_After.Text, Hid_Unit_After.Value, Lbl_Other_Unit_After.Text == "" ? null : Lbl_Other_Unit_After.Text, Lbl_Value_After.Text);
            }

            Grd_Production_After.DataSource = table;
            Grd_Production_After.DataBind();

            Txt_Product_Name_After.Text = "";
            Txt_Quantity_After.Text = "";
            DrpDwn_Unit_After.SelectedIndex = 0;
            Txt_Value_After.Text = "";
            Txt_Other_Unit_After.Text = "";
            Txt_Other_Unit_After.Visible = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    protected void ImgBtn_Delete_After_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));

            for (int i = 0; i < Grd_Production_After.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Product_Name_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Product_Name_After");
                    Label Lbl_Quantity_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Quantity_After");
                    Label Lbl_Unit_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Unit_After");
                    Label Lbl_Other_Unit_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Other_Unit_After");
                    Label Lbl_Value_After = (Label)Grd_Production_After.Rows[i].FindControl("Lbl_Value_After");
                    HiddenField Hid_Unit_After = (HiddenField)Grd_Production_After.Rows[i].FindControl("Hid_Unit_After");
                    table.Rows.Add(Lbl_Product_Name_After.Text, Lbl_Quantity_After.Text, Lbl_Unit_After.Text, Hid_Unit_After.Value, Lbl_Other_Unit_After.Text, Lbl_Value_After.Text);
                }
            }

            Grd_Production_After.DataSource = table;
            Grd_Production_After.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }

    ////  Term Loan Add More
    protected void LnkBtn_TL_Add_More_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            table.Rows.Add(Txt_TL_Financial_Institution.Text, Txt_TL_State.Text, Txt_TL_City.Text, Txt_TL_Amount.Text, Txt_TL_Sanction_Date.Text, Txt_TL_Avail_Amount.Text, Txt_TL_Availed_Date.Text);
            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");
                table.Rows.Add(Lbl_TL_Financial_Inst.Text, Lbl_TL_State.Text, Lbl_TL_City.Text, Lbl_TL_Amount.Text, Lbl_TL_Sanction_Date.Text, Lbl_TL_Avail_Amt.Text, Lbl_TL_Avail_Date.Text);
            }

            Grd_TL.DataSource = table;
            Grd_TL.DataBind();

            Txt_TL_Financial_Institution.Text = "";
            Txt_TL_State.Text = "";
            Txt_TL_City.Text = "";
            Txt_TL_Amount.Text = "";
            Txt_TL_Sanction_Date.Text = "";
            Txt_TL_Avail_Amount.Text = "";
            Txt_TL_Availed_Date.Text = "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    protected void ImgBtn_Delete_TL_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                    Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                    Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                    Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                    Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                    Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                    Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");
                    table.Rows.Add(Lbl_TL_Financial_Inst.Text, Lbl_TL_State.Text, Lbl_TL_City.Text, Lbl_TL_Amount.Text, Lbl_TL_Sanction_Date.Text, Lbl_TL_Avail_Amt.Text, Lbl_TL_Avail_Date.Text);
                }
            }

            Grd_TL.DataSource = table;
            Grd_TL.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }

    ////  Working Capital Loan Add More
    protected void LnkBtn_WC_Add_More_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            table.Rows.Add(Txt_WC_Financial_Institution.Text, Txt_WC_State.Text, Txt_WC_City.Text, Txt_WC_Amount.Text, Txt_WC_Sanction_Date.Text, Txt_WC_Avail_Amount.Text, Txt_WC_Availed_Date.Text);
            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");
                table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Txt_WC_Availed_Date.Text);
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();

            Txt_WC_Financial_Institution.Text = "";
            Txt_WC_State.Text = "";
            Txt_WC_City.Text = "";
            Txt_WC_Amount.Text = "";
            Txt_WC_Sanction_Date.Text = "";
            Txt_WC_Avail_Amount.Text = "";
            Txt_WC_Availed_Date.Text = "";

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    protected void ImgBtn_Delete_WC_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                    Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                    Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                    Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                    Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                    Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                    Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");
                    table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Txt_WC_Availed_Date.Text);
                }
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }

    #endregion

    //// Document Upload Delete and View
    #region Document Upload Delete and View

    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkBtn_Upload_Org_Doc.ID))
            {
                if (FU_Org_Doc.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ORGDOC";
                    UploadDocument(FU_Org_Doc, Hid_Org_Doc_File_Name, strFileName, Hyp_View_Org_Doc, Lbl_Msg_Org_Doc, LnkBtn_Delete_Org_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Unit_Type_Doc.ID))
            {
                if (FU_Unit_Type.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_UNITTYPEDOC";
                    UploadDocument(FU_Unit_Type, Hid_Unit_Type_File_Name, strFileName, Hyp_View_Unit_Type_Doc, Lbl_Msg_Unit_Type_Doc, LnkBtn_Delete_Unit_Type_Doc, "InctBasicDoc");
                }
            }
            //else if (string.Equals(lnk.ID, LnkBtn_Upload_Prod_Comm_Before_Doc.ID))
            //{
            //    if (FU_Prod_Comm_Before.HasFile)
            //    {
            //        string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PRODCOMMBFDOC" ;
            //        UploadDocument(FU_Prod_Comm_Before, Hid_Prod_Comm_Before_File_Name, strFileName, Hyp_View_Prod_Comm_Before_Doc, Lbl_Msg_Prod_Comm_Before_Doc, LnkBtn_Delete_Prod_Comm_Before_Doc, "InctBasicDoc");
            //    }
            //}
            //else if (string.Equals(lnk.ID, LnkBtn_Upload_Prod_Comm_After_Doc.ID))
            //{
            //    if (FU_Prod_Comm_After.HasFile)
            //    {
            //        string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PRODCOMMAFDOC" ;
            //        UploadDocument(FU_Prod_Comm_After, Hid_Prod_Comm_After_File_Name, strFileName, Hyp_View_Prod_Comm_After_Doc, Lbl_Msg_Prod_Comm_After_Doc, LnkBtn_Delete_Prod_Comm_After_Doc, "InctBasicDoc");
            //    }
            //}
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Direct_Emp_Before_Doc.ID))
            {
                if (FU_Direct_Emp_Before.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_DIRECTEMPBEDOC";
                    UploadDocument(FU_Direct_Emp_Before, Hid_Direct_Emp_Before_File_Name, strFileName, Hyp_View_Direct_Emp_Before_Doc, Lbl_Msg_Direct_Emp_Before_Doc, LnkBtn_Delete_Direct_Emp_Before_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Direct_Emp_After_Doc.ID))
            {
                if (FU_Direct_Emp_After.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_DIRECTEMPAFDOC";
                    UploadDocument(FU_Direct_Emp_After, Hid_Direct_Emp_After_File_Name, strFileName, Hyp_View_Direct_Emp_After_Doc, Lbl_Msg_Direct_Emp_After_Doc, LnkBtn_Delete_Direct_Emp_After_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_FFCI_Before_Doc.ID))
            {
                if (FU_FFCI_Before.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FFCIBEDOC";
                    UploadDocument(FU_FFCI_Before, Hid_FFCI_Before_File_Name, strFileName, Hyp_View_FFCI_Before_Doc, Lbl_Msg_FFCI_Before_Doc, LnkBtn_Delete_FFCI_Before_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_FFCI_After_Doc.ID))
            {
                if (FU_FFCI_After.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FFCIAFDOC";
                    UploadDocument(FU_FFCI_After, Hid_FFCI_After_File_Name, strFileName, Hyp_View_FFCI_After_Doc, Lbl_Msg_FFCI_After_Doc, LnkBtn_Delete_FFCI_After_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Approved_DPR_Before_Doc.ID))
            {
                if (FU_Approved_DPR_Before.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_APPDPRBEDOC";
                    UploadDocument(FU_Approved_DPR_Before, Hid_Approved_DPR_Before_File_Name, strFileName, Hyp_View_Approved_DPR_Before_Doc, Lbl_Msg_Approved_DPR_Before_Doc, LnkBtn_Delete_Approved_DPR_Before_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Approved_DPR_After_Doc.ID))
            {
                if (FU_Approved_DPR_After.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_APPDPRAFDOC";
                    UploadDocument(FU_Approved_DPR_After, Hid_Approved_DPR_After_File_Name, strFileName, Hyp_View_Approved_DPR_After_Doc, Lbl_Msg_Approved_DPR_After_Doc, LnkBtn_Delete_Approved_DPR_After_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Term_Loan_Doc.ID))
            {
                if (FU_Term_Loan.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_TLDOC";
                    UploadDocument(FU_Term_Loan, Hid_Term_Loan_File_Name, strFileName, Hyp_View_Term_Loan_Doc, Lbl_Msg_Term_Loan_Doc, LnkBtn_Delete_Term_Loan_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Pioneer_Doc.ID))
            {
                if (FU_Pioneer_Doc.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PIONEER";
                    UploadDocument(FU_Pioneer_Doc, Hid_Pioneer_Doc_File_Name, strFileName, Hyp_View_Pioneer_Doc, Lbl_Msg_Pioneer_Doc, LnkBtn_Delete_Pioneer_Doc, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Ancillary_Doc.ID))
            {
                if (FU_Ancillary_Doc.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ANCI";
                    UploadDocument(FU_Ancillary_Doc, Hid_Ancillary_Doc_File_Name, strFileName, Hyp_View_Ancillary_Doc, Lbl_Msg_Ancillary_Doc, LnkBtn_Delete_Ancillary_Doc, "InctBasicDoc");
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkBtn_Delete_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkBtn_Delete_Org_Doc.ID))
            {
                UpdFileRemove(Hid_Org_Doc_File_Name, LnkBtn_Upload_Org_Doc, LnkBtn_Delete_Org_Doc, Hyp_View_Org_Doc, Lbl_Msg_Org_Doc, FU_Org_Doc, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Unit_Type_Doc.ID))
            {
                UpdFileRemove(Hid_Unit_Type_File_Name, LnkBtn_Upload_Unit_Type_Doc, LnkBtn_Delete_Unit_Type_Doc, Hyp_View_Unit_Type_Doc, Lbl_Msg_Unit_Type_Doc, FU_Unit_Type, "InctBasicDoc");
            }
            //else if (string.Equals(lnk.ID, LnkBtn_Delete_Prod_Comm_Before_Doc.ID))
            //{
            //    UpdFileRemove(Hid_Prod_Comm_Before_File_Name, LnkBtn_Upload_Prod_Comm_Before_Doc, LnkBtn_Delete_Prod_Comm_Before_Doc, Hyp_View_Prod_Comm_Before_Doc, Lbl_Msg_Prod_Comm_Before_Doc, FU_Prod_Comm_Before, "InctBasicDoc");
            //}
            //else if (string.Equals(lnk.ID, LnkBtn_Delete_Prod_Comm_After_Doc.ID))
            //{
            //    UpdFileRemove(Hid_Prod_Comm_After_File_Name, LnkBtn_Upload_Prod_Comm_After_Doc, LnkBtn_Delete_Prod_Comm_After_Doc, Hyp_View_Prod_Comm_After_Doc, Lbl_Msg_Prod_Comm_After_Doc, FU_Prod_Comm_After, "InctBasicDoc");
            //}
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Direct_Emp_Before_Doc.ID))
            {
                UpdFileRemove(Hid_Direct_Emp_Before_File_Name, LnkBtn_Upload_Direct_Emp_Before_Doc, LnkBtn_Delete_Direct_Emp_Before_Doc, Hyp_View_Direct_Emp_Before_Doc, Lbl_Msg_Direct_Emp_Before_Doc, FU_Direct_Emp_Before, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Direct_Emp_After_Doc.ID))
            {
                UpdFileRemove(Hid_Direct_Emp_After_File_Name, LnkBtn_Upload_Direct_Emp_After_Doc, LnkBtn_Delete_Direct_Emp_After_Doc, Hyp_View_Direct_Emp_After_Doc, Lbl_Msg_Direct_Emp_After_Doc, FU_Direct_Emp_After, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_FFCI_Before_Doc.ID))
            {
                UpdFileRemove(Hid_FFCI_Before_File_Name, LnkBtn_Upload_FFCI_Before_Doc, LnkBtn_Delete_FFCI_Before_Doc, Hyp_View_FFCI_Before_Doc, Lbl_Msg_FFCI_Before_Doc, FU_FFCI_Before, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_FFCI_After_Doc.ID))
            {
                UpdFileRemove(Hid_FFCI_After_File_Name, LnkBtn_Upload_FFCI_After_Doc, LnkBtn_Delete_FFCI_After_Doc, Hyp_View_FFCI_After_Doc, Lbl_Msg_FFCI_After_Doc, FU_FFCI_After, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Approved_DPR_Before_Doc.ID))
            {
                UpdFileRemove(Hid_Approved_DPR_Before_File_Name, LnkBtn_Upload_Approved_DPR_Before_Doc, LnkBtn_Delete_Approved_DPR_Before_Doc, Hyp_View_Approved_DPR_Before_Doc, Lbl_Msg_Approved_DPR_Before_Doc, FU_Approved_DPR_Before, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Approved_DPR_After_Doc.ID))
            {
                UpdFileRemove(Hid_Approved_DPR_After_File_Name, LnkBtn_Upload_Approved_DPR_After_Doc, LnkBtn_Delete_Approved_DPR_After_Doc, Hyp_View_Approved_DPR_After_Doc, Lbl_Msg_Approved_DPR_After_Doc, FU_Approved_DPR_After, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Term_Loan_Doc.ID))
            {
                UpdFileRemove(Hid_Term_Loan_File_Name, LnkBtn_Upload_Term_Loan_Doc, LnkBtn_Delete_Term_Loan_Doc, Hyp_View_Term_Loan_Doc, Lbl_Msg_Term_Loan_Doc, FU_Term_Loan, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Pioneer_Doc.ID))
            {
                UpdFileRemove(Hid_Pioneer_Doc_File_Name, LnkBtn_Upload_Pioneer_Doc, LnkBtn_Delete_Pioneer_Doc, Hyp_View_Pioneer_Doc, Lbl_Msg_Pioneer_Doc, FU_Pioneer_Doc, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Ancillary_Doc.ID))
            {
                UpdFileRemove(Hid_Ancillary_Doc_File_Name, LnkBtn_Upload_Ancillary_Doc, LnkBtn_Delete_Ancillary_Doc, Hyp_View_Ancillary_Doc, Lbl_Msg_Ancillary_Doc, FU_Ancillary_Doc, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string strFoldername)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFoldername));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fuOrgDocument.HasFile)
            {
                string filename = string.Empty;
                if (Path.GetExtension(fuOrgDocument.FileName) != ".pdf" && Path.GetExtension(fuOrgDocument.FileName) != ".zip")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload either PDF or ZIP file !!')", true);
                    return;
                }
                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuOrgDocument.FileName);
                    //filename = strFileName;
                }
                fuOrgDocument.SaveAs(strMainFolderPath + filename);
                hdnOrgDocument.Value = filename;
                hypOrdDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFoldername, filename);
                hypOrdDocument.Visible = true;
                lnkOrgDocumentDelete.Visible = true;
                lblOrgDocument.Visible = true;
                fuOrgDocument.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolername)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
            string completePath = Server.MapPath(path);
            if (File.Exists(completePath))
            {
                File.Delete(completePath);
            }

            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion

    //// DropDownList/RadioButtonList SelectedIndexChanged Section
    #region SelectedIndexChanged

    protected void DrpDwn_Org_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = (DataSet)ViewState["dynamic_name_doc"];

            ds.Tables[0].DefaultView.RowFilter = "slno = '" + DrpDwn_Org_Type.SelectedValue + "'";
            DataTable dt = (ds.Tables[0].DefaultView).ToTable();

            if (dt.Rows.Count > 0)
            {
                Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                Lbl_Org_Doc_Type.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                Hid_Org_Doc_Type.Value = dt.Rows[0]["vchDocumentType"].ToString();
            }
            else
            {
                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                Hid_Org_Doc_Type.Value = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void DrpDwn_Unit_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = (DataSet)ViewState["dynamic_name_doc"];

            ds.Tables[1].DefaultView.RowFilter = "slno = '" + DrpDwn_Unit_Type.SelectedValue + "'";
            DataTable dt = (ds.Tables[1].DefaultView).ToTable();

            if (dt.Rows.Count > 0)
            {
                string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                if (strDocType != "")
                {
                    Div_Unit_Type_Doc.Visible = true;
                    Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();
                }
                else
                {
                    Div_Unit_Type_Doc.Visible = false;
                    Lbl_Unit_Type_Doc_Name.Text = "";
                    Hid_Unit_Type_Doc_Code.Value = "";
                }
            }
            else
            {
                Div_Unit_Type_Doc.Visible = false;
                Lbl_Unit_Type_Doc_Name.Text = "";
                Hid_Unit_Type_Doc_Code.Value = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void DrpDwn_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubSectorFiltered();
    }
    protected void DrpDwn_Sub_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        IncentiveMaster objEntity = new IncentiveMaster();

        try
        {
            objEntity.Action = "Q";
            objEntity.Param_2 = DrpDwn_Sector.SelectedValue;
            objEntity.Param_3 = DrpDwn_Sub_Sector.SelectedValue;

            DataSet ds = new DataSet();
            ds = objBAL.BindDerivedSector(objEntity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string strDesc = Convert.ToString(ds.Tables[0].Rows[0]["vchDesc"]);
                string strSectoralPolicy = Convert.ToString(ds.Tables[0].Rows[0]["bitSectoralPolicy"]);

                if (strDesc != "")
                {
                    Lbl_Derived_Sector.Text = strDesc;
                }
                else
                {
                    Lbl_Derived_Sector.Text = "NA";
                }

                if (strSectoralPolicy == "True")
                {
                    ChkBx_Sectoral.Checked = true;
                }
                else
                {
                    ChkBx_Sectoral.Checked = false;
                }
            }
            else
            {
                Lbl_Derived_Sector.Text = "NA";
                ChkBx_Sectoral.Checked = false;
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
    protected void Rad_Is_Priority_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rad_Is_Priority.SelectedValue == "1")
            {
                Div_Pioneer.Visible = true;
            }
            else
            {
                Div_Pioneer.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void DrpDwn_Unit_Before_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpDwn_Unit_Before.SelectedIndex > 0)
            {
                if (DrpDwn_Unit_Before.SelectedValue == "52")
                {
                    Txt_Other_Unit_Before.Visible = true;
                }
                else
                {
                    Txt_Other_Unit_Before.Text = "";
                    Txt_Other_Unit_Before.Visible = false;
                }
            }
            else
            {
                Txt_Other_Unit_Before.Text = "";
                Txt_Other_Unit_Before.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void DrpDwn_Unit_After_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpDwn_Unit_After.SelectedIndex > 0)
            {
                if (DrpDwn_Unit_After.SelectedValue == "52")
                {
                    Txt_Other_Unit_After.Visible = true;
                }
                else
                {
                    Txt_Other_Unit_After.Text = "";
                    Txt_Other_Unit_After.Visible = false;
                }
            }
            else
            {
                Txt_Other_Unit_After.Text = "";
                Txt_Other_Unit_After.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void Rad_Is_Ancillary_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rad_Is_Ancillary.SelectedValue == "1")
            {
                Div_Ancillary.Visible = true;
            }
            else
            {
                Div_Ancillary.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void Rad_Priority_User_SelectedIndexChanged(object sender, EventArgs e)
    {
        displayPriorityMsg();
    }

    #endregion

    //// View Production Certificate (Before)
    protected void LnkBtn_View_Prod_Comm_Before_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            if (Hid_Project_Type.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "New_Window", "window.open('../Portal/Incentive/PCPrintLarge.aspx?id=" + Hid_PC_App_No_Before.Value + "', null);", true);
            }
            else if (Hid_Project_Type.Value == "2")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "New_Window", "window.open('../Portal/Incentive/PcPrint.aspx?id=" + Hid_PC_App_No_Before.Value + "', null );", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    //// View Production Certificate (After)
    protected void LnkBtn_View_Prod_Comm_After_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            if (Hid_Project_Type.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "New_Window", "window.open('../Portal/Incentive/PCPrintLarge.aspx?id=" + Hid_PC_App_No_After.Value + "', null);", true);
            }
            else if (Hid_Project_Type.Value == "2")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "New_Window", "window.open('../Portal/Incentive/PcPrint.aspx?id=" + Hid_PC_App_No_After.Value + "', null );", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
}