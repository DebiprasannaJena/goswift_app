using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for AMServices
/// </summary> 
public class AMServices
{
     private static  IAMServiceProvider eProvider;
     public static IAMServiceProvider Provider
    {
        get
        {
            eProvider = new AMServiceProvider();
            return eProvider;
        }
    }
	public AMServices()
	{
		
	}

    public static DataTable FillSector()
    {
        return Provider.FillSector();
    }

    public static DataTable FillSLFCChecklist(int ProjectId)
    {
        return Provider.FillSLFCChecklist(ProjectId);
    }

    #region "Add Project Master"
    public static string AddProjectMaster(AMS objAMS)
    {
        return Provider.AddProjectMaster(objAMS);
    }
    #endregion

    #region "View Project Masteredit"
    public static DataSet ViewProjectMasterEdit(AMS objAMS)
    {
        return Provider.ViewProjectMasterEdit(objAMS);
    }
    #endregion

    #region "View New Requested Project"
    public static DataSet ViewNewRequest(AMS objAMS)
    {
        return Provider.ViewNewRequest(objAMS);
    }
    #endregion


    #region "View Project Master"
    public static DataTable ViewProjectMaster(AMS objAMS)
    {
        return Provider.ViewProjectMaster(objAMS);
    }
    #endregion

    #region "View Project Status"
    public static DataSet ViewProjectSts(AMS objAMS)
    {
        return Provider.ViewProjectSts(objAMS);
    }
    #endregion

    //-------------------------------------PROPOSAL------------------------------------
    #region "Add Proposal Master"
    public static string AddProposalMaster(AMS objAMS)
    {
        return Provider.AddProposalMaster(objAMS);
    }
    #endregion

    #region "View Proposal Master"
    public static DataTable ViewProposalMaster(AMS objAMS)
    {
        return Provider.ViewProposalMaster(objAMS);
    }

    public static DataTable ViewSWPProposalMaster(AMS objAMS)
    {
        return Provider.ViewSWPProposalMaster(objAMS);
    }
    #endregion
    
    #region "Add Finance Master"
    public static string AddFinanceMaster(AMS objAMS)
    {
        return Provider.AddFinanceMaster(objAMS);
    }
    #endregion

    public static string AddFinanceDocument(AMS objAMS)
    {
        return Provider.AddFinanceDocument(objAMS);
    }

    #region "View Financial Performance"
    public static DataTable ViewFinancperfm(AMS objAMS)
    {
        return Provider.ViewFinancperfm(objAMS);
    }
    #endregion

    #region "View Financial year"
    public static DataSet ViewFinaceYr(AMS objAMS)
    {
        return Provider.ViewFinaceYr(objAMS);
    }
    public static DataTable ViewFinace(AMS objAMS)
    {
        return Provider.ViewFinace(objAMS);
    }

    public static DataTable ViewFinaceDoc(AMS objAMS)
    {
        return Provider.ViewFinaceDoc(objAMS);
    }


    public static DataTable ViewSWPFinace(AMS objAMS)
    {
        return Provider.ViewSWPFinace(objAMS);
    }

    public static DataTable ViewSWPFinaceDoc(AMS objAMS)
    {
        return Provider.ViewSWPFinaceDoc(objAMS);
    }
    #endregion

    #region "Manage Officers"
    public static string AddOfficers(Agenda objA)
    {
        return Provider.AddOfficers(objA);
    }
    public static DataTable ViewOfficers(Agenda objA)
    {
        return Provider.ViewOfficers(objA);
    }

    public static int GetOfficersType(int UserId)
    {
        return Provider.GetOfficersType(UserId);
    }

    #endregion

    #region "Project Details"
    public static DataTable FillActiveSWPProject(Agenda objA)
    {
        return Provider.FillActiveSWPProject(objA);
    }
    public static DataTable FillActiveProject(Agenda objA)
    {
        return Provider.FillActiveProject(objA);
    }
    public static string AddProjectDetails(Agenda objA)
    {
        return Provider.AddProjectDetails(objA);
    }
    public static DataSet ViewProjectDetailsMaster(Agenda objA)
    {
        return Provider.ViewProjectDetailsMaster(objA);
    }
    public static DataSet ViewSWPProjectDetailsMaster(Agenda objA)
    {
        return Provider.ViewSWPProjectDetailsMaster(objA);
    }
    public static DataTable ViewProjectDetails(Agenda objA)
    {
        return Provider.ViewProjectDetails(objA);
    }
    #endregion

    #region "Decision"
    public static string AddDecision(AMS objAMS)
    {
        return Provider.AddDecision(objAMS);
    }
    public static DataTable ViewDecision(AMS objAMS)
    {
        return Provider.ViewDecision(objAMS);
    }
    
    #endregion

      #region "ViewMom"
    public static DataSet ViewMom(AMS objAMS)
    {
        return Provider.ViewMom(objAMS);
    }
    #endregion

    #region "View Nodal Officer"
    public static DataTable ViewNodalOfficer(AMS objAMS)
    {
        return Provider.ViewNodalOfficer(objAMS);
    }
    #endregion

    #region "View Nodal Officer"
    public static string AddProjectPublish(AMS objAMS)
    {
        return Provider.AddProjectPublish(objAMS);
      
    }
    #endregion

    public static DataSet ViewSLFC(AMS objAMS)
    {
        return Provider.ViewSLFC(objAMS);
      
    }
    #region "View Comments & Clarification of SLFC member"
    public static DataSet ViewComments(AMS objAMS)
    {
        return Provider.ViewComments(objAMS);

    }
    #endregion
    #region "Add Comments"
    public static string AddComments(AMS objAMS)
    {
        return Provider.AddComments(objAMS);
    }
    #endregion

    #region "View Mail Info"
    public static DataSet ViewMailInfo(AMS objAMS)
    {
        return Provider.ViewMailInfo(objAMS);
    }
    #endregion

    #region "TakeAction"
    public static string TakeAction(AMS objAMS)
    {
        return Provider.TakeAction(objAMS);
    }
    #endregion
    public static DataSet ViewNodalOfficerMailinfo(AMS objAMS)
    {
        return Provider.ViewNodalOfficerMailinfo(objAMS);
    }

    public static DataTable GetDefaultMember(AMS objAMS)
    {
        return Provider.GetDefaultMember(objAMS);
    }

    public static DataTable ViewEmployeeDtls(string StrAction, string strText)
    {
        return Provider.ViewEmployeeDtls(StrAction, strText);
    }
    public static string UpdateProjectDetails(Agenda objA)
    {
        return Provider.UpdateProjectDetails(objA);
    }
    public static DataTable ViewFinanceDetails(Agenda objA)
    {
        return Provider.ViewFinanceDetails(objA);
    }

    public static string AddSLFCComments(AMS objAMS)
    {
        return Provider.AddSLFCComments(objAMS);
    }
    public static DataTable FillTermCondition()
    {
        return Provider.FillTermCondition();
    }
    public static string ActiveSLFCComments(Agenda objA)
    {
        return Provider.ActiveSLFCComments(objA);
    }
    public static DataTable FillDistrict()
    {
        return Provider.FillDistrict();
    }
    public static string AddCostDesc(AMS objAMS)
    {
        return Provider.AddCostDesc(objAMS);
    }
    public static DataTable FillCostDtls()
    {
        return Provider.FillCostDtls();
    }
    public static string ActiveCostDescription(Agenda objA)
    {
        return Provider.ActiveCostDescription(objA);
    }
    public static DataTable FillCostDetails()
    {
        return Provider.FillCostDetails();
    }
    public static string AddFinDtls(AMS objAMS)
    {
        return Provider.AddFinDtls(objAMS);
    }
    public static DataTable FillFinDtls()
    {
        return Provider.FillFinDtls();
    }
    public static string ActiveFinDescription(Agenda objA)
    {
        return Provider.ActiveFinDescription(objA);
    }
    public static DataTable FillFinDetails()
    {
        return Provider.FillFinDetails();
    }
    public static string UpdateClarification(AMS objAMS)
    {
        return Provider.UpdateClarification(objAMS);
    }
    public static DataTable ViewSlfcFeedback(AMS objAMS)
    {
        return Provider.ViewSlfcFeedback(objAMS);
    }
    public static string UpdateFeedback(AMS objAMS)
    {
        return Provider.UpdateFeedback(objAMS);
    }
    public static string UpdateClarificationGM(AMS objAMS)
    {
        return Provider.UpdateClarificationGM(objAMS);
    }
    public static string AddFinDetails(Agenda objA)
    {
        return Provider.AddFinDetails(objA);
    }
    public static DataSet ViewForwordDetails(Agenda objA)
    {
        return Provider.ViewForwordDetails(objA);
    }
    public static DataTable FillInactiveTermsConditions()
    {
        return Provider.FillInactiveTermsConditions();
    }
    public static DataTable FillActiveTermCondition()
    {
        return Provider.FillActiveTermCondition();
    }
    public static DataTable EditSLFCDiscussion(AMS objAMS)
    {
        return Provider.EditSLFCDiscussion(objAMS);
    }
    public static string UpdateComments(AMS objAMS)
    {
        return Provider.UpdateComments(objAMS);
    }

    public static DataTable FillForwardProject(AMS objA)
    {
        return Provider.FillForwardProject(objA);
    }

    public static string UpdateForwardProject(AMS objA)
    {
        return Provider.UpdateForwardProject(objA);
    }
    
    public static string Reopen_Published_Project(AMS objA)
    {
        return Provider.Reopen_Published_Project(objA);
    }

    public static string InsertStatus(AMS objA)
    {
        return Provider.InsertStatus(objA);
    }

    public static string InsertAccountantComment(AMS objA)
    {
        return Provider.InsertAccountantComment(objA);
    }

    public static DataTable FillAccountantComment(AMS objA)
    {
        return Provider.FillAccountantComment(objA);
    }

    public static DataSet GetProjectCnt(AMS objAMS)
    {
        return Provider.GetProjectCnt(objAMS);
    }
    public static string UpdateStatus(AMS objAMS)
    {
        return Provider.UpdateStatus(objAMS);
    }
}