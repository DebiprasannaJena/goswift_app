using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Incentive;
using DataAcessLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Globalization;

public partial class Incentive_SectorView : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

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
    protected void Page_Load(object sender, EventArgs e)
    {
        CallPaging();
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

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
        BindGridView();
    }
    private void BindGridView()
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Sector_Master_Entity objEntity = new Sector_Master_Entity();

        try
        {
            objEntity.strAction = "V";
            objEntity.intPageNo = pageIndex;
            objEntity.intPageSize = pageSize;
            //objEntity.SecTagId = Convert.ToInt32(ddlPolicy.SelectedValue);

            ds = objLayer.Sector_Master_View(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvsector.DataSource = ds.Tables[0];
                gvsector.DataBind();

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
                gvsector.DataSource = null;
                gvsector.DataBind();
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
            objEntity = null;
        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Response.Redirect("Sector_Manage.aspx?ID=" + id + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&PIndex=" + 0 + "&URL=1");
    }

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Sector_Master_Entity objSector = new Sector_Master_Entity();
        string strRetval = string.Empty;
        string strSecTagIds = string.Empty;
        try
        {
            objSector.strAction = "D";
            foreach (GridViewRow gvRow in gvsector.Rows)
            {
                CheckBox chkBoxID = (CheckBox)gvRow.FindControl("chkSelectSingle");
                if (chkBoxID.Checked == true)
                {
                    strSecTagIds += gvsector.DataKeys[gvRow.RowIndex].Value.ToString() + ",";
                }
            }
            objSector.strSectorTagIds = strSecTagIds.TrimEnd(',');
            objSector.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            strRetval = objLayer.Sector_Master_AED(objSector);
            if (strRetval == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Record Deleted Successfully !</strong>','" + strProjName + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Error in Record Deletion');", true);
            }
            BindGridView();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objSector = null;
        }
    }
    protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageIndex = 1;
        pageSize = Int32.Parse(ddlSize.SelectedValue);
        BindGridView();
    }
}