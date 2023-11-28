using EntityLayer.Incentive;
using System;
using System.Linq;

/// <summary>
/// Summary description for ParamManager
/// </summary>
public class ParamManager
{
    private FormNumber FormNumber { get; set; }
    //Fetch Total number of items defined in an Form
    // Enum.GetNames(typeof(FormNumber)).Length
    private int[] paramArr = new int[28];
    public ParamManager(FormNumber formNumber)
    {
        //
        // TODO: Add constructor logic here
        //
        this.FormNumber = formNumber;
        //Set all the values to zero
        Array.Clear(paramArr, 0, paramArr.Length);

    }

    public int[] GetSectionParam()
    {
        switch (FormNumber)
        {
            case FormNumber.InterestSubsidy_01:
                paramArr[1] = 1;
                paramArr[4] = 1;
                break;
            case FormNumber.PioneerUnits_03:
                paramArr[0] = 3;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[5] = 1;
                paramArr[6] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.PatentRegistration_04:
                paramArr[0] = 4;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[7] = 1;
                paramArr[8] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.SubsidyOnPlantAndMachinery_05:
                paramArr[0] = 5;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[8] = 1;
                paramArr[10] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.StampDutyExemption_06:
                paramArr[0] = 6;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[11] = 1;
                paramArr[8] = 1;
                paramArr[6] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.TechnicalKnowHow_07:
                paramArr[0] = 7;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[12] = 1;
                paramArr[8] = 1;
                paramArr[6] = 1;
                paramArr[15] = 1;
                break; 
            case FormNumber.EmploymentCostSubsidy_09:
                //paramArr[0] = 9;
                paramArr[1] = 1;//// not for industrial unit. rather to call common panel insert proc
                paramArr[2] = 1; //////   production with employee cost subsidy               
                paramArr[8] = 1; ///// avail details
                paramArr[10] = 1; ////////// bank
                paramArr[6] = 1; ///////  additional document
                //paramArr[15] = 1;  //// means of finance
                break;
            case FormNumber.EntreprenuershipDevelopmentSubsidy_10:
                paramArr[0] = 10;
                paramArr[1] = 1;
                paramArr[8] = 1;
                paramArr[10] = 1;
                paramArr[13] = 1;
                paramArr[14] = 1;
                break;
            case FormNumber.PremiumLeviableForConversionOfLandForIndustrialUse_11:
                paramArr[0] = 11;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[15] = 1;
                paramArr[16] = 1;
                paramArr[6] = 1;
                break;
            case FormNumber.ElectricityDuty_12:
                paramArr[0] = 12;
                paramArr[1] = 1;
                paramArr[17] = 1;
                paramArr[10] = 1;
                paramArr[8] = 1;
                break;
            case FormNumber.OneTimeReimbursementOfEnergyAuditCost_13:
                paramArr[0] = 13;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[18] = 1;
                paramArr[19] = 1;
                paramArr[8] = 1;
                paramArr[6] = 1;
                paramArr[15] = 1;
                paramArr[17] = 1;
                break;
            case FormNumber.QualityCertification_14:
                paramArr[0] = 14;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[15] = 1;
                paramArr[20] = 1;
                paramArr[8] = 1;
                paramArr[6] = 1;
                break;
            case FormNumber.PowerTarrif_15:
                paramArr[0] = 15;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[21] = 1;
                paramArr[10] = 1;
                paramArr[6] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.TrainingSubsidy_16:
                paramArr[0] = 16;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[22] = 1;
                paramArr[10] = 1;
                paramArr[6] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.AnchorTenant_17:
                paramArr[0] = 17;
                paramArr[1] = 1;
                paramArr[23] = 1;
                paramArr[3] = 1;
                paramArr[24] = 1;
                paramArr[25] = 1;
                paramArr[10] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.CapitalInvestmentZld_18:
                paramArr[0] = 18;
                paramArr[1] = 1;
                paramArr[2] = 1;
                paramArr[3] = 1;
                paramArr[26] = 1;
                paramArr[10] = 1;
                paramArr[15] = 1;
                break;
            case FormNumber.GrantprioritySector_19:
                paramArr[0] = 19;
                paramArr[1] = 1;
                paramArr[5] = 1;
                paramArr[6] = 1;
                paramArr[25] = 1;
                paramArr[27] = 1;
                break;

        }
        return paramArr;
    }
}