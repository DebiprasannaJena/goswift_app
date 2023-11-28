using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class CheckProposal : SessionCheck
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] != null)
        {
           
          //  Server.Transfer("inestorlogin.aspx?serviceid=" + Request.QueryString["Srvcid"] + "");
        }
        else
        {
            Response.Redirect("inestorlogin.aspx?returnurl=CheckProposal.aspx?id=" + Request.QueryString["id"] + "");
        }


        if (!IsPostBack)
        {
            try
            {
                int investorid = Convert.ToInt32(Session["InvestorId"].ToString());
                DataTable dt = objService.ChkProposal("CK", investorid);
                if (dt.Rows.Count > 1)
                {
                    ddlproposal.Visible = true;
                    CommonHelperCls ob = new CommonHelperCls();
                    ob.BindDropDown(ddlproposal, "vchProposalNo", "vchProposalNo", " select vchProposalNo from T_PEAL_PROMOTER where intCreatedBy=" + investorid + "  and bitDeletedStatus=0");
                   
                }
                else if (dt.Rows.Count == 1)
                {
                    //Label1.Visible = false;
                    ddlproposal.Visible = true;
                    CommonHelperCls ob = new CommonHelperCls();
                    ob.BindDropDown(ddlproposal, "vchProposalNo", "vchProposalNo", " select vchProposalNo from T_PEAL_PROMOTER where intCreatedBy=" + investorid + "  and bitDeletedStatus=0");
                    ddlproposal.SelectedIndex = 1;
                    btnproceed.Visible = true;
                }
                else
                {
                    DataTable dtservicenew = objService.GetServiceDetails("GSD", Convert.ToInt32(Request.QueryString["id"].ToString()));
                    Response.Redirect("ServiceInstruction.aspx?FormId=" + Request.QueryString["id"].ToString() + "&ProposalNo=0&Amount=0&ServiceType=" + dtservicenew.Rows[0]["INT_SERVICE_TYPE"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    //protected void ddlproposal_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btnproceed.Visible = true;
    //}
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtservice = objService.GetServiceDetails("GSD", Convert.ToInt32(Request.QueryString["id"].ToString()));
            Response.Redirect("ServiceInstruction.aspx?FormId=" + Request.QueryString["id"].ToString() + "&ProposalNo=" + ddlproposal.SelectedValue.ToString() + "&Amount=0&ServiceType=" + dtservice.Rows[0]["INT_SERVICE_TYPE"]);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
   
        }
}