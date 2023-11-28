using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.Incentive;
using DataAcessLayer.Incentive;
using EntityLayer.Incentive;
using System.Globalization;

public partial class Master_ViewIncentive : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #region "Member Variables"

    private int pageSize = 10;
    private int tableDataCount
    {
        get
        {
            return ViewState["PAGE.SIZE"] == null ? 1 : int.Parse(ViewState["PAGE.SIZE"].ToString());
        }
        set { ViewState["PAGE.SIZE"] = value; }
    }
    public int pageIndex
    {
        get
        {
            return ViewState["pageIndex"] == null ? 1 : int.Parse(ViewState["pageIndex"].ToString());
        }
        set { ViewState["pageIndex"] = value; }
    }

    #endregion

    ///// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        CallPaging();
        pageSize = Int32.Parse(ddlSize.SelectedValue);
        if (!IsPostBack)
        {
            fillPolicy();
            fillGrid();
        }
    }

    ///// Button Search
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    ///// Function Used
    #region FunctionUsed

    ///// Bind Policy Name
    private void fillPolicy()
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "C";
            objLayer.BindDropdown(DrpDwn_Policy_Name, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            objLayer = null;
        }
    }
    ///// Fill Gridview
    private void fillGrid()
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Policy_Master_Entity objEntity = new Policy_Master_Entity();
        try
        {
            objEntity.strAction = "V";
            objEntity.intPageNo = pageIndex;
            objEntity.intPageSize = pageSize;
            objEntity.intPolicyId = Convert.ToInt32(DrpDwn_Policy_Name.SelectedValue);

            ds = objLayer.Policy_Master_View(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grd_Sector.DataSource = ds.Tables[0];
                Grd_Sector.DataBind();

                int totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);

                hdnCurrentIndex.Value = pageIndex.ToString(CultureInfo.InvariantCulture);
                uclPager.AddPageLinks(totalCount, pageSize, pageIndex);
                ShowPageIndex(pageIndex, pageSize, totalCount);
                ViewState["PAGE.SIZE"] = totalCount;
                divPaging.Visible = true;
                uclPager.Visible = true;
                divPagingShow.Visible = true;
                lblMessage.Visible = false;
                Btn_Delete.Visible = true;
            }
            else
            {
                Grd_Sector.DataSource = null;
                Grd_Sector.DataBind();
                lblMessage.Visible = true;
                divPaging.Visible = false;
                uclPager.Visible = false;
                divPagingShow.Visible = false;
                Btn_Delete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objLayer = null;
            objEntity = null;
        }
    }

    #endregion

    ///// Gridview RowCommand
    protected void Grd_Sector_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edt")
        {
            Response.Redirect("AddIncentivePolicy.aspx?ID=" + e.CommandArgument.ToString() + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&PIndex=" + 0 + "&URL=1");
        }
    }
    ///// Gridview RowDataBound
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.HtmlControls.HtmlAnchor ancDwld = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("ancDwld");
            HiddenField hdnView = ((HiddenField)e.Row.FindControl("hdnView"));
            HiddenField hdnCnt = ((HiddenField)e.Row.FindControl("hdnCnt"));
            LinkButton lnkBtn = ((LinkButton)e.Row.FindControl("lbtnEdit"));
            CheckBox chkSelectSingle = ((CheckBox)e.Row.FindControl("chkSelectSingle"));
            var spnNoFile = e.Row.FindControl("spnNoFile");
            var spnSave = e.Row.FindControl("spnSave");

            HiddenField Hid_Del_Flag = ((HiddenField)e.Row.FindControl("Hid_Del_Flag"));

            System.Web.UI.HtmlControls.HtmlAnchor ancDwldAmd = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("ancDwldAmd");
            HiddenField hdnViewAmd = ((HiddenField)e.Row.FindControl("hdnViewAmd"));
            var spnNoFileAmd = e.Row.FindControl("spnNoFileAmd");
            var spnSaveAmd = e.Row.FindControl("spnSaveAmd");

            if (e.Row.Cells[4].Text == "01-Jan-0001")
                e.Row.Cells[4].Text = "--";

            //if (hdnCnt.Value == "0")
            //{
            //    // lnkBtn.Visible = true;
            //    chkSelectSingle.Enabled = true;
            //}
            //else
            //{
            //    // lnkBtn.Visible = false;
            //    chkSelectSingle.Enabled = false;
            //}

            if (Hid_Del_Flag.Value == "Y")
            {
                chkSelectSingle.Visible = true;
            }
            else if (Hid_Del_Flag.Value == "N")
            {
                chkSelectSingle.Visible = false;
            }

            if (hdnView.Value != "")
            {
                spnNoFile.Visible = false;
                ancDwld.Visible = true;
                ancDwld.HRef = "../Incentive/PolicyDoc/" + hdnView.Value;
            }
            else
            {
                spnNoFile.Visible = true;
                //spnSave.Visible = false;
                ancDwld.Visible = false;
            }
            if (hdnViewAmd.Value != "")
            {
                spnNoFileAmd.Visible = false;
                ancDwldAmd.Visible = true;
                ancDwldAmd.HRef = "../Incentive/AmendmentDoc/" + hdnViewAmd.Value;
            }
            else
            {
                spnNoFileAmd.Visible = true;
                //spnSave.Visible = false;
                ancDwldAmd.Visible = false;
            }
        }
    }

    #region "For Paging"

    private void CallPaging()
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

    ///// Button Delete
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Policy_Master_Entity objIncentive = new Policy_Master_Entity();

        /*--------------------------------------------------------------------*/
        ///// Below initiation is only for avoid exception at serialization (As Common method is used for Add,Edit and Delete)
        List<SectionMasterItem> listSection = new List<SectionMasterItem>();
        objIncentive.listSectionItem = listSection;
        /*--------------------------------------------------------------------*/

        string strRetval = string.Empty;
        string strPlcIds = string.Empty;
        try
        {
            objIncentive.strAction = "D";
            foreach (GridViewRow gvRow in Grd_Sector.Rows)
            {
                CheckBox chkBoxID = (CheckBox)gvRow.FindControl("chkSelectSingle");
                if (chkBoxID.Checked == true)
                {
                    strPlcIds += Grd_Sector.DataKeys[gvRow.RowIndex].Value + ",";
                }
            }
            objIncentive.strPlcIds = strPlcIds.TrimEnd(',');
            objIncentive.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            strRetval = objLayer.Policy_Master_AED(objIncentive);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Record Deleted Successfully !</strong>','" + strProjName + "')", true);
            fillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
        }
    }
}