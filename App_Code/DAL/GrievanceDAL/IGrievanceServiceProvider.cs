using EntityLayer.GrievanceEntity;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IGrievanceServiceProvider
/// </summary>
public abstract class IGrievanceServiceProvider : ProviderBase
{
    public IGrievanceServiceProvider()
    {

    }

    public abstract DataTable FillUnitDetail(GrievanceEntity grievanceEntity);
    public abstract DataTable FillDistrict(GrievanceEntity grievanceEntity);
    public abstract DataTable FillIndustry(GrievanceEntity grievanceEntity);
    public abstract DataTable FillStatus(GrievanceEntity grievanceEntity);
    public abstract DataTable FillGrievanceType(GrievanceEntity grievanceEntity);
    public abstract DataTable FillGrievanceSubType(GrievanceEntity grievanceEntity);
    public abstract string SaveGrievanceDetail(GrievanceEntity grievanceEntity);
    public abstract DataTable DisplayInvestorGrievanceDetail(GrievanceEntity grievanceEntity);

    #region Added By Sushant Jena

    public abstract DataTable ViewGrivTakeActionDetails(GrievanceEntity objGrivEntity);
    public abstract DataTable ViewGrivDetails(GrievanceEntity objGrivEntity);
    public abstract DataSet ViewGrivApplicationDetails(GrievanceEntity objGrivEntity);

    public abstract string TakeActionDetail(GrievanceEntity grievanceEntity);
    public abstract DataTable GetDistrictIdByUser(GrievanceEntity objGrivEntity);

    #endregion

    #region add by anil sahoo

    //public abstract string SaveGrievanceType(GrievanceEntity objGrivEntity);
    //public abstract string SaveGrievanceTypeEdit(GrievanceEntity objGrivEntity);
    //public abstract DataTable ViewGrivTypeSerch(GrievanceEntity objGrivEntity);
    //public abstract DataTable FillGrievanceTypeFilter(GrievanceEntity objGrivEntity);
    //public abstract string SaveGrievanceSubtype(GrievanceEntity objGrivEntity);
    //public abstract DataTable ViewGrivSubTypeDetailsFilter(GrievanceEntity objGrivEntity);
    //public abstract string SaveGrievanceSubTypeEdit(GrievanceEntity objGrivEntity);


    public abstract string AddUpdateGrievanceType(GrievanceEntity objGrivEntity);
    public abstract DataTable ViewGrivTypeDetails(GrievanceEntity objGrivEntity);

    public abstract string AddUpdateGrievanceSubType(GrievanceEntity objGrivEntity);   
    public abstract DataTable ViewGrivSubTypeDetails(GrievanceEntity objGrivEntity);

    public abstract DataTable GetGrievanceSmsContent(GrievanceEntity objGrivEntity);

    public abstract DataTable GetUserInformationSmsEmailSend(GrievanceEntity objGrivEntity);

    public abstract DataTable GrievanceTrackDetails(GrievanceEntity objGrivEntity);
    #endregion

}