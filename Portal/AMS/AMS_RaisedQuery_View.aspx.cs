/*
 * File Name : AMS_RaisedQuery_View.aspx.cs
 * Class Name : Portal_AMS_AMS_RaisedQuery_View
 * Created On : 20th Feb 2018
 * Created By : Ritika Lath
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;

public partial class Portal_AMS_AMS_RaisedQuery_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonFunctions.PopulatePageSize(ddlNoOfRec);
            hdnPgindex.Value = "1";
            if (!string.IsNullOrEmpty(Request.QueryString["hdn"]))
            {
                hdnPgindex.Value = Request.QueryString["hdn"];
            }
            else
            {
                hdnPgindex.Value = "1";
            }
            if (Request.QueryString["pSize"] != null)
            {
                ddlNoOfRec.SelectedValue = Request.QueryString["pSize"];
            }
            else
            {
                ddlNoOfRec.SelectedValue = "10";
            }
            txtQuery.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',500);", txtQuery.ClientID, lblQuery.ClientID));
            txtQuery.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',500);", txtQuery.ClientID, lblQuery.ClientID));
            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
    }

    private void BindGridView(int intPageIndex, int intPageSize)
    {
        grdQuery.DataSource = null;
        grdQuery.DataBind();
        AMS_Search objSearch= new AMS_Search()
        {
            strActionCode = "view",
            intUserId = Convert.ToInt32(Session["userid"]),
            intPageSize = intPageSize,
            intIntPageIndex = intPageIndex
        };
        List<AMSNodalDetails> lstNodalDetails = new List<AMSNodalDetails>();
        lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);

        grdQuery.DataSource = lstNodalDetails;
        grdQuery.DataBind();
        if (grdQuery.Rows.Count > 0)
        {
            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstNodalDetails[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

            if (lstNodalDetails[0].intUserType != 1) //for SLFC officers and other officers
            {
                grdQuery.Columns[7].Visible = false;
            }
            else
            {
                grdQuery.Columns[8].Visible = false;
            }

            /****************code to show paging details in the label************/
            int intPIndex = Convert.ToInt32(hdnPgindex.Value);
            int intStartIndex = 1, intEndIndex = 0;
            int intPSize = Convert.ToInt32(ddlNoOfRec.SelectedValue);
            intStartIndex = ((intPIndex - 1) * intPSize) + 1;
            if (intPSize == grdQuery.Rows.Count)
            {
                intEndIndex = intPSize * intPIndex;
            }
            else
            {
                intEndIndex = grdQuery.Rows.Count + (intPSize * (intPIndex - 1));

            }
            lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(lstNodalDetails[0].intRowCount).ToString();

            //row span for the second column which shows the sector details
            RowSpanGridview(1);
        }
        else
        {
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
        }
    }

    #region Popup
    protected void grdSLFC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFile = (HiddenField)e.Row.FindControl("hdnFile");
            HyperLink hypFile= (HyperLink)e.Row.FindControl("hypFile");
            int intId = Convert.ToInt32(grdSLFC.DataKeys[e.Row.RowIndex].Value);
            if (!string.IsNullOrEmpty(hdnFile.Value))
            {
                string strMainFolderPath = string.Format("~/Portal/AMS/Files/Query/{0}/{1}", intId, hdnFile.Value);
                hypFile.NavigateUrl = strMainFolderPath;
                hypFile.Visible = true;
            }
            HiddenField hdnQuery = (HiddenField)e.Row.FindControl("hdnQuery");
            Boolean bitQuery = Convert.ToBoolean(hdnQuery.Value);
            Label lblRemarks =(Label)e.Row.FindControl("lblRemarks");
            if (bitQuery)
            {
                lblRemarks.Text = "No Queries";
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AMSNodalDetails objNodalDetails = new AMSNodalDetails();

        objNodalDetails.intQueryConfigId = Convert.ToInt32(hdnQueryConfigId.Value);
        if (!chkNoQuery.Checked && string.IsNullOrEmpty(txtQuery.Text))
        {
            string str=string.Empty;
            str = "alert('Query cannot be blank');";
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage(intRetValue.ToString()) + "');", true);
                BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
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

    private void ViewQueryDetails(int intKey)
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
            intServiceId = intKey
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

    protected void rptDocuments_DataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HyperLink hyp = (HyperLink)e.Item.FindControl("hypDocuments");
            hyp.NavigateUrl = string.Format("../../QueryFiles/{0}", hyp.Text);
        }
    }
    #endregion

    #region Data Paging
    /// <summary>
    /// Click event for all the link button created for the paging control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = (string)((sender as LinkButton).CommandArgument);
            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// selected index change event for the dropdown that contains different size for the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNoOfRec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = "1";
            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion

    protected void grdQuery_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strCommandName = e.CommandName;
        int intIndex =Convert.ToInt32(e.CommandArgument);

        LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button
        GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
        GridView myGrid = (GridView)sender; // the gridview

        int intKey = Convert.ToInt32(grdQuery.DataKeys[myRow.RowIndex].Value);
        HyperLink hypProposalNo = (HyperLink)myRow.FindControl("hypProposalNo");
        HiddenField hdnIsPassed = (HiddenField)myRow.FindControl("hdnIsPassed");
        HiddenField hdnIsUpdated = (HiddenField)myRow.FindControl("hdnIsUpdated");
        hdnQueryConfigId.Value = intKey.ToString();
        int intAction = 0;
        //if the date for adding comments has not passed
        if (!string.IsNullOrEmpty(hdnIsPassed.Value) && hdnIsPassed.Value == "0")
        {
            //check if the user has already updated the details
            if (!string.IsNullOrEmpty(hdnIsUpdated.Value) && hdnIsUpdated.Value == "0")
            {
                intAction = 1;

            }
            else
            {
                intAction = 2;
            }
        }

        //if the date has passed 
        else
        {
            //if the user has entered any data show the details
            if (!string.IsNullOrEmpty(hdnIsUpdated.Value) && hdnIsUpdated.Value == "0")
            {
                intAction = 2;
            }
        }
        if (intAction == 2)
        {
            ViewQueryDetails(intKey);
        }
        else
        {
            btnSubmit.Visible = true;
            btnCancel.Visible = true;
            txtQuery.Enabled = true;
            divFileUpload.Visible = true;
            chkNoQuery.Enabled = true;
            txtQuery.Text = string.Empty;
            fuQuery.Enabled = true;
            chkNoQuery.Checked = false;
            hypView.Visible = false;
            hypView.NavigateUrl = string.Empty;
        }

        if (string.Equals(e.CommandName, "slfc", StringComparison.OrdinalIgnoreCase))
        {
            AMS_Search objSearch = new AMS_Search()
            {
                strActionCode = "nod",
                intServiceId = intKey,
                intUserId = Convert.ToInt32(Session["userid"])
            };

            List<AMSNodalDetails> lstNodalDetails = new List<AMSNodalDetails>();
            lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);
            grdSLFC.DataSource = lstNodalDetails;
            grdSLFC.DataBind();
            lblProposalNo.Text = hypProposalNo.Text;
            if (lstNodalDetails.Count > 0)
            {
                lblInvReply.Text = string.IsNullOrEmpty(lstNodalDetails[0].strInvResponse) ? "NA" : lstNodalDetails[0].strInvResponse;
                lblResponseDate.Text = string.IsNullOrEmpty(lstNodalDetails[0].strInvReplyDate) ? "NA" : lstNodalDetails[0].strInvReplyDate;
                lblGMComments.Text = string.IsNullOrEmpty(lstNodalDetails[0].strGmComments) ? "NA" : lstNodalDetails[0].strGmComments;
            }

            objSearch = new AMS_Search()
            {
                strActionCode = "doc",
                intServiceId = intKey,
                intUserId = Convert.ToInt32(Session["userid"])
            };
            lstNodalDetails = new List<AMSNodalDetails>();
            lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);
            rptDocuments.DataSource = lstNodalDetails;
            rptDocuments.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ExModal();", true);
        }
        else if (string.Equals(e.CommandName, "INV", StringComparison.OrdinalIgnoreCase))
        {
            AMS_Search objSearch = new AMS_Search()
            {
                strActionCode = "sd",
                intServiceId = intKey,
                intUserId = Convert.ToInt32(Session["userid"])
            };
            h4SLFCComments.Visible = false;
            List<AMSNodalDetails> lstNodalDetails = new List<AMSNodalDetails>();
            lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);
            lblProposalNo.Text = hypProposalNo.Text;
            if (lstNodalDetails.Count > 0)
            {
                lblInvReply.Text = string.IsNullOrEmpty(lstNodalDetails[0].strInvResponse) ? "NA" : lstNodalDetails[0].strInvResponse;
                lblResponseDate.Text = string.IsNullOrEmpty(lstNodalDetails[0].strInvReplyDate) ? "NA" : lstNodalDetails[0].strInvReplyDate;
                lblGMComments.Text = string.IsNullOrEmpty(lstNodalDetails[0].strGmComments) ? "NA" : lstNodalDetails[0].strGmComments;
            }

            objSearch = new AMS_Search()
            {
                strActionCode = "doc",
                intServiceId = intKey,
                intUserId = Convert.ToInt32(Session["userid"])
            };
            lstNodalDetails = new List<AMSNodalDetails>();
            lstNodalDetails = AMSQueryServices.AMS_QueryManagement_View(objSearch);
            rptDocuments.DataSource = lstNodalDetails;
            rptDocuments.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ExModal();", true);
        }
    }

    protected void grdQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Rowid = 0;
            if (Convert.ToInt32(hdnPgindex.Value) > 1)
            {
                Rowid = (Convert.ToInt32(hdnPgindex.Value) - 1) * Convert.ToInt32(ddlNoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
            }
            else
            {
                Rowid = e.Row.DataItemIndex + 1;
            }
            e.Row.Cells[0].Text = Rowid.ToString();

            HyperLink hypProposalNo = (HyperLink)e.Row.FindControl("hypProposalNo");
            hypProposalNo.NavigateUrl = string.Format("../Proposal/ProposalDetails.aspx?Pno={0}", hypProposalNo.Text);

            HiddenField hdnIsPassed = (HiddenField)e.Row.FindControl("hdnIsPassed");
            HiddenField hdnIsUpdated = (HiddenField)e.Row.FindControl("hdnIsUpdated");
            LinkButton lnkSLFCDetails =(LinkButton)e.Row.FindControl("lnkSLFCDetails");
            LinkButton lnkInvestorDetails =(LinkButton)e.Row.FindControl("lnkInvestorDetails");

            //if the date for adding comments has not passed
            if (!string.IsNullOrEmpty(hdnIsPassed.Value) && hdnIsPassed.Value == "0")
            {
                //check if the user has already updated the details
                if (!string.IsNullOrEmpty(hdnIsUpdated.Value) && hdnIsUpdated.Value == "0")
                {
                    lnkSLFCDetails.Text = "Add Query";
                    lnkInvestorDetails.Text = "Add Query";
                }
                else
                {
                    lnkSLFCDetails.Text = "View Query";
                    lnkInvestorDetails.Text = "View Query";
                }
            }

            //if the date has passed 
            else
            {
                //if the user has entered any data show the details
                if (!string.IsNullOrEmpty(hdnIsUpdated.Value) && hdnIsUpdated.Value == "0")
                {
                    lnkSLFCDetails.Text = "View Query";
                    lnkInvestorDetails.Text = "View Query";
                }
            }
        }
    }

    /// <summary>
    /// Function to rowspan a partcular column
    /// </summary>
    /// <param name="intCellIndex">index of the column to do rowspan</param>
    private void RowSpanGridview(int intCellIndex)
    {
        for (int cnt = grdQuery.Rows.Count - 1; cnt > 0; cnt--)
        {
            GridViewRow objCurrRow = grdQuery.Rows[cnt];
            GridViewRow objUpperRow = grdQuery.Rows[cnt - 1];
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