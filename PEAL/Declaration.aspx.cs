
#region  Page Info
//******************************************************************************************************************
// File Name             :   Declaration.aspx.cs
// Description           :   Manage Details of Declaration
// Created by            :   Subhasmita Behera
// Created On            :   11-Aug-2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//"VERION= v1"
// FUNCTION NAME         :   
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
using BusinessLogicLayer.Agenda;
using System.IO;
using EntityLayer.Proposal;
using EntityLayer.Agenda;
using System.Xml.Serialization;
//using SelectPdf;
//using WebSupergoo.ABCpdf11;
//using System.Drawing;

#endregion

public partial class Declaration : SessionCheck
{
    AgendaBAL objAgendaService = new AgendaBAL();
    ProposalBAL objService = new ProposalBAL();
    Declartion objDclr = new Declartion();
    ProjectInfo objProp = new ProjectInfo();

    PromoterDet objPromoDet = new PromoterDet();
    LandDet objLandDet = new LandDet();
    ProposalDet ObjPropasal = new ProposalDet();

    string strRetval = "";
    string updSts = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
                {
                    FillDeclartion(Request.QueryString["StrPropNo"]);
                    GetCompanyInfo();
                    GetGroupOfCompanyDetails();//// Added by Sushant Jena On Dt:-27-Aug-2019
                    GetLandDetails();
                    GetEnclosureList();
                    getProjectInfo();
                    //hplPdf.Target = "_self";
                    //hplPdf.NavigateUrl = "GeneratePEALPDF.aspx?PropsalNo=" + Request.QueryString["StrPropNo"] + "&PId=1";
                    GetProductDetails();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Session["proposalno"] as string))
                    {
                        FillDeclartion(Session["proposalno"].ToString());
                        GetCompanyInfo();
                        GetGroupOfCompanyDetails();//// Added by Sushant Jena On Dt:-27-Aug-2019
                        GetLandDetails();
                        GetEnclosureList();
                        getProjectInfo();
                        //hplPdf.Target = "_self";
                        //hplPdf.NavigateUrl = "GeneratePEALPDF.aspx?PropsalNo=" + Session["proposalno"].ToString() + "&PId=1";
                        GetProductDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PEAL");
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
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }
            objProjList = objService.getProductNameDetails(objProp).ToList();
            grdProduct.DataSource = objProjList;
            grdProduct.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void FillDeclartion(string proposalNo)
    {
        try
        {
            objDclr.strAction = "V";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objDclr.vchProposalno = Request.QueryString["StrPropNo"];
            }
            else
            {
                objDclr.vchProposalno = Session["proposalno"].ToString();
            }
            List<Declartion> objProposalList = objService.GetDeclartionData(objDclr).ToList();
            if (objProposalList.Count > 0)
            {
                Session["ids"] = "1";
                if (objProposalList[0].intDeclartion.ToString() == "1")
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }
            }
            else { Session["ids"] = "0"; }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    private void GetCompanyInfo()
    {
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "V";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            }
            else
            {
                objPromoDet.vchProposalNo = Session["proposalno"].ToString();
            }

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
                dvFourthFy.Visible = false;
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
                    lblf1.Text = "Networth Certificate of the Proprietor duly certified by CA for Current/latest year";
                    lblf2.Text = "Tax Audit Report(if applicable) for Current/latest year";
                    lblf3.Text = "Income tax return for Current/latest year";
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

                if (lblOtheConstituition.Text == "Proprietorship")
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
                    dvBoard.Visible = false;
                    dvReserve.Visible = false;
                    dvShareCapital.Visible = false;
                }
                else
                {
                    dvBoard.Visible = true;
                    GetPromoDesignation();
                    dvReserve.Visible = true;
                    dvShareCapital.Visible = true;
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
                    dvFourthFy.Visible = true;
                    lblf1.Text = "Partnership deed";
                    lblf2.Text = "Complete balance sheet of the firm(latest 3 years)";
                    lblf3.Text = "Tax audit report of the Partnership firm";
                    dvReserve.Visible = false;
                    dvShareCapital.Visible = false;
                }
                else
                {
                    dvPartnership.Visible = false;
                    dvReserve.Visible = true;
                    dvShareCapital.Visible = true;
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
                    dvL1.Visible = false;
                    dvL2.Visible = false;
                }
                else
                {
                    lblProjType.Text = "Project cost upto < 50 crore";
                    GetRawMaterialDetails();
                    dvPrjMSME.Visible = true;
                    dvmaterial.Visible = true;
                    dvIRR.Visible = true;
                    dvL1.Visible = true;
                    dvL2.Visible = true;
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

                if ((lblFinYear1.Text == "NA") && (lblFinYear2.Text == "NA") && (lblFinYear3.Text == "NA"))
                {
                    dvx1.Visible = false;
                }

                if (lblFinYear1.Text == "NA")
                {
                    dvx2.Visible = false;
                    dvx21.Visible = false;
                    dvx22.Visible = false;
                    dvx23.Visible = false;
                    dvx24.Visible = false;
                    dvx25.Visible = false;
                }

                if (lblFinYear2.Text == "NA")
                {
                    dvx3.Visible = false;
                    dvx31.Visible = false;
                    dvx32.Visible = false;
                    dvx33.Visible = false;
                    dvx34.Visible = false;
                    dvx35.Visible = false;
                }

                if (lblFinYear3.Text == "NA")
                {
                    dvx4.Visible = false;
                    dvx41.Visible = false;
                    dvx42.Visible = false;
                    dvx43.Visible = false;
                    dvx44.Visible = false;
                    dvx45.Visible = false;
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

                if ((lblAnnlCrntYr.Text == "0.00") && (lblPftBTCrntYr.Text == "0.00") && (lblNWCrntYr.Text == "0.00") && (lblRSCrntyr.Text == "0.00") && (lblSCCrntYr.Text == "0.00"))
                {
                    dvx2.Visible = false;
                    dvx21.Visible = false;
                    dvx22.Visible = false;
                    dvx23.Visible = false;
                    dvx24.Visible = false;
                    dvx25.Visible = false;
                }

                if ((lblAnnlLastYr.Text == "0.00") && (lblPftBTLastYr.Text == "0.00") && (lblNWLastYr.Text == "0.00") && (lblRSLastYr.Text == "0.00") && (lblSCLastYr.Text == "0.00"))
                {
                    dvx3.Visible = false;
                    dvx31.Visible = false;
                    dvx32.Visible = false;
                    dvx33.Visible = false;
                    dvx34.Visible = false;
                    dvx35.Visible = false;
                }

                if ((lblAnnlPrevToLastYr.Text == "0.00") && (lblPftBTPrevToLastYr.Text == "0.00") && (lblNWPrevTolastyr.Text == "0.00") && (lblRSPrevTolastYr.Text == "0.00") && (lblSCPrevToLastYr.Text == "0.00"))
                {
                    dvx4.Visible = false;
                    dvx41.Visible = false;
                    dvx42.Visible = false;
                    dvx43.Visible = false;
                    dvx44.Visible = false;
                    dvx45.Visible = false;
                }

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
        ProjectInfo objProjIn = new ProjectInfo();
        try
        {
            objProjIn.strAction = "GCNW";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProjIn.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            }
            else
            {
                objProjIn.vchProposalNo = Session["proposalno"].ToString();
            }

            DataTable dt = new DataTable();
            dt = objService.GetGcNewWorthDetails(objProjIn);
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
            objProjIn = null;
        }
    }
    private void GetRawMaterialDetails()
    {
        List<PromoterDet> objProposalList = new List<PromoterDet>();
        objPromoDet = new PromoterDet();
        try
        {
            objPromoDet.strAction = "G";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objPromoDet.vchProposalNo = Session["proposalno"].ToString();
            }
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
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objPromoDet.vchProposalNo = Session["proposalno"].ToString();
            }
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
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            }
            else
            {
                objPromoDet.vchProposalNo = Session["proposalno"].ToString();
            }

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
                if (Convert.ToString(objProposalList[0].strIncomeTaxReturn) != "")
                {
                    hdnFyFourth.Value = Convert.ToString(objProposalList[0].strIncomeTaxReturn);
                    hplnkFyFourth.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strIncomeTaxReturn);
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
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }

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

            objProp.strAction = "E1";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }

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
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }

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
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objLandDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            }
            else
            {
                objLandDet.vchProposalNo = Session["proposalno"].ToString();
            }

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

                if (objProposalList[0].sintLandRequiredIDCO.ToString() == "1")
                {
                    dvIDCOName.Visible = true;
                    lblLandrequiredIDCO.Text = "Yes";
                    lblIDCOName.Text = objProposalList[0].vchIDCOInustrial.ToString();
                    dvLandAcquired.Visible = false;
                }
                else
                {
                    dvIDCOName.Visible = false;
                    lblLandrequiredIDCO.Text = "No";

                    dvLandAcquired.Visible = true;
                    if (objProposalList[0].sintLandAcquiredIDCO.ToString() == "1")
                    {
                        lbllandacquired.Text = "Yes";
                    }
                    else
                    {
                        lbllandacquired.Text = "No";
                    }
                }

                lblLoadGrid.Text = objProposalList[0].decPowerDemandGrid.ToString();
                lblLoadCPP.Text = objProposalList[0].decPowerDrawalCPP.ToString();
                lblCPPCapacity.Text = objProposalList[0].decCapacityofCPPPlant.ToString();

                LblPowerDemandIPP.Text = objProposalList[0].DecPowerProducerIpp.ToString(); /////Added by Sushant Jena On Dt:-24-Aug-2021

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

                if (Convert.ToString(objProposalList[0].strProjectLandStmt) != "")
                {
                    hdnLandUsestmt.Value = Convert.ToString(objProposalList[0].strProjectLandStmt);
                    hypProjectlandStatement.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strProjectLandStmt);
                }

                if (Convert.ToString(objProposalList[0].strProjectLayOut) != "")
                {
                    hdnLayOutPln.Value = Convert.ToString(objProposalList[0].strProjectLayOut);
                    hypProjectLaoutPlan.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strProjectLayOut);
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

    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            Response.Redirect("landdetails.aspx?StrPropNo=" + Request.QueryString["StrPropNo"].ToString());
        }
        else
        {
            Response.Redirect("landdetails.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["ids"]) != 0)
            {
                objDclr.strAction = "U";
            }
            else
            {
                objDclr.strAction = "A";
            }

            if (check.Checked == true)
            {
                objDclr.intDeclartion = 1;
            }
            else
            {
                objDclr.intDeclartion = 0;
            }

            objDclr.intCreatedBy = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objDclr.vchProposalno = Request.QueryString["StrPropNo"];
                objProp.RApplicationNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objDclr.vchProposalno = Session["proposalno"].ToString();
                objProp.RApplicationNo = Session["proposalno"].ToString();
            }
            objProp.RDesc = "Processing Fee";

            string strRetVal = objService.Declartion(objDclr);
            objProp.Ramount = strRetVal.Split('_')[1];
            objProp.RAccountHead = "0852-80-800-0234-02233-000";
            objProp.RUserName = Session["IndustryName"].ToString();
            if (strRetVal != "")
            {
                Session["proposalno"] = "";
            }
            string strImage = Server.MapPath("~/images/PEALPdfheader-new.jpg");
            Session["obj_App"] = objProp;

            //Doc theDoc = new Doc();
            //theDoc.MediaBox.String = "A4";
            //theDoc.Rect.String = theDoc.MediaBox.String;

            //using (XImage xi = XImage.FromFile(strImage, null))
            //{
            //    if (xi.Height < 100)
            //    {
            //        theDoc.Rect.Inset(20, 60);
            //    }
            //    else if (xi.Height > 100 && xi.Height < 155)
            //    {
            //        theDoc.Rect.Inset(20, 80);
            //    }
            //    else if (xi.Height > 155 && xi.Height < 165)
            //    {
            //        theDoc.Rect.Inset(30, 90);
            //    }
            //    else
            //    {
            //        theDoc.Rect.Inset(50, 100);
            //    }

            //}


            //theDoc.FontSize = 24;
            //theDoc.HtmlOptions.Engine = EngineType.Chrome;
            //theDoc.HtmlOptions.UseScript = true; // enable JavaScript
            //theDoc.HtmlOptions.Media = MediaType.Print; // Or Screen for a more screen oriented output
            //theDoc.HtmlOptions.InitialWidth = 1200; // In case we have a responsive site which is non-specific on good widths
            //theDoc.Page = theDoc.AddPage();
            //int theID;

            //string callUrl = "http://localhost/swp/PEAL/GeneratePEALPDF.aspx?PropsalNo=" + objDclr.vchProposalno + "&PId=0";
            //theID = theDoc.AddImageUrl(callUrl, true, 800, false);
            //while (true)
            //{
            //    if (!theDoc.Chainable(theID))
            //        break;
            //    theDoc.Page = theDoc.AddPage();
            //    theID = theDoc.AddImageToChain(theID);
            //}

            //theDoc.Rect.String = "0 780 500 842";
            //theDoc.TextStyle.HPos = 0.5;
            //theDoc.TextStyle.VPos = 0.5;

            //theDoc.FontSize = 9;
            //for (int i = 1; i <= theDoc.PageCount; i++)
            //{
            //    theDoc.PageNumber = i;
            //    string saveRect = theDoc.Rect.String;
            //    using (XImage xi = XImage.FromFile(strImage, null))
            //    {
            //        theDoc.Rect.Resize(xi.Width /2, xi.Height / 2, XRect.Corner.TopLeft);
            //        theDoc.AddImage(xi);
            //    }

            //    double padX = theDoc.FontSize;
            //    double padY = theDoc.FontSize / 3;
            //    string format = "<stylerun justification=\"1.0\" leftmargins=\"0 {0} {1}\">";
            //    string style = string.Format(format, theDoc.Rect.Height + padY, theDoc.Rect.Width + padX);
            //    theDoc.Rect.String = saveRect;
            //    int id = theDoc.AddTextStyled("");

            //    theDoc.Flatten();
            //}
            //theDoc.Rect.String = "90 50 580 60";
            //theDoc.TextStyle.HPos = 1.0;
            //theDoc.TextStyle.VPos = 0.5;

            //theDoc.FontSize = 10;
            //for (int i = 1; i <= theDoc.PageCount; i++)
            //{
            //    theDoc.PageNumber = i;
            //    theDoc.AddText("Date : " + DateTime.Now.ToString("dd-MMM-yyyy  h:mm tt").ToString());

            //}

            //string path = Server.MapPath("~/PEALDOCS/");


            //theDoc.Save(path + objDclr.vchProposalno + "_PEAL.pdf");
            //theDoc.Clear();
            Response.Redirect("~/PaymentModal.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnLater_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["ids"]) != 0)
        {
            objDclr.strAction = "U";
        }
        else
        {
            objDclr.strAction = "A";
        }
        if (check.Checked == true)
        {
            objDclr.intDeclartion = 1;
        }
        else
        {
            objDclr.intDeclartion = 0;
        }

        objDclr.intCreatedBy = 1;
        if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objDclr.vchProposalno = Request.QueryString["StrPropNo"];
        }
        else
        {
            objDclr.vchProposalno = Session["proposalno"].ToString();
        }
        string strRetVal = objService.Declartion(objDclr);
        if (strRetVal.Split('_')[0] == "1")
        {
            Response.Redirect("PEALformSuccess.aspx");
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