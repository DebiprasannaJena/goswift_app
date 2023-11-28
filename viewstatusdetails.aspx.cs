using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ServiceModel;
using System.Web.Services;
using System.IO;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Specialized;

public partial class viewstatusdetails : System.Web.UI.Page
{
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["propid"]))
            {
                try
                {
                    BindGrid();
                }
                catch (NullReferenceException ex) { throw ex; }
                catch (Exception ex)
                { throw ex; }
                finally { }
            }
        }
    }

    private void BindGrid()
    {
        try
        {
            List<ProposalDet> objProposalList = new List<ProposalDet>();
            objProposal = new ProposalDet();
            objProposal.strAction = "N";
            objProposal.strProposalNo = Request.QueryString["propid"];
            objProposal.strRemarks = Request.QueryString["cafNo"];
            objProposalList = objService.getRaisedQueryDetails(objProposal).ToList();
            gvProposal.DataSource = objProposalList;
            gvProposal.DataBind();
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
}