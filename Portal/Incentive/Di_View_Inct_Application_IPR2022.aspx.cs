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
using EntityLayer.Service;
using Common;
using System.Web.UI.HtmlControls;

public partial class Portal_Incentive_Di_View_Inct_Application_IPR2022 : System.Web.UI.Page
{

    bool IsPageRefresh = false;
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
    ServiceDetails objServiceEntity = new ServiceDetails();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();

    string str_Retvalue = string.Empty;

    static int intDataLoadType;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (Session["UserId"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {



                fillGrid();

                ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
                Session["SessionId"] = ViewState["ViewStateId"].ToString();

            }
            else
            {

                if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                {
                    IsPageRefresh = true;
                }

                Session["SessionId"] = System.Guid.NewGuid().ToString();
                ViewState["ViewStateId"] = Session["SessionId"].ToString();

            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }

    }

    private void fillGrid()
    {
        try
        {
            IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
            Inct_Application_Details_Entity objBU_Entity = new Inct_Application_Details_Entity();
            objBU_Entity.strAction = "DIV";
            objBU_Entity.intStatus = Convert.ToInt32(DrpDwn_Status.SelectedValue);
            objBU_Entity.strAppNo = Txt_App_No.Text == "" ? "0" : Convert.ToString(Txt_App_No.Text);

            objBU_Entity.strUserID = Convert.ToString(Session["UserId"]); // Passing of User Id to check DIC/RIC/DI ON 23-OCT-2017

            IList<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
            list = ObjIMB.DI_ViewIPR2022_Application_Details(objBU_Entity);
            if (list.Count > 0)
            {
                Grd_Application.DataSource = list;
                Grd_Application.DataBind();
                divPaging.Visible = true;

                divPagingShow.Visible = true;
            }
            else
            {
                Grd_Application.DataSource = null;
                Grd_Application.DataBind();
                divPaging.Visible = false;
                divPagingShow.Visible = false;
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    protected void LnkBtn_View_Application_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Form_Preview_Id = (HiddenField)row.FindControl("Hid_Form_Preview_Id");
            HiddenField Hid_Unique_Id = (HiddenField)row.FindControl("Hid_Unique_Id");

            if (Hid_Form_Preview_Id.Value != "")
            {

                Response.Redirect("~/Incentives/" + Hid_Form_Preview_Id.Value + "?InctUniqueNo=" + Hid_Unique_Id.Value + "", false);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No Form Preview Available for this Application !</strong>','SWP')", true);
                return;
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
        finally
        {

        }
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {

            fillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void Grd_Application_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer) as GridViewRow;
                LinkButton lnkButton = (LinkButton)row.FindControl("lnkButton");
                string unit = (row.FindControl("Lbl_Unit_Name") as Label).Text;
                string incname = (row.FindControl("Lbl_Inct_Name") as Label).Text;

                string Uniqueid = (row.FindControl("Hid_Unique_Id") as HiddenField).Value;
                string AppNo = (row.FindControl("HiddenField1") as HiddenField).Value;
                string code = (row.FindControl("hdnIncentiveNo") as HiddenField).Value;
                string FormPreviewId = (row.FindControl("Hid_Form_Preview_Id") as HiddenField).Value;
                LinkButton lbtnRaise = row.FindControl("lbtnRaise") as LinkButton;
                if (lnkButton.Text.Trim().ToLower() == "take action")
                {
                    Response.Redirect("Di_ApproveIncentive_IPR2022.aspx?URL=1&UniqueID=" + Uniqueid + "&IncentiveName=" + incname + "&ApplicationNo=" + AppNo + "&UnitName=" + unit + "&FormId=" + FormPreviewId);


                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    protected void Grd_Application_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string toptable = "";
                string strHTMlQuery2 = "";
                Label Lbl_Status = (Label)e.Row.FindControl("Lbl_Status");
                HiddenField hdnAppStatus = (HiddenField)e.Row.FindControl("hdnAppStatus");
                LinkButton lbtnDisbursedDtls = (e.Row.FindControl("lbtnDisbursedDtls") as LinkButton);
                LinkButton lnkButton = (LinkButton)e.Row.FindControl("lnkButton");
                LinkButton LnkBtn_View_Application = (LinkButton)e.Row.FindControl("LnkBtn_View_Application");
                string unit = (e.Row.FindControl("Lbl_Unit_Name") as Label).Text;
                string IncentiveName = (e.Row.FindControl("Lbl_Inct_Name") as Label).Text;
                HiddenField hdnSanFileName = (HiddenField)e.Row.FindControl("hdnSanFileName");
                HiddenField hdnRemarks = (HiddenField)e.Row.FindControl("hdnRemarks");
                HtmlGenericControl DisbursedList = (HtmlGenericControl)e.Row.FindControl("DisbursedList");
                LinkButton lbtnRaises = (e.Row.FindControl("lbtnRaise") as LinkButton);
                LinkButton lbtnQueryDtls = (e.Row.FindControl("lbtnQueryDtls") as LinkButton);
                HiddenField hdnTextVal1 = (HiddenField)e.Row.FindControl("hdnTextVal1");


                toptable = "<table class='table table-bordered table-hover'>";
                toptable += "<tr><th colspan='2'>Sanction Details</th></tr>";
                toptable += "<tr><td>Application No.</td><td>" + LnkBtn_View_Application.Text + "</td></tr>";
                toptable += "<tr><td>Unit Name</td><td>" + unit + "</td></tr>";
                toptable += "<tr><td>Incentive Name</td><td>" + IncentiveName + "</td></tr>";
                toptable += "<tr><td>Sanction Order Document</td><td><a target=\"_blank\"  href=\"../../Portal/Incentive/Sanctionorder/" + hdnSanFileName.Value + "\" ><i class=\"fa fa-file-text-o\" ></i></a>   </td></tr>";
                toptable += "<tr><td>Remarks</td><td>" + hdnRemarks.Value + "</td></tr>";
                toptable += "</table>";



                if (hdnAppStatus.Value == "2")
                {
                    Lbl_Status.Text = "Approved";
                    lnkButton.Visible = false;
                    lbtnDisbursedDtls.Visible = true;
                    DisbursedList.InnerHtml = toptable;
                    lbtnDisbursedDtls.Visible = true;
                }
                else if (hdnAppStatus.Value == "3")
                {
                    Lbl_Status.Text = "Reject";
                    lnkButton.Visible = false;
                    lbtnDisbursedDtls.Visible = true;
                    DisbursedList.InnerHtml = toptable;
                    lbtnDisbursedDtls.Visible = true;
                }
                else if (hdnAppStatus.Value == "8")
                {
                    Lbl_Status.Text = "Forwarded";
                    lnkButton.Visible = true;
                    lbtnDisbursedDtls.Visible = false;
                    lbtnDisbursedDtls.Visible = false;
                }

            }

        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }
}