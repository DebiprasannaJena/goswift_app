using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Portal_Incentive_PCPrintLarge :SessionCheck
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                GetApplicationDetails();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "incentive");
            }
        }
    }

    private void GetApplicationDetails()
    {
        string strMainFile = string.Empty;
        trProductChanges.Visible = false;
        trPlantChanges.Visible = false;
        tblAmendement.Visible = false;
        tblNewPc.Visible = false;
        trChangeCategory.Visible = false;
        PcSearch objSearch = new PcSearch()
        {
            intAppFor = Convert.ToInt32(Request.QueryString["id"]),
            strActionCode = "PCCE",
            intPageIndex = 0,
            intPageSize = 0,
            strFromDate = string.Empty,
            strToDate = string.Empty
        };
        DataSet objDs = new DataSet();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objDs = objBuisnessLayer.Incentive_PcForm_Large_View(objSearch);
        if (objDs != null)
        {
            int intApplicationType = 0;
            Boolean bitProdModified = false, bitPlantModified = false;
            DataTable dtPcDetails = new DataTable();
            dtPcDetails = objDs.Tables[0];
            DataTable dtChanges = new DataTable();
            if (objDs.Tables.Count > 3)
            {
                dtChanges = objDs.Tables[3];
            }
            string strOldFilePath = string.Empty;
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                DataRow objRow = dtPcDetails.Rows[0];
                intApplicationType = Convert.ToInt32(objRow["vchAppFor"].ToString());
                if (objRow["bitPlantModified"] != null && objRow["bitPlantModified"] != DBNull.Value)
                {
                    bitPlantModified = Convert.ToBoolean(objRow["bitPlantModified"].ToString());
                }
                if (objRow["bitprodModified"] != null && objRow["bitprodModified"] != DBNull.Value)
                {
                    bitProdModified = Convert.ToBoolean(objRow["bitprodModified"].ToString());
                }
                lblUnit.Text = objRow["vchCompName"].ToString();
                lblEnterpriseAdd.Text = objRow["vchAddr"].ToString();
                lblOwner.Text = objRow["vchOwnerName"].ToString();
                lblFirstDate.Text = Convert.ToDateTime(objRow["dtmFFCI"].ToString()).ToString("dd-MMM-yyyy");
                lblOwnerType.Text = objRow["OWNER_TYPE"].ToString();
                lblCompanyType.Text = objRow["COMPANY_TYPE"].ToString();
                lblComType.Text = objRow["COMPANY_TYPE"].ToString();
                lblComType2.Text = string.Equals(objRow["COMPANY_TYPE"].ToString(), "Manufactured", StringComparison.OrdinalIgnoreCase) ? "Manufactured" : "Serviced";
                if ((intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod) && ((dtChanges != null && dtChanges.Rows.Count > 0) || bitPlantModified || bitProdModified))
                {
                    tblAmendement.Visible = true;
                    tblNewPc.Visible = false;
                    lblPcAmd.Text = objRow["vchPCNo"].ToString();
                    lblAmdDate.Text = objRow["dtmAmendedOn"].ToString();
                    lblCatChangeDate.Text = objRow["dtmChangeCategory"].ToString();
                    lblPlaceAmd.Text = objRow["vchPlaceAmd"].ToString();
                    if (string.IsNullOrEmpty(lblCatChangeDate.Text))
                    {
                        trChangeCategory.Visible = false;
                    }
                    else
                    {
                        trChangeCategory.Visible = true;
                    }
                    if (Request.QueryString["type"] != null)
                    {
                        imgSignatureAmd.ImageUrl = Server.MapPath(string.Format("~/incentives/Files/Approval/{0}", objRow["vchPcSignatureAmd"].ToString()));
                    }
                    else
                    {
                        imgSignatureAmd.ImageUrl = string.Format("~/incentives/Files/Approval/{0}", objRow["vchPcSignatureAmd"].ToString());

                    }
                    if (bitProdModified)
                    {
                        lblProductChanges.Text = objRow["vchProductAmdRemarks"].ToString();
                    }
                    if (bitPlantModified)
                    {
                        lblPlantChanges.Text = objRow["vchplantremarks"].ToString();
                    }
                }
                else
                {
                    tblAmendement.Visible = false;
                    tblNewPc.Visible = true;
                    lblAppNo.Text = objRow["vchPCNo"].ToString();
                    if (Request.QueryString["type"] != null)
                    {
                        imgSignature.ImageUrl = Server.MapPath(string.Format("~/incentives/Files/Approval/{0}", objRow["vchPcSignature"].ToString()));
                    }
                    else
                    {
                        imgSignature.ImageUrl = string.Format("~/incentives/Files/Approval/{0}", objRow["vchPcSignature"].ToString());
                    }
                    lblSignatory.Text = objRow["vchRISignature"].ToString();
                    lblSignatoryAmend.Text = objRow["vchRISignature"].ToString();
                    lblPlaceValue.Text = objRow["vchPlaceNew"].ToString();
                    lblPcIssuedate.Text = objRow["dtmIssueDate"].ToString();
                }
            }

            if ((intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod) && ((dtChanges != null && dtChanges.Rows.Count > 0) || bitPlantModified || bitProdModified))
            {
                if (bitProdModified)
                {
                    dtPcDetails = objDs.Tables[1];
                    if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                    {
                        trProductChanges.Visible = true;
                        grdProductAmd.DataSource = dtPcDetails;
                        grdProductAmd.DataBind();
                    }
                }
                if (bitPlantModified)
                {
                    dtPcDetails = new DataTable();
                    dtPcDetails = objDs.Tables[2];
                    if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                    {
                        grdPlantAmd.DataSource = dtPcDetails;
                        grdPlantAmd.DataBind();
                        trPlantChanges.Visible = true;
                    }
                }
                dtPcDetails = new DataTable();
                dtPcDetails = objDs.Tables[3];
                if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                {
                    grdChanges.DataSource = dtPcDetails;
                    grdChanges.DataBind();
                }

                if (Request.QueryString["type"] != null) //if redirected from generate pc page
                {
                    dtPcDetails = new DataTable();
                    dtPcDetails = objDs.Tables[4];
                    if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                    {
                        DataRow objRow = dtPcDetails.Rows[0];
                        strOldFilePath = objRow["vchPCFilePath"].ToString();
                        string strFileName = CreatePDF();
                        if (!string.IsNullOrEmpty(strOldFilePath) && !string.IsNullOrEmpty(strFileName))
                        {
                            string[] arrFiles = { Server.MapPath("~" + strOldFilePath), Server.MapPath("~" + strFileName) };
                            List<byte[]> lstBytes = new List<byte[]>();

                            for (int cnt = 0; cnt < arrFiles.Length; cnt++)
                            {
                                byte[] fileContent = null;
                                string strCurrFile = arrFiles[cnt];
                                System.IO.FileStream fs = new System.IO.FileStream(strCurrFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
                                long byteLength = new System.IO.FileInfo(strCurrFile).Length;
                                fileContent = binaryReader.ReadBytes((Int32)byteLength);
                                fs.Close();
                                fs.Dispose();
                                binaryReader.Close();
                                lstBytes.Add(fileContent);
                            }

                            string str = Server.MapPath("~" + strFileName);
                            FileStream objfs = new FileStream(str, FileMode.Create);
                            byte[] lst = MergeFiles(lstBytes);
                            objfs.Write(lst, 0, lst.Length);
                            strMainFile = strFileName;
                        }
                    }
                }
            }
            else
            {
                dtPcDetails = objDs.Tables[1];
                if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                {
                    grdProducts.DataSource = dtPcDetails;
                    grdProducts.DataBind();
                }

                dtPcDetails = new DataTable();
                dtPcDetails = objDs.Tables[2];
                if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                {
                    gvPlant.DataSource = dtPcDetails;
                    gvPlant.DataBind();
                }
                if (Request.QueryString["type"] != null) //if redirected from generate pc page
                {
                    strMainFile = CreatePDF();
                }
            }

        }

        if (Request.QueryString["type"] != null) //if redirected from generate pc page
        {
            //update the pdf file name 
            CertificateDetails objMaster = new CertificateDetails()
            {
                intAppNo = Convert.ToInt32(Request.QueryString["id"]),
                strActionCode = "f",
                intCreatedBy = Convert.ToInt32(Session["userId"]),
                strPdfName = strMainFile
            };

            IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
            int intStatus = objBuisness.PcPrintDetailsLarge_AED(objMaster);
            if (intStatus == 2)
            {
                Response.Redirect(string.Format("ViewIncentiveApplication.aspx?ID={0}&linkm={1}&linkn={2}&btn={3}&tab={4}&ranNum={5}&pcupstat={6}';", Request.QueryString["ID"], Request.QueryString["linkm"], Request.QueryString["linkn"], Request.QueryString["btn"], Request.QueryString["tab"], Session["RandomNo"], 5), false);
            }
            else
            {
                Response.Redirect(string.Format("ProductionCertificate_large.aspx?ID={0}&linkm={1}&linkn={2}&btn={3}&tab={4}&ranNum={5}&pcupstat={6}';", Request.QueryString["ID"], Request.QueryString["linkm"], Request.QueryString["linkn"], Request.QueryString["btn"], Request.QueryString["tab"], Session["RandomNo"], 5), false);
            }
        }
    }

    private void BindProductGridview(DataTable dtView)
    {
        grdProducts.DataSource = dtView;
        grdProducts.DataBind();
        int totalCnt = grdProducts.Rows.Count;

    }

    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (lblCompanyType.Text == "Manufacturing")
            {
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;

            }
            else
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = true;

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (lblCompanyType.Text == "Manufacturing")
            {
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;

            }
            else
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = true;
            }
        }
    }

    public string CreatePDF()
    {

        StringBuilder strPdfPath = new StringBuilder(Server.MapPath(string.Format("~/incentives/Files/PC/")));
        StringBuilder strFileName = new StringBuilder();
        Document Doc = new Document(PageSize.A4, 30f, 30f, 30f, 0f);
        try
        {
            //create file path and if folder is not existing create them
            if (!Directory.Exists(strPdfPath.ToString()))
            {
                Directory.CreateDirectory(strPdfPath.ToString());
            }
            if (tblAmendement.Visible)
            {
                strFileName.Append(lblPcAmd.Text);
            }
            else if (tblNewPc.Visible)
            {
                strFileName.Append(lblAppNo.Text);
            }
            strFileName.Append(DateTime.Now.ToString("_ddMMyyhhmmss"));
            strFileName.Append(".pdf");
            strPdfPath.Append(strFileName.ToString());

            //create pdf
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            divPdf.RenderControl(htmlTextWriter);

            StringReader stringReader = new StringReader(stringWriter.ToString());
            HTMLWorker htmlparser = new HTMLWorker(Doc);
            PdfWriter.GetInstance(Doc, new FileStream(strPdfPath.ToString(), FileMode.OpenOrCreate));
            Doc.Open();
            htmlparser.Parse(stringReader);
            Doc.Close();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (Doc != null && Doc.IsOpen())
                Doc.Close();
            Context.ApplicationInstance.CompleteRequest();
        }
        return string.Format("/incentives/Files/PC/{0}", strFileName.ToString());
    }

    public static byte[] MergeFiles(List<byte[]> sourceFiles)
    {
        Document document = new Document();
        using (MemoryStream ms = new MemoryStream())
        {
            PdfCopy copy = new PdfCopy(document, ms);
            document.Open();
            int documentPageCounter = 0;

            // Iterate through all pdf documents
            for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
            {
                // Create pdf reader
                PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                int numberOfPages = reader.NumberOfPages;

                // Iterate through all pages
                for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                {
                    documentPageCounter++;
                    PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                    PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

                    //// Write header
                    //ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                    //    new Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30,
                    //    importedPage.Width < importedPage.Height ? 0 : 1);

                    //// Write footer
                    //ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                    //    new Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30,
                    //    importedPage.Width < importedPage.Height ? 0 : 1);

                    pageStamp.AlterContents();

                    copy.AddPage(importedPage);
                }

                copy.FreeReader(reader);
                reader.Close();
            }

            document.Close();

            return ms.GetBuffer();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}