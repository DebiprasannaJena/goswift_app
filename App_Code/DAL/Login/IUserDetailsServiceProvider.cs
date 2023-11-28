/*
 * Created By : Ritika lath
 * Created On : 10th May 2018
 * Class name : IUserDetailsServiceProvider
 * File name : IUserDetailsServiceProvider.cs
 */

using System.Collections.Generic;
using System.Configuration.Provider;

/// <summary>
/// Summary description for IUserDetailsServiceProvider
/// </summary>
public abstract class IUserDetailsServiceProvider : ProviderBase
{
    public IUserDetailsServiceProvider()
    {
    }

    public abstract Dictionary<int, string> GetUserList(ManageUserDetails objUserDetails);

    public abstract int ManageUser_AED(ManageUserDetails objUserDetails);
}