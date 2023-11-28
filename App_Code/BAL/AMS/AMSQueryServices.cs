/*
Class Name : AMSQueryServices
 * File name: AMSQueryServices.cs
 * Created On : 22nd Feb 2018
 * Created By : Ritika lath
*/
using System.Collections.Generic;
using EntityLayer.Proposal;

/// <summary>
/// Summary description for AMSQueryServices
/// </summary>
public class AMSQueryServices
{
    private static  IAMSQueryServiceProvider eProvider;
    public static IAMSQueryServiceProvider Provider
    {
        get
        {
            eProvider = new AMSQueryServiceProvider();
            return eProvider;
        }
    }

    public AMSQueryServices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AMS Query Management System
    public static int AMS_QueryManagement_AED(AMSNodalDetails ObjNodalDetails)
    {
        return Provider.AMS_QueryManagement_AED(ObjNodalDetails);
    }
    public static List<AMSNodalDetails> AMS_QueryManagement_View(AMS_Search objSearch)
    {
        return Provider.AMS_QueryManagement_View(objSearch);
    }

    public static List<ProposalDet> getProposalDetails_AMS(ProposalDet objProposal)
    {
        return Provider.getProposalDetails_AMS(objProposal);
    }
    #endregion
}