using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Incentive;
using System.IO;
using BusinessLogicLayer.Incentive;

public partial class incentives_Exemption_Electricity_Duty : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #region "Methods"

    private void crdtdutylectric()
    {
        ViewState["dtdutylectric"] = null;

        DataTable dtdutylectric = new DataTable();

        DataColumn dcRowId = new DataColumn("dcRowId");
        dcRowId.DataType = Type.GetType("System.Int32");
        dcRowId.AutoIncrement = true;
        dcRowId.AutoIncrementSeed = 1;
        dcRowId.AutoIncrementStep = 1;
        dtdutylectric.Columns.Add(dcRowId);

        DataColumn dcstategovt = new DataColumn("dcstategovt");
        dcstategovt.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcstategovt);

        DataColumn dcfrmdate = new DataColumn("dcfrmdate");
        dcfrmdate.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcfrmdate);

        DataColumn dctodate = new DataColumn("dctodate");
        dctodate.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dctodate);

        DataColumn dcamtclaim = new DataColumn("dcamtclaim");
        dcamtclaim.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcamtclaim);

        DataColumn dcmfininst = new DataColumn("dcmfininst");
        dcmfininst.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcmfininst);

        DataColumn vchrowdb = new DataColumn("vchrowdb");
        vchrowdb.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(vchrowdb);


        ViewState["dtdutylectric"] = dtdutylectric;
        //grdelectric.DataSource = dtdutylectric;
        //grdelectric.DataBind();


    }
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
    private string ReturnDateFormat(string srcDate)
    {
        string resdt = "1900/01/01";
        try
        {
            if (srcDate != "")
            {
                string[] strarr = srcDate.Split('/');
                resdt = strarr[2] + "/" + strarr[0] + "/" + strarr[1];
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

        return resdt;
    }
    private string RetDateFrmDB(string srcDate)
    {
        string retdt = "";
        try
        {
            if (srcDate != "")
            {
                DateTime dbdt = Convert.ToDateTime(srcDate);
                retdt = dbdt.ToString("MM/dd/yyyy");

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retdt;
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
            objEntity.FYear = Convert.ToInt16(lblfinyear.Text.Trim());

            objEntity.incentivetype = 4;
            objEntity.FormType = FormNumber.ElectricityDuty_12;

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
            objEntity.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = Hid_Auth_Letter_File_Name.Value;
            objEntity.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;

            #endregion

            #region "Electricity Consumption/Load Details "

            ConsumeLoadDet objconsume = new ConsumeLoadDet();
            List<Statedetails> statelist = new List<Statedetails>();
            DataTable dtdutylectric = new DataTable();
            dtdutylectric = (DataTable)ViewState["dtdutylectric"];

            objEntity.ConsumLoadDet = objconsume;

            objconsume.stringSUPPLYDATE = txtsupplydate.Text;
            objconsume.stringCONSUMENUMBER = txtconsumenumber.Text;
            objconsume.stringCONNECTEDLOAD = txtconnectedload.Text;
            objconsume.INTELECTRICITYAVIL = "1";
            objconsume.strbillpmtvoucher = Hid_Electricity_Bill_File_Name.Value;
            objconsume.strelecconsumption = "";
            objconsume.strElecDetails = "";
            objconsume.strdpsdocument = Hid_DPS_File_Name.Value;
            objconsume.strcondocument = Hid_Connect_Load_File_Name.Value;


            List<Statedetails> LstEmp = new List<Statedetails>();
            for (int i = 0; i < dtdutylectric.Rows.Count; i++)
            {
                Statedetails objstateitem = new Statedetails();
                objstateitem.strStateName = dtdutylectric.Rows[i]["dcstategovt"].ToString();
                objstateitem.strStatefrmDate = dtdutylectric.Rows[i]["dcfrmdate"].ToString();
                objstateitem.strStatetodate = dtdutylectric.Rows[i]["dctodate"].ToString();
                objstateitem.strStateAmt = dtdutylectric.Rows[i]["dcamtclaim"].ToString();
                objstateitem.strStateFin = dtdutylectric.Rows[i]["dcmfininst"].ToString();

                LstEmp.Add(objstateitem);
            }
            objEntity.ConsumLoadDet.TstrSTATEDETAIL = LstEmp;
            objEntity.ConsumLoadDet = objconsume;

            #endregion

            #region "Availed Details"

            AvailDetails objAvailDetails = new AvailDetails();

            objAvailDetails.NeverAvailedPrior = Convert.ToInt16(RadBtn_Availed_Earlier.SelectedValue.ToString());
            objAvailDetails.SubsidyAvailed = 0;
            objAvailDetails.UndertakingSubsidyDoc = Hid_Undertaking_File_Name.Value;
            //objAvailDetails.SupportingDocs = hdnFinDoc.Value;
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

            #region "Bank Details "

            BankDetails obj = new BankDetails();
            obj.BankName = txtbankname.Text;
            obj.BranchName = txtbranchname.Text;
            obj.IFSCNo = txtifsc.Text;
            obj.AccountNo = txtAccNo.Text;
            obj.MICRNo = txtmicr.Text;
            obj.BankDoc = Hid_Cancelled_Cheque_File_Name.Value;
            objEntity.BankDet = obj;
            objEntity.FormType = FormNumber.ElectricityDuty_12;

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

            retVal = IncentiveManager.CreateIncentiveElectricity(objEntity);

            return retVal;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retVal;

    }
    private void PostPopulateData(int id)
    {
        try
        {
            Incentive objEntity = new Incentive();
            GetAndViewParam objPar = new GetAndViewParam();
            objEntity.strcActioncode = "12";
            objPar.Param1ID = "";
            objPar.Param2ID = "";
            objPar.Param3ID = "";
            objPar.InctType = 4;
            objEntity.UnqIncentiveId = id;
            objEntity.GetVwPrmtrs = objPar;
            objEntity.FormType = FormNumber.ElectricityDuty_12;
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveEDD(objEntity);

            #region "Electricity"

            DataTable dtelectric = ds.Tables[0];
            if (dtelectric.Rows.Count > 0)
            {
                txtsupplydate.Text = dtelectric.Rows[0]["DTMSUPPLYDATE"].ToString();
                txtconsumenumber.Text = dtelectric.Rows[0]["VCHCONSUMENUMBER"].ToString();
                txtconnectedload.Text = dtelectric.Rows[0]["VCHCONNECTEDLOAD"].ToString();

                /*-------------------------------------------------------------*/

                Hid_DPS_File_Name.Value = dtelectric.Rows[0]["vchdopsdoc"].ToString();
                if (Hid_DPS_File_Name.Value.Trim() != "")
                {
                    Hyp_View_DPS_Doc.Visible = true;
                    LnkBtn_Delete_DPS_Doc.Visible = true;
                    FU_DPS_Doc.Enabled = false;
                    Hyp_View_DPS_Doc.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "EDD", Hid_DPS_File_Name.Value);
                }
                else
                {
                    Hyp_View_DPS_Doc.Visible = false;
                    LnkBtn_Delete_DPS_Doc.Visible = false;
                    FU_DPS_Doc.Enabled = true;
                }

                /*-------------------------------------------------------------*/

                Hid_Connect_Load_File_Name.Value = dtelectric.Rows[0]["vchcondoc"].ToString();
                if (Hid_Connect_Load_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Connect_Load.Visible = true;
                    LnkBtn_Delete_Connect_Load_Doc.Visible = true;
                    FU_Connect_Load_Doc.Enabled = false;
                    Hyp_View_Connect_Load.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "EDD", Hid_Connect_Load_File_Name.Value);
                }
                else
                {
                    Hyp_View_Connect_Load.Visible = false;
                    LnkBtn_Delete_Connect_Load_Doc.Visible = false;
                    FU_Connect_Load_Doc.Enabled = true;
                }

                /*-------------------------------------------------------------*/

                Hid_Electricity_Bill_File_Name.Value = dtelectric.Rows[0]["vchpmtvoucherdoc"].ToString();
                if (Hid_Electricity_Bill_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Electricity_Bill_Doc.Visible = true;
                    LnkBtn_Delete_Electricity_Bill.Visible = true;
                    FU_Electricity_Bill.Enabled = false;
                    Hyp_View_Electricity_Bill_Doc.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "EDD", Hid_Electricity_Bill_File_Name.Value);
                }
                else
                {
                    Hyp_View_Electricity_Bill_Doc.Visible = false;
                    LnkBtn_Delete_Electricity_Bill.Visible = false;
                    FU_Electricity_Bill.Enabled = true;
                }
            }

            #endregion

            #region "Availed Details"

            DataTable dtavail = ds.Tables[3];
            DataTable dtavailgrd1 = ds.Tables[4];

            if (dtavail.Rows.Count > 0)
            {
                Hid_Undertaking_File_Name.Value = dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                if (Hid_Undertaking_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Undertaking_Doc.Visible = true;
                    LnkBtn_Delete_Undertaking_Doc.Visible = true;
                    FU_Undertaking_Doc.Enabled = false;
                    Hyp_View_Undertaking_Doc.NavigateUrl = string.Format("~/incentives/files/{0}/{1}", "EDD", Hid_Undertaking_File_Name.Value);
                }
                else
                {
                    Hyp_View_Undertaking_Doc.Visible = false;
                    LnkBtn_Delete_Undertaking_Doc.Visible = false;
                    FU_Undertaking_Doc.Enabled = true;
                }

                //HiddenField1.Value = dtavail.Rows[0]["vchSupportingDocs"].ToString();
                //Label4.Text = dtavail.Rows[0]["vchSupportingDocs"].ToString();

                Hid_Asst_Sanc_File_Name.Value = dtavail.Rows[0]["VchSanctionDoc"].ToString();
                if (Hid_Asst_Sanc_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Asst_Sanc_Doc.Visible = true;
                    LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
                    FU_Asst_Sanc_Doc.Enabled = false;
                    Hyp_View_Asst_Sanc_Doc.NavigateUrl = string.Format("~/incentives/files/{0}/{1}", "EDD", Hid_Asst_Sanc_File_Name.Value);
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

            #region "Bank Details"
            DataTable dtbank = ds.Tables[2];
            if (dtbank.Rows.Count > 0)
            {
                txtAccNo.Text = dtbank.Rows[0]["VCHACCOUNTNO"].ToString();
                txtbankname.Text = dtbank.Rows[0]["VCHBANKNAME"].ToString();
                txtbranchname.Text = dtbank.Rows[0]["VCHBRANCHNAME"].ToString();
                txtifsc.Text = dtbank.Rows[0]["VCHIFSCNO"].ToString();
                txtmicr.Text = dtbank.Rows[0]["VCHMICR"].ToString();
                Hid_Cancelled_Cheque_File_Name.Value = dtbank.Rows[0]["vchBankDoc"].ToString();

                if (Hid_Cancelled_Cheque_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Cancelled_Cheque.Visible = true;
                    LnkBtn_Delete_Cancelled_Cheque.Visible = true;
                    FU_Cancelled_Cheque.Enabled = false;
                    Hyp_View_Cancelled_Cheque.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "EDD", Hid_Cancelled_Cheque_File_Name.Value);
                }
                else
                {
                    Hyp_View_Cancelled_Cheque.Visible = false;
                    LnkBtn_Delete_Cancelled_Cheque.Visible = false;
                    FU_Cancelled_Cheque.Enabled = true;
                }



            }
            #endregion

            //FillAlldocs(ds.Tables[5]);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    private string RemoveDecimal(string strval)
    {
        string retStr = strval;
        try
        {
            if (strval.IndexOf(".") > 0)
            {
                retStr = strval.Substring(strval.IndexOf("."), 3);
                retStr = strval.Remove(strval.IndexOf("."), 3);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retStr;
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
    private void documentsindt(string strdocid, string filname, string foldername)
    {
        DataTable dtdocument = new DataTable();
        dtdocument = (DataTable)ViewState["dtdocument"];
        DataRow drdoc = dtdocument.NewRow();
        drdoc["vchDocId"] = strdocid;
        drdoc["vchFileName"] = filname;
        drdoc["vchFilePath"] = "../incentives/" + foldername + "/";
        dtdocument.Rows.Add(drdoc);
        ViewState["dtdocument"] = dtdocument;

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
            File.Delete(completePath);
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
    private void precheckpreEDDdata()
    {
        try
        {
            Incentive objEntityExe = new Incentive();
            objEntityExe.Userid = Convert.ToInt16(Session["InvestorId"]);
            objEntityExe.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            DataSet dsEDDearly = IncentiveManager.getPreEDDdataExistance(objEntityExe);

            if (dsEDDearly.Tables[0].Rows.Count > 0)
            {

                lblsupplydate.Text = dsEDDearly.Tables[0].Rows[0]["DTMSUPPLYDATE"].ToString();
                txtsupplydate.Text = dsEDDearly.Tables[0].Rows[0]["DTMSUPPLYDATE"].ToString();
                txtconsumenumber.Text = dsEDDearly.Tables[0].Rows[0]["VCHCONSUMENUMBER"].ToString();
                txtconnectedload.Text = dsEDDearly.Tables[0].Rows[0]["VCHCONNECTEDLOAD"].ToString();


                if (dsEDDearly.Tables[0].Rows[0]["DTMSUPPLYDATE"].ToString().Trim() == "")
                {
                    Div4.Visible = true;
                    Div8.Visible = false;
                }
                else
                {
                    Div4.Visible = false;
                    Div8.Visible = true;
                }
                if (dsEDDearly.Tables[0].Rows[0]["VCHCONSUMENUMBER"].ToString().Trim() == "")
                {
                    txtconsumenumber.Enabled = true;

                }
                else
                {
                    txtconsumenumber.Enabled = false;
                }
                if (dsEDDearly.Tables[0].Rows[0]["VCHCONNECTEDLOAD"].ToString().Trim() == "")
                {
                    txtconnectedload.Enabled = true;
                }
                else
                {
                    txtconnectedload.Enabled = false;
                }
            }
            else
            {
                txtconnectedload.Enabled = true;
                txtconsumenumber.Enabled = true;
                Div4.Visible = true;
                Div8.Visible = false;

            }






            if (dsEDDearly.Tables[1].Rows.Count > 0)
            {
                DataTable dtdutylectric = new DataTable();
                dtdutylectric = (DataTable)ViewState["dtdutylectric"];

                foreach (DataRow drelecdrdb in dsEDDearly.Tables[1].Rows)
                {
                    DataRow dr = dtdutylectric.NewRow();
                    dr["dcstategovt"] = drelecdrdb["VCHSTATEGOVT"].ToString();
                    dr["dcfrmdate"] = drelecdrdb["DTMFRMDATE"].ToString();
                    dr["dctodate"] = drelecdrdb["DTMTODATE"].ToString();
                    dr["dcamtclaim"] = drelecdrdb["NUMCLAIMAMOUNT"].ToString();
                    dr["dcmfininst"] = drelecdrdb["VCHFININST"].ToString();
                    dr["vchrowdb"] = drelecdrdb["INTELECDETID"].ToString();
                    dtdutylectric.Rows.Add(dr);

                }
                ViewState["dtdutylectric"] = dtdutylectric;
                grdelectric.DataSource = dtdutylectric;
                grdelectric.DataBind();


            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void FillAlldocs(DataTable dtDocument)
    {
        try
        {
            DataRow[] dr = dtDocument.Select("vchDocId in ('D149','D267','D268','D230','D253','D266')");
            if (dr.Length > 0)
            {
                DataTable dtdocument = new DataTable();
                dtdocument = (DataTable)ViewState["dtdocument"];


                foreach (DataRow rows in dr.Take(5))
                {
                    if (rows["vchDocId"].ToString().Trim() == "D149")
                    {
                        Hid_Electricity_Bill_File_Name.Value = rows["vchFileName"].ToString().Trim();
                        Hyp_View_Electricity_Bill_Doc.NavigateUrl = string.Format("~/incentives/" + rows["vchFolderPath"].ToString() + "{0}", rows["vchFileName"]);
                        Hyp_View_Electricity_Bill_Doc.Visible = true;
                        LnkBtn_Delete_Electricity_Bill.Visible = true;
                        FU_Electricity_Bill.Enabled = false;

                    }

                    if (rows["vchDocId"].ToString().Trim() == "D267")
                    {
                        Hid_DPS_File_Name.Value = rows["vchFileName"].ToString().Trim();
                        Hyp_View_DPS_Doc.NavigateUrl = string.Format("~/incentives/" + rows["vchFolderPath"].ToString() + "{0}", rows["vchFileName"]);
                        Hyp_View_DPS_Doc.Visible = true;
                        LnkBtn_Delete_DPS_Doc.Visible = true;
                        FU_DPS_Doc.Enabled = false;
                    }


                    if (rows["vchDocId"].ToString().Trim() == "D268")
                    {
                        Hid_Connect_Load_File_Name.Value = rows["vchFileName"].ToString().Trim();
                        Hyp_View_Connect_Load.NavigateUrl = string.Format("~/incentives/" + rows["vchFolderPath"].ToString() + "{0}", rows["vchFileName"]);
                        Hyp_View_Connect_Load.Visible = true;
                        LnkBtn_Delete_Connect_Load_Doc.Visible = true;
                        FU_Connect_Load_Doc.Enabled = false;
                    }

                    if (rows["vchDocId"].ToString().Trim() == "D230")
                    {
                        Hid_Undertaking_File_Name.Value = rows["vchFileName"].ToString().Trim();
                        Hyp_View_Undertaking_Doc.NavigateUrl = string.Format("~/incentives/" + rows["vchFolderPath"].ToString() + "{0}", rows["vchFileName"]);
                        Hyp_View_Undertaking_Doc.Visible = true;
                        LnkBtn_Delete_Undertaking_Doc.Visible = true;
                        FU_Undertaking_Doc.Enabled = false;
                    }

                    if (rows["vchDocId"].ToString().Trim() == "D253")
                    {
                        Hid_Asst_Sanc_File_Name.Value = rows["vchFileName"].ToString().Trim();
                        Hyp_View_Asst_Sanc_Doc.NavigateUrl = string.Format("~/incentives/" + rows["vchFolderPath"].ToString() + "{0}", rows["vchFileName"]);
                        Hyp_View_Asst_Sanc_Doc.Visible = true;
                        LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
                        FU_Asst_Sanc_Doc.Enabled = false;
                    }

                    if (rows["vchDocId"].ToString().Trim() == "D266")
                    {
                        Hid_Cancelled_Cheque_File_Name.Value = rows["vchFileName"].ToString().Trim();
                        Hyp_View_Cancelled_Cheque.NavigateUrl = string.Format("~/incentives/" + rows["vchFolderPath"].ToString() + "{0}", rows["vchFileName"]);
                        Hyp_View_Cancelled_Cheque.Visible = true;
                        LnkBtn_Delete_Cancelled_Cheque.Visible = true;
                        FU_Cancelled_Cheque.Enabled = false;
                    }


                    DataRow drnew = dtdocument.NewRow();
                    drnew["vchDocId"] = rows["vchDocId"].ToString();
                    drnew["vchFileName"] = rows["vchFileName"].ToString();
                    drnew["vchFilePath"] = rows["vchFolderPath"].ToString();
                    dtdocument.Rows.Add(drnew);
                }


                ViewState["dtdocument"] = dtdocument;


            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
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
    private void PrepopulateDataPlus(int id)
    {
        try
        {
            DataSet dslivePre = IncentiveManager.PostpopulateDataPLUS(id);
            DataTable dtBank = dslivePre.Tables[0];////////////industry panel
            if (dtBank.Rows.Count > 0)
            {
                PreBankPlus(dtBank);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void PreBankPlus(DataTable dtBank)
    {
        try
        {
            txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
            txtbankname.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
            txtbranchname.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
            txtifsc.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
            txtmicr.Text = dtBank.Rows[0]["VCHMICR"].ToString();
            if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
            {
                Hid_Cancelled_Cheque_File_Name.Value = dtBank.Rows[0]["vchBankDoc"].ToString(); /////AUTHORIZEDFILE file upload
                Hyp_View_Cancelled_Cheque.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
                Hyp_View_Cancelled_Cheque.Visible = true;
                LnkBtn_Delete_Cancelled_Cheque.Visible = true;
                //lblOrgDocument.Visible = true;
                FU_Cancelled_Cheque.Enabled = false;
            }
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtsupplydate.Attributes.Add("readonly", "readonly");
            txtstatefrmdate.Attributes.Add("readonly", "readonly");
            txtstatetodate.Attributes.Add("readonly", "readonly");
            txtsacdat.Attributes.Add("readonly", "readonly");

            GetMasterdetails();
            if (Session["FyYear"] != null && Convert.ToString(Session["FyYear"]) != "")
            {
                lblfinyear.Text = Convert.ToString(Session["FyYear"]);
            }
            else
            {
                lblfinyear.Text = Convert.ToString(int.Parse(DateTime.Now.Month.ToString()) >= 4 ? int.Parse(DateTime.Now.Year.ToString()) : int.Parse(DateTime.Now.Year.ToString()) - 1);

            }

            //Assigning Financial From Date and to date in hidden field
            //-------------------------------------------------------------------------
            hdffinfrm.Value = "01-Apr-" + lblfinyear.Text;
            hdffintod.Value = "31-Mar-" + Convert.ToString(Convert.ToInt16(lblfinyear.Text) + 1);
            //----------------------------------------------------------------------------


            if (!Page.IsPostBack)
            {
                fillSalutation();
                crdtdutylectric();
                crdtincentive();
                documenttable();

                if (Convert.ToString(Session["ApplySource"]) == "0")
                {
                    PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                    PostPopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                }
                else
                {
                    PrepopulateDataComm(Convert.ToInt16(Session["InvestorId"]));
                    PrepopulateDataPlus(Convert.ToInt16(Session["InvestorId"]));

                }

                #region "Pre checking for EDD record exist or not for user id and  invoice number"
                precheckpreEDDdata();
                #endregion
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    #region "For ElectricityGrid"
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblfinyear.Text.Trim() != "")
            {
                DateTime dtstart = Convert.ToDateTime("01-Apr-" + lblfinyear.Text);
                DateTime dtenddt = Convert.ToDateTime("31-Mar-" + Convert.ToString(Convert.ToInt16(lblfinyear.Text) + 1));

                //if ((Convert.ToDateTime(txtstatefrmdate.Text) >= dtstart) && (Convert.ToDateTime(txtstatefrmdate.Text) <= dtenddt))
                //{
                //    if ((Convert.ToDateTime(txtstatetodate.Text) >= dtstart) && (Convert.ToDateTime(txtstatetodate.Text) <= dtenddt))
                //    {

                DataTable dtdutylectric = new DataTable();
                dtdutylectric = (DataTable)ViewState["dtdutylectric"];
                DataRow dr = dtdutylectric.NewRow();
                dr["dcstategovt"] = "";
                dr["dcfrmdate"] = txtstatefrmdate.Text.Trim();
                dr["dctodate"] = txtstatetodate.Text.Trim().Trim();
                dr["dcamtclaim"] = txtstateamt.Text.Trim();
                dr["dcmfininst"] = txtfininst.Text.Trim();
                dr["vchrowdb"] = "0";
                dtdutylectric.Rows.Add(dr);
                ViewState["dtdutylectric"] = dtdutylectric;

                grdelectric.DataSource = dtdutylectric;
                grdelectric.DataBind();


                txtstatefrmdate.Text = "";
                txtstatetodate.Text = "";
                txtstateamt.Text = "";
                txtfininst.Text = "";
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script> jAlert('To date is not coming within above financial year', 'SWP'); </script>", true);
                //        return;
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script> jAlert('From date is not coming within above financial year', 'SWP'); </script>", true);
                //    return;
                //}
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void grdelectric_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            HiddenField hdfanew = (HiddenField)grdelectric.Rows[e.RowIndex].Cells[5].FindControl("elechdfrowid");
            DataTable dtnew0 = new DataTable();
            dtnew0 = (DataTable)ViewState["dtdutylectric"];
            DataRow[] dr1 = null;
            dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {

                dr1[i].Delete();
            }
            dtnew0.AcceptChanges();
            grdelectric.DataSource = dtnew0;
            grdelectric.DataBind();
            ViewState["dtdutylectric"] = dtnew0;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void grdelectric_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfrowdb = (HiddenField)e.Row.FindControl("hdnelecrowdb");
                LinkButton lnkdel = (LinkButton)e.Row.FindControl("lnkdel");
                if (hdfrowdb.Value.Trim() == "0")
                {
                    lnkdel.Visible = true;
                }
                else
                {
                    lnkdel.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    #endregion

    #region "For Avail Details Grid"

    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void grdAssistanceDetailsAD_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion

    #region "File UPLOADS"

    #region "Electric Section"

    protected void LnkBtn_Upload_Electricity_Bill_Click(object sender, EventArgs e) /// for upload Details of Last month Electricity Bill with payment voucher
    {
        if (FU_Electricity_Bill.HasFile)
        {
            if (IsFileValid(FU_Electricity_Bill) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }
            string extension = Path.GetExtension(FU_Electricity_Bill.PostedFile.FileName);
            string filename = "PMT" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(FU_Electricity_Bill, Hid_Electricity_Bill_File_Name, filename, Hyp_View_Electricity_Bill_Doc, Lbl_Msg_Electricity_Bill_Doc, LnkBtn_Delete_Electricity_Bill, "EDD");
            documentsindt(hdnpmtvoucher.Value, filename, "EDD");
        }
    }
    protected void LnkBtn_Delete_Electricity_Bill_Click(object sender, EventArgs e) /// for delete Details of Last month Electricity Bill with payment voucher
    {

        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Electricity_Bill.ID))
        {
            UpdFileRemove(Hid_Electricity_Bill_File_Name, LnkBtn_Upload_Electricity_Bill, LnkBtn_Delete_Electricity_Bill, Hyp_View_Electricity_Bill_Doc, Lbl_Msg_Electricity_Bill_Doc, FU_Electricity_Bill, "EDD");
        }

    }

    protected void LnkBtn_Upload_DPS_Doc_Click(object sender, EventArgs e)/// for upload Details of document for power supply
    {
        if (FU_DPS_Doc.HasFile)
        {
            if (IsFileValid(FU_DPS_Doc) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }

            string extension = Path.GetExtension(FU_DPS_Doc.PostedFile.FileName);
            string filename = "DPS" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(FU_DPS_Doc, Hid_DPS_File_Name, filename, Hyp_View_DPS_Doc, Lbl_Msg_DPS_Doc, LnkBtn_Delete_DPS_Doc, "EDD");
            documentsindt(hdnsopdocid.Value, filename, "EDD");
        }
    }
    protected void LnkBtn_Delete_DPS_Doc_Click(object sender, EventArgs e)/// for delete Details of document for power supply
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_DPS_Doc.ID))
        {
            UpdFileRemove(Hid_DPS_File_Name, LnkBtn_Upload_DPS_Doc, LnkBtn_Delete_DPS_Doc, Hyp_View_DPS_Doc, Lbl_Msg_DPS_Doc, FU_DPS_Doc, "EDD");
        }
    }

    protected void LnkBtn_Upload_Connect_Load_Doc_Click(object sender, EventArgs e)/// for upload Details of document for contract demand
    {
        if (FU_Connect_Load_Doc.HasFile)
        {
            if (IsFileValid(FU_Connect_Load_Doc) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }

            string extension = Path.GetExtension(FU_Connect_Load_Doc.PostedFile.FileName);
            string filename = "CNL" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(FU_Connect_Load_Doc, Hid_Connect_Load_File_Name, filename, Hyp_View_Connect_Load, Lbl_Msg_Connect_Load_Doc, LnkBtn_Delete_Connect_Load_Doc, "EDD");
            documentsindt(hdncondocid.Value, filename, "EDD");
        }

    }
    protected void LnkBtn_Delete_Connect_Load_Doc_Click(object sender, EventArgs e)/// for delete Details of document for contract demand
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Connect_Load_Doc.ID))
        {
            UpdFileRemove(Hid_Connect_Load_File_Name, LnkBtn_Upload_Connect_Load_Doc, LnkBtn_Delete_Connect_Load_Doc, Hyp_View_Connect_Load, Lbl_Msg_Connect_Load_Doc, FU_Connect_Load_Doc, "EDD");
        }
    }

    #endregion

    #region "Industrial Unit"

    protected void LnkBtn_Upload_Auth_Letter_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        if (string.Equals(lnk.ID, LnkBtn_Upload_Auth_Letter.ID))
        {
            if (FU_Auth_Letter.HasFile)
            {
                if (IsFileValid(FU_Auth_Letter) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                    return;
                }

                string extension = Path.GetExtension(FU_Auth_Letter.PostedFile.FileName);
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss") + extension;
                string strFolderName = "InctBasicDoc";
                UploadDocument(FU_Auth_Letter, Hid_Auth_Letter_File_Name, strFileName, Hyp_View_Auth_Letter, Lbl_Msg_Auth_Letter, LnkBtn_Delete_Auth_Letter, strFolderName);
                //documentsindt(hdnexemption.Value, strFileName,"IndustryUnit");
            }
            else
            {
            }
        }
    }
    protected void LnkBtn_Delete_Auth_Letter_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Auth_Letter.ID))
        {
            string strFolderName = "InctBasicDoc";
            UpdFileRemove(Hid_Auth_Letter_File_Name, LnkBtn_Upload_Auth_Letter, LnkBtn_Delete_Auth_Letter, Hyp_View_Auth_Letter, Lbl_Msg_Auth_Letter, FU_Auth_Letter, strFolderName);
        }
    }

    #endregion

    #region "Avail Details"

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
            UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, filename, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, "EDD");
            documentsindt(hdnundertakingdocid.Value, filename);
        }
    }
    protected void LnkBtn_Delete_Undertaking_Doc_Click(object sender, EventArgs e)/// for delete uploaded undertaking document
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Undertaking_Doc.ID))
        {
            UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, "EDD");
        }
    }

    protected void LnkBtn_Upload_Asst_Sanc_Doc_Click(object sender, EventArgs e) // Document details of assistance sanctioned
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
            UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, filename, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, "EDD");
            documentsindt(hdndetailsassistantdocid.Value, filename);
        }
    }
    protected void LnkBtn_Delete_Asst_Sanc_Doc_Click(object sender, EventArgs e) // Document details of assistance sanctioned
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Asst_Sanc_Doc.ID))
        {
            UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, "EDD");
        }
    }

    #endregion

    #region "Bank Details"

    protected void LnkBtn_Upload_Cancelled_Cheque_Click(object sender, EventArgs e)
    {
        if (FU_Cancelled_Cheque.HasFile)
        {
            if (IsFileValid(FU_Cancelled_Cheque) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }

            string extension = Path.GetExtension(FU_Cancelled_Cheque.PostedFile.FileName);
            string filename = "BNK" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
            UploadDocument(FU_Cancelled_Cheque, Hid_Cancelled_Cheque_File_Name, filename, Hyp_View_Cancelled_Cheque, Lbl_Msg_Cancelled_Cheque, LnkBtn_Delete_Cancelled_Cheque, "EDD");
            documentsindt(hdnbnkdocid.Value, filename, "EDD");
        }
    }
    protected void LnkBtn_Delete_Cancelled_Cheque_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Cancelled_Cheque.ID))
        {
            UpdFileRemove(Hid_Cancelled_Cheque_File_Name, LnkBtn_Upload_Cancelled_Cheque, LnkBtn_Delete_Cancelled_Cheque, Hyp_View_Cancelled_Cheque, Lbl_Msg_Cancelled_Cheque, FU_Cancelled_Cheque, "EDD");
        }
    }

    #endregion


    #endregion

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string retval = dataSave();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        /////------------------------------------------------------------------------------------------------
        Response.Redirect("FormPreview_EDD.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
        //Response.Redirect("FormPreview_EDD.aspx");
    }
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string retval = dataSave();
        if (retval.Split('~')[0].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !!</strong>', '" + strProjName + "'); </script>", false);
        }
    }


    #region Commonviewdetail

    public void PrepopulateDataComm(int id)
    {
        try
        {
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


                lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();


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

                DdlGender.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
                TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
                lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();
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


                lbl_District.Text = dtindustryPre.Rows[0]["distname"].ToString();
                lbl_Sector.Text = dtindustryPre.Rows[0]["sectorName"].ToString();
                lbl_Sub_Sector.Text = dtindustryPre.Rows[0]["SubsectorName"].ToString();
                Lbl_Derived_Sector.Text = dtindustryPre.Rows[0]["vchDerivedSector"].ToString();

                if (dtindustryPre.Rows[0]["bitPriorityIPR"].ToString() == "1")
                {
                    lbl_Sectoral.Text = "Yes";
                }
                else
                {
                    lbl_Sectoral.Text = "No";
                }

            }
            #endregion

            #region Production
            if (dtProductionPre.Rows.Count > 0)
            {
                Grd_Production_Before.DataSource = dtProductionDetBefPre;
                Grd_Production_Before.DataBind();
                Grd_Production_After.DataSource = dtProductionDetAftPre;
                Grd_Production_After.DataBind();


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
                Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
                Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
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
                //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
                lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
                Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
                lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
                //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
                //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
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

            #region "Document Checklist Dynamic bind"


            tdorgdoc.InnerHtml = Lbl_Org_Doc_Type.Text.Trim(); ///////// TD assignments to dynamic document name
            tdunitDoc.InnerHtml = Lbl_Unit_Type_Doc_Name.Text.Trim(); ///////// TD assignments to dynamic document name
            tdcommafterdoc.InnerHtml = Lbl_Prod_Comm_After_Doc_Name.Text.Trim();
            tdpioneerdoc.InnerHtml = Lbl_Pioneer_Doc_Name.Text.Trim();
            tdempafterdoc.InnerHtml = Lbl_Direct_Emp_After_Doc_Name.Text.Trim();
            tdFFCIafterdoc.InnerHtml = Lbl_FFCI_After_Doc_Name.Text.Trim();
            tdDPRafterdoc.InnerHtml = Lbl_Approved_DPR_After_Doc_Name.Text.Trim();
            tdtermloandoc.InnerHtml = Lbl_Term_Loan_Doc_Name.Text.Trim();


            if (dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString().Trim() != "")
            {
                Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
            }
            if (dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString().Trim() != "")
            {

                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            }

            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                if (dtindustryPre.Rows[0]["vchappnoAft"].ToString().Trim() != "")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                if (dtindustryPre.Rows[0]["vchappnoAft"].ToString().Trim() != "")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
            }
            if (dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString().Trim() != "")
            {
                Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString();
            }
            if (dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString().Trim() != "")
            {
                Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
            }
            if (dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString().Trim() != "")
            {
                Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString();
            }
            if (dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString().Trim() != "")
            {
                Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString();
            }
            if (dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString().Trim() != "")
            {
                Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();

            }


            if (tdunitDoc.InnerHtml.Trim() != "")
            {
                trunitdoc.Visible = false;
            }
            if (tdorgdoc.InnerHtml.Trim() != "")
            {
                trorgdoc.Visible = false;
            }
            if (tdcommafterdoc.InnerHtml.Trim() != "")
            {
                trcommafterdoc.Visible = false;
            }
            if (tdpioneerdoc.InnerHtml.Trim() != "")
            {
                trpioneerdoc.Visible = false;
            }
            if (tdempafterdoc.InnerHtml.Trim() != "")
            {
                trempafterdoc.Visible = false;
            }
            if (tdFFCIafterdoc.InnerHtml.Trim() != "")
            {
                trFFCIafterdoc.Visible = false;
            }
            if (tdDPRafterdoc.InnerHtml.Trim() != "")
            {
                trDPRafterdoc.Visible = false;
            }
            if (tdtermloandoc.InnerHtml.Trim() != "")
            {
                trtermloandoc.Visible = false;
            }

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

                Hid_Auth_Letter_File_Name.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();

                if (Hid_Auth_Letter_File_Name.Value.Trim() != "")
                {
                    Hyp_View_Auth_Letter.Visible = true;
                    LnkBtn_Delete_Auth_Letter.Visible = true;
                    FU_Auth_Letter.Enabled = false;
                    Hyp_View_Auth_Letter.NavigateUrl = string.Format("~/incentives/{0}/{1}", "/Files/InctBasicDoc", Hid_Auth_Letter_File_Name.Value);

                }
                else
                {
                    Hyp_View_Auth_Letter.Visible = false;
                    LnkBtn_Delete_Auth_Letter.Visible = false;
                    FU_Auth_Letter.Enabled = true;
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


                lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();

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

                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();

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
                Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
                Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocBefore"].ToString();
                lbl_FFCI_Date_After.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateAfter"].ToString();
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

            #region "Document Checklist Dynamic bind"


            tdorgdoc.InnerHtml = Lbl_Org_Doc_Type.Text.Trim(); ///////// TD assignments to dynamic document name
            tdunitDoc.InnerHtml = Lbl_Unit_Type_Doc_Name.Text.Trim(); ///////// TD assignments to dynamic document name
            tdcommafterdoc.InnerHtml = Lbl_Prod_Comm_After_Doc_Name.Text.Trim();
            tdpioneerdoc.InnerHtml = Lbl_Pioneer_Doc_Name.Text.Trim();
            tdempafterdoc.InnerHtml = Lbl_Direct_Emp_After_Doc_Name.Text.Trim();
            tdFFCIafterdoc.InnerHtml = Lbl_FFCI_After_Doc_Name.Text.Trim();
            tdDPRafterdoc.InnerHtml = Lbl_Approved_DPR_After_Doc_Name.Text.Trim();
            tdtermloandoc.InnerHtml = Lbl_Term_Loan_Doc_Name.Text.Trim();


            if (dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString().Trim() != "")
            {
                Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
            }
            if (dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString().Trim() != "")
            {

                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            }

            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                if (dtindustryPre.Rows[0]["vchappnoAft"].ToString().Trim() != "")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                if (dtindustryPre.Rows[0]["vchappnoAft"].ToString().Trim() != "")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
            }
            if (dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString().Trim() != "")
            {
                Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString();
            }
            if (dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString().Trim() != "")
            {
                Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
            }
            if (dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString().Trim() != "")
            {
                Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString();
            }
            if (dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString().Trim() != "")
            {
                Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString();
            }
            if (dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString().Trim() != "")
            {
                Hyp_View_Term_Loan_Doc.NavigateUrl = dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();

            }


            if (tdunitDoc.InnerHtml.Trim() != "")
            {
                trunitdoc.Visible = false;
            }
            if (tdorgdoc.InnerHtml.Trim() != "")
            {
                trorgdoc.Visible = false;
            }
            if (tdcommafterdoc.InnerHtml.Trim() != "")
            {
                trcommafterdoc.Visible = false;
            }
            if (tdpioneerdoc.InnerHtml.Trim() != "")
            {
                trpioneerdoc.Visible = false;
            }
            if (tdempafterdoc.InnerHtml.Trim() != "")
            {
                trempafterdoc.Visible = false;
            }
            if (tdFFCIafterdoc.InnerHtml.Trim() != "")
            {
                trFFCIafterdoc.Visible = false;
            }
            if (tdDPRafterdoc.InnerHtml.Trim() != "")
            {
                trDPRafterdoc.Visible = false;
            }
            if (tdtermloandoc.InnerHtml.Trim() != "")
            {
                trtermloandoc.Visible = false;
            }

            #endregion


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


    }

    #endregion
}
