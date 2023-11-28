//******************************************************************************************************************
// File Name             :   DepartmentClearance.aspx.cs
// Description           :   Show the clearance details of Investors from various departments
// Created by            :   AMit Sahoo
// Created on            :   30th June 2017
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
using DataAcessLayer.Service;

public partial class DepartmentClearance : SessionCheck
{
    #region Variables

    BusinessLogicLayer.Service.ServiceBusinessLayer objService = new BusinessLogicLayer.Service.ServiceBusinessLayer();
    EntityLayer.Service.ServiceDetails objServiceEntity = new EntityLayer.Service.ServiceDetails();

    static int intInvestorid;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (!IsPostBack)
        {
            intInvestorid = Convert.ToInt32(Session["InvestorId"]);
            FillProposal(intInvestorid);          
            LblIndustryName.Text = Session["IndustryName"].ToString();
        }
    }

    #region FunctionUsed

    private void FillProposal(int intInvestorId)
    {
        try
        {
            List<EntityLayer.Service.ServiceDetails> ServiceDetail = objService.FillProposalId(intInvestorId).ToList();
           

            if (ServiceDetail.Count > 0)
            {
                DdlProposal.DataSource = ServiceDetail;
                DdlProposal.DataTextField = "strProposalId";
                DdlProposal.DataValueField = "strProposalId";
                DdlProposal.DataBind();
            }
            else
            {
                DdlProposal.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            ///Bind Gridview
            BindGrid(DdlProposal.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    private void BindGrid(string strProposalId)
    {
        try
        {
            /*-------------------------------------------------------------------*/
            ///Get the list of external and internal services         
            /*-------------------------------------------------------------------*/
            objServiceEntity.intProposalId = strProposalId;
            List<EntityLayer.Service.ServiceDetails> ServiceDetail = objService.ViewDepartmentWiseServiceDetails(objServiceEntity).ToList();

            /*-------------------------------------------------------------------*/
            ///Filter the Internal Services and Bind on 1st Grid
            /*-------------------------------------------------------------------*/
            List<EntityLayer.Service.ServiceDetails> ObjInternalSvc = ServiceDetail.Where(n => n.Int_ServiceType == 0).ToList();
            GrdInternalService.DataSource = ObjInternalSvc;
            GrdInternalService.DataBind();

            /*-------------------------------------------------------------------*/
            ///Filter the External Services and Bind on 2nd Grid
            /*-------------------------------------------------------------------*/
            List<EntityLayer.Service.ServiceDetails> ObjExternalSvc = ServiceDetail.Where(n => n.Int_ServiceType == 1).ToList();
            GrdExternalService.DataSource = ObjExternalSvc;
            GrdExternalService.DataBind();

            Session["SvcMasterData"] = null;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }   
   

    #endregion

    protected void GrdInternalService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            CheckBox ChkBxSelect = (CheckBox)e.Row.FindControl("ChkBxSelect");

            HiddenField Hid_Disable_Status = (HiddenField)e.Row.FindControl("Hid_Disable_Status");

            int intServiceType = Convert.ToInt32(GrdInternalService.DataKeys[e.Row.RowIndex].Values["Int_ServiceType"]);
            int intExternalType = Convert.ToInt32(GrdInternalService.DataKeys[e.Row.RowIndex].Values["intExternalType"]);

            if (intServiceType == 0 && intExternalType == 0)
            {
                e.Row.Attributes.Add("title", "Internal Service");
            }
            else if (intServiceType == 0 && intExternalType == 1)
            {
                e.Row.Attributes.Add("title", "External Service");
            }

            /*--------------------------------------------------------------------------*/
            ///If the service is already appplied, then disable the checkbox.
            /*--------------------------------------------------------------------------*/
            if (Convert.ToBoolean(Hid_Disable_Status.Value))
            {
                ChkBxSelect.Enabled = true;
            }
            else
            {
                ChkBxSelect.Enabled = false;
            }

            /// Added by Sushant Jena On Dated:-18-Nov-2020
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e8eded';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
        }
    }
    protected void GrdExternalService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = ((GrdExternalService.PageIndex * GrdExternalService.PageSize) + e.Row.RowIndex + 1).ToString();

            HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hypApply");
            string strProposalId = DdlProposal.SelectedValue.ToString();

            string strServiceId = Convert.ToString(GrdExternalService.DataKeys[e.Row.RowIndex].Values["intServiceId"]);
            decimal decAmount = Convert.ToDecimal(GrdExternalService.DataKeys[e.Row.RowIndex].Values["Dec_Amount"]);
            int intServiceType = Convert.ToInt32(GrdExternalService.DataKeys[e.Row.RowIndex].Values["Int_ServiceType"]);

            hyRcvd.NavigateUrl = "ServiceInstructionExternal.aspx?ReqMode=S&FormId=" + strServiceId + "&ProposalNo=" + strProposalId + "&Amount=" + decAmount + "&ServiceType=" + intServiceType;

            e.Row.Attributes.Add("title", "External Service");


           
            ///Added by Sushant Jena On Dated:-18-Nov-2020
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#e8eded';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
        }
    }

    protected void DdlProposal_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(DdlProposal.SelectedValue.ToString());
    }

    protected void BtnApplyMultiple_Click(object sender, EventArgs e)
    {
        try
        {
            int intCheckStatus = 0;
            int intFormCount = 0;

            /*-----------------------------------------------------------------------------------*/
            /// The Datatable is created to store required information of clubbed services.
            /// This data will be transfred to next page through Session variable and according to the status the required pages will be loaded.
            /// Column Specifications are like below:
            /// intSlNo           :- Serial No
            /// intServiceId      :- Service Id of the selected applications.
            /// vchFormName       :- To mainatain a shortcut name for the services like Form-1,Form-2 and so on.
            /// vchServiceName    :- Name of the service.
            /// intServiceType    :- Service Type (0- Internal, 1-External).
            /// intExternalType   :- Is the service is external type or not (0- Pure Internal, 1-External).
            /// vchProposalNo     :- Proposal No.
            /// decAmount         :- It will store calculated amount for each services.The calculated amount will be updated from next page.           
            /// intCompletedStatus:- To maintain form submission status (1-Submitted,0-Draft).
            /// vchApplicationKey :- Unique appilcation number of the each services applied.
            /// vchUrl            :- Store dynamic url for respective service.
            /*-----------------------------------------------------------------------------------*/
            DataTable dt = new DataTable();

            dt.Columns.Add("intSlNo", typeof(int));
            dt.Columns.Add("intServiceId", typeof(string));
            dt.Columns.Add("vchFormName", typeof(string));
            dt.Columns.Add("vchServiceName", typeof(string));
            dt.Columns.Add("intServiceType", typeof(string));
            dt.Columns.Add("intExternalType", typeof(string));
            dt.Columns.Add("vchProposalNo", typeof(string));
            dt.Columns.Add("decAmount", typeof(string));
            dt.Columns.Add("intCompletedStatus", typeof(string));
            dt.Columns.Add("vchApplicationKey", typeof(string));
            dt.Columns.Add("vchUrl", typeof(string));
            dt.Columns.Add("vchUpdateUrl", typeof(string));
            dt.Columns.Add("vchDeptName", typeof(string));
            dt.Columns.Add("intHoaAccount", typeof(string));
            dt.Columns.Add("vchTrackingId", typeof(string));

            string strProposalNo = DdlProposal.SelectedValue.ToString();

            for (int i = 0; i < GrdInternalService.Rows.Count; i++)
            {
                int intServiceId = Convert.ToInt32(GrdInternalService.DataKeys[i].Values["intServiceId"]);
                decimal decAmount = Convert.ToDecimal(GrdInternalService.DataKeys[i].Values["Dec_Amount"]);
                int intServiceType = Convert.ToInt32(GrdInternalService.DataKeys[i].Values["Int_ServiceType"]);
                int intExternalType = Convert.ToInt32(GrdInternalService.DataKeys[i].Values["intExternalType"]);
                string vchurl = GrdInternalService.DataKeys[i].Values["Str_ExtrnalServiceUrl"].ToString();
                CheckBox ChkBxSelect = (CheckBox)GrdInternalService.Rows[i].FindControl("ChkBxSelect");
                Label LblServiceName = (Label)GrdInternalService.Rows[i].FindControl("lblService");
                Label LblDeptName = (Label)GrdInternalService.Rows[i].FindControl("lblDept");

                if (ChkBxSelect.Checked)
                {
                    intCheckStatus = 1;
                    intFormCount++;
                    string strFormName = LblServiceName.Text.Length > 20 ? LblServiceName.Text.Substring(0, 20) + "..." : LblServiceName.Text;
                    dt.Rows.Add(intFormCount, intServiceId, strFormName, LblServiceName.Text, intServiceType, intExternalType, strProposalNo, decAmount, 0, "", vchurl, "", LblDeptName.Text, 0, "");
                }
            }

            if (intCheckStatus == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "jAlert('<strong>Please select at least one check box to proceed !</strong>');", true);
                return;
            }

            Session["SvcMasterData"] = dt;
            GrdInstruction.DataSource = dt;
            GrdInstruction.DataBind();

            DepartmentClsModalPopup.Show();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Response.Redirect("ServiceProcess.aspx?ReqMode=M", false); ////M-Multiple Apply, S-Single Apply
    }

    protected void BtnNo_Click(object sender, EventArgs e)
    {
        Session["SvcMasterData"] = null;
        DepartmentClsModalPopup.Hide();
    }
}