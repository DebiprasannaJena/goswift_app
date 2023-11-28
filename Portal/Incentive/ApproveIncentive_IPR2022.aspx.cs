using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Globalization;
using System.Data;
using System.Collections.Specialized;

public partial class Portal_Incentive_ApproveIncentive_IPR2022 : System.Web.UI.Page
{
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    string str_Retvalue = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblUnitName.InnerText = Request.QueryString["UnitName"].ToString();
        lblIncentive.InnerText = Request.QueryString["IncentiveName"].ToString();
        lblAppNo.InnerText = Request.QueryString["ApplicationNo"].ToString();
        String Id= Request.QueryString["FormId"].ToString();
        hypView.NavigateUrl = "../../Incentives/" + Id +"?InctUniqueNo=" + Request.QueryString["UniqueID"].ToString();


    }

    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf",
                                      "application/x-zip-compressed",
                                      "application/msxls",
                                      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                      "application/msword",
                                      "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                    };
        string[] allowedExtension = { ".pdf", ".xls", ".xlsx", ".doc", ".docx" };
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

    private string FileUpload(FileUpload fupUpload, string FilePath, string FileName)
    {
        string retval = "";
        try
        {
            var directory = new DirectoryInfo(FilePath);
            if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePath)) == false)
            {
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(FilePath));
            }

            string file = System.Web.HttpContext.Current.Server.MapPath(FilePath) + FileName + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fupUpload.FileName);
            retval = FileName + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fupUpload.FileName);
            string files = System.Web.HttpContext.Current.Server.MapPath(FilePath) + file;

            if (File.Exists(file))
            {
                File.Delete(file);
            }

            fupUpload.SaveAs(file);
           

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ApproveThrustorPriorityIPR2022");
            
        }
        return retval;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CommonHelperCls comm = new CommonHelperCls();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Inct_Application_Details_Entity objIncentive = new Inct_Application_Details_Entity();
        string apprSt = "";
        try
        {
            #region Validation

            if (ddlStatusPop.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select status !</strong>','" + strProjName + "'); </script>", false);
                ddlStatusPop.Focus();
                return;
            }

            if (fupSanctionDoc.HasFile)
            {
                if (!IsFileValid(fupSanctionDoc))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) File name may contain dots !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload document !</strong>','" + strProjName + "'); </script>", false);
                fupSanctionDoc.Focus();
                return;
            }

            if (txtRemark.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please enter remark !</strong>','" + strProjName + "'); </script>", false);
                txtRemark.Focus();
                return;
            }

            if (txtRemark.Text.Trim().Length > 250)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Maximum characters allowed for remark is 250 !</strong>','" + strProjName + "'); </script>", false);
                txtRemark.Focus();
                return;
            }

            #endregion

            /*----------------------------------------------------------------*/

            objIncentive.strAction = "TS";
            objIncentive.Remark = txtRemark.Text;
            objIncentive.INTINCUNQUEID = Convert.ToInt32(Request.QueryString["UniqueID"].ToString());//Request.QueryString["AppNo"]
            objIncentive.intStatus = Convert.ToInt32(ddlStatusPop.SelectedValue);
            objIncentive.intSectorStatus= Convert.ToInt32(Ddl_Sector.SelectedValue);
            objIncentive.strSanFileName = FileUpload(fupSanctionDoc, "~/Portal/Incentive/Sanctionorder/", "ScanDoc_");
            if (ddlStatusPop.SelectedValue == "2")
            {
                apprSt = "Approved";
            }
            else if (ddlStatusPop.SelectedValue == "3")
            {
                apprSt = "Rejected";
            }
            else if (ddlStatusPop.SelectedValue == "4")
            {
                apprSt = "Pending";
            }
            else if (ddlStatusPop.SelectedValue == "8")
            {
                apprSt = "Forwarded";
            }
            objIncentive.intActionTakenBy = Convert.ToInt32(Session["UserId"].ToString());

            /////// DML Operation
            str_Retvalue = objLayer.IPR2022Incentive_Approval(objIncentive);
            if (str_Retvalue == "1")
            {
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './View_Inct_Application_IPR2022.aspx';}); </script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ApproveThrustorPriorityIPR2022");
        }
    }
}