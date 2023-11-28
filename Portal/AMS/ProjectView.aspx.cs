//******************************************************************************************************************
// File Name             :   SingleWindow/ProjectView.aspx
// Description           :   To View project master
// Created by            :   Tapan Kumar Mishra
// Created on            :   26-July-2016
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_ProjectView : System.Web.UI.Page
{
    #region "Member Variable"

   DataTable dt = null;

    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                FillDetails();
            }
        }
    }
    #endregion

    #region "Fill Details"

    public void FillDetails()
    {
        AMS objams = new AMS();
        objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        objams.Action = "L";
        DataSet ds = new DataSet();
        ds = AMServices.ViewProjectMasterEdit(objams);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblName.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_NAME"]);
            DateTime DT = Convert.ToDateTime(ds.Tables[0].Rows[0]["DTMAPPLICATION_EBIZ"]);
            lblDate.Text = Convert.ToString(DT.Date.ToString("dd-MMM-yyyy"));
            //lblLocation.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_LOCATION"]);
            //lblProduct.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPRODUCT"]);
            lblCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["Category"]);
            lblType.Text = Convert.ToString(ds.Tables[0].Rows[0]["PROJECTTYPE"]);

            lblTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJECT_TITLE"]);
            lblSector.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHSECTOR"]);
            //lblOfficers.Text = Convert.ToString(ds.Tables[0].Rows[0]["Nodalofficer"]);
           
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtDirector = null;
                var rows = ds.Tables[1].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 1);

                if (rows.Any())
                {
                    dtDirector = rows.CopyToDataTable();
                    rptDirectors.DataSource = dtDirector;
                    rptDirectors.DataBind();

                }

                DataTable dtBusiness = null;
                var rows1 = ds.Tables[1].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 2);
                if (rows1.Any())
                {
                    dtBusiness = rows1.CopyToDataTable();
                    rptExisting.DataSource = dtBusiness;
                    rptExisting.DataBind();

                }
             
            }
            if (ds.Tables[2].Rows.Count > 0)
            {              
                GrdCapacity.DataSource = ds.Tables[2];
                GrdCapacity.DataBind();
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                     
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    Label label = new Label();
                    string District = ds.Tables[3].Rows[i]["District"].ToString();
                    string Loc = ds.Tables[3].Rows[i]["Location"].ToString();
                    string Com = District + "," + Loc + "<br/>";
                    label.Text = Com;
                    placeholder.Controls.Add(label);                   
                } 
            }
        }

    }
    #endregion
}
