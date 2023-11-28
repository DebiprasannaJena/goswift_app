using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EntityLayer.Proposal;
using System.Data;

namespace BusinessLogicLayer.Proposal
{
    [ServiceContract]
    public interface IProposalBusinessLayer
    {
      
         [OperationContract]
         string ManPowerBAL(ManPowerDetails objManPower);
         [OperationContract]
         List<ManPowerDetails> ViewManpower(ManPowerDetails objViewmanPower);

         [OperationContract]
         string AddFinacialHelth(FinancialHelth objfinalcial);
         [OperationContract]
         List<FinancialHelth> ViewFinacialHelth(FinancialHelth objfinancial);

         [OperationContract]
         string AddWaterDetails(WaterDetails objwater);
         [OperationContract]
         List<WaterDetails> ViewWaterDetails(WaterDetails objwater);

         [OperationContract]
         string PowerDetails(PowerDetails objPower);
         [OperationContract]
         List<PowerDetails> ViewPowerDetails(PowerDetails objPower);

         [OperationContract]
         string ProposalSiteDetails(EntitySiteDetails objProposal);

         [OperationContract]
         DataTable ViewSiteDetails(string ProposalNo, string action);
    }
}
