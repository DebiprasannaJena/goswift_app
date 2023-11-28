using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;

public partial class SingleWindow_ViewCommentHistory : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    public bool isVis { get; set; }
    static int intType = 0;
    public bool IsVisible
    {
        get { return isVis; }
        set { isVis = value; }
    }
    #endregion
    #region page load

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
                    intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                    ViewComments();
                }
            }
        }
        //if (intType == 4)
        //{
        //    tr1.Visible = true;
        //}
    }

    #endregion

    #region for view comments
    public void ViewComments()
    {
        try
        {
            objams.Action = "V";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            DataSet ds = new DataSet();
            ds = AMServices.ViewComments(objams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RptrComments.DataSource = ds.Tables[0];
                    RptrComments.DataBind();
                    lblMessage.Visible = false;
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    rptClarification.DataSource = ds.Tables[1];
                    rptClarification.DataBind();
                    IsVisible = true;
                }
                else
                    IsVisible = false;
            }
            else
            {
                lblMessage.Visible = true;
            }
        }
        catch (Exception m) { Response.Write(m.Message); }
    }
    #endregion

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string URL = "ViewClarification.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ProjectID=" + Request.QueryString["ID"] + "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "window.location.href='" + URL + "';", true);
    }
}