using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;

public partial class Portal_MISReport_Peal_Yearly_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonBindDropDown(drpDepartment, "DD", string.Empty);
            int intStartYear = 2015;
            int intEndYear = DateTime.Now.Year;
            int intCurrMonth = DateTime.Now.Month;
            if (intCurrMonth <= 3)
            {
                intEndYear = intEndYear - 1;
            }
            while (intStartYear <= intEndYear)
            {
                drpFinancialYear.Items.Add(new ListItem(intStartYear.ToString() + "-" + (intStartYear + 1).ToString().Substring(2, 2), intStartYear.ToString()));
                intStartYear++;
            }
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpService.Items.Clear();
        if (drpDepartment.SelectedValue != "0")
        {
            CommonBindDropDown(drpService, "SS", drpDepartment.SelectedValue);
        }
    }

    private void CommonBindDropDown(DropDownList drp, string strActionCode, string strProposalNo)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProposalBAL objProposalBal = new ProposalBAL();
        ProjectInfo objProp = new ProjectInfo()
        {
            strAction = strActionCode,
            vchProposalNo = strProposalNo
        };

        objProjList = objProposalBal.PopulateProjDropdowns(objProp).ToList();
        drp.DataSource = objProjList;
        drp.DataTextField = "vchserviceName";
        drp.DataValueField = "intserviceid";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        //IncentiveCommonFunctions.ExportToExcel("ChildServicesRpt", grdDepartment, "Report on Child Services", lblSearchDetails.Text + "<br/> As on date - " + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        //IncentiveCommonFunctions.CreatePdf("ChildServicesRpt", grdDepartment);
    }
}