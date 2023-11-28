using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Configuration.Provider;
using System.Data;

/// <summary>
/// Summary description for IAMServiceProvider
/// </summary>
public abstract class IAMServiceProvider : ProviderBase
{
	public IAMServiceProvider()
	{ 

	}

    public abstract DataTable FillSector();
    public abstract DataTable FillSLFCChecklist(int ProjectId);

    #region "Add Project Master"
    public abstract string AddProjectMaster(AMS objAMS);
    #endregion

    
    #region "View Project Master edit"
    public abstract DataSet ViewProjectMasterEdit(AMS objAMS);
    #endregion

    #region "View New Request from Singlw window portal"
    public abstract DataSet ViewNewRequest(AMS objAMS);
    #endregion

    #region "View Project Master"
    public abstract DataTable ViewProjectMaster(AMS objAMS);
    #endregion

    #region "View Project Status"
    public abstract DataSet ViewProjectSts(AMS objAMS);
    #endregion

    #region "Add Proposal Master"
    public abstract string AddProposalMaster(AMS objAMS);
    #endregion

    #region "View Proposal Master"
    public abstract DataTable ViewProposalMaster(AMS objAMS);
    public abstract DataTable ViewSWPProposalMaster(AMS objAMS);
    #endregion

    #region "Manage Officers"
    public abstract string AddOfficers(Agenda objA);
    public abstract DataTable ViewOfficers(Agenda objA);
    public abstract int GetOfficersType(int UserId);

    #endregion

    #region "Project Details"
    public abstract DataTable FillActiveSWPProject(Agenda objA);
    public abstract DataTable FillActiveProject(Agenda objA);
    public abstract string AddProjectDetails(Agenda objA);
    public abstract DataSet ViewProjectDetailsMaster(Agenda objA);
    public abstract DataSet ViewSWPProjectDetailsMaster(Agenda objA);
    public abstract DataTable ViewProjectDetails(Agenda objA);
    public abstract string UpdateProjectDetails(Agenda objA);
    #endregion

    
     #region "Add Finance Master"
    public abstract string AddFinanceMaster(AMS objAMS);
    #endregion

    public abstract string AddFinanceDocument(AMS objAMS);

    #region "View Financial Performance"
    public abstract DataTable ViewFinancperfm(AMS objAMS);
    public abstract DataTable ViewFinace(AMS objAMS);
    public abstract DataTable ViewFinaceDoc(AMS objAMS);
    public abstract DataTable ViewSWPFinace(AMS objAMS);
    public abstract DataTable ViewSWPFinaceDoc(AMS objAMS);
    #endregion

    #region "Financial Performance year"
    public abstract DataSet ViewFinaceYr(AMS objAMS);
    #endregion

     #region "Decision"
    public abstract string AddDecision(AMS objAMS);
    public abstract DataTable ViewDecision(AMS objAMS);

    #endregion

    //MOM
     #region "View MOM"
    public abstract DataSet ViewMom(AMS objAMS);
    #endregion
    
    #region "View Nodal Officer"
    public abstract DataTable  ViewNodalOfficer(AMS objAMS);
    #endregion

   #region "add project for publish"
    public abstract string AddProjectPublish(AMS objAMS);
   #endregion

    public abstract DataSet ViewSLFC(AMS objAMS);

    #region "ViewComments"
    public abstract DataSet ViewComments(AMS objAMS);
    #endregion
    #region "Add Comments"
    public abstract string AddComments(AMS objAMS);
    #endregion
    #region "View for Mail Info"
    public abstract DataSet ViewMailInfo(AMS objAMS);
    #endregion
 
    #region "TakeAction"
    public abstract string TakeAction(AMS objAMS);
    #endregion
   
    #region "Nodal Officer Mail Info"
    public abstract DataSet ViewNodalOfficerMailinfo(AMS objAMS);
    #endregion
    public abstract DataTable GetDefaultMember(AMS objAMS);

    public abstract DataTable ViewEmployeeDtls(string StrAction, string strText);
    public abstract DataTable ViewFinanceDetails(Agenda objA);

    #region "Add Project Master"
    public abstract string AddSLFCComments(AMS objAMS);
    public abstract DataTable FillTermCondition();
    public abstract string ActiveSLFCComments(Agenda objA);
    public abstract DataTable FillDistrict();
    public abstract string AddCostDesc(AMS abjAMS);
    public abstract DataTable FillCostDtls();
    public abstract string ActiveCostDescription(Agenda objA);
    public abstract DataTable FillCostDetails();

    public abstract string AddFinDtls(AMS abjAMS);
    public abstract DataTable FillFinDtls();
    public abstract string ActiveFinDescription(Agenda objA);
    public abstract DataTable FillFinDetails();
    public abstract string UpdateClarification(AMS abjAMS);
    public abstract DataTable ViewSlfcFeedback(AMS objAMS);
    public abstract string UpdateFeedback(AMS abjAMS);
    public abstract string UpdateClarificationGM(AMS abjAMS);
    public abstract string AddFinDetails(Agenda objA);
    #endregion
    public abstract DataSet ViewForwordDetails(Agenda objA);

    public abstract DataTable FillInactiveTermsConditions();
    public abstract DataTable FillActiveTermCondition();
    public abstract DataTable EditSLFCDiscussion(AMS objAMS);
    public abstract string UpdateComments(AMS objAMS);

   
    public abstract DataTable FillForwardProject(AMS objA);
    public abstract string UpdateForwardProject(AMS objA);

    public abstract string Reopen_Published_Project(AMS objA);

    public abstract string InsertStatus(AMS objA);

    public abstract string InsertAccountantComment(AMS objA);
    public abstract DataTable FillAccountantComment(AMS objA);

    public abstract DataSet GetProjectCnt(AMS objAMS);

    public abstract string UpdateStatus(AMS objAMS);
}
    
