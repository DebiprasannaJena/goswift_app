using EntityLayer.GrievanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GrievanceServices
/// </summary>
public class GrievanceServices
{
    private static IGrievanceServiceProvider eProvider;
    public static IGrievanceServiceProvider Provider
    {
        get
        {
            eProvider = new GrievanceServiceProvider();
            return eProvider;
        }
    }
    public GrievanceServices()
    {

    }

    public static DataTable FillUnitDetail(GrievanceEntity objSearch)
    {
        return Provider.FillUnitDetail(objSearch);
    }
    public static DataTable FillDistrict(GrievanceEntity objSearch)
    {
        return Provider.FillDistrict(objSearch);
    }
    public static DataTable FillIndustry(GrievanceEntity objSearch)
    {
        return Provider.FillIndustry(objSearch);
    }

    public static DataTable FillStatus(GrievanceEntity objSearch)
    {
        return Provider.FillStatus(objSearch);
    }
    public static DataTable FillGrievanceType(GrievanceEntity objSearch)
    {
        return Provider.FillGrievanceType(objSearch);
    }
    public static DataTable FillGrievanceSubType(GrievanceEntity objSearch)
    {
        return Provider.FillGrievanceSubType(objSearch);
    }
    public static string SaveGrievanceDetail(GrievanceEntity objSearch)
    {
        return Provider.SaveGrievanceDetail(objSearch);
    }
    public static DataTable DisplayInvestorGrievanceDetail(GrievanceEntity objSearch)
    {
        return Provider.DisplayInvestorGrievanceDetail(objSearch);
    }

    /*---------------------------------------------------------------------------*/

    #region Added By Sushant Jena

    public DataTable ViewGrivTakeActionDetails(GrievanceEntity objGrivEntity)
    {
        return Provider.ViewGrivTakeActionDetails(objGrivEntity);
    }
    public DataTable ViewGrivDetails(GrievanceEntity objGrivEntity)
    {
        return Provider.ViewGrivDetails(objGrivEntity);
    }
    public DataSet ViewGrivApplicationDetails(GrievanceEntity objGrivEntity)
    {
        return Provider.ViewGrivApplicationDetails(objGrivEntity);
    }

    public string TakeActionDetail(GrievanceEntity objGrivEntity)
    {
        return Provider.TakeActionDetail(objGrivEntity);
    }
    public DataTable GetDistrictIdByUser(GrievanceEntity objGrivEntity)
    {
        return Provider.GetDistrictIdByUser(objGrivEntity);
    }
    #endregion

    #region add by anil sahoo

    //public static string SaveGrievanceType(GrievanceEntity objSearch)
    //{
    //    return Provider.SaveGrievanceType(objSearch);
    //}
    //public static string SaveGrievanceTypeEdit(GrievanceEntity objSearch)
    //{
    //    return Provider.SaveGrievanceTypeEdit(objSearch);
    //}    
    //public DataTable ViewGrivTypeSerch(GrievanceEntity objGrivEntity)
    //{
    //    return Provider.ViewGrivTypeSerch(objGrivEntity);
    //}
    //public DataTable FillGrievanceTypeFilter(GrievanceEntity objGrivEntity)
    //{
    //    return Provider.FillGrievanceTypeFilter(objGrivEntity);
    //}
    //public   string SaveGrievanceSubtype(GrievanceEntity objSearch)
    //{
    //    return Provider.SaveGrievanceSubtype(objSearch);
    //}   
    //public DataTable ViewGrivSubTypeDetailsFilter(GrievanceEntity objGrivEntity)
    //{
    //    return Provider.ViewGrivSubTypeDetailsFilter(objGrivEntity);
    //}
    //public  string SaveGrievanceSubTypeEdit(GrievanceEntity objSearch)
    //{
    //    return Provider.SaveGrievanceSubTypeEdit(objSearch);
    //}



    public string AddUpdateGrievanceType(GrievanceEntity objSearch)
    {
        return Provider.AddUpdateGrievanceType(objSearch);
    }
    public DataTable ViewGrivTypeDetails(GrievanceEntity objGrivEntity)
    {
        return Provider.ViewGrivTypeDetails(objGrivEntity);
    }

    public string AddUpdateGrievanceSubType(GrievanceEntity objSearch)
    {
        return Provider.AddUpdateGrievanceType(objSearch);
    }
    public DataTable ViewGrivSubTypeDetails(GrievanceEntity objGrivEntity)
    {
        return Provider.ViewGrivSubTypeDetails(objGrivEntity);
    }


    public DataTable GetGrievanceSmsContent(GrievanceEntity objGrivEntity)
    {
        return Provider.GetGrievanceSmsContent(objGrivEntity);
    }



    public DataTable GetUserInformationSmsEmailSend(GrievanceEntity objGrivEntity)
    {
        return Provider.GetUserInformationSmsEmailSend(objGrivEntity);
    }

    public DataTable GrievanceTrackDetails(GrievanceEntity objGrivEntity)
    {
        return Provider.GrievanceTrackDetails(objGrivEntity);
    }


    #endregion
}