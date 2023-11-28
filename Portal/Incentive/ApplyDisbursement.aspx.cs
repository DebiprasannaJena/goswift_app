using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

public partial class Portal_Incentive_ApplyDisbursement : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    string str_Retvalue = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillField();
        }
    }

    #region FunctionUsed

    //// Upload File
    private void fillField()
    {
        try
        {
            IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
            Inct_Application_Details_Entity objBU_Entity = new Inct_Application_Details_Entity();

            objBU_Entity.strAction = "A";
            objBU_Entity.INTINCUNQUEID = Convert.ToInt16(Request.QueryString["UniqueID"].ToString());

            IList<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
            list = ObjIMB.View_Application_ApprveFetch(objBU_Entity);
            if (list.Count > 0)
            {
                lblUnitName.InnerText = list[0].strUnitName;
                lblIncentive.InnerText = list[0].strInctName.ToString();
                lblAppNo.InnerText = list[0].strApplicationNum.ToString();
                hypView.NavigateUrl = "../../Incentives/" + list[0].strFormPreviewId + "?InctUniqueNo=" + Request.QueryString["UniqueID"].ToString();
                hnkSanctionDoc.NavigateUrl = "../../Portal/Incentive/Sanctionorder/" + list[0].strSanFileName;
                hdnEmail.Value = list[0].EMAILId.ToString();
                hdnMobile.Value = list[0].MOBILENo.ToString();
                lblRemarks.InnerText = list[0].Remark;
                lblsanctionedamt.Text = list[0].SanctionedAmount.ToString();
            }
            else
            {
                lblUnitName.InnerText = "--";
                lblIncentive.InnerText = "--";
                lblAppNo.InnerText = "--";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DisburseInct");
        }
    }

    //// Method to Check File MimeType
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

    //// Upload File
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

            retval = FileName + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fupUpload.FileName);
            string file = System.Web.HttpContext.Current.Server.MapPath(FilePath) + retval;
            string files = System.Web.HttpContext.Current.Server.MapPath(FilePath) + file;
            if (File.Exists(files))
            {
                File.Delete(files);
            }

            fupUpload.SaveAs(file);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", ex.Message.ToString(), true);
        }
        return retval;
    }

    //// Send SMS and Email
    private void SMSEmailContent()
    {
        try
        {
            CommonHelperCls objcomm = new CommonHelperCls();
            string strSubject = "GO-SWIFT: Application Disbursed successfully";
            string strBody = "Department has disbursed the application for" + lblIncentive.InnerText + " of M/s " + lblUnitName.InnerText + ". Log into www.swp.investodisha.gov.in for further details.";
            string SMSContent = strSubject;

            var myList = new List<string>();
            myList.Add(hdnEmail.Value.ToString());
            string[] tomail = myList.ToArray();

            bool mailStatus = objcomm.sendMail(strSubject, strBody, tomail, true);
            bool smsStatus = objcomm.SendSmsNew(hdnMobile.Value, SMSContent);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DisburseInct");
        }
    }

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Inct_Application_Details_Entity objIncentive = new Inct_Application_Details_Entity();
        try
        {
            objIncentive.strAction = "D";
            objIncentive.INTINCUNQUEID = Convert.ToInt32(Request.QueryString["UniqueID"].ToString());//Request.QueryString["AppNo"]
            objIncentive.DisburseNo = Convert.ToInt64(txtDisbursementNo.Text);
            objIncentive.DisburseAmount = Convert.ToDecimal(txtAmount.Text);
            objIncentive.DisburseDate = txtDate.Text;
            objIncentive.DisburseTime = txtTime.Text;
            objIncentive.BankName = txtBank.Text;
            objIncentive.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            if (fupDisburseDoc.HasFile)
            {
                if (!IsFileValid(fupDisburseDoc))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) File name may contain dots !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
                else
                {
                    objIncentive.DisbursementDocument = FileUpload(fupDisburseDoc, "~/Portal/Incentive/Disbursement/", "DisbursementDoc_");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload disbursement document !</strong>','" + strProjName + "'); </script>", false);
                fupDisburseDoc.Focus();
                return;
            }

            ///// DML Operation
            str_Retvalue = objLayer.Incentive_Approval(objIncentive);
            if (str_Retvalue == "1")
            {
                SMSEmailContent();
                string qrystring = "?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> alertredirect('" + qrystring + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DisburseInct");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("View_Inct_Application.aspx?PIndex=" + Request.QueryString["PIndex"] + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
    }
}