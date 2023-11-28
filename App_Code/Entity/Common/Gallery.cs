using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Common
{
   public class Gallery
    {
       public string strAction { get; set; }
        public int intImageId        {get;set;}   
        public string vchImgDescription {get;set;}
        public string vchImage { get; set; }
        public int intCreatedBy      {get;set;}
        public int intUpdatedBy      {get;set;}
        public int bitDeletedFlag { get; set; }

    }
}
