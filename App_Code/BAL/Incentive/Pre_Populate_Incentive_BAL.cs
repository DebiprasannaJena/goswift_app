using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Pre_Populate_Incentive_BAL
/// </summary>
public class Pre_Populate_Incentive_BAL : IPre_Populate_Incentive_BAL
{
    public Pre_Populate_Incentive_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet Pre_Populate_Inct_Data(Pre_Populate_Incentive_Entity objInctEntity)
    {
        Pre_Populate_Incentive_DAL objINCTDal = new Pre_Populate_Incentive_DAL();
        return objINCTDal.Pre_Populate_Inct_Data(objInctEntity);
    }
}