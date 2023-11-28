using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    public class ConsumeLoadDet
    {
        public string strACTION { get; set; }
        public string INTCONSUMEID { get; set; }
        public string INTINCUNQUEID { get; set; }
        public string stringSUPPLYDATE { get; set; }
        public string stringCONSUMENUMBER { get; set; }
        public string stringCONNECTEDLOAD { get; set; }
        public string INTELECTRICITYAVIL { get; set; }
        public string INTCREATEDBY { get; set; }
        public string TstrACTION { get; set; }
        public List<Statedetails> TstrSTATEDETAIL { get; set; }
        public string strbillpmtvoucher { get; set; }
        public string strelecconsumption { get; set; }
        public string strElecDetails { get; set; }
        public string strdpsdocument { get; set; }
        public string strcondocument { get; set; }




    }
    public class Statedetails
    {
        public string strStateName { get; set; }
        public string strStatefrmDate { get; set; }
        public string strStatetodate { get; set; }
        public string strStateAmt { get; set; }
        public string strStateFin { get; set; }
    }
}