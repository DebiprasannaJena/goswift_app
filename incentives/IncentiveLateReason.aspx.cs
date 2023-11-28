using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using BusinessLogicLayer.Proposal;
using EntityLayer.Incentive;
using EntityLayer.Proposal;
using System.Data;
using System.IO;
using System.Collections.Specialized;

public partial class incentives_IncentiveLateReason : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UnitCode"] == null)
        {
            Response.Redirect("incentiveoffered.aspx");
        }

        /*----------------------------------------------------------------------*/
        ///// For Allow Enter Key in Reason Field
        FilteredTxtExt_Dealy_Reason.ValidChars = FilteredTxtExt_Dealy_Reason.ValidChars + "\r\n";

        if (!IsPostBack)
        {
            fillDetails();
        }
    }

    //// Function Used
    #region FunctionUsed

    private void fillDetails()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strAction = "A";

            ds = objBAL.Inct_EC_Delay_Reason_VIEW(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Lbl_Industry_Code.Text = Convert.ToString(Session["UnitCode"]);
                Lbl_Enterprise_Name.Text = ds.Tables[0].Rows[0]["vchEnterpriseName"].ToString();
                Lbl_Unit_Category.Text = ds.Tables[0].Rows[0]["vchUnitCat"].ToString();
                Hid_Unit_Category.Value = ds.Tables[0].Rows[0]["intUnitCat"].ToString();
                Lbl_Unit_Type.Text = ds.Tables[0].Rows[0]["vchUnitType"].ToString();
                Hid_Unit_Type.Value = ds.Tables[0].Rows[0]["intUnitType"].ToString();

                /*----------------------------------------------------------------------------*/
                ///// Latest FFCI Date

                string strFFCIDate = Convert.ToString(ds.Tables[0].Rows[0]["dtmFFCIDateAfter"]);
                if (strFFCIDate != "")
                {
                    Lbl_FFCI_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strFFCIDate));
                }
                else
                {
                    Lbl_FFCI_Date.Text = "";
                }

                /*----------------------------------------------------------------------------*/
                ///// Latest Production Commencement Date

                string strProdCommDate = Convert.ToString(ds.Tables[0].Rows[0]["dtmProdCommAfter"]);
                if (strProdCommDate != "")
                {
                    Lbl_Prod_Comm_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strProdCommDate));
                }
                else
                {
                    Lbl_Prod_Comm_Date.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
        }
    }

    #endregion

    //// Document Upload Delete and View
    #region Document Upload Delete and View

    //// Upload Document Button Click
    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            if (FileUpload_Document.HasFile)
            {
                string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", "InctEcDelayDoc"));
                if (!Directory.Exists(strMainFolderPath))
                {
                    Directory.CreateDirectory(strMainFolderPath);
                }

                string filename = string.Empty;
                string strFileExt = Path.GetExtension(FileUpload_Document.FileName).ToLower();

                if (strFileExt != ".pdf" && strFileExt != ".zip" && strFileExt != ".doc" && strFileExt != ".docx" && strFileExt != ".xls" && strFileExt != ".xlsx")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload either .pdf/.zip/.doc/.docx/.xls/.xlsx file !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                if (!IsFileValid(FileUpload_Document))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) File name may contain dots !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                int fileSize = FileUpload_Document.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
                else
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ECDELAYDOC";
                    filename = strFileName + Path.GetExtension(FileUpload_Document.FileName);
                }

                FileUpload_Document.SaveAs(strMainFolderPath + filename);

                table.Columns.Add("vchDocDesc", typeof(string));
                table.Columns.Add("vchFileName", typeof(string));
                table.Rows.Add(Txt_Description.Text, filename);

                for (int i = 0; i < Grd_Files.Rows.Count; i++)
                {
                    Label Lbl_Doc_Desc = (Label)Grd_Files.Rows[i].FindControl("Lbl_Doc_Desc");
                    HiddenField Hid_File_Name = (HiddenField)Grd_Files.Rows[i].FindControl("Hid_File_Name");

                    table.Rows.Add(Lbl_Doc_Desc.Text, Hid_File_Name.Value);
                }

                Grd_Files.DataSource = table;
                Grd_Files.DataBind();

                ///// Clear Fields After Add
                Txt_Description.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload file !!</strong>', '" + strProjName + "'); </script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    //// Delete Document Button Click
    protected void ImgBtn_Delete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchDocDesc", typeof(string));
            table.Columns.Add("vchFileName", typeof(string));

            for (int i = 0; i < Grd_Files.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Doc_Desc = (Label)Grd_Files.Rows[i].FindControl("Lbl_Doc_Desc");
                    HiddenField Hid_File_Name = (HiddenField)Grd_Files.Rows[i].FindControl("Hid_File_Name");

                    table.Rows.Add(Lbl_Doc_Desc.Text, Hid_File_Name.Value);
                }
            }

            Grd_Files.DataSource = table;
            Grd_Files.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    //// Method to Check File MimeType
    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed", "application/msxls", "application/msword" };
        string[] allowedExtension = { ".pdf", ".zip", ".xls", ".xlsx", ".doc", ".docx" };
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
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    //// Submit Button Click
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();

        try
        {
            objEntity.strIndustryCode = Lbl_Industry_Code.Text == "" ? null : Lbl_Industry_Code.Text;
            objEntity.strEnterpriseName = Lbl_Enterprise_Name.Text == "" ? null : Lbl_Enterprise_Name.Text;
            objEntity.intUnitCat = Convert.ToInt32(Hid_Unit_Category.Value);
            objEntity.intUnitType = Convert.ToInt32(Hid_Unit_Type.Value);
            objEntity.strFFCIDate = Lbl_FFCI_Date.Text;
            objEntity.strProdCommDate = Lbl_Prod_Comm_Date.Text;
            objEntity.strDelayReason = Txt_Delay_Reason.Text == "" ? null : Txt_Delay_Reason.Text;
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);

            /*---------------------------------------------------------------------*/
            ////// Add Supporting Documents

            InctECDelayDoc objECDoc = new InctECDelayDoc();
            List<InctECDelayDoc> listECDoc = new List<InctECDelayDoc>();

            for (int i = 0; i < Grd_Files.Rows.Count; i++)
            {
                InctECDelayDoc objItem = new InctECDelayDoc();

                Label Lbl_Doc_Desc = (Label)Grd_Files.Rows[i].FindControl("Lbl_Doc_Desc");
                HiddenField Hid_File_Name = (HiddenField)Grd_Files.Rows[i].FindControl("Hid_File_Name");

                objItem.vchDocDesc = Lbl_Doc_Desc.Text;
                objItem.vchFileName = Hid_File_Name.Value;

                listECDoc.Add(objItem);
            }

            objEntity.ECDelayDoc = listECDoc;

            /*---------------------------------------------------------------*/
            /////// Data Insert

            string strReturnStatus = objBAL.Inct_EC_Delay_Reason_AED(objEntity);

            /*---------------------------------------------------------------*/

            string message = "";
            if (strReturnStatus == "1")
            {
                message = @"<strong>Record Saved Successfully !</strong>";
            }
            else if (strReturnStatus == "2")
            {
                message = @"<strong>Your Application is at Empowered Committee !</strong>";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "');</script>", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    //// Gridview RowDataBound
    protected void Grd_Files_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_File_Name = (HiddenField)e.Row.FindControl("Hid_File_Name");
            HyperLink Hyp_View_Doc = (HyperLink)e.Row.FindControl("Hyp_View_Doc");
            Hyp_View_Doc.NavigateUrl = "../incentives/Files/InctEcDelayDoc/" + Hid_File_Name.Value;
        }
    }
}