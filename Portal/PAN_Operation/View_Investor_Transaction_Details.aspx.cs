using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Portal_PAN_Operation_View_Investor_Transaction_Details : System.Web.UI.Page
{
    int intInvetsorId = 0;
    int intRequestId = 0;
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["val"] != null)
                {
                    intInvetsorId = Convert.ToInt32(Request.QueryString["val"]);
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ViewDetails");
            }
        }
    }

    private void FillGrid()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();
        try
        {
            objCommand.CommandText = "USP_INV_USER_MANAGEMENT_VIEW";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "V9");
            objCommand.Parameters.AddWithValue("@P_INT_INVESTOR_ID", intInvetsorId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            /*-----------------------------------------------------*/
            ///Bind PEAL Details
            /*-----------------------------------------------------*/
            if (objds.Tables[0].Rows.Count > 0)
            {
                GrdPEAL.DataSource = objds.Tables[0];
                GrdPEAL.DataBind();
            }
            else
            {
                GrdPEAL.DataSource = null;
                GrdPEAL.DataBind();
            }

            /*-----------------------------------------------------*/
            ///Bind Service Details
            /*-----------------------------------------------------*/
            if (objds.Tables[1].Rows.Count > 0)
            {
                GrdService.DataSource = objds.Tables[1];
                GrdService.DataBind();
            }
            else
            {
                GrdService.DataSource = null;
                GrdService.DataBind();
            }

            /*-----------------------------------------------------*/
            ///Bind Incentive Details
            /*-----------------------------------------------------*/
            if (objds.Tables[2].Rows.Count > 0)
            {
                GrdIncentive.DataSource = objds.Tables[2];
                GrdIncentive.DataBind();
            }
            else
            {
                GrdIncentive.DataSource = null;
                GrdIncentive.DataBind();
            }

            /*-----------------------------------------------------*/
            ///Bind Grievance Details
            /*-----------------------------------------------------*/
            if (objds.Tables[3].Rows.Count > 0)
            {
                GrdGrievance.DataSource = objds.Tables[3];
                GrdGrievance.DataBind();
            }
            else
            {
                GrdGrievance.DataSource = null;
                GrdGrievance.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {           
        }
    }

    ////// Button Back     
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("User_Enquire.aspx");
    }

    protected void GrdPEAL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Proposal_No = (HiddenField)e.Row.FindControl("Hid_Proposal_No");
            HyperLink HypLnk_Proposal_No = (HyperLink)e.Row.FindControl("HypLnk_Proposal_No");
            HypLnk_Proposal_No.NavigateUrl = "../Proposal/ProposalDetails.aspx?Pno=" + Hid_Proposal_No.Value;
        }
    }
    protected void GrdService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Service_App_No = (HiddenField)e.Row.FindControl("Hid_Service_App_No");
            HiddenField Hid_Service_Id = (HiddenField)e.Row.FindControl("Hid_Service_Id");
            HyperLink HyperLink2 = (HyperLink)e.Row.FindControl("HyperLink2");
            HyperLink2.NavigateUrl = "../Service/ServiceDetailsView.aspx?ApplicationNo=" + Hid_Service_App_No.Value + "&ServiceId=" + Hid_Service_Id.Value;
        }
    }
    protected void GrdGrievance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Griv_Id = (HiddenField)e.Row.FindControl("Hid_Griv_Id");
            HyperLink HyperLink4 = (HyperLink)e.Row.FindControl("HyperLink4");
            HyperLink4.NavigateUrl = "../Grievance/GrievanceApplicationDetails.aspx?GrivId=" + Hid_Griv_Id.Value + "&RequestId=5";
        }
    }
    protected void LnkBtn_View_Application_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Inct_Unique_No = (HiddenField)row.FindControl("Hid_Inct_Unique_No");
            HiddenField Hid_Form_Preview_Id = (HiddenField)row.FindControl("Hid_Form_Preview_Id");
            HiddenField Hid_Apply_Flag = (HiddenField)row.FindControl("Hid_Apply_Flag");

            if (Hid_Form_Preview_Id.Value != "")
            {
                if (Hid_Apply_Flag.Value == "True")
                {
                    string url = "../../Incentives/" + Hid_Form_Preview_Id.Value + "?InctUniqueNo=" + Hid_Inct_Unique_No.Value + "";
                    string strWindow = "window.open('" + url + "', 'popup_window');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", strWindow, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>No Form Preview Available for this Application as this application is in Draft Stage !</strong>', 'GOSWIFT');</script>", false);         
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "<script>jAlert('<strong>No Form Preview Available for this Application !</strong>','SWP');</script>", true);
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
}