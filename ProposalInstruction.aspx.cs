using BusinessLogicLayer.Proposal;
using DWHServiceReference;
using EntityLayer.Proposal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProposalInstruction : SessionCheck
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindInvestorInfo();

            if (Session["NswsInvSwsId"] != null && Session["NswsInvSwsId"] != "")
            {
                ///// Pull State CAF from NSWS portal and Populate at respective places.
                string strInvSwsId = Convert.ToString(Session["NswsInvSwsId"]);
                PullStateCafNsws(strInvSwsId);
            }
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
            Util.LogError(ex, "ProposalInstruction");
        }
    }

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
            Util.LogError(ex, "ProposalInstruction");
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
    protected void btnApply_Click(object sender, EventArgs e)
    {
        Session["proposalno"] = null;
        Response.Redirect("~/PEAL/PromoterDetails.aspx");
    }

    private void PullStateCafNsws(string strInvestorSwsId)
    {
        PromoterDet objEntity = new PromoterDet();
        ProposalBAL objBAL = new ProposalBAL();
        try
        {
            objEntity.strAction = "B";
            objEntity.strInvestorSWSId = strInvestorSwsId;
            DataTable dt = new DataTable();
            dt = objBAL.GetCAFDetailsNSWS(objEntity);
            if (dt.Rows.Count == 0)
            {
                /////Pull State CAF from NSWS Portal.
                NSWSScheduler objNSWS = new NSWSScheduler();
                string strReturnStatus = objNSWS.PullAndInsertStateCAF(strInvestorSwsId);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalInstruction");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }
}