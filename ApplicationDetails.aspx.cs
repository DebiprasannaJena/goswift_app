//******************************************************************************************************************
// File Name             :   ApplicationDetails.aspx.cs
// Description           :   Show the Application Details
// Created by            :   Prasun Kali
// Created on            :   21st August 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAcessLayer.Service;
using System.Web.Services;
using BusinessLogicLayer.Proposal;
using System.IO;
using BusinessLogicLayer.Service;
using EntityLayer.Service;
using EntityLayer.Proposal;

using ProfessionalTax;
using Newtonsoft.Json;
using System.Configuration;
using BPAS;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using TradeLicenceServiceReference;
using System.Web.UI.HtmlControls;
using BusinessLogicLayer.Dashboard;
using RestSharp;
using System.Globalization;

public partial class ApplicationDetails : SessionCheck
{
    #region Variables

    TradeCheckStatusClient obj = new TradeCheckStatusClient();
    TradeLicenseCheckStatusEntity obj1 = new TradeLicenseCheckStatusEntity();

    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    ServiceDetails objServiceEntity = new ServiceDetails();
    ServiceStatus objServcStatus = new ServiceStatus();
    List<ServiceDetails> ServiceDetail = new List<ServiceDetails>();

    int dtval = 0;
    DataTable dtable;
    DataSet ds;
    static string Str_UsrName;
    static string StrInvestorId;
    static int IntGridBindCount = 0;

    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Str_UsrName = Session["UserId"].ToString();
                    StrInvestorId = Session["InvestorId"].ToString();
                    IntGridBindCount = 0;
                    fillDepartment();
                    fillApplicationNo();
                    fillInvestorChildUnit();
                    BindGrid();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Service");
            }
        }
    }

    public void BindGrid()
    {
        try
        {
            /*----------------------------------------------------------*/
            /////// Filter Chain and Indivisual as per Unit Name Selection
            /*----------------------------------------------------------*/
            string strFilterMode = "";
            string strInvestorId = "";
            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                strFilterMode = "I";
                strInvestorId = DrpDwn_Investor_Unit.SelectedValue;
            }
            else
            {
                strFilterMode = "C";
                strInvestorId = Convert.ToString(Session["InvestorId"]);
            }

            /*----------------------------------------------------------*/
            /////// If Department is Selected
            /*----------------------------------------------------------*/
            int intDeptId = 0;
            if (ddlDepartment.SelectedIndex > 0)
            {
                intDeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
            }

            /*----------------------------------------------------------*/
            /////// If Application Number is Selected
            /*----------------------------------------------------------*/
            string strAppNo = "";
            if (ddlApplicationNo.SelectedIndex > 0)
            {
                strAppNo = Convert.ToString(ddlApplicationNo.SelectedValue);
            }

            /*----------------------------------------------------------*/

            objServiceEntity.intCreatedBy = Convert.ToInt32(strInvestorId);
            objServiceEntity.strFilterMode = strFilterMode;
            objServiceEntity.Deptid = intDeptId;
            objServiceEntity.str_ApplicationNo = strAppNo;

            List<ServiceDetails> ServiceDetail = objService.GetAllApplicationDetails(objServiceEntity).ToList();

            gvApplicationDetails.DataSource = ServiceDetail;
            gvApplicationDetails.DataBind();


            if (ServiceDetail.Count > 0)
            {
                icon.Visible = true;
            }
            else
            {
                icon.Visible = false;
            }

            dtval = ServiceDetail.Count;

            DisplayPaging();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objServcStatus = null;
        }
    }

    protected void gvApplicationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblApplctionNo = (Label)e.Row.FindControl("lblApplicationNo");
                HyperLink Lnkdownload = (HyperLink)e.Row.FindControl("btnDetail");
                HyperLink CertificateDownload = (HyperLink)e.Row.FindControl("btnDownload");
                HyperLink btnNoc = (HyperLink)e.Row.FindControl("btnNoc");
                Label lblAppStatus = (Label)e.Row.FindControl("lblappstatsVal");
                Label lblText = (Label)e.Row.FindControl("Label1");
                Label lblpaymentStatus = (Label)e.Row.FindControl("lblpaymentStatus");
                Label lblpaymentAmount = (Label)e.Row.FindControl("lblpaymentAmount");
                HyperLink btnPaymentStatus = (HyperLink)e.Row.FindControl("btnPaymentStatus");
                HyperLink hyprQuery = (HyperLink)e.Row.FindControl("hyprQuery");
                //Added by Priti
                Label lblappstatsVal = (Label)e.Row.FindControl("lblappstatsVal");
                LinkButton btnCertificate = (LinkButton)e.Row.FindControl("btnCertificate");
                Label lblService = (Label)e.Row.FindControl("lblServiceName");
                HiddenField Hid_App_Fee = (HiddenField)e.Row.FindControl("Hid_App_Fee"); //// Added by Sushant Jena On Dt:-15-Apr-2020
                Label lblSubmitedOn = (Label)e.Row.FindControl("lblSubmitedOn"); //// Added by manoj kumar behera for F&B and Labour
                CheckBox ChkBxSelect = (CheckBox)e.Row.FindControl("ChkBxSelect");
                HyperLink LnkServiceApply = (HyperLink)e.Row.FindControl("btnservicelink");

                //Added by Priti

                int INT_Chkstatus = Convert.ToInt32(gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[0]);
                string ulbcode = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string proposalId = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[3].ToString();
                string svcid1 = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[1].ToString();

                //Added by manoj kumar behera

                string Status = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[6].ToString();
                string strCertificateFilename = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[4].ToString();
                string Str_NocFileName = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[5].ToString();
                string str_UlbCode = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string ESIGNSTATUS = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[7].ToString();
                string strProposalId = gvApplicationDetails.DataKeys[e.Row.RowIndex].Values[3].ToString();
                string Value = "";

                if (lblpaymentStatus.Text == "1")
                {
                    ChkBxSelect.Enabled = false;
                }
                else
                {
                    if (svcid1 == "28")
                    {
                        ChkBxSelect.Enabled = false;
                    }
                    else
                    {
                        ChkBxSelect.Enabled = true;
                    }
                        
                }

                if((svcid1 == "25" || svcid1 == "26") && Status == "4") // for reapply
                {
                    LnkServiceApply.NavigateUrl = ConfigurationManager.AppSettings["TreeTransitReapply"].ToString()+ "?appln_id="+lblApplctionNo.Text+ "&service_code="+svcid1+ "&UserId="+ StrInvestorId;
                    LnkServiceApply.Visible = true;
                }

                //Added by manoj kumar behera

                //Service View Details Section

                if (svcid1 == "28")
                {
                    Lnkdownload.NavigateUrl = "viewstatusdetails.aspx?propid=" + proposalId + "&cafNo=" + lblApplctionNo.Text.ToString();
                    Lnkdownload.Visible = true;
                }
                else if (svcid1 == "20")
                {
                    Value = BPAS(lblApplctionNo.Text.ToString(), "5");
                    Lnkdownload.NavigateUrl = ConfigurationManager.AppSettings["BPASRedirectionURL"].ToString() + Value;
                    Lnkdownload.Visible = true;
                }
                else if (svcid1 == "29")
                {
                    EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
                    Value = obj.Encrypt(lblApplctionNo.Text.ToString(), "m8s3e3k5");
                    Lnkdownload.NavigateUrl = "" + ConfigurationManager.AppSettings["GOIPASServiceViewURL"].ToString() + "?GId=" + Value + "";
                    Lnkdownload.Visible = true;
                }
                else if (svcid1 == "62" || svcid1 == "63")
                {
                    Value = FIRE(lblApplctionNo.Text.ToString(), Convert.ToInt32(svcid1));
                    Lnkdownload.NavigateUrl = ConfigurationManager.AppSettings["FIREREDIRECTIONURL"].ToString() + "?Query=" + Value + "";
                    Lnkdownload.Visible = true;
                }
                else if (svcid1 == "67" || svcid1 == "68")
                {
                    Value = EXCISE(lblApplctionNo.Text.ToString(), Convert.ToInt32(svcid1));
                    Lnkdownload.NavigateUrl = Value;
                    Lnkdownload.Visible = true;
                }
                else if (svcid1 == "69")
                {
                    Value = OSBC(lblApplctionNo.Text.ToString());
                    Lnkdownload.NavigateUrl = Value;
                    Lnkdownload.Visible = true;
                }
                else if (svcid1 == "73")
                {
                    Value = EIT(lblApplctionNo.Text.ToString(), Convert.ToInt32(svcid1));
                    Lnkdownload.NavigateUrl = Value;
                    Lnkdownload.Visible = true;
                }
                else
                {
                    Lnkdownload.NavigateUrl = objServcStatus.GetUrl(lblApplctionNo.Text, INT_Chkstatus, svcid1, proposalId, StrInvestorId, lblSubmitedOn.Text);
                    Lnkdownload.Visible = true;
                }

                //Service View Details Section

                //Service Approval Document Link Section

                if (INT_Chkstatus != 1)
                {
                    if (strCertificateFilename != "")
                    {
                        CertificateDownload.NavigateUrl = "Portal/ApprovalDocs/" + strCertificateFilename;
                        CertificateDownload.Visible = true;
                    }
                }
                else
                {
                    if (Status == "2" && svcid1 != "5" && svcid1 != "6" && svcid1 != "7" && svcid1 != "34" && svcid1 != "35" && svcid1 != "36" && svcid1 != "39" && svcid1 != "40" && svcid1 != "70" && svcid1 != "71" && svcid1 != "72" && svcid1 != "20" && svcid1 != "29" && svcid1 != "62" && svcid1 != "63" && svcid1 != "67" && svcid1 != "68" && svcid1 != "69" && svcid1 != "73" && svcid1 != "37") // add by anil service 37
                    {
                        CertificateDownload.NavigateUrl = "~/DownloadCertificate.aspx?ServiceType=" + INT_Chkstatus + "&ApplicationNo=" + lblApplctionNo.Text + "&ServiceId=" + svcid1 + "&ProposalId=" + proposalId;
                        CertificateDownload.Visible = true;
                    }
                    else if (Str_NocFileName != "")
                    {
                        btnNoc.NavigateUrl = Str_NocFileName;
                        btnNoc.Visible = true;
                    }
                    else if (svcid1 == "20" && Status == "2")
                    {
                        CertificateDownload.NavigateUrl = strCertificateFilename;
                        CertificateDownload.Visible = true;
                    }
                    else if (svcid1 == "29" && Status == "2")
                    {
                        CertificateDownload.NavigateUrl = strCertificateFilename;
                        CertificateDownload.Visible = true;
                    }
                    else if ((svcid1 == "62" || svcid1 == "63") && Status == "2")
                    {
                        CertificateDownload.NavigateUrl = strCertificateFilename;
                        CertificateDownload.Visible = true;
                    }
                    else if (svcid1 == "67" || svcid1 == "68")
                    {
                        CertificateDownload.Visible = false;
                    }
                    else if (svcid1 == "69" && Status == "2")
                    {
                        CertificateDownload.NavigateUrl = strCertificateFilename;
                        CertificateDownload.Visible = true;
                    }
                    else if ((svcid1 == "5" || svcid1 == "6" || svcid1 == "7" || svcid1 == "34" || svcid1 == "35" || svcid1 == "36" || svcid1 == "39" || svcid1 == "40" || svcid1 == "70" || svcid1 == "71" || svcid1 == "72" || svcid1 == "37") && (Status == "2" || Status == "3")) //F&B and Labour Service add by anil service number 37
                    {
                        CultureInfo culture = new CultureInfo("es-ES");
                        if (DateTime.Parse(lblSubmitedOn.Text, culture).Date > DateTime.Parse("15/01/2021", culture).Date)
                        {
                            CertificateDownload.NavigateUrl = strCertificateFilename;
                            CertificateDownload.Visible = true;
                        }
                        else
                        {
                            CertificateDownload.NavigateUrl = "Portal/ApprovalDocs/" + strCertificateFilename;
                            CertificateDownload.Visible = true;
                        }
                    }
                    else if (svcid1 == "73" && Status == "2")
                    {
                        CertificateDownload.NavigateUrl = strCertificateFilename;
                        CertificateDownload.Visible = true;
                    }
                }

                //Service Approval Document Link Section

                //Quer Section Of Service

                int INT_Qrystatus = Convert.ToInt32(str_UlbCode);
                if (INT_Qrystatus == 5 || INT_Qrystatus == 6)
                {
                    if (svcid1 == "28")
                    {
                        hyprQuery.NavigateUrl = objServcStatus.QueryUrlCNET(proposalId, INT_Chkstatus, svcid1);
                        hyprQuery.Visible = true;
                    }
                    else if (svcid1 == "20")
                    {
                        hyprQuery.Visible = false;
                    }
                    else if (svcid1 == "62" || svcid1 == "63")
                    {
                        Value = FIRE(lblApplctionNo.Text.ToString(), Convert.ToInt32(svcid1));
                        hyprQuery.NavigateUrl = ConfigurationManager.AppSettings["FIREREDIRECTIONURL"].ToString() + "?Query=" + Value + "";
                        hyprQuery.Visible = true;
                    }
                    else if (svcid1 == "67" || svcid1 == "68")
                    {
                        hyprQuery.Visible = false;
                    }
                    else if (svcid1 == "69")
                    {
                        hyprQuery.Visible = false;
                    }
                    else if (svcid1 == "73")
                    {
                        Value = EIT(lblApplctionNo.Text.ToString(), Convert.ToInt32(svcid1));
                        hyprQuery.NavigateUrl = Value;
                        hyprQuery.Visible = true;
                    }
                    else
                    {
                        hyprQuery.NavigateUrl = objServcStatus.QueryUrl(lblApplctionNo.Text, INT_Chkstatus, svcid1, StrInvestorId, lblSubmitedOn.Text);
                        hyprQuery.Visible = true;
                    }
                }
                else
                {

                }

                //Quer Section Of Service

                //-------------------------ESIGN SECTION --------------------------//

                int intenableEsign = 0;
                string strSQLQueryEsign = "SELECT Esign FROM  tbl_Config";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(strSQLQueryEsign, con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        intenableEsign = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }
                //--------------------------ESIGN SECTION --------------------------//

                //--------------------------PAYMENT SECTION --------------------------//

                if (intenableEsign == 1)
                {
                    if (ESIGNSTATUS == "1")
                    {
                        btnPaymentStatus.NavigateUrl = "ServicePayment.aspx?ApplicationKey=" + lblApplctionNo.Text.ToString() + "&Amount=" + lblpaymentAmount.Text.ToString() + "&AccountHd=yes&ServiceID=" + svcid1 + "&T=2";
                    }
                    else
                    {
                        btnPaymentStatus.NavigateUrl = "ServiceFileCheck.aspx?ApplicationKey=" + lblApplctionNo.Text.ToString() + "&Amount=" + lblpaymentAmount.Text.ToString() + "&AccountHd=yes&ServiceID=" + svcid1 + "&T=2";
                    }
                }
                else
                {
                    btnPaymentStatus.NavigateUrl = "ServicePayment.aspx?ApplicationKey=" + lblApplctionNo.Text.ToString() + "&Amount=" + lblpaymentAmount.Text.ToString() + "&AccountHd=yes&ServiceID=" + svcid1 + "&T=2" + "&AppFee=" + Convert.ToString(Hid_App_Fee.Value) + "";
                }

                if (Convert.ToInt32(lblpaymentStatus.Text) == 0 && Convert.ToDecimal(lblpaymentAmount.Text) > Convert.ToDecimal(0.0))
                {
                    if (intenableEsign == 1)
                    {
                        if (ESIGNSTATUS == "1")
                        {
                            btnPaymentStatus.Visible = true;
                            btnPaymentStatus.Text = "Pay Now";
                        }
                        else
                        {
                            btnPaymentStatus.Visible = true;
                            btnPaymentStatus.Text = "eSign";
                        }
                    }
                    else
                    {
                        btnPaymentStatus.Visible = true;
                        btnPaymentStatus.Text = "Pay Now";
                        //Added BY Priti
                    }

                    if (Status != "9")
                    {
                        lblappstatsVal.Text = "Not Paid";
                    }
                    if (Status == "11")
                    {
                        lblappstatsVal.Text = "Field Inspection Completed";
                    }
                    if (Status == "2")
                    {
                        lblappstatsVal.Text = "First level Approved";
                    }

                    //Added BY Priti
                    if (INT_Chkstatus == 1)
                    {
                        if (svcid1 == "28")
                        {
                            string appPayUrl = ConfigurationManager.AppSettings["CNETPaymenturl"].ToString();
                            string demandSts = objServcStatus.CheckDemandStatus(lblApplctionNo.Text.ToString());
                            if (demandSts == "2")
                            {
                                btnPaymentStatus.NavigateUrl = "" + appPayUrl + "?proposal_no=" + strProposalId + "&demand_type=2";
                            }
                            else
                            {
                                btnPaymentStatus.NavigateUrl = "" + appPayUrl + "?proposal_no=" + strProposalId + "&demand_type=1";
                            }
                            btnPaymentStatus.Visible = true;
                            btnPaymentStatus.Text = "Pay Now (Processing Fee)";
                            //Added BY Priti
                            lblappstatsVal.Text = "Not Paid";
                        }
                        //Added By Manoj Kumar Behera  
                        else if (svcid1 == "20")
                        {
                            DataTable dt = new DataTable();
                            dt = objServcStatus.CheckPaymentType(lblApplctionNo.Text.ToString());
                            if (Convert.ToInt32(dt.Rows[0]["NUM_PAYMENT_AMOUNT"]) >= 0 && Convert.ToInt32(dt.Rows[0]["num_Demand_Amount"]) <= 0)
                            {
                                string applicationno = BPAS(lblApplctionNo.Text.ToString(), "3");
                                btnPaymentStatus.NavigateUrl = ConfigurationManager.AppSettings["BPASScrutinyPaymentURL"].ToString() + applicationno;
                                btnPaymentStatus.Text = "Pay Now (Scrutiny Fee)";
                            }
                            btnPaymentStatus.Visible = true;
                            lblappstatsVal.Text = "Not Paid";
                        }
                        else if (svcid1 == "5" || svcid1 == "6" || svcid1 == "7" || svcid1 == "34" || svcid1 == "35" || svcid1 == "36" || svcid1 == "39" || svcid1 == "40" || svcid1 == "70" || svcid1 == "71" || svcid1 == "72" || svcid1 == "37") //F&B and Labour Service add by anil service number 37
                        {
                            CultureInfo culture = new CultureInfo("es-ES");
                            if (DateTime.Parse(lblSubmitedOn.Text, culture).Date > DateTime.Parse("15/01/2021", culture).Date)
                            {
                                btnCertificate.Visible = true;
                                btnPaymentStatus.Visible = false;
                                lblpaymentAmount.Visible = false;
                            }
                            else
                            {
                                btnPaymentStatus.Visible = true;
                            }
                        }
                        else
                        {
                            btnCertificate.Visible = true;
                            btnPaymentStatus.Visible = false;
                            lblpaymentAmount.Visible = false;
                        }
                    }
                }
                else if (Convert.ToInt32(lblpaymentStatus.Text) == 1)
                {
                    lblpaymentAmount.Visible = true;
                    lblpaymentAmount.Text = "Paid";
                }
                else if (Convert.ToInt32(lblpaymentStatus.Text) == 0 && Convert.ToDecimal(lblpaymentAmount.Text) == Convert.ToDecimal(0.0))
                {

                }
                if (svcid1 == "29")
                {
                    string PaymntUrl = objServcStatus.GetPaymntURlForWater(lblApplctionNo.Text);
                    if (PaymntUrl != "")
                    {
                        btnPaymentStatus.NavigateUrl = PaymntUrl;
                        btnPaymentStatus.Visible = true;
                    }
                }
                else if (svcid1 == "20")
                {
                    if (Status == "9")
                    {
                        string applicationno = BPAS(lblApplctionNo.Text.ToString(), "4");
                        btnPaymentStatus.NavigateUrl = ConfigurationManager.AppSettings["BPASScrutinyPaymentURL"].ToString() + applicationno;
                        btnPaymentStatus.Text = "Pay Now (Demanded Fee)";
                        btnPaymentStatus.Visible = true;
                        lblpaymentAmount.Visible = false;
                    }
                }

                //--------------------------PAYMENT SECTION --------------------------//

                #region TransactionDetail

                //This region of code is to show transactionDetail//
                HtmlGenericControl OrderList = (HtmlGenericControl)e.Row.FindControl("OrderList");
                HtmlGenericControl OrderList1 = (HtmlGenericControl)e.Row.FindControl("OrderList1");
                List<ServiceDetails> objOrderList = new List<ServiceDetails>();
                ServiceDetails objProp1 = new ServiceDetails();
                objProp1.STRACTION = "D";
                objProp1.strApplicationUnqKey = lblApplctionNo.Text;
                //PaymentOrderDetails(ServiceDetails objService)
                ServiceBusinessLayer objService1 = new ServiceBusinessLayer();
                objOrderList = objService1.PaymentOrderDetails(objProp1).ToList();
                if (objOrderList.Count > 0)
                {

                    DataTable dt = CommonHelperCls.ConvertToDataTable<ServiceDetails>(objOrderList);
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "strStatus = 'Success'";

                    string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th> Date</th><th>Order No.</th><th>Amount</th></tr>";
                    Decimal TotalAmt = 0;
                    for (int i = 0; i < dv.ToTable().Rows.Count; i++)
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + dv.ToTable().Rows[i]["dtmCreatedOn"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchOrderNo"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchAmount"].ToString() + "</td></tr>";
                        TotalAmt = TotalAmt + Convert.ToDecimal(dv.ToTable().Rows[i]["vchAmount"]);
                    }
                    strHTMlQuery = strHTMlQuery + "<tr><td></td><td>Total</td><td>" + TotalAmt + "</td></tr></table>";
                    OrderList.InnerHtml = strHTMlQuery;

                    dv = new DataView(dt);
                    dv.RowFilter = "strStatus = 'Pending'";
                    TotalAmt = 0;
                    strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th> Date</th><th>Order No.</th><th>Amount</th></tr>";
                    for (int i = 0; i < dv.ToTable().Rows.Count; i++)
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + dv.ToTable().Rows[i]["dtmCreatedOn"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchOrderNo"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchAmount"].ToString() + "</td></tr>";
                        TotalAmt = TotalAmt + Convert.ToDecimal(dv.ToTable().Rows[i]["vchAmount"]);
                    }
                    strHTMlQuery = strHTMlQuery + "<tr><td></td><td>Total</td><td>" + TotalAmt + "</td></tr></table>";
                    OrderList1.InnerHtml = strHTMlQuery;
                    // QueryHist1.InnerHtml = strHTMlQuery;
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void gvApplicationDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvApplicationDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void gvApplicationDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            Label lblApplctionNo = (Label)gvApplicationDetails.Rows[rowindex].FindControl("lblApplicationNo");
            LinkButton Lnkdownload = (LinkButton)gvApplicationDetails.Rows[rowindex].FindControl("btnCertificate");
            HyperLink CertificateDownload = (HyperLink)gvApplicationDetails.Rows[rowindex].FindControl("btnDownload");
            LinkButton lbtn = (LinkButton)gvApplicationDetails.Rows[rowindex].FindControl("lnkQuery");
            Label lblAppStatus = (Label)gvApplicationDetails.Rows[rowindex].FindControl("lblappstatsVal");
            Label lblText = (Label)gvApplicationDetails.Rows[rowindex].FindControl("Label1");
            Label lblUpdatedOn = (Label)gvApplicationDetails.Rows[rowindex].FindControl("Label4");

            int INT_Chkstatus = Convert.ToInt32(gvApplicationDetails.DataKeys[rowindex].Values[0]);

            string proposalId = gvApplicationDetails.DataKeys[rowindex].Values[3].ToString();
            string ServiceId = gvApplicationDetails.DataKeys[rowindex].Values[1].ToString();
            if (e.CommandName == "cmdcheckStatus")
            {
                if (INT_Chkstatus == 1)
                {
                    #region ForTradeLicence
                    if (ServiceId == "52")
                    {
                        TradeInput ti = new TradeInput();

                        ti.applicationNo = lblApplctionNo.Text;
                        ti.UlbCode = objService.GetULBCode(lblApplctionNo.Text);
                        obj1 = obj.SearchStatus(ti);

                        string[] msg;
                        if (obj1.applicationstatusId == "2")
                        {

                            CertificateDownload.NavigateUrl = "~/DownloadCertificate.aspx?ServiceType=" + INT_Chkstatus + "&ApplicationNo=" + lblApplctionNo.Text + "&ServiceId=" + ServiceId;
                            CertificateDownload.Visible = true;
                            msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, obj1.applicationstatus, Convert.ToInt32(obj1.applicationstatusId), "~/DownloadCertificate.aspx?ServiceType=" + INT_Chkstatus + "&ApplicationNo=" + lblApplctionNo.Text + "&ServiceId=" + ServiceId, "EXT");
                        }
                        else
                        {
                            msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, obj1.applicationstatus, Convert.ToInt32(obj1.applicationstatusId), "", "EXT");
                        }

                        lblAppStatus.Text = msg[0];
                        lblUpdatedOn.Text = msg[1];

                        lblUpdatedOn.Visible = true;
                        lblAppStatus.Visible = true;
                    }

                    #endregion

                    #region ForPartnershipFirm

                    else if (ServiceId == "49")
                    {
                        PartnershipFirm.Service1Client obj = new PartnershipFirm.Service1Client();
                        PartnershipFirm.FirmDtl objfirm = new PartnershipFirm.FirmDtl();
                        //objfirm = obj.GetStatus(ApplicationID,ProposalID);
                        objfirm = obj.GetStatus(lblApplctionNo.Text, proposalId);
                        string StatusID = objfirm.StatusId.ToString();
                        string StatusName = objfirm.StatusName.ToString();
                        IsDirectoryCreated("PF");
                        string[] msg;
                        if (StatusID == "2")
                        {
                            string appViewDetailUrl = ConfigurationManager.AppSettings["PartnershipFirmDetail"].ToString();

                            string Url = appViewDetailUrl + "?UserId=" + StrInvestorId + "&ApplicationID=" + lblApplctionNo.Text;
                            msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, StatusName, Convert.ToInt32(StatusID), Url, "EXT");
                            CertificateDownload.NavigateUrl = Url;
                            CertificateDownload.Visible = true;
                        }
                        else
                        {
                            msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, StatusName, Convert.ToInt32(StatusID), "", "EXT");
                        }
                        lblAppStatus.Text = msg[0];
                        lblUpdatedOn.Text = msg[1];
                        lblUpdatedOn.Visible = true;
                        lblAppStatus.Visible = true;

                    }
                    #endregion

                    #region WaterAllotment

                    else if (ServiceId == "29")
                    {
                        string inputJson = (new JavaScriptSerializer()).Serialize(lblApplctionNo.Text);
                        inputJson = inputJson.TrimStart('[').TrimEnd(']');
                        string serviceUrl = "http://erp.idco.in/" + "sendStatusOfWaterApplFromERPtoSWP?wa_ref_no=" + lblApplctionNo.Text;
                        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
                        httpRequest.Accept = "application/json";
                        httpRequest.ContentType = "application/json";
                        httpRequest.Method = "GET";
                        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                        {
                            using (Stream stream = httpResponse.GetResponseStream())
                            {
                                string strResult = (new StreamReader(stream)).ReadToEnd();
                                string Status = JObject.Parse(strResult)["status_code"].ToString();
                                string RefNo = JObject.Parse(strResult)["refno"].ToString();
                                string StatusMessage = JObject.Parse(strResult)["status_message"].ToString();

                                string[] msg = objServcStatus.UpdateStatus(RefNo, Convert.ToInt32(ServiceId), proposalId, StatusMessage, Convert.ToInt32(Status), "", "EXT");
                                lblAppStatus.Text = msg[0];
                                lblUpdatedOn.Text = msg[1];
                                lblUpdatedOn.Visible = true;
                                lblAppStatus.Visible = true;
                            }
                        }

                    }
                    #endregion

                    #region ForProfessionalTax
                    else if (ServiceId == "10")
                    {
                        WebServiceSoapClient obj = new WebServiceSoapClient();
                        string str = obj.PTRegistrationStatus(ConfigurationManager.AppSettings["PTKEY"].ToString(), lblApplctionNo.Text, "");

                        List<CPT> list = JsonConvert.DeserializeObject<List<CPT>>(str);
                        string[] msg = objServcStatus.UpdateStatus(list[0].TokenNo.ToString(), Convert.ToInt32(ServiceId), proposalId, list[0].PT_Status.ToString(), Convert.ToInt32(list[0].Status.ToString()), "", "EXT");
                        lblAppStatus.Text = msg[0];
                        lblUpdatedOn.Text = msg[1];
                        lblUpdatedOn.Visible = true;
                        lblAppStatus.Visible = true;
                    }
                    #endregion

                    #region ForLandAllotment
                    else if (ServiceId == "28")
                    {

                        string[] returnval = GetIntegrationStatusPEALWithIDCO(lblApplctionNo.Text);
                        string[] msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, Convert.ToString(returnval[2].ToString()), Convert.ToInt32(returnval[5].ToString()), Convert.ToString(returnval[4].ToString()), "EXT");
                    }
                    #endregion

                    #region ForBuildingPlanApproval
                    else if (ServiceId == "20")
                    {
                        string RestServiceURL = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                        // string RedirectionURL = "http://164.100.141.98/BDALogin";
                        try
                        {
                            using (var client = new WebClient()) //WebClient  
                            {
                                //Variable given by User
                                string _uniquecode = lblApplctionNo.Text; string _SWPCode = Session["InvestorId"].ToString();


                                string _msg = string.Empty;
                                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                                client.Headers.Add("Accept:application/json");
                                var result = client.DownloadString(RestServiceURL + "/FetchApplicationsByUniqueCode?_uniquecode=" + _uniquecode + "&_SWPCode=" + _SWPCode + "");
                                string reslt = result;
                                JavaScriptSerializer serializer = new JavaScriptSerializer();

                                object a = serializer.Deserialize(result, typeof(object));
                                string str = a.ToString();
                                if (str == "No data found.")
                                {
                                    lblAppStatus.Text = "No data found.";

                                    string[] msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, "No data found.", 0, "", "EXT");
                                    lblAppStatus.Text = msg[0];
                                    lblUpdatedOn.Text = msg[1];
                                    lblUpdatedOn.Visible = true;
                                    lblAppStatus.Visible = true;
                                }
                                else
                                {
                                    DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
                                    if (Convert.ToInt32(DynTable.Rows[0]["applicationstatusid"].ToString()) == 2)
                                    {


                                        using (var client1 = new WebClient()) //WebClient  
                                        {
                                            var result1 = client.DownloadString(RestServiceURL + "/FetchCertificate?uniquecode=" + _uniquecode + "&swpusercode=" + _SWPCode + "&_msg=a");

                                            if (result1 != "null")
                                            {

                                                var response = result1
                                                .Substring(1, result1.Length - 2)
                                                .Replace(@"\/", "/");
                                                byte[] d = Convert.FromBase64String(response);


                                                string filename1 = string.Empty;
                                                string prefix = "BPAS";
                                                string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                                                filename1 = string.Format("SWP_" + prefix + "_{0:yyyy_MM_dd_hh_mm_ss_tt_}" + ".pdf", DateTime.Now);
                                                IsDirectoryCreated(prefix);
                                                System.IO.File.WriteAllBytes(apppath + "/File/" + prefix + "/" + filename1, d);
                                                string[] msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, Convert.ToString(DynTable.Rows[0]["applicationstatus"].ToString()), Convert.ToInt32(DynTable.Rows[0]["applicationstatusid"].ToString()), "~/File/" + prefix + "/" + filename1, "EXT");
                                                lblAppStatus.Text = msg[0];
                                                lblUpdatedOn.Text = msg[1];
                                                lblUpdatedOn.Visible = true;
                                                lblAppStatus.Visible = true;
                                                CertificateDownload.NavigateUrl = "~/File/" + prefix + "/" + filename1;
                                                CertificateDownload.Visible = true;

                                            }
                                            else
                                            {

                                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('File not exists at physical location!!!Please try after Sometime')", true);


                                            }

                                        }

                                    }
                                    else
                                    {
                                        string[] msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, Convert.ToString(DynTable.Rows[0]["applicationstatus"].ToString()), Convert.ToInt32(DynTable.Rows[0]["applicationstatusid"].ToString()), "", "EXT");
                                        lblAppStatus.Text = msg[0];
                                        lblUpdatedOn.Text = msg[1];
                                        lblUpdatedOn.Visible = true;
                                        lblAppStatus.Visible = true;
                                    }

                                }


                            }
                        }
                        catch (Exception)
                        {


                        }


                        //BPASConnectServiceClient obj = new BPASConnectServiceClient();
                        //string retResult = obj.FetchApplications(lblApplctionNo.Text);
                        //StringReader theReader = new StringReader(retResult);
                        //DataSet ds = new DataSet();
                        //ds.ReadXml(theReader);
                        //if (ds.Tables[0].Rows[0]["ReturnMsg"].ToString() != "No application found for the user!")
                        //{
                        //    int intstatus = Convert.ToInt16(ds.Tables[0].Rows[0]["Statusid"].ToString());
                        //    string statusName = ds.Tables[0].Rows[0]["StatusName"].ToString();
                        //    string []msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, statusName, intstatus, "","EXT");
                        //}
                        //else
                        //{
                        //    lblAppStatus.Visible = true;
                        //    lblAppStatus.Text = ds.Tables[0].Rows[0]["ReturnMsg"].ToString();
                        //}
                    }
                    #endregion

                    #region ForTreeTransit
                    else if (ServiceId == "25" || ServiceId == "26")
                    {
                        // string Url = "117.247.252.221/ttpermit_beta/users/getStatus";
                        // string Url = " https://ttpermitodisha.in/users/getStatus";
                        string Url = ConfigurationManager.AppSettings["TreecheckStatus"].ToString();

                        string[] returnval = TreeTransit(ServiceId, lblApplctionNo.Text, "TT", Url);
                        string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                        string[] msg = new string[2];
                        if (returnval[1].ToString() == "2")
                        {
                            msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, returnval[0].ToString(), Convert.ToInt32(returnval[1].ToString()), "~/File/TT/" + returnval[2].ToString(), "EXT");
                            CertificateDownload.NavigateUrl = "~/File/TT/" + returnval[2].ToString();
                            CertificateDownload.Visible = true;
                        }
                        else if (returnval[1].ToString() == "4" && returnval[2].ToString() != "")
                        {
                            msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, returnval[0].ToString(), Convert.ToInt32(returnval[1].ToString()), "~/File/TT/" + returnval[2].ToString(), "NOC");
                            CertificateDownload.NavigateUrl = "~/File/TT/" + returnval[2].ToString();
                            CertificateDownload.Visible = true;
                        }
                        else
                        {
                            msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, returnval[0].ToString(), Convert.ToInt32(returnval[1].ToString()), "", "EXT");
                        }

                        lblAppStatus.Text = msg[0];
                        lblUpdatedOn.Text = msg[1];
                        lblUpdatedOn.Visible = true;
                        lblAppStatus.Visible = true;
                    }
                    #endregion

                    #region ForHealthAndFamily
                    else if (ServiceId == "30" || ServiceId == "31" || ServiceId == "32")
                    {
                        // string url = "117.247.252.220:8484/dcodisha/dcstatus.php";
                        // string url = "http://dcodishaonline.nic.in/dcodisha/dcstatus.php";
                        string url = ConfigurationManager.AppSettings["DrugcheckStatus"].ToString();
                        string[] returnval = Health(ServiceId, lblApplctionNo.Text, url);
                        string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                        if (returnval[2] == null)
                        {
                            returnval[2] = "";
                        }

                        string[] msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, returnval[0].ToString(), Convert.ToInt32(returnval[1].ToString()), returnval[2].ToString(), "EXT");
                        lblAppStatus.Text = msg[0];
                        lblUpdatedOn.Text = msg[1];
                        lblUpdatedOn.Visible = true;
                        lblAppStatus.Visible = true;

                    }
                    #endregion

                    #region ForPollutionControlBoard
                    else if (ServiceId == "43" || ServiceId == "44" || ServiceId == "45" || ServiceId == "46" || ServiceId == "50" || ServiceId == "53" || ServiceId == "54")
                    {
                        string url = ConfigurationManager.AppSettings["PollutioncheckStatus"].ToString();

                        XmlDocument doc1 = new XmlDocument();
                        //doc1.Load(" http://164.100.163.18/ODWS/checkStatus?serviceId=" + ServiceId + "&applicationNo=" + lblApplctionNo.Text + " ");
                        //doc1.Load(" http://odocmms.nic.in/ODWS/checkStatus?serviceId=" + ServiceId + "&applicationNo=" + lblApplctionNo.Text + " ");
                        doc1.Load("" + url + "?serviceId=" + ServiceId + "&applicationNo=" + lblApplctionNo.Text + " ");
                        XmlElement root = doc1.DocumentElement;
                        XmlNodeList nodes = root.SelectNodes("/applicationDetail");
                        string filename1 = "";
                        string applicationID = "";
                        string serviceId = "";
                        string statusMsg = "";
                        string statusId = "";
                        string certifictUrl = "";
                        string[] msg = new string[2];
                        string prefix = string.Empty;
                        foreach (XmlNode node in nodes)
                        {
                            applicationID = node["applicationID"].InnerText;
                            serviceId = node["serviceId"].InnerText;
                            statusMsg = node["statusMsg"].InnerText;
                            statusId = node["status"].InnerText;
                            certifictUrl = node["certificateURL"].InnerText;

                            if (certifictUrl != "")
                            {

                                if (ServiceId == "43")
                                {
                                    prefix = "CEW";
                                }
                                else if (ServiceId == "44")
                                {
                                    prefix = "CEA";
                                }
                                else if (ServiceId == "45")
                                {
                                    prefix = "COW";
                                }
                                else if (ServiceId == "46")
                                {
                                    prefix = "COA";
                                }
                                else if (ServiceId == "53")
                                {
                                    prefix = "CEB";
                                }
                                else if (ServiceId == "54")
                                {
                                    prefix = "COB";
                                }
                                else if (ServiceId == "50")
                                {
                                    prefix = "HWM";
                                }
                                WebClient client = new WebClient();
                                byte[] data = client.DownloadData(certifictUrl);
                                string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                                filename1 = string.Format("SWP_" + prefix + "_{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + applicationID + ".pdf", DateTime.Now);
                                IsDirectoryCreated(prefix);

                                System.IO.File.WriteAllBytes(apppath + "/File/" + prefix + "/" + filename1, data);
                            }
                            if (certifictUrl != "")
                            {
                                msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, statusMsg, Convert.ToInt32(statusId), "~/File/" + prefix + "/" + filename1, "EXT");
                                CertificateDownload.NavigateUrl = "~/File/" + prefix + "/" + filename1;

                            }
                            else
                            {
                                msg = objServcStatus.UpdateStatus(lblApplctionNo.Text, Convert.ToInt32(ServiceId), proposalId, statusMsg, Convert.ToInt32(statusId), "", "EXT");

                            }
                            lblAppStatus.Text = msg[0];
                            lblUpdatedOn.Text = msg[1];
                            lblUpdatedOn.Visible = true;
                            lblAppStatus.Visible = true;

                        }

                    }
                    #endregion
                }
                else if (INT_Chkstatus == 0)
                {
                    string msg = objServcStatus.UpdateStatusNew(lblApplctionNo.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    private string[] GetIntegrationStatusPEALWithIDCO(string oas_cafno)
    {
        string[] retunval;
        NameValueCollection nvc = null;
        //string serviceUrl = "http://erp.idco.in/" + "sendStatusOfApplicationFromERPtoSWP?oas_cafno=" + oas_cafno;

        string json = "{" + "\"oas_cafno\" : " + "\"" + oas_cafno + "\""
                                             + "}";
        string serviceUrl = "http://erp.idco.in/rest/sendStatusOfApplicationFromERPtoSWP";
        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
        httpRequest.Accept = "application/json";
        httpRequest.ContentType = "application/json";
        httpRequest.Method = "POST";
        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        {
            //initiate the request
            var resToWrite = json;
            streamWriter.Write(resToWrite);
            streamWriter.Flush();
            streamWriter.Close();
        }
        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        {
            using (Stream stream = httpResponse.GetResponseStream())
            {
                string strResult = (new StreamReader(stream)).ReadToEnd();
                var jss = new JavaScriptSerializer();
                var dict = jss.Deserialize<Dictionary<string, string>>(strResult);

                if (dict != null)
                {
                    nvc = new NameValueCollection(dict.Count);
                    foreach (var k in dict)
                    {
                        nvc.Add(k.Key, k.Value);
                    }
                }
                retunval = new string[] { nvc["unique_application_id_from_swp"], nvc["cafno"], nvc["status_message"], nvc["industry_code"], nvc["allotment_order_url"], nvc["status_code"] };
            }
        }
        return retunval;
    }
    private string[] TreeTransit(string ServiceId, string ApplicationNo, string prefix, string Url)
    {
        string filename1 = string.Empty;
        string[] retunval;
        using (WebClient client = new WebClient())
        {
            var reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("ServiceID", ServiceId);
            reqparm.Add("ApplicationNumber", ApplicationNo);
            byte[] responsebytes = client.UploadValues(Url, "POST", reqparm);
            string responsebody = Encoding.UTF8.GetString(responsebytes);

            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, string>>(responsebody);
            NameValueCollection nvc = null;
            if (dict != null)
            {
                nvc = new NameValueCollection(dict.Count);
                foreach (var k in dict)
                {
                    nvc.Add(k.Key, k.Value);
                }
            }
            if (nvc["CertificateURL"] != "")
            {
                byte[] data = client.DownloadData(nvc["CertificateURL"]);
                string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                filename1 = string.Format("SWP_" + prefix + "_{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + ApplicationNo + ".pdf", DateTime.Now);
                IsDirectoryCreated(prefix);
                System.IO.File.WriteAllBytes(apppath + "/File/" + prefix + "/" + filename1, data);
            }
            retunval = new string[] { nvc["Statusmsg"], nvc["Status"], filename1 };

        }
        return retunval;
    }
    private string[] Health(string ServiceId, string ApplicationNo, string Url)
    {
        string filename1 = string.Empty;
        string[] retunval;
        using (WebClient client = new WebClient())
        {
            var reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("ServiceID", ServiceId);
            reqparm.Add("ApplicationNumber", ApplicationNo);
            reqparm.Add("app_type", "New");
            byte[] responsebytes = client.UploadValues(Url, "POST", reqparm);
            string responsebody = Encoding.UTF8.GetString(responsebytes);

            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, string>>(responsebody);
            NameValueCollection nvc = null;
            if (dict != null)
            {
                nvc = new NameValueCollection(dict.Count);
                foreach (var k in dict)
                {
                    nvc.Add(k.Key, k.Value);
                }
            }

            retunval = new string[] { nvc["Statusmsg"], nvc["Status"], nvc["CertificateURL"] };

        }
        return retunval;
    }
    public void IsDirectoryCreated(string prefix)
    {
        string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        if (!Directory.Exists(apppath + "/File/" + prefix))
        {
            Directory.CreateDirectory(apppath + "/File/" + prefix);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProposal = new ProposalDet();

        try
        {
            objProposal.strAction = "A";
            objProposal.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProposal.strProposalNo = hdnProposalno.Value;
            objProposal.intStatus = 6;
            objProposal.intQueryId = Convert.ToInt32(hdnservice.Value);

            objProposal.strRemarks = txtA1.Text;

            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + hdnProposalno.Value + "_Query1" + ".pdf", DateTime.Now);

            if (FileUpload1.HasFile)
            {

                if (Path.GetExtension(FileUpload1.FileName) != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                    return;
                }
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Portal/QueryFiles/Services/"));
            if (!string.IsNullOrEmpty(FileUpload1.FileName))
            {
                if (dir.Exists)
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Portal/QueryFiles/Services/" + filepath));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Portal/QueryFiles/Services/"));
                    FileUpload1.SaveAs(Server.MapPath("~/Portal/QueryFiles/Services/" + filepath));

                }
            }
            else { filepath = ""; }

            string filepath1 = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + hdnProposalno.Value + "_Query2" + ".pdf", DateTime.Now);

            if (FileUpload2.HasFile)
            {

                if (Path.GetExtension(FileUpload2.FileName) != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(FileUpload2.FileName))
            {
                if (dir.Exists)
                {
                    FileUpload2.SaveAs(Server.MapPath("~/Portal/QueryFiles/Services/" + filepath1));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Portal/QueryFiles/Services/"));
                    FileUpload2.SaveAs(Server.MapPath("~/Portal/QueryFiles/Services/" + filepath1));

                }
            }
            else { filepath1 = ""; }

            string filepath2 = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + hdnProposalno.Value + "_Query3" + ".pdf", DateTime.Now);

            if (FileUpload3.HasFile)
            {

                if (Path.GetExtension(FileUpload3.FileName) != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(FileUpload3.FileName))
            {
                if (dir.Exists)
                {
                    FileUpload3.SaveAs(Server.MapPath("~/Portal/QueryFiles/Services/" + filepath2));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Portal/QueryFiles/Services/"));
                    FileUpload3.SaveAs(Server.MapPath("~/Portal/QueryFiles/Services/" + filepath2));

                }
            }
            else { filepath2 = ""; }

            objProposal.strFileName = filepath + "," + filepath1 + "," + filepath2 + ",";

            string strRetVal = objService.ServiceProposalRaiseQuery(objProposal);
            BindGrid();
            if (strRetVal == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objProposal = null;
        }
    }

    [WebMethod]
    public static List<ProposalDet> ShowQuery(string id, string sid)
    {
        string gStrRetVal = null;
        int returnval = 0;
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProp = new ProposalDet();
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        try
        {
            gStrRetVal = id;
            objProp.strAction = "E";
            objProp.strProposalNo = id;
            objProp.intNoOfTimes = Convert.ToInt32(sid);
            objProposalList = objService.ServicegetRaisedQueryDetails(objProp).ToList();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            //objLinc = null;
        }
        return objProposalList;
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                objServiceEntity = new ServiceDetails();
                objServiceEntity.intCreatedBy = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
                objServiceEntity.strAction = "GDA";
                objServiceEntity.Deptid = Convert.ToInt32(ddlDepartment.SelectedValue);
                ServiceDetail = objService.GetApplicationNoofParticularUserDepartmentWise(objServiceEntity).ToList();
                ddlApplicationNo.DataSource = ServiceDetail;
                ddlApplicationNo.DataTextField = "strApplicationUnqKey";
                ddlApplicationNo.DataBind();
                ddlApplicationNo.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                fillApplicationNo();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                this.gvApplicationDetails.PageIndex = 0;
                this.gvApplicationDetails.AllowPaging = false;
            }
            else
            {
                lbtnAll.Text = "All";
                this.gvApplicationDetails.AllowPaging = true;
            }

            BindGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void DisplayPaging()
    {
        try
        {
            if (gvApplicationDetails.Rows.Count > 0)
            {
                lblPaging.Visible = true;
                lbtnAll.Visible = true;
                if (gvApplicationDetails.PageIndex + 1 == gvApplicationDetails.PageCount)
                {
                    lblPaging.Text = "Results <b>" + ((Label)gvApplicationDetails.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + dtval + "</b> of <b>" + dtval + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results <b>" + ((Label)gvApplicationDetails.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)gvApplicationDetails.Rows[0].FindControl("lblsl")).Text) + gvApplicationDetails.PageSize - 1) + "</b> of <b>" + dtval + "</b>";
                }
            }
            else
            {
                lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            IntGridBindCount = 1;
            BindGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    #region Added by Sushant Jena

    /// <summary>
    /// Added by Sushant Jena On Dt.17-Aug-2018
    /// To get child unit name and self unit name for a investor.
    /// </summary>
    private void fillInvestorChildUnit()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "INVUNIT";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

            DataTable dt = new DataTable();
            dt = objserviceDashboard.getInvestorChildUnit(objSWP);

            if (dt.Rows.Count > 1)
            {
                DrpDwn_Investor_Unit.DataTextField = "VCH_INV_NAME";
                DrpDwn_Investor_Unit.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_Investor_Unit.DataSource = dt;
                DrpDwn_Investor_Unit.DataBind();
                DrpDwn_Investor_Unit.Items.Insert(0, new ListItem("-Select Unit-", "0"));

                divUnitName.Visible = true;
            }
            else
            {
                DrpDwn_Investor_Unit.Items.Clear();
                divUnitName.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Added by Sushant Jena On Dt.18-Aug-2018
    /// To get list of Departments for a user (Including department of child user)
    /// This method is actually splited from above BindDropDown() method (which have no use in after modification)
    /// </summary>
    private void fillDepartment()
    {
        try
        {
            /*----------------------------------------------------------*/
            /////// Fill Department Name DropDownList
            /*----------------------------------------------------------*/

            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                objServiceEntity.intCreatedBy = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }
            else
            {
                objServiceEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            }

            ServiceDetail = objService.GetDepartmentofParticularUser(objServiceEntity).ToList();

            ddlDepartment.Items.Clear();
            if (ServiceDetail.Count > 0)
            {
                ddlDepartment.DataSource = ServiceDetail;
                ddlDepartment.DataTextField = "str_Department";
                ddlDepartment.DataValueField = "Deptid";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Added by Sushant Jena On Dt.18-Aug-2018
    /// To get list of Application Numbers for a user (Including application nos. of child user)
    /// This method is actually splited from above BindDropDown() method (which have no use in after modification)
    /// </summary>
    private void fillApplicationNo()
    {
        try
        {
            /*----------------------------------------------------------*/
            /////// Fill Application Numbers DropDownList
            /*----------------------------------------------------------*/

            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                objServiceEntity.intCreatedBy = Convert.ToInt32(DrpDwn_Investor_Unit.SelectedValue);
            }
            else
            {
                objServiceEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            }

            objServiceEntity.strAction = "GA";
            ServiceDetail = objService.GetAppliedApplicationNoofParticularUser(objServiceEntity).ToList();

            ddlApplicationNo.Items.Clear();
            if (ServiceDetail.Count > 0)
            {
                ddlApplicationNo.DataSource = ServiceDetail;
                ddlApplicationNo.DataTextField = "strApplicationUnqKey";
                ddlApplicationNo.DataBind();
                ddlApplicationNo.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlApplicationNo.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt.17-Aug-2018
    /// Filter details by unit 
    /// C-Chain (Means Including child users)
    /// I-Indivisual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DrpDwn_Investor_Unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //BindDropDown();
            fillDepartment();
            fillApplicationNo();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    #endregion

    #region Added by Manoj Kumar Behera

    private string BPAS(string strApplicationKey, string demandtype)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        string EncryptValue = "";
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_BPAS_SERVICE_DISPLAY";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
                cmd.Parameters.AddWithValue("@VCH_ACTION", "DRAFTSERVICEINFO");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strApplicationKey);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "BPASVIEWSP");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }

            if (dt.Rows.Count > 0)
            {
                string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                var client = new RestClient("" + AppBmcUrl + "");
                var request = new RestRequest(Method.POST);
                request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "~::~" + dt.Rows[0]["VCH_EMAIL"].ToString() + "~::~" + dt.Rows[0]["VCH_INV_USERID"].ToString() + "~::~" + dt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString() + "~::~" + ConfigurationManager.AppSettings["GOSWIFTAPPLICATIONURL"].ToString() + "~::~" + demandtype + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string JSON = response.Content;
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                EncryptValue = dict["result"].ToString();
            }
            else
            {
                string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
                var client = new RestClient("" + AppBmcUrl + "");
                var request = new RestRequest(Method.POST);
                request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + ConfigurationManager.AppSettings["GOSWIFTAPPLICATIONURL"].ToString() + "~::~" + demandtype + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string JSON = response.Content;
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
                EncryptValue = dict["result"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "BPASVIEW");
        }
        return EncryptValue;
    }
    private string FIRE(string strApplicationKey, int intServiceid)
    {
        string EncryptValue = "";
        try
        {
            var client = new RestClient("" + ConfigurationManager.AppSettings["FIREVIEWSTATUSURL"].ToString() + "?applicationId=" + strApplicationKey + "&serviceId=" + intServiceid + "&source='GOSWIFT'");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            string JSON = response.Content;
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
            EncryptValue = dict["result"].ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "FIREVIEW");
        }
        return EncryptValue;
    }
    private string EXCISE(string strApplicationKey, int intServiceid)
    {
        string EncryptValue = "";
        try
        {
            if (intServiceid == 67)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISESRVIEWSTATUSURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"gosiwft_id\":\"" + strApplicationKey + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall" + intServiceid.ToString() + "", client.ToString(), response.StatusCode.ToString());
                    EncryptValue = "";
                }
                else
                {
                    EncryptValue = response.ResponseUri.ToString();
                }
            }
            else
            {
                var client = new RestClient(ConfigurationManager.AppSettings["EXCISEGNSRVIEWSTATUSURL"].ToString());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\r\n    \"gosiwft_id\":\"" + strApplicationKey + "\"\r\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() != "OK")
                {
                    Util.LogRequestResponse("ExciseServiceCall" + intServiceid.ToString() + "", client.ToString(), response.StatusCode.ToString());
                    EncryptValue = "";
                }
                else
                {
                    EncryptValue = response.ResponseUri.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EXCISEVIEW");
        }
        return EncryptValue;
    }
    private string EIT(string strApplicationKey, int intServiceid)
    {
        string EncryptValue = "";
        try
        {
            EncryptValue = ConfigurationManager.AppSettings["MobileTowerViewStatusUrl"].ToString() + "?uniqid=" + strApplicationKey + "&ServiceId=" + intServiceid + "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EITVIEW");
        }
        return EncryptValue;
    }
    private string OSBC(string strApplicationKey)
    {
        string EncryptValue = "";
        try
        {
            EncryptValue = ConfigurationManager.AppSettings["OSBCVIEWSTATUSURL"].ToString() + "?Application_No=" + strApplicationKey + "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OSBCVIEW");
        }
        return EncryptValue;
    }

    #endregion

    protected void BtnPayMultipe_Click(object sender, EventArgs e)
    {
        try
        {
            int intCheckStatus = 0;
            StringBuilder VCHSERVICEID = new StringBuilder();
            StringBuilder VCHAPPLICATIONKEY = new StringBuilder();

            /*------------------------------------------------------------------------------------*/
            ///// The Description for below DataTable provided in "DepartmentClearance.aspx" Page.
            /*------------------------------------------------------------------------------------*/
            DataTable dt = new DataTable();
            dt.Columns.Add("intSlNo", typeof(int));
            dt.Columns.Add("intServiceId", typeof(string));
            dt.Columns.Add("vchFormName", typeof(string));
            dt.Columns.Add("vchServiceName", typeof(string));
            dt.Columns.Add("intServiceType", typeof(string));
            dt.Columns.Add("intExternalType", typeof(string));
            dt.Columns.Add("vchProposalNo", typeof(string));
            dt.Columns.Add("decAmount", typeof(string));
            dt.Columns.Add("intCompletedStatus", typeof(string));
            dt.Columns.Add("vchApplicationKey", typeof(string));
            dt.Columns.Add("vchUrl", typeof(string));
            dt.Columns.Add("vchUpdateUrl", typeof(string));
            dt.Columns.Add("vchDeptName", typeof(string));
            dt.Columns.Add("intHoaAccount", typeof(string));

            for (int i = 0; i < gvApplicationDetails.Rows.Count; i++)
            {
                int intServiceId = Convert.ToInt32(gvApplicationDetails.DataKeys[i].Values["intServiceId"]);
                string strProposalNo = Convert.ToString(gvApplicationDetails.DataKeys[i].Values["strProposalId"]);

                Label lblApplicationNo = (Label)gvApplicationDetails.Rows[i].FindControl("lblApplicationNo");
                Label LblServiceName = (Label)gvApplicationDetails.Rows[i].FindControl("lblServiceName");
                Label LblPaymentAmount = (Label)gvApplicationDetails.Rows[i].FindControl("lblpaymentAmount");
                CheckBox ChkBxSelect = (CheckBox)gvApplicationDetails.Rows[i].FindControl("ChkBxSelect");

                if (ChkBxSelect.Checked)
                {

                    intCheckStatus = 1;

                    if (VCHSERVICEID.Length > 0)
                    {
                        VCHSERVICEID.Append("," + intServiceId + "");
                    }
                    else
                    {
                        VCHSERVICEID.Append("" + intServiceId + "");
                    }

                    if (VCHAPPLICATIONKEY.Length > 0)
                    {
                        VCHAPPLICATIONKEY.Append("," + lblApplicationNo.Text + "");
                    }
                    else
                    {
                        VCHAPPLICATIONKEY.Append("" + lblApplicationNo.Text + "");
                    }

                    dt.Rows.Add(0, intServiceId, "", LblServiceName.Text, 1, 1, strProposalNo, LblPaymentAmount.Text, 1, lblApplicationNo.Text, "", "", "", 0);
                }
            }

            if (intCheckStatus == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "jAlert('<strong>Please select at least one check box to proceed !</strong>');", true);
                return;
            }
            else
            {
                Session["SvcMasterData"] = dt;
                Response.Redirect("ApplicationConsolidateDetails.aspx");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
}