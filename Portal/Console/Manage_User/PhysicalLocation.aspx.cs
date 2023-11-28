using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using KWAdminConsole.Messages;


public partial class Admin_Manage_User_PhysicalLocation : System.Web.UI.Page
{



    AdminAppService ObjAdminBal = new AdminAppService();
    Location objPhyLoc = new Location();
    int intOutPut;
     #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        // Code added by Dilip Tripathy on dated 25-June-2012
        //To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        if (Request.QueryString["att"] != null)
        {
            string strAtt = CommonFunction.DecryptData(Request.QueryString["att"].ToString());
        }
        //Code Added By : Dilip Kumar Tripathy on dated 10-May-2013
        //Purpose : To clear the browser cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");

        //this.txtLocation.Attributes.Add("onkeyup", "return TextCounter('" + txtLocation.ClientID + "','" + lblchar.ClientID + "',200);");
        //objAdmin = objKwantify.CreateAdminConsole();
        //objHierarchy = objAdmin.CreateHirarchy();
        //objPhyLoc = objHierarchy.CreatePhysicalLocation();
        //this.btnSubmit.Attributes.Add("onClick", "return validation();");
        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabLevelDetails.ActiveTab.HeaderText;
        if (Request.QueryString["pdx"] != null)
        {
            grd.PageIndex = Convert.ToInt32(Request.QueryString["pdx"]);
        }
        if (!IsPostBack)
        {
            FillCountryName();
            //ddlCountry = objComm.PopupDropDown("ConnectionString", "SELECT intCountryId,vchCountryName FROM M_POR_COUNTRY WHERE bitStatus<>1 ORDER BY vchCountryName", ddlCountry);
            FillGrid();
            if (Request.QueryString["PLID"] != null)
            {
                FillToEdit(Convert.ToInt32(CommonFunction.DecryptData( Request.QueryString["PLID"].ToString())));
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";                
            }
            else
            {
                btnSubmit.Text = "Save";
                TabLevelDetails.ActiveTabIndex =1;
            }
            if (TabLevelDetails.ActiveTabIndex == 0)
            {
                txtLocation.Focus();
            }
        }
    }
    #endregion
    public void FillToEdit(int locId)
    {

        IList<Location> objLoc = new List<Location>();
        objLoc = ObjAdminBal.GetAllPhysicalLocation("E", locId);
        foreach (Location objPLoc in objLoc)
        {
            txtLocation.Text = objPLoc.PhysicalLocationName;
            ddlCountry.SelectedValue = objPLoc.PhysicalLocationCountryID.ToString();
            ddlTimezone.Items.FindByValue(objPLoc.LocDiff).Selected=true;
        }
       
        TabLevelDetails.ActiveTabIndex = 0;
     
    }
    public void UpdateLocation()
    {
        objPhyLoc.ActionCode = "U";
        //objPhyLoc.PhysicalLocationID = Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["PLID"].ToString()));
        objPhyLoc.PhysicalLocationName = txtLocation.Text.Trim();
        objPhyLoc.PhysicalLocationCountryID = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
        objPhyLoc.LocDiff = ddlTimezone.SelectedValue.ToString();
        // objPhyLoc.PhysicalLocationCountryID = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
        //objPhyLoc.TimeZone = Convert.ToInt32(textTimeZone.Text.Trim());
        objPhyLoc.CreatedBy = 1;
        intOutPut = Convert.ToInt32(ObjAdminBal.EditLocation(objPhyLoc));
        string strOutmsg = StaticValues.message(intOutPut, "Location");
        string strUrl = "PhysicalLocation.aspx?pdx="+Request.QueryString["Pindex"].ToString();
        ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "');document.location.href='" + strUrl + "';", true);
        //document.location.href('PhysicalLocation.aspx');
        TabLevelDetails.ActiveTabIndex = 1;
        grd.PageIndex =Convert.ToInt32(Request.QueryString["Pindex"].ToString());
        FillGrid();
     }
    #region "Button Events"
    /// <summary>
    /// To add location.
    /// </summary>
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Save")
        {
            AddLocation();
        }
        else
        {
            UpdateLocation();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Reset")
        {
            txtLocation.Text = string.Empty;
            ddlCountry.SelectedIndex = 0;
            ddlTimezone.SelectedIndex = 0;
        }
        else
        {
            string strUrl = "PhysicalLocation.aspx?pdx=" + Request.QueryString["Pindex"].ToString();
            ClientScript.RegisterStartupScript(GetType(), "", "document.location.href='" + strUrl + "';", true);
        }
    }
    #endregion

    #region "User Function"
    /// <summary>
    /// To Fill the grid with location details.
    /// </summary>
    private void FillGrid()
    {
        IList<Location> objLoc = new List<Location>();
        objLoc = ObjAdminBal.GetAllPhysicalLocation("V", 0);
        grd.DataSource = objLoc;
        grd.DataBind();
        DisplayPaging(grd, objLoc.Count);
        if (grd.Rows.Count > 0)
        {
            btnDelete.Visible = true;
        }
        else
        {
            btnDelete.Visible = false;
        }

    }

    /// <summary>
    /// Created By   : Dilip Kumar Tripathy.
    /// Created Date :14-Mar-2012
    /// Purpose      : To show  Paging details of gridview
    /// </summary>
    private void DisplayPaging(GridView gridviewone, int totalRowCount)
    {
        if (gridviewone.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lnkBtnAll.Visible = true;
            lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, totalRowCount);
        }
        else
        {
            this.lblPaging.Visible = false;
            lnkBtnAll.Visible = false;
        }
    }
    /// <summary>
    /// To add Location details.
    /// </summary>
    private void AddLocation()
    {
        objPhyLoc.ActionCode = "A";
        objPhyLoc.PhysicalLocationID = "0";
        objPhyLoc.PhysicalLocationName = txtLocation.Text.Trim();
        objPhyLoc.PhysicalLocationCountryID = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
        //objPhyLoc.TimeZone = Convert.ToInt32(txtTimeZone.Text.Trim());
        objPhyLoc.LocDiff = ddlTimezone.SelectedValue.ToString();
        objPhyLoc.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
        intOutPut = Convert.ToInt32(ObjAdminBal.AddLocation(objPhyLoc));
        string strOutmsg = StaticValues.message(intOutPut, "Location");
        ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "');", true);
        txtLocation.Text = string.Empty;
        ddlCountry.SelectedIndex = 0;
        ddlTimezone.SelectedIndex = 0;
        FillGrid();
    }
    /// <summary>
    /// Created By   : Sudhansu Sekhar Sarangi
    /// Created Date : 24-Feb-2014
    /// Purpose      : To Fill Country Dropdown
    /// </summary>
    protected void FillCountryName()
    {

        try
        {
            IList<Location> lstFunction = ObjAdminBal.FillCountry();
            ddlCountry.DataSource = lstFunction;
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataTextField = "CountryName";         
            ddlCountry.DataBind();
            //ddlCountry.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    #endregion

    #region "Grid Events"
    /// <summary>
    /// To edit location details.
    /// </summary>
    //protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    Label lblCountry = (Label)grd.Rows[e.NewEditIndex].FindControl("countryname");
    //    string strCountry = lblCountry.Text;

    //    grd.EditIndex = e.NewEditIndex;
    //    string strCID = grd.Rows[e.NewEditIndex].Cells[4].Text;
    //    FillGrid();
    //    DropDownList ddlCountryName = (DropDownList)grd.Rows[e.NewEditIndex].FindControl("ddlECountry");
    //    ddlCountryName.SelectedValue = getCountry(strCountry).ToString();

    //}
    //protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    GridViewRow row = (GridViewRow)grd.Rows[e.RowIndex];
    //    Label lbl = (Label)row.FindControl("lblID");
    //    TextBox textLocation = (TextBox)row.FindControl("TextBox1");
    //    //TextBox textTimeZone = (TextBox)row.FindControl("TextBox2");
    //    DropDownList ddlTimezone = (DropDownList)row.FindControl("ddlTimezone");
    //    DropDownList ddlCountry = (DropDownList)row.FindControl("ddlECountry");

    //    grd.EditIndex = -1;
    //    objPhyLoc.ActionCode = "U";
    //    objPhyLoc.PhysicalLocationID = Convert.ToInt32(lbl.Text);
    //    objPhyLoc.PhysicalLocationName = textLocation.Text.Trim();
    //    objPhyLoc.PhysicalLocationCountryID = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
    //    objPhyLoc.LocDiff = ddlTimezone.SelectedValue.ToString();
    //   // objPhyLoc.PhysicalLocationCountryID = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
    //    //objPhyLoc.TimeZone = Convert.ToInt32(textTimeZone.Text.Trim());
    //    objPhyLoc.CreatedBy = 1;
    //    intOutPut = Convert.ToInt32(objPhyLoc.EditLocation(objPhyLoc));
    //    string strOutmsg = StaticValues.message(intOutPut, "Location");
    //    ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "');", true);
    //    //document.location.href('PhysicalLocation.aspx');
    //    FillGrid();
    //}
    ///// <summary>
    ///// To cancel updation.
    ///// </summary>
    //protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    grd.EditIndex = -1;
    //    FillGrid();
    //}
    /// <summary>
    /// To delete location details.
    /// </summary>
    //protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    GridViewRow row = (GridViewRow)grd.Rows[e.RowIndex];
    //    Label lbldeleteID = (Label)row.FindControl("lblID");
    //    objPhyLoc.ActionCode = "D";
    //    objPhyLoc.PhysicalLocationID = Convert.ToInt32(lbldeleteID.Text);
    //    objPhyLoc.PhysicalLocationName = "";
    //    objPhyLoc.TimeZone = 0;
    //    objPhyLoc.CreatedBy = 1;
    //    intOutPut = Convert.ToInt32(objPhyLoc.DeleteLocation(objPhyLoc));
    //    string strOutmsg = StaticValues.message(intOutPut, "Location");
    //    ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "');", true);
    //    //document.location.href('PhysicalLocation.aspx');
    //    FillGrid();
    //    //TabLevelDetails.ActiveTabIndex = 1;
    //}

    protected void grd_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    #endregion

    protected void TabLevelDetails_ActiveTabChanged(object sender, EventArgs e)
    {
        if (TabLevelDetails.ActiveTabIndex == 0)
        {
            txtLocation.Text = "";
            ddlCountry.SelectedIndex = 0;
            btnSubmit.Text = "Save";
            txtLocation.Focus();
        }

    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlGCountry = (DropDownList)e.Row.FindControl("ddlECountry");
            //FillDropDown(ddlGCountry);
            //DataRowView objRowView = (DataRowView)e.Row.DataItem;
            //ddlGCountry.SelectedValue = objRowView["PhysicalLocationCountryID"].ToString();
            string locId = grd.DataKeys[e.Row.RowIndex].Value.ToString();
            HyperLink objHyper = e.Row.FindControl("hypEdit") as HyperLink;
            objHyper.NavigateUrl = "PhysicalLocation.aspx?PLID=" +CommonFunction.EncryptData(locId)+"&Pindex="+grd.PageIndex;
        }

    }

    //private void FillDropDown(DropDownList objDdl)
    //{
    //    try
    //    {
    //        objDdl = objComm.PopupDropDown("ConnectionString", "SELECT INT_COUNTRYID,VCH_COUNTRY_NAME FROM M_POR_COUNTRY WHERE INT_DELETED_FLAG<>1 ORDER BY VCH_COUNTRY_NAME", objDdl);

    //    }
    //    catch
    //    {
    //    }
    //    finally
    //    {
    //        objComm.Dispose();
    //    }

    //}
    //private int getCountry(string strCountryName)
    //{
    //    string strQuery = "SELECT INT_COUNTRYID FROM M_POR_COUNTRY WHERE  VCH_COUNTRY_NAME='" + strCountryName + "' AND INT_DELETED_FLAG<>1";
    //    int intCountryID = (int)objComm.ExeScalar("ConnectionString", strQuery, 0);
    //    return intCountryID;
    //}

    protected void lnkBtnAll_Click(object sender, EventArgs e)
    {
        if (lnkBtnAll.Text == "All")
        {
            lnkBtnAll.Text = "Paging";
            this.grd.PageIndex = 0;
            grd.AllowPaging = false;
            FillGrid();
            if (grd.Rows.Count > 0)
            {
                this.lblPaging.Text = "1-" + grd.Rows.Count.ToString() + " Of " + grd.Rows.Count.ToString();
            }

        }
        else
        {
            lnkBtnAll.Text = "All";
            grd.AllowPaging = true;
            FillGrid();

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= grd.Rows.Count - 1; i++)
        {
            CheckBox chknews = default(CheckBox);
            chknews = (CheckBox)grd.Rows[i].FindControl("chkSelect");
            if ((chknews.Checked == true))
            {
                objPhyLoc.ActionCode = "D";
                objPhyLoc.PhysicalLocationID =Convert.ToString(grd.DataKeys[i].Value.ToString());
                objPhyLoc.PhysicalLocationName = "";
                objPhyLoc.TimeZone = 0;
                objPhyLoc.CreatedBy = 1;
                intOutPut = Convert.ToInt32(ObjAdminBal.DeleteLocation(objPhyLoc));
            }
        }
        if (intOutPut == 13)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "alert('Location is assigned to a user.Can not be delete.');", true);
        }
        else
        {
            string strOutmsg = StaticValues.message(intOutPut, "Location");
            ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "');", true);

        }
        FillGrid();
    }





}
