#region Namespaces
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
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
#endregion

public partial class ProposalDetails : System.Web.UI.Page
{

    ProposalBAL objService = new ProposalBAL();
    PromoterDet objPromoDet = new PromoterDet();
    LandDet objLandDet = new LandDet();
    ProposalDet ObjPropasal = new ProposalDet();
    InvestorBusinessLayer objservice = new InvestorBusinessLayer();
    InvestorDetails objInvestor = new InvestorDetails();
    EncryptDecryptQueryString objEncrypt = new EncryptDecryptQueryString();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["StrPropNo"] != null)
            {
                GetCompanyInfo();
                GetLandDetails();
                GetEnclosureList();
                getProjectInfo();
                GetQueryDetails();
                RepeterData();
                GetProductDetails();
                //GetDeptRemarks();
            }
        }
    }
    private void GetProductDetails()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "M2";
            objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProjList = objService.getProductNameDetails(objProp).ToList();
            grdProduct.DataSource = objProjList;
            grdProduct.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }
    private void GetCompanyInfo()
    {
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "V";
            objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            List<PromoterDet> objProposalList = objService.ViewCompanyInformation(objPromoDet).ToList();
            if (objProposalList.Count > 0)
            {
                lblCompName.Text = objProposalList[0].vchCompName.ToString();
                lblAddress.Text = objProposalList[0].vchAddress.ToString();
                lblCountry.Text = objProposalList[0].vchCountryName.ToString();
                if (lblCountry.Text == "India")
                {
                    lblState.Text = objProposalList[0].vchStateName.ToString();
                }
                else
                {
                    lblState.Text = objProposalList[0].vchOtherState.ToString();
                }
                lblCity.Text = objProposalList[0].vchCity.ToString();
                if (objProposalList[0].vchPhoneNo.ToString()!="")
                {
                    lblISDPHNo.Text = objProposalList[0].VCHISDPHNo.ToString();
                    lblPhoneNo.Text = objProposalList[0].vchPhoneNo.ToString();
                    lblPhoneStateCode.Text = objProposalList[0].PhoneStateCode.ToString();
                }
                else
                {
                    lblISDPHNo.Text = "";
                    lblPhoneNo.Text = "";
                    lblPhoneStateCode.Text = "";
                }
                if (objProposalList[0].vchFaxNo.ToString() != "")
                {
                    lblFaxNo.Text = objProposalList[0].vchFaxNo.ToString();
                    lblISDFXNo.Text = objProposalList[0].VCHISDFXNo.ToString();
                }
                else
                {
                    lblFaxNo.Text = "";
                    lblISDFXNo.Text = "";
                }
                lblEmail.Text = objProposalList[0].vchEmail.ToString();
                lblPin.Text = objProposalList[0].intPin.ToString();

                //Correspondence address

                lblContactPerson.Text = objProposalList[0].vchContactPerson.ToString();
                lblCorAdd.Text = objProposalList[0].vchCorAdd.ToString();
                lblCorCountry.Text = objProposalList[0].vchCorCountryName.ToString();
                if (lblCorCountry.Text == "India")
                {
                    lblCorState.Text = objProposalList[0].vchCorStateName.ToString();
                }
                else
                {
                    lblCorState.Text = objProposalList[0].vchOtherStateCor.ToString();
                }
                
                lblCorCity.Text = objProposalList[0].vchCorCity.ToString();
                lblISDMOB.Text = objProposalList[0].VCHISDMOBo.ToString();
                lblCorMobileNo.Text = objProposalList[0].vchCorMobileNo.ToString();

                if (objProposalList[0].vchCorFaxNo.ToString() != "")
                {
                    lblFaxCordet.Text = objProposalList[0].VCHISDFAXCor.ToString();
                    lblCorFaxNo.Text = objProposalList[0].vchCorFaxNo.ToString();
                }
                else
                {
                    lblFaxCordet.Text = "";
                    lblCorFaxNo.Text = "";
                }
                lblCorEmail.Text = objProposalList[0].vchCorEmail.ToString();
                lblCorrPin.Text = objProposalList[0].intCorPin.ToString();
                lblOtheConstituition.Text = objProposalList[0].vchConstitution.ToString();
                if (lblOtheConstituition.Text == "Others")
                {
                    dvConstothers.Visible = true;
                    lblOthers.Text = objProposalList[0].vchOtheConstituition.ToString();
                    dvBoard.Visible = true;
                    GetPromoDesignation();
                    DVC1.Visible = true;
                    DVC2.Visible = true;
                    DVC3.Visible = true;
                    DVC4.Visible = true;
                }
                else
                {
                    dvConstothers.Visible = false;
                    dvBoard.Visible = false;
                }
                if (lblOtheConstituition.Text == "Proprietorship")
                {
                    dvPromoter.Visible = true;
                    lblNameOfPromoter.Text = objProposalList[0].vchNameOfPromoter.ToString();
                    DVC1.Visible = false;
                    DVC2.Visible = false;
                    DVC3.Visible = false;
                    DVC4.Visible = false;
                }
                else
                {
                    dvPromoter.Visible = false;
                }
                //if (lblOtheConstituition.Text == "Private Limited Company")
                //{
                //    dvBoard.Visible = true;
                //    GetPromoDesignation();
                //}
                //else
                //{
                //    dvBoard.Visible = false;
                //}

                if ((lblOtheConstituition.Text == "Proprietorship") || (lblOtheConstituition.Text == "Partnership"))
                {
                    dvBoard.Visible = false;
                }
                else
                {

                    dvBoard.Visible = true;
                    GetPromoDesignation();
                }
                if (lblOtheConstituition.Text == "Partnership")
                {
                    dvPartnership.Visible = true;
                    lblNumberOfPartner.Text = objProposalList[0].intNumberOfPartner.ToString();
                    lblManagPartner.Text = objProposalList[0].vchManagPartner.ToString();
                    DVC1.Visible = false;
                    DVC2.Visible = false;
                    DVC3.Visible = false;
                    DVC4.Visible = true;
                }
                else
                {
                    dvPartnership.Visible = false;
                }
                if (lblOtheConstituition.Text == "SPV")
                {
                    DVC1.Visible = true;
                    DVC2.Visible = true;
                    DVC3.Visible = false;
                    DVC4.Visible = true;
                }
                if (lblOtheConstituition.Text == "Co-operative")
                {
                    DVC1.Visible = true;
                    DVC2.Visible = true;
                    DVC3.Visible = false;
                    DVC4.Visible = true;
                }
                lblExperience.Text = objProposalList[0].intExpInYr.ToString();

                ////Company Reg details
                lblYearIncorp.Text = objProposalList[0].intYearOfIncorporation.ToString();
                lblPlaceIncor.Text = objProposalList[0].vchPlaceIncor.ToString();
                lblGSTIN.Text = objProposalList[0].vchGSTIN.ToString();

                hdnProjType.Value = objProposalList[0].intProjectType.ToString();
                if (hdnProjType.Value == "1")
                {
                    lblProjType.Text = "Project Cost >= 50 crore";
                    dvPrjMSME.Visible = false;
                    dvmaterial.Visible = false;
                    dvIRR.Visible = false;
                }
                else
                {
                    lblProjType.Text = "Project cost upto < 50 crore";
                    GetRawMaterialDetails();
                    dvPrjMSME.Visible = true;
                    dvmaterial.Visible = true;
                    dvIRR.Visible = true;
                }
                hdnApplicationFor.Value = objProposalList[0].intApplicationFor.ToString();
                if (hdnApplicationFor.Value == "1")
                {
                    lblApplicationFor.Text = "New Unit";
                }
                else
                {
                    lblApplicationFor.Text = "Expansion of existing unit";
                }
                if (lblApplicationFor.Text == "Expansion of existing unit")
                {
                    dvExistingUnit.Visible = true;
                }
                else
                {
                    dvExistingUnit.Visible = false;
                }
                if (hdnProjType.Value == "2" && hdnApplicationFor.Value == "1")
                {
                    dvNetWorth.Visible = true;
                }
                else
                {
                    dvNetWorth.Visible = false;
                }
                hdnFinYear1.Value = objProposalList[0].intFyn1.ToString();
                int FinYear1 = Convert.ToInt32(hdnFinYear1.Value);
                if (FinYear1 > 0)
                {
                    int FinYearN1 = FinYear1 + 1;
                    lblFinYear1.Text = FinYear1 + "-" + FinYearN1;
                }
                else
                {
                    lblFinYear1.Text = "NA";
                }
               
                hdnFinYear2.Value = objProposalList[0].intFyn2.ToString();
                int FinYear2 = Convert.ToInt32(hdnFinYear2.Value);
                if (FinYear2 > 0)
                {
                    int FinYearN2 = FinYear2 + 1;
                    lblFinYear2.Text = FinYear2 + "-" + FinYearN2;
                }
                else
                {
                    lblFinYear2.Text = "NA";
                }
                hdnFinYear3.Value = objProposalList[0].intFyn3.ToString();
                int FinYear3=Convert.ToInt32(hdnFinYear3.Value);
                if (FinYear3 > 0)
                {
                    int FinYearN3 = FinYear3 + 1;
                    lblFinYear3.Text = FinYear3 + "-" + FinYearN3;
                }
                else
                {
                    lblFinYear3.Text = "NA";
                }
                
                lblAnnlCrntYr.Text = objProposalList[0].decAnnulTurnOvr1.ToString();
                lblAnnlLastYr.Text = objProposalList[0].decAnnulTurnOvr2.ToString();
                lblAnnlPrevToLastYr.Text = objProposalList[0].decAnnulTurnOvr3.ToString();
                lblPftBTCrntYr.Text = objProposalList[0].decProfitAftrTx1.ToString();
                lblPftBTLastYr.Text = objProposalList[0].decProfitAftrTx2.ToString();
                lblPftBTPrevToLastYr.Text = objProposalList[0].decProfitAftrTx3.ToString();
                lblNWCrntYr.Text = objProposalList[0].decNetWorth1.ToString();
                lblNWLastYr.Text = objProposalList[0].decNetWorth2.ToString();
                lblNWPrevTolastyr.Text = objProposalList[0].decNetWorth3.ToString();
                lblRSCrntyr.Text = objProposalList[0].decResvSurp1.ToString();
                lblRSLastYr.Text = objProposalList[0].decResvSurp2.ToString();
                lblRSPrevTolastYr.Text = objProposalList[0].decResvSurp3.ToString();
                lblSCCrntYr.Text = objProposalList[0].decShareCap1.ToString();
                lblSCLastYr.Text = objProposalList[0].decShareCap2.ToString();
                lblSCPrevToLastYr.Text = objProposalList[0].decShareCap3.ToString();
                hdnQ.Value = objProposalList[0].intEduQualif.ToString();
                if (hdnQ.Value == "1")
                {
                    lblEduQualif.Text = "10th";
                }
                else if (hdnQ.Value == "2")
                {
                    lblEduQualif.Text = "+2/Intermediate";
                }
                else if (hdnQ.Value == "3")
                {
                    lblEduQualif.Text = "Graduation";
                }
                else if (hdnQ.Value == "4")
                {
                    lblEduQualif.Text = "Post Graduation and Above";
                }
                else if (hdnQ.Value == "5")
                {
                    lblEduQualif.Text = "Under Matric";
                }
              
                hdnTechQ.Value = objProposalList[0].intTecQualif.ToString();
                if (hdnTechQ.Value == "1")
                {
                    lblTechQual.Text = "ITI";
                }
                else if (hdnTechQ.Value == "2")
                {
                    lblTechQual.Text = "Diploma";
                }
                else if (hdnTechQ.Value == "3")
                {
                    lblTechQual.Text = "BE/B.Tech";
                }
                else if (hdnTechQ.Value == "4")
                {
                    lblTechQual.Text = "MCA";
                }
                else if (hdnTechQ.Value == "5")
                {
                    lblTechQual.Text = "MBA";
                }
                else if (hdnTechQ.Value == "6")
                {
                    lblTechQual.Text = "Non Technical";
                }
                lblExistInd.Text = objProposalList[0].vchExisIndName.ToString();
                lblDistrictName.Text = objProposalList[0].vchDistrictName.ToString();
                lblBlock.Text = objProposalList[0].vchBlockName.ToString();
                if (objProposalList[0].intAllotedBy.ToString() == "1")
                {
                    lblLandAllotIDCO.Text = "Yes";
                }
                else
                {
                    lblLandAllotIDCO.Text = "No";
                }

                lblExtentLand.Text = objProposalList[0].vchlandInAcres.ToString();
                lblNatActivity.Text = objProposalList[0].vchNatureAct.ToString();
                lblSector.Text = objProposalList[0].vchSector.ToString();
                lblSubSector.Text = objProposalList[0].vchSubSector.ToString();
                lblCapacity.Text = objProposalList[0].vchCapacity.ToString();
                lblUnitCapacity.Text = objProposalList[0].vchCapacityUnit.ToString();
                if (objProposalList[0].vchCapacityUnit.ToString() == "Others")
                {
                    dvOthrs.Visible = true;
                    lblUnitCapacity.Visible = false;
                    lblOtherspecify.Text = objProposalList[0].vchOther.ToString();
                }
                else
                {
                    dvOthrs.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objPromoDet = null;
        }
    }

    private void GetRawMaterialDetails()
    {
        List<PromoterDet> objProposalList = new List<PromoterDet>();
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "G";
            objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProposalList = objService.GetRawMetrialDetails(objPromoDet).ToList();
            grdRawMaterials.DataSource = objProposalList;
            grdRawMaterials.DataBind();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objPromoDet = null;
        }
    }
   
    private void GetPromoDesignation()
    {
        List<PromoterDet> objProposalList = new List<PromoterDet>();
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "F";
            objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProposalList = objService.GetNameDesgDetails(objPromoDet).ToList();
            GrdDesignation.DataSource = objProposalList;
            GrdDesignation.DataBind();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objPromoDet = null;
        }
    }
    private void GetEnclosureList()
    {
        List<PromoterDet> objProposalList = new List<PromoterDet>();
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "H";
            objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProposalList = objService.GetEnclosureDetails(objPromoDet).ToList();
            if (objProposalList.Count > 0)
            {
                if (Convert.ToString(objProposalList[0].vchPanfile) != "")
                {
                    hdnPanFile.Value = Convert.ToString(objProposalList[0].vchPanfile);
                    hplnkPan.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchPanfile);
                }
                if (Convert.ToString(objProposalList[0].vchGSTNfile) != "")
                {
                    hdnGstinFile.Value = Convert.ToString(objProposalList[0].vchGSTNfile);
                    hplnkGstin.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchGSTNfile);
                }
                if (Convert.ToString(objProposalList[0].vchMemorandumfile) != "")
                {
                    hdnMemoFile.Value = Convert.ToString(objProposalList[0].vchMemorandumfile);
                    hplnkMemo.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchMemorandumfile);
                }
                if (Convert.ToString(objProposalList[0].vchCertificateincorpfile) != "")
                {
                    hdnCerti.Value = Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                    hplnkCerti.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                }
                if (Convert.ToString(objProposalList[0].vchEduQualifile) != "")
                {
                    hdnEdu.Value = Convert.ToString(objProposalList[0].vchEduQualifile);
                    hplnkEdu.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchEduQualifile);
                }
                if (Convert.ToString(objProposalList[0].vchTechniQualifile) != "")
                {
                    hdnTecnical.Value = Convert.ToString(objProposalList[0].vchTechniQualifile);
                    hplnkTechQ.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchTechniQualifile);
                }
                if (Convert.ToString(objProposalList[0].vchExpFile) != "")
                {
                    hdnExperience.Value = Convert.ToString(objProposalList[0].vchExpFile);
                    hplnkExperience.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchExpFile);
                }
                if (Convert.ToString(objProposalList[0].vchAuditFile) != "")
                {
                    hdnAudit.Value = Convert.ToString(objProposalList[0].vchAuditFile);
                    hplnkAudit.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFile);

                }
                if (Convert.ToString(objProposalList[0].vchAuditFileSecondYrs) != "")
                {
                    hdnFySecond.Value = Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);
                    hplnkFySecond.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);

                }
                if (Convert.ToString(objProposalList[0].vchAuditFileThrdYrs) != "")
                {
                    hdnFyThird.Value = Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);
                    hplnkFyThird.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);

                }
                if (Convert.ToString(objProposalList[0].vchNetWorthfile) != "")
                {
                    hdnNetWorth.Value = Convert.ToString(objProposalList[0].vchNetWorthfile);
                    hplnkNetWorth.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchNetWorthfile);
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objPromoDet = null;
        }
    }

    private void getProjectInfo()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        try
        {
            objProp.strAction = "V";
            objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProjList = objService.getProjectInfoDetails(objProp).ToList();
            if (objProjList.Count > 0)
            {
                lblUnit.Text = Convert.ToString(objProjList[0].vchNameOfUnit);
                hdnEin.Value = Convert.ToString(objProjList[0].intEinNoderr);
                if (hdnEin.Value == "1")
                {
                    lblEin.Text = "EIN :";
                    lblEIM.Text = "EIN :";
                }
                else if (hdnEin.Value == "2")
                {
                    lblEin.Text = "IEM :";
                    lblEIM.Text = "IEM :";
                }
                else if (hdnEin.Value == "3")
                {
                    lblEin.Text = "IL :";
                    lblEIM.Text = "IL :";
                }
                else if (hdnEin.Value == "4")
                {
                    lblEin.Text = "Udyog Aadhaar :";
                    lblEIM.Text = "Udyog Aadhaar :";
                }
                lblEINIEM.Text = Convert.ToString(objProjList[0].vchEINnIEMnIL);
                lblSectorActivity.Text = Convert.ToString(objProjList[0].vchSectorName);
                lblSubSect.Text = Convert.ToString(objProjList[0].vchSubSectorName);
                lblPropAnnual.Text = Convert.ToString(objProjList[0].decProposedAnnualCapacity);
                hdnAnnualUnit.Value = Convert.ToString(objProjList[0].vchUnit);

                lblAnnualUnit.Text = Convert.ToString(objProjList[0].CapacityUnit);
                if (Convert.ToString(objProjList[0].CapacityUnit) == "Other")
                {
                    lblAnnualUnit.Text = Convert.ToString(objProjList[0].vchOtherUnit);
                }
                //else
                //{
                //    lblAnnualUnit.Visible = false;
                //}
                
                //hdnAnnualUnit.Value = Convert.ToString(objProjList[0].vchUnit);
                //if (hdnAnnualUnit.Value == "1")
                //{
                //    lblAnnualUnit.Text = "MT";
                //}
                //else if (hdnAnnualUnit.Value == "2")
                //{
                //    lblAnnualUnit.Text = "Litre";
                //}
                //else if (hdnAnnualUnit.Value == "3")
                //{ lblAnnualUnit.Text = "KG"; }
                //else
                //{
                //    lblAnnualUnit.Text = "Other";
                //}
                
                lblLandincludinglanddevelopment.Text = Convert.ToString(objProjList[0].decLandIncLandDev);
                lblBuildingCivilConstruction.Text = Convert.ToString(objProjList[0].decBuildingndConstruction);
                lblPlantMachinery.Text = Convert.ToString(objProjList[0].decPlantndMachinery);
                lblOthersProj.Text = Convert.ToString(objProjList[0].decOthers);
                lblCapitalInvestment.Text = Convert.ToString(objProjList[0].decTotCapitalInvestment);
                lblPerCommence.Text = Convert.ToString(objProjList[0].intPeriodToCommenceProduction);
                hdnProjComing.Value = Convert.ToString(objProjList[0].vchProjectComingUnder);
                if (hdnProjComing.Value == "1")
                {
                    lblProjectComing.Text = "Yes";
                }
                else
                {
                    lblProjectComing.Text = "No";
                }
                
                hdnPolCat.Value = Convert.ToString(objProjList[0].vchPollutionCategory);
                if (hdnPolCat.Value == "1")
                {
                    lblPolCat.Text = "White";
                }
                else if (hdnPolCat.Value == "2")
                {
                    lblPolCat.Text ="Green";
                }
                else if (hdnPolCat.Value == "3")
                {
                   lblPolCat.Text ="Orange";
                }
                else if (hdnPolCat.Value == "4")
                {
                    lblPolCat.Text = "Red";
                }
                lblGround.Text = Convert.ToString(objProjList[0].intGroundBreaking);
                lblCivilstructural.Text = Convert.ToString(objProjList[0].intCivilndStructuralCompln);
                lblEquipment.Text = Convert.ToString(objProjList[0].intMajorEquipmentErect);
                lblCommercial.Text = Convert.ToString(objProjList[0].intStartOfCommercialProd);
                lblEquity.Text = Convert.ToString(objProjList[0].decEquityContribution);
                lblBankFin.Text = Convert.ToString(objProjList[0].decBankndInstitutionalFin);
                lblTotal.Text = Convert.ToString(objProjList[0].decTotFinance);
                lblFDI.Text = Convert.ToString(objProjList[0].decForeignInvestment);
                lblIRR.Text = Convert.ToString(objProjList[0].vchIRR);
                lblDSCR.Text = Convert.ToString(objProjList[0].vchDSCR);
                lblManagerExist.Text = Convert.ToString(objProjList[0].intMangerExist);
                lblManagerProposed.Text = Convert.ToString(objProjList[0].intManagerProp);
                lblSupervisorExist.Text = Convert.ToString(objProjList[0].intSupervisorExist);
                lblSupervisorProposed.Text = Convert.ToString(objProjList[0].intSupervisorProp);
                lblSkilledExist.Text = Convert.ToString(objProjList[0].intSkilledExist);
                lblSkilledProposed.Text = Convert.ToString(objProjList[0].intSkilledProp);
                lblSemiskilledExist.Text = Convert.ToString(objProjList[0].intSemiSkilledExist);
                lblSemiskilledProposed.Text = Convert.ToString(objProjList[0].intSemiSkilledProp);
                lblUnskilledExist.Text = Convert.ToString(objProjList[0].intUnSkilledExist);
                lblUnskilledProposed.Text = Convert.ToString(objProjList[0].intUnSkilledProp);
                lblTotalExist.Text = Convert.ToString(objProjList[0].intTotalExist);
                lblTotalProposed.Text = Convert.ToString(objProjList[0].intTotalProp);
                lblDirectEmp.Text = Convert.ToString(objProjList[0].intPropDirectEmployment);
                lblContractualEmp.Text = Convert.ToString(objProjList[0].intPropContractualEmployment);

                if (Convert.ToString(objProjList[0].vchIndustryInterprenur) != "")
                {
                    hdnIndustryEntMemorandum.Value = Convert.ToString(objProjList[0].vchIndustryInterprenur);
                    hplnkIndustryEntMemorandum.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchIndustryInterprenur);
                }

                if (Convert.ToString(objProjList[0].vchManufacturingProcessFlow) != "")
                {
                    hdnFileMnfprocess.Value = Convert.ToString(objProjList[0].vchManufacturingProcessFlow);
                    hplnkFileMnfprocess.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchManufacturingProcessFlow);

                }

                if (Convert.ToString(objProjList[0].vchFeasibilityReport) != "")
                {
                    hdnFeasibilityReport.Value = Convert.ToString(objProjList[0].vchFeasibilityReport);
                    hplnkFeasibilityReport.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchFeasibilityReport);
                }


                if (Convert.ToString(objProjList[0].vchBoardResolution) != "")
                {
                    hdnBoardResolution.Value = Convert.ToString(objProjList[0].vchBoardResolution);
                    hplnkBoardResolution.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchBoardResolution);

                }

                if (Convert.ToString(objProjList[0].vchSourceOfFinance) != "")
                {
                    hdnOtherFin.Value = Convert.ToString(objProjList[0].vchSourceOfFinance);
                    hplnkOtherFin.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchSourceOfFinance);
                }

            }

            objProp.strAction = "E1";
            objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProjList = objService.getProjectLOCDetails(objProp).ToList();
            gvLOCDetails.DataSource = objProjList;
            gvLOCDetails.DataBind();
            if (objProjList.Count > 0)
            {
                lblProjectsLocation.Text = "Yes";
                dvLocDetIndia.Visible = true;
            }
            else
            {
                lblProjectsLocation.Text = "No";
                dvLocDetIndia.Visible = false;
            }

            objProp.strAction = "E2";
            objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProjList = objService.getOtherUnitlDetails(objProp).ToList();
            gvOtherUnits.DataSource = objProjList;
            gvOtherUnits.DataBind();
            if (objProjList.Count > 0)
            {
                lblUnitOutSide.Text="Yes";
                dvLocDetOutInd.Visible = true;
            }
            else
            {
                lblUnitOutSide.Text = "No";
                dvLocDetOutInd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objProp = null;
        }

    }

    private void GetLandDetails()
    {
        objLandDet = new LandDet();
        try
        {
            objLandDet.strAction = "V";
            objLandDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            List<LandDet> objProposalList = objService.ViewLandUtility(objLandDet).ToList();
            if (objProposalList.Count > 0)
            {
                hdnLandReq.Value = objProposalList[0].bitLandRequired.ToString();
                if (hdnLandReq.Value == "True")
                {
                    lblLandRequired.Text = "Yes";
                    dvLandReq.Visible = true;
                    
                }
                else
                {
                    lblLandRequired.Text = "No";
                    dvLandReq.Visible = false;
                }
                lblDistrictLand.Text = objProposalList[0].nvchDistrictName.ToString();
                lblBlockLand.Text = objProposalList[0].vchBlockName.ToString();
                lblExtentLandReq.Text = objProposalList[0].decExtendLand.ToString();
                if(objProposalList[0].sintLandRequiredIDCO.ToString()=="1")
                {
                    dvIDCOName.Visible = true;
                    lblLandrequiredIDCO.Text = "Yes";
                    lblIDCOName.Text = objProposalList[0].vchIDCOInustrial.ToString();
                }
                else
                {
                    dvIDCOName.Visible = false;
                    lblLandrequiredIDCO.Text = "No";
                }
                if (objProposalList[0].sintLandAcquiredIDCO.ToString() == "1")
                {
                    lbllandacquired.Text = "Yes";
                }
                else
                {
                    lbllandacquired.Text = "No";
                }
                lblLoadGrid.Text = objProposalList[0].decPowerDemandGrid.ToString();
                
                lblLoadCPP.Text = objProposalList[0].decPowerDrawalCPP.ToString();
                lblCPPCapacity.Text = objProposalList[0].decCapacityofCPPPlant.ToString();
                lblWaterRequireExist.Text = objProposalList[0].decWaterRequireExist.ToString();
                lblWaterReqireProposed.Text = objProposalList[0].decWaterReqireProposed.ToString();
                lblWaterReq.Text = objProposalList[0].decWaterRequirProduct.ToString();
                lblQuantum.Text = objProposalList[0].vchQuntRecyllingWaste.ToString();
                
                if (objProposalList[0].vchSurfaceWater.ToString() != "" && objProposalList[0].vchIdcoSupply.ToString() != "")
                {
                    lblSurface.Text = objProposalList[0].vchSurfaceWater.ToString() + ",";
                }
                else
                {
                    lblSurface.Text = objProposalList[0].vchSurfaceWater.ToString();
                }

                if (objProposalList[0].vchIdcoSupply.ToString() != "" && objProposalList[0].vchRainWtrHarvesting.ToString() != "")
                {
                    lblIdcoSupply.Text = objProposalList[0].vchIdcoSupply.ToString() + ",";
                }
                else
                {
                    lblIdcoSupply.Text = objProposalList[0].vchIdcoSupply.ToString();
                }
                if (objProposalList[0].vchRainWtrHarvesting.ToString() != "" && objProposalList[0].vchOtherSpecify.ToString() != "")
                {
                    lblRainWtrHarvesting.Text = objProposalList[0].vchRainWtrHarvesting.ToString() + ",";
                }
                else
                {
                    lblRainWtrHarvesting.Text = objProposalList[0].vchRainWtrHarvesting.ToString();
                }
                lblsourceOther.Text = objProposalList[0].vchOtherSpecify.ToString();
                lblother.Text = objProposalList[0].vchother.ToString();

                if (Convert.ToString(objProposalList[0].vchWasteConserFile) != "")
                {
                    hdnWaterFile.Value = Convert.ToString(objProposalList[0].vchWasteConserFile);
                    hplnkWaterFile.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchWasteConserFile);
                }
                if (Convert.ToString(objProposalList[0].vchWaterHazardousFile) != "")
                {
                    hdnHazardousFile.Value = Convert.ToString(objProposalList[0].vchWaterHazardousFile);
                    hplnkHazardousFile.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchWaterHazardousFile);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objPromoDet = null;
        }

    }
    public void RepeterData()
    {
        try
        {
            con.Open();
            cmd = new SqlCommand("SELECT  A.VCH_APPLICATION_UNQ_KEY, A.INT_SERVICEID, B.VCH_SERVICENAME FROM T_APPLICATION_TBL A INNER JOIN M_SERVICEMASTER_TBL B ON A.INT_SERVICEID = B.INT_SERVICEID WHERE VCH_PROPOSALID='" + Request.QueryString["StrPropNo"].ToString() + "' ORDER BY VCH_SERVICENAME", con);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            rptCustomers.DataSource = ds;
            rptCustomers.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
       
    }
    private void GetQueryDetails()
    {
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        ObjPropasal = new ProposalDet();
        try
        {
            ObjPropasal.strAction = "F";
            ObjPropasal.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            ObjPropasal.strProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProposalList = objService.getRaisedQueryDetails(ObjPropasal).ToList();
            grdQuery.DataSource = objProposalList;
            grdQuery.DataBind();
         

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");

        }
        finally
        {
            objPromoDet = null;
        }
    }


    //private void GetDeptRemarks()
    //{
    //    objInvestor.strAction = "VI";

    //    List<InvestorDetails> objList = objservice.ViewInvestorDetailsPortal(objInvestor).ToList();
    //    lblDeptRemarks.Text = objList[0].StrRemarks.ToString();
    //}
    protected void grdQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string[] strFileName = grdQuery.DataKeys[e.Row.RowIndex].Values[0].ToString().Split(',');
            HiddenField hdnFile = (HiddenField)e.Row.FindControl("hdnFile");
            for (int i = 0; i <= Convert.ToInt32(strFileName.Length) - 1; i++)
            {
                hdnFile.Value = strFileName[i];
                HyperLink hprlnkQuery = new HyperLink();
                hprlnkQuery.ID = "DynLink"; 
                hprlnkQuery.Text = "<i class='fa fa-download' aria-hidden='true'></i>";
                hprlnkQuery.ToolTip = hdnFile.Value;
                hprlnkQuery.Target = "_Blank";
                hprlnkQuery.NavigateUrl = "~/QueryFiles/" + hdnFile.Value;
                
                e.Row.Cells[3].Controls.Add(hprlnkQuery);
                if (hdnFile.Value != "")
                {
                    hprlnkQuery.Visible = true;
                }
                else
                {
                    hprlnkQuery.Visible = false;
                }
            }
            
        }
    }
    protected void rptCustomers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string AppID = ((Label)e.Item.FindControl("lblApplicationKey")).Text;
            int IntServiceID = Convert.ToInt32(((HiddenField)e.Item.FindControl("lblDeptRemarks")).Value);
            HtmlGenericControl dvService = e.Item.FindControl("DvServiceContent") as HtmlGenericControl;
            dvService.InnerHtml = GetServiceDetailsContent(AppID, IntServiceID).ToString();
        }
    }
    StringWriter GetServiceDetailsContent(string AppID, int IntServiceID)
    {
        StringWriter writer = new StringWriter();
        //Server.Execute("~/AppDetails.aspx?ApplicationNo=" + AppID + "&ServiceId=" + IntServiceID.ToString(), writer);
        return writer;
    }
}