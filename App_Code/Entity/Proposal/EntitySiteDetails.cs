using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
   public class EntitySiteDetails
    {
        #region SiteDetails
        public int intSiteId { get; set; }
        public string vchProposalNo { get; set; }
        public int intDistrictId { get; set; }
        public int intBlockId { get; set; }
        public int intTahasilId { get; set; }
        public string vchVillage { get; set; }
        public string vchProjectArea { get; set; }
        public string vchPresentLand { get; set; }
        public string vchProposedLand { get; set; }
        public string vchProposedLandAcquire { get; set; }
        public string vchOpenStorage { get; set; }
        public string vchParkingArea { get; set; }
        public string vchOtherInfra { get; set; }
        public decimal decBuiltUpArea { get; set; }
        public decimal decVacantArea { get; set; }
        public decimal decGreenArea { get; set; }
        public string vchLandUseStatement { get; set; }
        public int intSitePlanAttach { get; set; }
        public int intLocPlanAttach { get; set; }
        public decimal decDistance { get; set; }
        public int intConnectRoad { get; set; }
        public int intLandDocAttach { get; set; }
        public int intAgriLand { get; set; }
        public int intHighway { get; set; }
        public int intRiver { get; set; }
        public int intForest { get; set; }
        public int intPond { get; set; }
        public int intSea { get; set; }
        public int intMountain { get; set; }
        public int intIndustry { get; set; }
        public string vchAgriLand { get; set; }
        public string vchHighway { get; set; }
        public string vchRiver { get; set; }
        public string vchForest { get; set; }
        public string vchPond { get; set; }
        public string vchSea { get; set; }
        public string vchMountain { get; set; }
        public string vchIndustry { get; set; }
        public string vchSiteEast { get; set; }
        public string vchSiteWest { get; set; }
        public string vchSiteNorth { get; set; }
        public string vchSiteSouth { get; set; }
        public string vchAdditionalInfo { get; set; }
        public string XmlData { get; set; }
        public int intStatus { get; set; }
        public string strAction { get; set; }
        public int intCreatedBy { get; set; }
        #endregion
    }
}
