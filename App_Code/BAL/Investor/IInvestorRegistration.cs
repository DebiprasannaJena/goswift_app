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
   public interface IInvestorRegistration
    {
        [OperationContract]
        string InvestorRegistrationBAL(InvestorInfo objInvestor, string strAction);
        [OperationContract]
        DataTable BindDistrict(string action);
        [OperationContract]
        DataTable FillBlock(string action,int districtid);
        [OperationContract]
        DataTable GetSMSContent(InvestorDetails objprop);
          [OperationContract]
        DataTable GetInvestorName(InvestorDetails objprop);
        [OperationContract]
        DataTable BindEntityType(string action);
        [OperationContract]
        DataTable BindRegdCountry(string action);
        [OperationContract]
        DataTable BindRegdState(string action, int countryid);
    }
    
}
