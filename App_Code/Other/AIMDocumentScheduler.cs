using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using DWHServiceReference;
using System.Configuration;

/// <summary>
/// Summary description for AIMDocumentScheduler
/// </summary>
public class AIMDocumentScheduler
{
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    public AIMDocumentScheduler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void GetEinPcDocumentFromAIM()
    {
        Util.LogRequestResponse("AIMDocScheduler", "GetEinPcDocumentFromAIM", "Fired");


        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objds = new DataTable();

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            objCommand.CommandText = "USP_INV_USER_MANAGEMENT_VIEW";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "V8");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Rows.Count > 0)
            {
                string strModuleName = "AIMDocScheduler";
                string strMethodName = "GetEinPcDocumentFromAIM";
                string strReqResData = "";

                for (int i = 0; i < objds.Rows.Count; i++)
                {
                    string strUniqueId = Convert.ToString(objds.Rows[i]["VCH_UNIQUEID"]);
                    //strUniqueId = "CC5DDF02-CAFA-4C41-8165-9C55EF34E107";

                    if (i > 0)
                    {
                        Thread.Sleep(30000); //// Wait for 30 sec
                    }

                    /*----------------------------------------------------------*/

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LICNDOC.pdf";
                    string strPath = "~/Document/RegdDoc/";

                    if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(strPath)))
                    {
                        Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath(strPath));
                    }

                    ///// AIM File Handler       
                    AIMServiceReference.UserRegistrationClient objAimRegd = new AIMServiceReference.UserRegistrationClient();
                    string strEncUniqueKey = objAimRegd.Encrypt(strUniqueId, "AZBY19LXaghkLM12WX");

                    ///// Send request to download file
                    byte[] data;
                    using (WebClient client = new WebClient())
                    {
                        string strAddress = "http://164.100.141.243/DownloadPDF.ashx?PARAM=" + strEncUniqueKey;
                        data = client.DownloadData(strAddress);
                    }

                    if (data.Length > 0)
                    {
                        if (IsFileValid(data))
                        {
                            /////Copy the file to destination folder
                            File.WriteAllBytes(System.Web.Hosting.HostingEnvironment.MapPath(strPath + strFileName), data);

                            //// Save file to database(GOSWIFT)
                            InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
                            InvestorDetails objEntity = new InvestorDetails();

                            objEntity.strAction = "UD";
                            objEntity.strUniqueId = strUniqueId;
                            objEntity.strLicenceDoc = strFileName;

                            ///////DML Operation
                            string strResult = objBAL.UserManagementAED(objEntity);
                            if (strResult == "1")
                            {
                                /*-----------------------------------------------------------------*/
                                ///// Once updated in GOSWIFT,update the document name in DWH     
                                /*-----------------------------------------------------------------*/
                                /////// Service Initialization
                                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                                EINModel objEnt = new EINModel
                                {
                                    ///// Assign value to property
                                    ///// intRequestType = 2 Means it will only update document Name
                                    ///// intRequestType = 1 Means it will only update EIN No and EIN Type
                                    intRequestType = 2,
                                    strDocument = strFileName,
                                    strUniqueId = strUniqueId
                                };

                                /////// Generate Encryption Key (Security key to access Data Warehouse servce methods)
                                string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                                string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

                                /////// DML opertion through service
                                string strReturnVal = objSrvRef.EinUpdate(objEnt, strSecurityKey);
                                if (strReturnVal == "11")
                                {
                                    strReqResData = "(Request)---Unique ID :- " + strUniqueId + "--->" + "(Response)--- StatusDesc :- " + "Invalid User at DWH Portal.(Return Status Code=11)";
                                }
                                else if (strReturnVal == "2")
                                {
                                    strReqResData = "(Request)---Unique ID :- " + strUniqueId + "--->" + "(Response)--- StatusDesc :- " + "Document downloaded successfully(Return Status Code=2)";
                                }
                                else
                                {
                                    strReqResData = "(Request)---Unique ID :- " + strUniqueId + "--->" + "(Response)--- StatusDesc :- " + "Something went wrong to update file name in DWH.";
                                }
                            }
                            else if (strResult == "2")
                            {
                                strReqResData = "(Request)---Unique ID :- " + strUniqueId + "--->" + "(Response)--- StatusDesc :- " + "Something went wrong to update file name in GOSWIFT.";
                            }
                        }
                        else
                        {
                            strReqResData = "(Request)---Unique ID :- " + strUniqueId + "--->" + "(Response)--- StatusDesc :- " + "Invalid or corrupted file found.";
                        }
                    }
                    else
                    {
                        strReqResData = "(Request)---Unique ID :- " + strUniqueId + "--->" + "(Response)--- StatusDesc :- " + "No file found for download.";
                    }

                    ///// Write Request Response Log
                    Util.LogRequestResponse(strModuleName, strMethodName, strReqResData);
                }
            }
            else
            {
                Util.LogRequestResponse("AIMDocScheduler", "GetEinPcDocumentFromAIM", "No record found" + objds.Rows.Count.ToString());
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "AIMDocScheduler");
            Util.LogRequestResponse("AIMDocScheduler", "GetEinPcDocumentFromAIM", "Exception" + ex.Message.ToString());
        }
        finally
        {
            objConn.Close();
            objCommand = null;
            objds = null;
        }
    }

    private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
    private bool IsFileValid(byte[] filebyte)
    {
        if (filebyte.Take(7).SequenceEqual(PDF))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}