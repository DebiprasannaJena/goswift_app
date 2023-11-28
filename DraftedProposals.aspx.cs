using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ServiceModel;
//using PropsalBALSVC;
using System.Web.Services;
using System.IO;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using DWHServiceReference;
using System.Configuration;

public partial class DraftedProposals : System.Web.UI.Page
{
    DataTable dtable;
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    string strRetval = "";
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridDraft();
            BindInvestorInfo();
        }
    }

    private void BindInvestorInfo()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
            DataTable dt = new DataTable();
            ProposalDet objProp = new ProposalDet();
            objProp.IntInvestorId = Convert.ToInt32(Session["InvestorId"].ToString());
            objProp.VCH_CONTACT_FIRSTNAME = "";
            objProp.VCH_EMAIL = "";
            objProp.VCH_OFF_MOBILE = "";
            objProp.strAction = "DIF";
            dt.Reset();
            dt = objService.INVESTORINFODISPLAY(objProp);
            if (dt.Rows.Count > 0 && Session["SkipCount"].ToString() == "0")
            {
                txtContactPersn.Text = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                txtEmailId.Text = dt.Rows[0]["VCH_EMAIL"].ToString();
                txtMobileNo.Text = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                ModalPopupExtender2.Show();
            }
            else
            {
                txtContactPersn.Text = "";
                txtEmailId.Text = "";
                txtMobileNo.Text = "";
                ModalPopupExtender2.Hide();
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftProposal");
        }
    }

    #region Member Function
    private void BindGridDraft()
    {
        objProposal = new ProposalDet();
        try
        {
            List<ProposalDet> objProposalList = new List<ProposalDet>();
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "D";
            objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProposalList = objService.getRaisedQueryDetails(objProp).ToList();
            if (objProposalList.Count > 0)
            {
                //lblLastUpdatedon.Text = objProposalList[0].dtmCreatedOn.ToString();
                gvDraftProposal.DataSource = objProposalList;
                gvDraftProposal.DataBind();
            }
            else
            {
                gvDraftProposal.DataSource = null;
                gvDraftProposal.DataBind();
            }
            intRetVal = gvDraftProposal.Rows.Count;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProposal = null;
        }
    }
    #endregion
    #region Event Click
    protected void gvDraftProposal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strProposalNo = gvDraftProposal.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink hprlnkproposal = (HyperLink)e.Row.FindControl("hypLink");
            hprlnkproposal.NavigateUrl = "~/PEAL/ProposalDetails.aspx?StrPropNo=" + strProposalNo.ToString();

            HyperLink hprlnkedit = (HyperLink)e.Row.FindControl("hypEdit");
            hprlnkedit.NavigateUrl = "~/PEAL/PromoterDetails.aspx?StrPropNo=" + strProposalNo.ToString();

        }
    }
    protected void gvDraftProposal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDraftProposal.PageIndex = e.NewPageIndex;
        BindGridDraft();
    }
    #endregion
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string gStrRetVal = null;
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProp = new ProposalDet();
        try
        {
            objProp.IntInvestorId = Convert.ToInt32(Session["InvestorId"].ToString());
            objProp.VCH_CONTACT_FIRSTNAME = txtContactPersn.Text;
            objProp.VCH_EMAIL = txtEmailId.Text;
            objProp.VCH_OFF_MOBILE = txtMobileNo.Text;
            objProp.strAction = "UIF";
            gStrRetVal = objService.INVESTORINFOUPDATE(objProp).ToString();
            if (gStrRetVal == "1")
            {
                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                DWH_Model objEnt = new DWH_Model();
                objEnt.INTUSERID = Convert.ToInt32(Session["DWHUserId"]);
                objEnt.VCHPROMOTERFNAME = txtContactPersn.Text;
                objEnt.VCHEMAILID = txtEmailId.Text;
                objEnt.VCHMOBILENO = txtMobileNo.Text;
                string strSecurityKey = objSrvRef.KeyEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"].ToString());
                string strReturnVal = objSrvRef.ProfileUpdateGoSmile(objEnt, strSecurityKey);
                Session["SkipCount"] = 1;
                ModalPopupExtender2.Hide();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Updated Successfully.')</script>;", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DraftProposal");
        }
        finally
        {
            gStrRetVal = null;
        }
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {
        Session["SkipCount"] = 1;
        ModalPopupExtender2.Hide();
    }

    protected void Linkclose_Click(object sender, EventArgs e)
    {
        Session["SkipCount"] = 1;
        ModalPopupExtender2.Hide();
    }
}