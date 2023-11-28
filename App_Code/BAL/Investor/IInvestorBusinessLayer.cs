using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Investor;
using System.Data;

namespace BusinessLogicLayer.Investor
{
    [ServiceContract]
    public interface IInvestorBusinessLayer
    {
        [OperationContract]
        string InvestorData(InvestorDetails objInvestor);

        [OperationContract]
        IList<InvestorDetails> ViewInvestorDetailsWeb(InvestorDetails objInvestor);
        [OperationContract]
        DataTable ViewInvestorDetails(string UserId, string action);

        [OperationContract]
        IList<InvestorDetails> PopulateIndustryGroups();

        [OperationContract]
        List<InvestorDetails> ViewInvestorDetailsPortal(InvestorDetails objInvestor);

        [OperationContract]
        string ApproveIndGroup(InvestorDetails objInvestor);

        [OperationContract]
        string RejectIndGroup(InvestorDetails objInvestor);

        [OperationContract]
        string ViewRejectIndGroup(InvestorDetails objInvestor);
        [OperationContract]
        string InvestorRegistration(InvestorInfo objInvestor, string strAction);

        [OperationContract]
        string InvApprovalDetails(InvestorDetails objInvestor, string strAction,int InvId,string strRemark);
        [OperationContract]
        string strOTRStatus(InvestorDetails objInvestor, string strAction, string InvId);
        [OperationContract]
        List<InvestorDetails> ViewInvestorDetailsToInsertInSSO(InvestorDetails objInvestor);
        [OperationContract]
        string strUpdateUniueInvId(string strAction, string InvId, string strInvestorUId, string strUnitCode);
        [OperationContract]
        DataTable GetInvestorLoginDetails(InvestorDetails objprop);
    }
}
