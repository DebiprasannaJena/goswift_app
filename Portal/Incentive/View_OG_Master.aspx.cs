//******************************************************************************************************************
// File Name             :   View_OG_Master.aspx.cs
// Description           :   View Operational Guidelines Master
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
using System.Data;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class Portal_Incentive_View_OG_Master : System.Web.UI.Page
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

    ///// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        CallPaging();
        pageSize = Int32.Parse(ddlSize.SelectedValue);

        if (!IsPostBack)
        {
            fillGrid();
            fillPolicy();
        }
    }

    ///// Function Used
    #region FunctionUsed

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
    private void fillOG()
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "H";
            objIncentive.Param = Convert.ToInt32(DrpDwn_Policy_Name.SelectedValue);
            objLayer.BindDropdown(DrpDwn_OG_Name, objIncentive);
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

    private void clearFields()
    {
        DrpDwn_Policy_Name.SelectedIndex = 0;
        DrpDwn_OG_Name.SelectedIndex = 0;
    }
    private void fillGrid()
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        OG_Master_Entity objBU_Entity = new OG_Master_Entity();

        try
        {
            int? i_plc_id = null;
            if (DrpDwn_Policy_Name.SelectedIndex > 0)
            {
                i_plc_id = Convert.ToInt32(DrpDwn_Policy_Name.SelectedValue);
            }

            int? i_og_id = null;
            if (DrpDwn_OG_Name.SelectedIndex > 0)
            {
                i_og_id = Convert.ToInt32(DrpDwn_OG_Name.SelectedValue);
            }

            objBU_Entity.intPlcId = i_plc_id;
            objBU_Entity.intOGId = i_og_id;
            objBU_Entity.strAction = "V";
            objBU_Entity.intPageNo = pageIndex;
            objBU_Entity.intPageSize = pageSize;

            ds = ObjIMB.OG_Master_View(objBU_Entity);

            Grd_OG_Details.DataSource = ds.Tables[0];
            Grd_OG_Details.DataBind();
            Grd_OG_Details.Visible = true;

            if (ds.Tables[0].Rows.Count > 0)
            {
                int totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);

                hdnCurrentIndex.Value = pageIndex.ToString(CultureInfo.InvariantCulture);
                uclPager.AddPageLinks(totalCount, pageSize, pageIndex);
                ShowPageIndex(pageIndex, pageSize, totalCount);
                ViewState["PAGE.SIZE"] = totalCount;
                divPaging.Visible = true;
                uclPager.Visible = true;
                divPagingShow.Visible = true;
                Btn_Delete.Visible = true;
            }
            else
            {
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
            ObjIMB = null;
            objBU_Entity = null;
        }
    }

    #endregion

    ///// Policy DropdownList Selected Index Change
    protected void DrpDwn_Policy_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillOG();
    }

    ///// Button Search
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    ///// Button Edit
    protected void LnkBtn_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

        HiddenField Hid_OG_Id_G = (HiddenField)row.FindControl("Hid_OG_Id_G");

        //Response.Redirect("OG_Master.aspx?og_id=" + Hid_OG_Id_G.Value);
        Response.Redirect("OG_Master.aspx?og_id=" + Hid_OG_Id_G.Value + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&PIndex=" + 0 + "&URL=1");
    }
    ///// Button Delete
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        OG_Master_Entity objBU_Entity = new OG_Master_Entity();
        string strRetval = string.Empty;
        string strId = string.Empty;
        try
        {
            objBU_Entity.strAction = "D";
            foreach (GridViewRow gvRow in Grd_OG_Details.Rows)
            {
                CheckBox chkBoxID = (CheckBox)gvRow.FindControl("chkSelectSingle");
                if (chkBoxID.Checked == true)
                {
                    HiddenField Hid_OG_Id_G = (HiddenField)gvRow.FindControl("Hid_OG_Id_G");
                    strId += Hid_OG_Id_G.Value + ",";
                }
            }
            objBU_Entity.strOGIds = strId.TrimEnd(',');
            objBU_Entity.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            strRetval = objLayer.OG_Master_AED(objBU_Entity);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Record Deleted Successfully !</strong>','" + strProjName + "')", true);
            fillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objLayer = null;
            objBU_Entity = null;
        }
    }

    ///// Gridview Row Databound
    protected void Grd_OG_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_File_Name_G = (HiddenField)e.Row.FindControl("Hid_File_Name_G");
            HiddenField Hid_Del_Flag = ((HiddenField)e.Row.FindControl("Hid_Del_Flag"));
            CheckBox chkSelectSingle = ((CheckBox)e.Row.FindControl("chkSelectSingle"));
            HtmlAnchor ancDwldOG = (HtmlAnchor)e.Row.FindControl("ancDwldOG");
            var spnNoFileOG = e.Row.FindControl("spnNoFileOG");

            if (Hid_File_Name_G.Value != "")
            {
                spnNoFileOG.Visible = false;
                ancDwldOG.Visible = true;
                ancDwldOG.HRef = "../Incentive/OGDoc/" + Hid_File_Name_G.Value;
            }
            else
            {
                spnNoFileOG.Visible = true;
                ancDwldOG.Visible = false;
            }

            /*-------------------------------------------------------*/
            ////// Check Whether allowed for delete or not !!
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
}