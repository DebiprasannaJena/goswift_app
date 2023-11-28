#region  PageInfo
//******************************************************************************************************************
// File Name             :   IndustryList.aspx.cs
// Description           :   Get Industry List Based On PAN Number, Unit Name and Status
// Created by            :   Satyaprakash
// Created On            :   16-04-2019
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Data;
using System.Configuration;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;

public partial class Portal_MISReport_IndustryList : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();

    int intRetVal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        if ((Txt_Unit_Name.Text == "") && (Txt_PAN.Text == "") && (drpStatusDet.SelectedIndex == 0))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select At Least One Field')", true);
        }
        else
        {
            fillGrid();
        }
    }

    private void fillGrid()
    {
        DataTable dt = new DataTable();
        try
        {
            objProposal.strAction = "D1";
            objProposal.strPanno = Txt_PAN.Text.Trim();
            objProposal.strIndName = Txt_Unit_Name.Text.Trim();
            objProposal.IntApprovalStatus = Convert.ToInt32(drpStatusDet.SelectedValue);
            dt = objService.IndustryListDetails(objProposal);
            GrdIndustryList.DataSource = dt;
            GrdIndustryList.DataBind();
            intRetVal = dt.Rows.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustryList");
        }
        finally
        {
            objProposal = null;
        }
    }
    private void DisplayPaging()
    {
        if (GrdIndustryList.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (GrdIndustryList.PageIndex + 1 == GrdIndustryList.PageCount)
            {
                lblPaging.Text = "Results <b>" + ((Label)GrdIndustryList.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdIndustryList.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)GrdIndustryList.Rows[0].FindControl("lblsl")).Text) + GrdIndustryList.PageSize - 1) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            GrdIndustryList.PageIndex = 0;
            GrdIndustryList.AllowPaging = false;
            fillGrid();
        }
        else
        {
            lbtnAll.Text = "All";
            GrdIndustryList.AllowPaging = true;
            fillGrid();
        }
    }

    protected void GrdIndustryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdIndustryList.PageIndex = e.NewPageIndex;
        fillGrid();
    }
    protected void GrdIndustryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label las = e.Row.FindControl("Lbl_Approval_Status") as Label;

                if (las.Text == "Pending")
                {
                    las.ForeColor = System.Drawing.Color.Blue;
                }
                else if (las.Text == "Approved")
                {
                    las.ForeColor = System.Drawing.Color.Green;
                }
                else if (las.Text == "Rejected")
                {
                    las.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustryList");
        }
    }

    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Txt_Unit_Name.Text = "";
        Txt_PAN.Text = "";
        drpStatusDet.SelectedIndex = 0;
    }
    protected void LnkBtn_Inv_Name_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");

            Response.Redirect("ViewMISIndustryList.aspx?val=" + Hid_Investor_Id.Value, false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustryList");
        }
    }
}