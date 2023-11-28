using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;

public partial class IntermediateApplyServices : System.Web.UI.Page
{
   ProposalBAL objservice = new ProposalBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["FormId"] == null)
            {
                Response.Redirect("DepartmentClearance.aspx");
            }
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        intermidiateDetails(txtProposalid.Text);
    }

    private void intermidiateDetails(string proposalno)
    {

        List<PromoterDet> objProjList = new List<PromoterDet>();
       PromoterDet objProp = new PromoterDet();

        objProp.strAction = "C";
        objProp.vchProposalNo = proposalno;
        objProjList = objservice.Intermidiate(objProp).ToList();
        int intCount = objProjList.Count;
        if (intCount > 0)
        {
           
            Response.Redirect("FormView.aspx?FormId=" + Request.QueryString["FormId"].ToString() + "&ProposalNo=" + proposalno);
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Proposal  Data Valid successfully !');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Please enter valid Proposal No or Industry Code !');", true);
        }

    }
}