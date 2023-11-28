//******************************************************************************************************************
// File Name             :   CmsOrtpsaPosting.cs
// Description           :   To push PEAL information to CMS (Central Monitoring System) Portal for tracking of ORTPSA timeline
// Created by            :   Sushant Kumar Jena
// Created on            :   27-Nov-2019
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Configuration;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for CmsOrtpsaPosting
/// </summary>
public class CmsOrtpsaPosting
{
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    public CmsOrtpsaPosting()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void SendServiceInfoToCMS()
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            objCommand.CommandText = "USP_CMS_ORTPSA_DETAILS";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "A");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Tables[0].Rows.Count > 0)
            {
                string strModuleName = "CmsOrtpsaScheduler";
                string strMethodName = "SendServiceInfoToCMS";
                string strReqData = "";
                string strResData = "";

                for (int i = 0; i < objds.Tables[0].Rows.Count; i++)
                {
                    string strDistCode = Convert.ToString(objds.Tables[0].Rows[i]["vchCMSDistrictId"]);
                    string strOfficeCode = Convert.ToString(objds.Tables[0].Rows[i]["vchCMSOfficeCode"]);
                    string strOfficeAddress = Convert.ToString(objds.Tables[0].Rows[i]["vchDomainUName"]);
                    string strOfficerName = Convert.ToString(objds.Tables[0].Rows[i]["vchFullName"]);
                    string strApplicantName = Convert.ToString(objds.Tables[0].Rows[i]["vchCompName"]);
                    string strApplicantAddress = Convert.ToString(objds.Tables[0].Rows[i]["vchAddress"]);
                    string strMobileNo = Convert.ToString(objds.Tables[0].Rows[i]["vchPhoneNo"]);
                    string strApplicationDate = Convert.ToString(objds.Tables[0].Rows[i]["dtmPealApplyDate"]);
                    string strApplicationLastDate = Convert.ToString(objds.Tables[0].Rows[i]["dtmUpdatedOn"]);
                    string strApplicantStatus = Convert.ToString(objds.Tables[0].Rows[i]["intApprovalStatus"]);
                    string strAckNo = Convert.ToString(objds.Tables[0].Rows[i]["vchProposalNo"]);
                    string strIdcoStatus = Convert.ToString(objds.Tables[0].Rows[i]["PIDCOStatus"]);
                    string strIdcoStatusCode = Convert.ToString(objds.Tables[0].Rows[i]["vchstatuscode"]);
                    int intApprovalStatus = Convert.ToInt32(objds.Tables[0].Rows[i]["intApprovalStatus"]);
                    int intIDCObtnClickStatus = Convert.ToInt32(objds.Tables[0].Rows[i]["intIDCObtnClickStatus"]);

                    /*----------------------------------------------------------------------------------------------------*/
                    ///// For each proposal 5 stages of data to be sent to CMS portal.
                    ///// If application status is PENDING (1) then service code will be "87" and the action method "app_entry" to be called (STAGE-1).
                    ///// If application status is APPROVED (2) the service code will be either 87,88 or 89 depending upon idco forwarded status
                    ///// If intIDCObtnClickStatus=0 means application is not forwarded to IDCO portal the service code=87 and Action="app_update"(STAGE-2).
                    ///// If intIDCObtnClickStatus=1 means application is forwarded to IDCO portal, then service code will be either 88 or 89 depending on the Idco status code.
                    ///// If intIDCObtnClickStatus=1 and strIdcoStatusCode !=4 means application is forwarded to IDCO and application is in 1st stage of processing.
                    ///// For above case the service code will be 88 and action will be "app_entry"(STAGE-3)(Means Intial stage for an application in Idco portal).
                    ///// If intIDCObtnClickStatus=1 and strIdcoStatusCode = 4 then service code will be either 88 or 89 depending on the Idco status text.
                    ///// If strIdcoStatus="AD CHECK (IN-PROGRESS)" then service code=88 and Action = "app_update"(STAGE-4).
                    ///// If strIdcoStatus="LAND ALLOTTED" then service code=89 and Action = "app_entry"(STAGE-5).
                    /*----------------------------------------------------------------------------------------------------*/

                    string strServiceCode = "";
                    string strServiceAction = "";
                    int intSentStatus = 0;

                    if (intApprovalStatus == 1) //// Pending
                    {
                        strServiceCode = "87";
                        strServiceAction = "app_entry";
                        intSentStatus = 1;
                    }
                    else if (intApprovalStatus == 2) //// Approved 
                    {
                        if (intIDCObtnClickStatus == 0) //// Not forwarded to IDCO
                        {
                            strServiceCode = "87";
                            strServiceAction = "app_update";
                            intSentStatus = 2;
                        }
                        else if (intIDCObtnClickStatus == 1) //// Forwarded to IDCO
                        {
                            if (strIdcoStatusCode != "4" && Convert.ToString(objds.Tables[0].Rows[i]["dtmIdcoForwardDate"]) != "")
                            {
                                strApplicationLastDate = Convert.ToString(objds.Tables[0].Rows[i]["dtmIdcoForwardDate"]);
                                strServiceCode = "88";
                                strServiceAction = "app_entry";
                                intSentStatus = 3;
                            }
                            else
                            {
                                if (strIdcoStatusCode == "4" && strIdcoStatus.ToUpper() == "AD CHECK (IN-PROGRESS)")
                                {
                                    strServiceCode = "88";
                                    strApplicationLastDate = Convert.ToString(objds.Tables[0].Rows[i]["dtmIdcoStatusDate"]);
                                    strServiceAction = "app_update";
                                    intSentStatus = 4;
                                }

                                if (strIdcoStatusCode == "4" && strIdcoStatus.ToUpper() == "LAND ALLOTTED")
                                {
                                    strApplicationLastDate = Convert.ToString(objds.Tables[0].Rows[i]["dtmIdcoStatusDate"]);
                                    strServiceCode = "89";
                                    strServiceAction = "app_entry";
                                    intSentStatus = 5;
                                }
                            }
                        }
                    }

                    /*-----------------------------------------------------------------*/

                    if (i > 0)
                    {
                        Thread.Sleep(2000); //// Wait for 2 sec
                    }

                    /*-----------------------------------------------------------------*/
                    //// Check whether the sent status is present for respective proposal or not.
                    //// If not present then sent request else goto next loop.                   
                    /*-----------------------------------------------------------------*/
                    DataRow[] dRow = objds.Tables[1].Select("vchProposalNo = '" + strAckNo + "' and intStatus='" + intSentStatus + "'");
                    if (dRow.Length == 0)
                    {
                        if (strServiceCode != "") //// Progress on status for the respective proposal.
                        {
                            string strServiceUrl = ConfigurationManager.AppSettings["CmsOrtpsaServiceUrl"].ToString();
                            if (strServiceAction == "app_entry")
                            {
                                strServiceUrl = strServiceUrl
                                                 + "?circuit=public"
                                                 + "&fuseaction=app_entry"
                                                 + "&apikey=9c2768c10b87736f5b307b36ed5238c28c15b38c"
                                                 + "&deptcode=16"
                                                 + "&dcode=" + strDistCode
                                                 + "&scode=" + strServiceCode
                                                 + "&ocode=" + strOfficeCode
                                                 + "&oaddress=" + strOfficeAddress
                                                 + "&doname=" + strOfficerName
                                                 + "&appname=" + strApplicantName
                                                 + "&appaddress=" + strApplicantAddress
                                                 + "&phone=" + strMobileNo
                                                 + "&entry_date=" + strApplicationDate
                                                 + "&last_date=" + strApplicationLastDate
                                                 + "&astatus=" + strApplicantStatus
                                                 + "&ack_no=" + strAckNo;
                            }
                            else if (strServiceAction == "app_update")
                            {
                                strServiceUrl = strServiceUrl
                                                + "?circuit=public"
                                                + "&fuseaction=app_update"
                                                + "&apikey=9c2768c10b87736f5b307b36ed5238c28c15b38c"
                                                + "&deptcode=16"
                                                + "&dcode=" + strDistCode
                                                + "&scode=" + strServiceCode
                                                + "&oaddress=" + strOfficeAddress
                                                + "&ack_no=" + strAckNo
                                                + "&ddate=" + strApplicationDate
                                                + "&last_date=" + strApplicationLastDate
                                                + "&dstatus=" + strApplicantStatus
                                                + "&rejected_details=" + "";
                            }

                            strReqData = "(Request):-" + strServiceUrl;


                            /*----------------------------------------------------------------------------------------------------*/
                            ///// CMS ORTPSA Service Response Description (Refer Service document for details)
                            ///// (For action = app_entry)
                            ///// 0-Failure
                            ///// 1-Success
                            ///// 2-Invalid API Key
                            ///// 3-Entry Date Or Last Date Invalid Format
                            ///// 4-Invalid Acknowledgement Number
                            ///// 5-Duplicate Record
                            ///// 6-Invalid Department Code/District Code/Service Code
                            ///// 7-Application not present at CMS end but app_update action is requested.
                            ///// (For Action = app_update)                       
                            ///// 1-Success
                            ///// 2-Invalid API Key
                            ///// 3-Application Delivery Date OR Last Date Invalid Format
                            ///// 4-Invalid Acknowledgement Number
                            ///// 5-Invalid Delivery Status/Invalid Rejected Reason if Application is Rejected
                            ///// 6-Invalid Department Code/District Code/Service Code 
                            /*----------------------------------------------------------------------------------------------------*/

                            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(strServiceUrl));
                            httpRequest.Accept = "application/json";
                            httpRequest.ContentType = "application/json";
                            httpRequest.Method = "GET";
                            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                            {
                                using (Stream stream = httpResponse.GetResponseStream())
                                {
                                    string strResult = (new StreamReader(stream)).ReadToEnd();
                                    strResData = "(Response):-" + strResult;

                                    if (strResult == "1")
                                    {
                                        objCommand = new SqlCommand();
                                        objCommand.CommandText = "USP_CMS_ORTPSA_DETAILS";
                                        objCommand.CommandType = CommandType.StoredProcedure;
                                        objCommand.Connection = objConn;

                                        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "B"); //// Insert data to Log.
                                        objCommand.Parameters.AddWithValue("@P_VCH_PROPOSAL_NO", strAckNo);
                                        objCommand.Parameters.AddWithValue("@P_INT_STATUS", intSentStatus);

                                        objCommand.ExecuteNonQuery();
                                    }
                                }
                            }

                            ///// Write Request Response Log
                            Util.LogRequestResponse(strModuleName, strMethodName, strReqData);
                            Util.LogRequestResponse(strModuleName, strMethodName, strResData);
                        }
                        else
                        {
                            ///// Write Request Response Log
                            Util.LogRequestResponse(strModuleName, strMethodName, "No progress in status for proposal no:- " + strAckNo + "");
                        }
                    }
                    else
                    {
                        ///// Write Request Response Log
                        Util.LogRequestResponse(strModuleName, strMethodName, "Status Info for Proposal No: " + strAckNo + " and Sent Status: " + intSentStatus + " has already sent.");
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("CmsOrtpsaScheduler", "SendServiceInfoToCMS", "No record found " + objds.Tables[0].Rows.Count.ToString());
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CmsOrtpsaScheduler");
        }
        finally
        {
            objConn.Close();
            objCommand = null;
            objds = null;
        }
    }
}