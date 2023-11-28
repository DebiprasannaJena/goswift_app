using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;

/// <summary>
/// Summary description for WaterService
/// </summary>
public class WaterService : IWaterService
{
    public string AddWaterAllotmentDetails(WaterAllotmentDetails objWater)
    {
        WaterServiceProvider objdata = new WaterServiceProvider();
        try
        {
            return objdata.AddWaterAllotmentDetails(objWater);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objdata = null;
        }
    }
    public string UpdateStatus(WaterAllotmentDetails objWater)
    {
        WaterServiceProvider objdata = new WaterServiceProvider();
        try
        {
            return objdata.UpdateStatus(objWater);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objdata = null;
        }
    }
    public DataTable IEName(WaterAllotmentDetails objWater)
    {
        WaterServiceProvider objdata = new WaterServiceProvider();
        try
        {
            return objdata.IEName(objWater);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objdata = null;
        }
    }
}