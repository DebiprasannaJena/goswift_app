/*
 * Created By : Ritika lath
 * Created On : 10th May 2018
 * Class name : UserDetailsServices
 * File name : UserDetailsServices.cs
 */

using System.Collections.Generic;

/// <summary>
/// Summary description for UserDetailsServices
/// </summary>
public class UserDetailsServices
{

    private static  IUserDetailsServiceProvider eProvider;
    public static IUserDetailsServiceProvider Provider
    {
        get
        {
            eProvider = new UserDetailsServiceProvider();
            return eProvider;
        }
    }
    public UserDetailsServices()
    {
    }

    public static Dictionary<int, string> GetUserList(ManageUserDetails objUserDetails)
    {
        return Provider.GetUserList(objUserDetails);
    }

    public static int ManageUser_AED(ManageUserDetails objUserDetails)
    {
        return Provider.ManageUser_AED(objUserDetails);
    }

}