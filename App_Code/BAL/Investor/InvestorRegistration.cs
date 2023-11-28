using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLayer.Investor;
using DataAcessLayer.Investor;
using System.Data;

namespace BusinessLogicLayer.Investor
{
    public class InvestorRegistration : IInvestorRegistration
    {
        InvestorDataLayer objDataAccess = new InvestorDataLayer();

        public string InvestorRegistrationBAL(EntityLayer.Investor.InvestorInfo objInvestor, string strAction)
        {
            return objDataAccess.InvestorRegistration(objInvestor, strAction);
        }
        public DataTable BindDistrict(string action)
        {
            return objDataAccess.BindDistrict(action);
        }
        public DataTable FillBlock(string action, int districtid)
        {
            return objDataAccess.FillBlock(action, districtid);
        }
        public DataTable GetSMSContent(InvestorDetails objprop)
        {
            return objDataAccess.GetSMSContent(objprop);
        }
        public DataTable GetInvestorName(InvestorDetails objprop)
        {
            return objDataAccess.GetInvestorName(objprop);
        }
        public string InvestorRegistrationNew(InvestorInfo objInvestor, string strAction)
        {
            return objDataAccess.InvestorRegistrationNew(objInvestor, strAction);
        }
        public DataTable PanCheckValidation(string strPanNo, string strAction)
        {
            return objDataAccess.PanCheckValidation(strPanNo, strAction);
        }
        public DataTable ApprovalDetails(InvestorInfo objInvestor, string strAction)
        {
            return objDataAccess.ApprovalDetails(objInvestor, strAction);
        }
        public DataTable CheckSecondLevelUser(InvestorInfo objInvEntity)
        {
            return objDataAccess.CheckSecondLevelUser(objInvEntity);
        }
        public string checkEmailAvail(InvestorInfo objInvestor)
        {
            return objDataAccess.checkEmailAvail(objInvestor);
        }
        public string getUniqueId(InvestorInfo objInvestor)
        {
            return objDataAccess.getUniqueId(objInvestor);
        }
        public string updateUniqueId(InvestorInfo objInvestor)
        {
            return objDataAccess.updateUniqueId(objInvestor);
        }

        public DataTable viewDraftedUsers(InvestorInfo objInvestor)
        {
            return objDataAccess.viewDraftedUsers(objInvestor);
        }

        public DataTable viewUnitDetails(InvestorInfo objInvestor)
        {
            return objDataAccess.viewUnitDetails(objInvestor);
        }
        public DataTable fillUserList2ndLevel(InvestorInfo objInvestor)
        {
            return objDataAccess.fillUserList2ndLevel(objInvestor);
        }
        public string ApplicationPermissionAED(InvestorInfo objInvestor)
        {
            return objDataAccess.ApplicationPermissionAED(objInvestor);
        }
        public DataTable ApplicationPermissionView(InvestorInfo objInvestor)
        {
            return objDataAccess.ApplicationPermissionView(objInvestor);
        }

        public DataTable BindEntityType(string action)
        {
            return objDataAccess.BindEntityType(action);
        }
    }
}
