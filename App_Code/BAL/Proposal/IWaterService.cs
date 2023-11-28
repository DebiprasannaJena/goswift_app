using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;

/// <summary>
/// Summary description for IWaterService
/// </summary>


public partial interface IWaterService
{
    string AddWaterAllotmentDetails(WaterAllotmentDetails objWater);
    string UpdateStatus(WaterAllotmentDetails objWater);
}