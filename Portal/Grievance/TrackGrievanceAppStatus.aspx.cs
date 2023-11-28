using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Configuration;
using System.Data.SqlClient;
using EntityLayer.GrievanceEntity;

public partial class Portal_Helpdesk_TrackGrievanceAppStatus : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
    }

    public void BindGridDetails()
    {
        GrievanceServices objBAL = new GrievanceServices();
        GrievanceEntity objGrivEntity = new GrievanceEntity();

        try
        {
            objGrivEntity.StrAction = "TGD";
            objGrivEntity.strGrivId = Txt_Griv_Id.Text.Trim();
            objGrivEntity.vchMobileNo = Txt_Mobile_No.Text.Trim();
            objGrivEntity.vchEmail = Txt_Email_Id.Text.Trim();
            objGrivEntity.vchApplicantName = Txt_Company_Name.Text;

            DataTable dt = new DataTable();
            dt = objBAL.GrievanceTrackDetails(objGrivEntity);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Txt_Griv_Id.Text = "";
        Txt_Company_Name.Text = "";
        Txt_Mobile_No.Text = "";
        Txt_Email_Id.Text = "";

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink HypLnk_Griv_Id = (HyperLink)e.Row.FindControl("HypLnk_Griv_Id");
            HypLnk_Griv_Id.NavigateUrl = "GrievanceApplicationDetails.aspx?GrivId=" + GridView1.DataKeys[e.Row.RowIndex].Values["vchGrivId"] + "&RequestId=3";

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
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridDetails();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txt_Griv_Id.Text == "" && Txt_Company_Name.Text == "" && Txt_Mobile_No.Text == "" && Txt_Email_Id.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter at least one field !</strong>');", true);
                return;
            }
            else
            {
                BindGridDetails();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }
    }
}