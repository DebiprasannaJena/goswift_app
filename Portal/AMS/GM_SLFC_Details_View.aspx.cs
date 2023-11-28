using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using EntityLayer.Service;

public partial class Portal_AMS_GM_SLFC_Details_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtComments.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',1000);", txtComments.ClientID, lblComments.ClientID));
            txtComments.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',1000);", txtComments.ClientID, lblComments.ClientID));
            FillGridview();
        }
    }

    private void FillGridview()
    {
        AMS_Search objSearch = new AMS_Search()
        {
            strActionCode = "slfc",
            intServiceId = Convert.ToInt32(Request.QueryString["cid"])
        };

        List<AMSNodalDetails> lstNodalDetails = new List<AMSNodalDetails>();
        lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);
        grdSLFC.DataSource = lstNodalDetails;
        grdSLFC.DataBind();
        lblProposalNo.Text = Request.QueryString["pNo"];
        if (lstNodalDetails.Count > 0)
        {
            lblInvestorName.Text = lstNodalDetails[0].strInvName;
            if (lstNodalDetails[0].intInvReplyStatus == 0)
            {
                // pnlmain.Enabled = false;
                fuQuery.Enabled = false;
                btnCancel.Visible = false;
                btnForward.Visible = false;
                btnForwardForAgenda.Visible = false;
                if (!string.IsNullOrEmpty(lstNodalDetails[0].strGmFileName))
                {
                    hypView.NavigateUrl = string.Format("~/Portal/AMS/Files/Query/{0}/{1}", Session["userId"], lstNodalDetails[0].strGmFileName);
                    hypView.Visible = true;
                    divFileUpload.Visible = false;
                }
                lblInvReply.Text = string.IsNullOrEmpty(lstNodalDetails[0].strInvResponse) ? "NA" : lstNodalDetails[0].strInvResponse;
                lblResponseDate.Text = string.IsNullOrEmpty(lstNodalDetails[0].strInvReplyDate) ? "NA" : lstNodalDetails[0].strInvReplyDate;
                txtComments.Text = lstNodalDetails[0].strGmComments;
            }
            else
            {
                StringBuilder strComments = new StringBuilder();
                for (int cnt =0; cnt < grdSLFC.Rows.Count; cnt++)
                {
                    GridViewRow gRow = grdSLFC.Rows[cnt];
                    Label lblQueries = (Label)gRow.FindControl("lblQueries");
                    HiddenField hdnNoQueries = (HiddenField)gRow.FindControl("hdnNoQueries");
                    if (!string.IsNullOrEmpty(hdnNoQueries.Value) && (Convert.ToBoolean(hdnNoQueries.Value) == false))
                    {
                        strComments.Append(lblQueries.Text.Trim());
                        strComments.Append(Environment.NewLine);
                    }
                }
                txtComments.Text = strComments.ToString();
            }
        }

        objSearch = new AMS_Search()
       {
           strActionCode = "doc",
           intServiceId = Convert.ToInt32(Request.QueryString["cid"])
       };
        lstNodalDetails = new List<AMSNodalDetails>();
        lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);


        rptDocuments.DataSource = lstNodalDetails;
        rptDocuments.DataBind();

    }

    protected void rptDocuments_DataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyp = (HyperLink)e.Item.FindControl("hypDocuments");
            hyp.NavigateUrl = string.Format("../../QueryFiles/{0}", hyp.Text);
        }
    }

    protected void grdSLFC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnNoQueries = (HiddenField)e.Row.FindControl("hdnNoQueries");
            Label lblQueries = (Label)e.Row.FindControl("lblQueries");
            HyperLink  hypFile = (HyperLink)e.Row.FindControl("hypFile");
            HiddenField hdnFile = (HiddenField)e.Row.FindControl("hdnFile");
            Boolean noQuery = Convert.ToBoolean(hdnNoQueries.Value);
            if (!noQuery)
            {
                if (!string.IsNullOrEmpty(hdnFile.Value))
                {
                    hypFile.NavigateUrl = string.Format("~/Portal/AMS/Files/Query/{0}/{1}", grdSLFC.DataKeys[e.Row.RowIndex].Value, hdnFile.Value);
                    hypFile.Visible = true;
                }
            }
            else if (noQuery)
            {
                lblQueries.Text = "No Queries";
                hypFile.Visible = false;
            }
        }
    }

    protected void btnForwardForAgenda_Click(object sender, EventArgs e)
    {
        AMSNodalDetails objProposalDet = new AMSNodalDetails()
        {
            strActionCode = "int",
            strProposalNo = lblProposalNo.Text,
            intCreatedBy = Convert.ToInt32(Session["userID"]),
            strGmComments = txtComments.Text.Trim()
        };

        int intRetValue = 0;
        intRetValue = AMSQueryServices.AMS_QueryManagement_AED(objProposalDet);

        if (intRetValue == 1)
        {
            StringBuilder sbUrl = new StringBuilder();
            sbUrl.Append("<script>alert('Agenda forwarded successfully!');");
            sbUrl.Append("window.location.href='GM_Details_View.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "&cid=" + Request.QueryString["cid"] + "&pNo=" + Request.QueryString["pNo"] + "&PageInd=" + Request.QueryString["PageInd"] + "&pSize=" + Request.QueryString["pSize"] + "'");
            sbUrl.Append("</script>");
            Response.Write(sbUrl.ToString());
        }
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        ProposalDet objProposal = new ProposalDet();
        objProposal.strAction = "Q";
        objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
        objProposal.strProposalNo = lblProposalNo.Text;
        objProposal.intStatus = 5;
        objProposal.strRemarks = txtComments.Text.Trim();
        string filePath = string.Empty;
        if (fuQuery.HasFile)
        {
            filePath = UploadQueryDocument();
        }
        objProposal.strFileName = filePath;
        ProposalBAL objProposalBal = new ProposalBAL();
        string strRetVal =objProposalBal.ProposalRaiseQuery(objProposal);
        if (strRetVal == "2")
        {
            CommonHelperCls comm = new CommonHelperCls();
            List<ProposalDet> objProposalList = new List<ProposalDet>();
            ProposalDet objProp = new ProposalDet();

            objProp.strAction = "S";
            objProp.strProposalNo = lblProposalNo.Text.Trim();
            objProp.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposalList = objProposalBal.getRaisedQueryDetails(objProp).ToList();
            string mobile = "";
            string smsContent = "";
            string strSubject = "";
            string strBody = "";
            string strDeptSMSSubject = "";
            string strDeptSMSBody = "";
            string strDeptMailSubject = "";
            string[] toEmail = new string[1];

            if (objProposalList.Count > 0)
            {
                mobile = Convert.ToString(objProposalList[0].MobileNo);
                smsContent = Convert.ToString(objProposalList[0].strSMSContent);
                toEmail[0] = Convert.ToString(objProposalList[0].EmailID);
                strSubject = Convert.ToString(objProposalList[0].EmailSubject);
                strBody = Convert.ToString(objProposalList[0].EmailBody);
                strDeptSMSSubject = Convert.ToString(objProposalList[0].strDeptSMSSub);
                strDeptSMSBody = Convert.ToString(objProposalList[0].strDeptSMSContent);
                strDeptMailSubject = Convert.ToString(objProposalList[0].strDeptMailContent);

                //   bool mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                //bool smsStatus = comm.SendSmsNew(mobile, smsContent);

                // FOR SMS and Mail Status Update
                //  string str = comm.UpdateMailSMSStaus("PEALQuery", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", 5, lblProposalNo.Text.Trim(), smsContent, strBody, mailStatus, smsStatus);
            }

            //For Sending SMS TO HOD
            objProp.strAction = "T";
            objProp.strProposalNo = lblProposalNo.Text.Trim();
            objProposalList = objProposalBal.getRaisedQueryDetails(objProp).ToList();

            if (objProposalList.Count > 0)
            {
                if (objProposalList[0].intNoOfTimes >= 2) // for fetching how many times query raised by dept
                {
                    ServiceDetails objServiceEntity = new ServiceDetails();
                    objServiceEntity.INT_SERVICEID = 500; //service id for peal is 500
                    objServiceEntity.strSubject = strDeptSMSSubject;
                    objServiceEntity.strBody = strDeptMailSubject;
                    objServiceEntity.smsContent = strDeptSMSBody;
                    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
                    //objDepartmntSms.DepartmentSendSms(objServiceEntity);
                }
            }

            AMSNodalDetails objNodalDetails = new AMSNodalDetails();
            objNodalDetails.strActionCode = "gm";
            objNodalDetails.strRemarks = txtComments.Text.Trim();
            objNodalDetails.intCreatedBy = Convert.ToInt32(Session["userid"]);
            objNodalDetails.intQueryConfigId = Convert.ToInt32(Request.QueryString["cid"]);
            objNodalDetails.strFileName = filePath;

            int intRetValue = AMSQueryServices.AMS_QueryManagement_AED(objNodalDetails);

            if (intRetValue == 1)
            {

                StringBuilder sbUrl = new StringBuilder();
                sbUrl.Append("<script>alert('" + Messages.ShowMessage(intRetValue.ToString()) + "');");
                sbUrl.Append("window.location.href='GM_Details_View.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "&cid=" + Request.QueryString["cid"] + "&pNo=" + Request.QueryString["pNo"] + "&PageInd=" + Request.QueryString["PageInd"] + "&pSize=" + Request.QueryString["pSize"] + "'");
                sbUrl.Append("</script>");
                Response.Write(sbUrl.ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage(intRetValue.ToString()) + "');", true);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();
        sbUrl.Append("<script>");
        sbUrl.Append("window.location.href='GM_Details_View.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "&cid=" + Request.QueryString["cid"] + "&pNo=" + Request.QueryString["pNo"] + "&PageInd=" + Request.QueryString["PageInd"] + "&pSize=" + Request.QueryString["pSize"] + "'");
        sbUrl.Append("</script>");
        Response.Write(sbUrl.ToString());
    }

    private DataTable CreateChildTable()
    {
        DataTable dtChild = new DataTable();
        dtChild.Columns.Add(new DataColumn("intUserId"));
        dtChild.Columns.Add(new DataColumn("vchFileName"));
        dtChild.Columns.Add(new DataColumn("vchComments"));
        dtChild.Columns.Add(new DataColumn("bitNoQuery"));
        return dtChild;
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
        else if (fileSize > (12 * 1024 * 1024))
        {
            str = "jAlert('<strong>File size is too large. Maximum file size permitted is 12 MB</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuQuery.ID + "').focus(); });";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
        }
        else if (!IncentiveCommonFunctions.IsFileValid(fuQuery, arrExtension))
        {
            str = "jAlert('<strong>Invalid file type (or) File name might contain dots</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuQuery.ID + "').focus(); });";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
        }
        else
        {
            string strMainFolderPath = Server.MapPath("~/QueryFiles/");
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            filename = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + lblProposalNo.Text.Trim() + "_Query1" + ".pdf", DateTime.Now);
            fuQuery.SaveAs(strMainFolderPath + filename);
        }
        return filename;
    }

    /// <summary>
    /// Function to rowspan a partcular column
    /// </summary>
    /// <param name="intCellIndex">index of the column to do rowspan</param>
    private void RowSpanGridview(int intCellIndex)
    {
        for (int cnt = grdSLFC.Rows.Count - 1; cnt > 0; cnt--)
        {
            GridViewRow objCurrRow = grdSLFC.Rows[cnt];
            GridViewRow objUpperRow = grdSLFC.Rows[cnt - 1];
            HyperLink hypProposalNoCurr = (HyperLink)objCurrRow.FindControl("hypProposalNo");
            HyperLink hypProposalNoUpper = (HyperLink)objUpperRow.FindControl("hypProposalNo");
            string strCurrEmp = hypProposalNoCurr.Text;
            string strUpperEmp = hypProposalNoUpper.Text;

            //merge the age column
            if (string.Equals(strCurrEmp, strUpperEmp, StringComparison.OrdinalIgnoreCase))
            {
                if (objUpperRow.Cells[intCellIndex].RowSpan == 0)
                {
                    if (objCurrRow.Cells[intCellIndex].RowSpan == 0)
                    {
                        objUpperRow.Cells[intCellIndex].RowSpan += 2;
                    }
                    else
                    {
                        objUpperRow.Cells[intCellIndex].RowSpan = objCurrRow.Cells[intCellIndex].RowSpan + 1;
                    }
                    objCurrRow.Cells[intCellIndex].Visible = false;
                }
            }
        }
    }
}