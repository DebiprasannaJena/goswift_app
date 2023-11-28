
namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for PrioritySectorDetails
    /// </summary>
    public class PrioritySectorDetails
    {
        public int? intSectionNo { get; set; }
        public int? intAvailPriorityCertf { get; set; }
        public string strPrioritycCertf2015 { get; set; }
        public string strAppcnAcknow { get; set; }


        //Added By Satyajit Rath

        public int? intSectorId { get; set; }
        public int? intSubSectorId { get; set; }
        public string strDerivedSector { get; set; }
        public int? intLiesSector { get; set; }
        public int? intSpecificActivity { get; set; }
        public string strSpecificActivity { get; set; }
        public string strNote { get; set; }
        public string strSupportingDoc { get; set; }
        public int? intActivityid { get; set; }


    }
}