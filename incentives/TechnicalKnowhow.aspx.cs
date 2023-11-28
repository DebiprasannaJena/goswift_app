using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using EntityLayer.Incentive;
using DataAcessLayer.Common;
using DataAcessLayer.Incentive;
using BusinessLogicLayer.Incentive;


public partial class incentives_TechnicalKnowhow : SessionCheck
{
    string gProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];

    #region "Methods"

    private void crdtincentive()
    {
        DataTable dtincentive = new DataTable();


        DataColumn dcRowId = new DataColumn("dcRowId");
        dcRowId.DataType = Type.GetType("System.Int32");
        dcRowId.AutoIncrement = true;
        dcRowId.AutoIncrementSeed = 1;
        dcRowId.AutoIncrementStep = 1;
        dtincentive.Columns.Add(dcRowId);

        DataColumn vchagency = new DataColumn("vchagency");
        vchagency.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchagency);

        DataColumn vchsacamt = new DataColumn("vchsacamt");
        vchsacamt.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchsacamt);

        DataColumn vchsacord = new DataColumn("vchsacord");
        vchsacord.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchsacord);

        DataColumn vchsacdat = new DataColumn("vchsacdat");
        vchsacdat.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchsacdat);

        DataColumn vchavilamt = new DataColumn("vchavilamt");
        vchavilamt.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchavilamt);




        ViewState["dtincentive"] = dtincentive;
        grdAssistanceDetailsAD.DataSource = dtincentive;
        grdAssistanceDetailsAD.DataBind();
    }
    private void crdttechnicalhow()
    {
        DataTable dttechnical = new DataTable();


        DataColumn dcRowId = new DataColumn("dcRowId");
        dcRowId.DataType = Type.GetType("System.Int32");
        dcRowId.AutoIncrement = true;
        dcRowId.AutoIncrementSeed = 1;
        dcRowId.AutoIncrementStep = 1;
        dttechnical.Columns.Add(dcRowId);

        DataColumn vchimport = new DataColumn("vchimport");
        vchimport.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchimport);

        DataColumn vchimportid = new DataColumn("vchimportid");
        vchimportid.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchimportid);

        DataColumn vchagenname = new DataColumn("vchagenname");
        vchagenname.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchagenname);

        DataColumn vchagenadd = new DataColumn("vchagenadd");
        vchagenadd.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchagenadd);

        DataColumn vchprof = new DataColumn("vchprof");
        vchprof.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchprof);

        DataColumn vchpermi = new DataColumn("vchpermi");
        vchpermi.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchpermi);

        DataColumn vchamt = new DataColumn("vchamt");
        vchamt.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchamt);

        DataColumn vchbill = new DataColumn("vchbill");
        vchbill.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchbill);

        DataColumn vchbillno = new DataColumn("vchbillno");
        vchbillno.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchbillno);

        DataColumn vchbilldt = new DataColumn("vchbilldt");
        vchbilldt.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchbilldt);

        DataColumn vchbillamt = new DataColumn("vchbillamt");
        vchbillamt.DataType = Type.GetType("System.String");
        dttechnical.Columns.Add(vchbillamt);

        ViewState["dttechnical"] = dttechnical;
        grdindigious.DataSource = dttechnical;
        grdindigious.DataBind();
    }
    private void documenttable()
    {
        DataTable dtdocument = new DataTable();


        DataColumn id = new DataColumn("id");
        id.DataType = Type.GetType("System.Int32");
        id.AutoIncrement = true;
        id.AutoIncrementSeed = 1;
        id.AutoIncrementStep = 1;
        dtdocument.Columns.Add(id);

        DataColumn vchDocId = new DataColumn("vchDocId");
        vchDocId.DataType = Type.GetType("System.String");
        dtdocument.Columns.Add(vchDocId);

        DataColumn vchFileName = new DataColumn("vchFileName");
        vchFileName.DataType = Type.GetType("System.String");
        dtdocument.Columns.Add(vchFileName);

        DataColumn vchFilePath = new DataColumn("vchFilePath");
        vchFilePath.DataType = Type.GetType("System.String");
        dtdocument.Columns.Add(vchFilePath);

        ViewState["dtdocument"] = dtdocument;
    }
    private void documentsindt(string strdocid, string filname)
    {
        DataTable dtdocument = new DataTable();
        dtdocument = (DataTable)ViewState["dtdocument"];
        DataRow drdoc = dtdocument.NewRow();
        drdoc["vchDocId"] = strdocid;
        drdoc["vchFileName"] = filname;
        drdoc["vchFilePath"] = "../incentives/TKH/";
        dtdocument.Rows.Add(drdoc);
        ViewState["dtdocument"] = dtdocument;

    }

    private string dataSave()
    {
        string retVal = "";
        try
        {
            Incentive objEntity = new Incentive();
            objEntity.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();

            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objEntity.UnqIncentiveId = 0;
            }
            else
            {
                objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objEntity.strcActioncode = "A";

            objEntity.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objEntity.PealNum = Convert.ToString(Session["ProposalNo"]);
            objEntity.PCNum = Convert.ToString(Session["PCNo"]);
            objEntity.UnitCode = Convert.ToString(Session["UnitCode"]);
            objEntity.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objEntity.Userid = Convert.ToInt16(Session["InvestorId"]);
            objEntity.Createdby = Convert.ToInt16(Session["InvestorId"]);

            #region "Industrial Unit"

            objEntity.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex >= 0)
            {
                objEntity.IndsutUnitMstDet.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }
            if (radApplyBy.SelectedIndex >= 0)
            {
                objEntity.IndsutUnitMstDet.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objEntity.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar1.Text;
            objEntity.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objEntity.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;

            #endregion

            #region "Technical Know how"

            objEntity.TechnicalKnowDet = new Technicalknow();
            objEntity.TechnicalKnowDet.STR_BRIEF_ON_TECHNICAL = txtbrief.Text;
            objEntity.TechnicalKnowDet.INT_TECHNICAL_CLAIM = 0;

            DataTable dttechnical = new DataTable();
            dttechnical = (DataTable)ViewState["dttechnical"];

            List<Technicalknowdetail> techlist = new List<Technicalknowdetail>();
            Technicalknowdetail objtechnical = new Technicalknowdetail();

            if (dttechnical.Rows.Count > 0)
            {
                foreach (DataRow drtech in dttechnical.Rows)
                {

                    objtechnical = new Technicalknowdetail();
                    objtechnical.STR_IMPORTED = drtech["vchimportid"].ToString();
                    objtechnical.STR_NAME_OF_THE_AGENCY = drtech["vchagenname"].ToString();
                    objtechnical.STR_ADDRESS_OF_THE_AGENCY = drtech["vchagenadd"].ToString();
                    objtechnical.STR_PROFILE_UPLOAD_DOCUMENT = drtech["vchprof"].ToString();
                    objtechnical.STR_GOI_PERMISSION = drtech["vchpermi"].ToString();
                    if (drtech["vchamt"].ToString().Trim() != "")
                    {
                        objtechnical.DEC_AMOUNT_0F_EXPENDITURE = Convert.ToDecimal(drtech["vchamt"].ToString());
                    }
                    else
                    {
                        objtechnical.DEC_AMOUNT_0F_EXPENDITURE = 0;
                    }
                    objtechnical.STR_BILL_NO = drtech["vchbillno"].ToString();
                    objtechnical.STR_BILL_DOCUMENT = drtech["vchbill"].ToString();
                    objtechnical.DTM_BILL_DATE = drtech["vchbilldt"].ToString();

                    if (drtech["vchbillamt"].ToString().Trim() != "")
                    {

                        objtechnical.DEC_TOTAL_BILL_AMOUNT = Convert.ToDecimal(drtech["vchbillamt"].ToString());
                    }
                    else
                    {
                        objtechnical.DEC_TOTAL_BILL_AMOUNT = 0;
                    }
                    techlist.Add(objtechnical);
                }
                objEntity.TechnicalKnowDet.Technicalknowdetails = techlist;
            }


            #endregion

            #region "Availed Details"

            AvailDetails objAvailDetails = new AvailDetails();

            objAvailDetails.NeverAvailedPrior = Convert.ToInt16(RadBtn_Availed_Earlier.SelectedValue.ToString());
            objAvailDetails.SubsidyAvailed = 0;
            objAvailDetails.UndertakingSubsidyDoc = Hid_Undertaking_File_Name.Value;
            objAvailDetails.SanctionOrderDoc = Hid_Asst_Sanc_File_Name.Value;

            if (txtdiffclaimamt.Text.Trim() != "")
            {
                objAvailDetails.ClaimtExempted = Convert.ToDouble(txtdiffclaimamt.Text);
            }
            else
            {
                objAvailDetails.ClaimtExempted = 0;
            }

            if (txtreimamt.Text.Trim() != "")
            {
                objAvailDetails.ClaimReimbursement = Convert.ToDouble(txtreimamt.Text);
            }
            else
            {
                objAvailDetails.ClaimReimbursement = 0;
            }

            List<Assistance> listIncentiveAvailed = new List<Assistance>();
            Assistance objIncentiveAvailed = new Assistance();
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];

            if (dtincentive.Rows.Count > 0)
            {
                foreach (DataRow dr in dtincentive.Rows)
                {
                    objIncentiveAvailed = new Assistance();

                    objIncentiveAvailed.InstitutionName = dr["vchagency"].ToString();
                    if (dr["vchsacamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.SanctionedAmount = Convert.ToDouble(dr["vchsacamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.SanctionedAmount = 0;
                    }
                    if (dr["vchavilamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.AmountAvailed = Convert.ToDouble(dr["vchavilamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.AmountAvailed = 0;
                    }
                    objIncentiveAvailed.SanctionOrderNo = dr["vchsacord"].ToString();
                    objIncentiveAvailed.AvailedDate = Convert.ToDateTime(dr["vchsacdat"].ToString());

                    listIncentiveAvailed.Add(objIncentiveAvailed);
                }
            }

            objAvailDetails.IncentiveAvailed = listIncentiveAvailed;
            objEntity.AvailDet = objAvailDetails;


            #endregion

            #region "Additional Documents"

            if (hdnIsOsPCBDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.OSPCB, hdnvalidstatutaryfile, "TKH");
            }
            if (hdnBoilderDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.Boiler, D280, "TKH");
            }

            objEntity.AdditionalDocument = new EntityLayer.Incentive.AdditionalDocuments();
            objEntity.AdditionalDocument.intStatutoryCleanOSPCB_NOC = 0;
            objEntity.AdditionalDocument.intStatutoryCleanOSPCB_Consent = 0;
            objEntity.AdditionalDocument.intStatutoryCleanCentralExec = 0;
            objEntity.AdditionalDocument.intStatutoryCleanFSHGSCD = 0;
            objEntity.AdditionalDocument.intStatutoryCleanExplosive_NOC = 0;
            objEntity.AdditionalDocument.strCleanApproveAuthorityOSPCB = hdnvalidstatutaryfile.Value; // OSPCB
            objEntity.AdditionalDocument.strStCleanConsentOSPCB = "";
            objEntity.AdditionalDocument.strClearanceCetiftOSPCB = "";
            objEntity.AdditionalDocument.strValidSatutoryGreenCategory = D280.Value; // Factory and Boiler
            objEntity.AdditionalDocument.strCondoDocumentationDelay = hdndelaydocfile.Value; // Sector Relevant

            #endregion

            #region "All Uploaded documents"

            DataTable dtdocument = new DataTable();
            dtdocument = (DataTable)ViewState["dtdocument"];
            if (dtdocument.Rows.Count > 0)
            {
                List<lstFileUpload> listItmProp = new List<lstFileUpload>();
                listItmProp = dtdocument.AsEnumerable().Select(m => new lstFileUpload()
                {
                    id = m.Field<int>("id"),
                    vchDocId = m.Field<string>("vchDocId"),
                    vchFileName = m.Field<string>("vchFileName"),
                    vchFilePath = m.Field<string>("vchFilePath")

                }).ToList();


                objEntity.FileUploadDetails = listItmProp;
            }


            #endregion


            objEntity.FormType = FormNumber.TechnicalKnowHow_07;
            retVal = IncentiveManager.CreateIncentiveTechKHW(objEntity);

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
            retVal = "";
        }
        return retVal;
    }
    private string RemoveDecimal(string strval)
    {
        string retStr = strval;
        if (strval.IndexOf(".") > 0)
        {
            retStr = strval.Substring(strval.IndexOf("."), 3);
            retStr = strval.Remove(strval.IndexOf("."), 3);
        }
        return retStr;
    }
    private void PostPopulateData(int reqid)
    {
        try
        {
            Incentive objEntity = new Incentive();
            GetAndViewParam objPar = new GetAndViewParam();
            objEntity.strcActioncode = "7";
            objPar.Param1ID = "";
            objPar.Param2ID = "";
            objPar.Param3ID = "";
            objPar.InctType = 4;
            objEntity.UnqIncentiveId = reqid;
            objEntity.GetVwPrmtrs = objPar;
            objEntity.FormType = FormNumber.TechnicalKnowHow_07;
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveEDD(objEntity);

            #region "Technical Know How"

            DataTable dtTechno = ds.Tables[0];
            DataTable dtTechno1 = ds.Tables[1];

            if (dtTechno.Rows.Count > 0)
            {
                txtbrief.Text = dtTechno.Rows[0]["VCH_BRIEF_ON_TECHNICAL"].ToString();
            }

            if (dtTechno1.Rows.Count > 0)
            {
                DataTable dttechnical = new DataTable();
                dttechnical = (DataTable)ViewState["dttechnical"];

                foreach (DataRow drtechno in dtTechno1.Rows)
                {
                    DataRow drtechnogrd = dttechnical.NewRow();

                    if (drtechno["VCH_IMPORTED"].ToString().Trim() == "1")
                    {
                        drtechnogrd["vchimport"] = "Imported";
                    }
                    else
                    {
                        drtechnogrd["vchimport"] = "Indigenous";
                    }

                    drtechnogrd["vchimportid"] = drtechno["VCH_IMPORTED"].ToString();
                    drtechnogrd["vchagenname"] = drtechno["VCH_NAME_OF_THE_AGENCY"].ToString();
                    drtechnogrd["vchagenadd"] = drtechno["VCH_ADDRESS_OF_THE_AGENCY"].ToString();
                    drtechnogrd["vchprof"] = drtechno["VCH_PROFILE_UPLOAD_DOCUMENT"].ToString();
                    drtechnogrd["vchpermi"] = drtechno["VCH_GOI_PERMISSION"].ToString();
                    drtechnogrd["vchamt"] = drtechno["DEC_AMOUNT_0F_EXPENDITURE"].ToString();
                    drtechnogrd["vchbill"] = drtechno["VCH_BILL_DOCUMENT"].ToString();
                    drtechnogrd["vchbillno"] = drtechno["VCH_BILL_NO"].ToString();
                    drtechnogrd["vchbilldt"] = drtechno["DTM_BILL_DATE"].ToString();
                    drtechnogrd["vchbillamt"] = drtechno["DEC_TOTAL_BILL_AMOUNT"].ToString();
                    dttechnical.Rows.Add(drtechnogrd);
                }

                ViewState["dttechnical"] = dttechnical;
                grdindigious.DataSource = dttechnical;
                grdindigious.DataBind();
            }
            #endregion
            #region "Availed Details"

            DataTable dtavail = ds.Tables[2];
            DataTable dtavailgrd1 = ds.Tables[3];


            if (dtavail.Rows.Count > 0)
            {
                Hid_Undertaking_File_Name.Value = dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                Lbl_Msg_Undertaking_Doc.Text = dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                if (Hid_Undertaking_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Undertaking_Doc.Visible = true;
                    LnkBtn_Delete_Undertaking_Doc.Visible = true;
                    FU_Undertaking_Doc.Enabled = false;
                    Hyp_View_Undertaking_Doc.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "TKH", Hid_Undertaking_File_Name.Value);

                }
                else
                {
                    Hyp_View_Undertaking_Doc.Visible = false;
                    LnkBtn_Delete_Undertaking_Doc.Visible = false;
                    FU_Undertaking_Doc.Enabled = true;
                }


                Hid_Asst_Sanc_File_Name.Value = dtavail.Rows[0]["VchSanctionDoc"].ToString();
                //Lbl_Msg_Asst_Sanc_Doc.Text = dtavail.Rows[0]["VchSanctionDoc"].ToString();

                if (Hid_Asst_Sanc_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Asst_Sanc_Doc.Visible = true;
                    LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
                    FU_Asst_Sanc_Doc.Enabled = false;
                    Hyp_View_Asst_Sanc_Doc.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "TKH", Hid_Asst_Sanc_File_Name.Value);
                }
                else
                {
                    Hyp_View_Asst_Sanc_Doc.Visible = false;
                    LnkBtn_Delete_Asst_Sanc_Doc.Visible = false;
                    FU_Asst_Sanc_Doc.Enabled = true;
                }

                txtdiffclaimamt.Text = dtavail.Rows[0]["decClaimExempted"].ToString();
                txtreimamt.Text = dtavail.Rows[0]["decClaimReimbursement"].ToString();
                RadBtn_Availed_Earlier.SelectedValue = dtavail.Rows[0]["intNeverAvailedPrior"].ToString();
            }
            if (dtavailgrd1.Rows.Count > 0)
            {
                DataTable dtincentive = new DataTable();
                dtincentive = (DataTable)ViewState["dtincentive"];

                foreach (DataRow dravgr1 in dtavailgrd1.Rows)
                {
                    DataRow drassistant = dtincentive.NewRow();
                    drassistant["vchagency"] = dravgr1["vchInstitutionName"].ToString();
                    drassistant["vchsacamt"] = dravgr1["decSanctionedAmount"].ToString();
                    drassistant["vchsacord"] = dravgr1["vchSanctionOrderNo"].ToString();
                    drassistant["vchsacdat"] = dravgr1["dtmAvailedDate"].ToString();
                    drassistant["vchavilamt"] = dravgr1["decAmountAvailed"].ToString();
                    dtincentive.Rows.Add(drassistant);
                }

                ViewState["dtincentive"] = dtincentive;
                grdAssistanceDetailsAD.DataSource = dtincentive;
                grdAssistanceDetailsAD.DataBind();
            }

            #endregion
            #region "Additional Documents"

            DataTable dtAdditional = ds.Tables[4];
            if (dtAdditional.Rows.Count > 0)
            {
                hdnvalidstatutaryfile.Value = dtAdditional.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
                hdndelaydocfile.Value = dtAdditional.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
                D280.Value = dtAdditional.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();

                if (hdnvalidstatutaryfile.Value.Trim() != "")
                {
                    hypValidStatutary.Visible = true;
                    lnkDValidStatutary.Visible = true;
                    flValidStatutary.Enabled = false;
                    hypValidStatutary.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "TKH", hdnvalidstatutaryfile.Value);
                }

                if (hdndelaydocfile.Value.Trim() != "")
                {
                    hypDelay.Visible = true;
                    lnkDDelay.Visible = true;
                    flDelay.Enabled = false;
                    hypDelay.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "TKH", hdndelaydocfile.Value);
                }

                if (D280.Value.Trim() != "")
                {
                    flCleanApproveAuthority.Enabled = false;
                    hypCleanApproveAuthority.Visible = true;
                    lnkDCleanApproveAuthority.Visible = true;
                    hypCleanApproveAuthority.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "TKH", D280.Value);
                }
            }

            #endregion
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
            //if (File.Exists(completePath))
            //{
            //File.Delete(completePath);

            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
            //}
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

            fuOrgDocument.SaveAs(strMainFolderPath + strFileName);
            hdnOrgDocument.Value = strFileName;
            hypOrdDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFoldername, strFileName);
            hypOrdDocument.Visible = true;
            lnkOrgDocumentDelete.Visible = true;
            lblOrgDocument.Visible = true;
            fuOrgDocument.Enabled = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    private void GetMasterdetails()
    {
        try
        {
            Incentive objIncentive = new Incentive();
            objIncentive.strcActioncode = "M";
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objIncentive);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtbPostSubFlag = ds.Tables[0];
                lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    private int IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed" };
        string[] allowedExtension = { ".pdf", ".zip" };
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        imageTypes.AddRange(allowedImageTyps);
        imageExtension.AddRange(allowedExtension);
        string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);// 
        int count = FileUpload1.FileName.Count(f => f == '.');
        string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
        CommonFunctions cmmf = new CommonFunctions();

        if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        txtbilldate.Attributes.Add("readonly", "readonly");
        txtsacdat.Attributes.Add("readonly", "readonly");

        if (!Page.IsPostBack)
        {
            fillSalutation();
            GetMasterdetails();
            crdtincentive();
            documenttable();
            crdttechnicalhow();

            if (Convert.ToString(Session["ApplySource"]) == "0")
            {
                PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                PostPopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
            else
            {
                PrepopulateDataComm(Convert.ToInt16(Session["InvestorId"]));
            }
        }
    }

    #region "File UPLOADS"

    protected void LnkBtn_Upload_Undertaking_Doc_Click(object sender, EventArgs e) /// for upload undertaking document
    {
        if (FU_Undertaking_Doc.HasFile)
        {
            if (IsFileValid(FU_Undertaking_Doc) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }
            string extension = Path.GetExtension(FU_Undertaking_Doc.PostedFile.FileName);
            string filename = "UND" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, filename, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, "TKH");
            documentsindt(hdnundertakingdocid.Value, filename);
        }
    }
    protected void LnkBtn_Delete_Undertaking_Doc_Click(object sender, EventArgs e)/// for delete uploaded undertaking document
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Undertaking_Doc.ID))
        {
            UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, "TKH");
        }
    }

    protected void LnkBtn_Upload_Asst_Sanc_Doc_Click(object sender, EventArgs e) /// for upload Details of assistance sanctioned document
    {
        if (FU_Asst_Sanc_Doc.HasFile)
        {
            if (IsFileValid(FU_Asst_Sanc_Doc) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }
            string extension = Path.GetExtension(FU_Asst_Sanc_Doc.PostedFile.FileName);
            string filename = "ASSTSANC" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, filename, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, "TKH");
            documentsindt(hdndetailsassistantdocid.Value, filename);
        }
    }
    protected void LnkBtn_Delete_Asst_Sanc_Doc_Click(object sender, EventArgs e) /// for delete Details of assistance sanctioned document
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Asst_Sanc_Doc.ID))
        {
            UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, "TKH");
        }
    }

    protected void lnkUValidStatutary_Click(object sender, EventArgs e)  /// for upload Details of statutary clearance document
    {
        if (flValidStatutary.HasFile)
        {
            if (IsFileValid(flValidStatutary) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }
            string extension = Path.GetExtension(flValidStatutary.PostedFile.FileName);
            string filename = "OSPCB" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(flValidStatutary, hdnvalidstatutaryfile, filename, hypValidStatutary, lblValidStatutary, lnkDValidStatutary, "TKH");
            documentsindt(hdnValidStatutaryCode.Value, filename);
        }
    }
    protected void lnkDValidStatutary_Click(object sender, EventArgs e)  /// for deleting Details of statutary clearance document
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkDValidStatutary.ID))
        {
            UpdFileRemove(hdnvalidstatutaryfile, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, flValidStatutary, "TKH");
        }
    }

    protected void lnkUDelay_Click(object sender, EventArgs e)
    {
        if (flDelay.HasFile)
        {
            if (IsFileValid(flDelay) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }

            string extension = Path.GetExtension(flDelay.PostedFile.FileName);
            string filename = "STA" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(flDelay, hdndelaydocfile, filename, hypDelay, lblDelay, lnkDDelay, "TKH");
            documentsindt(hdnDelayCode.Value, filename);
        }
    }
    protected void lnkDDelay_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkDDelay.ID))
        {
            UpdFileRemove(hdndelaydocfile, lnkUDelay, lnkDDelay, hypDelay, lblDelay, flDelay, "TKH");
        }
    }

    protected void lnkDocumentUpload_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                if (IsFileValid(FlupAUTHORIZEDFILE) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                    return;
                }

                string extension = Path.GetExtension(FlupAUTHORIZEDFILE.PostedFile.FileName);
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss") + extension;
                string strFolderName = "InctBasicDoc";
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, strFolderName);
            }
        }
    }
    protected void lnkDocumentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
        {
            string strFolderName = "Files/InctBasicDoc";
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);
        }
    }

    protected void lnkUCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        try
        {
            if (flCleanApproveAuthority.HasFile)
            {
                if (IsFileValid(flCleanApproveAuthority) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                    return;
                }

                string strFileName = "Boiler" + DateTime.Now.ToString("_ddMMyyhhmmss") + ".zip";
                UploadDocument(flCleanApproveAuthority, D280, strFileName, hypCleanApproveAuthority, lblCleanApproveAuthority, lnkDCleanApproveAuthority, "TKH");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkDCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        try
        {
            //string strFolderName = "AdditionalDocument";
            //UpdFileRemove(D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, flCleanApproveAuthority, strFolderName);
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkDCleanApproveAuthority.ID))
            {
                UpdFileRemove(D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, flCleanApproveAuthority, "TKH");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    #endregion

    #region "Availed2ndGrid"

    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        DataTable dtincentive = new DataTable();
        dtincentive = (DataTable)ViewState["dtincentive"];
        DataRow dr = dtincentive.NewRow();
        dr["vchagency"] = txtagency.Text.Trim();
        dr["vchsacamt"] = txtsacamt.Text.Trim();
        dr["vchsacord"] = txtsacord.Text.Trim();
        dr["vchsacdat"] = txtsacdat.Text.Trim();
        dr["vchavilamt"] = txtavilamt.Text.Trim();
        dtincentive.Rows.Add(dr);
        ViewState["dtincentive"] = dtincentive;

        grdAssistanceDetailsAD.DataSource = dtincentive;
        grdAssistanceDetailsAD.DataBind();

        txtagency.Text = "";
        txtsacamt.Text = "";
        txtsacord.Text = "";
        txtsacdat.Text = "";
        txtavilamt.Text = "";
    }
    protected void grdAssistanceDetailsAD_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdfanew = (HiddenField)grdAssistanceDetailsAD.Rows[e.RowIndex].Cells[5].FindControl("hdnRowId");
        DataTable dtnew0 = new DataTable();
        dtnew0 = (DataTable)ViewState["dtincentive"];
        DataRow[] dr1 = null;
        dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
        for (int i = 0; i < dr1.Length; i++)
        {

            dr1[i].Delete();
        }
        dtnew0.AcceptChanges();
        grdAssistanceDetailsAD.DataSource = dtnew0;
        grdAssistanceDetailsAD.DataBind();
        ViewState["dtincentive"] = dtnew0;
    }

    #endregion

    #region "Technical Grid"

    protected void LinkButton23_Click(object sender, EventArgs e)
    {

        if (FUPProfdoc.HasFile)
        {
            string extension = Path.GetExtension(FUPProfdoc.PostedFile.FileName);
            string filename = "TCHPROF_" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            FUPProfdoc.SaveAs(Server.MapPath("../incentives/Files/TKH/") + filename);
            hdnProfdoc.Value = filename;

        }
        if (FUPbilldoc.HasFile)
        {
            string extension = Path.GetExtension(FUPbilldoc.PostedFile.FileName);
            string filename = "TCHBILL_" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            FUPbilldoc.SaveAs(Server.MapPath("../incentives/Files/TKH/") + filename);
            hdnbilldoc.Value = filename;
        }



        DataTable dttechnical = new DataTable();
        dttechnical = (DataTable)ViewState["dttechnical"];
        DataRow dr = dttechnical.NewRow();

        dr["vchimport"] = ddlimp.SelectedItem.Text.ToString();
        dr["vchimportid"] = ddlimp.SelectedValue.ToString();

        dr["vchagenname"] = txtagenname.Text.Trim();

        dr["vchagenadd"] = txtagenadd.Text.Trim();

        dr["vchprof"] = hdnProfdoc.Value.ToString();

        dr["vchpermi"] = txtimported.Text.Trim();
        dr["vchamt"] = txtagenamt.Text.ToString();

        dr["vchbill"] = hdnbilldoc.Value.ToString();
        dr["vchbillno"] = txtbill.Text.Trim();
        dr["vchbilldt"] = txtbilldate.Text.ToString();
        dr["vchbillamt"] = txtbillamt.Text.Trim();

        dttechnical.Rows.Add(dr);
        ViewState["dttechnical"] = dttechnical;

        grdindigious.DataSource = dttechnical;
        grdindigious.DataBind();

        ddlimp.SelectedValue = "0";
        txtagenname.Text = "";
        txtagenadd.Text = "";
        hdnProfdoc.Value = "";
        txtimported.Text = "";
        txtagenamt.Text = "";
        hdnbilldoc.Value = "";
        txtbill.Text = "";
        txtbilldate.Text = "";
        txtbillamt.Text = "";



    }

    protected void grdindigious_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypProf = (HyperLink)e.Row.FindControl("hypProf");
            HyperLink hypBill = (HyperLink)e.Row.FindControl("hypBill");
            if (!string.IsNullOrEmpty(hypProf.Text))
            {
                hypProf.Visible = true;
                hypProf.NavigateUrl = "~/incentives/Files/TKH/" + hypProf.Text;
            }
            if (!string.IsNullOrEmpty(hypBill.Text))
            {
                hypBill.Visible = true;
                hypBill.NavigateUrl = "~/incentives/Files/TKH/" + hypBill.Text;
            }
        }

    }
    protected void grdindigious_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdfanew = (HiddenField)grdindigious.Rows[e.RowIndex].Cells[3].FindControl("hdnRowId");
            LinkButton lnbprof = (LinkButton)grdindigious.Rows[e.RowIndex].Cells[3].FindControl("lnkprofile");
            LinkButton lnbill = (LinkButton)grdindigious.Rows[e.RowIndex].Cells[6].FindControl("lnkbill");

            DataTable dtnew0 = new DataTable();
            dtnew0 = (DataTable)ViewState["dttechnical"];
            DataRow[] dr1 = null;
            dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {

                dr1[i].Delete();
            }

            dtnew0.AcceptChanges();
            grdindigious.DataSource = dtnew0;
            grdindigious.DataBind();
            ViewState["dttechnical"] = dtnew0;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    #endregion

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string retval = dataSave();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        ///------------------------------------------------------------------------------------------------
        Response.Redirect("FormPreview_TKH.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string retval = dataSave();
        if (retval.Split('~')[0].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !!</strong>', '" + gProjName + "'); </script>", false);
        }
    }

    #region viewdetail

    public void PrepopulateDataComm(int id)
    {
        try
        {
            Grd_TL.DataSource = null;
            Grd_TL.DataBind();
            Grd_WC.DataSource = null;
            Grd_WC.DataBind();
            DataSet dslivePre = IncentiveManager.PrepopulateData(id);
            DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan

            #region IndustrailUnit

            if (dtindustryPre.Rows.Count > 0)
            {

                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                //dtindustryPre.Rows[0]["intOrganisationType"].ToString();	
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                    Hid_Org_Doc_Type.Value = dt.Rows[0]["vchDocumentType"].ToString();
                    lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    hidAuthorizing.Value = dt.Rows[0]["vchDocumentType"].ToString();
                }
                else
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                    Hid_Org_Doc_Type.Value = "";
                }



                lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
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
                        Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                        Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
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




                //dtindustryPre.Rows[0]["intUnitType"].ToString();			
                lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();
                //dtindustryPre.Rows[0]["vchDocCode"].ToString();	

                //dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();

                if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
                {
                    lblIs_Priority.Text = "Yes";
                    Pioneersec.Visible = true;

                }
                else
                {
                    lblIs_Priority.Text = "No";
                    Pioneersec.Visible = false;

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

                lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();

                DdlGender.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
                TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                //dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                // Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                //dtindustryPre.Rows[0]["vchCertOfRegdDocCode"].ToString();		


                //dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
                lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();
                //dtindustryPre.Rows[0]["vchProdCommCertBeforeCode"].ToString();	
                //Lbl_Prod_Comm_Before_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertBeforeDocName"].ToString();
                if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                }
                else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                }


                //dtindustryPre.Rows[0]["vchProdCommCertBefore"].ToString();
                if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
                {
                    divbefor.Visible = true;
                    divbefor1.Visible = true;
                    divbefor2.Visible = true;

                }
                else
                {
                    divbefor.Visible = false;
                    divbefor1.Visible = false;
                    divbefor2.Visible = false;
                    lblAfterEMD11.Text = "Date of Production Commencement";
                    lblAfterEMD189.Text = "PC Issuance Date";
                    lbl_PC_No_After.Text = "PC No";
                    lblemd.Text = "";
                    Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                    lblEMDInvestment.Text = "";
                }

                lbl_Prod_Comm_Date_After.Text = dtindustryPre.Rows[0]["dtmProdCommAfter"].ToString();
                lbl_PC_Issue_Date_After.Text = dtindustryPre.Rows[0]["dtmPCIssueDateAfter"].ToString();
                //dtindustryPre.Rows[0]["vchProdCommCertAfterCode"].ToString();		
                //Lbl_Prod_Comm_After_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertAfterDocName"].ToString();

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
            if (dtProductionPre.Rows.Count > 0)
            {
                Grd_Production_Before.DataSource = dtProductionDetBefPre;
                Grd_Production_Before.DataBind();
                Grd_Production_After.DataSource = dtProductionDetAftPre;
                Grd_Production_After.DataBind();

                //dtProductionPre.Rows[0]["intProductionId"].ToString();
                lbl_Direct_Emp_Before.Text = dtProductionPre.Rows[0]["intDirectEmpBefore"].ToString();
                lbl_Contract_Emp_Before.Text = dtProductionPre.Rows[0]["intContractualEmpBefore"].ToString();
                Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocBefore"].ToString();
                lbl_Managarial_Before.Text = dtProductionPre.Rows[0]["intManagerialBefore"].ToString();
                lbl_Supervisor_Before.Text = dtProductionPre.Rows[0]["intSupervisorBefore"].ToString();
                lbl_Skilled_Before.Text = dtProductionPre.Rows[0]["intSkilledBefore"].ToString();
                lbl_Semi_Skilled_Before.Text = dtProductionPre.Rows[0]["intSemiSkilledBefore"].ToString();
                lbl_Unskilled_Before.Text = dtProductionPre.Rows[0]["intUnskilledBefore"].ToString();
                lbl_Total_Emp_Before.Text = dtProductionPre.Rows[0]["intTotalEmpBefore"].ToString();
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


                lbl_Managarial_After.Text = dtProductionPre.Rows[0]["intManagerialAfter"].ToString();
                lbl_Supervisor_After.Text = dtProductionPre.Rows[0]["intSupervisorAfter"].ToString();
                lbl_Skilled_After.Text = dtProductionPre.Rows[0]["intSkilledAfter"].ToString();
                lbl_Semi_Skilled_After.Text = dtProductionPre.Rows[0]["intSemiSkilledAfter"].ToString();
                lbl_Unskilled_After.Text = dtProductionPre.Rows[0]["intUnskilledAfter"].ToString();
                lbl_Total_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpAfter"].ToString();
                lbl_General_After.Text = dtProductionPre.Rows[0]["intGeneralAfter"].ToString();
                lbl_SC_After.Text = dtProductionPre.Rows[0]["intSCAfter"].ToString();
                lbl_ST_After.Text = dtProductionPre.Rows[0]["intSTAfter"].ToString();
                lbl_Total_Cast_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpCastAfter"].ToString();
                lbl_Women_After.Text = dtProductionPre.Rows[0]["intWomenAfter"].ToString();
                lbl_PHD_After.Text = dtProductionPre.Rows[0]["intDisabledAfter"].ToString();

                //dtProductionPre.Rows[0]["intCreatedBy"].ToString();
            }
            #endregion

            #region Investment
            if (dtInvestmentPre.Rows.Count > 0)
            {
                //dtInvestmentPre.Rows[0]["slno"].ToString();
                Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateBefore"].ToString();
                //dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCode"].ToString();
                Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
                Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocBefore"].ToString();
                lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["decLandAmtBefore"].ToString();
                lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["decBuildingAmtBefore"].ToString();
                lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtBefore"].ToString();
                lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
                lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["decTotalAmtBefore"].ToString();
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
            }
            #endregion

            #region MEANS OF FINANCE
            if (dtMeansFinancePre.Rows.Count > 0)
            {

                lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
                Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
                lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
                Lbl_Term_Loan_Doc_Name.Text = dtMeansFinancePre.Rows[0]["vchTermLoanDocCodeNAme"].ToString();

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
            }
            #endregion

            #region Get Documents (OSPCB,Factory & Boiler) from Service

            /*-----------------------------------------------------------*/
            ///// Get Documents (OSPCB,Factory & Boiler) from Service

            BindDocFromService(enServiceDocType.OSPCB, hdnvalidstatutaryfile, flValidStatutary, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, hdnIsOsPCBDownloaded);
            BindDocFromService(enServiceDocType.Boiler, D280, flCleanApproveAuthority, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, hdnBoilderDownloaded);

            #endregion
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
            Grd_TL.DataSource = null;
            Grd_TL.DataBind();
            Grd_WC.DataSource = null;
            Grd_WC.DataBind();
            DataSet dslivePre = IncentiveManager.PostpopulateData(id);
            DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan

            //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();

            #region "Post Industrial Unit Populateddata"

            if (dtindustryPre.Rows.Count > 0)
            {
                DdlGender.SelectedValue = dtindustryPre.Rows[0]["INTGENDER"].ToString();
                TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString();
                radApplyBy.SelectedValue = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();

                if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
                }

                hidAuthorizing.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString();
                lblAuthorizing.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODEName"].ToString();

                hdnAUTHORIZEDFILE.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();

                if (hdnAUTHORIZEDFILE.Value.Trim() != "")
                {
                    hypAUTHORIZEDFILE.Visible = true;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                    FlupAUTHORIZEDFILE.Enabled = false;
                    hypAUTHORIZEDFILE.NavigateUrl = string.Format("~/incentives/{0}/{1}", "/Files/InctBasicDoc", hdnAUTHORIZEDFILE.Value);
                }
                else
                {
                    hypAUTHORIZEDFILE.Visible = false;
                    lnkAUTHORIZEDFILEDdelete.Visible = false;
                    FlupAUTHORIZEDFILE.Enabled = true;
                }
            }

            #endregion

            #region IndustrailUnit
            if (dtindustryPre.Rows.Count > 0)
            {
                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                //dtindustryPre.Rows[0]["intOrganisationType"].ToString();	
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();

                if (dt.Rows.Count > 0)
                {
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                    Hid_Org_Doc_Type.Value = dt.Rows[0]["vchDocumentType"].ToString();
                    lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    hidAuthorizing.Value = dt.Rows[0]["vchDocumentType"].ToString();
                }
                else
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                    Hid_Org_Doc_Type.Value = "";
                }



                lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
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
                        Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                        Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
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




                //dtindustryPre.Rows[0]["intUnitType"].ToString();			
                lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();
                //dtindustryPre.Rows[0]["vchDocCode"].ToString();	

                //dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();

                if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
                {
                    lblIs_Priority.Text = "Yes";
                    Pioneersec.Visible = true;

                }
                else
                {
                    lblIs_Priority.Text = "No";
                    Pioneersec.Visible = false;

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



                lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();

                //DdlGender.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
                //TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString(); 


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


                lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();

                if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                }
                else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                }



                if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
                {
                    divbefor.Visible = true;
                    divbefor1.Visible = true;
                    divbefor2.Visible = true;

                }
                else
                {
                    divbefor.Visible = false;
                    divbefor1.Visible = false;
                    divbefor2.Visible = false;
                    lblAfterEMD11.Text = "Date of Production Commencement";
                    lblAfterEMD189.Text = "PC Issuance Date";
                    lbl_PC_No_After.Text = "PC No";
                    lblemd.Text = "";
                    Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                    lblEMDInvestment.Text = "";
                }

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

                ////dtindustryPre.Rows[0]["intDistrictCode"].ToString();			
                lbl_District.Text = dtindustryPre.Rows[0]["distname"].ToString();
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
            if (dtProductionPre.Rows.Count > 0)
            {
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
            }
            #endregion

            #region Investment


            if (dtInvestmentPre.Rows.Count > 0)
            {
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
            }
            #endregion

            #region MEANS OF FINANCE
            if (dtMeansFinancePre.Rows.Count > 0)
            {
                lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
                Hyp_View_Term_Loan_Doc.NavigateUrl = dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
                lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
                Lbl_Term_Loan_Doc_Name.Text = dtMeansFinancePre.Rows[0]["vchTermLoanDocCodeNAme"].ToString();

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
            }
            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion

    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypViewDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName, LinkButton lnkBtnUpload = null, Image imgshow = null)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFolderName));//, Session["investorid"]
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fuDocument.HasFile)
            {
                if (IsFileValid(fuDocument) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                    return;
                }

                string filename = string.Empty;

                int fileSize = fuDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 2 MB')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 5 MB !', ''); </script>", false);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuDocument.FileName);
                }

                fuDocument.SaveAs(strMainFolderPath + filename);
                hdnDocument.Value = filename;
                hypViewDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
                hypViewDocument.Visible = true;
                lnkDocumentDelete.Visible = true;
                lblDocument.Visible = true;
                fuDocument.Enabled = false;
                lnkBtnUpload.Visible = false;

                imgshow.Attributes.Add("src", "../images/incapproved.png");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtnUpload, LinkButton lnkDel, HyperLink hplnkView, Label lblFile, FileUpload updFile, string strFolderName, Image imgshow = null)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
            string completePath = Server.MapPath(path);
            if (File.Exists(completePath))
            {
                ////File.Delete(completePath);
            }
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtnUpload.Visible = true;
            hplnkView.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
            imgshow.Attributes.Add("src", "../images/cancel-square.png");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void fillSalutation()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "R";
            objBAL.BindDropdown(DdlGender, objEntity);
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

    #region Bind Document From Service

    ///// Added by Sushant Jena on Dated 04-Jan-2018

    /// <summary>
    /// Function to delete all the service document saved in temp folder and create the new ones in respective folder
    /// </summary>
    /// <param name="aEnserviceDocType">service doc type</param>
    /// <param name="hdnFileName">hiddenfield that has the file name</param>
    private void SaveServiceDoc(enServiceDocType aEnserviceDocType, HiddenField hdnFileName, string destfoldername)
    {
        try
        {
            string strPreFix = string.Empty;
            if (aEnserviceDocType == enServiceDocType.Boiler)
            {
                strPreFix = "FactoryBoiler";
            }
            else if (aEnserviceDocType == enServiceDocType.OSPCB)
            {
                strPreFix = "OSPCB";
            }
            string strSourceFile = Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPreFix, Session["investorId"].ToString()));
            if (File.Exists(strSourceFile))
            {
                string strDestinationFile = Server.MapPath(string.Format("~/incentives/Files/{1}/{0}", hdnFileName.Value, destfoldername));
                File.Copy(strSourceFile, strDestinationFile, true);
                hdnFileName.Value = strPreFix + DateTime.Now.ToString("_ddMMyyhhmmss") + ".zip";
                string strReNameFile = Server.MapPath(string.Format("~/incentives/Files/{1}/{0}", hdnFileName.Value, destfoldername));

                System.IO.File.Move(strDestinationFile, strReNameFile);
                //then delete the old folder and old zip folder
                File.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPreFix, Session["investorId"].ToString())));
                Directory.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}", strPreFix, Session["investorId"].ToString())), true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Functiion to get certificate details for OSPCB/Factory and boiler from service
    /// </summary>
    /// <param name="aEnServiceDocType">Service Doc Type i.e whether boiler or factory</param>
    /// <param name="Hid_File_Name">name of hidden field in which the document filename will be stored</param>
    /// <param name="FU_Upload">fileupload control for the document to disable them in case service has document</param>
    /// <param name="LnkBtn_Add">add linkbutton for the document to disable them in case service has document</param>
    /// <param name="LnkBtn_Delete">delete linkbutton for the document to disable them in case service has document</param>
    /// <param name="Hyp_View_Doc">Hyperlink to view/download the document</param>
    /// <param name="Hid_Service_Doc_Status">hidden field to store the status as to whether doc is present or not</param>    
    private void BindDocFromService(enServiceDocType aEnServiceDocType, HiddenField Hid_File_Name, FileUpload FU_Upload, LinkButton LnkBtn_Add, LinkButton LnkBtn_Delete, HyperLink Hyp_View_Doc, HiddenField Hid_Service_Doc_Status)
    {
        try
        {
            /*------------------------------------------------------------------*/
            //first send the investorid to database and get all the records for documents
            InctSearch objSearch = new InctSearch()
            {
                intUserUnitType = Convert.ToInt32(Session["investorid"]),
                strActionCode = "view",
                intUnitType = (int)aEnServiceDocType
            };

            List<string> lstFiles = new List<string>();
            IncentiveMasterBusinessLayer objInctBuisnessLayer = new IncentiveMasterBusinessLayer();
            lstFiles = objInctBuisnessLayer.ViewInctOSPCBDetails(objSearch);

            /*------------------------------------------------------------------*/
            ////// File Zipping Process
            string strTempFilePath = IncentiveCommonFunctions.GetCertificateDetailsFromService(aEnServiceDocType, lstFiles, Convert.ToInt32(Session["investorid"]));

            if (!string.IsNullOrEmpty(strTempFilePath))
            {
                //set hidden field value
                Hid_File_Name.Value = string.Format("{0}.zip", Session["investorId"].ToString());

                //disable the file upload control
                FU_Upload.Enabled = false;
                LnkBtn_Add.Visible = false;

                //remove the delete button
                LnkBtn_Delete.Visible = false;

                //// Assign File Path to Hyperlink for Display
                Hyp_View_Doc.Visible = true;
                Hyp_View_Doc.NavigateUrl = strTempFilePath;
                Hid_Service_Doc_Status.Value = "1";
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    #endregion
}