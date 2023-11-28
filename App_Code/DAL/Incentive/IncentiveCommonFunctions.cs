/*********************************************
 * File name : IncentiveCommonFunctions.cs
 * Class name : IncentiveCommonFunctions
 * Created On : 21st Nov 2017
 * Created by : Dolagovinda Acharya
 * Description : Added to keep all the common functions related to incentive in one place
 ************************************************/

using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using EntityLayer.Incentive;
using System.Collections.Generic;
using DataAcessLayer.Incentive;
using Ionic.Zip;
using System.Globalization;

/// <summary>
/// static class for all common function for incentive
/// </summary>
public static class IncentiveCommonFunctions
{
    public enum enUnitCategory
    {
        MICRO = 36,
        SMALL = 37,
        MEDIUM = 38,
        LARGE = 39
    }


    /// <summary>
    /// Function to determine the unitCategory of a unit based on the company type and invesment for plant and machinery
    /// </summary>
    /// <param name="strCompanyType">company type</param>
    /// <param name="decInvestment">Investment for plant and machinery</param>
    /// <returns>unit category type</returns>
    public static string GetUnitCategory(string strCompanyType, decimal decInvestment)
    {
        string unittype = string.Empty;

        //for manufacturing
        if (strCompanyType == "40")
        {
            if (decInvestment <= 25)
                unittype = "MICRO";
            else if (decInvestment > 25 && decInvestment <= 500)
                unittype = "SMALL";
            else if (decInvestment > 500 && decInvestment <= 1000)
                unittype = "MEDIUM";
            else if (decInvestment > 1000)
                unittype = "LARGE";
        }

        //for servicing
        else if (strCompanyType == "41")
        {
            if (decInvestment <= 10)
                unittype = "MICRO";
            else if (decInvestment > 10 && decInvestment <= 200)
                unittype = "SMALL";
            else if (decInvestment > 200 && decInvestment <= 500)
                unittype = "MEDIUM";
            else if (decInvestment > 500)
                unittype = "LARGE";
        }
        return unittype;
    }

    /// <summary>
    /// Function to get the unitcategory id according to the company type and investment
    /// </summary>
    /// <param name="strCompanyType">Company type</param>
    /// <param name="decInvestment">Investment details</param>
    /// <returns>unit category id</returns>
    public static int GetUnitCategoryId(string strCompanyType, decimal decInvestment)
    {
        int unittype = 0;

        //for manufacturing
        if (strCompanyType == "40")
        {
            if (decInvestment <= 25)
                unittype = (int)enUnitCategory.MICRO;
            else if (decInvestment > 25 && decInvestment <= 500)
                unittype = (int)enUnitCategory.SMALL;
            else if (decInvestment > 500 && decInvestment <= 1000)
                unittype = (int)enUnitCategory.MEDIUM;
            else if (decInvestment > 1000)
                unittype = (int)enUnitCategory.LARGE;
        }

        //for servicing
        else if (strCompanyType == "41")
        {
            if (decInvestment <= 10)
                unittype = (int)enUnitCategory.MICRO;
            else if (decInvestment > 10 && decInvestment <= 200)
                unittype = (int)enUnitCategory.SMALL;
            else if (decInvestment > 200 && decInvestment <= 500)
                unittype = (int)enUnitCategory.MEDIUM;
            else if (decInvestment > 500)
                unittype = (int)enUnitCategory.LARGE;
        }
        return unittype;
    }

    /// <summary>
    /// Function to check if the file mime type is same as per the extensions allowed in the page
    /// </summary>
    /// <param name="FileUpload1">File upload control</param>
    /// <param name="arrAllowedExtension">types of extensions allowed in the page</param>
    /// <returns>Boolean as in true and false</returns>
    public static bool IsFileValid(FileUpload FileUpload1, string[] arrAllowedExtension)
    {
        string[] arrAllowedMime = new string[arrAllowedExtension.Length];
        for (int cnt = 0; cnt < arrAllowedExtension.Length; cnt++)
        {
            arrAllowedMime[cnt] = GetMimeTypeByFileExtension(arrAllowedExtension[cnt]);
        }
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        imageTypes.AddRange(arrAllowedMime);
        imageExtension.AddRange(arrAllowedExtension);
        string filename = System.IO.Path.GetFileName(FileUpload1.FileName);

        //to calculate dots
        int count = filename.Count(f => f == '.');
        string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);

        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);// 

        if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Check if the file name is containing any dots
    /// </summary>
    /// <param name="objFileUpload">FileUpload controls</param>
    /// <returns>boolean value</returns>
    public static Boolean isFilNameHavingDots(FileUpload objFileUpload)
    {
        string filename = System.IO.Path.GetFileNameWithoutExtension(objFileUpload.FileName);

        //to calculate dots
        int count = filename.Count(f => f == '.');
        if (count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Function to get the mime type of file by extension
    /// </summary>
    /// <param name="strExtension">File extension with .</param>
    /// <returns>mime type with string format</returns>
    private static string GetMimeTypeByFileExtension(string strExtension)
    {
        string strMimeType = string.Empty;
        switch (strExtension.ToUpper())
        {
            case ".PDF":
                strMimeType = "application/pdf";
                break;
            case ".PNG":
                strMimeType = "image/png";
                break;
            case ".JPG":
                strMimeType = "image/jpeg";
                break;
            case ".JPEG":
                strMimeType = "image/jpeg";
                break;
            case ".ZIP":
                strMimeType = "application/x-zip-compressed";
                break;
            case ".RAR":
                strMimeType = "application/x-rar-compressed";
                break;
            case ".DOC":
                strMimeType = "application/msword";
                break;
            case ".DOCX":
                strMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                break;
            case ".XLSX":
                strMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                break;
        }
        return strMimeType;
    }

    #region "Export Grid to Excel"
    /// <summary>
    /// Function for exporting grid to excel.
    /// </summary>
    /// <param name="strFileName">File name of the excel sheet</param>
    /// <param name="grd">Gridview object</param>
    /// <param name="title">title at the start of the excel</param>
    /// <param name="details">Details if any</param>
    /// <param name="year">Year when excel was created</param>
    /// <param name="check">whether to check if there are any controls and if present to remove them from excel sheet</param>
    /// <returns>html form which provides excel sheet for download</returns>
    public static HtmlForm ExportToExcel(string strFileName, Control grd, string title = null, string details = null, string year = null, bool check = false)
    {
        string attachment = "attachment; filename=" + strFileName + ".xls";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        if (check)
        {
            RemoveControls(grd);
        }
        // Create a form to contain the grid
        HtmlForm frm = new HtmlForm();
        grd.Parent.Controls.Add(frm);
        //frm.Attributes("runat") = "server";
        frm.Controls.Add(grd);
        frm.RenderControl(htw);

        StringBuilder sbContent = new StringBuilder();
        sbContent.Append("<font color='#0000FF' size='4' font-style='bold'>");
        sbContent.Append(title);
        sbContent.Append("</font>");
        sbContent.Append("<font color='#0000FF'>");
        sbContent.Append("<br>");
        sbContent.Append(details);
        sbContent.Append("<br>");
        if (!string.IsNullOrEmpty(year))
        {
            sbContent.Append("Year : " + year);
        }
        sbContent.Append("</font>");
        HttpContext.Current.Response.Write(sbContent);
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
        return frm;
    }

    /// <summary>
    /// Function to remove the controls from the gridview
    /// </summary>
    /// <param name="grdReport">Gridview to remove the controls</param>
    private static void RemoveControls(Control grdReport)
    {
        Literal literal = new Literal();
        for (int i = 0; i <= grdReport.Controls.Count - 1; i++)
        {
            if (grdReport.Controls[i] is HyperLink)
            {
                literal.Text = (grdReport.Controls[i] as HyperLink).Text;
                Boolean isVisible = (grdReport.Controls[i] as HyperLink).Visible;
                grdReport.Controls.Remove(grdReport.Controls[i]);
                if (isVisible)
                {
                    grdReport.Controls.AddAt(i, literal);
                }
            }
            else if (grdReport.Controls[i] is LinkButton)
            {
                literal.Text = (grdReport.Controls[i] as LinkButton).Text;
                Boolean isVisible = (grdReport.Controls[i] as LinkButton).Visible;
                grdReport.Controls.Remove(grdReport.Controls[i]);
                if (isVisible)
                {
                    grdReport.Controls.AddAt(i, literal);
                }

            }
            if (grdReport.Controls[i].HasControls())
            {
                RemoveControls(grdReport.Controls[i]);
            }
        }
    }
    #endregion

    /// <summary>
    /// Function to create PDF File from the control
    /// </summary>
    /// <param name="strFilename"></param>
    /// <param name="grd"></param>
    public static void CreatePdf(string strFilename, Control grd)
    {
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFilename + ".pdf");
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        grd.RenderControl(htmlTextWriter);

        StringReader stringReader = new StringReader(stringWriter.ToString());
        Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(Doc);
        PdfWriter.GetInstance(Doc, HttpContext.Current.Response.OutputStream);

        Doc.Open();
        htmlparser.Parse(stringReader);
        Doc.Close();
        HttpContext.Current.Response.Output.Write(stringWriter);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    public static string GetCertificateDetailsFromService(enServiceDocType aEnServiceDocType, List<string> lstFilePath, int aIntIvestorId)
    {
        string strTempZipFolderPath = string.Empty;
        string strPrefix = string.Empty;
        if (aEnServiceDocType == enServiceDocType.OSPCB)
        {
            strPrefix = "OSPCB";
        }
        else if (aEnServiceDocType == enServiceDocType.Boiler)
        {
            strPrefix = "FactoryBoiler";
        }

        //if there are files from service only then follow the procedure
        if (lstFilePath.Count > 0)
        {
            //then create a new folder OSPCB_Temp, it will contain all the temp files for any investor that are created
            StringBuilder sbTempOspcbFolderPath = new StringBuilder();
            sbTempOspcbFolderPath.Append(HttpContext.Current.Server.MapPath("~/incentives/Files/"));
            sbTempOspcbFolderPath.Append(strPrefix);
            sbTempOspcbFolderPath.Append("_Temp/");

            if (!Directory.Exists(sbTempOspcbFolderPath.ToString()))
            {
                Directory.CreateDirectory(sbTempOspcbFolderPath.ToString());
            }

            //Check if there is already a folder existing for this investor or not
            StringBuilder sbTempOspcbInvFolderPath = new StringBuilder();
            sbTempOspcbInvFolderPath.Append(sbTempOspcbFolderPath.ToString());
            sbTempOspcbInvFolderPath.Append("/");
            sbTempOspcbInvFolderPath.Append(aIntIvestorId.ToString());

            if (!Directory.Exists(sbTempOspcbInvFolderPath.ToString()))
            {
                Directory.CreateDirectory(sbTempOspcbInvFolderPath.ToString());
            }
            else // if existing then delete all the old files
            {
                //if a folder is already existing then delete the old one
                string[] arrDelFiles = Directory.GetFiles(sbTempOspcbInvFolderPath.ToString());
                if (arrDelFiles != null)
                {
                    for (int cnt=0; cnt < arrDelFiles.Count(); cnt++)
                    {
                        if (File.Exists(arrDelFiles[cnt]))
                        {
                            File.Delete(arrDelFiles[cnt]);
                        }
                    }
                }

                //also delete the old zip file
                StringBuilder sbTempOspcbInvZipFolderPath = new StringBuilder();
                sbTempOspcbInvZipFolderPath.Append(sbTempOspcbInvFolderPath);
                sbTempOspcbInvZipFolderPath.Append(".zip");
                if (File.Exists(sbTempOspcbInvZipFolderPath.ToString()))
                {
                    File.Delete(sbTempOspcbInvZipFolderPath.ToString());
                }
            }

            //then copy all the files from service folder where the files are stored to this folder
            for (int cnt = 0; cnt < lstFilePath.Count; cnt++)
            {
                string strSourFile = HttpContext.Current.Server.MapPath(lstFilePath[cnt]);
                if (!string.IsNullOrEmpty(strSourFile) && File.Exists(strSourFile))
                {
                    string strExtension = Path.GetExtension(strSourFile);
                    string strDestFile = string.Format("{0}\\{1}_{2}{3}{4}", sbTempOspcbInvFolderPath.ToString(), strPrefix, (cnt + 1), DateTime.Now.ToString("_ddMMyyhhmmss"), strExtension);
                    File.Copy(strSourFile, strDestFile, true);
                }
            }

            //create zip folder 
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName(aIntIvestorId.ToString());
                string [] lst = Directory.GetFiles(sbTempOspcbInvFolderPath.ToString());
                if (lst != null)
                {
                    for (int i = 0; i <= lst.Count() - 1; i++)
                    {
                        string FileName = lst[i];
                        zip.AddFile(FileName, aIntIvestorId.ToString());
                        zip.Save(string.Format("{0}/{1}.zip", sbTempOspcbFolderPath.ToString(), aIntIvestorId.ToString()));
                    }

                    //complete path starting with ~/string.Format("
                    strTempZipFolderPath = string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPrefix, aIntIvestorId.ToString());
                }

            }
        }

        return strTempZipFolderPath;
    }


    public static string FormatString(string strVal)
    {
        try
        {
            CultureInfo CInfo = new CultureInfo("hi-IN");
            strVal = Convert.ToInt64(strVal).ToString("N", CInfo);
            strVal = strVal.Replace(".00", "");
            return strVal;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string FormatDecimalString(string strVal)
    {
        try
        {
            CultureInfo CInfo = new CultureInfo("hi-IN");
            strVal = Convert.ToDecimal(strVal).ToString("N", CInfo);
            return strVal;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}