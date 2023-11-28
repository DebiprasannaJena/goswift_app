/*
 * File Name : GM_Details_View.aspx.cs
 * Class Name : Portal_AMS_GM_Details_View
 * Created On : 21st Feb 2018
 * Created By : Ritika Lath
 */

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Portal_AMS_GM_Details_View : System.Web.UI.Page
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

            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
    }

    private void BindGridView(int intPageIndex, int intPageSize)
    {
        grdQuery.DataSource = null;
        grdQuery.DataBind();
        AMS_Search objSearch= new AMS_Search()
        {
            strActionCode = "GM",
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
            RowSpanGridview(1);
            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstNodalDetails[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

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
        }
        else
        {
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
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
            HyperLink hypDetails = (HyperLink)e.Row.FindControl("hypDetails");

            StringBuilder strUrl = new StringBuilder();
            int ID = Convert.ToInt32(grdQuery.DataKeys[e.Row.RowIndex].Value);
            strUrl.Append("GM_SLFC_Details_View.aspx?pNo=");
            strUrl.Append(hypProposalNo.Text);
            strUrl.Append("&cid=");
            strUrl.Append(ID);
            hypDetails.NavigateUrl = strUrl.ToString();
        }
    }

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
        }
    }
}