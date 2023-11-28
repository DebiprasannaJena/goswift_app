/*
 * Created by Ritika lath
 * Created On - 29th March 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Proposal;

namespace BusinessLogicLayer.Proposal
{
    /// <summary>
    /// Summary description for IPealPaymentBAL
    /// </summary>
    public interface IPealPaymentBAL
    {
        List<PEAL_Update_Payment_Entity> Peal_UpdatePaymentStatus_View(string strActionCode, string strProposalNo);
        int Peal_UpdatePaymentStatus_AED(PEAL_Update_Payment_Entity objEntity);  
    }
}