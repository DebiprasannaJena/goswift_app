using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ManageMailBusinessLayer
/// </summary>
public class ManageMailBusinessLayer : IManageMailBusinessLayer
{
    ManageMailDataLayer objDAL = new ManageMailDataLayer();

    public ManageMailBusinessLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet Manage_Mail_View(ManageMailEntityLayer objPolicy)
    {
        try
        {
            return objDAL.Manage_Mail_View(objPolicy);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public string Manage_Mail_AED(ManageMailEntityLayer objPolicy)
    {
        try
        {
            return objDAL.Manage_Mail_AED(objPolicy);
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region anil sahoo
    public string Edit_MailData(ManageMailEntityLayer objPolicy)
    {
        try
        {
            return objDAL.Edit_MailData(objPolicy);
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
}