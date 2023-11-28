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

public partial class Proposal_ProposalDetails : SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();

    PromoterDet objPromoDet = new PromoterDet();
    LandDet objLandDet = new LandDet();
    ProposalDet ObjPropasal = new ProposalDet();

    string strRetval = "";
    int intRetVal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) // Add by anil sahoo
        {
            Response.Redirect("~/portal/SessionRedirect.aspx", false);
            return;
        }

        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["Pno"] != null)
                {
                    GetCompanyInfo();
                    GetGroupOfCompanyDetails();//// Added by Sushant Jena On Dt:-27-Aug-2019
                    GetLandDetails();
                    GetEnclosureList();
                    getProjectInfo();
                    GetQueryDetails();
                    GetActionDetails(); //satya added                    
                    GetProductDetails();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ProposalDetails");
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
    //private void BindData()
    //{
    //    objProposal = new ProposalDet();
    //    try
    //    {
    //        objProposal.strAction = "E";
    //        objProposal.strProposalNo = Request.QueryString["Pno"].ToString();
    //        List<ProposalDet> objProposalList = objService.getRaisedQueryDetails(objProposal).ToList();
    //        hdnNoofrecord.Value = objProposalList.Count.ToString();
    //        if (objProposalList.Count == 0)
    //        { div1stcnt.Visible = true; }
    //        else if (objProposalList.Count == 2)
    //        { div2ndcnt.Visible = true; }
    //        else
    //        { div1stcnt.Visible = false; div2ndcnt.Visible = false; }

    //        if (objProposalList.Count > 0 && objProposalList.Count <=2)
    //        {
    //            divA1.Visible = true;
    //            divQ1.Visible = true;
    //           // divQ2.Visible = true;
    //            lblq1.Text = objProposalList[0].strRemarks.ToString();
    //            txtq1.Visible = false;            

    //            if (objProposalList.Count > 1)
    //            {
    //                divQ2.Visible = true;
    //                lblq2.Visible = false;

    //                lbla1.Text = objProposalList[1].strRemarks.ToString();
    //                if (objProposalList[1].strFileName.ToString() != "")
    //                {
    //                    string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
    //                    if (strarr[0] != "")
    //                    {
    //                        hlDoc1.NavigateUrl = "../../QueryFiles/" + strarr[0];
    //                        pdficon1.Visible = true;
    //                        divfile1.Visible = true;
    //                    }
    //                    if (strarr[1] != "")
    //                    {
    //                        hlDoc2.NavigateUrl = "../../QueryFiles/" + strarr[1];
    //                        pdficon2.Visible = true;
    //                        divfile1.Visible = true;
    //                    }
    //                    if (strarr[2] != "")
    //                    {
    //                        hlDoc3.NavigateUrl = "../../QueryFiles/" + strarr[2];
    //                        pdficon3.Visible = true;
    //                        divfile1.Visible = true;
    //                    }
    //                }
    //            }
    //            else { lbla1.Visible = false; divA1.Visible = false; btnSubmit.Visible = false; btnCancel.Visible = false; }
    //        }
    //        else if (objProposalList.Count > 2 && objProposalList.Count <= 3)
    //        {
    //            divA1.Visible = true;
    //            divQ1.Visible = true;
    //            txtq1.Visible = false;
    //            txtq2.Visible = false;
    //            divQ2.Visible = true;

    //            btnSubmit.Visible = false;
    //            btnCancel.Visible = false;
    //            lblq1.Text = objProposalList[0].strRemarks.ToString();
    //            lbla1.Text = objProposalList[1].strRemarks.ToString();

    //            lblq2.Text = objProposalList[2].strRemarks.ToString();
    //            //txta2.Text = objProposalList[3].strRemarks.ToString();
    //            if (objProposalList[1].strFileName.ToString() != "")
    //            {                   
    //                string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
    //                if (strarr[0] != "")
    //                {
    //                    hlDoc1.NavigateUrl = "../../QueryFiles/" + strarr[0];
    //                    pdficon1.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[1] != "")
    //                {
    //                    hlDoc2.NavigateUrl = "../../QueryFiles/" + strarr[1];
    //                    pdficon2.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[2] != "")
    //                {
    //                    hlDoc3.NavigateUrl = "../../QueryFiles/" + strarr[2];
    //                    pdficon3.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //            }
    //            //if (objProposalList[3].strFileName.ToString() != "")
    //            //{

    //            //    string[] strarr = objProposalList[3].strFileName.ToString().Split(',');
    //            //    if (strarr[0] != "")
    //            //    {
    //            //        hlDoc4.NavigateUrl = "../../SWP_Web/QueryFiles/" + strarr[0];
    //            //        pdficon4.Visible = true;
    //            //        divfile2.Visible = true;
    //            //    }
    //            //    if (strarr[1] != "")
    //            //    {
    //            //        hlDoc5.NavigateUrl = "../../SWP_Web/QueryFiles/" + strarr[1];
    //            //        pdficon5.Visible = true;
    //            //        divfile2.Visible = true;
    //            //    }
    //            //    if (strarr[2] != "")
    //            //    {
    //            //        hlDoc6.NavigateUrl = "../../SWP_Web/QueryFiles/" + strarr[2];
    //            //        pdficon6.Visible = true;
    //            //        divfile2.Visible = true;
    //            //    }
    //            //}

    //        }
    //        else if (objProposalList.Count > 2 && objProposalList.Count <= 4)
    //        {

    //            divA1.Visible = true;
    //            divQ1.Visible = true;
    //            txtq1.Visible = false;
    //            txtq2.Visible = false;
    //            divQ2.Visible = true;
    //            divA2.Visible = true;
    //            btnSubmit.Visible = false;
    //            btnCancel.Visible = false;
    //            lblq1.Text = objProposalList[0].strRemarks.ToString();
    //            lbla1.Text = objProposalList[1].strRemarks.ToString();

    //            lblq2.Text = objProposalList[2].strRemarks.ToString();
    //            lbla2.Text = objProposalList[3].strRemarks.ToString();
    //            if (objProposalList[1].strFileName.ToString() != "")
    //            {

    //                string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
    //                if (strarr[0] != "")
    //                {
    //                    hlDoc1.NavigateUrl = "../../QueryFiles/" + strarr[0];
    //                    pdficon1.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[1] != "")
    //                {
    //                    hlDoc2.NavigateUrl = "../../QueryFiles/" + strarr[1];
    //                    pdficon2.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[2] != "")
    //                {
    //                    hlDoc3.NavigateUrl = "../../QueryFiles/" + strarr[2];
    //                    pdficon3.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //            }

    //            else { pdficon1.Visible = false;
    //            pdficon2.Visible = false;
    //            pdficon3.Visible = false;
    //            }
    //            if (objProposalList[3].strFileName.ToString() != "")
    //            {

    //                string[] strarr = objProposalList[3].strFileName.ToString().Split(',');
    //                if (strarr[0] != "")
    //                {
    //                    hlDoc4.NavigateUrl = "../../QueryFiles/" + strarr[0];
    //                    pdficon4.Visible = true;
    //                    divfile2.Visible = true;
    //                }
    //                if (strarr[1] != "")
    //                {
    //                    hlDoc5.NavigateUrl = "../../QueryFiles/" + strarr[1];
    //                    pdficon5.Visible = true;
    //                    divfile2.Visible = true;
    //                }
    //                if (strarr[2] != "")
    //                {
    //                    hlDoc6.NavigateUrl = "../../QueryFiles/" + strarr[2];
    //                    pdficon6.Visible = true;
    //                    divfile2.Visible = true;
    //                }
    //            }
    //        }
    //        else if (objProposalList.Count==0)
    //        { divQ1.Visible = true; lblq1.Visible = false; div1stcnt.Visible = true; div2ndcnt.Visible = false; }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objProposal = null;
    //    }
    //}
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{

    //    try
    //    {           
    //        objProposal.strAction = "Q";
    //        objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
    //        objProposal.strProposalNo = Request.QueryString["Pno"].ToString();
    //        objProposal.intStatus =5;
    //        if (hdnNoofrecord.Value == "0")
    //        {
    //            objProposal.strRemarks = txtq1.Text;
    //        }
    //        else { objProposal.strRemarks = txtq2.Text; }

    //        string strRetVal = objService.ProposalRaiseQuery(objProposal);

    //        if (strRetVal == "2")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Record Saved Successfully.')</script>;", false);
    //           //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');document.location.href='ViewProposal.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'", false);
    //        }
    //        else if (strRetVal == "4")
    //        { ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Action can not be taken Successfully.')</script>;", false); }


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objProposal = null;
    //    }
    //}

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
                Lbl_Proposal_No.Text = objPromoDet.vchProposalNo; //// Added by Sushant Jena on Dt:-19-Aug-2019
                Lbl_Application_Date.Text = string.Format("{0:dd-MMM-yyyy}", objProposalList[0].dtmApplicationDate); //// Added by Sushant Jena on Dt:-19-Aug-2019

                lblCompName.Text = objProposalList[0].vchCompName.ToString();
                //lblAddress.Text = objProposalList[0].vchAddress.ToString();
                if (objProposalList[0].vchAddress.ToString() != "")//Add by Debiprasanna on 30-12-2022
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
                    lblFaxNo.Text ="-NA-";
                    lblISDFXNo.Text = "";
                }
                if (objProposalList[0].vchEmail.ToString() !="")//Add by Debiprasanna on 30-12-2022 
                {
                    lblEmail.Text = objProposalList[0].vchEmail.ToString();
                }
                else
                {
                    lblEmail.Text = "-NA-";
                }
                
                //lblPin.Text = objProposalList[0].intPin.ToString();
                if (objProposalList[0].intPin.ToString() != "")//Add by Debiprasanna on 30-12-2022 
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
                if (objProposalList[0].vchCorMobileNo.ToString() != "")//Add by Debiprasanna on 30-12-2022
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
                    lblCorFaxNo.Text = "-NA-";
                }

                //lblCorEmail.Text = objProposalList[0].vchCorEmail.ToString();
                if (objProposalList[0].vchCorEmail.ToString() != "")//Add by Debiprasanna on 30-12-2022
                {
                    lblCorEmail.Text = objProposalList[0].vchCorEmail.ToString();
                }
                else
                {
                    lblCorEmail.Text = "-NA-";
                }
                // lblCorrPin.Text = objProposalList[0].intCorPin.ToString();
                if (objProposalList[0].intCorPin.ToString() != "")//Add by Debiprasanna on 30-12-2022
                {
                    lblCorrPin.Text = objProposalList[0].intCorPin.ToString();
                }
                else
                {
                    lblCorrPin.Text = "-NA-";
                }

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
                    lblf1.Text = "Upload Audited Financial Statements for First Year.";
                    lblf2.Text = "Upload Audited Financial Statements for Second Year.";
                    lblf3.Text = "Upload Audited Financial Statements for Third Year.";
                    lblf4.Text = "Net worth certified by CA.";
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
                    //Add by Debiprasanna on Dt:18-04-2023//
                    lblf1.Text = "Networth Certificate of the Proprietor duly certified by CA for Current/latest year.";
                    lblf2.Text = "Tax Audit Report(if applicable) for Current/latest year. ";
                    lblf3.Text = "Income tax return for Current/latest year.";
                    lblf4.Text = "Net worth certified by CA.";

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
                    DVC1.Visible = true;
                    DVC2.Visible = false;
                    DVC3.Visible = false;
                    DVC4.Visible = true;
                    lblf1.Text = "Partnership deed.";//Add by Debiprasanna on Dt:18-04-2023
                    lblf2.Text = "Complete balance sheet of the firm(latest 3 years).";
                    lblf3.Text = "Tax audit report of the Partnership firm.";
                    lblf4.Text = "Income tax return.";
                    lblResSur.Visible = false;
                    lblShaCap.Visible = false;
                    
                   
                }
                else
                {
                    dvPartnership.Visible = false;
                }
               if(lblOtheConstituition.Text== "Private Limited Company")//Add by Debiprasanna on Dt:18-04-2023
                {
                    lblf1.Text = "Upload Audited Financial Statements for First Year.";
                    lblf2.Text = "Upload Audited Financial Statements for Second Year.";
                    lblf3.Text = "Upload Audited Financial Statements for Third Year.";
                    lblf4.Text = "Net worth certified by CA.";
                }
                if (lblOtheConstituition.Text == "Public Limited Company")//Add by Debiprasanna on Dt:18-04-2023
                {
                    lblf1.Text = "Upload Audited Financial Statements for First Year.";
                    lblf2.Text = "Upload Audited Financial Statements for Second Year.";
                    lblf3.Text = "Upload Audited Financial Statements for Third Year.";
                    lblf4.Text = "Net worth certified by CA.";
                }
                if (lblOtheConstituition.Text == "PSU")//Add by Debiprasanna on Dt:18-04-2023
                {
                    lblf1.Text = "Upload Audited Financial Statements for First Year.";
                    lblf2.Text = "Upload Audited Financial Statements for Second Year.";
                    lblf3.Text = "Upload Audited Financial Statements for Third Year.";
                    lblf4.Text = "Net worth certified by CA.";
                }



                if (lblOtheConstituition.Text == "SPV")
                {
                    DVC1.Visible = true;
                    DVC2.Visible = true;
                    DVC3.Visible = false;
                    DVC4.Visible = true;
                    lblf1.Text = "Upload Audited Financial Statements for First Year.";//Add by Debiprasanna on Dt:18-04-2023
                    lblf2.Text = "Upload Audited Financial Statements for Second Year.";
                    lblf3.Text = "Upload Audited Financial Statements for Third Year.";
                    lblf4.Text = "Net worth certified by CA.";
                }

                if (lblOtheConstituition.Text == "Co-operative")
                {
                    DVC1.Visible = true;
                    DVC2.Visible = true;
                    DVC3.Visible = false;
                    DVC4.Visible = true;
                    lblf1.Text = "Upload Audited Financial Statements for First Year.";//Add by Debiprasanna on Dt:18-04-2023
                    lblf2.Text = "Upload Audited Financial Statements for Second Year.";
                    lblf3.Text = "Upload Audited Financial Statements for Third Year.";
                    lblf4.Text = "Net worth certified by CA.";
                }

                lblExperience.Text = objProposalList[0].intExpInYr.ToString();

                ////Company Reg details
                //lblYearIncorp.Text = objProposalList[0].intYearOfIncorporation.ToString();
                if (objProposalList[0].intYearOfIncorporation.ToString() != "")//Add by Debiprasanna on 30-12-2022
                {
                    lblYearIncorp.Text = objProposalList[0].intYearOfIncorporation.ToString();
                }
                else
                {
                    lblYearIncorp.Text = "-NA-";
                }
                //lblPlaceIncor.Text = objProposalList[0].vchPlaceIncor.ToString();
                if (objProposalList[0].vchPlaceIncor.ToString() != "")//Add by Debiprasanna on 30-12-2022
                {
                    lblPlaceIncor.Text = objProposalList[0].vchPlaceIncor.ToString();
                }
                else
                {
                    lblPlaceIncor.Text = "-NA-";
                }
                //lblGSTIN.Text = objProposalList[0].vchGSTIN.ToString();
                if (objProposalList[0].vchGSTIN.ToString() != "")//Add by Debiprasanna on 30-12-2022
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

                if (lblOtheConstituition.Text == "Partnership")
                {
                    dvNetWorth.Visible = true;

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

                //lblExistInd.Text = objProposalList[0].vchExisIndName.ToString();
                if (objProposalList[0].vchExisIndName.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblExistInd.Text = objProposalList[0].vchExisIndName.ToString();
                }
                else
                {
                    lblExistInd.Text = "-NA-";
                }
                //lblDistrictName.Text = objProposalList[0].vchDistrictName.ToString();
                if (objProposalList[0].vchDistrictName.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblDistrictName.Text = objProposalList[0].vchDistrictName.ToString();
                }
                else
                {
                    lblDistrictName.Text = "-NA-";
                }
                //lblBlock.Text = objProposalList[0].vchBlockName.ToString();
                if (objProposalList[0].vchBlockName.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblBlock.Text = objProposalList[0].vchBlockName.ToString();
                }
                else
                {
                    lblBlock.Text = "-NA-";
                }

                if (objProposalList[0].intAllotedBy.ToString() == "1")
                {
                    lblLandAllotIDCO.Text = "Yes";
                }
                else
                {
                    lblLandAllotIDCO.Text = "No";
                }

                //lblExtentLand.Text = objProposalList[0].vchlandInAcres.ToString();
                if (objProposalList[0].vchlandInAcres.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblExtentLand.Text = objProposalList[0].vchlandInAcres.ToString();
                }
                else
                {
                    lblExtentLand.Text = "-NA-";
                }

                lblNatActivity.Text = objProposalList[0].vchNatureAct.ToString();
               // lblSector.Text = objProposalList[0].vchSector.ToString();
                if (objProposalList[0].vchSector.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblSector.Text = objProposalList[0].vchSector.ToString();
                }
                else
                {
                    lblSector.Text = "-NA-";
                }
                //lblSubSector.Text = objProposalList[0].vchSubSector.ToString();
                if (objProposalList[0].vchSubSector.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblSubSector.Text = objProposalList[0].vchSubSector.ToString();
                }
                else
                {
                    lblSubSector.Text = "-NA-";
                }
                //lblCapacity.Text = objProposalList[0].vchCapacity.ToString();
                if (objProposalList[0].vchCapacity.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblCapacity.Text = objProposalList[0].vchCapacity.ToString();
                }
                else
                {
                    lblCapacity.Text = "-NA-";
                }
                lblUnitCapacity.Text = objProposalList[0].vchCapacityUnit.ToString();

                if (objProposalList[0].vchCapacityUnit.ToString() == "Other")
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
                if (Convert.ToString(objProposalList[0].vchPanfile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchPanfile)) != true))
                {
                    hdnPanFile.Value = Convert.ToString(objProposalList[0].vchPanfile);
                    hplnkPan.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchPanfile);
                    lblPanLink.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblpanLabel.Visible = false;
                }
                else
                {
                    lblPanLink.Visible = false;
                    lblpanLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchGSTNfile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchGSTNfile)) != true))
                {
                    hdnGstinFile.Value = Convert.ToString(objProposalList[0].vchGSTNfile);
                    hplnkGstin.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchGSTNfile);
                    lblGstLink.Visible = true;//add by Debiprasanna Dt-06-09-22
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
                    lblMemoLink.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblMemoLabel.Visible = false;
                    DVC3.Visible = true;
                }
                else
                {
                    lblMemoLink.Visible = false;
                    lblMemoLabel.Visible = true;
                }
                if (Convert.ToString(objProposalList[0].vchCertificateincorpfile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchCertificateincorpfile)) != true))
                {
                    hdnCerti.Value = Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                    hplnkCerti.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchCertificateincorpfile);
                    lblLinkCert.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblCertLabel.Visible = false;
                    DVC4.Visible = true;
                }
                else
                {
                    lblLinkCert.Visible = false;
                    lblCertLabel.Visible = true;
                }


                if (Convert.ToString(objProposalList[0].vchEduQualifile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchEduQualifile)) != true))
                {
                    hdnEdu.Value = Convert.ToString(objProposalList[0].vchEduQualifile);
                    hplnkEdu.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchEduQualifile);
                    lblLinkEdu.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblEduLabel.Visible = false;
                }
                else
                {
                    lblLinkEdu.Visible = false;
                    lblEduLabel.Visible = true;
                }
            
                if (Convert.ToString(objProposalList[0].vchTechniQualifile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchTechniQualifile)) != true))
                {
                    hdnTecnical.Value = Convert.ToString(objProposalList[0].vchTechniQualifile);
                    hplnkTechQ.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchTechniQualifile);
                    lblLinkTechq.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblTechqLabel.Visible = false;
                }
                else
                {
                    lblLinkTechq.Visible = false;
                    lblTechqLabel.Visible = true;
                }
            
                if (Convert.ToString(objProposalList[0].vchExpFile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchExpFile)) != true))
                {
                    hdnExperience.Value = Convert.ToString(objProposalList[0].vchExpFile);
                    hplnkExperience.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchExpFile);
                    lblLinkExperience.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblExperienceLabel.Visible = false;
                }
                else
                {
                    lblLinkExperience.Visible = false;
                    lblExperienceLabel.Visible = true;
                }
            
                if (Convert.ToString(objProposalList[0].vchAuditFile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchAuditFile)) != true))
                {
                    hdnAudit.Value = Convert.ToString(objProposalList[0].vchAuditFile);
                    hplnkAudit.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFile);
                    lblLinkAudit.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblAuditLabel.Visible = false;

                }
                else
                {
                    lblLinkAudit.Visible = false;
                    lblAuditLabel.Visible = true;
                }
            
                if (Convert.ToString(objProposalList[0].vchAuditFileSecondYrs) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchAuditFileSecondYrs)) != true))
                {
                    hdnFySecond.Value = Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);
                    hplnkFySecond.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileSecondYrs);
                    lblLinkFySecond.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblFySecondLabel.Visible = false;
                }
                else
                {
                    lblLinkFySecond.Visible = false;
                    lblFySecondLabel.Visible = true;
                }
            
                if (Convert.ToString(objProposalList[0].vchAuditFileThrdYrs) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchAuditFileThrdYrs)) != true))
                {
                    hdnFyThird.Value = Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);
                    hplnkFyThird.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchAuditFileThrdYrs);
                    lblLinkFyThird.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblFyThirdLabel.Visible = false;
                }
                else
                {
                    lblLinkFyThird.Visible = false;
                    lblFyThirdLabel.Visible = true;
                }

             if(lblOtheConstituition.Text== "Partnership")
                {
                    if (Convert.ToString(objProposalList[0].strIncomeTaxReturn) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].strIncomeTaxReturn)) != true))
                    {
                        hdnNetWorth.Value = Convert.ToString(objProposalList[0].strIncomeTaxReturn);
                        hplnkNetWorth.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strIncomeTaxReturn);
                        lblLinkNetWorth.Visible = true;//add by Debiprasanna Dt-06-09-22
                        lblNetWorthLabel.Visible = false;
                    }
                    else
                    {
                        lblLinkNetWorth.Visible = false;
                        lblNetWorthLabel.Visible = true;

                    }
                   
             }
                else
                {
                    if (Convert.ToString(objProposalList[0].vchNetWorthfile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchNetWorthfile)) != true))
                    {
                        hdnNetWorth.Value = Convert.ToString(objProposalList[0].vchNetWorthfile);
                        hplnkNetWorth.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchNetWorthfile);
                        lblLinkNetWorth.Visible = true;//add by Debiprasanna Dt-06-09-22
                        lblNetWorthLabel.Visible = false;
                    }
                    else
                    {
                        lblLinkNetWorth.Visible = false;
                        lblNetWorthLabel.Visible = true;
                    }
                }

            }


            //if (Convert.ToString(objProposalList[0].vchNetWorthfile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchNetWorthfile)) != true))
            //    {
            //        hdnNetWorth.Value = Convert.ToString(objProposalList[0].vchNetWorthfile);
            //        hplnkNetWorth.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchNetWorthfile);
            //        lblLinkNetWorth.Visible = true;//add by Debiprasanna Dt-06-09-22
            //        lblNetWorthLabel.Visible = false;
            //    }
            //    else
            //    {
            //        lblLinkNetWorth.Visible = false;
            //        lblNetWorthLabel.Visible = true;
            //    }
            //}
            
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
                else if (hdnEin.Value == "5")
                {
                    lblEin.Text = "Udayam Aadhar";
                    lblEIM.Text = "Udayam Aadhar";
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

                if (Convert.ToString(objProjList[0].vchIndustryInterprenur) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProjList[0].vchIndustryInterprenur)) != true))
                {
                    hdnIndustryEntMemorandum.Value = Convert.ToString(objProjList[0].vchIndustryInterprenur);
                    hplnkIndustryEntMemorandum.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchIndustryInterprenur);
                    lblLinkEIM.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblEimLabel.Visible = false;
                }
                else
                {
                    lblLinkEIM.Visible = false;
                    lblEimLabel.Visible = true;
                }
            

                if (Convert.ToString(objProjList[0].vchManufacturingProcessFlow) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProjList[0].vchManufacturingProcessFlow)) != true))
                {
                    hdnFileMnfprocess.Value = Convert.ToString(objProjList[0].vchManufacturingProcessFlow);
                    hplnkFileMnfprocess.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchManufacturingProcessFlow);
                    lblLinkFileMnfprocess.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblFileMnfprocessLabel.Visible = false;
                }
                else
                {
                    lblLinkFileMnfprocess.Visible = false;
                    lblFileMnfprocessLabel.Visible = true;
                }
            

                if (Convert.ToString(objProjList[0].vchFeasibilityReport) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProjList[0].vchFeasibilityReport)) != true))
                {
                    hdnFeasibilityReport.Value = Convert.ToString(objProjList[0].vchFeasibilityReport);
                    hplnkFeasibilityReport.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchFeasibilityReport);
                    lblLinkFeasibilityReport.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblFeasibilityReportLabel.Visible = false;
                }
                else
                {
                    lblLinkFeasibilityReport.Visible = false;
                    lblFeasibilityReportLabel.Visible = true;
                }

                if (Convert.ToString(objProjList[0].vchBoardResolution) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProjList[0].vchBoardResolution)) != true))
                {
                    hdnBoardResolution.Value = Convert.ToString(objProjList[0].vchBoardResolution);
                    hplnkBoardResolution.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchBoardResolution);
                    lblLinkBoardResolution.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblBoardResolutionLabel.Visible = false;
                }
                else
                {
                    lblLinkBoardResolution.Visible = false;
                    lblBoardResolutionLabel.Visible = true;
                }

                if (Convert.ToString(objProjList[0].vchSourceOfFinance) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProjList[0].vchSourceOfFinance)) != true))
                {
                    hdnOtherFin.Value = Convert.ToString(objProjList[0].vchSourceOfFinance);
                    hplnkOtherFin.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchSourceOfFinance);
                    lblLinkOtherFin.Visible = true;//add by Debiprasanna Dt-06-09-22
                    lblOtherFinLabel.Visible = false;
                }
                else
                {
                    lblLinkOtherFin.Visible = false;
                    lblOtherFinLabel.Visible = true;
                }

            }

            /*----------------------------------------------------------*/

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

            /*----------------------------------------------------------*/

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
                   // DivLandRecom.Visible = true;  // Add by Anil Sahoo
                }
                else
                {
                    lblLandRequired.Text = "No";
                    dvLandReq.Visible = false;
                   // DivLandRecom.Visible = false;  // Add by Anil Sahoo
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

                if (objProposalList[0].decPowerDemandGrid.ToString().Trim() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblLoadGrid.Text = objProposalList[0].decPowerDemandGrid.ToString();
                }
                else
                {
                    lblLoadGrid.Text = "-NA-";
                }
                if (objProposalList[0].decPowerDrawalCPP.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblLoadCPP.Text = objProposalList[0].decPowerDrawalCPP.ToString();
                }
                else
                {
                    lblLoadCPP.Text = "-NA-";
                }
                if (objProposalList[0].decCapacityofCPPPlant.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblCPPCapacity.Text = objProposalList[0].decCapacityofCPPPlant.ToString();
                }
                else
                {
                    lblCPPCapacity.Text = "-NA-";
                }
                if (objProposalList[0].DecPowerProducerIpp.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    LblPowerDemandIPP.Text = objProposalList[0].DecPowerProducerIpp.ToString(); /////Added by Sushant Jena On Dt:-24-Aug-2021
                }
                else
                {
                    LblPowerDemandIPP.Text = "-NA-";
                }
                if (objProposalList[0].decWaterRequireExist.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {

                    lblWaterRequireExist.Text = objProposalList[0].decWaterRequireExist.ToString();
                }
                else
                {
                    lblWaterRequireExist.Text = "-NA-";
                }
                if (objProposalList[0].decWaterReqireProposed.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblWaterReqireProposed.Text = objProposalList[0].decWaterReqireProposed.ToString();
                }
                else
                {
                    lblWaterReqireProposed.Text = "-NA-";
                }
                if (objProposalList[0].decWaterRequirProduct.ToString() != "")//Add by Debiprasanna Dt-06-09-22
                {
                    lblWaterReq.Text = objProposalList[0].decWaterRequirProduct.ToString();
                }
                else
                {
                    lblWaterReq.Text = "-NA-";
                }
                //lblQuantum.Text = objProposalList[0].vchQuntRecyllingWaste.ToString();
                if (objProposalList[0].vchQuntRecyllingWaste.ToString() == " ")//Add by Debiprasanna Dt-06-09-22
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
                    strWaterSource=strWaterSource+ objProposalList[0].vchSurfaceWater.ToString() + ", ";
                    //lblSurface.Text = objProposalList[0].vchSurfaceWater.ToString() + ",";
                }
               

                if (objProposalList[0].vchIdcoSupply.ToString().Trim() != "")
                {
                    strWaterSource = strWaterSource + objProposalList[0].vchIdcoSupply.ToString() + ", ";
                   // lblIdcoSupply.Text = objProposalList[0].vchIdcoSupply.ToString() + ",";
                }                

                if (objProposalList[0].vchRainWtrHarvesting.ToString().Trim() != "" )
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

                lblSurface.Text = "-NA-";

                /*-----------------------------------------------------------------------------------*/

                if (Convert.ToString(objProposalList[0].vchWasteConserFile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchWasteConserFile)) != true))
                {
                    hdnWaterFile.Value = Convert.ToString(objProposalList[0].vchWasteConserFile);
                    hplnkWaterFile.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchWasteConserFile);
                    lblLinkWaterFile.Visible = true;//Add by Debiprasanna Dt-06-09-22
                    lblWaterFileLabel.Visible = false;
                }
                else
                {
                    lblLinkWaterFile.Visible = false;
                    lblWaterFileLabel.Visible = true;
                }

                if (Convert.ToString(objProposalList[0].vchWaterHazardousFile) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].vchWasteConserFile)) != true))
                {
                    hdnHazardousFile.Value = Convert.ToString(objProposalList[0].vchWaterHazardousFile);
                    hplnkHazardousFile.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].vchWaterHazardousFile);
                    lblLinkHazardousFile.Visible = true;//Add by Debiprasanna Dt-06-09-22
                    lblHazardousFileLabel.Visible = false;
                }
                else
                {
                    lblLinkHazardousFile.Visible = false;
                    lblHazardousFileLabel.Visible = true;

                }

                if (Convert.ToString(objProposalList[0].strProjectLandStmt) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].strProjectLandStmt)) != true))
                {
                    hdnLandUsestmt.Value = Convert.ToString(objProposalList[0].strProjectLandStmt);
                    hypProjectlandStatement.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strProjectLandStmt);
                    lblLinkProjectlandStatement.Visible = true;//Add by Debiprasanna Dt-06-09-22
                    lblProjectlandStatementLablel.Visible = false;
                }
                else
                {
                    lblLinkProjectlandStatement.Visible = false;
                    lblProjectlandStatementLablel.Visible = true;
                }
            

                if (Convert.ToString(objProposalList[0].strProjectLayOut) != "" && (string.IsNullOrWhiteSpace(Convert.ToString(objProposalList[0].strProjectLayOut)) != true))
                {
                    hdnLayOutPln.Value = Convert.ToString(objProposalList[0].strProjectLayOut);
                    hypProjectLaoutPlan.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProposalList[0].strProjectLayOut);
                    lblLinkProjectLaoutPlan.Visible = true;//Add by Debiprasanna Dt-06-09-22
                    lblProjectLaoutPlanLabel.Visible = false;
                }
                else
                {
                    lblLinkProjectLaoutPlan.Visible = false;
                    lblProjectLaoutPlanLabel.Visible = true;
                }
            
                HdnLandRequr.Value= objProposalList[0].bitLandRequired.ToString(); //Add Anil Sahoo
                if(HdnLandRequr.Value =="True") // Add Anil sahoo for Land Recommened details
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
    private void GetActionDetails()// GetActionDetails satya added
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