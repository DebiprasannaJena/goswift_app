using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Globalization;

public partial class Portal_CMS_ContactusView : System.Web.UI.Page
{
    #region Variable Declaration

    string Str_Retvalue = "";
    int retval = 0;
    string fileNM = "";
    CMSDetails obj = new CMSDetails();
    List<CMSDetails> newlist = new List<CMSDetails>();
    CmsBusinesslayer objbusiness = new CmsBusinesslayer();
    int intOutput = 0, gIntretval = 0;
    string strShowMsg = string.Empty;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
            TxtFromDate.Attributes.Add("ReadOnly", "ReadOnly");
            TxtToDate.Attributes.Add("ReadOnly", "ReadOnly");

            try
            {
                FillGrid();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ViewConatctUs");
            }
        }
    }

    #region FillGrid
    protected void FillGrid()
    {
        try
        {
            string strFromDate = string.Empty;
            string strToDate = string.Empty;
            int intMonth = DateTime.Today.Month;
            if (intMonth == 1)
            {
                strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            CMSDetails obj = new CMSDetails()
            {
                actioncode = "CMSCUV",
                Strusername = Convert.ToString(TxtName.Text.Trim()),
                Strmail = Convert.ToString(txtEmail.Text.Trim()),
                Strcompanyname = Convert.ToString(txtCompanyName.Text.Trim()),
                Strmobileno = Convert.ToString(TxtPhoneNumber.Text.Trim()),
                StrFromDate = string.IsNullOrEmpty(TxtFromDate.Text.Trim()) ? strFromDate : TxtFromDate.Text.Trim(),
                StrToDate = string.IsNullOrEmpty(TxtToDate.Text.Trim()) ? strToDate : TxtToDate.Text.Trim(),
            };
            newlist = objbusiness.ContactusDetails(obj);

            GrdViewData.DataSource = newlist;
            GrdViewData.DataBind();

            retval = newlist.Count();

            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewConatctUs");
        }
    }
    #endregion

    private void ClearFilter()
    {
        txtCompanyName.Text = "";
        txtEmail.Text = "";
        TxtName.Text = "";
        TxtPhoneNumber.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
    }
    #region Labal button CLick
    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (LbtnAll.Text == "ALL")
            {
                LbtnAll.Text = "Paging";
                GrdViewData.PageIndex = 0;
                GrdViewData.AllowPaging = false;
                FillGrid();
            }
            else
            {
                LbtnAll.Text = "ALL";
                GrdViewData.AllowPaging = true;
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewConatctUs");
        }
    }
    #endregion
    #region Paging
    private void DisplayPaging()
    {
        try
        {
            if (GrdViewData.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                LbtnAll.Visible = true;
                if (GrdViewData.PageIndex + 1 == GrdViewData.PageCount)
                {
                    this.lblPaging.Text = "Results" + " <b>" + GrdViewData.Rows[0].Cells[0].Text + "</b> - <b>" + retval + "</b> Of <b>" + retval + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results" + " <b>" + GrdViewData.Rows[0].Cells[0].Text + "</b> - <b>" + Convert.ToInt32(Convert.ToInt32(GrdViewData.Rows[0].Cells[0].Text) + Convert.ToInt32((GrdViewData.PageSize - 1))) + "</b> Of <b>" + retval + "</b>";
                }
            }
            else
            {
                this.lblPaging.Visible = false;
                LbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewConatctUs");
        }
    }
    #endregion
    #region Deleting Method

    #endregion
    #region Page Event Chnging
    protected void GrdViewData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewData.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewConatctUs");
        }
    }
    #endregion
    protected void GrdViewData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GrdViewData.PageIndex * this.GrdViewData.PageSize) + e.Row.RowIndex + 1);
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validation

            if (TxtFromDate.Text.Trim() != "" && TxtToDate.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter To date.</strong>');", true);
                TxtToDate.Focus();
                return;
            }
            if (TxtFromDate.Text.Trim() == "" && TxtToDate.Text.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter From date.</strong>');", true);
                TxtFromDate.Focus();
                return;
            }
            if (TxtFromDate.Text.Trim() != "" && TxtToDate.Text.Trim() != "")
            {
                if (Convert.ToDateTime(TxtFromDate.Text.Trim()) > Convert.ToDateTime(TxtToDate.Text.Trim()))
                {
                    TxtFromDate.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Holiday From date should not be greater than holiday To date.</strong>');", true);
                    return;
                }
            }

            #endregion

            FillGrid();

            if (GrdViewData.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                LbtnAll.Visible = true;
            }
            else
            {
                this.lblPaging.Visible = false;
                LbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewConatctUs");
        }
    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {
        ClearFilter();
    }
}