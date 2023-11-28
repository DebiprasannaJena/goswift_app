using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntityLayer.Service
{
    public class ServiceDetails
    {
        //Added By Girija on 27-Jul-2017 for declaration of variable for Service 
        #region "Service"
        public int intServiceId { get; set; }
        public string strServiceName { get; set; }
        public string strAction { get; set; }
        public string strfullname { get; set; }
        public int userid { get; set; }
        public int Deptid { get; set; }
        public string strdeptname { get; set; }
        public int LinedeptId { get; set; }
        public int OfficeId { get; set; }
        public string strlinedeptname { get; set; }
        public string OfficeName { get; set; }
        public int intConfigId { get; set; }

        public string strUsername { get; set; }

        public int LocationId { get; set; }
        public string StrLocationName { get; set; }

        public int DirectId { get; set; }
        public string StrDireName { get; set; }

        public int DivisionId { get; set; }
        public string StrDivisionName { get; set; }

        public int DistId { get; set; }
        public string StrDistname { get; set; }
        public int intTahashilId { get; set; }

        public int intdiscomeid { get; set; }
        public string vchdiscomename { get; set; }


        public string strProposalId { get; set; }
        public int intSectorId { get; set; }
        public int intSubSectorId { get; set; }
        public string XMLDATA { get; set; }

        public string UploadJs { get; set; }
        public string desigid { get; set; }
        public string strFromdate { get; set; }
        public string strTodate { get; set; }
        public string strFileUpload { get; set; }
        public string strIndType { get; set; }
        public string DTM_CREATEDON { get; set; }
        public int intServiceCategory { get; set; }
        public string strServiceAliasName { get; set; }
        public string intProId2 { get; set; }
        public string vchChallanNo { get; set; }
        public string vchTranscationNo { get; set; }
        public string vchAmount { get; set; }
        public string vchChallanFile { get; set; }
        public string vchChallanDate { get; set; }
        public string TransMode { get; set; }
        #endregion

        #region "Approval Process"
        public string intProposalId { get; set; }
        public string strInvesterName { get; set; }
        public string strApplicationUnqKey { get; set; }
        public int intStatus { get; set; }
        public int intActionTakenBy { get; set; }
        public int intActionTobeTakenBy { get; set; }
        public string strActionTakenBy { get; set; }
        public string strActionTobeTakenBy { get; set; }
        public int intPaymentStatus { get; set; }
        public string strPaymentAmount { get; set; }
        public string strCertificateFilename { get; set; }
        public string strReferenceFilename { get; set; }
        public string strRemark { get; set; }
        public string Requestdate { get; set; }
        public string strStatus { get; set; }
        public string strExcalationDays { get; set; }
        public string strIndustryName { get; set; }
        public int intEscalationId { get; set; }
        public int intCreatedBy { get; set; }
        public string Typeid { get; set; }
        //Added By Pranay Kumar on 11-Sept-2017 for checking query status 
        public int intQueryStatus { get; set; }
        //Ended By Pranay Kumar on 11-Sept-2017 for checking query status
        //Added By Pranay Kumar on 21-Sept-2017 for Checking Current Query Status
        public string strQueryStatus { get; set; }
        //Ended By Pranay Kumar on 21-Sept-2017 for Checking Current Query Status
        #endregion

        public string strFilterMode { get; set; } ///// Added by Sushant Jena On Dt.17-Aug-2018
        public decimal decAppFee { get; set; } ///// Added by Sushant Jena On Dt.15-Apr-2020

        public int ESIGNSTATUS { get; set; }

        public string vchAccountHead { get; set; }

        public int intHOACount { get; set; } ///// Added by Sushant Jena On Dt.19-Feb-2021 
        public int intExternalType { get; set; } ///// Added by Sushant Jena On Dt.19-Feb-2021 


        //Added By Prasun on 18-Aug-2017 for declaration of variable for DepartmentClearance 

        #region DepartmentClearance
        public decimal Dec_Amount { get; set; }
        public string Str_Amount { get; set; }
        public int Int_ServiceType { get; set; }
        public string Str_ExtrnalServiceUrl { get; set; }

        #endregion

        #region ApplicationDetails

        public string str_Department { get; set; }
        public string str_ServicesName { get; set; }
        public string str_ApplicationNo { get; set; }
        public string str_ApplicationStatus { get; set; }
        public string str_ApplicantName { get; set; }
        public string str_checkStatus { get; set; }
        public string str_TradeName { get; set; }
        public string str_TraderAddress { get; set; }
        public string str_LicenseStatus { get; set; }
        public string str_PaymentReceived { get; set; }
        public string str_UlbCode { get; set; }
        public string str_CorrectionRemark { get; set; }
        public string str_QueryStatus { get; set; }
        public string vchType { get; set; }
        #endregion

        #region "DepartmentWise Report"
        public string action { get; set; }
        public int intLevelDetailId { get; set; }
        public string nvchLevelName { get; set; }
        public int INT_SERVICEID { get; set; }
        public string VCH_SERVICENAME { get; set; }
        public string Total_Recieved { get; set; }
        public string Applied { get; set; }
        public string Rejected { get; set; }
        public string Approved { get; set; }
        public string Pending { get; set; }
        public string QueryRasied { get; set; }
        public string QueryReverted { get; set; }
        public string Differed { get; set; }
        public string InProgress { get; set; }
        public string VCH_APPLICATION_UNQ_KEY { get; set; }
        public string INT_STATUS { get; set; }
        public string VCH_INVESTOR_NAME { get; set; }
        public string Payment { get; set; }
        public int intdeptid { get; set; }
        public int INT_PAYMENT_STATUS { get; set; }
        public int intdesignationid { get; set; }
        #endregion

        #region "StatusWise Report"

        public string vchOrderNo { get; set; }
        public string VCH_APPLICATION_NO { get; set; }
        public string vchChallanAmount { get; set; }

        public int vchBankTransStatus { get; set; }
        public string status { get; set; }
        public string SMFrmDat { get; set; }
        public string SMToDt { get; set; }
        public string strFileName { get; set; }
        public string dtmCreatedOn { get; set; }
        #endregion

        public int FormId { get; set; }
        public int PanelId { get; set; }
        public string PanelName { get; set; }
        public string PanelText { get; set; }
        public string strMobileno { get; set; }
        public string Email { get; set; }
        public string InvestorName { get; set; }
        public string strSubject { get; set; }

        public string strBody { get; set; }

        public string smsContent { get; set; }

        public string STRSMSCONTENT { get; set; }
        public string STRMAILCONTENT { get; set; }
        public string STRMOBILENO { get; set; }
        public string STREMAILID { get; set; }
        public string STRINVESTRNAME { get; set; }
        public string STRACTION { get; set; }



        public string ControlName { get; set; }
        public string ControlType { get; set; }
        public string ControlLabel { get; set; }
        public string ControlSize { get; set; }
        public string ControlReq { get; set; }
        public int ControlId { get; set; }

        public string INT_SEQUENCEID { get; set; }
        public string INT_PANEL_ID { get; set; }
        public string PVCH_VALIDATIONTYPE { get; set; }
        public string PVCH_TOOLTIP { get; set; }
        public string PINT_AUTOMAPPING { get; set; }
        public string PVCH_TEXTMODE { get; set; }
        public string PVCH_CSSCLASS { get; set; }
        public string PVCH_DATASOURCE { get; set; }
        public string PVCH_DATAVALUEFIELD { get; set; }
        public string PVCH_DATATEXTFIELD { get; set; }
        public string PVCH_FILEALLOWED { get; set; }
        public string PINT_MAXSIZE { get; set; }
        public string PVCH_OPTION { get; set; }
        public string PVCH_DEFAULTVALUE { get; set; }
        public string PVCH_HEADINGTEXT { get; set; }
        public string PVCH_LABEL_NAME { get; set; }
        public int PINT_LENGTH { get; set; }
        public string PINT_REQVALIDATION { get; set; }
        public string PVCH_PLUGINID { get; set; }
        public string PVCH_CONTROL_ID { get; set; }
        public string PVCH_CONTROL_TYPE { get; set; }
        public string PVCH_CONTROL_NAME { get; set; }
        public int INT_ID { get; set; }
        public int INT_FORM_ID { get; set; }



        public string UpdatedOn { get; set; }
        public string Str_NocFileName { get; set; }

        public string GenerateDemand { get; set; }

        public string Forwarded { get; set; }
        public string VCHINDUSTRIESNAME { get; set; }

        public string VCHINSPECTIONFILENAME { get; set; }

        public string VCH_PAN { get; set; }
        public string VCHRESTRATIONFILENAME { get; set; }
        public string ParentUniqueKey { get; set; }

        public string VCHCURRENTQUERYSTATUSDATE { get; set; }

        public int intNoOfTimes { get; set; } /// Add By Debiprasanna
    }
    public class CPT
    {
        public string TokenNo { get; set; }
        public string PT_AckwonlegementNo { get; set; }
        public string PT_Number { get; set; }
        public string PT_ActType { get; set; }
        public string Status { get; set; }
        public string PT_Status { get; set; }
        public string PT_EmailId { get; set; }
        public string PT_PAN { get; set; }
    }

    public class SMSAndMAILCls
    {
        public string SMApplicationNo { get; set; }
        public string SMSmsStatus { get; set; }
        public string SMMailStatus { get; set; }
        public string SMEmail { get; set; }
        public string SMMobileNo { get; set; }
        public string SMType { get; set; }
        public string SMSMSContent { get; set; }
        public string SMMAILContent { get; set; }
        public int SMDEPTID { get; set; }
        public int SMServiceID { get; set; }
        public string SMFrmDat { get; set; }
        public string SMToDt { get; set; }
        public string SMAppStatus { get; set; }
    }

    public class BPASCheckStatus
    {

        public int sno { get; set; }

        public string applicationreferencenumber { get; set; }

        public string applicationnumber { get; set; }

        public string applicationname { get; set; }

        public string applicationtype { get; set; }

        public string applicationdate { get; set; }

        public string applicationstatus { get; set; }

        public string applicationstatusid { get; set; }

        public string lastactionstatus { get; set; }

        public string workflowstatus { get; set; }
        public string applicationfee { get; set; }
        public string paymentstatus { get; set; }


    }
    public class fileCheckCls
    {
        public string PVCH_LABEL_NAME { get; set; }
        public string PVCH_CONTROL_NAME { get; set; }
        public int PVCH_FORMID { get; set; }
        public string PVCH_COLUMNNAME { get; set; }
        public string PVCH_APPLICATIONKEY { get; set; }
        public string PVCH_ACTIONCODE { get; set; }
        public string VCH_FILENAME { get; set; }
    }

    #region Add anil sahoo

    public class TrackService
    {
        public string StrAction { get; set; }
        public string Str_Application_Id { get; set; }

    }

    #endregion

}
