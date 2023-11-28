using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Service;
using BusinessLogicLayer.Service;

public partial class ServiceMaster_ViewServiceMaster : System.Web.UI.Page
{
    DataTable dtable;
    DataSet ds = new DataSet();
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try{
            BindGridDetails();
            BindDept();
            txtServiceName.Attributes.Add("autocomplete", "off");
                txtServiceName.Text = "";
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Service");
            }
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            BindGridDetails();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            BindGridDetails();
        }
    }
    #region "Display Google Paging"

    private void DisplayPaging()
    {

        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + intRetVal + "</b> Of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvService.Rows[0].Cells[0].Text) + (gvService.PageSize - 1)) + "</b> Of <b>" + intRetVal + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Label5 = (Label)e.Row.FindControl("Label5");
            HyperLink hlink = (HyperLink)e.Row.FindControl("hlnk");

            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);

            if (gvService.DataKeys[e.Row.RowIndex].Values[0].ToString() != "0")
            { Label5.Text = "No"; }
            else { Label5.Text = "Yes"; }
            hlink.NavigateUrl = "AddServiceMaster.aspx?Sid=" + gvService.DataKeys[e.Row.RowIndex].Values[1].ToString();

            Label lblservicetype = (Label)e.Row.FindControl("lblservicetype");
            if (lblservicetype.Text == "1")
            { lblservicetype.Text = "External Service"; }
            else
            { lblservicetype.Text = "Internal Service"; }

            Label LblExternalType = (Label)e.Row.FindControl("LblExternalType");
            if (LblExternalType.Text == "1")
            { LblExternalType.Text = "Yes"; }
            else
            { LblExternalType.Text = "No"; }

        }
    }
    private void BindDept()
    {
        try{
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindDepartment("DP").ToList();
        ddlDept.DataSource = objServicelist;
        ddlDept.DataTextField = "strdeptname";
        ddlDept.DataValueField = "Deptid";
        ddlDept.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDept.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
    private void BindGridDetails()
    {
       
        try
        {
            ServiceDetails objService = new ServiceDetails();
            ServiceBusinessLayer objService1 = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objService.strAction = "V";
            objService.intdeptid = Convert.ToInt32(ddlDept.SelectedValue);
            objService.strServiceName = txtServiceName.Text;
            objServicelist = objService1.ViewServiceMasterDet(objService);
            gvService.DataSource = objServicelist;
            gvService.DataBind();
         
            intRetVal = objServicelist.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
 
   

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridDetails();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindGridDetails();
    }
   
    protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Page.IsPostBack)
        {
            if (e.CommandName.Equals("DeleteRow"))
            {
                GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
                ServiceDetails objservice = new ServiceDetails();
                string rawURL = "";
                try
                {
                 objservice.strAction = "D";
                 objservice.intServiceId = Convert.ToInt32(gvService.DataKeys[RowIndex].Values[1]);                   
                 objservice.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                string strRetVal = objServiceDet.AddServiceMasterDet(objservice);
                string strShowMsg = Messages.ShowMessage(strRetVal);
                string ff = "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);

                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Service");
                }
            }
        }
    }
}