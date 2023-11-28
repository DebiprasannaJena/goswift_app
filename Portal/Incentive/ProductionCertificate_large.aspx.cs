using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using EntityLayer.Proposal;
using System.Collections.Generic;
using BusinessLogicLayer.Proposal;
using System.IO;
using System.Web.UI;

public partial class Portal_Incentive_ProductionCertificate_large : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                getDist();
                GetApplicationDetails();
                txtDateOfIssue.Attributes.Add("readonly", "readonly");
                txtAmendedOn.Attributes.Add("readonly", "readonly");
                txtDatefchange.Attributes.Add("readonly", "readonly");
                txtProductDetails.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',200);", txtProductDetails.ClientID, lblProductDetails.ClientID));
                txtPlantAndMachinery.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',200);", txtPlantAndMachinery.ClientID, lblPlantAndMachinery.ClientID));
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "incentive");
            }
        }
    }

    /// <summary>
    /// Common function to upload signature for the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkUploadSignature_Click(object sender, EventArgs e)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/Approval/"));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            LinkButton lnk = (LinkButton)sender;
            if (lnk.ID == lnkOrgDocumentPdf.ID)
            {
                UploadDocument(fuOrgDocument, strMainFolderPath, hdnOrgDocument, lblOrgDocument, hypOrdDocument, lnkOrgDocumentDelete);
            }
            else if (lnk.ID == lnkSecondSignAdd.ID)
            {
                UploadDocument(fuSecondSignature, strMainFolderPath, hdnSecondSign, lblSecondSign, hypSecondSign, lnkSecondSignDel);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "incentive");
        }
    }

    /// <summary>
    /// Common function to delete the uploaded signature for the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkOrgDocumentDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/Approval/"));
            LinkButton lnk = (LinkButton)sender;
            if (lnk.ID == lnkOrgDocumentDelete.ID)
            {
                DeleteDocument(hdnOrgDocument, strMainFolderPath, lnkOrgDocumentDelete, lnkOrgDocumentPdf, hypOrdDocument, lblOrgDocument, fuOrgDocument);
            }
            else if (lnk.ID == lnkSecondSignDel.ID)
            {
                DeleteDocument(hdnSecondSign, strMainFolderPath, lnkSecondSignDel, lnkSecondSignAdd, hypSecondSign, lblSecondSign, fuSecondSignature);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "incentive");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CertificateDetails objMaster = new CertificateDetails()
            {
                intAppNo = Convert.ToInt32(Request.QueryString["id"]),
                strPlaceNew = txtPlaceNew.Text.Trim(),
                strDateNew = divAmendment.Visible == true ? txtAmendedOn.Value : txtDateOfIssue.Value,
                strFileNew = hdnOrgDocument.Value,
                strActionCode = "u",
                intCreatedBy = Convert.ToInt32(Session["userId"]),
                strPlaceAmd = txtPlaceAmd.Text,
                strDateAmd = txtAmendedOn.Value,
                strDateChangeCat = txtDatefchange.Value,
                strFileAmd = hdnSecondSign.Value,
                strIRSignature = lblSignatory.Text,
                strProdEmd = txtProductDetails.Text,
                strPlantEmd = txtPlantAndMachinery.Text
            };

            IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
            int intStatus = objBuisness.PcPrintDetailsLarge_AED(objMaster);

            //if record saved successfully
            if (intStatus == 2)
            {
                //redirect to print pc page create the pdf
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "Myalert", string.Format("alert('{0}');window.location.href='PCPrintLarge.aspx?ID={1}&linkm={2}&linkn={3}&btn={4}&tab={5}&ranNum={6}&pcupstat={7}&type=1';", Messages.ShowMessage("2"), Request.QueryString["ID"], Request.QueryString["linkm"], Request.QueryString["linkn"], Request.QueryString["btn"], Request.QueryString["tab"], Session["RandomNo"], 5), true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "incentive");
        }
    }

    /// <summary>
    /// Common function to delete the uploaded document
    /// </summary>
    /// <param name="hdnSignature">hidden field where filename is stored</param>
    /// <param name="strMainFolderPath">path of the folder where file is stored</param>
    /// <param name="lnkUploadSignatureDelete">button to delete the file</param>
    /// <param name="lnkUploadSignature">button to upload the file</param>
    /// <param name="hypUploadSignature">hyperlink to view the file for the user</param>
    /// <param name="lblUploadSignature">label to show if record uploaded successfully</param>
    /// <param name="fuSignature">file upload control</param>
    private void DeleteDocument(HiddenField hdnSignature, string strMainFolderPath, LinkButton lnkUploadSignatureDelete, LinkButton lnkUploadSignature, HyperLink hypUploadSignature, Label lblUploadSignature, FileUpload fuSignature)
    {

        string filename = hdnSignature.Value;
        string path = string.Format("~/incentives/Files/Approval/{0}", filename);
        string completePath = Server.MapPath(path);
        if (File.Exists(completePath))
        {
            File.Delete(completePath);
            hdnSignature.Value = string.Empty;
            lnkUploadSignatureDelete.Visible = false;
            lnkUploadSignature.Visible = true;
            hypUploadSignature.Visible = false;
            lblUploadSignature.Visible = false;
            lblUploadSignature.Text = string.Empty;
            fuSignature.Enabled = true;
        }
    }

    /// <summary>
    /// Common function to upload the document
    /// </summary>
    /// <param name="fuSignature">file upload control</param>
    /// <param name="strMainFolderPath">path of the folder where file is stored</param>
    /// <param name="hdnSignature">hidden field where filename is stored</param>
    /// <param name="lblUploadSignature">label to show if record uploaded successfully</param>
    /// <param name="hypUploadSignature">hyperlink to view the file for the user</param>
    /// <param name="lnkUploadSignatureDelete">button to delete the file</param>
    private void UploadDocument(FileUpload fuSignature, String strMainFolderPath, HiddenField hdnSignature, Label lblUploadSignature, HyperLink hypUploadSignature, LinkButton lnkUploadSignatureDelete)
    {
        string[] arrExtension = { ".jpg", ".jpeg", ".png" };

        if (fuSignature.HasFile)
        {
            string filename = string.Empty;
            string fileExtension = Path.GetExtension(fuSignature.FileName);
            int fileSize = fuSignature.PostedFile.ContentLength;
            string str = string.Empty;
            if (!arrExtension.Contains(fileExtension))
            {
                str = "jAlert('<strong>Please Upload PNG,JPG,JPEG file Only!</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuSignature.ID + "').focus(); });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (fileSize > (4 * 1024 * 1024))
            {
                str = "jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuSignature.ID + "').focus(); });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (!IncentiveCommonFunctions.IsFileValid(fuSignature, arrExtension))
            {
                str = "jAlert('<strong>Invalid file type (or) File name might contain dots</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuSignature.ID + "').focus(); });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else
            {
                System.Drawing.Image imgUploaded = System.Drawing.Image.FromStream(fuSignature.PostedFile.InputStream);
                if (imgUploaded.Height <= 200 && imgUploaded.Width <= 200)
                {
                    filename = string.Format("Signature{0:_ddMMyyhhmmss}{1}", System.DateTime.Now, Path.GetExtension(fuSignature.FileName));
                    fuSignature.SaveAs(strMainFolderPath + filename);
                    hdnSignature.Value = filename;
                    lblUploadSignature.Text = "Signature Uploaded successfully";
                    hypUploadSignature.NavigateUrl = strMainFolderPath + filename;
                    hypUploadSignature.Visible = true;
                    lnkUploadSignatureDelete.Visible = true;
                    fuSignature.Enabled = false;
                }
                else
                {
                    str = "jAlert('<strong>Height and width of signature image should not exceed 200px</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuSignature.ID + "').focus(); });";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
                }
            }
        }
    }

    //Common function to get the district for the 
    private void getDist()
    {
        DataSet dt = new DataSet();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        PcSearch objSearch = new PcSearch()
        {

            strActionCode = "Dist",
            intPageSize = 0,
            intPageIndex = 0,
            intAppFor = 0,
            vchAppNos = "",
            intDistId = "0",
            strUnitName = string.Empty,
            UserId = Convert.ToInt32(Session["userId"])
        };
        dt = objBuisnessLayer.Incentive_PcForm_View(objSearch);
        if (dt.Tables[0].Rows.Count > 0)
        {
            if (dt.Tables[0].Rows[0]["intDesignationId"].ToString() == "9")
            {
                lblSignatoryAmend.Text = "General Manager Regional Industries Centre At-" + dt.Tables[0].Rows[0]["levelname"].ToString();
                lblSignatory.Text = "General Manager Regional Industries Centre At-" + dt.Tables[0].Rows[0]["levelname"].ToString();

            }
            else if (dt.Tables[0].Rows[0]["intDesignationId"].ToString() == "10")
            {
                lblSignatoryAmend.Text = "General Manager District Industries Centre At-" + dt.Tables[0].Rows[0]["levelname"].ToString();
                lblSignatory.Text = "General Manager District Industries Centre At-" + dt.Tables[0].Rows[0]["levelname"].ToString();

            }

        }

    }

    /// <summary>
    /// row data bound event for the product gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    protected void grdChanges_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtChangeDetails = (TextBox)e.Row.FindControl("txtChangeDetails");
            Label lblChangeDetails = (Label)e.Row.FindControl("lblChangeDetails");
            txtChangeDetails.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',200);", txtChangeDetails.ClientID, lblChangeDetails.ClientID));
        }
    }



    ///// <summary>
    ///// Function to get all the details based on the application details called when editing
    ///// </summary>
    private void GetApplicationDetails()
    {
        divAmendment.Visible = false;
        divNewPc.Visible = false;
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
        if (objDs != null && objDs.Tables.Count > 0)
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
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                DataRow objRow = dtPcDetails.Rows[0];
                intApplicationType = Convert.ToInt32(objRow["vchAppFor"].ToString());
                lblAppNo.Text = objRow["vchPCNo"].ToString();
                lblUnit.Text = objRow["vchCompName"].ToString();
                lblEnterpriseAdd.Text = objRow["vchAddr"].ToString();
                lblOwner.Text = objRow["vchOwnerName"].ToString();
                lblFirstDate.Text = Convert.ToDateTime(objRow["dtmFFCI"].ToString()).ToString("dd-MMM-yyyy");
                lblOwnerType.Text = objRow["OWNER_TYPE"].ToString();
                lblCompanyType.Text = objRow["COMPANY_TYPE"].ToString();
                lblComType.Text = objRow["COMPANY_TYPE"].ToString();
                lblComType2.Text = string.Equals(objRow["COMPANY_TYPE"].ToString(), "Manufactured", StringComparison.OrdinalIgnoreCase) ? "Manufactured" : "Serviced";
                hdnLastIssue.Value = objRow["lastissuedt"].ToString();
                hdnProdDate.Value = objRow["dtmProdComm"].ToString();
                hdnInspectionReport.Value = objRow["dtmInspectionReport"].ToString();
                if (objRow["bitPlantModified"] != null && objRow["bitPlantModified"] != DBNull.Value)
                {
                    bitPlantModified = Convert.ToBoolean(objRow["bitPlantModified"].ToString());
                }
                if (objRow["bitprodModified"] != null && objRow["bitprodModified"] != DBNull.Value)
                {
                    bitProdModified = Convert.ToBoolean(objRow["bitprodModified"].ToString());
                }
                if (intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
                {
                    if ((dtChanges != null && dtChanges.Rows.Count > 0) || (bitPlantModified) || bitProdModified)
                    {
                        divAmendment.Visible = true;
                        divNewPc.Visible = false;
                        lblPcAmd.Text = objRow["vchPCNo"].ToString();
                    }
                    else
                    {
                        divAmendment.Visible = false;
                        divNewPc.Visible = true;
                    }
                }
                else
                {
                    divAmendment.Visible = false;
                    divNewPc.Visible = true;
                }

            }

            if (intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
            {
                if ((dtChanges != null && dtChanges.Rows.Count > 0) || (bitPlantModified) || bitProdModified)
                {
                    if (bitProdModified)
                    {
                        dtPcDetails = objDs.Tables[1];
                        if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                        {
                            divProductAmd.Visible = true;
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
                            divPlandAmd.Visible = true;
                        }
                    }
                    dtPcDetails = new DataTable();
                    dtPcDetails = objDs.Tables[3];
                    if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                    {
                        grdChanges.DataSource = dtPcDetails;
                        grdChanges.DataBind();
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
            }
        }
    }

    protected void rbtnChange_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnChange.SelectedValue == "2")
            {
                dvChange.Visible = true;
            }
            else
            {
                dvChange.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "incentive");
        }
    }
}