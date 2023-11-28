//******************************************************************************************************************
// File Name             :   View_Incentive_Name_Master.aspx.cs
// Description           :   View Incentive Name Master
// Created by            :   Sushant Kumar Jena
// Created on            :   7th Sept 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Globalization;
using System.Data;

public partial class Portal_Incentive_View_Incentive_Name_Master : System.Web.UI.Page
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

    ////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        CallPaging();
        pageSize = Int32.Parse(ddlSize.SelectedValue);

        if (!IsPostBack)
        {
            fillGrid();
            fillOG();
            Hid_Inct_Id.Value = "0";
        }
    }

    ///// Function Used
    #region Function Used

    private void fillOG()
    {
        try
        {
            IncentiveMaster objIncentive = new IncentiveMaster();
            IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
            objIncentive.Action = "D";
            objLayer.BindDropdown(DrpDwn_OG_Name, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {

        }
    }
    private void fillFilteredIncentive()
    {
        try
        {
            IncentiveMaster objIncentive = new IncentiveMaster();
            IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
            objIncentive.Action = "S";
            objIncentive.Param_1 = Convert.ToInt32(DrpDwn_OG_Name.SelectedValue);
            objLayer.BindDropdown(DrpDwn_Incentive_Name, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {

        }
    }
    private void clearFields()
    {
        DrpDwn_OG_Name.SelectedIndex = 0;
        DrpDwn_Incentive_Name.SelectedIndex = 0;

        Hid_Inct_Id.Value = "0";
    }
    private void fillGrid()
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        Incentive_Master_Entity objBU_Entity = new Incentive_Master_Entity();

        try
        {
            int? i_og_id = null;
            if (DrpDwn_OG_Name.SelectedIndex > 0)
            {
                i_og_id = Convert.ToInt32(DrpDwn_OG_Name.SelectedValue);
            }

            int? i_inct_id = null;
            if (DrpDwn_Incentive_Name.SelectedIndex > 0)
            {
                i_inct_id = Convert.ToInt32(DrpDwn_Incentive_Name.SelectedValue);
            }

            objBU_Entity.intInctId = i_inct_id;
            objBU_Entity.intOGId = i_og_id;
            objBU_Entity.strAction = "V";
            objBU_Entity.intPageNo = pageIndex;
            objBU_Entity.intPageSize = pageSize;

            ds = ObjIMB.Inct_Name_Master_View(objBU_Entity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Grd_Inct_Details.DataSource = ds.Tables[0];
                Grd_Inct_Details.DataBind();
                Grd_Inct_Details.Visible = true;

                int totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);

                hdnCurrentIndex.Value = pageIndex.ToString(CultureInfo.InvariantCulture);
                uclPager.AddPageLinks(totalCount, pageSize, pageIndex);
                ShowPageIndex(pageIndex, pageSize, totalCount);
                ViewState["PAGE.SIZE"] = totalCount;
                divPaging.Visible = true;
                uclPager.Visible = true;
                divPagingShow.Visible = true;
            }
            else
            {
                divPaging.Visible = false;
                uclPager.Visible = false;
                divPagingShow.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion

    ////// Button Search
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    ////// Button Reset
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    ////// Go For Edit
    protected void LnkBtn_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

        HiddenField Hid_Inct_Id_G = (HiddenField)row.FindControl("Hid_Inct_Id_G");

        Response.Redirect("Incentive_Name_Master.aspx?inct_id=" + Hid_Inct_Id_G.Value);
    }

    ////// Go For Delete
    ///// Button Delete
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Incentive_Master_Entity objBU_Entity = new Incentive_Master_Entity();
        string strRetval = string.Empty;
        string strId = string.Empty;
        try
        {
            objBU_Entity.strAction = "D";
            foreach (GridViewRow gvRow in Grd_Inct_Details.Rows)
            {
                CheckBox chkBoxID = (CheckBox)gvRow.FindControl("chkSelectSingle");
                if (chkBoxID.Checked == true)
                {
                    HiddenField Hid_Inct_Id_G = (HiddenField)gvRow.FindControl("Hid_Inct_Id_G");

                    strId += Hid_Inct_Id_G.Value + ",";
                }
            }
            objBU_Entity.strInctIds = strId.TrimEnd(',');
            objBU_Entity.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            strRetval = objLayer.Inct_Name_Master_AED(objBU_Entity);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Record Deleted Successfully !</strong>','" + strProjName + "')", true);
            fillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBU_Entity = null;
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
    protected void DrpDwn_OG_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillFilteredIncentive();
    }
    protected void Grd_Inct_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Del_Flag = ((HiddenField)e.Row.FindControl("Hid_Del_Flag"));
            CheckBox chkSelectSingle = ((CheckBox)e.Row.FindControl("chkSelectSingle"));

            if (Hid_Del_Flag.Value == "Y")
            {
                chkSelectSingle.Visible = true;
            }
            else if (Hid_Del_Flag.Value == "N")
            {
                chkSelectSingle.Visible = false;
            }
        }
    }
}