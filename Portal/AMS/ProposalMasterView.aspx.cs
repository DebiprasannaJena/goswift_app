//******************************************************************************************************************
// File Name             :   SingleWindow/ProposalMasterView.aspx
// Description           :   To View Propsal details against a project id
// Created by            :   Tapan Kumar Mishra
// Created on            :   21-July-2016
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_ProposalMasterView : System.Web.UI.Page
{

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


    #region "User function"

    private void FillDetails()
    {
        AMS objams = new AMS();
        DataTable dt = new DataTable(); 
        try
        {
            objams.Action = "E";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);            
            dt = AMServices.ViewProposalMaster(objams);
            GrdProposal.DataSource = dt;
            GrdProposal.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblMessage.Visible = false;
                lblName.Text = dt.Rows[0]["VCHPROJCT_NAME"].ToString();
            }
            else
            {
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; dt = null; }

    }

    #endregion
    protected void GrdProposal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                string encoded = e.Row.Cells[1].Text;
                e.Row.Cells[1].Text = Context.Server.HtmlDecode(encoded);
            }
        }
    }
}