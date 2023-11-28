//******************************************************************************************************************
// File Name             :   SingleWindow/SLFCDecisionView.aspx
// Description           :   To View SLFC Decision Point details against a project id
// Created by            :   Tapan Kumar Mishra
// Created on            :   25-July-2016
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


public partial class SingleWindow_SLFCDecisionView : System.Web.UI.Page
{
    public string DecisionText { get; set; }

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
            objams.Action = "L";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            dt = AMServices.ViewDecision(objams);
            rptDecision.DataSource = dt;
            rptDecision.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblMessage.Visible = false;
                //lblName.Text = dt.Rows[0]["VCHPROJCT_NAME"].ToString();
                DecisionText = dt.Rows[0]["VCHDECISION"].ToString();
                rptDecision.DataBind();
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
}