#region  Page Info
//******************************************************************************************************************
// File Name             :   PromoterDetails.aspx.cs
// Description           :   Manage Company Information Details of Promoter
// Created by            :   Subhasmita Behera
// Created On            :   18-Aug-2017
//  "VERION= v2"
// Modification History  :
//                          <CR no.>        <Date>               <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   1             27-Aug-2019           Sushant Jena                    Group of Company Net Worth Details Added                
//******************************************************************************************************************
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ServiceModel;
using BusinessLogicLayer.Proposal;
using System.IO;
using EntityLayer.Proposal;
using System.Collections.Specialized;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;


#endregion

public partial class Promoter_Details : SessionCheck
{
    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 for Declaration of Variables
    /// </summary>
    #region Declaration And Variables

    ProposalBAL objService = new ProposalBAL();
    PromoterDet objProposal = new PromoterDet();
    CommonValidation objcmv = new CommonValidation();

    string strRetval = "";
    string updSts = "0";
    DataTable objdt;
    string FilePath = "";
    string FilePathGSTN = "";
    string FilePathMemo = "";
    string FilePathCerti = "";
    string FilePathEdu = "";
    string FilePathTech = "";
    string FilePathexperi = "";
    string FilePathAudit = "";
    string FilePathNetWorth = "";
    string FilePathAuditsec = "";
    string FilePathAuditThird = "";
    string FilePathAuditFourth = "";
    string strCounterValue1 = "250";
    string allval = "";
    string Constitution = "";
    string allFileVal = "";

    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 for page load events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Guid id = Guid.NewGuid();
                st2.Visible = false;
                st4.Visible = false;

                CreateDataTable();
                CreateDataTableBD();
                CreateDataTableRWM();

                DelBtnHideshow();
                BindCountry();
                ddlCountry.SelectedValue = "1";
                drpCorCountry.SelectedValue = "1";
                int countryid = Convert.ToInt32(ddlCountry.SelectedValue);
                BindState(countryid);
                BindCountry2();
                int countryid2 = Convert.ToInt32(drpCorCountry.SelectedValue);
                BindState2(countryid2);
                BindDistrict();
                BindSector();
                BindUnits();
                BindQualification();
                BindCountryCode();
                BindCountryCodeFx();
                BindYearofEstablishment();
                BindCountryCodeMB();
                BindCountryCodeFxcor();

                ddlCode.SelectedValue = "1";
                drpFx.SelectedValue = "1";
                drpMob.SelectedValue = "1";
                drpFax2.SelectedValue = "1";

                int year = DateTime.Now.Year - 1, count = 1;
                for (int i = year; i < year + 2; i++)
                {
                    ListItem list = new ListItem();
                    list.Text = (i - 1).ToString() + "-" + i.ToString();
                    list.Value = Convert.ToString((i - 1).ToString());
                    drpFinYear1.Items.Insert(0, list);
                    count++;
                }
                ListItem list1 = new ListItem();
                list1.Text = "--Select--";
                list1.Value = "0";
                drpFinYear1.Items.Insert(0, list1);

                drpFinYear2.Enabled = false;
                drpFinYear3.Enabled = false;

                /*-------------------------------------------------------------------------------------------------*/

                if ((!string.IsNullOrEmpty(Request.QueryString["StrPropNo"])) || (!string.IsNullOrEmpty(Session["proposalno"] as string)))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
                    {
                        GetCompanyInfo(Request.QueryString["StrPropNo"]);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
                        {
                            GetCompanyInfo(Session["proposalno"].ToString());
                        }
                    }
                }
                else
                {
                    //Session["NswsInvSwsId"] = "SW6050184036";
                    if (Session["NswsInvSwsId"] != null && Convert.ToString(Session["NswsInvSwsId"]) != "")
                    {
                        ///// Pull State CAF from NSWS portal and Populate at respective places.
                        string strInvSwsId = Convert.ToString(Session["NswsInvSwsId"]);
                        PullStateCafNsws(strInvSwsId);
                        
                    }
                }

                //allFileVal = hdnAllFileValue.Value;
                //allFileVal = allFileVal  + ',';
                //hdnAllFileValue.Value = allFileVal;
                //string script = "$(document).ready(function () { $('[id*=btnNext]').click(); });";
                //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 to get company info details
    /// </summary>
    /// <returns></returns>
    #region Get Company details

    private void GetCompanyInfo(string proposalno)
    {
        try
        {
            List<PromoterDet> objProposalList = new List<PromoterDet>();
            PromoterDet objProp = new PromoterDet();

            /*--------------------------------------------------------------------*/

            objProp.strAction = "F";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }
            objProposalList = objService.GetNameDesgDetails(objProp).ToList();
            GrdBD.DataSource = objProposalList;
            GrdBD.DataBind();
            DataTable dt1 = CreateDataTableBD();
            for (int i = 0; i <= GrdBD.Rows.Count - 1; i++)
            {
                HiddenField hdpid1 = (HiddenField)GrdBD.Rows[i].FindControl("hdpid1");
                DataRow dr = dt1.NewRow();
                dr["intProId1"] = (dt1.Rows.Count + 1).ToString();//hdpid1.Value;
                dr["vchName"] = GrdBD.Rows[i].Cells[1].Text;
                dr["vchDesignation"] = GrdBD.Rows[i].Cells[2].Text;
                dt1.Rows.Add(dr);
                dt1.AcceptChanges();
            }
            ViewState["BoardOfDirectors"] = dt1;
            drpTagTo.Items.Clear();
            drpTagTo.DataSource = dt1;
            drpTagTo.DataTextField = "vchName";
            drpTagTo.DataValueField = "intProId1";
            drpTagTo.DataBind();
            drpTagTo.Items.Insert(0, new ListItem("--Select--", "0"));

            /*--------------------------------------------------------------------*/

            objProp.strAction = "V";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }
            objProposalList = objService.GetCompanyDetails(objProp).ToList();
            if (objProposalList.Count > 0)
            {
                txtIName.Text = Convert.ToString(Server.HtmlDecode(objProposalList[0].vchCompName.ToString()));
                txtAddress.Text = Convert.ToString(Server.HtmlDecode(objProposalList[0].vchAddress.ToString()));
                ddlCountry.SelectedValue = Convert.ToString(objProposalList[0].intCountry.ToString());
                int cntid = Convert.ToInt32(ddlCountry.SelectedValue);
                BindState(cntid);
                if (Convert.ToInt32(ddlCountry.SelectedValue.TrimEnd()) == 1)
                {
                    ddlState.SelectedValue = Convert.ToString(objProposalList[0].intState.ToString());
                    txtState.Text = "";
                    st1.Visible = true;
                    st2.Visible = false;
                }
                else
                {
                    txtState.Text = Convert.ToString(objProposalList[0].vchOtherState.ToString());
                    ddlState.SelectedValue = "0";
                    st1.Visible = false;
                    st2.Visible = true;
                }

                txtCity.Text = Convert.ToString(objProposalList[0].vchCity.ToString());
                if (objProposalList[0].intPin.ToString() == "0")
                {
                    txtPinCode.Text = "";
                }
                else
                {
                    txtPinCode.Text = Convert.ToString(objProposalList[0].intPin.ToString());
                }

                txtPhoneNo.Text = Convert.ToString(objProposalList[0].vchPhoneNo.ToString());
                txtPhoneNoStateCodedet.Text = Convert.ToString(objProposalList[0].PhoneStateCode.ToString());
                txtFaxNo.Text = Convert.ToString(objProposalList[0].vchFaxNo.ToString());
                txtEmail.Text = Convert.ToString(objProposalList[0].vchEmail.ToString());

                if (Convert.ToString(objProposalList[0].bitAddresSameAsCorp.ToString()) == "1")
                {
                    chkBoxAddress.Checked = true;
                }
                else
                {
                    chkBoxAddress.Checked = false;
                }

                txtCperson.Text = Convert.ToString(objProposalList[0].vchContactPerson.ToString());
                txtCorAddrs.Text = Convert.ToString(Server.HtmlDecode(objProposalList[0].vchCorAdd.ToString()));
                drpCorCountry.SelectedValue = Convert.ToString(objProposalList[0].intCorCountry.ToString());
                int cntid2 = Convert.ToInt32(drpCorCountry.SelectedValue);
                BindState2(cntid2);
                if (Convert.ToInt32(drpCorCountry.SelectedValue.TrimEnd()) == 1)
                {
                    drpCorState.SelectedValue = Convert.ToString(objProposalList[0].intCorState.ToString());
                    txtCorState.Text = "";
                    st3.Visible = true;
                    st4.Visible = false;
                }
                else
                {
                    txtCorState.Text = Convert.ToString(objProposalList[0].vchOtherStateCor.ToString());
                    drpCorState.SelectedValue = "0";
                    st3.Visible = false;
                    st4.Visible = true;
                }

                txtCorCity.Text = Convert.ToString(objProposalList[0].vchCorCity.ToString());
                if (objProposalList[0].intCorPin.ToString() == "0")
                {
                    txtCorPin.Text = "";
                }
                else
                {
                    txtCorPin.Text = Convert.ToString(objProposalList[0].intCorPin.ToString());
                }

                txtCorMob.Text = Convert.ToString(objProposalList[0].vchCorMobileNo.ToString());
                txtCorFax.Text = Convert.ToString(objProposalList[0].vchCorFaxNo.ToString());
                txtCorEmailid.Text = Convert.ToString(objProposalList[0].vchCorEmail.ToString());
                ddlConstitution.SelectedValue = Convert.ToString(objProposalList[0].intConstitution.ToString());
                txtOthrConsti.Text = Convert.ToString(objProposalList[0].vchOtheConstituition.ToString());
                //txtyrIncorporation.Text = Convert.ToString(objProposalList[0].intYearOfIncorporation.ToString());
                //BindYearofEstablishment();
                drpYearofEstablishment.SelectedValue = Convert.ToString(objProposalList[0].intYearOfIncorporation.ToString());

                txtPlaceIncorporation.Text = Convert.ToString(objProposalList[0].vchPlaceIncor.ToString());
                txtGSTIN.Text = Convert.ToString(objProposalList[0].vchGSTIN.ToString());
                drpProjectCat.SelectedValue = Convert.ToString(objProposalList[0].intProjectType.ToString());
                drpApplicationFor.SelectedValue = Convert.ToString(objProposalList[0].intApplicationFor.ToString());
                txtNoOfParter.Text = Convert.ToString(objProposalList[0].intNumberOfPartner.ToString());
                txtNameOfMP.Text = Convert.ToString(objProposalList[0].vchManagPartner.ToString());
                txtAnnual1.Text = Convert.ToString(objProposalList[0].decAnnulTurnOvr1.ToString());
                txtAnnual2.Text = Convert.ToString(objProposalList[0].decAnnulTurnOvr2.ToString());
                txtAnnual3.Text = Convert.ToString(objProposalList[0].decAnnulTurnOvr3.ToString());
                txtProfit1.Text = Convert.ToString(objProposalList[0].decProfitAftrTx1.ToString());
                txtProfit2.Text = Convert.ToString(objProposalList[0].decProfitAftrTx2.ToString());
                txtProfit3.Text = Convert.ToString(objProposalList[0].decProfitAftrTx3.ToString());
                txtNtWorth1.Text = Convert.ToString(objProposalList[0].decNetWorth1.ToString());
                txtNtWorth2.Text = Convert.ToString(objProposalList[0].decNetWorth2.ToString());
                txtNtWorth3.Text = Convert.ToString(objProposalList[0].decNetWorth3.ToString());
                txtReserve1.Text = Convert.ToString(objProposalList[0].decResvSurp1.ToString());
                txtReserve2.Text = Convert.ToString(objProposalList[0].decResvSurp2.ToString());
                txtReserve3.Text = Convert.ToString(objProposalList[0].decResvSurp3.ToString());
                txtShare1.Text = Convert.ToString(objProposalList[0].decShareCap1.ToString());
                txtShare2.Text = Convert.ToString(objProposalList[0].decShareCap2.ToString());
                txtShare3.Text = Convert.ToString(objProposalList[0].decShareCap3.ToString());

                drpEducation.SelectedValue = Convert.ToString(objProposalList[0].intEduQualif.ToString());
                BindtechQualifi(drpEducation.SelectedValue);

                drpTechnical.SelectedValue = Convert.ToString(objProposalList[0].intTecQualif.ToString());
                txtexpinYr.Text = Convert.ToString(objProposalList[0].intExpInYr.ToString());
                txtExtIndName.Text = Convert.ToString(Server.HtmlDecode(objProposalList[0].vchExisIndName.ToString()));

                ddlDistrict.SelectedValue = Convert.ToString(objProposalList[0].intExisDistrict.ToString());
                if (ddlDistrict.SelectedValue != "0")
                {
                    BindBlock(ddlDistrict.SelectedValue);
                }

                drpBlock.SelectedValue = Convert.ToString(objProposalList[0].intExisBlock.ToString());
                ddlAlloted.SelectedValue = Convert.ToString(objProposalList[0].intAllotedBy.ToString());
                txtExtentLand.Text = Convert.ToString(objProposalList[0].vchlandInAcres.ToString());
                txtNatureAct.Text = Convert.ToString(objProposalList[0].vchNatureAct.ToString());
                ddlsector.SelectedValue = Convert.ToString(objProposalList[0].intSectorId.ToString());
                if (ddlsector.SelectedValue != "0")
                {
                    BindSubSector(ddlsector.SelectedValue);
                }

                ddlSubSec.SelectedValue = Convert.ToString(objProposalList[0].intSubSectorId.ToString());
                if (objProposalList[0].vchCapacity.ToString() == "")
                {
                    txtCapacity.Text = "0";
                }
                else
                {
                    txtCapacity.Text = Convert.ToString(objProposalList[0].vchCapacity.ToString());
                }

                drpCp.SelectedValue = Convert.ToString(objProposalList[0].intCapacityUnit.ToString());
                txtCapOthr.Text = Convert.ToString(objProposalList[0].vchOther.ToString());
                drpFinYear1.SelectedValue = Convert.ToString(objProposalList[0].intFyn1.ToString());
                drpFinYear2.SelectedValue = Convert.ToString(objProposalList[0].intFyn2.ToString());
                drpFinYear3.SelectedValue = Convert.ToString(objProposalList[0].intFyn3.ToString());
                txtNameOfPromoter.Text = Convert.ToString(objProposalList[0].vchNameOfPromoter.ToString());
                ddlCode.SelectedValue = Convert.ToString(objProposalList[0].intISDPHNo.ToString());
                drpFx.SelectedValue = Convert.ToString(objProposalList[0].intISDFXNo.ToString());
                drpMob.SelectedValue = Convert.ToString(objProposalList[0].intISDMOBo.ToString());
                drpFax2.SelectedValue = Convert.ToString(objProposalList[0].intFaxCordet.ToString());
                drpTagTo.SelectedValue = Convert.ToString(objProposalList[0].Tagtodet.ToString());
            }

            /*--------------------------------------------------------------------*/

            objProp.strAction = "G";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }

            objProposalList = objService.GetRawMetrialDetails(objProp).ToList();
            GrvRWM.DataSource = objProposalList;
            GrvRWM.DataBind();
            DataTable dt2 = CreateDataTableRWM();
            for (int i = 0; i <= GrvRWM.Rows.Count - 1; i++)
            {
                HiddenField hdpid2 = (HiddenField)GrvRWM.Rows[i].FindControl("hdpid2");
                DataRow dr = dt2.NewRow();
                dr["intProId2"] = hdpid2.Value;
                dr["vchRawMaterial"] = GrvRWM.Rows[i].Cells[1].Text;
                dr["vchRawMeterialSrc"] = GrvRWM.Rows[i].Cells[2].Text;
                dt2.Rows.Add(dr);
                dt2.AcceptChanges();
            }
            ViewState["RawMeterial"] = dt2;

            /*--------------------------------------------------------------------*/

            objProp.strAction = "H";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }
            objProposalList = objService.GetEnclosureDetails(objProp).ToList();
            if (objProposalList.Count > 0)
            {
                if (Convert.ToString(objProposalList[0].vchPanfile) != "")
                {
                    hdnPanFile.Value = Convert.ToString(objProposalList[0].vchPanfile);
                    hlDoc1.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchPanfile);
                    hlDoc1.Visible = true;
                    lnkDelPan.Visible = true;
                    lblPAN.Visible = true;
                    FileUpldPan.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchGSTNfile) != "")
                {
                    hdnGstinFile.Value = Convert.ToString(objProposalList[0].vchGSTNfile);
                    hlDoc2.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchGSTNfile);
                    hlDoc2.Visible = true;
                    lnkDelGST.Visible = true;
                    lblGSTIN.Visible = true;
                    FileUpldGstn.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchMemorandumfile) != "")
                {
                    hdnMemoFile.Value = Convert.ToString(objProposalList[0].vchMemorandumfile);
                    hlDoc3.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchMemorandumfile);
                    hlDoc3.Visible = true;
                    lnkDelMemo.Visible = true;
                    lblMemo.Visible = true;
                    FileUpldMemo.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchCertificateincorpfile) != "")
                {
                    hdnCerti.Value = Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                    hlDoc4.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                    hlDoc4.Visible = true;
                    lnkDelCerti.Visible = true;
                    lblCerti.Visible = true;
                    FileUpldCerti.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchEduQualifile) != "")
                {
                    hdnEdu.Value = Convert.ToString(objProposalList[0].vchEduQualifile);
                    hlDoc5.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchEduQualifile);
                    hlDoc5.Visible = true;
                    lnkDelEdu.Visible = true;
                    lblEdu.Visible = true;
                    FileUpldEducational.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchTechniQualifile) != "")
                {
                    hdnTecnical.Value = Convert.ToString(objProposalList[0].vchTechniQualifile);
                    hlDoc6.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchTechniQualifile);
                    hlDoc6.Visible = true;
                    lnkDelTechni.Visible = true;
                    lblTechni.Visible = true;
                    FileUpldTechnical.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchExpFile) != "")
                {
                    hdnExperience.Value = Convert.ToString(objProposalList[0].vchExpFile);
                    hlDoc7.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchExpFile);
                    hlDoc7.Visible = true;
                    lnkDelExperience.Visible = true;
                    lblExp.Visible = true;
                    FileUpldExperience.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchAuditFile) != "")
                {
                    hdnAudit.Value = Convert.ToString(objProposalList[0].vchAuditFile);
                    hlDoc8.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFile);
                    hlDoc8.Visible = true;
                    lnkDelAudit.Visible = true;
                    lblAudit1.Visible = true;
                    if (ddlConstitution.SelectedValue == "1")
                    {
                        lblAudit1.Text = "Networth Certificate of the Proprietor duly certified by CA for Current/latest year uploaded successfully";
                    }
                    else if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit1.Text = "Partnership deed uploaded successfully";
                    }
                    else
                    {
                        lblAudit1.Text = "Audited Financial Statements for First Year uploaded successfully";
                    }
                    FileUpldAudited.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchNetWorthfile) != "")
                {
                    hdnNetWorth.Value = Convert.ToString(objProposalList[0].vchNetWorthfile);
                    hlDoc9.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchNetWorthfile);
                    lnkDelnetWorth.Visible = true;
                    hlDoc9.Visible = true;
                    lblNet.Visible = true;
                    FileUpldNetWorth.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchAuditFileSecondYrs) != "")
                {
                    hdnFySecond.Value = Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);
                    hlDoc12.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);
                    lnkFySecondDelete.Visible = true;
                    hlDoc12.Visible = true;
                    lblAudit2.Visible = true;
                    if (ddlConstitution.SelectedValue == "1")
                    {
                        lblAudit2.Text = "Tax Audit Report(if applicable) for Current/latest year uploaded successfully";
                    }
                    else if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit2.Text = "Complete balance sheet of the firm(latest 3 years) uploaded successfully";
                    }
                    else
                    {
                        lblAudit2.Text = "Audited Financial Statements for Second Year uploaded successfully";
                    }
                    FileUploadSecond.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].vchAuditFileThrdYrs) != "")
                {
                    hdnFyThird.Value = Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);
                    hlDoc13.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);
                    lnkFyThirdDel.Visible = true;
                    hlDoc13.Visible = true;
                    lblAudit3.Visible = true;
                    if (ddlConstitution.SelectedValue == "1")
                    {
                        lblAudit3.Text = "Income tax return for Current/latest year uploaded successfully";
                    }
                    else if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit3.Text = "Tax audit report of the Partnership firm uploaded successfully";
                    }
                    else
                    {
                        lblAudit3.Text = "Audited Financial Statements for Third Year uploaded successfully";
                    }
                    FileUploadThird.Enabled = false;
                }
                if (Convert.ToString(objProposalList[0].strIncomeTaxReturn) != "")
                {
                    hdnFyFourth.Value = Convert.ToString(objProposalList[0].strIncomeTaxReturn);
                    hlDoc14.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strIncomeTaxReturn);
                    lnkFyFourDel.Visible = true;
                    hlDoc14.Visible = true;
                    lblAudit4.Visible = true;
                    if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit4.Text = "Income tax return uploaded successfully";
                    }
                    //FileUploadFourth.Enabled = false; //// Commented by Sushant Jena on Dt:-14-Aug-2019
                    FileUploadFourthupd.Enabled = false; //// Added by Sushant Jena on Dt:-14-Aug-2019
                }
            }

            ///*--------------------------------------------------------------------*/
            /////// For Group of Comapny Net Worth Details

            //objProp.strAction = "GCNW";
            //if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            //{
            //    objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            //}
            //else
            //{
            //    objProp.vchProposalNo = Session["proposalno"].ToString();
            //}

            //DataTable dtNW = new DataTable();
            //dtNW = objService.GetGcNewWorthDetails(objProp);
            //Grd_GC_Net_Worth.DataSource = dtNW;
            //Grd_GC_Net_Worth.DataBind();
            //ViewState["GCNetWorth"] = dtNW;

            ///*--------------------------------------------------------------------*/
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 to Create Data Tables
    /// </summary>
    /// <returns></returns>
    #region Create Data Tables
    private DataTable CreateDataTable()
    {
        DataTable dtTrans = new DataTable();
        DataColumn intProId = new DataColumn("intProId");
        intProId.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(intProId);
        DataColumn vchNameOfPromoter = new DataColumn("vchNameOfPromoter");
        vchNameOfPromoter.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchNameOfPromoter);
        ViewState["PromoterName"] = dtTrans;
        return dtTrans;
    }
    private DataTable CreateDataTableBD()
    {
        DataTable dtTrans = new DataTable();
        DataColumn intProId1 = new DataColumn("intProId1");
        intProId1.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(intProId1);
        DataColumn vchName = new DataColumn("vchName");
        vchName.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchName);
        DataColumn vchDesignation = new DataColumn("vchDesignation");
        vchDesignation.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchDesignation);
        ViewState["BoardOfDirectors"] = dtTrans;
        return dtTrans;
    }
    private DataTable CreateDataTableRWM()
    {
        DataTable dtTrans = new DataTable();
        DataColumn intProId2 = new DataColumn("intProId2");
        intProId2.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(intProId2);
        DataColumn vchRawMaterial = new DataColumn("vchRawMaterial");
        vchRawMaterial.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchRawMaterial);
        DataColumn vchRawMeterialSrc = new DataColumn("vchRawMeterialSrc");
        vchRawMeterialSrc.DataType = Type.GetType("System.String");
        dtTrans.Columns.Add(vchRawMeterialSrc);
        ViewState["RawMeterial"] = dtTrans;
        return dtTrans;
    }
    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 to Convert Data Tables to XML
    /// </summary>
    /// <param name="dtTable"></param>
    /// <returns></returns>
    #region Convert Data Tables to XML
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = "";
        if ((dtTable != null))
        {
            if (dtTable.Rows.Count > 0)
            {
                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw);
                strXMLResult = sw.ToString();
                sw.Close();
                sw.Dispose();
            }
        }
        return strXMLResult;
    }
    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 05-Aug-2017 to add/update Promoter Details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region ButtonClick
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            AddUpdatePromoterInfo();
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                Response.Redirect("proposeddetails.aspx?StrPropNo=" + Request.QueryString["StrPropNo"].ToString(), false);
            }
            else
            {
                Response.Redirect("proposeddetails.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        try
        {
            AddUpdatePromoterInfo();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Record(s) saved successfully.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void AddUpdatePromoterInfo()
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProposal.strAction = "U";
                objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else if (!string.IsNullOrEmpty(Session["proposalno"] as string))
            {
                objProposal.strAction = "U";
                objProposal.vchProposalNo = Session["proposalno"].ToString();
            }
            else
            {
                objProposal.strAction = "A";
            }

            /*--------------------------------------------------------------------------*/
            ///// Assign values to property.
            /*--------------------------------------------------------------------------*/
            objProposal.vchCompName = Convert.ToString(Server.HtmlEncode(txtIName.Text.TrimEnd()));
            objProposal.vchAddress = Convert.ToString(Server.HtmlEncode(txtAddress.Text.TrimEnd()));
            objProposal.intCountry = Convert.ToInt32(ddlCountry.SelectedValue.TrimEnd());

            if (Convert.ToInt32(ddlCountry.SelectedValue.TrimEnd()) == 1)
            {
                objProposal.intState = Convert.ToInt32(ddlState.SelectedValue.TrimEnd());
                objProposal.vchOtherState = "";
            }
            else
            {
                objProposal.vchOtherState = Convert.ToString(txtState.Text.TrimEnd());
                objProposal.intState = 0;
            }

            objProposal.vchCity = Convert.ToString(txtCity.Text.TrimEnd());
            if (txtPinCode.Text != "")
            {
                objProposal.intPin = Convert.ToInt32(txtPinCode.Text.TrimEnd());
            }
            else
            {
                objProposal.intPin = 0;
            }

            objProposal.vchPhoneNo = Convert.ToString(txtPhoneNo.Text.TrimEnd());
            objProposal.vchFaxNo = Convert.ToString(txtFaxNo.Text.TrimEnd());
            objProposal.vchEmail = Convert.ToString(txtEmail.Text.TrimEnd());

            if (chkBoxAddress.Checked == true)
            {
                objProposal.bitAddresSameAsCorp = 1;
            }
            else
            {
                objProposal.bitAddresSameAsCorp = 0;
            }

            objProposal.vchContactPerson = Convert.ToString(txtCperson.Text.TrimEnd());
            objProposal.vchCorAdd = Convert.ToString(Server.HtmlEncode(txtCorAddrs.Text.TrimEnd()));
            objProposal.intCorCountry = Convert.ToInt32(drpCorCountry.SelectedValue.TrimEnd());

            if (Convert.ToInt32(drpCorCountry.SelectedValue.TrimEnd()) == 1)
            {
                objProposal.intCorState = Convert.ToInt32(drpCorState.SelectedValue.TrimEnd());
                objProposal.vchOtherStateCor = "";
            }
            else
            {
                objProposal.vchOtherStateCor = Convert.ToString(txtState.Text.TrimEnd());
                objProposal.intCorState = 0;
            }

            objProposal.vchCorCity = Convert.ToString(txtCorCity.Text.TrimEnd());
            if (txtCorPin.Text != "")
            {
                objProposal.intCorPin = Convert.ToInt32(txtCorPin.Text.TrimEnd());
            }
            else
            {
                objProposal.intCorPin = 0;
            }

            objProposal.vchCorMobileNo = Convert.ToString(txtCorMob.Text.TrimEnd());
            objProposal.vchCorFaxNo = Convert.ToString(txtCorFax.Text.TrimEnd());
            objProposal.vchCorEmail = Convert.ToString(txtCorEmailid.Text.TrimEnd());
            objProposal.intConstitution = Convert.ToInt32(ddlConstitution.SelectedValue.TrimEnd());

            if (ddlConstitution.SelectedValue.TrimEnd() == "8")
            {
                objProposal.vchOtheConstituition = Convert.ToString(txtOthrConsti.Text.TrimEnd());
            }
            else
            {
                objProposal.vchOtheConstituition = "";
            }

            if (drpYearofEstablishment.SelectedValue != "0")
            {
                objProposal.intYearOfIncorporation = Convert.ToString(drpYearofEstablishment.SelectedValue);
            }
            else
            {
                objProposal.vchOtheConstituition = "";
                objProposal.intYearOfIncorporation = "0";
            }

            objProposal.vchPlaceIncor = Convert.ToString(txtPlaceIncorporation.Text.TrimEnd());
            objProposal.vchGSTIN = Convert.ToString(txtGSTIN.Text.TrimEnd());
            objProposal.intProjectType = Convert.ToInt32(drpProjectCat.SelectedValue.TrimEnd());
            objProposal.intApplicationFor = Convert.ToInt32(drpApplicationFor.SelectedValue.TrimEnd());

            if (txtAnnual1.Text != "")
            {
                objProposal.decAnnulTurnOvr1 = Convert.ToString(txtAnnual1.Text.TrimEnd());
            }
            else
            {
                objProposal.decAnnulTurnOvr1 = "0";
            }

            if (txtAnnual2.Text != "")
            {
                objProposal.decAnnulTurnOvr2 = Convert.ToString(txtAnnual2.Text.TrimEnd());
            }
            else
            {
                objProposal.decAnnulTurnOvr2 = "0";
            }

            if (txtAnnual3.Text != "")
            {
                objProposal.decAnnulTurnOvr3 = Convert.ToString(txtAnnual3.Text.TrimEnd());
            }
            else
            {
                objProposal.decAnnulTurnOvr3 = "0";
            }

            if (txtProfit1.Text != "")
            {
                objProposal.decProfitAftrTx1 = Convert.ToString(txtProfit1.Text.TrimEnd());
            }
            else
            {
                objProposal.decProfitAftrTx1 = "0";
            }

            if (txtProfit2.Text != "")
            {
                objProposal.decProfitAftrTx2 = Convert.ToString(txtProfit2.Text.TrimEnd());
            }
            else
            {
                objProposal.decProfitAftrTx2 = "0";
            }

            if (txtProfit3.Text != "")
            {
                objProposal.decProfitAftrTx3 = Convert.ToString(txtProfit3.Text.TrimEnd());
            }
            else
            {
                objProposal.decProfitAftrTx3 = "0";
            }

            if (txtNtWorth1.Text != "")
            {
                objProposal.decNetWorth1 = Convert.ToString(Request["txtNtWorth1"]);
            }
            else
            {
                objProposal.decNetWorth1 = "0";
            }

            if (txtNtWorth2.Text != "")
            {
                objProposal.decNetWorth2 = Convert.ToString(Request["txtNtWorth2"]);
            }
            else
            {
                objProposal.decNetWorth2 = "0";
            }

            if (txtNtWorth3.Text != "")
            {
                objProposal.decNetWorth3 = Convert.ToString(Request["txtNtWorth3"]);
            }
            else
            {
                objProposal.decNetWorth3 = "0";
            }

            if (txtReserve1.Text != "")
            {
                objProposal.decResvSurp1 = Convert.ToString(txtReserve1.Text.TrimEnd());
            }
            else
            {
                objProposal.decResvSurp1 = "0";
            }

            if (txtReserve2.Text != "")
            {
                objProposal.decResvSurp2 = Convert.ToString(txtReserve2.Text.TrimEnd());
            }
            else
            {
                objProposal.decResvSurp2 = "0";
            }

            if (txtReserve3.Text != "")
            {
                objProposal.decResvSurp3 = Convert.ToString(txtReserve3.Text.TrimEnd());
            }
            else
            {
                objProposal.decResvSurp3 = "0";
            }

            if (txtShare1.Text != "")
            {
                objProposal.decShareCap1 = Convert.ToString(txtShare1.Text.TrimEnd());
            }
            else
            {
                objProposal.decShareCap1 = "0";
            }

            if (txtShare2.Text != "")
            {
                objProposal.decShareCap2 = Convert.ToString(txtShare2.Text.TrimEnd());
            }
            else
            {
                objProposal.decShareCap2 = "0";
            }

            if (txtShare3.Text != "")
            {
                objProposal.decShareCap3 = Convert.ToString(txtShare3.Text.TrimEnd());
            }
            else
            {
                objProposal.decShareCap3 = "0";
            }

            if (drpEducation.SelectedValue == "")
            {
                objProposal.intEduQualif = 0;
            }
            else
            {
                objProposal.intEduQualif = Convert.ToInt32(drpEducation.SelectedValue.TrimEnd());
            }

            if (drpTechnical.SelectedValue == "")
            {
                objProposal.intTecQualif = 0;
            }
            else
            {
                objProposal.intTecQualif = Convert.ToInt32(drpTechnical.SelectedValue.TrimEnd());
            }

            if (txtexpinYr.Text == "")
            {
                objProposal.intExpInYr = 0;
            }
            else
            {
                objProposal.intExpInYr = Convert.ToInt32(txtexpinYr.Text.TrimEnd());
            }

            objProposal.vchExisIndName = Convert.ToString(Server.HtmlEncode(txtExtIndName.Text.TrimEnd()));
            objProposal.intExisDistrict = Convert.ToInt32(ddlDistrict.SelectedValue.TrimEnd());
            objProposal.intExisBlock = Convert.ToInt32(drpBlock.SelectedValue.TrimEnd());
            objProposal.intAllotedBy = Convert.ToInt32(ddlAlloted.SelectedValue.TrimEnd());
            objProposal.vchlandInAcres = Convert.ToString(txtExtentLand.Text.TrimEnd());
            objProposal.vchNatureAct = Convert.ToString(txtNatureAct.Text.TrimEnd());
            objProposal.intSectorId = Convert.ToInt32(ddlsector.SelectedValue.TrimEnd());
            objProposal.intSubSectorId = Convert.ToInt32(ddlSubSec.SelectedValue.TrimEnd());

            if (txtCapacity.Text.TrimEnd() == "")
            {
                txtCapacity.Text = "0";
            }
            else
            {
                objProposal.vchCapacity = Convert.ToString(txtCapacity.Text.TrimEnd());
            }

            objProposal.intCapacityUnit = Convert.ToInt32(drpCp.SelectedValue.TrimEnd());
            objProposal.vchOther = Convert.ToString(txtCapOthr.Text.TrimEnd());
            objProposal.intFyn1 = Convert.ToInt32(drpFinYear1.SelectedValue.TrimEnd());
            objProposal.intFyn2 = Convert.ToInt32(drpFinYear2.SelectedValue.TrimEnd());
            objProposal.intFyn3 = Convert.ToInt32(drpFinYear3.SelectedValue.TrimEnd());
            objProposal.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProposal.intUpdatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProposal.intPromoterId = Convert.ToInt32(Session["InvestorId"]);

            if (ddlConstitution.SelectedValue == "1")
            {
                objProposal.vchNameOfPromoter = Convert.ToString(txtNameOfPromoter.Text.TrimEnd());
            }
            else
            {
                objProposal.vchNameOfPromoter = "";
            }

            if (ddlConstitution.SelectedValue == "2")
            {
                if (txtNoOfParter.Text != "")
                {
                    objProposal.intNumberOfPartner = Convert.ToInt32(txtNoOfParter.Text.TrimEnd());
                }
                else
                {
                    objProposal.intNumberOfPartner = 0;
                }
                objProposal.vchManagPartner = Convert.ToString(txtNameOfMP.Text.TrimEnd());
            }
            else
            {
                objProposal.intNumberOfPartner = 0;
                objProposal.vchManagPartner = "";
            }

            objProposal.intCordist = 0;
            objProposal.strXML_Data = "";

            if ((Convert.ToInt32(ddlConstitution.SelectedValue) != 1) && (Convert.ToInt32(ddlConstitution.SelectedValue) != 2))
            {
                objdt = (DataTable)ViewState["BoardOfDirectors"];
                objdt.TableName = "BoardOfDirectors";
                objProposal.strXML_BD_Data = GetSTRXMLResult(objdt);
            }
            else
            {
                objProposal.strXML_BD_Data = "";
            }

            objdt = (DataTable)ViewState["RawMeterial"];
            objdt.TableName = "RawMeterial";
            objProposal.strXML_RWM_Data = GetSTRXMLResult(objdt);

            objProposal.vchPanfile = hdnPanFile.Value;
            objProposal.vchGSTNfile = hdnGstinFile.Value;
            objProposal.vchMemorandumfile = hdnMemoFile.Value;
            objProposal.vchCertificateincorpfile = hdnCerti.Value;
            objProposal.vchEduQualifile = hdnEdu.Value;
            objProposal.vchTechniQualifile = hdnTecnical.Value;
            objProposal.vchExpFile = hdnExperience.Value;
            objProposal.vchAuditFile = hdnAudit.Value;
            objProposal.vchNetWorthfile = hdnNetWorth.Value;
            objProposal.vchAuditFileSecondYrs = hdnFySecond.Value;
            objProposal.vchAuditFileThrdYrs = hdnFyThird.Value;
            objProposal.strIncomeTaxReturn = hdnFyFourth.Value;
            objProposal.intISDPHNo = Convert.ToInt32(ddlCode.SelectedValue);
            objProposal.intISDFXNo = Convert.ToInt32(drpFx.SelectedValue);
            objProposal.intISDMOBo = Convert.ToInt32(drpMob.SelectedValue);
            objProposal.intFaxCordet = Convert.ToInt32(drpFax2.SelectedValue);

            if (txtPhoneNoStateCodedet.Text == "")
            {
                objProposal.PhoneStateCode = 0;
            }
            else
            {
                objProposal.PhoneStateCode = Convert.ToInt32(txtPhoneNoStateCodedet.Text.Trim());
            }

            objProposal.Tagtodet = Convert.ToInt32(drpTagTo.SelectedValue);

            /*--------------------------------------------------------------------------------------*/
            //// DML Operation
            /*--------------------------------------------------------------------------------------*/
            string strRetVal = objService.ProposalPromoterAED(objProposal);

            string proposalno = strRetVal.ToString().Split('_')[1];
            Session["proposalno"] = proposalno;
            Session["txtIName"] = txtIName.Text;
            Session["ProjectCategory"] = drpProjectCat.SelectedValue;
            Session["ApplicationFor"] = drpApplicationFor.SelectedValue;
            Session["AllFileValue"] = hdnAllFileValue.Value;
            Session["Constitution"] = ddlConstitution.SelectedValue;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 for Add more click events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region Add More

    protected void AddMoreBD_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            DataTable dt1 = new DataTable();
            dt1 = ViewState["BoardOfDirectors"] as DataTable;
            DataTable dts = new DataTable();
            DataRow dr = null;
            dt1 = (ViewState["BoardOfDirectors"] as DataTable);
            for (int z = 0; z < dt1.Rows.Count; z++)
            {
                if (dt1.Rows[z]["vchName"].ToString() == txtPName.Text.ToString())
                {
                    flag = 1;
                    break;
                }
            }
            Regex regex = new Regex(@"^[ A-Za-z0-9]*$");

            if (! regex.IsMatch(txtPName.Text.ToString()))
            {

                flag = 2;
            }
            if (!regex.IsMatch(txtPDesg.Text.ToString()))
            {

                flag = 2;
            }


            if (flag == 0)
            {
                dr = dt1.NewRow();
                dr["intProId1"] = (dt1.Rows.Count + 1).ToString();
                dr["vchName"] = txtPName.Text.TrimEnd();
                dr["vchDesignation"] = txtPDesg.Text.TrimEnd();
                dt1.Rows.Add(dr);
                dr = null;
                GrdBD.DataSource = dt1;
                GrdBD.DataBind();
                dt1.TableName = "BoardOfDirectors";
                ViewState["BoardOfDirectors"] = dt1;
                ClearValue1();
                hdna.Value = "0";

                drpTagTo.DataSource = dt1;
                drpTagTo.DataTextField = "vchName";
                drpTagTo.DataValueField = "intProId1";
                drpTagTo.DataBind();
                drpTagTo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Name should not be duplicate.', '" + Messages.TitleOfProject + "'); </script>", false);
            }
            if (flag == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Special characters not allowed.', '" + Messages.TitleOfProject + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void btnAddMoreRWM_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = ViewState["RawMeterial"] as DataTable;
            DataTable dts = new DataTable();
            DataRow dr = null;
            dt1 = (ViewState["RawMeterial"] as DataTable);
            int flag1 = 0;
            for (int z = 0; z < dt1.Rows.Count; z++)
            {
                if (dt1.Rows[z]["vchRawMaterial"].ToString() == Server.HtmlEncode(txtRawMaterial.Text.ToString()))
                {
                    flag1 = 1;
                    break;
                }
            }

            Regex regex = new Regex(@"^[ A-Za-z0-9]*$");

            if (!regex.IsMatch(txtRawMaterial.Text.ToString()))
            {

                flag1 = 2;
            }

            if (flag1 == 0)
            {
                dr = dt1.NewRow();
                dr["intProId2"] = (dt1.Rows.Count + 1).ToString();
                dr["vchRawMaterial"] = Server.HtmlEncode(txtRawMaterial.Text.TrimEnd());
                dr["vchRawMeterialSrc"] = drprawMeterial.SelectedItem.Text;
                dt1.Rows.Add(dr);
                dr = null;
                GrvRWM.DataSource = dt1;
                GrvRWM.DataBind();
                dt1.TableName = "RawMeterial";
                ViewState["RawMeterial"] = dt1;
                ClearValue2();
            }
            if (flag1 == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Raw material for production should not be duplicate.', '" + Messages.TitleOfProject + "'); </script>", false);
            }
            if (flag1 == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Special characters not allowed.', '" + Messages.TitleOfProject + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 for clear all controls after add more in grid views
    /// </summary>
    #region Cleare All Controls
    private void ClearValue()
    {
        txtNameOfPromoter.Text = string.Empty;
    }
    private void ClearValue1()
    {
        txtPName.Text = string.Empty;
        txtPDesg.Text = string.Empty;
    }
    private void ClearValue2()
    {
        txtRawMaterial.Text = string.Empty;
        drprawMeterial.SelectedValue = "0";
    }
    #endregion

    /// <summary>
    /// Added By Subhasmita Behera on 18-Aug-2017 for delete from gridview after add more
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region Delete From Gridview After Add More

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = ViewState["PromoterName"] as DataTable;
            ImageButton imgbtn = (ImageButton)sender;
            HiddenField hdnid = (HiddenField)imgbtn.FindControl("hdpid");
            DataRow[] dr1 = null;
            dr1 = dt2.Select("intProId='" + hdnid.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {
                dr1[i].Delete();
            }
            dt2.AcceptChanges();
            GrvNameOfPromoter.DataSource = dt2;
            GrvNameOfPromoter.DataBind();
            ViewState["PromoterName"] = dt2;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void imgbtnDelete1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = ViewState["BoardOfDirectors"] as DataTable;
            ImageButton imgbtn = (ImageButton)sender;
            HiddenField hdnid = (HiddenField)imgbtn.FindControl("hdpid1");
            DataRow[] dr1 = null;
            dr1 = dt2.Select("intProId1='" + hdnid.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {
                dr1[i].Delete();
            }
            dt2.AcceptChanges();
            GrdBD.DataSource = dt2;
            GrdBD.DataBind();
            ViewState["BoardOfDirectors"] = dt2;

            drpTagTo.DataSource = dt2;
            drpTagTo.DataTextField = "vchName";
            drpTagTo.DataValueField = "intProId1";
            drpTagTo.DataBind();
            drpTagTo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void imgbtnDelete2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = ViewState["RawMeterial"] as DataTable;
            ImageButton imgbtn = (ImageButton)sender;
            HiddenField hdnid = (HiddenField)imgbtn.FindControl("hdpid2");
            DataRow[] dr1 = null;
            dr1 = dt2.Select("intProId2='" + hdnid.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {
                dr1[i].Delete();
            }
            dt2.AcceptChanges();
            GrvRWM.DataSource = dt2;
            GrvRWM.DataBind();
            ViewState["RawMeterial"] = dt2;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    #endregion

    #region Get Financial Year
    protected void drpFinYear1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int firFinVal = Convert.ToInt32(drpFinYear1.SelectedValue);
        if (firFinVal != 0)
        {
            int fin2 = firFinVal - 1;
            drpFinYear2.SelectedValue = Convert.ToString(fin2);
            int firFinVal2 = Convert.ToInt32(drpFinYear2.SelectedValue);
            int firFinVal3 = firFinVal2 - 1;
            drpFinYear3.SelectedValue = Convert.ToString(firFinVal3);
            lblFirstYear.Text = '(' + drpFinYear1.SelectedItem.Text + ')';
            lblSecondYear.Text = '(' + drpFinYear2.SelectedItem.Text + ')';
            lblThirdYear.Text = '(' + drpFinYear3.SelectedItem.Text + ')';
        }
        else
        {
            drpFinYear2.SelectedValue = "0";
            drpFinYear3.SelectedValue = "0";
            lblFirstYear.Text = "";
            lblSecondYear.Text = "";
            lblThirdYear.Text = "";
        }
    }
    #endregion

    #region Fill DropDownlist

    private void BindCountry()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "CT";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlCountry.DataSource = objProjList;
            ddlCountry.DataTextField = "vchCountryName";
            ddlCountry.DataValueField = "intCountryId";
            ddlCountry.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlCountry.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindYearofEstablishment()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "ci";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpYearofEstablishment.DataSource = objProjList;
            drpYearofEstablishment.DataTextField = "vchStatusName";
            drpYearofEstablishment.DataValueField = "intStatusId";
            drpYearofEstablishment.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpYearofEstablishment.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindState(int contid)
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "ST";
            objProp.vchProposalNo = Convert.ToString(contid);
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlState.DataSource = objProjList;
            ddlState.DataTextField = "vchStateName";
            ddlState.DataValueField = "intStateId";
            ddlState.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlState.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindCountry2()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "CT";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpCorCountry.DataSource = objProjList;
            drpCorCountry.DataTextField = "vchCountryName";
            drpCorCountry.DataValueField = "intCountryId";
            drpCorCountry.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpCorCountry.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindState2(int contid)
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "ST";
            objProp.vchProposalNo = Convert.ToString(contid);
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpCorState.DataSource = objProjList;
            drpCorState.DataTextField = "vchStateName";
            drpCorState.DataValueField = "intStateId";
            drpCorState.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpCorState.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindDistrict()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlDistrict.DataSource = objProjList;
            ddlDistrict.DataTextField = "vchDistName";
            ddlDistrict.DataValueField = "intDistId";
            ddlDistrict.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindSector()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SE";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlsector.DataSource = objProjList;
            ddlsector.DataTextField = "vchSectorName";
            ddlsector.DataValueField = "intSectorId";
            ddlsector.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlsector.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindSubSector(string strstate)
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SU";
            objProp.vchProposalNo = strstate;
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlSubSec.DataSource = objProjList;
            ddlSubSec.DataTextField = "vchSectorName";
            ddlSubSec.DataValueField = "intSectorId";
            ddlSubSec.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlSubSec.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void ddlsector_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSector(ddlsector.SelectedValue);
    }
    private void BindBlock(string strdist)
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "BL";
            objProp.vchProposalNo = strdist;
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpBlock.DataSource = objProjList;
            drpBlock.DataTextField = "vchBlockName";
            drpBlock.DataValueField = "intBlockId";
            drpBlock.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpBlock.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock(ddlDistrict.SelectedValue);
    }
    private void BindQualification()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "TA";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpEducation.DataSource = objProjList;
            drpEducation.DataTextField = "vchQf";
            drpEducation.DataValueField = "intQid";
            drpEducation.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpEducation.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void drpEducation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindtechQualifi(drpEducation.SelectedValue);
    }
    private void BindtechQualifi(string strdist)
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "TB";
            objProp.vchProposalNo = strdist;
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpTechnical.DataSource = objProjList;
            drpTechnical.DataTextField = "vchTQ";
            drpTechnical.DataValueField = "intTid";
            drpTechnical.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpTechnical.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindUnits()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "UT";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpCp.DataSource = objProjList;
            drpCp.DataTextField = "vchUnitName";
            drpCp.DataValueField = "intUnitId";
            drpCp.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select Unit--";
            list.Value = "0";
            drpCp.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindCountryCode()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "CC";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlCode.DataSource = objProjList;
            ddlCode.DataTextField = "vchCNTISDNo";
            ddlCode.DataValueField = "intCnt";
            ddlCode.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlCode.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindCountryCodeFx()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "CC";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpFx.DataSource = objProjList;
            drpFx.DataTextField = "vchCNTISDNo";
            drpFx.DataValueField = "intCnt";
            drpFx.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpFx.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindCountryCodeMB()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "CC";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpMob.DataSource = objProjList;
            drpMob.DataTextField = "vchCNTISDNo";
            drpMob.DataValueField = "intCnt";
            drpMob.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpMob.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    private void BindCountryCodeFxcor()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "CC";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpFax2.DataSource = objProjList;
            drpFax2.DataTextField = "vchCNTISDNo";
            drpFax2.DataValueField = "intCnt";
            drpFax2.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpFax2.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    #endregion

    #region File Upload

    private bool IsFileValidPAN(FileUpload FileUpload1)
    {
        string strFiletype = "";
        string fileExt = "";
        int count = 0;

        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldPan.FileName) != ".pdf") && (Path.GetExtension(FileUpldPan.FileName) != ".png") && (Path.GetExtension(FileUpldPan.FileName) != ".jpg") && (Path.GetExtension(FileUpldPan.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file Only!')", true);
                    return false;
                }

                int fileSize = FileUpldPan.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                else
                {
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "PAN" + Path.GetExtension(FileUpldPan.FileName), DateTime.Now);
                }

                if (!string.IsNullOrEmpty(FileUpldPan.FileName))
                {
                    hdPn1.Value = FileUpldPan.FileName;
                    allFileVal = allFileVal + FileUpldPan.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldPan.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldPan.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    hdnPanFile.Value = FilePath;
                    hlDoc1.NavigateUrl = "~/Enclosure/" + FilePath;
                    hlDoc1.Visible = true;
                    lnkDelPan.Visible = true;
                    lblPAN.Visible = true;
                    FileUpldPan.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidGSTIN(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldGstn.FileName) != ".pdf") && (Path.GetExtension(FileUpldGstn.FileName) != ".png") && (Path.GetExtension(FileUpldGstn.FileName) != ".jpg") && (Path.GetExtension(FileUpldGstn.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }

                int fileSize = FileUpldGstn.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }

                FilePathGSTN = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "GSTIN" + Path.GetExtension(FileUpldGstn.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldGstn.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldGstn.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldGstn.SaveAs(Server.MapPath("~/Enclosure/" + FilePathGSTN));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldGstn.SaveAs(Server.MapPath("~/Enclosure/" + FilePathGSTN));
                    }
                    hdnGstinFile.Value = FilePathGSTN;
                    hlDoc2.NavigateUrl = "~/Enclosure/" + FilePathGSTN;
                    hlDoc2.Visible = true;
                    lnkDelGST.Visible = true;
                    lblGSTIN.Visible = true;
                    FileUpldGstn.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidMemo(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldMemo.FileName) != ".pdf") && (Path.GetExtension(FileUpldMemo.FileName) != ".png") && (Path.GetExtension(FileUpldMemo.FileName) != ".jpg") && (Path.GetExtension(FileUpldMemo.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }

                int fileSize = FileUpldMemo.PostedFile.ContentLength;
                if (fileSize > (12 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 12 MB')", true);
                    return false;
                }
                FilePathMemo = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "Memo" + Path.GetExtension(FileUpldMemo.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldMemo.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldMemo.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldMemo.SaveAs(Server.MapPath("~/Enclosure/" + FilePathMemo));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldMemo.SaveAs(Server.MapPath("~/Enclosure/" + FilePathMemo));
                    }

                    hdnMemoFile.Value = FilePathMemo;
                    hlDoc3.NavigateUrl = "~/Enclosure/" + FilePathMemo;
                    hlDoc3.Visible = true;
                    lnkDelMemo.Visible = true;
                    lblMemo.Visible = true;
                    FileUpldMemo.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidCerti(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldCerti.FileName) != ".pdf") && (Path.GetExtension(FileUpldCerti.FileName) != ".png") && (Path.GetExtension(FileUpldCerti.FileName) != ".jpg") && (Path.GetExtension(FileUpldCerti.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUpldCerti.PostedFile.ContentLength;
                if (fileSize > (12 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 12 MB')", true);
                    return false;
                }

                FilePathCerti = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "Certificate" + Path.GetExtension(FileUpldCerti.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldCerti.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldCerti.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldCerti.SaveAs(Server.MapPath("~/Enclosure/" + FilePathCerti));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldCerti.SaveAs(Server.MapPath("~/Enclosure/" + FilePathCerti));
                    }

                    hdnCerti.Value = FilePathCerti;
                    hlDoc4.NavigateUrl = "~/Enclosure/" + FilePathCerti;
                    hlDoc4.Visible = true;
                    lnkDelCerti.Visible = true;
                    lblCerti.Visible = true;
                    FileUpldCerti.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidQuali(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldEducational.FileName) != ".pdf") && (Path.GetExtension(FileUpldEducational.FileName) != ".png") && (Path.GetExtension(FileUpldEducational.FileName) != ".jpg") && (Path.GetExtension(FileUpldEducational.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUpldEducational.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                FilePathEdu = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "Education" + Path.GetExtension(FileUpldEducational.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldEducational.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldEducational.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldEducational.SaveAs(Server.MapPath("~/Enclosure/" + FilePathEdu));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldEducational.SaveAs(Server.MapPath("~/Enclosure/" + FilePathEdu));
                    }

                    hdnEdu.Value = FilePathEdu;
                    hlDoc5.NavigateUrl = "~/Enclosure/" + FilePathEdu;
                    hlDoc5.Visible = true;
                    lnkDelEdu.Visible = true;
                    lblEdu.Visible = true;
                    FileUpldEducational.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidTechni(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldTechnical.FileName) != ".pdf") && (Path.GetExtension(FileUpldTechnical.FileName) != ".png") && (Path.GetExtension(FileUpldTechnical.FileName) != ".jpg") && (Path.GetExtension(FileUpldTechnical.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUpldTechnical.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                FilePathTech = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "Technical" + Path.GetExtension(FileUpldTechnical.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldTechnical.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldTechnical.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldTechnical.SaveAs(Server.MapPath("~/Enclosure/" + FilePathTech));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldTechnical.SaveAs(Server.MapPath("~/Enclosure/" + FilePathTech));
                    }

                    hdnTecnical.Value = FilePathTech;
                    hlDoc6.NavigateUrl = "~/Enclosure/" + FilePathTech;
                    hlDoc6.Visible = true;
                    lnkDelTechni.Visible = true;
                    lblTechni.Visible = true;
                    FileUpldTechnical.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidExperience(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldExperience.FileName) != ".pdf") && (Path.GetExtension(FileUpldExperience.FileName) != ".png") && (Path.GetExtension(FileUpldExperience.FileName) != ".jpg") && (Path.GetExtension(FileUpldExperience.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUpldExperience.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                FilePathexperi = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "Experience" + Path.GetExtension(FileUpldExperience.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldExperience.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldExperience.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldExperience.SaveAs(Server.MapPath("~/Enclosure/" + FilePathexperi));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldExperience.SaveAs(Server.MapPath("~/Enclosure/" + FilePathexperi));
                    }
                    hdnExperience.Value = FilePathexperi;
                    hlDoc7.NavigateUrl = "~/Enclosure/" + FilePathexperi;
                    hlDoc7.Visible = true;
                    lnkDelExperience.Visible = true;
                    lblExp.Visible = true;
                    FileUpldExperience.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidAudit(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldAudited.FileName) != ".pdf") && (Path.GetExtension(FileUpldAudited.FileName) != ".png") && (Path.GetExtension(FileUpldAudited.FileName) != ".jpg") && (Path.GetExtension(FileUpldAudited.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUpldAudited.PostedFile.ContentLength;
                if (fileSize > (12 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 12 MB')", true);
                    return false;
                }

                FilePathAudit = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "Audit" + Path.GetExtension(FileUpldAudited.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldAudited.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldAudited.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldAudited.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAudit));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldAudited.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAudit));

                    }
                    hdnAudit.Value = FilePathAudit;
                    hlDoc8.NavigateUrl = "~/Enclosure/" + FilePathAudit;
                    hlDoc8.Visible = true;
                    lnkDelAudit.Visible = true;
                    lblAudit1.Visible = true;
                    if (ddlConstitution.SelectedValue == "1")
                    {
                        lblAudit1.Text = "Networth Certificate of the Proprietor duly certified by CA for Current/latest year uploaded successfully";
                    }
                    else if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit1.Text = "Partnership deed uploaded successfully";
                    }
                    else
                    {
                        lblAudit1.Text = "Audited Financial Statements for First Year uploaded successfully";
                    }
                    FileUpldAudited.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidNetWorth(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldNetWorth.FileName) != ".pdf") && (Path.GetExtension(FileUpldNetWorth.FileName) != ".png") && (Path.GetExtension(FileUpldNetWorth.FileName) != ".jpg") && (Path.GetExtension(FileUpldNetWorth.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUpldNetWorth.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                FilePathNetWorth = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "NetWorth" + Path.GetExtension(FileUpldNetWorth.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldNetWorth.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpldNetWorth.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldNetWorth.SaveAs(Server.MapPath("~/Enclosure/" + FilePathNetWorth));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldNetWorth.SaveAs(Server.MapPath("~/Enclosure/" + FilePathNetWorth));

                    }
                    hdnNetWorth.Value = FilePathNetWorth;
                    hlDoc9.NavigateUrl = "~/Enclosure/" + FilePathNetWorth;
                    hlDoc9.Visible = true;
                    lnkDelnetWorth.Visible = true;
                    FileUpldNetWorth.Enabled = false;
                    lblNet.Visible = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidSecond(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUploadSecond.FileName) != ".pdf") && (Path.GetExtension(FileUploadSecond.FileName) != ".png") && (Path.GetExtension(FileUploadSecond.FileName) != ".jpg") && (Path.GetExtension(FileUploadSecond.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUploadSecond.PostedFile.ContentLength;
                if (fileSize > (12 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 12 MB')", true);
                    return false;
                }
                FilePathAuditsec = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "AuditSecond" + Path.GetExtension(FileUploadSecond.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUploadSecond.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUploadSecond.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUploadSecond.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAuditsec));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUploadSecond.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAuditsec));

                    }
                    hdnFySecond.Value = FilePathAuditsec;
                    hlDoc12.NavigateUrl = "~/Enclosure/" + FilePathAuditsec;
                    hlDoc12.Visible = true;
                    lnkFySecondDelete.Visible = true;
                    lblAudit2.Visible = true;
                    if (ddlConstitution.SelectedValue == "1")
                    {
                        lblAudit2.Text = "Tax Audit Report(if applicable) for Current/latest year uploaded successfully";
                    }
                    else if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit2.Text = "Complete balance sheet of the firm(latest 3 years) uploaded successfully";
                    }
                    else
                    {
                        lblAudit2.Text = "Audited Financial Statements for Second Year uploaded successfully";
                    }
                    FileUploadSecond.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidThird(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUploadThird.FileName) != ".pdf") && (Path.GetExtension(FileUploadThird.FileName) != ".png") && (Path.GetExtension(FileUploadThird.FileName) != ".jpg") && (Path.GetExtension(FileUploadThird.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUploadThird.PostedFile.ContentLength;
                if (fileSize > (12 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 12 MB')", true);
                    return false;
                }
                FilePathAuditThird = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "AuditThird" + Path.GetExtension(FileUploadThird.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUploadThird.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUploadThird.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUploadThird.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAuditThird));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUploadThird.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAuditThird));
                    }

                    hdnFyThird.Value = FilePathAuditThird;
                    hlDoc13.NavigateUrl = "~/Enclosure/" + FilePathAuditThird;
                    hlDoc13.Visible = true;
                    lnkFyThirdDel.Visible = true;
                    lblAudit3.Visible = true;
                    if (ddlConstitution.SelectedValue == "1")
                    {
                        lblAudit3.Text = "Income tax return for Current/latest year uploaded successfully";
                    }
                    else if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit3.Text = "Tax audit report of the Partnership firm uploaded successfully";
                    }
                    else
                    {
                        lblAudit3.Text = "Audited Financial Statements for Third Year uploaded successfully";
                    }
                    FileUploadThird.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidFourth(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUploadFourthupd.FileName) != ".pdf") && (Path.GetExtension(FileUploadFourthupd.FileName) != ".png") && (Path.GetExtension(FileUploadFourthupd.FileName) != ".jpg") && (Path.GetExtension(FileUploadFourthupd.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUploadFourthupd.PostedFile.ContentLength;
                if (fileSize > (12 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 12 MB')", true);
                    return false;
                }
                FilePathAuditFourth = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "AuditFourth" + Path.GetExtension(FileUploadFourthupd.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUploadFourthupd.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUploadFourthupd.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUploadFourthupd.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAuditFourth));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUploadFourthupd.SaveAs(Server.MapPath("~/Enclosure/" + FilePathAuditFourth));

                    }
                    hdnFyFourth.Value = FilePathAuditFourth;
                    hlDoc14.NavigateUrl = "~/Enclosure/" + FilePathAuditFourth;
                    hlDoc14.Visible = true;
                    lnkFyFourDel.Visible = true;
                    lblAudit4.Visible = true;
                    if (ddlConstitution.SelectedValue == "2")
                    {
                        lblAudit4.Text = "Income tax return uploaded successfully";
                    }
                    FileUploadFourthupd.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }

    protected void lnkPan_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidPAN(FileUpldPan);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkGSTIN_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidGSTIN(FileUpldGstn);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkMemo_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidMemo(FileUpldMemo);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkCerti_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidCerti(FileUpldCerti);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkQuali_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidQuali(FileUpldEducational);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkTechni_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidTechni(FileUpldTechnical);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkExperience_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidExperience(FileUpldExperience);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkAudit_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidAudit(FileUpldAudited);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnknetWorth_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidNetWorth(FileUpldNetWorth);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }

    }
    protected void lnkFySecond_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidSecond(FileUploadSecond);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }
    protected void lnkFyThird_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidThird(FileUploadThird);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }

    }
    protected void lnkFyFourth_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidFourth(FileUploadFourthupd);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {

        }
    }

    protected void lnkDelPan_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnPanFile, lnkPan, lnkDelPan, hlDoc1, lblPAN, FileUpldPan, "1");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "A";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "A";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelGST_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnGstinFile, lnkGSTIN, lnkDelGST, hlDoc2, lblGSTIN, FileUpldGstn, "2");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "B";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "B";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelMemo_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnMemoFile, lnkMemo, lnkDelMemo, hlDoc3, lblMemo, FileUpldMemo, "3");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "C";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "C";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelCerti_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnCerti, lnkCerti, lnkDelCerti, hlDoc4, lblCerti, FileUpldCerti, "4");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "D";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "D";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelEdu_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnEdu, lnkQuali, lnkDelEdu, hlDoc5, lblEdu, FileUpldEducational, "5");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "E";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "E";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelTechni_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnTecnical, lnkTechni, lnkDelTechni, hlDoc6, lblTechni, FileUpldTechnical, "6");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "F";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "F";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelExperience_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnExperience, lnkExperience, lnkDelExperience, hlDoc7, lblExp, FileUpldExperience, "7");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "G";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "G";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelAudit_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnAudit, lnkAudit, lnkDelAudit, hlDoc8, lblAudit1, FileUpldAudited, "8");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "H";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "H";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkDelnetWorth_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnNetWorth, lnknetWorth, lnkDelnetWorth, hlDoc9, lblNet, FileUpldNetWorth, "9");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "I";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "I";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkFySecondDelete_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnFySecond, lnkFySecond, lnkFySecondDelete, hlDoc12, lblAudit2, FileUploadSecond, "10");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "Q";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "Q";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkFyThirdDel_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnFyThird, lnkFyThird, lnkFyThirdDel, hlDoc13, lblAudit3, FileUploadThird, "11");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "R";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "R";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }
    protected void lnkFyFourDel_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnFyFourth, lnkFyFourth, lnkFyFourDel, hlDoc14, lblAudit4, FileUploadFourthupd, "12");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "X";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "X";
            string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
        }
    }

    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string Position)
    {
        if (Position == "1")
        {
            string strPanFiletoRemove = hdPn1.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "2")
        {
            string strPanFiletoRemove = hdPn2.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "3")
        {
            string strPanFiletoRemove = hdPn3.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "4")
        {
            string strPanFiletoRemove = hdPn4.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "5")
        {
            string strPanFiletoRemove = hdPn5.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "6")
        {
            string strPanFiletoRemove = hdPn6.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "7")
        {
            string strPanFiletoRemove = hdPn7.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "8")
        {
            string strPanFiletoRemove = hdPn8.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "9")
        {
            string strPanFiletoRemove = hdPn9.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "10")
        {
            string strPanFiletoRemove = hdPn10.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "11")
        {
            string strPanFiletoRemove = hdPn11.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "12")
        {
            string strPanFiletoRemove = hdPn14.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }

        string filename = hdnFile.Value;
        string path = "~/Enclosure/" + filename;
        string completePath = Server.MapPath(path);
        if (System.IO.File.Exists(completePath))
        {
            System.IO.File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
    }
    private void DelBtnHideshow()
    {
        lnkDelPan.Visible = false;
        lnkDelGST.Visible = false;
        lnkDelMemo.Visible = false;
        lnkDelCerti.Visible = false;
        lnkDelEdu.Visible = false;
        lnkDelTechni.Visible = false;
        lnkDelExperience.Visible = false;
        lnkDelAudit.Visible = false;
        lnkFySecondDelete.Visible = false;
        lnkFyThirdDel.Visible = false;
        lnkDelnetWorth.Visible = false;
        lnkFyFourDel.Visible = false;
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cntid = Convert.ToInt32(ddlCountry.SelectedValue);
        BindState(cntid);
        if (cntid == 1)
        {
            st1.Visible = true;
            st2.Visible = false;
        }
        else
        {
            st2.Visible = true;
            st1.Visible = false;
        }
        ddlCode.SelectedValue = ddlCountry.SelectedValue;
        drpFx.SelectedValue = ddlCountry.SelectedValue;
    }
    protected void drpCorCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cntid = Convert.ToInt32(drpCorCountry.SelectedValue);
        BindState2(cntid);
        if (cntid == 1)
        {
            st3.Visible = true;
            st4.Visible = false;
        }
        else
        {
            st4.Visible = true;
            st3.Visible = false;
        }
        drpMob.SelectedValue = drpCorCountry.SelectedValue;
        drpFax2.SelectedValue = drpCorCountry.SelectedValue;
    }
    protected void chkBoxAddress_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBoxAddress.Checked == true)
        {
            txtCorAddrs.Text = txtAddress.Text;
            drpCorCountry.SelectedValue = ddlCountry.SelectedValue;
            if (ddlCountry.SelectedValue == "1")
            {
                drpCorState.SelectedValue = ddlState.SelectedValue;
                st1.Visible = true;
                st2.Visible = false;
                st3.Visible = true;
                st4.Visible = false;
            }
            else
            {
                txtCorState.Text = txtState.Text;
                st1.Visible = false;
                st2.Visible = true;
                st3.Visible = false;
                st4.Visible = true;
            }
            txtCorCity.Text = txtCity.Text;
            drpMob.SelectedValue = ddlCode.SelectedValue;
            txtCorFax.Text = txtFaxNo.Text;
            txtCorEmailid.Text = txtEmail.Text;
            txtCorPin.Text = txtPinCode.Text;
            drpFax2.SelectedValue = drpFx.SelectedValue;
        }
        else
        {
            txtCorAddrs.Text = "";
            drpCorCountry.SelectedValue = "0";
            if (ddlCountry.SelectedValue == "1")
            {
                drpCorState.SelectedValue = "0";
                st1.Visible = true;
                st2.Visible = false;
                st3.Visible = true;
                st4.Visible = false;
            }
            else
            {
                txtCorState.Text = "";
                st1.Visible = false;
                st2.Visible = true;
                st3.Visible = false;
                st4.Visible = true;
            }
            txtCorCity.Text = "";
            txtCorFax.Text = "";
            txtCorEmailid.Text = "";
            txtCorPin.Text = "";
            drpFax2.SelectedValue = "1";
        }
    }


    #endregion
    #region Reset Control
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("PromoterDetails.aspx");
    }
    #endregion

    /// <summary>
    /// Added by Sushant Jena On Dt:- 05-04-2021.
    /// This method is used to pull state CAF details from NSWS,Invest India portal.
    /// This method will be executed only when the user is coming from NSWS portal.
    /// </summary>
    /// <param name="strInvestorSwsId"></param>
    private void PullStateCafNsws(string strInvestorSwsId)
    {
        try
        {
            /*---------------------------------------------------------------------------------*/
            //// Check whether the state CAF details has been pulled from NSWS portal or not.
            //// If pulled then display the records from table.
            //// If not pulled, then pull the state CAF details and then display it.
            /*---------------------------------------------------------------------------------*/

            objProposal.strAction = "B";
            objProposal.strInvestorSWSId = strInvestorSwsId;

            DataTable dt = new DataTable();
            dt = objService.GetCAFDetailsNSWS(objProposal);
            if (dt.Rows.Count > 0)
            {
                /*--------------------------------------------------------------*/
                ///Company Information
                /*--------------------------------------------------------------*/
                txtIName.Text = Convert.ToString(dt.Rows[0]["vchCompanyName"]);

                /*--------------------------------------------------------------*/
                ///Corporate Office Address
                /*--------------------------------------------------------------*/
                txtAddress.Text = Convert.ToString(dt.Rows[0]["vchCorpAddress"]);
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["vchCorpState"]);
                txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["vchCorpPhoneNo"]);
                ddlCountry.SelectedValue = Convert.ToString(dt.Rows[0]["vchCorpCountry"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["vchCorpCity"]);
                txtFaxNo.Text = Convert.ToString(dt.Rows[0]["vchCorpFaxNo"]);
                txtPinCode.Text = Convert.ToString(dt.Rows[0]["vchCorpPIN"]);
                txtEmail.Text = Convert.ToString(dt.Rows[0]["vchCorpEmailId"]);

                /*--------------------------------------------------------------*/
                ///Correspondence Address
                /*--------------------------------------------------------------*/
                txtCorAddrs.Text = Convert.ToString(dt.Rows[0]["vchCorrAddress"]);
                drpCorState.SelectedValue = Convert.ToString(dt.Rows[0]["vchCorrState"]);
                txtCorMob.Text = Convert.ToString(dt.Rows[0]["vchCorrPhoneNo"]);
                drpCorCountry.SelectedValue = Convert.ToString(dt.Rows[0]["vchCorrCountry"]);
                txtCorCity.Text = Convert.ToString(dt.Rows[0]["vchCorrCity"]);
                txtCorFax.Text = Convert.ToString(dt.Rows[0]["vchCorrFaxNo"]);
                txtCorPin.Text = Convert.ToString(dt.Rows[0]["vchCorrPIN"]);
                txtCorEmailid.Text = Convert.ToString(dt.Rows[0]["vchCorrEmailId"]);

                txtCperson.Text = Convert.ToString(dt.Rows[0]["vchContactPerson"]);

                string strConstituion = Convert.ToString(dt.Rows[0]["vchConstCompany"]);
                #region for Previous Comment Code
                // below Code Comment Because CONSTITUTION ID value Coming 17-09-22
                //if (strConstituion == "Proprietorship")
                //{
                //    ddlConstitution.SelectedValue = "1";
                //}
                //else if (strConstituion == "Partnership")
                //{
                //    ddlConstitution.SelectedValue = "2";
                //}
                //else if (strConstituion == "Private Limited Company")
                //{
                //    ddlConstitution.SelectedValue = "3";
                //}
                //else if (strConstituion == "Public Limited Company")
                //{
                //    ddlConstitution.SelectedValue = "4";
                //}
                //else if (strConstituion == "PSU")
                //{
                //    ddlConstitution.SelectedValue = "5";
                //}
                //else if (strConstituion == "SPV")
                //{
                //    ddlConstitution.SelectedValue = "6";
                //}
                //else if (strConstituion == "Co-operative")
                //{
                //    ddlConstitution.SelectedValue = "7";
                //}
                //else
                //{
                //    ddlConstitution.SelectedValue = "8";
                //}
                #endregion

                ddlConstitution.SelectedValue = strConstituion;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnableAndDiseableBasedOnddlConstitution(" + ddlConstitution.SelectedValue + ");", true);
                //Load Board of Director Details Constituion value if 3,4,5,6,7

                if (Convert.ToInt32(strConstituion)>2)
                {
                    string strvchBoardofDirector = Convert.ToString(dt.Rows[0]["vchBoardofDirector"]);
                    List<PromoterDet> objProposalList = new List<PromoterDet>();
                    if (!string.IsNullOrEmpty(strvchBoardofDirector))
                    {
                        List<NSWSAPIReponseCls.SubField> objSubFields = new List<NSWSAPIReponseCls.SubField>();
                        var objs = JsonConvert.DeserializeObject<List<NSWSAPIReponseCls.SubField>>(strvchBoardofDirector);
                        string strVchName = "";
                        string strVchDesignation = "";
                        foreach (NSWSAPIReponseCls.SubField objSubFieldEach in objs)
                        {

                            if (objSubFieldEach.fieldKey == "F-9020") //director
                            {
                                strVchName = objSubFieldEach.inputValue;
                            }
                            else if (objSubFieldEach.fieldKey == "F-39875") //desgin
                            {
                                strVchDesignation = objSubFieldEach.inputValue;
                            }
                            //Asign data to List
                            if (!string.IsNullOrEmpty(strVchName) && !string.IsNullOrEmpty(strVchDesignation))
                            {
                                PromoterDet objProp = new PromoterDet();
                                objProp.vchName = strVchName;
                                objProp.vchDesignation = strVchDesignation;
                                objProposalList.Add(objProp);
                                strVchName = "";
                                strVchDesignation = "";
                            }
                        }
                        if (objProposalList.Count > 0)
                        {
                            GrdBD.DataSource = objProposalList;
                            GrdBD.DataBind();
                            DataTable dt1 = CreateDataTableBD();
                            for (int i = 0; i <= GrdBD.Rows.Count - 1; i++)
                            {
                                HiddenField hdpid1 = (HiddenField)GrdBD.Rows[i].FindControl("hdpid1");
                                DataRow dr = dt1.NewRow();
                                dr["intProId1"] = (dt1.Rows.Count + 1).ToString();//hdpid1.Value;
                                dr["vchName"] = GrdBD.Rows[i].Cells[1].Text;
                                dr["vchDesignation"] = GrdBD.Rows[i].Cells[2].Text;
                                dt1.Rows.Add(dr);
                                dt1.AcceptChanges();
                            }
                            ViewState["BoardOfDirectors"] = dt1;
                            drpTagTo.Items.Clear();
                            drpTagTo.DataSource = dt1;
                            drpTagTo.DataTextField = "vchName";
                            drpTagTo.DataValueField = "intProId1";
                            drpTagTo.DataBind();
                            drpTagTo.Items.Insert(0, new ListItem("--Select--", "0"));
                        }
                    }
                }
               
                /*--------------------------------------------------------------*/
                ///Entrepreneur Registration Details
                /*--------------------------------------------------------------*/
                #region previous code
                //vchInvestmentLevel
                //Below Code is Comment using vchInvestmentLevel-17-03-22
                //string strProjectType = Convert.ToString(dt.Rows[0]["vchProjectType"]);
                //if (strProjectType == "Project Cost >= 50 crore")
                //{
                //    drpProjectCat.SelectedValue = "1";
                //}
                //else if (strProjectType == "Project cost upto < 50 crore")
                //{
                //    drpProjectCat.SelectedValue = "2";
                //}
                #endregion
                string strProjectType = Convert.ToString(dt.Rows[0]["vchInvestmentLevel"]);
                drpProjectCat.SelectedValue = strProjectType;
                

                string strApplicationType = Convert.ToString(dt.Rows[0]["vchApplicationType"]);

                #region Previous code
                //17-03-22
                //if (strApplicationType == "New Unit")
                //{
                //    drpApplicationFor.SelectedValue = "1";
                //}
                //else if (strApplicationType == "Expansion of existing unit")
                //{
                //    drpApplicationFor.SelectedValue = "2";

                //    /*--------------------------------------------------------------*/
                //    /////Existing Industry Details
                //    /*--------------------------------------------------------------*/
                //    //txtCapacity.Text = Convert.ToString(dt.Rows[0]["vchExistingCapacity"]);
                //    txtNatureAct.Text = Convert.ToString(dt.Rows[0]["vchExistingActivity"]);
                //    txtExtIndName.Text = Convert.ToString(dt.Rows[0]["vchExistingIndustryName"]);
                //    ddlsector.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingSector"]);
                //    ddlSubSec.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingSubSector"]);
                //    ddlDistrict.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingDistrict"]);
                //    drpBlock.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingBlock"]);
                //    txtExtentLand.Text = Convert.ToString(dt.Rows[0]["decExistingExtentOfLand"]);

                //    string strLandAllotted = Convert.ToString(dt.Rows[0]["vchExistingLandAllotted"]);
                //    if (strLandAllotted == "Yes")
                //    {
                //        ddlAlloted.SelectedValue = "1";
                //    }
                //    else if (strLandAllotted == "No")
                //    {
                //        ddlAlloted.SelectedValue = "2";
                //    }
                //}
                #endregion

                if (strApplicationType == "1")
                {
                    drpApplicationFor.SelectedValue = "1";
                }
                else if (strApplicationType == "2")
                {
                    drpApplicationFor.SelectedValue = "2";

                    /*--------------------------------------------------------------*/
                    /////Existing Industry Details
                    /*--------------------------------------------------------------*/
                    //txtCapacity.Text = Convert.ToString(dt.Rows[0]["vchExistingCapacity"]);
                    txtNatureAct.Text = Convert.ToString(dt.Rows[0]["vchExistingActivity"]);
                    txtExtIndName.Text = Convert.ToString(dt.Rows[0]["vchExistingIndustryName"]);
                    ddlsector.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingSector"]);
                    ddlSubSec.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingSubSector"]);
                    BindSubSector(ddlsector.SelectedValue);
                    
                    ddlDistrict.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingDistrict"]);
                    drpBlock.SelectedValue = Convert.ToString(dt.Rows[0]["vchExistingBlock"]);
                    BindBlock(ddlDistrict.SelectedValue);
                    txtExtentLand.Text = Convert.ToString(dt.Rows[0]["decExistingExtentOfLand"]);
                    string strLandAllotted = Convert.ToString(dt.Rows[0]["vchExistingLandAllotted"]);
                    if (strLandAllotted == "Yes")
                    {
                        ddlAlloted.SelectedValue = "1";
                    }
                    else if (strLandAllotted == "No")
                    {
                        ddlAlloted.SelectedValue = "2";
                    }
                }

                txtGSTIN.Text = Convert.ToString(dt.Rows[0]["vchGSTINNo"]);

                /*============================================================================================*/
                ///Download GSTIN document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strGSTINDoc = Convert.ToString(dt.Rows[0]["vchGSTINDoc"]);
                if (strGSTINDoc != "")
                {
                    DownloadDocFromNSWS(strGSTINDoc, "GSTIN", hdnGstinFile, lnkDelGST, hlDoc2, FileUpldGstn, lblGSTIN);
                }

                drpYearofEstablishment.SelectedValue = Convert.ToString(dt.Rows[0]["intYearOfEstablishment"]);
                txtPlaceIncorporation.Text = Convert.ToString(dt.Rows[0]["vchPlaceOfIncorp"]);

                /*============================================================================================*/
                ///Download PAN document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strPANDoc = Convert.ToString(dt.Rows[0]["vchPANDoc"]);
                if (strPANDoc != "")
                {
                    DownloadDocFromNSWS(strPANDoc, "PAN", hdnPanFile, lnkDelPan, hlDoc1, FileUpldPan, lblPAN);
                }

                //hdnPanFile.Value = strGoswiftFileName;
                //              hlDoc1.NavigateUrl = "~/Enclosure/" + strGoswiftFileName;
                //              hlDoc1.Visible = true;
                //              lnkDelPan.Visible = true;
                //              lblPAN.Visible = true;
                //              FileUpldPan.Enabled = false;

                /*============================================================================================*/
                ///Download Memorandum document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strMemorandumDoc = Convert.ToString(dt.Rows[0]["vchMemorandumDoc"]);
                if (strMemorandumDoc != "")
                {
                    DownloadDocFromNSWS(strMemorandumDoc, "Memo", hdnMemoFile, lnkDelMemo, hlDoc3, FileUpldMemo, lblMemo);
                }

                //hdnMemoFile.Value = FilePathMemo;
                //hlDoc3.NavigateUrl = "~/Enclosure/" + FilePathMemo;
                //hlDoc3.Visible = true;
                //lnkDelMemo.Visible = true;
                //lblMemo.Visible = true;
                //FileUpldMemo.Enabled = false;

                /*============================================================================================*/
                /////Download Certificate of Incorporation document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strCertOfIncorpDoc = Convert.ToString(dt.Rows[0]["vchCertOfIncorpDoc"]);
                if (strCertOfIncorpDoc != "")
                {
                    DownloadDocFromNSWS(strCertOfIncorpDoc, "Certificate", hdnCerti, lnkDelCerti, hlDoc4, FileUpldCerti, lblCerti);
                }

                //hdnCerti.Value = FilePathCerti;
                //hlDoc4.NavigateUrl = "~/Enclosure/" + FilePathCerti;
                //hlDoc4.Visible = true;
                //lnkDelCerti.Visible = true;
                //lblCerti.Visible = true;
                //FileUpldCerti.Enabled = false;


                //else if (fieldName == "PAN")
                //{

                //}
                //else if (fieldName == "GSTIN DOCUMENT") //// doc
                //{

                //}

                /*--------------------------------------------------------------*/
                /////Financial Status
                /*--------------------------------------------------------------*/
                txtProfit1.Text = Convert.ToString(dt.Rows[0]["decProfitAfterTax"]);
                txtNtWorth1.Text = Convert.ToString(dt.Rows[0]["decNetWorth"]);
                txtShare1.Text = Convert.ToString(dt.Rows[0]["decShareCapital"]);
                txtAnnual1.Text = Convert.ToString(dt.Rows[0]["decAnnualTurnOver"]);
                txtReserve1.Text = Convert.ToString(dt.Rows[0]["decResvSurp"]);

                //else if (fieldName == "Audited Financial Statements for First Year")              
                //else if (fieldName == "Audited Financial Statements for Second Year")               
                //else if (fieldName == "Audited Financial Statements for Third Year")               
                //else if (fieldName == "Net Worth Certified by CA")                

                //vchAuditDoc1 VARCHAR(200),
                //vchAuditDoc2 VARCHAR(200),
                //vchAuditDoc3 VARCHAR(200),
                //vchNetWorthDoc VARCHAR(200),


                /*============================================================================================*/
                ///Download Networth document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strNetWorthDoc = Convert.ToString(dt.Rows[0]["vchNetWorthDoc"]);
                if (strNetWorthDoc != "")
                {
                    DownloadDocFromNSWS(strNetWorthDoc, "Audit", hdnAudit, lnkDelAudit, hlDoc8, FileUpldAudited, lblAudit1);
                }


                /*--------------------------------------------------------------*/
                ///Details of the Management
                /*--------------------------------------------------------------*/
                txtPName.Text = Convert.ToString(dt.Rows[0]["vchBoDName"]);
                txtPDesg.Text = Convert.ToString(dt.Rows[0]["vchBoDDesignation"]);
                txtexpinYr.Text = Convert.ToString(dt.Rows[0]["intExperience"]);
                //else if (fieldName == "Educational Qualification of one of the Directors Technical Qualification of one of the Directors")
            }
            else
            {
                /////Pull State CAF from NSWS Portal.
                NSWSScheduler objNSWS = new NSWSScheduler();
                string strReturnStatus = objNSWS.PullAndInsertStateCAF(strInvestorSwsId);
                if (strReturnStatus == "1")
                {
                    PullStateCafNsws(strInvestorSwsId);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Added by Sushant Jena
    /// This method is used to download documen from NSWS portal and dispaly in PEAL form.
    /// </summary>
    /// <param name="strNswsFileName"></param>
    /// <param name="strFileType"></param>
    /// <param name="HdnFileName"></param>
    /// <param name="LnkBtnDelete"></param>
    /// <param name="HypLnkView"></param>
    /// <param name="FileUpd"></param>
    /// <param name="LblMsg"></param>
    private void DownloadDocFromNSWS(string strNswsFileName, string strFileType, HiddenField HdnFileName, LinkButton LnkBtnDelete, HyperLink HypLnkView, FileUpload FileUpd, Label LblMsg)
    {
        try
        {
            string strFileContentId = JsonConvert.DeserializeObject<Dictionary<string, object>>(strNswsFileName.Replace("[", "").Replace("]", ""))["value"].ToString();

            /*---------------------------------------------------------------------------------------*/
            ///Get the API address,access-id,access-secret and api key from web configuration file.
            /*---------------------------------------------------------------------------------------*/
            string strPullDocApiUrl = ConfigurationManager.AppSettings["NswsPullDocApiUrl"].ToString();
            string strAccessId = ConfigurationManager.AppSettings["NswsApiAccessId"].ToString();
            string strAccessSecret = ConfigurationManager.AppSettings["NswsApiAccessSecret"].ToString();
            string strApiKeyPullDoc = ConfigurationManager.AppSettings["NswsApiKeyPullDoc"].ToString();

            /*---------------------------------------------------------------------------------------*/

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var client = new RestClient(strPullDocApiUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("access-id", strAccessId);
            request.AddHeader("access-secret", strAccessSecret);
            request.AddHeader("api-key", strApiKeyPullDoc);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"contentId\":[\"" + strFileContentId + "\"]}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            string strCmnLogMsg = "File Type:- " + strFileType + " <----> File Content Id:- " + strFileContentId;
            string strFileResposnse = response.Content.Length > 200 ? response.Content.ToString().Substring(0, 200) : response.Content.ToString();
            Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> Response Content:- " + strFileResposnse);

            if (response.Content.ToString() != "")
            {
                PullApiDoc objApp = JsonConvert.DeserializeObject<PullApiDoc>(response.Content);

                string strStatus = objApp.status;
                if (strStatus == "200")
                {
                    List<DocResponseFile> objDocRes = new List<DocResponseFile>();
                    objDocRes = objApp.response.ToList();

                    string strFileName = objDocRes[0].fileName;
                    string strFileResponse = objDocRes[0].fileResponse; ////Byte stream of the file to be downloaded.

                    byte[] data = Convert.FromBase64String(strFileResponse);
                    if (data.Length > 0)
                    {
                        if (IsFileValidNSWS(data, strFileName))
                        {
                            /*--------------------------------------------------------------*/
                            ///// Rename the file as per the GOSWIFT naming format.
                            /*--------------------------------------------------------------*/
                            string strFileExtention = Path.GetExtension(strFileName);
                            string strGoswiftFileName = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + strFileType + strFileExtention, DateTime.Now);

                            /*--------------------------------------------------------------*/
                            //// Physical path of GOSWIFT document folder.
                            /*--------------------------------------------------------------*/
                            string strPath = Server.MapPath("~/Enclosure/");

                            /*--------------------------------------------------------------*/
                            /////Save the file to destination folder
                            /*--------------------------------------------------------------*/
                            FileStream fileStream = null;
                            fileStream = new FileStream(strPath + strGoswiftFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            using (System.IO.FileStream fs = fileStream)
                            {
                                fs.Write(data, 0, data.Length);

                                HdnFileName.Value = strGoswiftFileName;
                                HypLnkView.NavigateUrl = "~/Enclosure/" + strGoswiftFileName;
                                HypLnkView.Visible = true;
                                LnkBtnDelete.Visible = true;
                                LblMsg.Visible = true;
                                FileUpd.Enabled = false;

                                //hdnPanFile.Value = strGoswiftFileName;
                                //hlDoc1.NavigateUrl = "~/Enclosure/" + strGoswiftFileName;
                                //hlDoc1.Visible = true;
                                //lnkDelPan.Visible = true;
                                //lblPAN.Visible = true;
                                //FileUpldPan.Enabled = false;

                                if (strFileType == "Audit")
                                {
                                    if (ddlConstitution.SelectedValue == "1")
                                    {
                                        LblMsg.Text = "Networth Certificate of the Proprietor duly certified by CA for Current/latest year uploaded successfully";
                                    }
                                    else if (ddlConstitution.SelectedValue == "2")
                                    {
                                        LblMsg.Text = "Partnership deed uploaded successfully";
                                    }
                                    else
                                    {
                                        LblMsg.Text = "Audited Financial Statements for First Year uploaded successfully";
                                    }
                                }
                            }
                        }
                        else
                        {
                            Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> Invalid or corrupted file found");
                        }
                    }
                    else
                    {
                        Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No file found for download.");
                    }
                }
                else
                {
                    Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No file found (404 Error).");
                }
            }
            else
            {
                Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No response found for file.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public class PullApiDoc
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DocResponseFile> response { get; set; }
    }
    public class DocResponseFile
    {
        public string fileName { get; set; }
        public string fileResponse { get; set; }
    }

    private bool IsFileValidNSWS(byte[] filebyte, string strFileName)
    {
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();

        string[] allowedMimeTypes = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
        string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
        imageTypes.AddRange(allowedMimeTypes);
        imageExtension.AddRange(allowedExtension);
        string strFileMimeType = MimeType.GetMimeType(filebyte, strFileName);
        string strFileExt = System.IO.Path.GetExtension(strFileName);
        int intDotCount = strFileName.Count(f => f == '.');

        if (imageTypes.Contains(strFileMimeType) == true && imageExtension.Contains(strFileExt.ToLower()) && intDotCount == 1)
        {
            return true;

            //int fileSize = FileUpldPan.PostedFile.ContentLength;
            //if (fileSize > (4 * 1024 * 1024))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
            //    return false;
            //} 
        }
        else
        {
            Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strFileName + " ---- " + "File Mime type is not Correct.");
            return false;
        }
    }
}

