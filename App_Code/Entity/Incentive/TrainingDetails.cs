using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for TrainingDetails
    /// </summary>
    public class TrainingDetails
    {
        public int? SectionId { get; set; }
        public int? Newlyrecruited_NoOfTrainees { get; set; }
        public int? Newlyrecruited_NoOfDays { get; set; }
        public string Newlyrecruited_InHouseOrOutSide { get; set; }
        public string Newlyrecruited_NameOfInstitute { get; set; }

        public int? Skillupgradation_NoOfTrainees { get; set; }
        public int? Skillupgradation_NoOfDays { get; set; }
        public string Skillupgradation_InHouseOrOutSide { get; set; }
        public string Skillupgradation_NameOfInstitute { get; set; }

        public string TotalUnitConsumed { get; set; }
        public decimal? AmountPaid { get; set; }
        public string MoneyReceipt { get; set; }
        public string TraineeDetails { get; set; }
        
        public string vchFyear { get; set; }
        public List<lstTrainingDtlNewRec> NewlyRecruited { get; set; }//XML File Unit
        public List<lstTrainingDtlSkillUpgrade> SkillUpgradation { get; set; }//XML File Unit
    }

    public class lstTrainingDtlNewRec
    {
        
        public string vchTraineeType { get; set; }
        public string vchTraingLoc { get; set; }
        public int intTraingLoc { get; set; }
        public int intNoOftrainee { get; set; }
        public int intNoOfDays { get; set; }
        public string vchOrgName { get; set; }
    }

    public class lstTrainingDtlSkillUpgrade
    {

        public string vchTraineeType { get; set; }
        public string vchTraingLoc { get; set; }
        public int intTraingLoc { get; set; }
        public int intNoOftrainee { get; set; }
        public int intNoOfDays { get; set; }
        public string vchOrgName { get; set; }
    }
}

	