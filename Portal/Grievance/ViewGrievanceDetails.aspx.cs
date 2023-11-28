using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using Ionic.Zip;
using EntityLayer.Service;
//using Common;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json.Linq;


using EntityLayer.GrievanceEntity;
public partial class Portal_Grievance_ViewGrievanceDetails : System.Web.UI.Page
{
    #region "Global Variable"

    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    int intRecordCount = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }

        if (!Page.IsPostBack)
        {
            txtFromdate.Attributes.Add("readonly", "readonly");
            txtTodate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);

            try
            {
                //BindStatus();
                //BindDistrict();
                //BindGridDetails();

                if (Convert.ToString(Session["desId"]) == "126")  // change by anil sahoo
                {
                    BindStatus();
                    BindDistrict();
                    int intDistrictId = GetDistrictIdByUser();
                    DrpDwn_District.Enabled = false;
                    DrpDwn_District.SelectedValue = intDistrictId.ToString();

                   // DrpDwn_Investment_Level.SelectedValue = "2";
                   // DrpDwn_Investment_Level.Enabled = false;

                    
                    BindGridDetails();
                }
                else
                {
                    BindStatus();
                    BindDistrict();
                    int intDistrictId = GetDistrictIdByUser();
                    DrpDwn_District.Enabled = true;
                    DrpDwn_Investment_Level.Enabled = true;
                    BindGridDetails();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Grievance");
            }
        }
    }

    private void BindStatus()
    {
        try
        {
            DataTable dt = new DataTable();

            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BS"
            };
            dt = GrievanceServices.FillStatus(objSearch);
            DrpDwn_Status.DataSource = dt;
            DrpDwn_Status.DataTextField = "vchStatusName";
            DrpDwn_Status.DataValueField = "intStatusId";
            DrpDwn_Status.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DrpDwn_Status.Items.Insert(0, list);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void BindGridDetails()
    {
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();

        try
        {
            objGrivEntity.StrAction = "VGD";
            objGrivEntity.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
            objGrivEntity.intInvestmentLevel = Convert.ToInt32(DrpDwn_Investment_Level.SelectedItem.Value); // change anil sahoo
            objGrivEntity.strFromDate = txtFromdate.Text.Trim();
            objGrivEntity.strToDate = txtTodate.Text.Trim();
            objGrivEntity.strGrivId = Txt_Griv_Id.Text.Trim();
            objGrivEntity.intUserId = Convert.ToInt32(Session["UserId"]);
            objGrivEntity.intStatus = Convert.ToInt32(DrpDwn_Status.SelectedValue);

            DataTable dt = new DataTable();
            dt = objBAL.ViewGrivDetails(objGrivEntity);

            GridView1.DataSource = dt;
            GridView1.DataBind();

            intRecordCount = dt.Rows.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objBAL = null;
            objGrivEntity = null;
        }
    }
    private void BindDistrict()
    {
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();

        DataTable dt = new DataTable();
        try
        {
            objGrivEntity.StrAction = "BD";
            dt = GrievanceServices.FillDistrict(objGrivEntity);

            DrpDwn_District.DataTextField = "vchDistrictName";
            DrpDwn_District.DataValueField = "intDistrictId";
            DrpDwn_District.DataSource = dt;
            DrpDwn_District.DataBind();
            DrpDwn_District.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objBAL = null;
            objGrivEntity = null;
        }
    }

    private int GetDistrictIdByUser()
    {
        int intDistrictId = 0;

        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();

        DataTable dt = new DataTable();
        try
        {
            objGrivEntity.intUserId = Convert.ToInt32(Session["UserId"]);

            ////Get District Id By User
            dt = objBAL.GetDistrictIdByUser(objGrivEntity);
            if (dt.Rows.Count > 0)
            {
                intDistrictId = Convert.ToInt32(dt.Rows[0]["intDistrict"]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
            objBAL = null;
            objGrivEntity = null;
        }
        return intDistrictId;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1);

            HyperLink HypLnk_Griv_Id = (HyperLink)e.Row.FindControl("HypLnk_Griv_Id");
            HypLnk_Griv_Id.NavigateUrl = "GrievanceApplicationDetails.aspx?GrivId=" + GridView1.DataKeys[e.Row.RowIndex].Values["vchGrivId"] + "&RequestId=2";

            /*-------------------------------------------------------------------------------------------*/

            HiddenField Hid_Status = (HiddenField)e.Row.FindControl("Hid_Status");
            Label Lbl_Status = (Label)e.Row.FindControl("Lbl_Status");
            if (Hid_Status.Value == "3" || Hid_Status.Value == "7") /////Reject or Deferred
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Red;
            }
            else if (Hid_Status.Value == "13")/////Resolved
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Green;
            }
            else if (Hid_Status.Value == "8")/////Forwarded
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Orange;
            }

            /*-------------------------------------------------------------------------------------------*/

            HiddenField Hid_Invest_Level = (HiddenField)e.Row.FindControl("Hid_Invest_Level");
            Label Lbl_Invest_Level = (Label)e.Row.FindControl("Lbl_Invest_Level");
            if (Hid_Invest_Level.Value == "1")
            {
                Lbl_Invest_Level.ForeColor = System.Drawing.Color.Orange;
            }
            else
            {
                Lbl_Invest_Level.ForeColor = System.Drawing.Color.YellowGreen;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridDetails();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }

    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Txt_Griv_Id.Text = "";
        txtFromdate.Text = "";
        txtTodate.Text = "";

        DrpDwn_District.SelectedIndex = 0;
        DrpDwn_Investment_Level.SelectedIndex = 0;
    }

    #region "Display Google Paging"

    private void DisplayPaging()
    {
        if (GridView1.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (GridView1.PageIndex + 1 == GridView1.PageCount)
            {
                lblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRecordCount + "</b> of <b>" + intRecordCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)GridView1.Rows[0].FindControl("lblsl")).Text) + GridView1.PageSize - 1) + "</b> of <b>" + intRecordCount + "</b>";
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
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                GridView1.PageIndex = 0;
                GridView1.AllowPaging = false;
                BindGridDetails();
            }
            else
            {
                lbtnAll.Text = "All";
                GridView1.AllowPaging = true;
                BindGridDetails();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewProposal");
        }
    }

    #endregion
}
