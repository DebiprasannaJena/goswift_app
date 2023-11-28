//******************************************************************************************************************
// File Name             :   SingleWindow/NodalOfficerAdd.aspx
// Description           :   To Tag Nodal Officers & SLFC Member
// Created by            :   Tapan Kumar Mishra
// Created on            :   18-July-2016
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SingleWindow_NodalOfficerAdd : System.Web.UI.Page
{
    #region "Member Variable"
   
    Agenda objcs = null;
    DataTable dt = null;
    string strVal = "";
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
                
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    FillDetails();
                    this.btnReset.Text = "Cancel";
                    this.btnSubmit.Text = "Update";                    
                }
                else
                {
                    this.btnReset.Text = "Reset";
                    this.btnSubmit.Text = "Save";                    
                }
            }
        }
    }

    #endregion


    #region "Button Event"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       

        if (ddlType.SelectedValue=="0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Please Select Stakeholders Type');", true);
            return;
        }

        if (hdnOfficer.Value.Length==0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('No officer(s) name to add');", true);
            return;
        }

        objcs = new Agenda();
        try
        {
            string URL = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                URL = "NodalOfficerView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&Pging=" + Request.QueryString["Pging"] + "&PgIndex=" + Request.QueryString["PgIndex"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
                objcs.Action = "U";
                objcs.Id = Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
            {
                URL = "NodalOfficerAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
                objcs.Action = "I";
                objcs.Id = 0;
            }

            objcs.OfficerType = Convert.ToInt32(ddlType.SelectedValue);

            string data = string.Empty;           

            objcs.OfficerId = hdnOfficer.Value.TrimStart('`').TrimEnd('`');
            objcs.CreatedBy = Convert.ToInt32(Session["UserId"]);
            strVal = AMServices.AddOfficers(objcs);

            string msg = Messages.ShowMessage(strVal).ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + msg + "');window.location.href='" + URL + "';", true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objcs = null; }
        
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Cancel")
        {
            Response.Redirect("NodalOfficerView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&Pging=" + Request.QueryString["Pging"] + "&PgIndex=" + Request.QueryString["PgIndex"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "");

        }
        else
        {
            string URL = "NodalOfficerAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
            Response.Redirect(URL);
        }
    }

    #endregion

    #region "Fill Details"
    public void FillDetails()
    {
        objcs = new Agenda();
        objcs.Action = "E";
        objcs.OfficerType = Convert.ToInt32(Request.QueryString["ID"]);
        dt = new DataTable();
        dt = AMServices.ViewOfficers(objcs);

        if (dt.Rows.Count > 0)
        {
            lbOfficer.DataSource = dt;
            lbOfficer.DataValueField = dt.Columns["intUserId"].ColumnName;
            lbOfficer.DataTextField = dt.Columns["Fullname"].ColumnName;
            lbOfficer.DataBind();

            string data = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                data = data + Convert.ToString(dr["intUserId"]) + "`";

            }           
           
            hdnOfficer.Value = data.TrimEnd('`');

            ddlType.SelectedValue = Convert.ToInt32(Request.QueryString["ID"]).ToString();
        }
    }
    #endregion

}