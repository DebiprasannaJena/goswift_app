using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EntityLayer.Proposal;
using System.Data;
namespace BusinessLogicLayer.Proposal
{
    [ServiceContract]
    public interface IProposalBAL
    {
        #region Pradeep
        [OperationContract]
        string ProposalLandAED(LandDet objProposal);

        //added by nibedita behera on 16-09-2017
        [OperationContract]
        string UpdateLandApproval(LandDet objProposal);

        [OperationContract]
        List<LandDet> ViewLandDetails(LandDet objProposal);
        [OperationContract]
        List<LandDet> Industrial(LandDet objprop);
        [OperationContract]
        List<PromoterDet> Intermidiate(PromoterDet objprop);
        #endregion
        #region Added By Subhasmita Behera on 27-Jul-2017
        [OperationContract]
        string ProposalPromoterAED(PromoterDet objProposal);
        [OperationContract]
        List<PromoterDet> GetCompanyDetails(PromoterDet objProposal);
        [OperationContract]
        List<PromoterDet> GetPromoterNameDetails(PromoterDet objProposal);
        [OperationContract]
        List<PromoterDet> GetNameDesgDetails(PromoterDet objProposal);
        [OperationContract]
        List<PromoterDet> GetRawMetrialDetails(PromoterDet objProposal);
        [OperationContract]
        List<PromoterDet> GetEnclosureDetails(PromoterDet objProposal);
        [OperationContract]
        List<CNET> GetCNETCompanyDetails(CNET objProposal);
        [OperationContract]
        string ProposalCNETData(PromoterDet objProposal);
        #endregion
        #region "Added By Priti"
        #region "Proposal Approval"
        [OperationContract]
        List<ProposalDet> getProposalDetails(ProposalDet objProposal);
        [OperationContract]
        List<ProposalDet> getProposalDetailsMIS(ProposalDet objProposal);

        //Added by nibedita behera on 17-09-2017for admin view proposal
        [OperationContract]
        List<ProposalDet> getAdminProposalDetails(ProposalDet objProposal);

        [OperationContract]
        string ProposalTakeAction(ProposalDet objProposal);
        [OperationContract]
        List<ProposalDet> PopulateStatus(ProposalDet objProposal);
        #endregion
        #region "Raise Query"
        [OperationContract]
        string ProposalRaiseQuery(ProposalDet objProposal);
        [OperationContract]
        List<ProposalDet> getRaisedQueryDetails(ProposalDet objProposal);
        #endregion
        #region "Project Info"
        [OperationContract]
        List<ProjectInfo> getProjectInfoDetails(ProjectInfo objprop);
        [OperationContract]
        string ProjectInfoAED(ProjectInfo objprop);
        [OperationContract]
        List<ProjectInfo> getProjectLOCDetails(ProjectInfo objprop);
        [OperationContract]
        List<ProjectInfo> getOtherUnitlDetails(ProjectInfo objprop);
        [OperationContract]
        List<ProjectInfo> PopulateProjDropdowns(ProjectInfo objprop);
        [OperationContract]
        List<ProjectInfo> getProductNameDetails(ProjectInfo objprop);
        #endregion
        #endregion
        [OperationContract]
        string Declartion(Declartion objdec);
        [OperationContract]
        List<Declartion> GetDeclartionData(Declartion objDec);
        #region Added by nibedita behera on 18-08-2017 for View PEAL Report
        [OperationContract]
        List<PromoterDet> ViewCompanyInformation(PromoterDet objCompInfo);
        [OperationContract]
        List<LandDet> ViewLandUtility(LandDet objLand);
        #endregion
        #region "Raise Query Service"
        [OperationContract]
        string ServiceProposalRaiseQuery(ProposalDet objProposal);
        [OperationContract]
        List<ProposalDet> ServicegetRaisedQueryDetails(ProposalDet objProposal);

        #region"Added by suroj"
        [OperationContract]
        List<ProjectInfo> GetProposalDtls(ProjectInfo objProposal);
        [OperationContract]
        string AddProposalDtls(ProjectInfo objProposal);
        #endregion
        #endregion
        #region "Added By Pranay Kumar on 10-Sept-2017"
        #region "Check Raise Query Date withing Date Limit"
        [OperationContract]
        int CheckRaiseQStatus(ProposalDet ObjPropasal);
        #endregion
        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE"
        [OperationContract]
        int intExtendDate(string strAction, int intProposalNo);
        #endregion
        #endregion
        [OperationContract]
        List<LandDet> GETMobileNo(LandDet objprop);
        [OperationContract]
        List<LandDet> GETApplicationNoByOrderNo(LandDet objprop);
        [OperationContract]
        List<LandDet> GETMobileNoOfUser(LandDet objprop);
        [OperationContract]
        string ForwardLandToIDCO(ProposalDet objProposal);
        [OperationContract]
        string ProposalAMStatusUpdate(PromoterDet objProposal);
        [OperationContract]
        string ProposalIDCOtatusUpdate(PromoterDet objProposal);
        [OperationContract]
        DataTable GetParentData(String Query);
        [OperationContract]
        DataTable GetChildData(String Query, int val);
        [OperationContract]
        List<ProposalDet> GetMISAllStatus(ProposalDet objProposal);
        [OperationContract]
        string ProposalEnclosurUpdate(PromoterDet objProposal);
        [OperationContract]
        List<ProposalDet> GetMISAllDetailsStatus(ProposalDet objProposal);
        [OperationContract]
        string ProposalESIGNPromoterAED(PromoterDet objProposal);
        [OperationContract]
        List<LandDet> GetAMSUserId(LandDet objLandDet);
        [OperationContract]
        string PEALServiceOrderUpdate(PromoterDet objProposal);
        [OperationContract]
        List<PromoterDet> GetIDCOMISDetailsRPT(PromoterDet objProposal);
        [OperationContract]
        string IDCOBtntatusUpdate(PromoterDet objProposal);
        [OperationContract]
        List<PromoterDet> GetIDCOEmailDetails();

        DataSet GetProposalTrackDetails(ProposalDet objProposal);//add by anil sahoo
        //Add by Debiprasanna
        DataTable QueryDateUpdate(ProposalDet objProposal);
        //Add by Debiprasanna
        string UpdateQueryDate(ProposalDet objProposal);
        //Add by Debiprasanna
        DataSet GetPCTrackDetails(ProposalDet objProposal);
    }

}
