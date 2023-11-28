using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLayer.Investor;
using DataAcessLayer.Investor;
using System.Data;

namespace BusinessLogicLayer.Investor
{
    public class InvestorBusinessLayer : IInvestorBusinessLayer
    {
        InvestorDataLayer objDataAccess = new InvestorDataLayer();

        public string InvestorData(InvestorDetails objInvestor)
        {
            return objDataAccess.InvestorData(objInvestor);
        }
        public IList<InvestorDetails> ViewInvestorDetailsWeb(InvestorDetails objInvestor)
        {
            return objDataAccess.ViewInvestorDetailsWeb(objInvestor);
        }
        public DataTable ViewInvestorDetails(string UserId, string action)
        {
            return objDataAccess.ViewInvestorDetails(UserId, action);
        }
        public IList<InvestorDetails> PopulateIndustryGroups()
        {
            return objDataAccess.PopulateIndustryGroups();
        }
        public List<InvestorDetails> ViewInvestorDetailsPortal(InvestorDetails objInvestor)
        {
            return objDataAccess.ViewInvestorDetailsPortal(objInvestor);
        }
        public string ApproveIndGroup(InvestorDetails objInvestor)
        {
            return objDataAccess.ApproveIndGroup(objInvestor);
        }
        public string RejectIndGroup(InvestorDetails objInvestor)
        {
            return objDataAccess.RejectIndGroup(objInvestor);
        }
        public string ViewRejectIndGroup(InvestorDetails objInvestor)
        {
            return objDataAccess.ViewRejectIndGroup(objInvestor);
        }
        public string InvestorRegistration(InvestorInfo objInvestor, string strAction)
        {
            return objDataAccess.InvestorRegistration(objInvestor, strAction);
        }
        public string InvApprovalDetails(InvestorDetails objInvestor, string strAction, int InvId, string strRemark)
        {
            return objDataAccess.InvApprovalDetails(objInvestor, strAction, InvId, strRemark);
        }
        public string strOTRStatus(InvestorDetails objInvestor, string strAction, string InvId)
        {
            return objDataAccess.strOTRStatus(objInvestor, strAction, InvId);
        }
        public List<InvestorDetails> ViewInvestorDetailsToInsertInSSO(InvestorDetails objInvestor)
        {
            return objDataAccess.ViewInvestorDetailsToInsertInSSO(objInvestor);
        }
        public string strUpdateUniueInvId(string strAction, string InvId, string strInvestorUId, string strUnitCode)
        {
            return objDataAccess.strUpdateUniueInvId(strAction, InvId, strInvestorUId, strUnitCode);
        }
        public DataTable GetInvestorLoginDetails(InvestorDetails objprop)
        {
            return objDataAccess.GetInvestorLoginDetails(objprop);
        }


        #region Added by Sushant Jena

        public DataTable UserManagementView(InvestorDetails objEntity)
        {
            return objDataAccess.UserManagementView(objEntity);
        }

        public string UserManagementAED(InvestorDetails objEntity)
        {
            return objDataAccess.UserManagementAED(objEntity);
        }

        #endregion

    }
}
