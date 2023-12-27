﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json;

/// <summary>
/// Summary description for PaymentScheduler
/// </summary>
public class PaymentScheduler
{
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    ProposalBAL objService = new ProposalBAL();

    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();

    public PaymentScheduler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    ///// For Service     
    public void ServicePaymentSchedule()
    {
        Util.LogRequestResponse("PaymentScheduler", "ServicePaymentSchedule", "Called");

        string results;

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objds = new DataTable();

            objCommand.CommandText = "USP_FETCH_PAYMENT_ORDER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "S");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Rows.Count > 0)
            {
                Util.LogRequestResponse("PaymentScheduler", "ServicePaymentSchedule", "[TOTAL_RECORD_FOUND]:- " + objds.Rows.Count.ToString());

                for (int i = 0; i < objds.Rows.Count; i++)
                {
                    string strOrderNo = Convert.ToString(objds.Rows[i]["vchOrderNo"]);
                    decimal decChallanAmt = Convert.ToDecimal(objds.Rows[i]["vchChallanAmount"]);

                    if (i > 0)
                    {
                        Thread.Sleep(50000); //// Wait for around 1 min
                    }

                    /*--------------------------------------------------------------------*/
                    ///Get Payment Status From Treasury
                    /*--------------------------------------------------------------------*/
                    TreasuryPaymentTracking objTreasury = new TreasuryPaymentTracking();
                    string strTreasuryRes = objTreasury.GetPaymentStatusFromTreasury(strOrderNo, decChallanAmt);

                    Util.LogRequestResponse("PaymentScheduler", "ServicePaymentSchedule", "[TREASURY_RESPONSE]--->>[ORDER_NO]:- " + strOrderNo + " --- [AMOUNT]:- " + Convert.ToString(decChallanAmt) + " --- [RESPONSE_FROM_TREASURY]:- " + strTreasuryRes);

                    if (strTreasuryRes != "")
                    {
                        string[] Output = strTreasuryRes.Split('|');
                        string strBankTranStatus = Output[5].ToString();

                        /*--------------------------------------------------------------------*/
                        /// Only successfull transaction will be taken for updation.                       
                        /*--------------------------------------------------------------------*/
                        if (strBankTranStatus == "S")
                        {
                            PromoterDet objPromoter = new PromoterDet();
                            objPromoter.strAction = "B"; //// Update payment for Service
                            objPromoter.bankname = null;
                            objPromoter.ifsccode = null;
                            objPromoter.dealername = null;
                            objPromoter.ordernumber = Output[1].ToString();
                            objPromoter.bankcode = Output[4].ToString();
                            objPromoter.treasuryrefno = Output[3].ToString();
                            objPromoter.banktranstimestamp = Output[7].ToString();
                            objPromoter.banktransstatus = strBankTranStatus;

                            /*--------------------------------------------------------------------*/
                            /// DML Operation to Update Service Payment Status
                            /*--------------------------------------------------------------------*/
                            results = objService.PEALServiceOrderUpdate(objPromoter);
                        }
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("PaymentScheduler", "ServicePaymentSchedule", "[NO_RECORDS_FOUND]");
            }
        }
        catch (Exception ex)
        {
            results = ex.Message;
            Util.LogRequestResponse("PaymentScheduler", "ServicePaymentSchedule", "[OUTPUT_RESPONSE][FAILURE]:- " + results);
            Util.LogError(ex, "ServicePaymentScheduler");
        }
        finally
        {
            objConn.Close();
        }
    }

    // FOR json string format
    string FormatJSON(string name, string value)
    {
        //return "\"\"" + name + "\"\": " + "\"\"" + value + "\"\"";

        return "\"" + name + "\":" + "\"" + value + "\"";
    }

    public void ExternalServicePaymentSchedule()
    {
        Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "Called");
        string results;

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objds = new DataTable();
            objCommand.CommandText = "USP_FETCH_PAYMENT_ORDER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;
            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "ES");
            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);
            if (objds.Rows.Count > 0)
            {
                Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[TOTAL_RECORD_FOUND]:- " + objds.Rows.Count.ToString());
                for (int i = 0; i < objds.Rows.Count; i++)
                {
                    
                    string groupcode = Convert.ToString(objds.Rows[i]["VCH_TRACKING_ID"]);
                    string Serviceid = Convert.ToString(objds.Rows[i]["INT_SERVICEID"]);

                    if (i > 0)
                    {
                       // Thread.Sleep(50000); //// Wait for around 1 min  5 * 60 * 1000
                        Thread.Sleep(1 * 60 * 1000); //// Wait for around 5 min
                    }



                    if (string.IsNullOrEmpty(groupcode) !=true)
                    {
                        if(Serviceid == "5" || Serviceid == "6" || Serviceid == "7" || Serviceid == "34" || Serviceid == "35" || Serviceid == "36" || Serviceid == "39" || Serviceid == "40" || Serviceid == "70" || Serviceid == "71" || Serviceid == "72")  //F&B and Labour Service
                        {
                            string strOrderNo = Convert.ToString(objds.Rows[i]["vchOrderNo"]);
                            string decChallanAmt = Convert.ToString(objds.Rows[i]["vchChallanAmount"]);

                            string ApplicationNo = Convert.ToString(objds.Rows[i]["VCH_APPLICATION_UNQ_KEY"]);
                            string UserId = Convert.ToString(objds.Rows[i]["intCreatedBy"]);
                            string BnkTransid= Convert.ToString(objds.Rows[i]["vchBankTransid"]);
                            string uri = ConfigurationManager.AppSettings["ePareshramPaymentStatusUpdate"].ToString();
                           
                           

                            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                            // var client = new RestClient("http://61.2.215.77:8085/pareshram/goswift/UpdatePaymentInfo");
                             var client = new RestClient("http://61.2.45.238:8081/pareshram/goswift/UpdatePaymentInfo"); //staging url 
                           // client.Timeout = -1;
                            client.Timeout = 15000;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                          //  request.AddHeader("Cookie", "JSESSIONID=8AEF5EA82C3A974C204C66F758AAB184");
                            

                           
                            var body = @"{" + FormatJSON("UserId", UserId) + "," + FormatJSON("GoSwiftApplicationID", ApplicationNo) + "," + FormatJSON("BankTranscatioID", BnkTransid) + "," + FormatJSON("TYPE", "CC") + "," + FormatJSON("AMOUNT", decChallanAmt) + ","+ FormatJSON("STATUS", "1") +"}";

                            Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[URL GENERATE][from Eshram]:- " + uri + "" + body.ToString());

                            request.AddParameter("application/json", body, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            ///*----------------------------------------------------------------------------*/
                            /////Write the  URL API Response in the Log File.
                            ///*----------------------------------------------------------------------------*/
                            Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[RESPONSE_JSON_STRING_PAYMENT_UPDATE]:- " + response.Content.ToString());

                            if (response.Content.ToString() != "")
                            {
                                string strStatus = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content)["Code"].ToString();
                                if (strStatus == "1")
                                {
                                    
                                    string URLVALU = uri + " " + body;

                                    ///Insert application status in Log table.
                                    InsertQueryStatusPushLog(ApplicationNo, Serviceid, URLVALU, strStatus.ToString());
                                }
                            }

                        }
                    }

                }
            }
            else
            {
                Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[NO_RECORDS_FOUND]");
            }
        }
        catch(Exception ex)
        {
            results = ex.Message;
            Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[OUTPUT_RESPONSE][FAILURE]:- " + results);
            Util.LogError(ex, "ExternalServicePaymentSchedule");
        }
    }


    private void InsertQueryStatusPushLog(string ApplicationNo,string Serviceid,string URLVALU, string strStatus)
    {
        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }
        try
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandText = "USP_EXTERNAL_SERVICE_PAYMENT_STATUS_UPDATE_LOG";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;
            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "EL");
            objCommand.Parameters.AddWithValue("@vchApplicationNo", ApplicationNo);
            objCommand.Parameters.AddWithValue("@vchServiceId", Serviceid);
            objCommand.Parameters.AddWithValue("@vchRequestUrl", URLVALU);
            objCommand.Parameters.AddWithValue("@vchResponseUrl", strStatus);
            objCommand.ExecuteNonQuery();
            Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[OUTPUT_LOG RECORD INSERTED IN T_ExternalServiceLog table]:- " + ApplicationNo);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    ///// For PEAL    
    public void PealPaymentSchedule()
    {
        Util.LogRequestResponse("PaymentScheduler", "PealPaymentSchedule", "Called");

        string results;

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objds = new DataTable();

            objCommand.CommandText = "USP_FETCH_PAYMENT_ORDER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "P");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Rows.Count > 0)
            {
                Util.LogRequestResponse("PaymentScheduler", "PealPaymentSchedule", "[TOTAL_RECORD_FOUND]:-  " + objds.Rows.Count.ToString());

                for (int i = 0; i < objds.Rows.Count; i++)
                {
                    string strOrderNo = Convert.ToString(objds.Rows[i]["vchOrderNo"]);
                    decimal decChallanAmt = Convert.ToDecimal(objds.Rows[i]["vchChallanAmount"]);
                    string strMobileNo = Convert.ToString(objds.Rows[i]["VCH_OFF_MOBILE"]);

                    if (i > 0)
                    {
                        Thread.Sleep(50000);//// Wait for around 1 min
                    }

                    /*--------------------------------------------------------------------*/
                    ///Get Payment Status From Treasury
                    /*--------------------------------------------------------------------*/
                    TreasuryPaymentTracking objTreasury = new TreasuryPaymentTracking();
                    string strTreasuryRes = objTreasury.GetPaymentStatusFromTreasury(strOrderNo, decChallanAmt);

                    Util.LogRequestResponse("PaymentScheduler", "PealPaymentSchedule", "[TREASURY_RESPONSE]--->>[ORDER_NO]:- " + strOrderNo + " --- [AMOUNT]:- " + Convert.ToString(decChallanAmt) + " --- [RESPONSE_FROM_TREASURY]:- " + strTreasuryRes);

                    if (strTreasuryRes != "")
                    {
                        string[] Output = strTreasuryRes.Split('|');
                        string strBankTranStatus = Output[5].ToString();

                        /*--------------------------------------------------------------------*/
                        /// Only successfull transaction will be taken for updation.
                        /*--------------------------------------------------------------------*/
                        if (strBankTranStatus == "S")
                        {
                            PromoterDet objPromoter = new PromoterDet();
                            objPromoter.strAction = "A"; //// Update payment for PEAL

                            objPromoter.bankname = null;
                            objPromoter.ifsccode = null;
                            objPromoter.dealername = null;
                            objPromoter.ordernumber = Output[1].ToString();
                            objPromoter.bankcode = Output[4].ToString();
                            objPromoter.treasuryrefno = Output[3].ToString();
                            objPromoter.banktranstimestamp = Output[7].ToString();
                            objPromoter.banktransstatus = strBankTranStatus;

                            /*--------------------------------------------------------------------*/
                            /// DML Operation to Update PEAL Payment Status
                            /*--------------------------------------------------------------------*/
                            results = objService.PEALServiceOrderUpdate(objPromoter);

                            /*--------------------------------------------------------------------*/
                            /// Send promotional SMS to the Investor after payment get success.
                            /// After the payment for the PEAL application is successful, this SMS will be sent.
                            /*--------------------------------------------------------------------*/
                            CommonHelperCls objComm = new CommonHelperCls();
                            objComm.SendPromotionalSMS(strMobileNo, "AFTER_PEAL_SUBMISSION");
                        }
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("PaymentScheduler", "PealPaymentSchedule", "[NO_RECORDS_FOUND]");
            }
        }
        catch (Exception ex)
        {
            results = ex.Message;
            Util.LogRequestResponse("PaymentScheduler", "PealPaymentSchedule", "[OUTPUT_RESPONSE][FAILURE]:- " + results);
            Util.LogError(ex, "PealPaymentScheduler");
        }
        finally
        {
            objConn.Close();
        }
    }
}