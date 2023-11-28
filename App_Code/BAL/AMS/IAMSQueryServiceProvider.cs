/*
 * Created By : Ritika Lath
 * Created On : 22nd Feb 2018
 * File Name : IAMSQueryServiceProvider.cs
 * Class name : IAMSQueryServiceProvider
 */

using System.Collections.Generic;
using System.Configuration.Provider;
using EntityLayer.Proposal;

/// <summary>
/// Summary description for IAMSQueryServices
/// </summary>
public abstract class IAMSQueryServiceProvider : ProviderBase
{
    public IAMSQueryServiceProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Function to add edit and update 
    /// </summary>
    /// <param name="objNodalDetails">AMSNodalDetails object</param>
    /// <returns>status as to whether record was updated or not</returns>
    public abstract int AMS_QueryManagement_AED(AMSNodalDetails objNodalDetails);

    /// <summary>
    /// View function for AMS Query Management
    /// </summary>
    /// <param name="objSearch">AMS_Search object</param>
    /// <returns>List of AMSNodalDetails</returns>
    public abstract List<AMSNodalDetails> AMS_QueryManagement_View(AMS_Search objSearch);

    /// <summary>
    /// Function created for view and take action page - AMS Version
    /// Created By Ritika lath on 10th April 2018
    /// </summary>
    /// <param name="objProposal">object of type ProposalDet</param>
    /// <returns>List of Type Proposal Det</returns>
    public abstract List<ProposalDet> getProposalDetails_AMS(ProposalDet objProposal);
}