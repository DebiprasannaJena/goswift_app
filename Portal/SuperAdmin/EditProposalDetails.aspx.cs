using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Data;
using System.IO;

public partial class Portal_SuperAdmin_EditProposalDetails : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    PromoterDet objProposal = new PromoterDet();
    CommonValidation objcmv = new CommonValidation();
    DataTable objdt;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                st2.Visible = false;
                st4.Visible = false;
                ddlCode.SelectedValue = "1";
                drpFx.SelectedValue = "1";
                drpMob.SelectedValue = "1";
                drpFax2.SelectedValue = "1";
                ddlCountry.SelectedValue = "1";
                drpCorCountry.SelectedValue = "1";
                BindCountry();
                int countryid = Convert.ToInt32(ddlCountry.SelectedValue);
                BindState(countryid);
                BindCountryCode();
                BindCountryCodeFx();
                BindCountry2();
                int countryid2 = Convert.ToInt32(drpCorCountry.SelectedValue);
                BindState2(countryid2);
                BindCountryCodeMB();
                BindCountryCodeFxcor();
                BindYearofEstablishment();

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
                        //if (!string.IsNullOrEmpty(Session["proposalno"] as string))
                        //{
                        //    GetCompanyInfo(Session["proposalno"].ToString());
                        //}
                    }
                }
                else
                {


                }
            }


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtpPendingReport");
        }

    }

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
                //BindState2(cntid2);
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
                //BindtechQualifi(drpEducation.SelectedValue);

                drpTechnical.SelectedValue = Convert.ToString(objProposalList[0].intTecQualif.ToString());
                txtexpinYr.Text = Convert.ToString(objProposalList[0].intExpInYr.ToString());
                txtExtIndName.Text = Convert.ToString(Server.HtmlDecode(objProposalList[0].vchExisIndName.ToString()));

                ddlDistrict.SelectedValue = Convert.ToString(objProposalList[0].intExisDistrict.ToString());
                if (ddlDistrict.SelectedValue != "0")
                {
                    //BindBlock(ddlDistrict.SelectedValue);
                }

                drpBlock.SelectedValue = Convert.ToString(objProposalList[0].intExisBlock.ToString());
                ddlAlloted.SelectedValue = Convert.ToString(objProposalList[0].intAllotedBy.ToString());
                txtExtentLand.Text = Convert.ToString(objProposalList[0].vchlandInAcres.ToString());
                txtNatureAct.Text = Convert.ToString(objProposalList[0].vchNatureAct.ToString());
                ddlsector.SelectedValue = Convert.ToString(objProposalList[0].intSectorId.ToString());
                if (ddlsector.SelectedValue != "0")
                {
                    //BindSubSector(ddlsector.SelectedValue);
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
                 
                    FileUploadFourthupd.Enabled = false;
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
    protected void drpCorCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cntid = Convert.ToInt32(drpCorCountry.SelectedValue);
        BindState(cntid);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strProposalNo = TextBox1.Text;
        Response.Redirect("EditProposalDetails.aspx?StrPropNo=" + TextBox1.Text);
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
}
