/*
 * Created By : Ritika lath
 * Created on : 10th May 2018
 * Description : common class for the Manage user page (lock, unlock and reset password for account of the user)
 * Class name : ManageUserDetails
 * File Name : ManageUserDetails.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManageUserDetails
/// </summary>
public class ManageUserDetails
{
    public int intUserId { get; set; }
    public string strAction { get; set; }
    public int intNoOfLockHours { get; set; }
    public string strNewPassword { get; set; }
    public int intUpdatedBy { get; set; }
}