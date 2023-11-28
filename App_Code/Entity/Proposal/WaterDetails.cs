using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
   public class WaterDetails
    {
       public string Action                  {get;set;}
       public int    intWaterId              {get;set;}
       public string vchProposalNo		     {get;set;}
       public decimal decExisting	         {get;set;}
       public bool   bitGroundWater		     {get;set;}
       public bool   bitSurfaceWater	     {get;set;}
       public bool   bitSeaWater	         {get;set;}
       public bool   bitIdcoSupply		     {get;set;}
       public bool   bitULB { get; set; }
       public bool   bitOther	             {get;set;}
       public decimal decGndWtrProp		     {get;set;}
       public decimal decGndWtrExist { get; set; }
       public decimal decSurfaceWtrProp { get; set; }
       public decimal decSurfaceWtrExist { get; set; }
       public decimal decSeaWtrProp { get; set; }
       public decimal decSeaWtrExist { get; set; }
       public decimal decIdcoWtrProp { get; set; }
       public decimal decIdcoWtrExist { get; set; }
       public decimal decULBWtrProp { get; set; }
       public decimal decULBWtrExist { get; set; }
       public decimal decOtherWtrPropo { get; set; }
       public decimal decOtherWtrExist { get; set; }
       public string vchDetailsOfRain { get; set; }
       public string vchTreatment	         {get;set;}
       public string vchQtmWasteWtr		     {get;set;}
       public string vchMangeHazar		     {get;set;}
      
       public int intCreatedBy { get; set; }
    }
}
