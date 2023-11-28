using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;

public partial class SingleWindow_SLFCDecisionAdd : System.Web.UI.Page
{
    #region "Member Variable"

    static int rowIndex = -1;
    AMS objams = null;
    string strVal = "";
    DataTable dt = null;
    Agenda objcs = null;
    //List<Dicision> objItemDetails;
    //Dicision objItem;
    int intType = 0;
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
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                //btnSubmit.Enabled = false;
                //txtDecision.Enabled = false;
                FillProject();
                FillChecklist(Convert.ToInt32(ddlProject.SelectedValue));
            }
        }
    }

    #endregion

    #region "Fill Project"

    private void FillProject()
    {
        try
        {
            objcs = new Agenda();
            objcs.Action = "DF";
            objcs.OfficerType = intType;
            objcs.UserId = Convert.ToInt32(Session["UserId"]);
            dt = new DataTable();
            dt = AMServices.FillActiveProject(objcs);
            ddlProject.DataSource = dt;
            ddlProject.DataTextField = "PROJECTNAME";
            ddlProject.DataValueField = "INTPROJCTID";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objcs = null; dt = null; }
    }

    #endregion

    #region "Fill Checklist"

    private void FillChecklist(int ProjectId)
    {
        try
        {
            dt = new DataTable();
            dt = AMServices.FillSLFCChecklist(ProjectId);
            cbDecision.DataSource = dt;
            cbDecision.DataTextField = "ChecklistPoint";
            cbDecision.DataValueField = "ChecklistId";
            cbDecision.DataBind();
            if (cbDecision.Items.Count > 0)
                btnSubmit.Enabled = true;
            else
                btnSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objcs = null; dt = null; }
    }

    #endregion

    #region "Dropdown Event"

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlProject.SelectedValue) == 0)
        {
            txtDecision.Text = string.Empty;
            cbDecision.DataSource = null;
            cbDecision.DataBind();
            this.btnReset.Text = "Reset";
            this.btnSubmit.Text = "Submit";
        }
        else
        {
            txtDecision.Text = "";
            txtDecision.Focus();
            FillChecklist(Convert.ToInt32(ddlProject.SelectedValue));
            FillDetails(Convert.ToInt32(ddlProject.SelectedValue));
        }
    }

    #endregion


    #region "User function"

    private void FillDetails(int ProjectId)
    {
        objams = new AMS();
        try
        {
            objams.Action = "E";
            objams.ProjectId = ProjectId;
            dt = new DataTable();
            dt = AMServices.ViewDecision(objams);
            if (dt.Rows.Count > 0)
            {
                txtDecision.Text = dt.Rows[0]["VCHDECISION"].ToString();
                btnSubmit.Text = "Update";
                btnReset.Text = "Cancel";
                foreach (DataRow dr in dt.Rows)
                {
                    cbDecision.Items.FindByValue(dr["CHECKLISTID"].ToString()).Selected = true;                    
                }
            }
            else
            {
                this.btnReset.Text = "Reset";
                this.btnSubmit.Text = "Save";
                foreach (ListItem li in cbDecision.Items)
                {
                    li.Selected = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { objams = null; }

    }
    
    #endregion



    #region "Button Event"


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (cbDecision.Items.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyScript", "alert('Please Check at least one Decision to save..');", true);
            return;
        }
        objams = new AMS();
        string Decision = string.Empty;
        try
        {

            if (btnSubmit.Text == "Update")
                objams.Action = "U";
            else
                objams.Action = "A";

            objams.ProjectId = Convert.ToInt32(ddlProject.SelectedValue);
            objams.DECISION = txtDecision.Text.Trim();
            foreach (ListItem item in cbDecision.Items)
            {
                if (item.Selected)
                {
                    Decision = Decision + item.Value + '~';
                }
            }
            Decision = Decision == "" ? "0" : Decision.Trim('~');
            objams.DecisionPoint = Decision;
            objams.CreatedBy = Convert.ToInt16(Session["UserId"]);

            strVal = AMServices.AddDecision(objams);
            string msg = Messages.ShowMessage(strVal).ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + msg + "');window.location.href='SLFCDecisionAdd.aspx?ranNum=" + Request.QueryString["ranNum"] + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "';", true);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SLFCDecisionAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "");
    }

    #endregion


}