using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

using System.Data;


/// <summary>
/// Summary description for IHelpdeskBusinessLayer
/// </summary>
namespace BusinessLogicLayer.HelpDesk
{
    public interface IHelpdeskBusinessLayer
    {
        [OperationContract]
        List<IssueRegistration> BindCategory(IssueRegistration objDdl);
        [OperationContract]
         List<IssueRegistration> BindType(IssueRegistration objDdl);
        [OperationContract]
        List<IssueRegistration> BindSubCategory(IssueRegistration objDdl);
        [OperationContract]
        List<IssueRegistration> BindDepartment(IssueRegistration objDdl);
        [OperationContract]
        List<IssueRegistration> BindUser(IssueRegistration objDdl);
        [OperationContract]
        List<IssueRegistration> BindInvestor(IssueRegistration objDdl);
        [OperationContract]
        List<IssueRegistration> AddIssueRegister(IssueRegistration objMasterSector);
        [OperationContract]
        List<IssueRegistration> ViewIssueRegistration(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> ViewFile(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> HelpdeskCategory_Report(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> HelpdeskSubCategory_Report(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> Helpdesk_Report(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> Helpdesk_Status_Report(IssueRegistration obj);
        [OperationContract]
        string AddIssueCategory(IssueRegistration objMaster);
        [OperationContract]
        List<IssueRegistration> ViewIssueCategory(IssueRegistration obj);
        [OperationContract]
        string AddIssueSubCategory(IssueRegistration objMaster);
        [OperationContract]
        List<IssueRegistration> ViewIssueSubCategory(IssueRegistration obj);
        [OperationContract]
         List<IssueRegistration> FillAuthority(IssueRegistration obj);
        [OperationContract]
         string AddHDEscalationConfiguration(IssueRegistration obj);
        [OperationContract]
         int CountEscalationLevel(IssueRegistration objMaster);
        [OperationContract]
         List<IssueRegistration> ViewConfigEscalation(IssueRegistration obj);
        [OperationContract]
         List<IssueRegistration> AutoEscalationProcess(IssueRegistration objMaster);
        [OperationContract]
         List<IssueRegistration> ViewpopConfigEscalation(IssueRegistration obj);
        [OperationContract]
         List<IssueRegistration> EditViewConfigEscalation(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> ViewEscalationEmailRegistration(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> ViewIssueRegistrationMIS(IssueRegistration obj);
        [OperationContract]
        List<IssueRegistration> GetEmailID(IssueRegistration obj);
    }
}