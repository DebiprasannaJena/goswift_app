
/*
 * Created By : Suman Lata Gupta
 * Created On : 16th November 2017
 * Description : to show the incentive details of empowere committee filled by the user to the admin
 * Class name : Portal_Incentive_ViewEMPCommitteApproval
 * file name : ViewEMPCommitteApproval.aspx.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Globalization;
using System.IO;
using Ionic.Zip;
using System.Web.UI.HtmlControls;
using EntityLayer.Service;
using Common;
using System.Data;

public partial class Portal_Incentive_ViewEMPCommitteApproval : SessionCheck
{
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();

    private int pageSize = 10;
    private int tableDataCount
    {
        get
        {
            return ViewState["PAGE.SIZE"] == null ? 1 : int.Parse(ViewState["PAGE.SIZE"].ToString());
        }
        set { ViewState["PAGE.SIZE"] = value; }
    }
    string str_Retvalue = string.Empty;
    public int pageIndex
    {
        get
        {
            return ViewState["pageIndex"] == null ? 1 : int.Parse(ViewState["pageIndex"].ToString());
        }
        set { ViewState["pageIndex"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CommonHelper.GetPostBackControlId(this, "LnkBtn_View_Application"))
        {
            if (Grd_Application.Rows.Count > 0)
            {
                uclPager.AddPageLinks(tableDataCount, pageSize, pageIndex);
            }
        }
        else
        {
            CallPaging();

        }
        pageSize = Int32.Parse(ddlSize.SelectedValue);



        if (Session["UserId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {

            fillGrid();
        }
    }

    private void fillGrid()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.strAction = "B";
            objEntity.strEnterpriseName = Txt_EntName.Text.Trim();
            objEntity.intStatus = DrpDwn_Status.SelectedIndex == 0 ? 0 : int.Parse(DrpDwn_Status.SelectedValue);

            ds = objBAL.Inct_EC_Delay_Reason_VIEW(objEntity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Grd_Application.DataSource = ds.Tables[0];
                Grd_Application.DataBind();
                ViewState["PAGE.SIZE"] = ds.Tables[0].Rows.Count;

                hdnCurrentIndex.Value = pageIndex.ToString(CultureInfo.InvariantCulture);
                //uclPager.AddPageLinks(int.Parse( ds.Tables[0].Rows[0]["intTotalCount"].ToString()), pageSize, pageIndex);
                //ShowPageIndex(pageIndex, pageSize, int.Parse(ds.Tables[0].Rows[0]["intTotalCount"].ToString()));
                //ViewState["PAGE.SIZE"] = int.Parse(ds.Tables[0].Rows[0]["intTotalCount"].ToString());
                //divPaging.Visible = true;
                //uclPager.Visible = true;
                //divPagingShow.Visible = true;
            }
            else
            {
                Grd_Application.DataSource = null;
                Grd_Application.DataBind();
                //divPaging.Visible = false;
                //uclPager.Visible = false;
                //divPagingShow.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
        }



    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        ViewState["pageIndex"] = 1;
        fillGrid();
    }

    #region "For Paging"

    private void CallPaging()
    {
        try
        {
            this.uclPager.PaginationLinkClicked += new EventHandler(paginationLink_Click);
            string defaultInitialValueForHiddenControl = "Blank Value";
            int indexFromPreviousDataRetrieval = 1;
            if (!String.Equals(hdnCurrentIndex.Value, defaultInitialValueForHiddenControl))
            {
                indexFromPreviousDataRetrieval = Convert.ToInt32(hdnCurrentIndex.Value, CultureInfo.InvariantCulture);
            }

            uclPager.PreviousIndex = indexFromPreviousDataRetrieval;

            //Call method in user control
            uclPager.PreAddAllLinks(tableDataCount, pageSize, indexFromPreviousDataRetrieval);
        }
        catch (Exception)
        {
        }
    }
    private void ShowPageIndex(int pageNumber, int pageSize, int totalRecords)
    {
        litStart.Text = (((pageNumber - 1) * pageSize) + 1).ToString();
        litTotalRecord.Text = totalRecords.ToString();
        int last = (((pageNumber - 1) * pageSize) + pageSize);
        litEnd.Text = (last > totalRecords ? totalRecords : last).ToString();
        hdnTotalCount.Value = ((totalRecords % pageSize) > 0 ? (totalRecords / pageSize) + 1 : (totalRecords / pageSize)).ToString();
    }
    protected void paginationLink_Click(object sender, EventArgs e)
    {
        //When link is clicked, set the pageIndex from user control property
        pageIndex = uclPager.CurrentClickedIndex;


        fillGrid();


    }
    protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageIndex = 1;
        pageSize = Int32.Parse(ddlSize.SelectedValue);


        fillGrid();

    }
    #endregion

    #region
    protected void Grd_Application_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Status_G = (HiddenField)e.Row.FindControl("Hid_Status_G");
            LinkButton LnkBtn_Take_Action = (LinkButton)e.Row.FindControl("LnkBtn_Take_Action");

            if (Hid_Status_G.Value == "1")
            {
                LnkBtn_Take_Action.Visible = true;
            }
            else
            {
                LnkBtn_Take_Action.Visible = false;
            }
        }
    }
    #endregion
}