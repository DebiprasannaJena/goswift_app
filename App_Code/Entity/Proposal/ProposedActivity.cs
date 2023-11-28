using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
   public class ProposedActivity
   {
       #region "Proposed Activity"
       public string strAction { get; set; }
       public int intActivityId { get; set; }
       public string vchProposalNo { get; set; }
       public string vchCompanyName { get; set; }
       public int intProjectUnderpriority { get; set; }
       public int intPollutioncategory { get; set; }
       public string vchApprovalObtained { get; set; }
       public string vchFiscalIncentiveAvailed { get; set; }
       public string vchFinishedProduct { get; set; }
       public int intCreatedBy { get; set; }
       #endregion

       #region "Material"
       public int intMaterialId { get; set; }      
       public string vchRawmaterial { get; set; }
       public int intIsLocal { get; set; }
       public string vchMaterialSource { get; set; }
       public string vchProduct { get; set; }
       public decimal intAnnualconsumption { get; set; }
       public decimal decAmount { get; set; }
       #endregion

       #region "Finished Product"
       public int intProductId { get; set; }     
       public string vchNICcode { get; set; }      
       public decimal intAnnualcapacity { get; set; }
       #endregion

   }
}
