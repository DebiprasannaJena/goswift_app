using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;

public partial class SingleWindow_ViewCmdComments : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    public bool isVis { get; set; }
    static int intType = 0;    
    #endregion
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
    }
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
                if (ds.Tables[2].Rows.Count > 0)
                {
                    RptrCMDComments.DataSource = ds.Tables[2];
                    RptrCMDComments.DataBind();
                    lblMessage.Visible = false;
                }
              
                if (ds.Tables[3].Rows.Count > 0)
                {
                    RptrCMDCmntRetrn.DataSource = ds.Tables[3];
                    RptrCMDCmntRetrn.DataBind();
                    lblMessage.Visible = false;
                }
               
            }
            else
            {
                lblMessage.Visible = true;
            }
            
        }
        catch (Exception m) { Response.Write(m.Message); }
    }

}