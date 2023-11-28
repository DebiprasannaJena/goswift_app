#region
//Added by nibedita behera on 17-09-2017 for Admin View proposal district block wise 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Portal_Proposal_ViewAdminProposal : SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();

    int intRetVal = 0;
    int intRecordCount = 0;
    string strRetval = "";

    bool smsStatus;
    bool mailStatus;
   

    /////Page Load    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                fillGridview();
                BindDistrict();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ViewProposal");
            }
        }
    }

    #region "Display Google Paging"

    private void DisplayPaging()
    {
        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + intRecordCount + "</b> Of <b>" + intRecordCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvService.Rows[0].Cells[0].Text) + (gvService.PageSize - 1)) + "</b> Of <b>" + intRecordCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }

    #endregion

    #region FunctionUsed

    private void BindDistrict()
    {
        try
        {
           
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlDistrict.DataSource = objProjList;
            ddlDistrict.DataTextField = "vchDistName";
            ddlDistrict.DataValueField = "intDistId";
            ddlDistrict.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindBlock(string strdist)
    {
        try
        {
            
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "BL";
            objProp.vchProposalNo = strdist;
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlBlock.DataSource = objProjList;
            ddlBlock.DataTextField = "vchBlockName";
            ddlBlock.DataValueField = "intBlockId";
            ddlBlock.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlBlock.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void fillGridview()
    {
        try
        {

            objProposal.strAction = "V";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            List<ProposalDet> objProposalList = objService.getAdminProposalDetails(objProposal).ToList();

            if (objProposalList.Count > 0)
            {
                gvService.DataSource = objProposalList;
                gvService.DataBind();

                intRetVal = gvService.Rows.Count;
                intRecordCount = objProposalList.Count;
                DisplayPaging();
            }
            else
            {
                gvService.DataSource = null;
                gvService.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objProposal = null;
        }
    }

    #endregion

    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //// Create serial number and used for paging text.
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);

            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "Large Industries";
            }
            else if (e.Row.Cells[2].Text == "2")
            {
                e.Row.Cells[2].Text = "MSME";
            }

            string strProposalNo = gvService.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink hprlnkproposal = (HyperLink)e.Row.FindControl("hypLink");
            hprlnkproposal.NavigateUrl = "../Proposal/ProposalDetails.aspx?Pno=" + strProposalNo;

            //Added by nibedita behera on 16-09-2017
            LinkButton lnkbtnEdit = (e.Row.FindControl("lnkbtnEdit") as LinkButton);
            string status = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[1]);
            if (status == "Applied")
            {
                lnkbtnEdit.Visible = true;
            }
            else
            {
                lnkbtnEdit.Visible = false;
            }
        }
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        fillGridview();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindBlock(ddlDistrict.SelectedValue);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewProposal");
        }
    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                gvService.PageIndex = 0;
                gvService.AllowPaging = false;
                fillGridview();
            }
            else
            {
                lbtnAll.Text = "All";
                gvService.AllowPaging = true;
                fillGridview();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewProposal");
        }
    }
    protected void btnEditDist_Click(object sender, EventArgs e)
    {
        LandDet objlanDet = new LandDet();
        try
        {
            objlanDet.strAction = "U";
            objlanDet.vchProposalNo = hdnproposalno.Value;
            objlanDet.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            objlanDet.intBlockId = Convert.ToInt32(ddlBlock.SelectedValue.ToString());
            objlanDet.intCreatedBy = Convert.ToInt32(Session["UserId"]);
           
            strRetval = objService.UpdateLandApproval(objlanDet);
            if (strRetval == "2")
            {
                CommonHelperCls comm = new CommonHelperCls();
                LandDet objProp = new LandDet();

                objProp.strAction = "M";
                objProp.vchProposalNo = hdnCreted.Value;
                objProp.ApplicationNo = hdnproposalno.Value;
                List<LandDet> objProjList = objService.GETMobileNo(objProp).ToList();
                string mobile = "";
                string smsContent = "";
                string[] toEmail = new string[1];
                if (objProjList.Count > 0)
                {
                    mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                    smsContent = Convert.ToString(objProjList[0].SMSContent);
                    toEmail[0] = Convert.ToString(objProjList[0].Email);
                    string strSubject = "SWP: Change of Proposed location";
                    string strBody = "Department has changed the proposed location for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " ,Please Log into https://invest.odisha.gov.in for further details. ";
                   
                    mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                    smsStatus = comm.SendSmsNew(mobile, strBody);
                    // FOR SMS and Mail Status Update

                    comm.UpdateMailSMSStaus("PEAL Change of Proposed location", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", 1, hdnproposalno.Value, strBody, strBody, smsStatus, mailStatus);
                    // FOR SMS and Mail Status Update
                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Record(s) saved successfully.</strong>';location.href = 'ViewAdminProposal.aspx'; '" + Messages.TitleOfProject + "'); </script>", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewProposal");
        }
        finally
        {
            objlanDet = null;
        }
    }
}