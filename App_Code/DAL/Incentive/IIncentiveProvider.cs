using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Incentive;
using System.Data;
/// <summary>
/// Summary description for IIncentiveProvider
/// </summary>
public interface IIncentiveProvider
{
    string CreateIncentive(Incentive incentive);
    DataSet GetIncentive(Incentive incentive);
    int UpdateSignature(Incentive incentive);

    string CreateIncentivePioneer(Incentive incentive);
    string CreateIncentiveCapitalInvst(Incentive incentive);
    string CreateIncentiveSubsidyPlant_MAchinery(Incentive incentive);
    string CreateIncentiveLandSubsidy(Incentive incentive);
    string CreateIncentiveAnchorTenant(Incentive incentive);

    string CreateIncentiveElectricity(Incentive incentive);
    string CreateIncentiveTechKHW(Incentive incentive);
    string CreateIncentiveEntSubsidy(Incentive incentive);
    string CreateIncentiveInetersSubsidy(Incentive incentive);
    string CreateIncentivePowerTariff(Incentive incentive);
    string CreateIncentiveTrainingSubsidy(Incentive incentive);


    DataSet PrepopulateData(int userid);
    DataSet dynamic_name_doc_bind();
    DataSet PostpopulateData(int intUNQINCID);


    #region Jeevan PatentDetail
    string CreateIncentivePatent(Incentive incentive);
    DataSet patentView(int intUNQINCID);
    #endregion


    DataSet GetSubsidyClaim(Incentive incentive);
    DataSet GetIncentiveMaster(Incentive incentive);

    #region Anjali viewdetail
    DataSet PrepopulateFile(Incentive incentive);
    DataSet BindDropdown(string strActionCode);
    #endregion

    string CreateIncentiveEmpCostSubsidy(Incentive incentive);
    DataSet PostpopulateDataCostSubsidy(int intUNQINCID);


    DataSet PostpopulateCapSubsidy(Incentive incentive);


    DataSet getPreEDDdataExistance(Incentive incentive);
    DataSet GetIncentiveEDD(Incentive incentive);


    DataSet PostpopulateDataSPM(int intUNQINCID);
    DataSet PostpopulateDataLand(int intUNQINCID);
    DataSet GetIncentiveAnchorTenant(Incentive incentive);
    DataSet GetIncentiveOneTmReim(Incentive incentive);

    string CreateIncentive_OneTimeReimbursement(Incentive incentive);
    DataSet GetIncentivePioneer(Incentive incentive);


    DataTable GetFile_FromDocMaster_UnitCodeWise(string UnitCode);
    string CreateIncentiveQualityCertificate(Incentive incentive);
    DataSet GetIncentiveQuality(Incentive incentive);
    DataSet GetIncentivePower(Incentive incentive);

    string CreateGrantPriority(Incentive incentive);
    DataSet GetGrantPriority(Incentive incentive);
    DataSet PostpopulateEnterpreneurshipSubsidy(Incentive incentive);
    DataSet InetersSubsidyView(int intUNQINCID);
    DataSet PostpopulateDataPLUS(int intUserId);


    DataSet FillAllActivities(PrioritySectorDetails objPriority);
    string IsPriorityApp(int IntUserId, int IntAction);
    string IsProvisionalCertificate(int IntUserId, string strIncentiveNumber);

    DataSet ProvisionalThrustsectorpopulateData(int intUNQINCID);
    DataSet ProvisionalThrustsectorpopulateDatainDraft(int intUNQINCID);
    DataSet StampDutyExemptionpopulateData(int intUNQINCID);
    DataSet StampDutyExemptionpopulateDatainDraft(int intUNQINCID);
    DataSet ExemptionLandIndustrialUse_populateDatainDraft(int intUNQINCID);
    DataSet ExemptionLandIndustrialUse_ViewData(int intUNQINCID);
    DataSet MigratedIndustrialUnit_populateDatainDraft(int intUNQINCID);
    DataSet MigratedIndustrialUnit_ViewData(int intUNQINCID);
    
}