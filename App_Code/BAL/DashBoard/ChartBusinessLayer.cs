using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EntityLayer.Chart;

/// <summary>
/// Summary description for ChartBusinessLayer
/// </summary>
public class ChartBusinessLayer : IChartBusinessLayer
{
    ChartDataLayer objDAL = new ChartDataLayer();

    public ChartBusinessLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet Feedback_Chart_View(Feedback_Chart_Entity objPolicy)
    {
        return objDAL.Feedback_Chart_View(objPolicy);
    }
}