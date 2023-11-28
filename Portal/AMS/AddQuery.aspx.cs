/*
 * File Name : AMS_RaisedQuery_View.aspx.cs
 * Class Name : Portal_AMS_AMS_RaisedQuery_View
 * Created On : 21st Feb 2018
 * Created By : Ritika Lath
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

public partial class Portal_AMS_AddQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                lblProposalNo.Text = Request.QueryString["Pno"];
                txtQuery.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',500);", txtQuery.ClientID, lblQuery.ClientID));
                txtQuery.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',500);", txtQuery.ClientID, lblQuery.ClientID));
                string strAction = Request.QueryString["Action"];
                if (strAction == "2") // user will view the details only
                {
                    ViewQueryDetails();
                }
            }
        }
        else
        {
            Response.Redirect("../SessionRedirect.aspx");
        }
    }

    private void ViewQueryDetails()
    {
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        txtQuery.Enabled = false;
        divFileUpload.Visible = false;
        chkNoQuery.Enabled = false;

        AMS_Search objSearch = new AMS_Search()
        {
            strActionCode = "e",
            intUserId = Convert.ToInt32(Session["userId"]),
            intServiceId = Convert.ToInt32(Request.QueryString["cid"])
        };

        List<AMSNodalDetails> lstNodalDetails = new List<AMSNodalDetails>();
        lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);

        if (lstNodalDetails != null && lstNodalDetails.Count > 0)
        {
            AMSNodalDetails objNodalDetails = new AMSNodalDetails();
            objNodalDetails = lstNodalDetails[0];
            if (objNodalDetails.BitNoQuery)
            {
                chkNoQuery.Checked = true;
                txtQuery.Text = string.Empty;
                txtQuery.Enabled = false;
            }
            else
            {
                txtQuery.Text = objNodalDetails.strRemarks;
                string strMainFolderPath = string.Format("~/Portal/AMS/Files/Query/{0}/{1}", Session["userId"], objNodalDetails.strFileName);
                hypView.NavigateUrl = strMainFolderPath;
                hypView.Visible = true;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AMSNodalDetails objNodalDetails = new AMSNodalDetails();
        if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
        {
            objNodalDetails.intQueryConfigId = Convert.ToInt32(Request.QueryString["cid"]);
            if (!chkNoQuery.Checked && string.IsNullOrEmpty(txtQuery.Text))
            {
                string str=string.Empty;
                str = "jAlert('<strong>Query cannot be blank</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + txtQuery.ID + "').focus(); });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else
            {

                objNodalDetails.strActionCode = "edit";
                if (!chkNoQuery.Checked)
                {
                    objNodalDetails.strRemarks = txtQuery.Text;

                    if (fuQuery.HasFile)
                    {
                        objNodalDetails.strFileName = UploadQueryDocument();
                    }
                    else
                    {
                        objNodalDetails.strFileName = string.Empty;
                    }
                }
                else
                {
                    objNodalDetails.strRemarks = string.Empty;
                    objNodalDetails.strFileName = string.Empty;
                }
                objNodalDetails.BitNoQuery = chkNoQuery.Checked;
                objNodalDetails.intCreatedBy = Convert.ToInt32(Session["userId"]);
                int intRetValue = 0;
                intRetValue = AMSQueryServices.AMS_QueryManagement_AED(objNodalDetails);
                if (intRetValue == 1)
                {
                    StringBuilder sbUrl = new StringBuilder();
                    sbUrl.Append("<script>alert('" + Messages.ShowMessage(intRetValue.ToString()) + "');");
                    sbUrl.Append("window.location.href='AMS_RaisedQuery_View.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "&PageInd=" + Request.QueryString["PageInd"] + "&pSize=" + Request.QueryString["pSize"] + "'");
                    sbUrl.Append("</script>");
                    Response.Write(sbUrl.ToString());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage(intRetValue.ToString()) + "');", true);
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();
        sbUrl.Append("<script>");
        sbUrl.Append("window.location.href='AMS_RaisedQuery_View.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "&PageInd=" + Request.QueryString["PageInd"] + "&pSize=" + Request.QueryString["pSize"] + "'");
        sbUrl.Append("</script>");
        Response.Write(sbUrl.ToString());
    }

    private string UploadQueryDocument()
    {
        string[] arrExtension = { ".pdf" };
        string filename = string.Empty;
        string fileExtension = Path.GetExtension(fuQuery.FileName);
        int fileSize = fuQuery.PostedFile.ContentLength;
        string str = string.Empty;
        if (!arrExtension.Contains(fileExtension))
        {
            str = "jAlert('<strong>Please Upload PDF file Only!</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuQuery.ID + "').focus(); });";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
        }
        else if (fileSize > (4 * 1024 * 1024))
        {
            str = "jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuQuery.ID + "').focus(); });";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
        }
        else if (!IncentiveCommonFunctions.IsFileValid(fuQuery, arrExtension))
        {
            str = "jAlert('<strong>Invalid file type (or) File name might contain dots</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuQuery.ID + "').focus(); });";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
        }
        else
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/Portal/AMS/Files/Query/{0}/", Session["userId"]));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            filename = string.Format("Query{0:_ddMMyyhhmmss}{1}", System.DateTime.Now, Path.GetExtension(fuQuery.FileName));
            fuQuery.SaveAs(strMainFolderPath + filename);
        }
        return filename;
    }
}