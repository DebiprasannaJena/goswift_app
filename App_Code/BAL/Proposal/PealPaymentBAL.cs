using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Proposal;
using DataAcessLayer.Proposal;

namespace BusinessLogicLayer.Proposal
{
    /// <summary>
    /// Summary description for PealPaymentBAL
    /// </summary>
    public class PealPaymentBAL : IPealPaymentBAL
    {
        PealPaymentProvider objDataAccess = new PealPaymentProvider();
        public List<PEAL_Update_Payment_Entity> Peal_UpdatePaymentStatus_View(string strActionCode, string strProposalNo)
        {
            return objDataAccess.Peal_UpdatePaymentStatus_View(strActionCode, strProposalNo);
        }

        public int Peal_UpdatePaymentStatus_AED(PEAL_Update_Payment_Entity objEntity)
        {
            return objDataAccess.Peal_UpdatePaymentStatus_AED(objEntity);
        }
    }
}