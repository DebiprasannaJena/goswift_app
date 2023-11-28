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

public partial class Portal_Proposal_ProposalDetailsService : System.Web.UI.Page
{
    //ProposalService.ProposalBusinessLayerClient objService = new ProposalService.ProposalBusinessLayerClient();
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();

    PromoterDet objPromoDet = new PromoterDet();
    LandDet objLandDet = new LandDet();
    ProposalDet ObjPropasal = new ProposalDet();


    string strRetval = "";
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divA1.Visible = false;
            divQ1.Visible = false;

            divQ2.Visible = false;
            divA2.Visible = false;
            divfile2.Visible = false;
            divfile1.Visible = false;
            BindData();

            if (Request.QueryString["Pno"] != null)
            {
                GetCompanyInfo();
                GetLandDetails();

                GetEnclosureList();
                getProjectInfo();
                GetQueryDetails();
                //Added By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button
                CheckRaiseQueryStatus();
                //Ended By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button
            }
        }

    }
    private void BindData()
    {
        objProposal = new ProposalDet();
        try
        {
            objProposal.strAction = "E";
            objProposal.strProposalNo = Request.QueryString["Pno"].ToString();
            List<ProposalDet> objProposalList = objService.getRaisedQueryDetails(objProposal).ToList();
            hdnNoofrecord.Value = objProposalList.Count.ToString();
            if (objProposalList.Count == 0)
            { div1stcnt.Visible = true; }
            else if (objProposalList.Count == 2)
            { div2ndcnt.Visible = true; }
            else
            { div1stcnt.Visible = false; div2ndcnt.Visible = false; }

            if (objProposalList.Count > 0 && objProposalList.Count <= 2)
            {
                divA1.Visible = true;
                divQ1.Visible = true;
                // divQ2.Visible = true;
                lblq1.Text = objProposalList[0].strRemarks.ToString();
                txtq1.Visible = false;

                if (objProposalList.Count > 1)
                {
                    divQ2.Visible = true;
                    lblq2.Visible = false;

                    lbla1.Text = objProposalList[1].strRemarks.ToString();
                    if (objProposalList[1].strFileName.ToString() != "")
                    {
                        string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
                        if (strarr[0] != "")
                        {
                            hlDoc1.NavigateUrl = "../../QueryFiles/" + strarr[0];
                            pdficon1.Visible = true;
                            divfile1.Visible = true;
                        }
                        if (strarr[1] != "")
                        {
                            hlDoc2.NavigateUrl = "../../QueryFiles/" + strarr[1];
                            pdficon2.Visible = true;
                            divfile1.Visible = true;
                        }
                        if (strarr[2] != "")
                        {
                            hlDoc3.NavigateUrl = "../../QueryFiles/" + strarr[2];
                            pdficon3.Visible = true;
                            divfile1.Visible = true;
                        }
                    }
                }
                else { lbla1.Visible = false; divA1.Visible = false; btnSubmit.Visible = false; btnCancel.Visible = false; }
            }
            else if (objProposalList.Count > 2 && objProposalList.Count <= 3)
            {
                divA1.Visible = true;
                divQ1.Visible = true;
                txtq1.Visible = false;
                txtq2.Visible = false;
                divQ2.Visible = true;

                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                lblq1.Text = objProposalList[0].strRemarks.ToString();
                lbla1.Text = objProposalList[1].strRemarks.ToString();

                lblq2.Text = objProposalList[2].strRemarks.ToString();
                //txta2.Text = objProposalList[3].strRemarks.ToString();
                if (objProposalList[1].strFileName.ToString() != "")
                {
                    string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
                    if (strarr[0] != "")
                    {
                        hlDoc1.NavigateUrl = "../../QueryFiles/" + strarr[0];
                        pdficon1.Visible = true;
                        divfile1.Visible = true;
                    }
                    if (strarr[1] != "")
                    {
                        hlDoc2.NavigateUrl = "../../QueryFiles/" + strarr[1];
                        pdficon2.Visible = true;
                        divfile1.Visible = true;
                    }
                    if (strarr[2] != "")
                    {
                        hlDoc3.NavigateUrl = "../../QueryFiles/" + strarr[2];
                        pdficon3.Visible = true;
                        divfile1.Visible = true;
                    }
                }
                //if (objProposalList[3].strFileName.ToString() != "")
                //{

                //    string[] strarr = objProposalList[3].strFileName.ToString().Split(',');
                //    if (strarr[0] != "")
                //    {
                //        hlDoc4.NavigateUrl = "../../SWP_Web/QueryFiles/" + strarr[0];
                //        pdficon4.Visible = true;
                //        divfile2.Visible = true;
                //    }
                //    if (strarr[1] != "")
                //    {
                //        hlDoc5.NavigateUrl = "../../SWP_Web/QueryFiles/" + strarr[1];
                //        pdficon5.Visible = true;
                //        divfile2.Visible = true;
                //    }
                //    if (strarr[2] != "")
                //    {
                //        hlDoc6.NavigateUrl = "../../SWP_Web/QueryFiles/" + strarr[2];
                //        pdficon6.Visible = true;
                //        divfile2.Visible = true;
                //    }
                //}

            }
            else if (objProposalList.Count > 2 && objProposalList.Count <= 4)
            {

                divA1.Visible = true;
                divQ1.Visible = true;
                txtq1.Visible = false;
                txtq2.Visible = false;
                divQ2.Visible = true;
                divA2.Visible = true;
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                lblq1.Text = objProposalList[0].strRemarks.ToString();
                lbla1.Text = objProposalList[1].strRemarks.ToString();

                lblq2.Text = objProposalList[2].strRemarks.ToString();
                lbla2.Text = objProposalList[3].strRemarks.ToString();
                if (objProposalList[1].strFileName.ToString() != "")
                {

                    string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
                    if (strarr[0] != "")
                    {
                        hlDoc1.NavigateUrl = "../../QueryFiles/" + strarr[0];
                        pdficon1.Visible = true;
                        divfile1.Visible = true;
                    }
                    if (strarr[1] != "")
                    {
                        hlDoc2.NavigateUrl = "../../QueryFiles/" + strarr[1];
                        pdficon2.Visible = true;
                        divfile1.Visible = true;
                    }
                    if (strarr[2] != "")
                    {
                        hlDoc3.NavigateUrl = "../../QueryFiles/" + strarr[2];
                        pdficon3.Visible = true;
                        divfile1.Visible = true;
                    }
                }

                else
                {
                    pdficon1.Visible = false;
                    pdficon2.Visible = false;
                    pdficon3.Visible = false;
                }
                if (objProposalList[3].strFileName.ToString() != "")
                {

                    string[] strarr = objProposalList[3].strFileName.ToString().Split(',');
                    if (strarr[0] != "")
                    {
                        hlDoc4.NavigateUrl = "../../QueryFiles/" + strarr[0];
                        pdficon4.Visible = true;
                        divfile2.Visible = true;
                    }
                    if (strarr[1] != "")
                    {
                        hlDoc5.NavigateUrl = "../../QueryFiles/" + strarr[1];
                        pdficon5.Visible = true;
                        divfile2.Visible = true;
                    }
                    if (strarr[2] != "")
                    {
                        hlDoc6.NavigateUrl = "../../QueryFiles/" + strarr[2];
                        pdficon6.Visible = true;
                        divfile2.Visible = true;
                    }
                }
            }
            else if (objProposalList.Count == 0)
            { divQ1.Visible = true; lblq1.Visible = false; div1stcnt.Visible = true; div2ndcnt.Visible = false; }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProposal = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            objProposal.strAction = "Q";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.strProposalNo = Request.QueryString["Pno"].ToString();
            objProposal.intStatus = 5;
            if (hdnNoofrecord.Value == "0")
            {
                objProposal.strRemarks = txtq1.Text;
            }
            else { objProposal.strRemarks = txtq2.Text; }

            string strRetVal = objService.ProposalRaiseQuery(objProposal);

            if (strRetVal == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Record Saved Successfully.')</script>;", false);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');document.location.href='ViewProposal.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'", false);
            }
            else if (strRetVal == "4")
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Action can not be taken Successfully.')</script>;", false); }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProposal = null;
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
                lblISDPHNo.Text = "+" + objProposalList[0].intISDPHNo.ToString();
                lblPhoneNo.Text = objProposalList[0].vchPhoneNo.ToString();
                if (objProposalList[0].vchFaxNo.ToString() != "")
                {
                    lblFaxNo.Text = objProposalList[0].vchFaxNo.ToString();
                    lblISDFXNo.Text = "+" + objProposalList[0].intISDFXNo.ToString();
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
                lblISDMOB.Text = "+" + objProposalList[0].intISDMOBo.ToString();
                lblCorMobileNo.Text = objProposalList[0].vchCorMobileNo.ToString();
                if (objProposalList[0].vchCorFaxNo.ToString() != "")
                {
                    lblFaxCordet.Text = "+" + objProposalList[0].intFaxCordet.ToString();
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
                }
                else
                {
                    dvPromoter.Visible = false;
                }
                if (lblOtheConstituition.Text == "Private Limited Company")
                {
                    dvBoard.Visible = true;
                    GetPromoDesignation();
                }
                else
                {
                    dvBoard.Visible = false;
                }
                if (lblOtheConstituition.Text == "Partnership")
                {
                    dvPartnership.Visible = true;
                    lblNumberOfPartner.Text = objProposalList[0].intNumberOfPartner.ToString();
                    lblManagPartner.Text = objProposalList[0].vchManagPartner.ToString();
                }
                else
                {
                    dvPartnership.Visible = false;
                }
                lblExperience.Text = objProposalList[0].intExpInYr.ToString();

                ////Company Reg details
                lblYearIncorp.Text = objProposalList[0].intYearOfIncorporation.ToString();
                lblPlaceIncor.Text = objProposalList[0].vchPlaceIncor.ToString();
                lblGSTIN.Text = objProposalList[0].vchGSTIN.ToString();

                hdnProjType.Value = objProposalList[0].intProjectType.ToString();
                if (hdnProjType.Value == "1")
                {
                    lblProjType.Text = "Large";
                    dvPrjMSME.Visible = false;
                    dvmaterial.Visible = false;
                }
                else
                {
                    lblProjType.Text = "MSME";
                    GetRawMaterialDetails();
                    dvPrjMSME.Visible = true;
                    dvmaterial.Visible = true;
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
                int FinYearN1 = FinYear1 + 1;

                lblFinYear1.Text = FinYear1 + "-" + FinYearN1;

                hdnFinYear2.Value = objProposalList[0].intFyn2.ToString();
                int FinYear2 = Convert.ToInt32(hdnFinYear2.Value);
                int FinYearN2 = FinYear2 + 1;

                lblFinYear2.Text = FinYear2 + "-" + FinYearN2;

                hdnFinYear3.Value = objProposalList[0].intFyn3.ToString();

                int FinYear3 = Convert.ToInt32(hdnFinYear3.Value);
                int FinYearN3 = FinYear3 + 1;

                lblFinYear3.Text = FinYear3 + "-" + FinYearN3;

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
                    hplnkGstinFile.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchGSTNfile);
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
                    HyperLink1.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchEduQualifile);
                }
                if (Convert.ToString(objProposalList[0].vchTechniQualifile) != "")
                {
                    hdnTecnical.Value = Convert.ToString(objProposalList[0].vchTechniQualifile);
                    HyperLink2.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchTechniQualifile);
                }
                if (Convert.ToString(objProposalList[0].vchExpFile) != "")
                {
                    hdnExperience.Value = Convert.ToString(objProposalList[0].vchExpFile);
                    hlDoc7.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchExpFile);
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
                    lblEin.Text = "Udyog Aadhar :";
                    lblEIM.Text = "Udyog Aadhar :";
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
                    lblProjectComing.Text = "Priority Sectior";
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
                    hplnkMnfprocess.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchManufacturingProcessFlow);

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
            objProp.vchProposalNo = Request.QueryString["Pno"].ToString();
            objProjList = objService.getProjectLOCDetails(objProp).ToList();
            gvLOCDetails.DataSource = objProjList;
            gvLOCDetails.DataBind();
            if (objProjList.Count > 0)
            {
                lblProjectsLocation.Text = "Yes";
            }
            else
            {
                lblProjectsLocation.Text = "No";
            }

            objProp.strAction = "E2";
            objProp.vchProposalNo = Request.QueryString["Pno"].ToString();
            objProjList = objService.getOtherUnitlDetails(objProp).ToList();
            gvOtherUnits.DataSource = objProjList;
            gvOtherUnits.DataBind();
            if (objProjList.Count > 0)
            {
                lblUnitOutSide.Text = "Yes";
            }
            else
            {
                lblUnitOutSide.Text = "No";
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
                if (objProposalList[0].sintLandRequiredIDCO.ToString() == "1")
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
                lblSurface.Text = objProposalList[0].vchSurfaceWater.ToString();
                lblIdcoSupply.Text = objProposalList[0].vchIdcoSupply.ToString();
                lblRainWtrHarvesting.Text = objProposalList[0].vchRainWtrHarvesting.ToString();
                lblother.Text = objProposalList[0].vchother.ToString();

                if (lblother.Text != "")
                {
                    lblsourceOther.Text = objProposalList[0].vchOtherSpecify.ToString();
                }
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

    #region "Added By Pranay Kumar on 10-Sept-2017"
    #region "Show/Hide Raise Query Button"
    private void CheckRaiseQueryStatus()
    {
        ObjPropasal = new ProposalDet();
        try
        {
            int intStatus = 0;
            ObjPropasal.strAction = "RQ";
            ObjPropasal.strProposalNo = Request.QueryString["Pno"].ToString();
            intStatus = objService.CheckRaiseQStatus(ObjPropasal);
            if (intStatus == 1)
            {
                LinkButton1.Visible = true;
            }
            else
            {
                LinkButton1.Visible = false;
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
    #endregion

}