#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   viewFeedback.aspx.cs
// Description           :   View Feedback
// Created by            :   Sanghamitra Samal
// Created On            :   05 Sep 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Common;
using EntityLayer.Common;
#endregion

public partial class Miscellaneous_viewFeedback : SessionCheck
{
    #region Variable Declaration
    CommonBusinessLayer objService = new CommonBusinessLayer();

    int intcount = 0;
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
            TxtFromDate.Attributes.Add("ReadOnly", "ReadOnly");
            TxtToDate.Attributes.Add("ReadOnly", "ReadOnly");
            BindGridview();
        }

    }
    #endregion

    #region Common Function

    public void BindGridview()
    {
        List<Feedback> objList = new List<Feedback>();
        try
        {

            /*---------------------------------------------------------------*/
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

            /*---------------------------------------------------------------*/

            Feedback objServiceEntity = new Feedback()
            {
                strAction = "V",
                StrFullName = TxtName.Text.Trim(),
                vchEmail = txtEmail.Text.Trim(),
                StrFromDate = string.IsNullOrEmpty(TxtFromDate.Text.Trim()) ? strFromDate : TxtFromDate.Text.Trim(),
                StrToDate = string.IsNullOrEmpty(TxtToDate.Text.Trim()) ? strToDate : TxtToDate.Text.Trim(),
                vchMobileNo = TxtPhoneNumber.Text.Trim(),
                vchSubject = TxtSubject.Text.Trim(),
            };

            objList = objService.ViewFeedback(objServiceEntity).ToList();
            grdFeedback.DataSource = objList;
            intcount = objList.Count;
            grdFeedback.DataBind();
            DisplayPaging(intcount);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewFeedBack");
        }
        finally
        {
            objList = null;
        }
    }

    private void ClearFeild()
    {
        txtEmail.Text = "";
        TxtName.Text = "";
        TxtPhoneNumber.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
    }

    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (LbtnAll.Text == "All")
            {
                LbtnAll.Text = "Paging";
                grdFeedback.PageIndex = 0;
                grdFeedback.AllowPaging = false;
                BindGridview();
            }
            else
            {
                LbtnAll.Text = "All";
                grdFeedback.AllowPaging = true;
                BindGridview();
                DisplayPaging(intcount);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewFeedBack");
        }
    }

    #region "Display Paging"
    protected void DisplayPaging(int intRecCount)//Disply Paging of Gridview
    {
        try
        {
            if (grdFeedback.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                grdFeedback.Visible = true;
                if (grdFeedback.PageIndex + 1 == grdFeedback.PageCount)
                {
                    this.lblPaging.Text = "Results " + "<b>" + (Convert.ToInt32((grdFeedback.PageIndex * grdFeedback.PageSize)) + 1) + "</b> - <b>" + intRecCount + " " + "of" + " " + intcount + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results " + "<b>" + (Convert.ToInt32((grdFeedback.PageIndex * grdFeedback.PageSize)) + 1) + "</b> - <b>" + ((grdFeedback.PageIndex + 1) * grdFeedback.PageSize) + " " + "of" + " " + intcount + "</b>";
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
            Util.LogError(ex, "ViewFeedBack");
        }
    }
    #endregion
    #endregion

    #region GridView_Events
    protected void grdFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFeedback.PageIndex = e.NewPageIndex;
        BindGridview();
    }
    #endregion


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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong> Holiday From date should not be greater than holiday To date.</strong>');", true);
                    return;
                }
            }

            #endregion

            BindGridview();
            if (grdFeedback.Rows.Count > 0)
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
            Util.LogError(ex, "ViewFeedBack");
        }
    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {
        ClearFeild();
    }
}