using System.Collections.Generic;
/// <summary>
/// Summary description for AdditionalDocuments
/// </summary>
/// 
namespace EntityLayer.Incentive
{
    public class AdditionalDocuments
    {
        public int? intSectionNo { get; set; }
        public int? intStatutoryCleanOSPCB_NOC { get; set; }
        public int? intStatutoryCleanOSPCB_Consent { get; set; }
        public int? intStatutoryCleanCentralExec { get; set; }
        public int? intStatutoryCleanFSHGSCD { get; set; }
        public int? intStatutoryCleanExplosive_NOC { get; set; }
        public string strClearanceCetiftOSPCB { get; set; }
        public string strCleanApproveAuthorityOSPCB { get; set; }
        public string strValidSatutoryGreenCategory { get; set; }
        public string strCondoDocumentationDelay { get; set; }
        public string strStCleanConsentOSPCB { get; set; }
        public List<Additional> AdditionalDetails { get; set; }//XML

    }
    public class Additional
    {
        public string strDocumentName { get; set; }
        public string strFileName { get; set; }
    }
}