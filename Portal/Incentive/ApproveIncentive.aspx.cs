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

public partial class Portal_Incentive_ApproveIncentive : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    string str_Retvalue = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //lblUnitName.InnerText = Request.QueryString["UnitName"].ToString();
        //lblIncentive.InnerText = Request.QueryString["IncentiveNm"].ToString();
        ////lblAppNo.InnerText = Request.QueryString["AppNo"].ToString();
        //lblAppNo.InnerText = Request.QueryString["AppNumId"].ToString();

        if (!IsPostBack)
        {
            ////if (Request.QueryString["code"].ToString() == "10100110" && Request.QueryString["isprovisional"].ToString() == "1")
            ////{ 
            ////    dvSanctionUp.Visible = false;
            ////}
            ////else
            ////{ 
            ////    dvSanctionUp.Visible = true;
            ////}
            fillField();
        }
    }

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
                lblUnitName.InnerText = list[0].strUnitName; ////Request.QueryString["UnitName"].ToString();
                lblIncentive.InnerText = list[0].strInctName; //// Request.QueryString["IncentiveNm"].ToString();
                lblAppNo.InnerText = list[0].strApplicationNum; //// Request.QueryString["AppNumId"].ToString();
                hypView.NavigateUrl = "../../Incentives/" + list[0].strFormPreviewId + "?InctUniqueNo=" + Request.QueryString["UniqueID"].ToString();

                if (list[0].IsProvisional == 1 && (list[0].intAvailType == 4 || list[0].intAvailType == 5))
                {
                    hdnDocText.Value = "1";
                    DivStTypeText.InnerHtml = list[0].DocName;
                }

                hdnProvision.Value = list[0].IsProvisional.ToString();
                hdnAvailType.Value = list[0].intAvailType.ToString();
                HdnMobNo.Value = list[0].MOBILENo;
                HdnEmail.Value = list[0].EMAILId;
                hdndisbursetyp.Value = list[0].intDisburseType.ToString();
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
            Util.LogError(ex, "ApproveInct");
        }
    }
    #region "File Upload"

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
            //retval = true;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ApproveInct");
            // ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", ex.Message.ToString(), true);
        }
        return retval;
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

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
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

            objIncentive.strAction = "S";
            objIncentive.Remark = txtRemark.Text;
            objIncentive.INTINCUNQUEID = Convert.ToInt32(Request.QueryString["UniqueID"].ToString());//Request.QueryString["AppNo"]
            objIncentive.intStatus = Convert.ToInt32(ddlStatusPop.SelectedValue);

            if (ddlStatusPop.SelectedValue == "2")
            {
                apprSt = "Approved";
            }
            else if (ddlStatusPop.SelectedValue == "3")
            {
                apprSt = "Rejected";
            }

            objIncentive.IsProvisional = Convert.ToInt32(hdnProvision.Value);
            if (hdnProvision.Value == "1")
            {
                objIncentive.ProvisionalCertificate = FileUpload(fupSanctionDoc, "~/Portal/Incentive/Sanctionorder/", "ProvisionalDoc_");
                objIncentive.VCHPROVDOCCODE = "D250";
            }
            else
            {
                objIncentive.strSanFileName = FileUpload(fupSanctionDoc, "~/Portal/Incentive/Sanctionorder/", "SanctionOrder_");
                if (hdnAvailType.Value == "4")
                {
                    objIncentive.VCHSANCDOCCODE = "D249";
                }
                else if (hdnAvailType.Value == "5")
                {
                    objIncentive.VCHSANCDOCCODE = "D103";
                }
            }

            if (txtsanctionamount.Text.Trim() != "")
            {
                objIncentive.SanctionedAmount = Convert.ToDecimal(txtsanctionamount.Text.Trim());
            }
            else
            {
                objIncentive.SanctionedAmount = Convert.ToDecimal("0.00");
            }

            objIncentive.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            /////// DML Operation
            str_Retvalue = objLayer.Incentive_Approval(objIncentive);
            if (str_Retvalue == "1")
            {
                if (HdnMobNo.Value.ToString() != "")
                {
                    comm.SendSmsNew(HdnMobNo.Value.ToString(), "GO-Swift! Your application has been " + apprSt);
                }

                if (HdnEmail.Value.ToString() != "")
                {
                    string strSubject = "Go-Swift! Application Approval Status.";
                    string strBody = "Department has  " + apprSt + " the application for " + lblIncentive.InnerText + " of M/s " + lblUnitName.InnerText + "." + Environment.NewLine + "Log into www.swp.investodisha,gov.in for further details.";
                    string[] arramail = new string[] { HdnEmail.Value };
                    comm.sendMail(strSubject, strBody, arramail, true);
                }

                //ScriptManager.RegisterStartupScript(btnSave, this.GetType(), "Myalert", "alert('Action Taken Successfully');window.location.href='View_Inct_Application.aspx?ID=" + Request.QueryString["ID"].ToString() + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
                string qrystring = "?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> alertredirect('" + qrystring + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ApproveInct");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("View_Inct_Application.aspx?PIndex=" + Request.QueryString["PIndex"] + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
    }
}