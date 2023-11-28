<%--'*******************************************************************************************************************
' File Name         : DownloadFileRepositoryNSWS.ashx
' Description       : Download file from NSWS portal.
' Created by        : Sushant Jena
' Created On        : 25-Jul-2021
' Modification History:

' <CR no.>              <Date>                <Modified by>        <Modification Summary>                                         <Instructed By>                                                   
   
'   *********************************************************************************************************************--%>

<%@ WebHandler Language="C#" Class="DownloadFileRepositoryNSWS" %>

using System;
using System.Web;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

public class DownloadFileRepositoryNSWS : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            /*------------------------------------------------------------------------------------------*/
            /////Get NSWS API URL address and API Keys from Web Configuration File.
            /*------------------------------------------------------------------------------------------*/
            string strPullDocApiUrl = ConfigurationManager.AppSettings["NswsPullDocApiUrl"].ToString();
            string strAccessId = ConfigurationManager.AppSettings["NswsApiAccessId"].ToString();
            string strAccessSecret = ConfigurationManager.AppSettings["NswsApiAccessSecret"].ToString();
            string strApiKeyPullDoc = ConfigurationManager.AppSettings["NswsApiKeyPullDoc"].ToString();

            /*------------------------------------------------------------------------------------------*/
            /////Get Query String value.
            /*------------------------------------------------------------------------------------------*/
            string strSWSId = context.Request.QueryString["swsId"];
            string strDocumentId = context.Request.QueryString["documentId"];
            string strFileControlId = context.Request.QueryString["fileControlId"];

            /*------------------------------------------------------------------------------------------*/
            /////Formulate File Content Id
            /*------------------------------------------------------------------------------------------*/
            string strFileContentId = strSWSId + "-" + strDocumentId + "-1";

            /*------------------------------------------------------------------------------------------*/
            /////Call Pull Document API to Get File.
            /*------------------------------------------------------------------------------------------*/
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            string strJson = "{\"contentId\":[\"" + strFileContentId + "\"]}";
            var client = new RestClient(strPullDocApiUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("access-id", strAccessId);
            request.AddHeader("access-secret", strAccessSecret);
            request.AddHeader("api-key", strApiKeyPullDoc);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", strJson, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            /*------------------------------------------------------------------------------------------*/
            ///// Write the request and response details in Log file.
            /*------------------------------------------------------------------------------------------*/
            string strFileResposnse = response.Content.Length > 200 ? response.Content.ToString().Substring(0, 200) : response.Content.ToString();
            Util.LogRequestResponse("NSWS", "DownloadDocument", "(REQUEST_FROM_GOSWIFT)--[REQUEST_JSON_STRING]:- " + strJson + " <----> " + "(RESPONSE_FROM_NSWS)--[RESPONSE_JSON_STRING]:- " + strFileResposnse);

            if (response.Content.ToString() != "")
            {
                PullApiDoc objApp = JsonConvert.DeserializeObject<PullApiDoc>(response.Content);
                string strStatus = objApp.status;
                if (strStatus == "200")
                {
                    List<DocResponseFile> objDocRes = new List<DocResponseFile>();
                    objDocRes = objApp.response.ToList();

                    string strFileName = objDocRes[0].fileName;
                    string strFileResponse = objDocRes[0].fileResponse; ////Byte stream of the file to be downloaded.

                    byte[] data = Convert.FromBase64String(strFileResponse);
                    if (data.Length > 0)
                    {
                        if (IsFileValid(data, strFileName))
                        {
                            /*------------------------------------------------------------------------------------------*/
                            ///// Rename the file as per the GOSWIFT naming format.
                            /*------------------------------------------------------------------------------------------*/
                            string strFileExtention = Path.GetExtension(strFileName);
                            strFileName = strFileControlId + "_" + string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + strFileExtention;

                            ////File path for GOSWIFT document folder.
                            string strPath = HttpContext.Current.Server.MapPath("Portal/Document/Upload");

                            /////Save the file to destination folder
                            FileStream fileStream = null;
                            if (!string.IsNullOrEmpty(strPath))
                            {
                                if (Directory.Exists(strPath))
                                {
                                    fileStream = new FileStream(strPath + "\\" + strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                    using (System.IO.FileStream fs = fileStream)
                                    {
                                        fs.Write(data, 0, data.Length);
                                        context.Response.Write(strFileName);
                                    }
                                }
                            }
                        }
                        else
                        {
                            context.Response.Write(3);
                            Util.LogRequestResponse("NSWS", "NSWSFileRepository", "Error Code-3:- Invalid or corrupted file found.");
                        }
                    }
                    else
                    {
                        context.Response.Write("1");
                        Util.LogRequestResponse("NSWS", "NSWSFileRepository", "Error Code-1:- No file found for download.");
                    }
                }
                else
                {
                    context.Response.Write("2");
                    Util.LogRequestResponse("NSWS", "NSWSFileRepository", "Error Code-2:- Not found (404 Error).");
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSFileRepository");
        }
    }

    public class PullApiDoc
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DocResponseFile> response { get; set; }
    }
    public class DocResponseFile
    {
        public string fileName { get; set; }
        public string fileResponse { get; set; }
    }


    private bool IsFileValid(byte[] file, string strFileName)
    {
        try
        {
            StringCollection imageTypes = new StringCollection();
            StringCollection imageExtension = new StringCollection();

            string[] allowedImageTypes = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtensions = { ".jpeg", ".jpg", ".png", ".pdf" };

            imageTypes.AddRange(allowedImageTypes);
            imageExtension.AddRange(allowedExtensions);

            string strFileType = MimeType.GetMimeType(file, strFileName);
            string strfileExt = Path.GetExtension(strFileName);
            int intDotCount = strFileName.Count(f => f == '.');

            if (imageTypes.Contains(strFileType.ToLower()) == true && imageExtension.Contains(strfileExt.ToLower()) && intDotCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}