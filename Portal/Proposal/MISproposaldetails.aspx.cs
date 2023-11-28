using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using System.ServiceModel;
using BusinessLogicLayer.Proposal;
using System.Data;

public partial class Portal_Proposal_MISproposaldetails : SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();

    PromoterDet objPromoDet = new PromoterDet();
    LandDet objLandDet = new LandDet();
    ProposalDet ObjPropasal = new ProposalDet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../SessionRedirect.aspx");
            }
            else
            {
                try
                {
                    if (Request.QueryString["Pno"] != null)
                    {
                        GetCompanyInfo();
                        GetGroupOfCompanyDetails();//// Added by Sushant Jena On Dt:-19-Feb-2020
                        GetLandDetails();
                        GetEnclosureList();
                        getProjectInfo();
                        GetQueryDetails();
                        GetActionDetails(); //Satya added
                        GetProductDetails();
                    }
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "MISProposalDetails");
                }
            }
        }
    }

    #region FunctionUsed

    private void GetProductDetails()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "M2";
            objProp.vchProposalNo = Request.QueryString["Pno"].ToString();
            objProjList = objService.getProductNameDetails(objProp).ToList();
            grdProduct.DataSource = objProjList;
            grdProduct.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetCompanyInfo()
    {
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "V";
            objPromoDet.vchProposalNo = Request.QueryString["Pno"].ToString();
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
                if (objProposalList[0].vchPhoneNo.ToString() != "")
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
                int FinYear3 = Convert.ToInt32(hdnFinYear3.Value);
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
            throw ex;
        }
        finally
        {
            objPromoDet = null;
        }
    }
    /// <summary>
    /// To get Group of Company Net Worth Details
    /// Added by Sushant Jena On Dt:-27-Aug-2019
    /// Action code "GCNW" specifies Group of Comapny Net Worth
    /// </summary>
    private void GetGroupOfCompanyDetails()
    {
        ProjectInfo objProjInfo = new ProjectInfo();
        try
        {
            objProjInfo.strAction = "GCNW";
            objProjInfo.vchProposalNo = Request.QueryString["Pno"].ToString();

            DataTable dt = new DataTable();
            dt = objService.GetGcNewWorthDetails(objProjInfo);
            if (dt.Rows.Count > 0)
            {
                Grd_GC_Net_Worth.DataSource = dt;
                Grd_GC_Net_Worth.DataBind();
                divGroupOfCompany.Visible = true;
            }
            else
            {
                divGroupOfCompany.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProjInfo = null;
        }
    }
    private void GetRawMaterialDetails()
    {
        List<PromoterDet> objProposalList = new List<PromoterDet>();
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "G";
            objPromoDet.vchProposalNo = Request.QueryString["Pno"].ToString();
            objProposalList = objService.GetRawMetrialDetails(objPromoDet).ToList();
            grdRawMaterials.DataSource = objProposalList;
            grdRawMaterials.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
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
            objPromoDet.vchProposalNo = Request.QueryString["Pno"].ToString();
            objProposalList = objService.GetNameDesgDetails(objPromoDet).ToList();
            GrdDesignation.DataSource = objProposalList;
            GrdDesignation.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
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
            objPromoDet.vchProposalNo = Request.QueryString["Pno"].ToString();
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
            throw ex;
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
            objProp.vchProposalNo = Request.QueryString["Pno"].ToString();
            objProjList = objService.getProjectInfoDetails(objProp).ToList();
            if (objProjList.Count > 0)
            {
                lblUnit.Text = Convert.ToString(objProjList[0].vchNameOfUnit);

                hdnEin.Value = Convert.ToString(objProjList[0].intEinNoderr);
                if (hdnEin.Value == "1")
                {
                    lblEin.Text = "EIN";
                    lblEIM.Text = "EIN";
                }
                else if (hdnEin.Value == "2")
                {
                    lblEin.Text = "IEM";
                    lblEIM.Text = "IEM";
                }
                else if (hdnEin.Value == "3")
                {
                    lblEin.Text = "IL";
                    lblEIM.Text = "IL";
                }
                else if (hdnEin.Value == "4")
                {
                    lblEin.Text = "Udyog Aadhar";
                    lblEIM.Text = "Udyog Aadhar";
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
                    lblPolCat.Text = "Green";
                }
                else if (hdnPolCat.Value == "3")
                {
                    lblPolCat.Text = "Orange";
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

            /*-----------------------------------------------------*/

            objProp.strAction = "E1";
            objProp.vchProposalNo = Request.QueryString["Pno"].ToString();
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

            /*-----------------------------------------------------*/

            objProp.strAction = "E2";
            objProp.vchProposalNo = Request.QueryString["Pno"].ToString();
            objProjList = objService.getOtherUnitlDetails(objProp).ToList();
            gvOtherUnits.DataSource = objProjList;
            gvOtherUnits.DataBind();
            if (objProjList.Count > 0)
            {
                lblUnitOutSide.Text = "Yes";
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
            throw ex;
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
            objLandDet.vchProposalNo = Request.QueryString["Pno"].ToString();
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
                //LblLandUnit.Text = " (in " + objProposalList[0].strLandUnit.ToString() + ")";//// Added by Sushant Jena On Dt:-18-Feb-2020


                if (objProposalList[0].sintLandRequiredIDCO.ToString() == "1") //// Land Required from IDCO 
                {
                    dvIDCOName.Visible = true;
                    dvLandAcquired.Visible = false; ////Added by Bhagyashree Das on Dt:-28-Jan-2021

                    lblLandrequiredIDCO.Text = "Yes";
                    lblIDCOName.Text = objProposalList[0].vchIDCOInustrial.ToString();
                }
                else
                {
                    dvIDCOName.Visible = false;
                    dvLandAcquired.Visible = true;

                    lblLandrequiredIDCO.Text = "No";
                    lbllandacquired.Text = (objProposalList[0].sintLandAcquiredIDCO.ToString() == "1") ? "Yes" : "No";
                }

                lblLoadGrid.Text = objProposalList[0].decPowerDemandGrid.ToString();
                lblLoadCPP.Text = objProposalList[0].decPowerDrawalCPP.ToString();
                lblCPPCapacity.Text = objProposalList[0].decCapacityofCPPPlant.ToString();
                LblPowerProducerIPP.Text = objProposalList[0].DecPowerProducerIpp.ToString(); /////Added by Sushant Jena On Dt:-25-Aug-2021

                lblWaterRequireExist.Text = objProposalList[0].decWaterRequireExist.ToString();
                lblWaterReqireProposed.Text = objProposalList[0].decWaterReqireProposed.ToString();
                lblWaterReq.Text = objProposalList[0].decWaterRequirProduct.ToString();
                lblQuantum.Text = objProposalList[0].vchQuntRecyllingWaste.ToString();

                /*-----------------------------------------------------------------------------------*/

                string strWaterSource = "";

                if (objProposalList[0].vchSurfaceWater.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchSurfaceWater.ToString() + ", ";
                    //lblSurface.Text = objProposalList[0].vchSurfaceWater.ToString() + ",";
                }

                if (objProposalList[0].vchIdcoSupply.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchIdcoSupply.ToString() + ", ";
                    // lblIdcoSupply.Text = objProposalList[0].vchIdcoSupply.ToString() + ",";
                }

                if (objProposalList[0].vchRainWtrHarvesting.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchRainWtrHarvesting.ToString() + ", ";
                    // lblRainWtrHarvesting.Text = objProposalList[0].vchRainWtrHarvesting.ToString() + ",";
                }

                if (objProposalList[0].vchother.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchother.ToString() + ", ";
                    strWaterSource = strWaterSource + objProposalList[0].vchOtherSpecify.ToString() + ", ";
                    // lblother.Text = objProposalList[0].vchother.ToString() + ",";
                    // lblsourceOther.Text = objProposalList[0].vchOtherSpecify.ToString();
                }

                strWaterSource = strWaterSource.Trim().TrimEnd(',');

                lblSurface.Text = strWaterSource;

                /*-----------------------------------------------------------------------------------*/


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
                HdnLandRequr.Value = objProposalList[0].bitLandRequired.ToString(); //Add Anil Sahoo
                if (HdnLandRequr.Value == "True") // Add Anil sahoo for Land Recommened details
                {
                    DivLand.Visible = true;
                    DivNoRecord.Visible = false;
                    LblLandRequr.Text = "Yes";
                    LblLandRequAcre.Text = objProposalList[0].decExtendLand.ToString();
                    LblLandRecoSLFC.Text = objProposalList[0].DecRecomendLand.ToString();
                    if (Convert.ToString(objProposalList[0].StrLandRecomFile) != "")
                    {
                        HdnLandRecomDoc.Value = Convert.ToString(objProposalList[0].StrLandRecomFile);
                        HplLandReacomDoc.NavigateUrl = "~/Proposal/IDCODocs/" + HdnLandRecomDoc.Value;
                    }
                    else
                    {
                        HplLandReacomDoc.Visible = false;
                        LblLandRecom.Text = "-NA-";
                    }
                }
                else
                {
                    DivLand.Visible = false;
                    DivNoRecord.Visible = true;
                    LblNoRecord.Text = "No land recommendation details found as you have not requested any land requirement from the government.";
                    LblNoRecord.ForeColor = System.Drawing.Color.Red;

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objPromoDet = null;
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
            ObjPropasal.strProposalNo = Request.QueryString["Pno"].ToString();
            objProposalList = objService.getRaisedQueryDetails(ObjPropasal).ToList();

            grdQuery.DataSource = objProposalList;
            grdQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objPromoDet = null;
        }
    }
    private void GetActionDetails()//satya added for gridview bind
    {
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        ObjPropasal = new ProposalDet();

        try
        {
            ObjPropasal.strAction = "VA";
            ObjPropasal.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            ObjPropasal.strProposalNo = Request.QueryString["Pno"].ToString();
            objProposalList = objService.getRaisedQueryDetails(ObjPropasal).ToList();

            grdAction.DataSource = objProposalList;
            grdAction.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objPromoDet = null;
        }
    }

    #endregion

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
    protected void grdAction_RowDataBound(object sender, GridViewRowEventArgs e)//satya added for showing ref. file name and file certificate
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFileName = (HiddenField)e.Row.FindControl("hdnFileName");
            HiddenField hdnFileCert = (HiddenField)e.Row.FindControl("hdnFileCert");
            HiddenField hdnScoreCard = (HiddenField)e.Row.FindControl("hdnScoreCard");

            Label lblFile = (Label)e.Row.FindControl("lblFile");
            Label lblCert = (Label)e.Row.FindControl("lblCert");
            Label lblScoreCard = (Label)e.Row.FindControl("lblScoreCard");

            HyperLink hprlnkFileName = (HyperLink)e.Row.FindControl("hprlnkFileName");
            HyperLink hprlnkFileCert = (HyperLink)e.Row.FindControl("hprlnkFileCert");
            HyperLink hprlnkScoreCard = (HyperLink)e.Row.FindControl("hprlnkScoreCard");

            /////// For Reference File
            if (hdnFileName.Value == "")
            {
                lblFile.Text = "NA";
                lblFile.ForeColor = System.Drawing.Color.Red;
                hprlnkFileName.Visible = false;
            }
            else
            {
                hprlnkFileName.ToolTip = hdnFileName.Value;
                hprlnkFileName.Target = "_Blank";
                hprlnkFileName.NavigateUrl = "~/Proposal/ApprovalDocs/" + hdnFileName.Value;
            }

            /////// For PEAL Certificate
            if (hdnFileCert.Value == "")
            {
                lblCert.Text = "NA";
                lblCert.ForeColor = System.Drawing.Color.Red;
                hprlnkFileCert.Visible = false;
            }
            else
            {
                hprlnkFileCert.ToolTip = hdnFileCert.Value;
                hprlnkFileCert.Target = "_Blank";
                hprlnkFileCert.NavigateUrl = "~/Proposal/PEALCertificate/" + hdnFileCert.Value;
            }

            /////// For Score Card
            if (hdnScoreCard.Value == "")
            {
                lblScoreCard.Text = "NA";
                lblScoreCard.ForeColor = System.Drawing.Color.Red;
                hprlnkScoreCard.Visible = false;
            }
            else
            {
                hprlnkScoreCard.ToolTip = hdnScoreCard.Value;
                hprlnkScoreCard.Target = "_Blank";
                hprlnkScoreCard.NavigateUrl = "~/Proposal/ScoreCard/" + hdnScoreCard.Value;
            }
        }
    }
    protected void Grd_GC_Net_Worth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_GC_Net_Worth_File_Name_G = (HiddenField)e.Row.FindControl("Hid_GC_Net_Worth_File_Name_G");
            HyperLink Hyp_View_GC_Doc = (HyperLink)e.Row.FindControl("Hyp_View_GC_Doc");

            Hyp_View_GC_Doc.NavigateUrl = "~/Enclosure/" + Hid_GC_Net_Worth_File_Name_G.Value;
        }
    }
}