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

public partial class ProposalDetails : SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    PromoterDet objPromoDet = new PromoterDet();
    LandDet objLandDet = new LandDet();
    ProposalDet ObjPropasal = new ProposalDet();
    SqlCommand cmd;
    SqlDataAdapter da;
    

    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)/// Add by Anil sahoo
        {
            Response.Redirect("~/portal/SessionRedirect.aspx", false);
            return;
        }

        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["StrPropNo"] != null)
                {
                    GetCompanyInfo();
                    GetGroupOfCompanyDetails();//// Added by Sushant Jena On Dt:-27-Aug-2019
                    GetLandDetails();
                    GetEnclosureList();
                    getProjectInfo();
                    GetQueryDetails();
                    RepeterData();
                    GetProductDetails();
                    GetActionDetails(); //satya added
                   
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ProposalDetails");
            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    #region FunctionUsed

    private void GetProductDetails()
    {
        try
        {
          
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "M2";
            objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            List<ProjectInfo> objProjList = objService.getProductNameDetails(objProp).ToList();

            grdProduct.DataSource = objProjList;
            grdProduct.DataBind();
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
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
                Lbl_Proposal_No.Text = objPromoDet.vchProposalNo; //// Added by Sushant Jena on Dt:-19-Aug-2019
                Lbl_Application_Date.Text = string.Format("{0:dd-MMM-yyyy}", objProposalList[0].dtmApplicationDate); //// Added by Sushant Jena on Dt:-19-Aug-2019

                lblCompName.Text = objProposalList[0].vchCompName.ToString();
             
                if (objProposalList[0].vchAddress.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022
                {
                    lblAddress.Text = objProposalList[0].vchAddress.ToString();
                }
                else
                {
                    lblAddress.Text = "-NA-";
                }
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
                    lblPhoneNo.Text = "-NA-";
                    lblPhoneStateCode.Text = "";
                }

                if (objProposalList[0].vchFaxNo.ToString() != "")
                {
                    lblFaxNo.Text = objProposalList[0].vchFaxNo.ToString();
                    lblISDFXNo.Text = objProposalList[0].VCHISDFXNo.ToString();
                }
                else
                {
                    lblFaxNo.Text = "-NA-";
                    lblISDFXNo.Text = "";
                }

              
                if (objProposalList[0].vchEmail.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022 
                {
                    lblEmail.Text = objProposalList[0].vchEmail.ToString();
                }
                else
                {
                    lblEmail.Text = "-NA-";
                }
              
                if (objProposalList[0].intPin.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022 
                {
                    lblPin.Text = objProposalList[0].intPin.ToString();
                }
                else
                {
                    lblPin.Text = "-NA-";
                }
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
             
                if (objProposalList[0].vchCorMobileNo.ToString() != "")
                {
                    lblCorMobileNo.Text = objProposalList[0].vchCorMobileNo.ToString();
                }
                else
                {
                    lblCorMobileNo.Text = "-NA-";                   
                }

                if (objProposalList[0].vchCorFaxNo.ToString() != "")
                {
                    lblFaxCordet.Text = objProposalList[0].VCHISDFAXCor.ToString();
                    lblCorFaxNo.Text = objProposalList[0].vchCorFaxNo.ToString();
                }
                else
                {
                    lblFaxCordet.Text = "";
                    lblCorFaxNo.Text = "-NA-";//add by Debiprasanna Dt-23-12-22
                }

             
                if (objProposalList[0].vchCorEmail.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022
                {
                    lblCorEmail.Text = objProposalList[0].vchCorEmail.ToString();
                }
                else
                {
                    lblCorEmail.Text = "-NA-";
                }
               
                if (objProposalList[0].intCorPin.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022
                {
                    lblCorrPin.Text = objProposalList[0].intCorPin.ToString();
                }
                else
                {
                    lblCorrPin.Text = "-NA-";
                }
                dvFourthFy.Visible = false;
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
                    lblLinkCert.Visible = true;
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
                    lblLinkCert.Visible = false;
                    lblf1.Text = "Networth Certificate of the Proprietor duly certified by CA for Current/latest year";
                    lblf2.Text = "Tax Audit Report(if applicable) for Current/latest year";
                    lblf3.Text = "Income tax return for Current/latest year";
                }
                else
                {
                    dvPromoter.Visible = false;
                }

               

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
                    dvFourthFy.Visible = true;
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
                    lblLinkCert.Visible = true;
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
                    lblLinkCert.Visible = true;
                }

                if (lblOtheConstituition.Text == "Co-operative")
                {
                    DVC1.Visible = true;
                    DVC2.Visible = true;
                    lblMemoLabel.Visible = false;
                    lblLinkCert.Visible = true;
                }

                lblExperience.Text = objProposalList[0].intExpInYr.ToString();

                ////Company Reg details
              
                if (objProposalList[0].intYearOfIncorporation.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022
                {
                    lblYearIncorp.Text = objProposalList[0].intYearOfIncorporation.ToString();
                }
                else
                {
                    lblYearIncorp.Text = "-NA-";
                }
               
                if (objProposalList[0].vchPlaceIncor.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022
                {
                    lblPlaceIncor.Text = objProposalList[0].vchPlaceIncor.ToString();
                }
                else
                {
                    lblPlaceIncor.Text = "-NA-";
                }
               
                if (objProposalList[0].vchGSTIN.ToString() != "")//Add by Debiprasanna on Dt:30-12-2022
                {
                    lblGSTIN.Text = objProposalList[0].vchGSTIN.ToString();
                }
                else
                {
                    lblGSTIN.Text = "-NA-";
                }
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
            throw ex.InnerException;
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
            objProjIn.vchProposalNo = Request.QueryString["StrPropNo"].ToString();

           
          DataTable  dt = objService.GetGcNewWorthDetails(objProjIn);
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
            throw ex.InnerException;
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
            objPromoDet.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProposalList = objService.GetRawMetrialDetails(objPromoDet).ToList();

            grdRawMaterials.DataSource = objProposalList;
            grdRawMaterials.DataBind();
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
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
            throw ex.InnerException;
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
                if (Convert.ToString(objProposalList[0].vchPanfile) != "") //1
                {
                    hdnPanFile.Value = Convert.ToString(objProposalList[0].vchPanfile);
                    hplnkPan.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchPanfile);
                    lblPanLink.Visible=true;
                    lblpanLabel.Visible = false;
                }
                else
                {
                    lblPanLink.Visible = false;
                    lblpanLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchGSTNfile) != "") //2
                {
                    hdnGstinFile.Value = Convert.ToString(objProposalList[0].vchGSTNfile);
                    hplnkGstin.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchGSTNfile);
                    lblGstLink.Visible = true;
                    lblGstLabel.Visible = false;
                }
                else
                {
                    lblGstLink.Visible = false;
                    lblGstLabel.Visible = true;
                }

                if (Convert.ToString(objProposalList[0].vchMemorandumfile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchMemorandumfile)) != true))
                {
                    hdnMemoFile.Value = Convert.ToString(objProposalList[0].vchMemorandumfile);
                    hplnkMemo.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchMemorandumfile);
                    lblMemoLink.Visible = true;//add by Debiprasanna Dt-23-12-22
                    lblMemoLabel.Visible = false;
                    DVC3.Visible = true;
                }
                else
                {
                    lblMemoLink.Visible = false;
                    lblMemoLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchCertificateincorpfile) != "") //4
                {
                    hdnCerti.Value = Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                    hplnkCerti.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                    lblLinkCert.Visible = true;
                    lblCertLabel.Visible = false;
                }
                else
                {
                    lblLinkCert.Visible = false;
                    lblCertLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchEduQualifile) != "") //5
                {
                    hdnEdu.Value = Convert.ToString(objProposalList[0].vchEduQualifile);
                    hplnkEdu.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchEduQualifile);
                    lblLinkEdu.Visible = true;
                    lblEduLabel.Visible = false;
                }
                else
                {
                    lblLinkEdu.Visible = false;
                    lblEduLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchTechniQualifile) != "") //6
                {
                    hdnTecnical.Value = Convert.ToString(objProposalList[0].vchTechniQualifile);
                    hplnkTechQ.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchTechniQualifile);
                    lblLinkTechq.Visible = true;
                    lblTechqLabel.Visible = false;
                }
                else
                {
                    lblLinkTechq.Visible = false;
                    lblTechqLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchExpFile) != "") //7
                {
                    hdnExperience.Value = Convert.ToString(objProposalList[0].vchExpFile);
                    hplnkExperience.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchExpFile);
                    lblLinkExperience.Visible = true;
                    lblExperienceLabel.Visible = false;
                }
                else
                {
                    lblLinkExperience.Visible = false;
                    lblExperienceLabel.Visible = true;
                }

                if (Convert.ToString(objProposalList[0].vchAuditFile) != "") //8
                {
                    hdnAudit.Value = Convert.ToString(objProposalList[0].vchAuditFile);
                    hplnkAudit.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFile);
                    lblLinkAudit.Visible = true;
                    lblAuditLabel.Visible = false;
                }
                else
                {
                    lblLinkAudit.Visible = false;
                    lblAuditLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchAuditFileSecondYrs) != "") //9
                {
                    hdnFySecond.Value = Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);
                    hplnkFySecond.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);
                    lblLinkFySecond.Visible = true;
                    lblFySecondLabel.Visible = false;
                }
                else
                {
                    lblLinkFySecond.Visible = false;
                    lblFySecondLabel.Visible = true;
                }

                if (Convert.ToString(objProposalList[0].vchAuditFileThrdYrs) != "") //10
                {
                    hdnFyThird.Value = Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);
                    hplnkFyThird.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);
                    lblLinkFyThird.Visible = true;
                    lblFyThirdLabel.Visible = false;
                }
                else
                {
                    lblLinkFyThird.Visible = false;
                    lblFyThirdLabel.Visible = true;
                }

                if (Convert.ToString(objProposalList[0].vchNetWorthfile) != "") //11
                {
                    hdnNetWorth.Value = Convert.ToString(objProposalList[0].vchNetWorthfile);
                    hplnkNetWorth.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchNetWorthfile);
                    lblLinkNetWorth.Visible = true;
                    lblNetWorthLabel.Visible = false;
                }
                else
                {
                    lblLinkNetWorth.Visible = false;
                    lblNetWorthLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].strIncomeTaxReturn) != "") //12
                {
                    hdnFyFourth.Value = Convert.ToString(objProposalList[0].strIncomeTaxReturn);
                    hplnkFyFourth.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strIncomeTaxReturn);
                    lblLinkFyFourth.Visible = true;
                    lblFyFourthLabel.Visible = false;
                }
                else
                {
                    lblLinkFyFourth.Visible = false;
                    lblFyFourthLabel.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
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
                divIdIndustryNoRecord.Visible = false;
                divIndustryInfo.Visible = true;
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
                else if (hdnEin.Value == "5")
                {
                    lblEin.Text = "Udayam Aadhar :";
                    lblEIM.Text = "Udayam Aadhar :";
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
                    lblLinkEIM.Visible = true;
                    lblEimLabel.Visible = false;
                }
                else
                {
                    lblLinkEIM.Visible = false;
                    lblEimLabel.Visible = true;
                }

                if (Convert.ToString(objProjList[0].vchManufacturingProcessFlow) != "")
                {
                    hdnFileMnfprocess.Value = Convert.ToString(objProjList[0].vchManufacturingProcessFlow);
                    hplnkFileMnfprocess.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchManufacturingProcessFlow);
                    lblLinkFileMnfprocess.Visible = true;
                    lblFileMnfprocessLabel.Visible = false;
                }
                else
                {
                    lblLinkFileMnfprocess.Visible = false;
                    lblFileMnfprocessLabel.Visible = true;
                }

                if (Convert.ToString(objProjList[0].vchFeasibilityReport) != "")
                {
                    hdnFeasibilityReport.Value = Convert.ToString(objProjList[0].vchFeasibilityReport);
                    hplnkFeasibilityReport.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchFeasibilityReport);
                    lblLinkFeasibilityReport.Visible = true;
                    lblFeasibilityReportLabel.Visible = false;
                }
                else
                {
                    lblLinkFeasibilityReport.Visible = false;
                    lblFeasibilityReportLabel.Visible = true;
                }


                if (Convert.ToString(objProjList[0].vchBoardResolution) != "")
                {
                    hdnBoardResolution.Value = Convert.ToString(objProjList[0].vchBoardResolution);
                    hplnkBoardResolution.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchBoardResolution);
                    lblLinkBoardResolution.Visible = true;
                    lblBoardResolutionLabel.Visible = false;
                }
                else
                {
                    lblLinkBoardResolution.Visible = false;
                    lblBoardResolutionLabel.Visible = true;
                }

                if (Convert.ToString(objProjList[0].vchSourceOfFinance) != "")
                {
                    hdnOtherFin.Value = Convert.ToString(objProjList[0].vchSourceOfFinance);
                    hplnkOtherFin.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchSourceOfFinance);
                    lblLinkOtherFin.Visible = true;
                    lblOtherFinLabel.Visible = false;
                }
                else
                {
                    lblLinkOtherFin.Visible = false;
                    lblOtherFinLabel.Visible = true;
                }
            }
            else
            {
                divIdIndustryNoRecord.Visible = true;
                divIndustryInfo.Visible = false;
            }


            /*-----------------------------------------------------*/

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

            /*-----------------------------------------------------*/

            objProp.strAction = "E2";
            objProp.vchProposalNo = Request.QueryString["StrPropNo"].ToString();
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
            throw ex.InnerException;
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
                divProposedLand.Visible = true;
                divProposedSiteDetailsNoRecoord.Visible = false;
                divLanRecomNoValue.Visible = false;
                divLanRecomBody.Visible = true;
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
                // LblLandUnit.Text = " (in " + objProposalList[0].strLandUnit.ToString() + ")";//// Added by Sushant Jena On Dt:-18-Feb-2020

                if (objProposalList[0].sintLandRequiredIDCO.ToString() == "1") //// Land Required from IDCO
                {
                    dvIDCOName.Visible = true;
                    dvLandAcquired.Visible = false; ////Added by Bhagyashree Das on Dt:-28-Jan-2021

                    lblLandrequiredIDCO.Text = "Yes";
                    lblIDCOName.Text = objProposalList[0].vchIDCOInustrial.ToString();
                    lbllandacquired.Text = (objProposalList[0].sintLandAcquiredIDCO.ToString() == "1") ? "Yes" : "No";
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
                LblPowerProducerIpp.Text = objProposalList[0].DecPowerProducerIpp.ToString(); /////Added By Sushant Jena On Dt:- 24-Aug-2021


                lblWaterRequireExist.Text = objProposalList[0].decWaterRequireExist.ToString();
                lblWaterReqireProposed.Text = objProposalList[0].decWaterReqireProposed.ToString();
                lblWaterReq.Text = objProposalList[0].decWaterRequirProduct.ToString();
               
                if (objProposalList[0].vchQuntRecyllingWaste.ToString() == " ")//Add by Debiprasanna Dt-23-12-22
                {
                    lblQuantum.Text = objProposalList[0].vchQuntRecyllingWaste.ToString();
                }
                else
                {
                    lblQuantum.Text = "-NA-";
                }

                /*-----------------------------------------------------------------------------------*/

                string strWaterSource = "";

                if (objProposalList[0].vchSurfaceWater.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchSurfaceWater.ToString() + ", ";
                   
                }

                if (objProposalList[0].vchIdcoSupply.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchIdcoSupply.ToString() + ", ";
                  
                }

                if (objProposalList[0].vchRainWtrHarvesting.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchRainWtrHarvesting.ToString() + ", ";
                   
                }

                if (objProposalList[0].vchother.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchother.ToString() + ", ";
                    strWaterSource = strWaterSource + objProposalList[0].vchOtherSpecify.ToString() + ", ";
                   
                }

                strWaterSource = strWaterSource.Trim().TrimEnd(',');

                lblSurface.Text = "-NA-";

                /*-----------------------------------------------------------------------------------*/

                if (Convert.ToString(objProposalList[0].vchWasteConserFile) != "")
                {
                    hdnWaterFile.Value = Convert.ToString(objProposalList[0].vchWasteConserFile);
                    hplnkWaterFile.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchWasteConserFile);
                    lblLinkWaterFile.Visible = true;
                    lblWaterFileLabel.Visible = false;
                }
                else
                {
                    lblLinkWaterFile.Visible = false;
                    lblWaterFileLabel.Visible = true;
                }

                if (Convert.ToString(objProposalList[0].vchWaterHazardousFile) != "")
                {
                    hdnHazardousFile.Value = Convert.ToString(objProposalList[0].vchWaterHazardousFile);
                    hplnkHazardousFile.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchWaterHazardousFile);
                    lblLinkHazardousFile.Visible = true;
                    lblHazardousFileLabel.Visible = false;
                }
                else
                {
                    lblLinkHazardousFile.Visible = false;
                    lblHazardousFileLabel.Visible = true;

                }

                if (Convert.ToString(objProposalList[0].strProjectLandStmt) != "")
                {
                    hdnLandUsestmt.Value = Convert.ToString(objProposalList[0].strProjectLandStmt);
                    hypProjectlandStatement.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strProjectLandStmt);
                    lblLinkProjectlandStatement.Visible = true;
                    lblProjectlandStatementLablel.Visible = false;
                }
                else
                {
                    lblLinkProjectlandStatement.Visible = false;
                    lblProjectlandStatementLablel.Visible = true;
                }

                if (Convert.ToString(objProposalList[0].strProjectLayOut) != "")
                {
                    hdnLayOutPln.Value = Convert.ToString(objProposalList[0].strProjectLayOut);
                    hypProjectLaoutPlan.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strProjectLayOut);
                    lblLinkProjectLaoutPlan.Visible = true;
                    lblProjectLaoutPlanLabel.Visible = false;
                }
                else
                {
                    lblLinkProjectLaoutPlan.Visible = false;
                    lblProjectLaoutPlanLabel.Visible = true;
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
            else
            {
                divProposedLand.Visible = false;
                divProposedSiteDetailsNoRecoord.Visible = true;
                divLanRecomNoValue.Visible = true;
                divLanRecomBody.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objPromoDet = null;
        }

    }
    private void RepeterData()
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
            throw ex.InnerException;
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
            throw ex.InnerException ;
        }
        finally
        {
            objPromoDet = null;
        }
    }
    private void GetActionDetails()// GetActionDetails satya added
    {
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        ObjPropasal = new ProposalDet();
        try
        {
            ObjPropasal.strAction = "VA";
            ObjPropasal.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            ObjPropasal.strProposalNo = Request.QueryString["StrPropNo"].ToString();
            objProposalList = objService.getRaisedQueryDetails(ObjPropasal).ToList();

            grdAction.DataSource = objProposalList;
            grdAction.DataBind();
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
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
    protected void grdAction_RowDataBound(object sender, GridViewRowEventArgs e)//satya added
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFileName = (HiddenField)e.Row.FindControl("hdnFileName");
            HiddenField hdnFileCert = (HiddenField)e.Row.FindControl("hdnFileCert");
            HiddenField hdnScoreCard = (HiddenField)e.Row.FindControl("hdnScoreCard");

            Label lblFile = (Label)e.Row.FindControl("lblFile");
            Label lblCert = (Label)e.Row.FindControl("lblCert");
            Label lblScoreCard = (Label)e.Row.FindControl("lblScoreCard");

            HyperLink hprlnkQuery1 = (HyperLink)e.Row.FindControl("hprlnkQuery1");
            HyperLink hprlnkQuery2 = (HyperLink)e.Row.FindControl("hprlnkQuery2");
            HyperLink hprlnkScoreCard = (HyperLink)e.Row.FindControl("hprlnkScoreCard");

            /////// For Reference File
            if (hdnFileName.Value == "")
            {
                lblFile.Text = "NA";
                lblFile.ForeColor = System.Drawing.Color.Red;
                hprlnkQuery1.Visible = false;
            }
            else
            {
                hprlnkQuery1.ToolTip = hdnFileName.Value;
                hprlnkQuery1.Target = "_Blank";
                hprlnkQuery1.NavigateUrl = "~/Proposal/ApprovalDocs/" + hdnFileName.Value;
            }

            /////// For PEAL Certificate
            if (hdnFileCert.Value == "")
            {
                lblCert.Text = "NA";
                lblCert.ForeColor = System.Drawing.Color.Red;
                hprlnkQuery2.Visible = false;
            }
            else
            {
                hprlnkQuery2.ToolTip = hdnFileCert.Value;
                hprlnkQuery2.Target = "_Blank";
                hprlnkQuery2.NavigateUrl = "~/Proposal/PEALCertificate/" + hdnFileCert.Value;
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
    protected void Grd_GC_Net_Worth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_GC_Net_Worth_File_Name_G = (HiddenField)e.Row.FindControl("Hid_GC_Net_Worth_File_Name_G");
            HyperLink Hyp_View_GC_Doc = (HyperLink)e.Row.FindControl("Hyp_View_GC_Doc");

            Hyp_View_GC_Doc.NavigateUrl = "~/Enclosure/" + Hid_GC_Net_Worth_File_Name_G.Value;
        }
    }

    StringWriter GetServiceDetailsContent(string AppID, int IntServiceID)
    {
        StringWriter writer = new StringWriter();
        Server.Execute("../AppDetails.aspx?ApplicationNo=" + AppID + "&ServiceId=" + IntServiceID.ToString(), writer);
        return writer;
    }
}