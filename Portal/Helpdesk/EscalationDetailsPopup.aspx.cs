using System;
using System.Collections.Generic;
using BusinessLogicLayer.HelpDesk;

public partial class Portal_HelpDesk_EscalationDetailsPopup : System.Web.UI.Page
{
    IssueRegistration objswp = new IssueRegistration();
    List<IssueRegistration> lst = new List<IssueRegistration>();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDetails();
        }
    }

    #region "Fill Details"
    public void FillDetails()
    {
        List<IssueRegistration> lst1 = new List<IssueRegistration>();
        objswp.Action = "PO";
        objswp.int_SubcategoryId = Convert.ToInt32(Request.QueryString["SubId"]);
        lst1 = objlayer.ViewpopConfigEscalation(objswp);


        if (lst1.Count > 0)
        {
            lblComplaintType.Text = lst1[0].vch_CategoryName;
            lblSubComplaintType.Text = lst1[0].vch_SubCategoryName;
            gvEscalationDtls.DataSource = lst1;
            gvEscalationDtls.DataBind();
        }


    }

    #endregion
}