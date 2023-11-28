using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Incentive;
using System.Data;


/// <summary>
/// Summary description for IncentiveManager
/// </summary>
public static class IncentiveManager
{
    private static IIncentiveProvider Provider { get { return new IncentiveProvider(); } }
    public static string CreateIncentive(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentive(incentive);
    }
    public static DataSet GetIncentive(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormNumber parameter is NULL.");
        return Provider.GetIncentive(incentive);
    }
    public static int UpdateSignature(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.UpdateSignature(incentive);
    }
    public static string CreateIncentivePioneer(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentivePioneer(incentive);
    }
    public static string CreateIncentiveCapitalInvst(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveCapitalInvst(incentive);
    }


    public static string CreateIncentiveAnchorTenant(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveAnchorTenant(incentive);
    }

    public static string CreateIncentiveElectricity(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveElectricity(incentive);
    }
    public static string CreateIncentiveTechKHW(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveTechKHW(incentive);
    }

    public static string CreateIncentiveEntSubsidy(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveEntSubsidy(incentive);
    }
    public static string CreateIncentiveInetersSubsidy(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveInetersSubsidy(incentive);
    }
    public static string CreateIncentivePowerTariff(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentivePowerTariff(incentive);
    }
    public static string CreateIncentiveTrainingSubsidy(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveTrainingSubsidy(incentive);
    }

    public static string CreateIncentivePatent(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentivePatent(incentive);
    }


    public static DataSet PrepopulateData(int userid)
    {
        return Provider.PrepopulateData(userid);
    }
    public static DataSet dynamic_name_doc_bind()
    {
        return Provider.dynamic_name_doc_bind();
    }
    public static DataSet PostpopulateData(int intUNQINCID)
    {
        return Provider.PostpopulateData(intUNQINCID);
    }

    public static DataSet patentView(int intUNQINCID)
    {
        return Provider.patentView(intUNQINCID);
    }




    public static DataSet GetSubsidyClaim(Incentive incentive)
    {

        return Provider.GetSubsidyClaim(incentive);
    }
    public static DataSet GetIncentiveMaster(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormNumber parameter is NULL.");
        return Provider.GetIncentiveMaster(incentive);
    }
    public static DataSet PrepopulateFile(Incentive incentive)
    {
        return Provider.PrepopulateFile(incentive);
    }
    public static DataSet BindDropdown(string strActionCode)
    {
        return Provider.BindDropdown(strActionCode);
    }

    public static DataSet PostpopulateDataCostSubsidy(int intUNQINCID)
    {
        return Provider.PostpopulateDataCostSubsidy(intUNQINCID);
    }
    public static string CreateIncentiveEmpCostSubsidy(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveEmpCostSubsidy(incentive);
    }

    public static DataSet PostpopulateCapSubsidy(Incentive incentive)
    {
        return Provider.PostpopulateCapSubsidy(incentive);
    }



    public static DataSet getPreEDDdataExistance(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.getPreEDDdataExistance(incentive);
    }

    public static DataSet GetIncentiveEDD(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.GetIncentiveEDD(incentive);
    }





    public static string CreateIncentiveSubsidyPlant_MAchinery(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveSubsidyPlant_MAchinery(incentive);
    }
    public static DataSet PostpopulateDataSPM(int intUNQINCID)
    {
        return Provider.PostpopulateDataSPM(intUNQINCID);
    }
    public static string CreateIncentiveLandSubsidy(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveLandSubsidy(incentive);
    }
    public static DataSet PostpopulateDataLand(int intUNQINCID)
    {
        return Provider.PostpopulateDataLand(intUNQINCID);
    }


    public static DataSet GetIncentiveAnchorTenant(Incentive incentive)
    {
        return Provider.GetIncentiveAnchorTenant(incentive);
    }
    public static DataSet GetIncentiveOneTmReim(Incentive incentive)
    {
        return Provider.GetIncentiveOneTmReim(incentive);
    }

    public static string CreateIncentive_OneTimeReimbursement(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentive_OneTimeReimbursement(incentive);
    }

    public static DataSet GetIncentivePioneer(Incentive incentive)
    {
        return Provider.GetIncentivePioneer(incentive);
    }

    public static DataTable GetFile_FromDocMaster_UnitCodeWise(string UnitCode)
    {
        return Provider.GetFile_FromDocMaster_UnitCodeWise(UnitCode);
    }

    public static string CreateIncentiveQualityCertificate(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateIncentiveQualityCertificate(incentive);
    }

    public static DataSet GetIncentiveQuality(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormNumber parameter is NULL.");
        return Provider.GetIncentiveQuality(incentive);
    }

    public static DataSet GetIncentivePower(Incentive incentive)
    {
        if (incentive.FormType == null)
            throw new ArgumentException("FormNumber parameter is NULL.");
        return Provider.GetIncentivePower(incentive);
    }

    public static string CreateGrantPriority(Incentive incentive)
    {
        if (incentive == null)
            throw new ArgumentException("Incentive parameter is NULL.");
        return Provider.CreateGrantPriority(incentive);
    }

    public static DataSet GetGrantPriority(Incentive incentive)
    {
        return Provider.GetGrantPriority(incentive);
    }

    public static DataSet PostpopulateEnterpreneurshipSubsidy(Incentive incentive)
    {
        return Provider.PostpopulateEnterpreneurshipSubsidy(incentive);
    }
    public static DataSet InetersSubsidyView(int intUNQINCID)
    {
        return Provider.InetersSubsidyView(intUNQINCID);
    }
    public static DataSet PostpopulateDataPLUS(int intUserId)
    {
        return Provider.PostpopulateDataPLUS(intUserId);
    }

    public static DataSet FillAllActivities(PrioritySectorDetails objPriority)
    {
        return Provider.FillAllActivities(objPriority);
    }
    public static string IsPriorityApp(int IntUserId, int IntAction)
    {
        return Provider.IsPriorityApp(IntUserId, IntAction);

    }


    public static string IsProvisionalCertificate(int IntUserId, string strIncentiveNumber)
    {
        return Provider.IsProvisionalCertificate(IntUserId, strIncentiveNumber);
    }

    public static DataSet ProvisionalThrustsectorpopulateData(int intUNQINCID)
    {
        return Provider.ProvisionalThrustsectorpopulateData(intUNQINCID);
    }
    public static DataSet ProvisionalThrustsectorpopulateDatainDraft(int intUNQINCID)
    {
        return Provider.ProvisionalThrustsectorpopulateDatainDraft(intUNQINCID);
    }
    public static DataSet StampDutyExemptionpopulateData(int intUNQINCID)
    {
        return Provider.StampDutyExemptionpopulateData(intUNQINCID);
    }
    public static DataSet StampDutyExemptionpopulateDatainDraft(int intUNQINCID)
    {
        return Provider.StampDutyExemptionpopulateDatainDraft(intUNQINCID);
    }

}