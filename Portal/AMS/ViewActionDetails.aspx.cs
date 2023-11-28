using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_ViewActionDetails : System.Web.UI.Page
{
    #region "Member Variable"

    Agenda objcs = null;
    DataTable dt = null;
    Double x = 0;
    Double y = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //FillDetails();
        FillForwordDtls();
    }
    //public void FillDetails()
    //{
    //    objcs = new Agenda();
    //    objcs.Action = "V";
    //    objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
    //    dt = new DataTable();
    //    dt = AMServices.ViewProjectDetails(objcs);

    //    if (dt.Rows.Count > 0)
    //    {
    //        lblproject.Text = dt.Rows[0]["VCHPROJCT_NAME"].ToString();
    //        lblAddedOn.Text = dt.Rows[0]["CREATEDON"].ToString();     
    //    }
    //}
    public void FillForwordDtls()
    {
        objcs = new Agenda();
        objcs.Action = "AD";
        objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        DataSet ds = new DataSet();
        ds = AMServices.ViewForwordDetails(objcs);

        if (ds.Tables[3].Rows.Count > 0)
        {
            lblproject.Text = ds.Tables[3].Rows[0]["VCHPROJCT_NAME"].ToString();
            lblAddedOn.Text = ds.Tables[3].Rows[0]["CREATEDON"].ToString();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            GrdProjectCostDtls.DataSource = ds.Tables[0];
            GrdProjectCostDtls.DataBind();
            
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            GrdSlfcCommnet.DataSource = ds.Tables[1];
            GrdSlfcCommnet.DataBind();
            LblSlfcComnts.Visible = false;
        }
        else
        {
            LblSlfcComnts.Visible = true;
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            grvClarificationDtls.DataSource = ds.Tables[2];
            grvClarificationDtls.DataBind();
            LblClarification.Visible = false;
        }
        else
        {
            LblClarification.Visible = true;
        }
    }



}