using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Data;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using DataAcessLayer.Common;

public partial class incentives_FormPreview_PostGPSS : SessionCheck
{

    #region Common Member
    Incentive objIncentive = new Incentive();
    string intCreatedBy;
    DataTable dtSalutation;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            FillFormDetails();
            TRVisibility();




        }
    }

    #region Fill Form Details
    public void FillFormDetails()
    {
        try
        {
            objIncentive = new Incentive();
            objIncentive.GetVwPrmtrs = new GetAndViewParam();
            objIncentive.GetVwPrmtrs.Param1ID = "";
            objIncentive.GetVwPrmtrs.Param2ID = "";
            objIncentive.GetVwPrmtrs.Param3ID = "";
            objIncentive.GetVwPrmtrs.Param4ID = "";
            objIncentive.GetVwPrmtrs.InctType = 4;
            objIncentive.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);

            objIncentive.FormType = FormNumber.GrantprioritySector_19;  // Chage the pAGE NUMBER
            DataSet dslive = IncentiveManager.GetGrantPriority(objIncentive);



            #region Fill Priority Sector Details
            DataTable dtPrioritySector = dslive.Tables[0];
            FillPrioritySector(dtPrioritySector);
            #endregion

            #region Fill Additional Document
            DataTable dtAdditionalDoc = dslive.Tables[1];
            DataTable dtAdditionalDocDtl = dslive.Tables[2];
            FillAdditionalDoc(dtAdditionalDoc, dtAdditionalDocDtl);
            #endregion
            #region Fill dtDLSWCA
            DataTable dtDLSWCA = dslive.Tables[3];
            FillDLSWCADetails(dtDLSWCA);
            #endregion


            #region IncentivesAvailedinEarlierIPRs
            DataTable dtBriefDetails = dslive.Tables[4];
            AvailedinEarlierIPRs(dtBriefDetails);
            #endregion

            #region "Dynamic Form Name"

            DataTable dtdynamic = dslive.Tables[6];
            if (dtdynamic.Rows.Count > 0)
            {
                lblTitle.Text = "Application For " + dtdynamic.Rows[0]["vchInctName"].ToString();
                //lblTitleH.Text = "Application For " + dtdynamic.Rows[0]["vchInctName"].ToString();
            }


            #endregion

            #region "For Header visiblity"

            DataTable dtMainTable = dslive.Tables[5];
            string creatby = dtMainTable.Rows[0]["INTCREATEDBY"].ToString();
            if (creatby != Convert.ToString(Session["InvestorId"]))
            {
                HdnValueFlag.Value = "1";
            }
            else
            {
                if ((dtMainTable.Rows[0]["BITFLAG"].ToString().Trim() == "1"))
                {
                    HdnValueFlag.Value = "1";
                }

            }
            string bitflag = dtMainTable.Rows[0]["BITFLAG"].ToString();
            if (bitflag == "1")
            {
                if (dtMainTable.Rows[0]["VCHSIGNATURE"].ToString() != "")
                {
                    PreviewImage.Attributes.Add("src", "../incentives/Files/Signature/" + dtMainTable.Rows[0]["VCHSIGNATURE"].ToString());
                    PreviewImage.Attributes.Add("style", "display:block");
                }
            }

            lblcurdt.Text = dtMainTable.Rows[0]["DTMCREATEDBY"].ToString();


            #endregion

            DataTable dtActivities = dslive.Tables[7];
            if (dtActivities.Rows.Count > 0)
            {
                lblsectoravilability.Text = dtActivities.Rows[0]["actvities"].ToString();
            }



        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion
    #region Fill Priority Sector Details
    public void FillPrioritySector(DataTable dtPrioritySector)
    {
        if (dtPrioritySector.Rows.Count > 0)
        {

            if (dtPrioritySector.Rows[0]["INTCERTAVAIL"].ToString() == "1")
            {
                lblPriority.Text = "Yes";
            }
            else if (dtPrioritySector.Rows[0]["INTCERTAVAIL"].ToString() == "2")
            {
                lblPriority.Text = "Applied";
            }
            else
            {
                lblPriority.Text = "No";
            }
            if (dtPrioritySector.Rows[0]["sectorName"].ToString() != "")
            {
                lblSector.Text = dtPrioritySector.Rows[0]["sectorName"].ToString();
            }
            if (dtPrioritySector.Rows[0]["SubsectorName"].ToString() != "")
            {
                lblSubSector.Text = dtPrioritySector.Rows[0]["SubsectorName"].ToString();
            }

            ddlSpecificActivity.SelectedValue = dtPrioritySector.Rows[0]["INTSPECIFICACTIVITY"].ToString();
            if (dtPrioritySector.Rows[0]["vchpresentnotestage"].ToString() != "")
            {
                HyperLink4.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["vchpresentnotestage"].ToString();
            }

            // dtPrioritySector.Rows[0]["vchpresentnotestage"].ToString();
            lblsectoravilability.Text = ddlSpecificActivity.SelectedItem.Text.ToString();

            if (dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString() != "")
            {
                lnkViewCertificate.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString();
            }
            if (dtPrioritySector.Rows[0]["VCHACKNOW"].ToString() != "")
            {
                lknViewApplAcknow.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["VCHACKNOW"].ToString();
            }
            if (dtPrioritySector.Rows[0]["vchsupportcertificatedoc"].ToString() != "")
            {
                HyperLink1.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["vchsupportcertificatedoc"].ToString();
            }

        }

    }
    #endregion


    public void AvailedinEarlierIPRs(DataTable dtBriefDetails)
    {
        if (dtBriefDetails.Rows.Count > 0)
        {
            grdAvailedEarlier.DataSource = dtBriefDetails;
            grdAvailedEarlier.DataBind();

        }
    }


    #region Fill Additional Document

    public void FillAdditionalDoc(DataTable dtAdditionalDoc, DataTable dtAdditionalDocDtl)
    {

        if (dtAdditionalDoc.Rows.Count > 0)
        {

            if (dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString().Trim() != "")
            {
                HyperLink3.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
            }
            if (dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString().Trim() != "")
            {
                lknUpladView.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
            }
            if (dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString().Trim() != "")
            {
                HyperLink2.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
            }

        }

    }
    #endregion



    public void FillDLSWCADetails(DataTable dtDLSWCA)
    {
        if (dtDLSWCA.Rows.Count > 0)
        {
            if (dtDLSWCA.Rows[0]["DTMAPPROVALDATE"].ToString() != "")
            {
                lblDLSWCADateOfApproval.Text = dtDLSWCA.Rows[0]["DTMAPPROVALDATE"].ToString();
            }
            if (dtDLSWCA.Rows[0]["DCMLANDAPPROVED"].ToString() != "")
            {
                lblDLSWCALandApproved.Text = dtDLSWCA.Rows[0]["DCMLANDAPPROVED"].ToString();
            }
            if (dtDLSWCA.Rows[0]["DCMCOSTOFLAND"].ToString() != "")
            {
                lblDLSWCALandCost.Text = dtDLSWCA.Rows[0]["DCMCOSTOFLAND"].ToString();
            }
            if (dtDLSWCA.Rows[0]["DCMAMOUNTELIGIBLE"].ToString() != "")
            {
                lblDLSWCASubsidyAmt.Text = dtDLSWCA.Rows[0]["DCMAMOUNTELIGIBLE"].ToString();
            }

            if (dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString() != "")
            {


                lnkDLSWCAApprovalDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString();
                lnkDLSWCAApprovalDocView.Visible = true;


            }
            if (dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString() != "")
            {
                hdnDLSWCASubstanDoc.Value = dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();
                lnkDLSWCASubstanDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();

            }

        }

    }
    /// <summary>
    ///  It will create a dynamic header for the gridview.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdMeansOfFinance_rowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow objHeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableHeaderCell objSlNo = new TableHeaderCell();
                objSlNo.Text = "Sl #";
                objSlNo.RowSpan = 2;
                objHeaderRow.Cells.Add(objSlNo);

                TableHeaderCell objZoneRank = new TableHeaderCell();
                objZoneRank.Text = "Name of Financial Institution";
                objZoneRank.RowSpan = 2;
                objHeaderRow.Cells.Add(objZoneRank);
                TableHeaderCell objRLJ = new TableHeaderCell();
                objRLJ.Text = "Location ";
                objRLJ.ColumnSpan = 2;
                objHeaderRow.Cells.Add(objRLJ);
                TableHeaderCell objSP = new TableHeaderCell();
                objSP.Text = "Term Loan Amount";
                objSP.RowSpan = 2;
                objHeaderRow.Cells.Add(objSP);
                TableHeaderCell objOL = new TableHeaderCell();
                objOL.Text = "Sanction Date";
                objOL.RowSpan = 2;
                objHeaderRow.Cells.Add(objOL);
                TableHeaderCell objCPC = new TableHeaderCell();
                objCPC.Text = "Availed Amount";
                objCPC.RowSpan = 2;
                objHeaderRow.Cells.Add(objCPC);
                TableHeaderCell objMobile = new TableHeaderCell();
                objMobile.Text = "Availed Date";
                objMobile.RowSpan = 2;
                objHeaderRow.Cells.Add(objMobile);

                // grdMeansOfFinance.Controls[0].Controls.AddAt(0, objHeaderRow);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// row data bound event for Enforcement grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdMeansOfFinance_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;

            }

        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "jAlert('<strong>Error occured.</strong>', 'STA');", true);
        }

    }


    /// <summary>
    ///  It will create a dynamic header for the gridview.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdWorkingLoan_rowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow objHeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableHeaderCell objSlNo = new TableHeaderCell();
                objSlNo.Text = "Sl #";
                objSlNo.RowSpan = 2;
                objHeaderRow.Cells.Add(objSlNo);

                TableHeaderCell objZoneRank = new TableHeaderCell();
                objZoneRank.Text = "Name of Financial Institution";
                objZoneRank.RowSpan = 2;
                objHeaderRow.Cells.Add(objZoneRank);
                TableHeaderCell objRLJ = new TableHeaderCell();
                objRLJ.Text = "Location ";
                objRLJ.ColumnSpan = 2;
                objHeaderRow.Cells.Add(objRLJ);
                TableHeaderCell objSP = new TableHeaderCell();
                objSP.Text = "Term Loan Amount";
                objSP.RowSpan = 2;
                objHeaderRow.Cells.Add(objSP);
                TableHeaderCell objOL = new TableHeaderCell();
                objOL.Text = "Sanction Date";
                objOL.RowSpan = 2;
                objHeaderRow.Cells.Add(objOL);
                TableHeaderCell objCPC = new TableHeaderCell();
                objCPC.Text = "Availed Amount";
                objCPC.RowSpan = 2;
                objHeaderRow.Cells.Add(objCPC);
                TableHeaderCell objMobile = new TableHeaderCell();
                objMobile.Text = "Availed Date";
                objMobile.RowSpan = 2;
                objHeaderRow.Cells.Add(objMobile);

                //grdWorkingLoan.Controls[0].Controls.AddAt(0, objHeaderRow);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// row data bound event for Enforcement grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdWorkingLoan_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;

            }

        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "jAlert('<strong>Error occured.</strong>', 'STA');", true);
        }

    }


    protected void btnApply_Click(object sender, EventArgs e)
    {
        Incentive objEntity = new Incentive();
        try
        {
            string[] fileext = { ".png", ".jpg", ".jpeg" };
            if (FileUpload1.HasFile)
            {
                if (fileext.Contains(System.IO.Path.GetExtension(FileUpload1.FileName).ToLower()))
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/Signature/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/Signature/"));

                    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string filename = "Signature" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    FileUpload1.SaveAs(Server.MapPath("../incentives/Files/Signature/") + filename);
                    objEntity.Signature = filename;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload .png/.jpg/.jpeg format image only !', 'SWP'); </script>", false);
                    return;
                }
                objEntity.ApprovalAction = "A";
                objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
                int i = IncentiveManager.UpdateSignature(objEntity);
                SMSEMailContent();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + Messages.ShowMessage("1") + "');</script>", false);









            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature. !', 'SWP'); </script>", false);
            }


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    public void PostpopulateDataComm(int id)
    {
        try
        {
            DataSet dslivePre = IncentiveManager.PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan
            dtSalutation = dslivePre.Tables[8];///////////For Mail Id and Mobile number
            ViewState["salutation"] = dtSalutation;

            //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();

            #region IndustrailUnit
            if (dtindustryPre.Rows.Count > 0)
            {

                TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString();
                lblMr.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
                lblName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
                lblAddress.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                lblPresent.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                Label101.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                lblUnitAddress.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                lblApplyBy.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";


                if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                    {
                        TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
                    }
                    divadhhardetails.Visible = true;
                    divAuthorizing.Visible = false;
                }
                else
                {
                    divadhhardetails.Visible = false;
                    divAuthorizing.Visible = true;

                    if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString() != "")
                    {
                        Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODEName"].ToString();
                        lblauthority.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODEName"].ToString();
                        hypAUTHORIZEDFILE.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    }
                }

                //lbl_EnterPrise_Name1.Text = lbl_EnterPrise_Name.Text;
                //dtindustryPre.Rows[0]["intOrganisationType"].ToString();	
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                    //lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();               

                }
                else
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                    //Hid_Org_Doc_Type.Value = "";
                }



                lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                //lblAt.Text = lbl_Industry_Address.Text;
                //Label9.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                //Label9.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                //dtindustryPre.Rows[0]["intUnitCat"].ToString();		
                lbl_Unit_Cat.Text = dtindustryPre.Rows[0]["Unitcategoryname"].ToString();
                Lbl_Pioneer_Doc_Name.Text = dtindustryPre.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();



                dt = (ds1.Tables[1].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                    if (strDocType != "")
                    {
                        Div_Unit_Type_Doc.Visible = true;
                        Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();

                        Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
                    }
                    else
                    {
                        Div_Unit_Type_Doc.Visible = false;
                        Lbl_Unit_Type_Doc_Name.Text = "";

                    }
                }
                else
                {
                    Div_Unit_Type_Doc.Visible = false;
                    Lbl_Unit_Type_Doc_Name.Text = "";

                }




                lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();

                if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
                {
                    lblIs_Priority.Text = "Yes";
                    Pioneersec.Visible = true;
                    DivPioneer.Visible = true;

                }
                else if (dtindustryPre.Rows[0]["intPriority"].ToString() == "3")
                {
                    lblIs_Priority.Text = "Provisional";
                    Pioneersec.Visible = true;
                    DivPioneer.Visible = true;

                }
                else
                {
                    lblIs_Priority.Text = "No";
                    Pioneersec.Visible = false;
                    DivPioneer.Visible = false;

                }




                if (dtindustryPre.Rows[0]["intPioneer"].ToString() == "1")
                {
                    lblIs_Is_Pioneer.Text = "Yes";

                }
                else
                {
                    lblIs_Is_Pioneer.Text = "No";

                }


                Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString();



                lbl_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
                //dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();	
                if (dtindustryPre.Rows[0]["GenderType"].ToString() == "1")
                {
                    lbl_Gender_Partner.Text = "Mr." + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                }
                else
                {
                    lbl_Gender_Partner.Text = "Ms." + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                }

                lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                //dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                //dtindustryPre.Rows[0]["vchCertOfRegdDocCode"].ToString();		


                //dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();
                //dtindustryPre.Rows[0]["vchProdCommCertBeforeCode"].ToString();	
                //Lbl_Prod_Comm_Before_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertBeforeDocName"].ToString();
                //Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchProdCommCertBefore"].ToString();
                //dtindustryPre.Rows[0]["vchProdCommCertBefore"].ToString();

                //if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
                //{
                //    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                //}
                //else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
                //{
                //    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                //}
                if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
                {

                    divbefor.Visible = true;
                    divbefor1.Visible = true;
                    divbefor2.Visible = true;
                    //tr_Prod_Comm_Before.Visible = true;
                    divEmp_Before_Doc_Name.Visible = true;
                    FFCI_Before_Doc_Name.Visible = true;
                    Approved_DPR_Before_Doc_Name.Visible = true;

                }
                else
                {
                    divbefor.Visible = false;
                    divbefor1.Visible = false;
                    divbefor2.Visible = false;
                    //tr_Prod_Comm_Before.Visible = false;
                    divEmp_Before_Doc_Name.Visible = false;
                    FFCI_Before_Doc_Name.Visible = false;
                    Approved_DPR_Before_Doc_Name.Visible = false;
                    lblAfterEMD11.Text = "Date of Production Commencement";
                    lblAfterEMD189.Text = "PC Issurance Date";
                    lbl_PC_No_After.Text = "PC No";
                    lblemd.Text = "";
                    Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                    lblEMDInvestment.Text = "";
                }
                lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();
                lbl_Prod_Comm_Date_After.Text = dtindustryPre.Rows[0]["dtmProdCommAfter"].ToString();
                lbl_PC_Issue_Date_After.Text = dtindustryPre.Rows[0]["dtmPCIssueDateAfter"].ToString();
                //dtindustryPre.Rows[0]["vchProdCommCertAfterCode"].ToString();		
                //Lbl_Prod_Comm_After_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertAfterDocName"].ToString();
                //Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchProdCommCertAfter"].ToString();
                if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
                else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
                //dtindustryPre.Rows[0]["intDistrictCode"].ToString();			
                lbl_District.Text = dtindustryPre.Rows[0]["distname"].ToString();
                Label5.Text = dtindustryPre.Rows[0]["distname"].ToString();

                lblGM.Text = lbl_District.Text;
                //dtindustryPre.Rows[0]["intSectorId"].ToString();			
                lbl_Sector.Text = dtindustryPre.Rows[0]["sectorName"].ToString();
                //dtindustryPre.Rows[0]["intSubSectorId"].ToString();			
                lbl_Sub_Sector.Text = dtindustryPre.Rows[0]["SubsectorName"].ToString();
                Lbl_Derived_Sector.Text = dtindustryPre.Rows[0]["vchDerivedSector"].ToString();
                //dtindustryPre.Rows[0]["bitSectoralPolicy"].ToString();

                if (dtindustryPre.Rows[0]["bitPriorityIPR"].ToString() == "1")
                {
                    lbl_Sectoral.Text = "Yes";
                }
                else
                {
                    lbl_Sectoral.Text = "No";
                }
                //dtindustryPre.Rows[0]["intCreatedBy"].ToString();
            }
            #endregion


            #region Production

            Grd_Production_Before.DataSource = dtProductionDetBefPre;
            Grd_Production_Before.DataBind();
            Grd_Production_After.DataSource = dtProductionDetAftPre;
            Grd_Production_After.DataBind();


            //dtProductionPre.Rows[0]["intProductionId"].ToString();
            lbl_Direct_Emp_Before.Text = dtProductionPre.Rows[0]["intDirectEmpBefore"].ToString();
            lbl_Contract_Emp_Before.Text = dtProductionPre.Rows[0]["intContractualEmpBefore"].ToString();
            Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["VCHEMPDOC"].ToString();
            lbl_Managarial_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDMANAGERIAL"].ToString();
            lbl_Supervisor_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDSUPERVISORY"].ToString();
            lbl_Skilled_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDSKILLED"].ToString();
            lbl_Semi_Skilled_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDSEMISKILLED"].ToString();
            lbl_Unskilled_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDUNSKILLED"].ToString();
            lbl_Total_Emp_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDTOTAL"].ToString();
            lbl_General_Before.Text = dtProductionPre.Rows[0]["intGeneralBefore"].ToString();
            lbl_SC_Before.Text = dtProductionPre.Rows[0]["intSCBefore"].ToString();
            lbl_ST_Before.Text = dtProductionPre.Rows[0]["intSTBefore"].ToString();
            lbl_Total_Cast_Emp_Before.Text = dtProductionPre.Rows[0]["intTotalEmpCastBefore"].ToString();
            lbl_Women_Before.Text = dtProductionPre.Rows[0]["intWomenBefore"].ToString();
            lbl_PHD_Before.Text = dtProductionPre.Rows[0]["intDisabledBefore"].ToString();
            lbl_Direct_Emp_After.Text = dtProductionPre.Rows[0]["intDirectEmpAfter"].ToString();
            lbl_Contract_Emp_After.Text = dtProductionPre.Rows[0]["intContractualEmpAfter"].ToString();
            //dtProductionPre.Rows[0]["vchEmpDocBeforeCode"].ToString();			
            Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
            Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
            //dtProductionPre.Rows[0]["vchEmpDocAfterCode"].ToString();			
            Lbl_Direct_Emp_After_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocAfterCodeName"].ToString();


            lbl_Managarial_After.Text = dtProductionPre.Rows[0]["INTCURRENTMANAGERIAL"].ToString();
            lbl_Supervisor_After.Text = dtProductionPre.Rows[0]["INTCURRENTSUPERVISORY"].ToString();
            lbl_Skilled_After.Text = dtProductionPre.Rows[0]["INTCURRENTSKILLED"].ToString();
            lbl_Semi_Skilled_After.Text = dtProductionPre.Rows[0]["INTCURRENTSEMISKILLED"].ToString();
            lbl_Unskilled_After.Text = dtProductionPre.Rows[0]["INTCURRENTUNSKILLED"].ToString();
            lbl_Total_Emp_After.Text = dtProductionPre.Rows[0]["INTCURRENTTOTAL"].ToString();
            lbl_General_After.Text = dtProductionPre.Rows[0]["intGeneralAfter"].ToString();
            lbl_SC_After.Text = dtProductionPre.Rows[0]["intSCAfter"].ToString();
            lbl_ST_After.Text = dtProductionPre.Rows[0]["intSTAfter"].ToString();
            lbl_Total_Cast_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpCastAfter"].ToString();
            lbl_Women_After.Text = dtProductionPre.Rows[0]["intWomenAfter"].ToString();
            lbl_PHD_After.Text = dtProductionPre.Rows[0]["intDisabledAfter"].ToString();

            //dtProductionPre.Rows[0]["intCreatedBy"].ToString();

            #endregion



            #region Investment

            //,vchFFCIDocBeforeCode,,,,,
            //    ,,INT_INCUNQUEID,vchProjectDocBeforeCode,vchProjectDocBefore,dtmFFCIDateAfter,
            //    vchFFCIDocAfterCode,vchFFCIDocAfter,decLandAmtAfter,decBuildingAmtAfter,decPlantMachAmtAfter,decOtheFixedAssetAmtAfter,
            //    decTotalAmtAfter,vchProjectDocAfterCode,vchProjectDocAfter,INT_CREATED_BY,DTM_CREATEDON

            //dtInvestmentPre.Rows[0]["slno"].ToString();
            Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
            //dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCode"].ToString();
            Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
            Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();
            lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
            lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["DEC_Building"].ToString();
            lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["DEC_Plant_Machinery"].ToString();
            lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
            lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["DEC_Total"].ToString();
            //dtInvestmentPre.Rows[0]["vchProjectDocBeforeCode"].ToString();			
            Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
            Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocBefore"].ToString();
            lbl_FFCI_Date_After.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateAfter"].ToString();
            //dtInvestmentPre.Rows[0]["vchFFCIDocAfterCode"].ToString();			
            Lbl_FFCI_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
            Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString();

            lbl_Land_After.Text = dtInvestmentPre.Rows[0]["decLandAmtAfter"].ToString();
            lbl_Building_After.Text = dtInvestmentPre.Rows[0]["decBuildingAmtAfter"].ToString();
            lbl_Plant_Mach_After.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtAfter"].ToString();
            lbl_Other_Fixed_Asset_After.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
            lbl_Total_Capital_After.Text = dtInvestmentPre.Rows[0]["decTotalAmtAfter"].ToString();


            //dtInvestmentPre.Rows[0]["vchProjectDocAfterCode"].ToString();			
            Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocAfterCodeName"].ToString();
            Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString();
            //dtInvestmentPre.Rows[0]["intCreatedBy"].ToString();

            #endregion

            #region MEANS OF FINANCE

            //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
            lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
            lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();

            lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
            //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
            //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
            //Lbl_Term_Loan_Doc_Name.Text = dtMeansFinancePre.Rows[0]["vchTermLoanDocCodeNAme"].ToString();



            if (dtMoFTermLoanPre.Rows.Count > 0)
            {
                Grd_TL.DataSource = dtMoFTermLoanPre;
                Grd_TL.DataBind();
            }

            if (dtMoFWorkingLoanPre.Rows.Count > 0)
            {
                Grd_WC.DataSource = dtMoFWorkingLoanPre;
                Grd_WC.DataBind();
            }
            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


    }

    private void SMSEMailContent()
    {
        try
        {
            CommonHelperCls objcomm = new CommonHelperCls();
            string strSubject = "GOSwift: Application submitted successfully";
            string PreviewURL = System.Configuration.ConfigurationManager.AppSettings["PreviewURL"];
            DataTable dtsal1 = (DataTable)ViewState["salutation"];

            string strBody = lblTitle.Text + " of M/s " + lblMr.Text + " has been submitted successfully. " + PreviewURL;
            string SMSContent = strSubject;

            var myList = new List<string>();
            myList.Add(dtsal1.Rows[0]["VCH_EMAIL"].ToString());
            string[] tomail = myList.ToArray();

            bool mailStatus = objcomm.sendMail(strSubject, strBody, tomail, true);
            bool smsStatus = objcomm.SendSmsNew(dtsal1.Rows[0]["VCH_OFF_MOBILE"].ToString(), SMSContent);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #region TRDisplay
    /// <summary>
    /// To make Tr visible false if respective Label Text is blank
    /// by GS Chhotray 17/11/2017
    /// </summary>
    /// <param name="lbtext"></param>
    /// <param name="trid"></param>
    protected void TRVisibility()
    {
        try
        {
            //TrDisplayNone(Lbl_Prod_Comm_Before_Doc_Name.Text, tr_Prod_Comm_Before);
            TrDisplayNone(Lbl_Prod_Comm_After_Doc_Name.Text, tr_Prod_Comm_After_Doc_Name);
            TrDisplayNone(Lbl_Pioneer_Doc_Name.Text, DivPioneer);
            TrDisplayNone(Lbl_Direct_Emp_Before_Doc_Name.Text, divEmp_Before_Doc_Name);
            TrDisplayNone(Lbl_Unit_Type_Doc_Name.Text, Div_Unit_Type_Doc);
            TrDisplayNone(Lbl_Direct_Emp_After_Doc_Name.Text, tr_Direct_Emp_After_Doc_Name);
            TrDisplayNone(Lbl_FFCI_Before_Doc_Name.Text, FFCI_Before_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_After_Doc_Name.Text, tr_Approved_DPR_After_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_Before_Doc_Name.Text, Approved_DPR_Before_Doc_Name);
            TrDisplayNone(Lbl_FFCI_After_Doc_Name.Text, tr_FFCI_After_Doc_Name);
            TrDisplayNone(Lbl_Term_Loan_Doc_Name.Text, tr_Term_Loan_Doc_Name);



            //TrDisplayNone(lblAvailDoc.Text, trAvailDoc);
        }
        catch (Exception)
        {
        }
    }
    /// <summary>
    /// To make Tr visible false if respective Label Text is blank
    /// by GS Chhotray 17/11/2017
    /// </summary>
    /// <param name="lbtext"></param>
    /// <param name="trid"></param>
    public void TrDisplayNone(string lbtext, System.Web.UI.HtmlControls.HtmlTableRow trid)
    {
        try
        {
            if (lbtext.Trim() == "")
            {
                trid.Visible = false;
            }
            else
            {
                trid.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
}