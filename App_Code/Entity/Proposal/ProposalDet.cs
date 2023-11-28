using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
    public class ProposalDet
    {
        #region "Added BY Priti For Proposal Approval"
        public int intProposalId { get; set; }
        public string strFileName { get; set; }
        public decimal decAmount { get; set; }
        public string VCH_RAISE_QUERY { get; set; }
        public decimal decLoadDemand { get; set; }
        public string strActionToBeTakenBY { get; set; }
        public string strAction { get; set; }
        public string strActionTakenBY { get; set; }
        public string strStatus { get; set; }
        public int intActionToBeTakenBy { get; set; }
        public int intCreatedBy { get; set; }
        public int intStatus { get; set; }
        public string strRemarks { get; set; }
        public string vchProposalno { get; set; }

        //added by nibedita behera for PEAL certificate file upload in Take action on 05-09-2017
        public string strPEALCertificate { get; set; }
        public string strScorecard { get; set; }
        public int intQueryStatus { get; set; }
        public int intExtendedStatus { get; set; }
        public string dtmCreatedOn { get; set; }
        public string ActionAuthority { get; set; }
        #endregion

        public int intQueryId { get; set; }
        public string strProposalNo { get; set; }
        public int intRaisedDept { get; set; }
        public int intNoOfTimes { get; set; }
        public string strQuerytype { get; set; }
        public string strAppliedDistBlock { get; set; }
        public int intpaymentStatus { get; set; }
        public int intDistId { get; set; }
        public int intBlockId { get; set; }

        #region "Added By Pranay Kumar on 12-Sept-2017"
        public string strApplicationKey { get; set; }
        public decimal decExtendLand { get; set; }
        public string strQueryStatus { get; set; }
        public int intsts { get; set; }
        public int intApplicFor { get; set; }
        public int intDist { get; set; }
        public string strFrom { get; set; }
        public string strTo { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string SMSContent { get; set; }
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public string compName { get; set; }
        public int intStsdet { get; set; }
        public string strSMSContent { get; set; }
        public string strDeptSMSContent { get; set; }
        public string strDeptSMSSub { get; set; }
        public string strDeptMailContent { get; set; }
        public string strIdcoDocs { get; set; }
        public int intidcoCnt { get; set; }
        public string vchcafno { get; set; }
        public string vchIndustryCode { get; set; }
        public string vchProcessingFeeRealizationStatus { get; set; }
        public string vchPaymentRealizationReferenceNo { get; set; }
        public string vchDemandNoteLink { get; set; }
        public string vchDemandReceipt { get; set; }
        public string vchAllotmentOrderLink { get; set; }
        public string decRecomendLand { get; set; }
        public int intFowardAMS { get; set; }
        public int intForwardIDCO { get; set; }
        public string strApplicationFrom { get; set; }
        public string strApplicationTo { get; set; }
        public string strEmployeemntTo { get; set; }
        public string strProposedInvTo { get; set; }
        public int intQueryRaisedValue { get; set; }
        public string strATOFrom { get; set; }
        public string strATOTo { get; set; }
        public int intLandReqd { get; set; }
        public int INTDISTRICTID { get; set; }
        public string VCHDISTRICTNAME { get; set; }
        public string TOTALAPPLICATION { get; set; }
        public string APPROVED { get; set; }
        public string QUERY1 { get; set; }
        public string QUERY2 { get; set; }
        public string PENDING { get; set; }
        public string REJECTED { get; set; }
        public string EXISTING { get; set; }
        public string PROPOSED { get; set; }
        public string TOTALCAPITALINVESTMENT { get; set; }
        public string STRLANDALLOTED { get; set; }
        public string AVERAGE_APPROVAL { get; set; }
        public string PROPOSED_EMP { get; set; }

        //added by Ritika Lath
        public int intAmsQueryStatus { get; set; }
        public int intNodalOfficerId { get; set; }

        //added by subhasmita on 19th april 2018
        public string IdcoStatus { get; set; }
        public string IdcoBtnStatus { get; set; }


        #endregion

        #region Added by Sushant Jena

        public string strFilterMode { get; set; } //// Added by Sushant Jena On Dt.16-Aug-2018 
        // public string strLandUnit { get; set; } //// Added by Sushant Jena On Dt.18-Feb-2020 

       

        #endregion

        public string strUpdatedOn { get; set; }
        public int strCreatedBy { get; set; }


        #region Added By Satya On 16-04-2019 for Industry List

        public int IntInvestorId { get; set; }
        public int IntApprovalStatus { get; set; }
        public string strPanno { get; set; }
        public string strIndName { get; set; }

        #endregion

        #region Added By MANOJ On 22-05-2019 for Industry UPDATE INFO
        public string VCH_CONTACT_FIRSTNAME { get; set; }
        public string VCH_EMAIL { get; set; }

        public string VCH_OFF_MOBILE { get; set; }
        public string VCH_INVESTOR_ID { get; set; }
        #endregion
       // public string VCH_RAISE_QUERY { get; set; }//Add by Debiprasanna
    }
}
