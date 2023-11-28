using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for IManageMailBusinessLayer
/// </summary>

public interface IManageMailBusinessLayer
{
    DataSet Manage_Mail_View(ManageMailEntityLayer objEntity);
    string Manage_Mail_AED(ManageMailEntityLayer objEntity);
}