using EntityLayer.GrievanceEntity;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class GrievanceNonIndustry  : System.Web.UI.Page
{
    DataTable Dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/LogOut.aspx", true);
        }

        if (!IsPostBack)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Grievance");
            }
        }
    }

    private void BindGrid()
    {
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BGI",
                intInvestorId = Convert.ToInt32(Session["InvestorId"].ToString())
            };

            Dt = GrievanceServices.DisplayInvestorGrievanceDetail(objSearch);
            GrdGrivDetails.DataSource = Dt;
            GrdGrivDetails.DataBind();
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    protected void GrdGrivDetails_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        try
        {
            GrdGrivDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grievance");
        }
    }
    protected void GrdGrivDetails_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hprlnkgrievance = (HyperLink)e.Row.FindControl("hypLink");
            hprlnkgrievance.NavigateUrl = "~/Grievance/GrievanceDetailsNonIndustry.aspx?StrGrievanceNo=" + GrdGrivDetails.DataKeys[e.Row.RowIndex].Values["vchGrivId"];

            /*-------------------------------------------------------------------------------------------*/

            HiddenField Hid_Status = (HiddenField)e.Row.FindControl("Hid_Status");
            Label LblStatus = (Label)e.Row.FindControl("LblStatus");
            if (Hid_Status.Value == "3")
            {
                LblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (Hid_Status.Value == "13")
            {
                LblStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LblStatus.ForeColor = System.Drawing.Color.Orange;
            }

            /*-------------------------------------------------------------------------------------------*/

            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e8eded';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
        }
    }

    protected void Btn_Add_Griv_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Grievance/AddGrievanceNonIndustry.aspx");
    }
}
