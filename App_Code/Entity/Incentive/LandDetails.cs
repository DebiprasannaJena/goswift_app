using System.Collections.Generic;
using System;

/// <summary>
/// Summary description for LandDetails
/// </summary>

    public class LandDetails
    {
        public LandDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int intSectionNo { get; set; }
        public int INCUNQUEID { get; set; }
        public string CostofProject { get; set; }//
        public string LandRequiredAsperReport { get; set; }//
        public string LandRequired { get; set; }//
        public int StausOfLandApply { get; set; }
        public List<LandInfo> Landconverted { get; set; }
        public string LandDocument { get; set; }//
        public string LANDUNDERTAKINGDOC { get; set; }
    }
    [Serializable]
    public class LandInfo
    {
        public string Mouza { get; set; }
        public string KhataNo { get; set; }
        public string PlotNo { get; set; }
        public string Area { get; set; }
        public string PresentKisam { get; set; }
    }
