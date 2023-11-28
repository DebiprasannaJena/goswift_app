using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ChartEntityLayer
/// </summary>
/// 
namespace EntityLayer.Chart
{
    public class Feedback_Chart_Entity
    {
        public string strAction { get; set; }

        public int intQuestionId { get; set; }
        public string strAnswer { get; set; }
        public int intAnswerCount { get; set; }
        public string strQuestion { get; set; }
    }
}