using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EntityLayer.Chart;

/// <summary>
/// Summary description for IChartBusinessLayer
/// </summary>
public interface IChartBusinessLayer
{
    DataSet Feedback_Chart_View(Feedback_Chart_Entity objEntity);
}